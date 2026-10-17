using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 12: Quản lý danh sách người dùng & Phân quyền / Khóa tài khoản
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public partial class UserManageControl : UserControl
    {
        private readonly NguoiDungService _userService;

        private DataGridView dgvUsers = null!;
        private TextBox txtSearch = null!;
        private ComboBox cboRole = null!;
        private Label lblRole = null!;
        private Label lblSearch = null!;
        private Button btnLock = null!;
        private Button btnUnlock = null!;
        private Button btnSearch = null!;
        private Button btnRefresh = null!;
        private Panel panelFilter = null!;
        private Panel panelBottomActions = null!;

        public UserManageControl()
        {
            _userService = new NguoiDungService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadUsers();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(25);

            // Tiêu đề
            Label lblHeader = new Label();
            lblHeader.Text = "QUẢN TRỊ DANH SÁCH TÀI KHOẢN NGƯỜI DÙNG";
            lblHeader.Location = new Point(25, 15);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 15, FontStyle.Bold);

            // 1. THANH LỌC VÀ TÌM KIẾM
            panelFilter = new Panel();
            panelFilter.Location = new Point(25, 55);
            panelFilter.Size = new Size(950, 50);
            panelFilter.BackColor = Color.FromArgb(27, 40, 56);
            panelFilter.Padding = new Padding(12, 8, 12, 8);

            lblRole = new Label();
            lblRole.Text = "Vai trò:";
            lblRole.Location = new Point(15, 15);
            lblRole.AutoSize = true;
            lblRole.ForeColor = Color.White;
            lblRole.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblRole.UseMnemonic = false;

            cboRole = new ComboBox();
            cboRole.Location = new Point(85, 10);
            cboRole.Width = 145;
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Font = new Font("Segoe UI", 10f);
            cboRole.BackColor = Color.FromArgb(18, 25, 35);
            cboRole.ForeColor = Color.White;
            cboRole.FlatStyle = FlatStyle.Flat;
            cboRole.Items.AddRange(new string[] { "Tất cả", "QuanTriVien", "KhachHang" });
            cboRole.SelectedIndex = 0;
            cboRole.SelectedIndexChanged += CboRole_SelectedIndexChanged;

            lblSearch = new Label();
            lblSearch.Text = "Tìm kiếm:";
            lblSearch.Location = new Point(260, 15);
            lblSearch.AutoSize = true;
            lblSearch.ForeColor = Color.White;
            lblSearch.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblSearch.UseMnemonic = false;

            txtSearch = new TextBox();
            txtSearch.Location = new Point(345, 10);
            txtSearch.Width = 340;
            txtSearch.Font = new Font("Segoe UI", 10f);
            txtSearch.BackColor = Color.FromArgb(18, 25, 35);
            txtSearch.ForeColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.PlaceholderText = "Tìm theo tên đăng nhập, họ tên, email...";
            txtSearch.TextChanged += TxtSearch_TextChanged;

            btnSearch = new Button();
            btnSearch.Text = "Tìm kiếm";
            btnSearch.Location = new Point(695, 9);
            btnSearch.Size = new Size(100, 32);
            btnSearch.BackColor = Color.FromArgb(49, 98, 130);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.UseMnemonic = false;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;

            btnRefresh = new Button();
            btnRefresh.Text = "Làm mới";
            btnRefresh.Location = new Point(805, 9);
            btnRefresh.Size = new Size(100, 32);
            btnRefresh.BackColor = Color.FromArgb(60, 75, 90);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.UseMnemonic = false;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            panelFilter.Controls.Add(btnRefresh);
            panelFilter.Controls.Add(btnSearch);
            panelFilter.Controls.Add(txtSearch);
            panelFilter.Controls.Add(lblSearch);
            panelFilter.Controls.Add(cboRole);
            panelFilter.Controls.Add(lblRole);

            // 2. BẢNG DANH SÁCH NGƯỜI DÙNG (READ)
            dgvUsers = new DataGridView();
            dgvUsers.Location = new Point(25, 110);
            dgvUsers.Size = new Size(950, 520);
            dgvUsers.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvUsers.ForeColor = Color.White;
            dgvUsers.GridColor = Color.FromArgb(42, 71, 94);
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvUsers.DefaultCellStyle.ForeColor = Color.White;
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvUsers.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 45, 62);
            dgvUsers.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(102, 192, 244);
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvUsers.EnableHeadersVisualStyles = false;

            dgvUsers.Columns.Add("MaNguoiDung", "ID");
            dgvUsers.Columns.Add("TenDangNhap", "Tên đăng nhập");
            dgvUsers.Columns.Add("HoTen", "Họ và tên");
            dgvUsers.Columns.Add("Email", "Email");
            dgvUsers.Columns.Add("SoDienThoai", "Số ĐT");
            dgvUsers.Columns.Add("VaiTro", "Vai trò");
            dgvUsers.Columns.Add("SoDuVi", "Số dư ví");
            dgvUsers.Columns.Add("TrangThai", "Trạng thái");

            dgvUsers.Columns["MaNguoiDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["MaNguoiDung"].FillWeight = 7;
            dgvUsers.Columns["MaNguoiDung"].MinimumWidth = 45;
            dgvUsers.Columns["MaNguoiDung"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvUsers.Columns["TenDangNhap"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["TenDangNhap"].FillWeight = 18;
            dgvUsers.Columns["TenDangNhap"].MinimumWidth = 100;

            dgvUsers.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["HoTen"].FillWeight = 22;
            dgvUsers.Columns["HoTen"].MinimumWidth = 110;

            dgvUsers.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["Email"].FillWeight = 23;
            dgvUsers.Columns["Email"].MinimumWidth = 120;

            dgvUsers.Columns["SoDienThoai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["SoDienThoai"].FillWeight = 11;
            dgvUsers.Columns["SoDienThoai"].MinimumWidth = 85;
            dgvUsers.Columns["SoDienThoai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvUsers.Columns["VaiTro"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["VaiTro"].FillWeight = 10;
            dgvUsers.Columns["VaiTro"].MinimumWidth = 80;
            dgvUsers.Columns["VaiTro"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvUsers.Columns["SoDuVi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["SoDuVi"].FillWeight = 11;
            dgvUsers.Columns["SoDuVi"].MinimumWidth = 90;
            dgvUsers.Columns["SoDuVi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvUsers.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["TrangThai"].FillWeight = 10;
            dgvUsers.Columns["TrangThai"].MinimumWidth = 75;
            dgvUsers.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 3. CÁC NÚT THAO TÁC CẬP NHẬT TRẠNG THÁI (UPDATE)
            panelBottomActions = new Panel();
            panelBottomActions.Location = new Point(25, 638);
            panelBottomActions.Size = new Size(950, 50);
            panelBottomActions.BackColor = Color.FromArgb(23, 26, 33);

            btnLock = new Button();
            btnLock.Text = "Khóa tài khoản";
            btnLock.Location = new Point(0, 5);
            btnLock.Size = new Size(180, 38);
            btnLock.BackColor = Color.FromArgb(180, 40, 40);
            btnLock.ForeColor = Color.White;
            btnLock.FlatStyle = FlatStyle.Flat;
            btnLock.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnLock.Cursor = Cursors.Hand;
            btnLock.UseMnemonic = false;
            btnLock.FlatAppearance.BorderSize = 0;
            btnLock.Click += BtnLock_Click;

            btnUnlock = new Button();
            btnUnlock.Text = "Mở khóa tài khoản";
            btnUnlock.Location = new Point(195, 5);
            btnUnlock.Size = new Size(205, 38);
            btnUnlock.BackColor = Color.FromArgb(90, 160, 45);
            btnUnlock.ForeColor = Color.White;
            btnUnlock.FlatStyle = FlatStyle.Flat;
            btnUnlock.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnUnlock.Cursor = Cursors.Hand;
            btnUnlock.UseMnemonic = false;
            btnUnlock.FlatAppearance.BorderSize = 0;
            btnUnlock.Click += BtnUnlock_Click;

            panelBottomActions.Controls.Add(btnUnlock);
            panelBottomActions.Controls.Add(btnLock);

            this.Controls.Add(panelBottomActions);
            this.Controls.Add(dgvUsers);
            this.Controls.Add(panelFilter);
            this.Controls.Add(lblHeader);

            panelBottomActions.BringToFront();

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
            if (panelFilter == null || dgvUsers == null || panelBottomActions == null || txtSearch == null || btnRefresh == null || lblRole == null || lblSearch == null || cboRole == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            int fullW = totalW - 50;
            panelFilter.Location = new Point(25, 55);
            panelFilter.Size = new Size(fullW, 50);

            // Bố cục tuần tự tương đối - không bao giờ chồng lấn nhãn
            lblRole.Location = new Point(15, 15);
            cboRole.Location = new Point(lblRole.Right + 12, 10);
            lblSearch.Location = new Point(cboRole.Right + 24, 15);
            txtSearch.Location = new Point(lblSearch.Right + 12, 10);

            int btnSearchW = 100;
            int btnRefreshW = 100;
            int searchW = Math.Clamp(fullW - txtSearch.Left - btnSearchW - btnRefreshW - 45, 180, 520);
            txtSearch.Size = new Size(searchW, 30);

            btnSearch.Location = new Point(txtSearch.Right + 10, 9);
            btnSearch.Size = new Size(btnSearchW, 32);

            btnRefresh.Location = new Point(btnSearch.Right + 10, 9);
            btnRefresh.Size = new Size(btnRefreshW, 32);

            int bottomH = 46;
            int contentH = Math.Max(180, totalH - 115 - bottomH - 18);

            dgvUsers.Location = new Point(25, 115);
            dgvUsers.Size = new Size(fullW, contentH);

            panelBottomActions.Location = new Point(25, 115 + contentH + 8);
            panelBottomActions.Size = new Size(fullW, bottomH);
        }

        public void LoadUsers()
        {
            dgvUsers.Rows.Clear();
            string? role = cboRole.SelectedItem?.ToString();
            string keyword = txtSearch.Text.Trim();

            var list = _userService.LayDanhSachNguoiDung(role, keyword);
            foreach (var u in list)
            {
                dgvUsers.Rows.Add(
                    u.MaNguoiDung,
                    u.TenDangNhap,
                    u.HoTen,
                    u.Email,
                    u.SoDienThoai ?? "---",
                    u.VaiTro == "QuanTriVien" ? "Quản Trị Viên" : "Khách Hàng",
                    FormatHelper.FormatCurrency(u.SoDuVi),
                    u.TrangThai ? "Hoạt động" : "Bị khóa"
                );
            }
        }

        private void ChangeStatus(bool newStatus)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần thay đổi trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvUsers.SelectedRows[0];
            if (row == null || row.Index < 0) return;
            var val = (dgvUsers.Columns.Contains("MaNguoiDung") ? row.Cells["MaNguoiDung"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;
            int id = Convert.ToInt32(val);
            string username = (dgvUsers.Columns.Contains("TenDangNhap") ? row.Cells["TenDangNhap"].Value : null)?.ToString()
                ?? (row.Cells.Count > 1 ? row.Cells[1].Value?.ToString() : string.Empty) ?? string.Empty;

            string actionText = newStatus ? "Mở khóa" : "Khóa";
            if (MessageBox.Show($"Bạn có chắc chắn muốn {actionText} tài khoản [{username}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ok = newStatus ? _userService.MoKhoaTaiKhoan(id, out string msg) : _userService.KhoaTaiKhoan(id, out msg);
                if (ok)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void CboRole_SelectedIndexChanged(object? sender, EventArgs e) => LoadUsers();
        private void TxtSearch_TextChanged(object? sender, EventArgs e) => LoadUsers();
        private void BtnSearch_Click(object? sender, EventArgs e) => LoadUsers();
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cboRole.SelectedIndex = 0;
            LoadUsers();
        }
        private void BtnLock_Click(object? sender, EventArgs e) => ChangeStatus(false);
        private void BtnUnlock_Click(object? sender, EventArgs e) => ChangeStatus(true);
    }
}
