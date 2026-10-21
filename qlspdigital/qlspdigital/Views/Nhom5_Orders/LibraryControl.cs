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
            LoadLibrary();
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

            List<ThuVienItemDTO> list = _libraryService.LayKhoThuVien(UserSession.CurrentUserId);
            foreach (ThuVienItemDTO item in list)
            {
                dgvLibrary.Rows.Add(item.MaSoHuu, item.TenSanPham, item.TenDanhMuc, item.PhienBan, $"{item.SoLanTai} lần");
            }

            if (list.Count == 0)
            {
                lblSelectedTitle.Text = "Thư viện trống (Hãy mua game tại Store!)";
                lblVersion.Text = "Phiên bản: -";
                lblSize.Text = "Dung lượng: -";
                lblDownloadCount.Text = "Số lần đã tải: -";
                SetButtonState(btnDownload, false, Color.FromArgb(90, 160, 45));
                SetButtonState(btnUninstall, false, Color.FromArgb(180, 40, 40));
            }
        }

        private void SetButtonState(Button btn, bool enabled, Color activeColor)
        {
            btn.Enabled = enabled;
            if (enabled)
            {
                btn.BackColor = activeColor;
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
            }
            else
            {
                btn.BackColor = Color.FromArgb(42, 54, 66);
                btn.ForeColor = Color.FromArgb(105, 120, 135);
                btn.Cursor = Cursors.No;
            }
        }

        private int GetSelectedSoHuuId()
        {
            if (dgvLibrary.SelectedRows.Count == 0) return 0;
            DataGridViewRow row = dgvLibrary.SelectedRows[0];
            if (row == null || row.Index < 0) return 0;
            object? val = (dgvLibrary.Columns.Contains("MaSoHuu") ? row.Cells["MaSoHuu"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            return val != null ? Convert.ToInt32(val) : 0;
        }

        private string GetSelectedProductName()
        {
            if (dgvLibrary.SelectedRows.Count == 0) return string.Empty;
            DataGridViewRow row = dgvLibrary.SelectedRows[0];
            if (row == null || row.Index < 0) return string.Empty;
            object? val = (dgvLibrary.Columns.Contains("TenSanPham") ? row.Cells["TenSanPham"].Value : null) ?? (row.Cells.Count > 1 ? row.Cells[1].Value : null);
            return val?.ToString() ?? string.Empty;
        }

        private void DgvLibrary_SelectionChanged(object? sender, EventArgs e)
        {
            int soHuuId = GetSelectedSoHuuId();
            if (soHuuId == 0)
            {
                SetButtonState(btnDownload, false, Color.FromArgb(90, 160, 45));
                SetButtonState(btnUninstall, false, Color.FromArgb(180, 40, 40));
                return;
            }

            List<ThuVienItemDTO> list = _libraryService.LayKhoThuVien(UserSession.CurrentUserId);
            ThuVienItemDTO? item = list.Find(x => x.MaSoHuu == soHuuId);

            if (item != null)
            {
                lblSelectedTitle.Text = item.TenSanPham;
                lblVersion.Text = $"Phiên bản: {item.PhienBan}";
                lblSize.Text = $"Dung lượng: {FormatHelper.FormatFileSize(item.DungLuongMB)}";
                lblDownloadCount.Text = $"Số lần đã tải: {item.SoLanTai} lượt";
                SetButtonState(btnDownload, true, Color.FromArgb(90, 160, 45));
                SetButtonState(btnUninstall, true, Color.FromArgb(180, 40, 40));
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

            List<ThuVienItemDTO> list = _libraryService.LayKhoThuVien(UserSession.CurrentUserId);
            ThuVienItemDTO? item = list.Find(x => x.MaSoHuu == soHuuId);

            if (item == null) return;

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = $"Chọn thư mục lưu tệp cho [{item.TenSanPham}]:";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    UpdateDownloadProgress(0);
                    SetButtonState(btnDownload, false, Color.FromArgb(90, 160, 45));
                    SetButtonState(btnUninstall, false, Color.FromArgb(180, 40, 40));
                    UpdateDownloadProgress(20);
                    lblProgressText.Text = "Đang kết nối máy chủ CDN và tải tệp...";
                    await Task.Delay(100);
                    UpdateDownloadProgress(50);

                    bool success = await _libraryService.TaiXuongTepAsync(item, fbd.SelectedPath);

                    SetButtonState(btnDownload, true, Color.FromArgb(90, 160, 45));
                    SetButtonState(btnUninstall, true, Color.FromArgb(180, 40, 40));

                    if (success)
                    {
                        UpdateDownloadProgress(100);
                        lblProgressText.Text = "Tải xuống hoàn tất! Đã kiểm tra SHA-256 an toàn.";
                        await Task.Delay(250);
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

        private void UpdateDownloadProgress(int percent)
        {
            percent = Math.Clamp(percent, 0, 100);
            if (percent < 100)
            {
                // Kỹ thuật WinForms Instant-Snap: Windows bỏ qua hiệu ứng trễ 500ms khi giá trị giảm
                prgDownload.Value = percent + 1;
                prgDownload.Value = percent;
            }
            else
            {
                prgDownload.Maximum = 101;
                prgDownload.Value = 101;
                prgDownload.Maximum = 100;
                prgDownload.Value = 100;
            }
            lblProgressText.Text = $"Tiến trình tải: {percent}%...";
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

        private void BtnRefresh_Click(object? sender, EventArgs e) => LoadLibrary();
    }
}
