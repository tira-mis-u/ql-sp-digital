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

        private DataGridView dgvLibrary = null!;
        private Panel panelActions = null!;
        private Label lblSelectedTitle = null!;
        private Label lblVersion = null!;
        private Label lblSize = null!;
        private Label lblDownloadCount = null!;
        private ProgressBar prgDownload = null!;
        private Label lblProgressText = null!;
        private Button btnDownload = null!;
        private Button btnUninstall = null!;
        private Button btnRefresh = null!;

        public LibraryControl()
        {
            _libraryService = new ThuVienService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadLibrary();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(25);

            // Tiêu đề + Nút làm mới
            Panel panelTop = new Panel();
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 45;

            Label lblHeader = new Label();
            lblHeader.Text = "🎮 KHO THƯ VIỆN SẢN PHẨM ĐÃ SỞ HỮU (MY LIBRARY)";
            lblHeader.Dock = DockStyle.Left;
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblHeader.UseMnemonic = false;

            btnRefresh = new Button();
            btnRefresh.Text = "🔄 Làm mới kho";
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.Width = 140;
            btnRefresh.BackColor = Color.FromArgb(49, 98, 130);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(lblHeader);

            // 1. BẢNG DANH SÁCH GAME TRONG THƯ VIỆN (READ)
            dgvLibrary = new DataGridView();
            dgvLibrary.Location = new Point(25, 65);
            dgvLibrary.Size = new Size(580, 620);
            dgvLibrary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvLibrary.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvLibrary.ForeColor = Color.White;
            dgvLibrary.GridColor = Color.FromArgb(42, 71, 94);
            dgvLibrary.BorderStyle = BorderStyle.None;
            dgvLibrary.RowHeadersVisible = false;
            dgvLibrary.AllowUserToAddRows = false;
            dgvLibrary.ReadOnly = true;
            dgvLibrary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLibrary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLibrary.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvLibrary.DefaultCellStyle.ForeColor = Color.White;
            dgvLibrary.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvLibrary.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvLibrary.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvLibrary.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvLibrary.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvLibrary.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvLibrary.EnableHeadersVisualStyles = false;

            dgvLibrary.Columns.Add("MaSoHuu", "ID");
            dgvLibrary.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvLibrary.Columns.Add("TenDanhMuc", "Thể loại");
            dgvLibrary.Columns.Add("PhienBan", "Phiên bản");
            dgvLibrary.Columns.Add("SoLanTai", "Lượt tải");

            dgvLibrary.Columns["MaSoHuu"].Visible = false;

            dgvLibrary.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLibrary.Columns["TenSanPham"].FillWeight = 65;

            dgvLibrary.Columns["TenDanhMuc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLibrary.Columns["TenDanhMuc"].FillWeight = 35;

            dgvLibrary.Columns["PhienBan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLibrary.Columns["PhienBan"].Width = 85;
            dgvLibrary.Columns["PhienBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvLibrary.Columns["SoLanTai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLibrary.Columns["SoLanTai"].Width = 75;
            dgvLibrary.Columns["SoLanTai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvLibrary.SelectionChanged += DgvLibrary_SelectionChanged;

            // 2. KHUNG HÀNH ĐỘNG TẢI TỆP VÀ GỠ BỎ BÊN PHẢI (UPDATE - DELETE)
            panelActions = new Panel();
            panelActions.Location = new Point(625, 65);
            panelActions.Size = new Size(390, 620);
            panelActions.BackColor = Color.FromArgb(27, 40, 56);
            panelActions.Padding = new Padding(20);

            lblSelectedTitle = new Label();
            lblSelectedTitle.Text = "Chọn sản phẩm trong thư viện";
            lblSelectedTitle.Location = new Point(20, 20);
            lblSelectedTitle.Size = new Size(350, 65);
            lblSelectedTitle.ForeColor = Color.White;
            lblSelectedTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblSelectedTitle.UseMnemonic = false;

            lblVersion = new Label();
            lblVersion.Text = "Phiên bản: ---";
            lblVersion.Location = new Point(20, 90);
            lblVersion.AutoSize = true;
            lblVersion.ForeColor = Color.FromArgb(199, 213, 224);
            lblVersion.Font = new Font("Segoe UI", 10);
            lblVersion.UseMnemonic = false;

            lblSize = new Label();
            lblSize.Text = "Dung lượng tệp: ---";
            lblSize.Location = new Point(20, 120);
            lblSize.AutoSize = true;
            lblSize.ForeColor = Color.FromArgb(199, 213, 224);
            lblSize.Font = new Font("Segoe UI", 10);
            lblSize.UseMnemonic = false;

            lblDownloadCount = new Label();
            lblDownloadCount.Text = "Số lần đã tải: 0 lượt";
            lblDownloadCount.Location = new Point(20, 150);
            lblDownloadCount.AutoSize = true;
            lblDownloadCount.ForeColor = Color.FromArgb(164, 208, 7);
            lblDownloadCount.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblDownloadCount.UseMnemonic = false;

            // Nút Tải xuống
            btnDownload = new Button();
            btnDownload.Text = "⬇️ TẢI XUỐNG VỀ MÁY";
            btnDownload.Location = new Point(20, 195);
            btnDownload.Size = new Size(350, 45);
            btnDownload.BackColor = Color.FromArgb(92, 126, 16);
            btnDownload.ForeColor = Color.White;
            btnDownload.FlatStyle = FlatStyle.Flat;
            btnDownload.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDownload.Cursor = Cursors.Hand;
            btnDownload.UseMnemonic = false;
            btnDownload.FlatAppearance.BorderSize = 0;
            btnDownload.Click += BtnDownload_Click;

            // Tiến trình tải ProgressBar
            lblProgressText = new Label();
            lblProgressText.Text = "Sẵn sàng tải xuống";
            lblProgressText.Location = new Point(20, 255);
            lblProgressText.AutoSize = true;
            lblProgressText.ForeColor = Color.FromArgb(102, 192, 244);
            lblProgressText.Font = new Font("Segoe UI", 9.5f, FontStyle.Italic);
            lblProgressText.UseMnemonic = false;

            prgDownload = new ProgressBar();
            prgDownload.Location = new Point(20, 280);
            prgDownload.Size = new Size(350, 22);
            prgDownload.Value = 0;
            prgDownload.Maximum = 100;

            // Nút Gỡ khỏi Thư viện
            btnUninstall = new Button();
            btnUninstall.Text = "🗑️ Gỡ khỏi Thư viện cá nhân";
            btnUninstall.Location = new Point(20, 550);
            btnUninstall.Size = new Size(350, 38);
            btnUninstall.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUninstall.BackColor = Color.FromArgb(180, 40, 40);
            btnUninstall.ForeColor = Color.White;
            btnUninstall.FlatStyle = FlatStyle.Flat;
            btnUninstall.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnUninstall.Cursor = Cursors.Hand;
            btnUninstall.UseMnemonic = false;
            btnUninstall.FlatAppearance.BorderSize = 0;
            btnUninstall.Click += BtnUninstall_Click;

            panelActions.Controls.Add(btnUninstall);
            panelActions.Controls.Add(prgDownload);
            panelActions.Controls.Add(lblProgressText);
            panelActions.Controls.Add(btnDownload);
            panelActions.Controls.Add(lblDownloadCount);
            panelActions.Controls.Add(lblSize);
            panelActions.Controls.Add(lblVersion);
            panelActions.Controls.Add(lblSelectedTitle);

            this.Controls.Add(panelActions);
            this.Controls.Add(dgvLibrary);
            this.Controls.Add(panelTop);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (dgvLibrary == null || panelActions == null || lblSelectedTitle == null) return;
            int totalW = Math.Max(1040, this.ClientSize.Width);
            int totalH = Math.Max(700, this.ClientSize.Height);

            int actionW = 400;
            int leftW = totalW - actionW - 65; // margin 25 left + 15 gap + 25 right
            int contentH = Math.Max(300, totalH - 85);

            dgvLibrary.Location = new Point(25, 65);
            dgvLibrary.Size = new Size(leftW, contentH);

            panelActions.Location = new Point(25 + leftW + 15, 65);
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

        private void DgvLibrary_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvLibrary.SelectedRows.Count == 0) return;

            int soHuuId = Convert.ToInt32(dgvLibrary.SelectedRows[0].Cells["MaSoHuu"].Value);
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

            if (dgvLibrary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần tải!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int soHuuId = Convert.ToInt32(dgvLibrary.SelectedRows[0].Cells["MaSoHuu"].Value);
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

            if (dgvLibrary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần gỡ khỏi Thư viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int soHuuId = Convert.ToInt32(dgvLibrary.SelectedRows[0].Cells["MaSoHuu"].Value);
            string name = dgvLibrary.SelectedRows[0].Cells["TenSanPham"].Value?.ToString() ?? string.Empty;

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
