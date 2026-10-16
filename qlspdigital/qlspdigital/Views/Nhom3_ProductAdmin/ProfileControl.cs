using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom3_ProductAdmin;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom3_ProductAdmin
{
    // UI 8: Xem và cập nhật Hồ sơ cá nhân, Đổi mật khẩu bảo mật
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public class ProfileControl : UserControl
    {
        private readonly HoSoService _hoSoService;

        private Label lblUsername = null!;
        private Label lblRole = null!;
        private Label lblBalance = null!;
        private Label lblCreatedDate = null!;

        private TextBox txtFullName = null!;
        private TextBox txtEmail = null!;
        private TextBox txtPhone = null!;
        private Button btnUpdateProfile = null!;

        private TextBox txtOldPassword = null!;
        private TextBox txtNewPassword = null!;
        private TextBox txtConfirmPassword = null!;
        private Button btnChangePassword = null!;

        public ProfileControl()
        {
            _hoSoService = new HoSoService();

            InitializeComponent();
            LoadProfile();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);

            // Tiêu đề
            Label lblHeader = new Label
            {
                Text = "👤 HỒ SƠ TÀI KHOẢN & CÀI ĐẶT BẢO MẬT",
                Location = new Point(25, 15),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15, FontStyle.Bold)
            };

            // 1. KHUNG TỔNG QUAN TÀI KHOẢN
            Panel panelInfo = new Panel
            {
                Location = new Point(25, 55),
                Size = new Size(950, 90),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            lblUsername = new Label { Text = "Tài khoản: ---", Location = new Point(20, 15), AutoSize = true, ForeColor = Color.FromArgb(102, 192, 244), Font = new Font("Segoe UI", 11, FontStyle.Bold) };
            lblRole = new Label { Text = "Quyền hạn: ---", Location = new Point(280, 15), AutoSize = true, ForeColor = Color.FromArgb(199, 213, 224), Font = new Font("Segoe UI", 10.5f) };
            lblBalance = new Label { Text = "Số dư ví: 0 ₫", Location = new Point(20, 50), AutoSize = true, ForeColor = Color.FromArgb(164, 208, 7), Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            lblCreatedDate = new Label { Text = "Ngày tham gia: ---", Location = new Point(280, 50), AutoSize = true, ForeColor = Color.FromArgb(161, 181, 196), Font = new Font("Segoe UI", 9.5f) };

            panelInfo.Controls.Add(lblCreatedDate);
            panelInfo.Controls.Add(lblBalance);
            panelInfo.Controls.Add(lblRole);
            panelInfo.Controls.Add(lblUsername);

            // 2. KHUNG FORM CẬP NHẬT THÔNG TIN LIÊN HỆ
            Panel panelProfileForm = new Panel
            {
                Location = new Point(25, 160),
                Size = new Size(460, 320),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            Label lblForm1 = new Label { Text = "📝 THAY ĐỔI THÔNG TIN CÁ NHÂN", Location = new Point(20, 15), AutoSize = true, ForeColor = Color.FromArgb(102, 192, 244), Font = new Font("Segoe UI", 11, FontStyle.Bold) };

            Label lblName = new Label { Text = "Họ và tên (*):", Location = new Point(20, 60), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txtFullName = new TextBox { Location = new Point(140, 57), Width = 280, Font = new Font("Segoe UI", 10), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White };

            Label lblEmail = new Label { Text = "Địa chỉ Email (*):", Location = new Point(20, 110), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txtEmail = new TextBox { Location = new Point(140, 107), Width = 280, Font = new Font("Segoe UI", 10), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White };

            Label lblPhone = new Label { Text = "Số điện thoại:", Location = new Point(20, 160), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txtPhone = new TextBox { Location = new Point(140, 157), Width = 280, Font = new Font("Segoe UI", 10), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White };

            btnUpdateProfile = new Button
            {
                Text = "Cập nhật hồ sơ",
                Location = new Point(140, 220),
                Size = new Size(160, 36),
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnUpdateProfile.FlatAppearance.BorderSize = 0;
            btnUpdateProfile.Click += BtnUpdateProfile_Click;

            panelProfileForm.Controls.Add(btnUpdateProfile);
            panelProfileForm.Controls.Add(txtPhone);
            panelProfileForm.Controls.Add(lblPhone);
            panelProfileForm.Controls.Add(txtEmail);
            panelProfileForm.Controls.Add(lblEmail);
            panelProfileForm.Controls.Add(txtFullName);
            panelProfileForm.Controls.Add(lblName);
            panelProfileForm.Controls.Add(lblForm1);

            // 3. KHUNG FORM ĐỔI MẬT KHẨU
            Panel panelPasswordForm = new Panel
            {
                Location = new Point(515, 160),
                Size = new Size(460, 320),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            Label lblForm2 = new Label { Text = "🔒 ĐỔI MẬT KHẨU BẢO MẬT", Location = new Point(20, 15), AutoSize = true, ForeColor = Color.FromArgb(164, 208, 7), Font = new Font("Segoe UI", 11, FontStyle.Bold) };

            Label lblOldPass = new Label { Text = "Mật khẩu cũ:", Location = new Point(20, 60), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txtOldPassword = new TextBox { Location = new Point(155, 57), Width = 265, Font = new Font("Segoe UI", 10), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White, UseSystemPasswordChar = true };

            Label lblNewPass = new Label { Text = "Mật khẩu mới:", Location = new Point(20, 110), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txtNewPassword = new TextBox { Location = new Point(155, 107), Width = 265, Font = new Font("Segoe UI", 10), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White, UseSystemPasswordChar = true };

            Label lblConfirmPass = new Label { Text = "Nhập lại MK mới:", Location = new Point(20, 160), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txtConfirmPassword = new TextBox { Location = new Point(155, 157), Width = 265, Font = new Font("Segoe UI", 10), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White, UseSystemPasswordChar = true };

            btnChangePassword = new Button
            {
                Text = "Xác nhận đổi MK",
                Location = new Point(155, 220),
                Size = new Size(160, 36),
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.Click += BtnChangePassword_Click;

            panelPasswordForm.Controls.Add(btnChangePassword);
            panelPasswordForm.Controls.Add(txtConfirmPassword);
            panelPasswordForm.Controls.Add(lblConfirmPass);
            panelPasswordForm.Controls.Add(txtNewPassword);
            panelPasswordForm.Controls.Add(lblNewPass);
            panelPasswordForm.Controls.Add(txtOldPassword);
            panelPasswordForm.Controls.Add(lblOldPass);
            panelPasswordForm.Controls.Add(lblForm2);

            this.Controls.Add(panelPasswordForm);
            this.Controls.Add(panelProfileForm);
            this.Controls.Add(panelInfo);
            this.Controls.Add(lblHeader);
        }

        public void LoadProfile()
        {
            if (!UserSession.IsLoggedIn)
            {
                lblUsername.Text = "Tài khoản: Khách vãng lai";
                lblRole.Text = "Chưa đăng nhập";
                lblBalance.Text = "0 ₫";
                lblCreatedDate.Text = "---";
                return;
            }

            var user = _hoSoService.LayHoSo(UserSession.CurrentUserId);
            if (user != null)
            {
                lblUsername.Text = $"Tài khoản: {user.TenDangNhap}";
                lblRole.Text = $"Quyền hạn: {(user.VaiTro == "QuanTriVien" ? "Quản Trị Viên (Admin)" : "Khách Hàng")}";
                lblBalance.Text = $"Số dư ví: {FormatHelper.FormatCurrency(user.SoDuVi)}";
                lblCreatedDate.Text = $"Ngày tham gia: {FormatHelper.FormatDate(user.NgayTao)}";

                txtFullName.Text = user.HoTen;
                txtEmail.Text = user.Email;
                txtPhone.Text = user.SoDienThoai ?? string.Empty;
            }
        }

        private void BtnUpdateProfile_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để cập nhật hồ sơ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (_hoSoService.CapNhatHoSo(UserSession.CurrentUserId, name, email, phone, out string msg))
            {
                UserSession.CurrentFullName = name;
                UserSession.CurrentEmail = email;
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProfile();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnChangePassword_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn) return;

            string oldPass = txtOldPassword.Text;
            string newPass = txtNewPassword.Text;
            string confirmPass = txtConfirmPassword.Text;

            if (_hoSoService.DoiMatKhau(UserSession.CurrentUserId, oldPass, newPass, confirmPass, out string msg))
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOldPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi đổi mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
