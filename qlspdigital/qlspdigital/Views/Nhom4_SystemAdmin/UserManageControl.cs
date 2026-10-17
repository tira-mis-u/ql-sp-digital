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
        private Button btnLock = null!;
        private Button btnUnlock = null!;
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
            lblHeader.Text = "👥 QUẢN TRỊ DANH SÁCH TÀI KHOẢN NGƯỜI DÙNG";
            lblHeader.Location = new Point(25, 15);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 15, FontStyle.Bold);

            // 1. THANH LỌC VÀ TÌM KIẾM
            panelFilter = new Panel();
            panelFilter.Location = new Point(25, 55);
            panelFilter.Size = new Size(950, 45);
            panelFilter.BackColor = Color.FromArgb(27, 40, 56);
            panelFilter.Padding = new Padding(12, 8, 12, 8);

            Label lblRole = new Label();
            lblRole.Text = "Vai trò:";
            lblRole.Location = new Point(12, 12);
            lblRole.AutoSize = true;
            lblRole.ForeColor = Color.White;
            lblRole.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblRole.UseMnemonic = false;

            cboRole = new ComboBox();
            cboRole.Location = new Point(80, 9);
            cboRole.Width = 140;
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Font = new Font("Segoe UI", 9.5f);
            cboRole.BackColor = Color.FromArgb(18, 25, 35);
            cboRole.ForeColor = Color.White;
            cboRole.FlatStyle = FlatStyle.Flat;
            cboRole.Items.AddRange(new string[] { "Tất cả", "QuanTriVien", "KhachHang" });
            cboRole.SelectedIndex = 0;
            cboRole.SelectedIndexChanged += CboRole_SelectedIndexChanged;

            Label lblSearch = new Label();
            lblSearch.Text = "Tìm kiếm:";
            lblSearch.Location = new Point(245, 12);
            lblSearch.AutoSize = true;
            lblSearch.ForeColor = Color.White;
            lblSearch.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblSearch.UseMnemonic = false;

            txtSearch = new TextBox();
            txtSearch.Location = new Point(330, 9);
            txtSearch.Width = 300;
            txtSearch.Font = new Font("Segoe UI", 9.5f);
            txtSearch.BackColor = Color.FromArgb(18, 25, 35);
            txtSearch.ForeColor = Color.White;
            txtSearch.PlaceholderText = "Tìm theo tên đăng nhập, họ tên, email...";
            txtSearch.TextChanged += TxtSearch_TextChanged;

            btnRefresh = new Button();
            btnRefresh.Text = "Làm mới";
            btnRefresh.Location = new Point(650, 7);
            btnRefresh.Size = new Size(95, 30);
            btnRefresh.BackColor = Color.FromArgb(49, 98, 130);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.UseMnemonic = false;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            panelFilter.Controls.Add(btnRefresh);
            panelFilter.Controls.Add(txtSearch);
            panelFilter.Controls.Add(lblSearch);
            panelFilter.Controls.Add(cboRole);
            panelFilter.Controls.Add(lblRole);

            // 2. BẢNG DANH SÁCH NGƯỜI DÙNG (READ)
            dgvUsers = new DataGridView();
            dgvUsers.Location = new Point(25, 110);
            dgvUsers.Size = new Size(950, 520);
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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

            dgvUsers.Columns["MaNguoiDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvUsers.Columns["MaNguoiDung"].Width = 45;
            dgvUsers.Columns["MaNguoiDung"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvUsers.Columns["TenDangNhap"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["TenDangNhap"].FillWeight = 26;

            dgvUsers.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["HoTen"].FillWeight = 36;

            dgvUsers.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["Email"].FillWeight = 38;

            dgvUsers.Columns["SoDienThoai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvUsers.Columns["SoDienThoai"].Width = 110;
            dgvUsers.Columns["SoDienThoai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvUsers.Columns["VaiTro"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvUsers.Columns["VaiTro"].Width = 115;
            dgvUsers.Columns["VaiTro"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvUsers.Columns["SoDuVi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvUsers.Columns["SoDuVi"].Width = 120;
            dgvUsers.Columns["SoDuVi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvUsers.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvUsers.Columns["TrangThai"].Width = 95;
            dgvUsers.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 3. CÁC NÚT THAO TÁC CẬP NHẬT TRẠNG THÁI (UPDATE)
            panelBottomActions = new Panel();
            panelBottomActions.Location = new Point(25, 638);
            panelBottomActions.Size = new Size(950, 50);
            panelBottomActions.BackColor = Color.FromArgb(23, 26, 33);

            btnLock = new Button();
            btnLock.Text = "🔒 Khóa tài khoản";
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
            btnUnlock.Text = "🔓 Mở khóa tài khoản";
            btnUnlock.Location = new Point(195, 5);
            btnUnlock.Size = new Size(205, 38);
            btnUnlock.BackColor = Color.FromArgb(92, 126, 16);
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
            if (panelFilter == null || dgvUsers == null || panelBottomActions == null || txtSearch == null || btnRefresh == null) return;
            int totalW = Math.Max(1040, this.ClientSize.Width);
            int totalH = Math.Max(700, this.ClientSize.Height);

            int fullW = totalW - 50;
            panelFilter.Location = new Point(25, 55);
            panelFilter.Size = new Size(fullW, 45);

            int searchW = Math.Max(260, fullW - 470);
            txtSearch.Size = new Size(searchW, txtSearch.Height);
            btnRefresh.Location = new Point(txtSearch.Location.X + searchW + 15, 7);

            int bottomH = 50;
            int contentH = Math.Max(250, totalH - 110 - bottomH - 25);

            dgvUsers.Location = new Point(25, 110);
            dgvUsers.Size = new Size(fullW, contentH);

            panelBottomActions.Location = new Point(25, 110 + contentH + 10);
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

            int id = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["MaNguoiDung"].Value);
            string username = dgvUsers.SelectedRows[0].Cells["TenDangNhap"].Value?.ToString() ?? string.Empty;

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
