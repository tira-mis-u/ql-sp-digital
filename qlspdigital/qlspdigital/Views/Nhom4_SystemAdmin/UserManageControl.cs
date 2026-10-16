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
    public class UserManageControl : UserControl
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
            LoadUsers();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);

            // Tiêu đề
            Label lblHeader = new Label
            {
                Text = "👥 QUẢN TRỊ DANH SÁCH TÀI KHOẢN NGƯỜI DÙNG",
                Location = new Point(25, 15),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15, FontStyle.Bold)
            };

            // 1. THANH LỌC VÀ TÌM KIẾM
            panelFilter = new Panel
            {
                Location = new Point(25, 55),
                Size = new Size(950, 45),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(12, 8, 12, 8)
            };

            Label lblRole = new Label { Text = "Vai trò:", Location = new Point(12, 12), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), UseMnemonic = false };
            cboRole = new ComboBox { Location = new Point(80, 9), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            cboRole.Items.AddRange(new string[] { "Tất cả", "QuanTriVien", "KhachHang" });
            cboRole.SelectedIndex = 0;
            cboRole.SelectedIndexChanged += (s, e) => LoadUsers();

            Label lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(245, 12), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), UseMnemonic = false };
            txtSearch = new TextBox { Location = new Point(330, 9), Width = 300, Font = new Font("Segoe UI", 9.5f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White, PlaceholderText = "Tìm theo tên đăng nhập, họ tên, email..." };
            txtSearch.TextChanged += (s, e) => LoadUsers();

            btnRefresh = new Button
            {
                Text = "Làm mới",
                Location = new Point(650, 7),
                Size = new Size(95, 30),
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); cboRole.SelectedIndex = 0; LoadUsers(); };

            panelFilter.Controls.Add(btnRefresh);
            panelFilter.Controls.Add(txtSearch);
            panelFilter.Controls.Add(lblSearch);
            panelFilter.Controls.Add(cboRole);
            panelFilter.Controls.Add(lblRole);

            // 2. BẢNG DANH SÁCH NGƯỜI DÙNG (READ)
            dgvUsers = new DataGridView
            {
                Location = new Point(25, 110),
                Size = new Size(950, 520),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackgroundColor = Color.FromArgb(27, 40, 56),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(42, 71, 94),
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
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
            panelBottomActions = new Panel
            {
                Location = new Point(25, 638),
                Size = new Size(950, 50),
                BackColor = Color.FromArgb(23, 26, 33)
            };

            btnLock = new Button
            {
                Text = "🔒 Khóa tài khoản",
                Location = new Point(0, 5),
                Size = new Size(180, 38),
                BackColor = Color.FromArgb(180, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnLock.FlatAppearance.BorderSize = 0;
            btnLock.Click += (s, e) => ChangeStatus(false);

            btnUnlock = new Button
            {
                Text = "🔓 Mở khóa tài khoản",
                Location = new Point(195, 5),
                Size = new Size(205, 38),
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnUnlock.FlatAppearance.BorderSize = 0;
            btnUnlock.Click += (s, e) => ChangeStatus(true);

            panelBottomActions.Controls.Add(btnUnlock);
            panelBottomActions.Controls.Add(btnLock);

            this.Controls.Add(panelBottomActions);
            this.Controls.Add(dgvUsers);
            this.Controls.Add(panelFilter);
            this.Controls.Add(lblHeader);

            panelBottomActions.BringToFront();
            AdjustLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (this.ClientSize.Width < 500 || panelFilter == null || dgvUsers == null || panelBottomActions == null || txtSearch == null || btnRefresh == null) return;
            int totalW = this.ClientSize.Width;
            int totalH = this.ClientSize.Height;

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
    }
}
