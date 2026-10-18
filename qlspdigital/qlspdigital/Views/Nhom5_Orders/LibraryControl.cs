using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom5_Orders;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom5_Orders
{
    // UI 6: Kho thư viện trò chơi & sản phẩm số đã sở hữu (Steam Library style)
    // Thành viên phụ trách: Phạm Văn Đam (Nhóm 5)
    public partial class LibraryControl : UserControl
    {
        private readonly ThuVienService _libraryService;

        public LibraryControl()
        {
            _libraryService = new ThuVienService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadLibrary();

            ThuVienService.LibraryUpdated += () =>
            {
                if (!this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try { this.BeginInvoke(new Action(LoadLibrary)); } catch { }
                }
            };
            UserSession.SessionChanged += () =>
            {
                if (!this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try { this.BeginInvoke(new Action(LoadLibrary)); } catch { }
                }
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (dgvLibrary == null || panelActions == null || lblSelectedTitle == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            int actionW = Math.Clamp((int)(totalW * 0.35), 320, 420);
            int leftW = Math.Max(260, totalW - actionW - 65); // margin 25 left + 15 gap + 25 right
            int contentH = Math.Max(180, totalH - 100 - 25);

            dgvLibrary.Location = new Point(25, 100);
            dgvLibrary.Size = new Size(leftW, contentH);

            panelActions.Location = new Point(25 + leftW + 15, 100);
            panelActions.Size = new Size(actionW, contentH);

            int btnW = actionW - 40;
            lblSelectedTitle.Width = btnW;
            btnDownload.Width = btnW;
            prgDownload.Width = btnW;
            btnUninstall.Width = btnW;
        }

        public void LoadLibrary()
        {
            dgvLibrary.Rows.Clear();
            prgDownload.Value = 0;
            lblProgressText.Text = "Sẵn sàng tải xuống";

            if (!UserSession.IsLoggedIn)
            {
                lblSelectedTitle.Text = "Vui lòng đăng nhập để xem thư viện";
                return;
            }

            var list = _libraryService.LayKhoThuVien(UserSession.CurrentUserId);
            foreach (var item in list)
            {
                dgvLibrary.Rows.Add(
                    item.MaSoHuu,
                    item.TenSanPham,
                    item.TenDanhMuc,
                    item.PhienBan,
                    $"{item.SoLanTai} lần"
                );
            }

            if (list.Count == 0)
            {
                lblSelectedTitle.Text = "Thư viện trống (Hãy mua game tại Store!)";
            }
        }

        private int GetSelectedSoHuuId()
        {
            if (dgvLibrary.SelectedRows.Count == 0) return 0;
            var row = dgvLibrary.SelectedRows[0];
            if (row == null || row.Index < 0) return 0;
            var val = (dgvLibrary.Columns.Contains("MaSoHuu") ? row.Cells["MaSoHuu"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            return val != null ? Convert.ToInt32(val) : 0;
        }

        private string GetSelectedProductName()
        {
            if (dgvLibrary.SelectedRows.Count == 0) return string.Empty;
            var row = dgvLibrary.SelectedRows[0];
            if (row == null || row.Index < 0) return string.Empty;
            var val = (dgvLibrary.Columns.Contains("TenSanPham") ? row.Cells["TenSanPham"].Value : null) ?? (row.Cells.Count > 1 ? row.Cells[1].Value : null);
            return val?.ToString() ?? string.Empty;
        }

        private void DgvLibrary_SelectionChanged(object? sender, EventArgs e)
        {
            int soHuuId = GetSelectedSoHuuId();
            if (soHuuId == 0) return;

            var list = _libraryService.LayKhoThuVien(UserSession.CurrentUserId);
            var item = list.Find(x => x.MaSoHuu == soHuuId);

            if (item != null)
            {
                lblSelectedTitle.Text = item.TenSanPham;
                lblVersion.Text = $"Phiên bản: {item.PhienBan}";
                lblSize.Text = $"Dung lượng: {FormatHelper.FormatFileSize(item.DungLuongMB)}";
                lblDownloadCount.Text = $"Số lần đã tải: {item.SoLanTai} lượt";
            }
        }

        private async void BtnDownload_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn) return;

            int soHuuId = GetSelectedSoHuuId();
            if (soHuuId == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần tải!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var list = _libraryService.LayKhoThuVien(UserSession.CurrentUserId);
            var item = list.Find(x => x.MaSoHuu == soHuuId);

            if (item == null) return;

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = $"Chọn thư mục lưu tệp cho [{item.TenSanPham}]:";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    btnDownload.Enabled = false;
                    lblProgressText.Text = "Đang kết nối máy chủ CDN và tải tệp...";

                    bool success = await _libraryService.TaiXuongTepAsync(item, fbd.SelectedPath, (p) =>
                    {
                        prgDownload.Value = p;
                        lblProgressText.Text = $"Tiến trình tải: {p}%...";
                    });

                    btnDownload.Enabled = true;

                    if (success)
                    {
                        lblProgressText.Text = "Tải xuống hoàn tất! Đã kiểm tra SHA-256 an toàn.";
                        MessageBox.Show($"Tải tệp thành công về thư mục:\n{fbd.SelectedPath}", "Hoàn tất tải xuống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadLibrary();
                    }
                    else
                    {
                        lblProgressText.Text = "Tải xuống thất bại!";
                        MessageBox.Show("Không thể ghi tệp vào thư mục đích.", "Lỗi tải", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnUninstall_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn) return;

            int soHuuId = GetSelectedSoHuuId();
            if (soHuuId == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần gỡ khỏi Thư viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = GetSelectedProductName();

            if (MessageBox.Show($"Bạn có chắc chắn muốn gỡ [{name}] khỏi Thư viện của mình?", "Xác nhận gỡ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_libraryService.GoKhoiThuVien(soHuuId, UserSession.CurrentUserId, out string msg))
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLibrary();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi gỡ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadLibrary();
        }
    }
}
