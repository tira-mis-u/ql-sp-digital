using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom3_ProductAdmin;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom3_ProductAdmin
{
    // UI 8: Xem và cập nhật Hồ sơ cá nhân, Đổi mật khẩu bảo mật
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public partial class ProfileControl : UserControl
    {
        private readonly HoSoService _hoSoService;

        private Label lblUsername = null!;
        private Label lblRole = null!;
        private Label lblBalance = null!;
        private Label lblCreatedDate = null!;

        private Panel panelInfo = null!;
        private Panel panelProfileForm = null!;
        private Panel panelPasswordForm = null!;
        private Panel panelSecurityBanner = null!;
        private Label lblSecurityContent = null!;

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
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadProfile();
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
            lblHeader.Text = "HỒ SƠ TÀI KHOẢN & CÀI ĐẶT BẢO MẬT";
            lblHeader.Location = new Point(25, 15);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblHeader.UseMnemonic = false;

            // 1. KHUNG TỔNG QUAN TÀI KHOẢN
            panelInfo = new Panel();
            panelInfo.Location = new Point(25, 55);
            panelInfo.Size = new Size(980, 90);
            panelInfo.BackColor = Color.FromArgb(27, 40, 56);
            panelInfo.Padding = new Padding(20);

            lblUsername = new Label();
            lblUsername.Text = "Tài khoản: ---";
            lblUsername.Location = new Point(20, 15);
            lblUsername.AutoSize = true;
            lblUsername.ForeColor = Color.FromArgb(102, 192, 244);
            lblUsername.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblUsername.UseMnemonic = false;

            lblRole = new Label();
            lblRole.Text = "Quyền hạn: ---";
            lblRole.Location = new Point(320, 15);
            lblRole.AutoSize = true;
            lblRole.ForeColor = Color.FromArgb(199, 213, 224);
            lblRole.Font = new Font("Segoe UI", 10.5f);
            lblRole.UseMnemonic = false;

            lblBalance = new Label();
            lblBalance.Text = "Số dư ví: 0 ₫";
            lblBalance.Location = new Point(20, 50);
            lblBalance.AutoSize = true;
            lblBalance.ForeColor = Color.FromArgb(164, 208, 7);
            lblBalance.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblBalance.UseMnemonic = false;

            lblCreatedDate = new Label();
            lblCreatedDate.Text = "Ngày tham gia: ---";
            lblCreatedDate.Location = new Point(320, 50);
            lblCreatedDate.AutoSize = true;
            lblCreatedDate.ForeColor = Color.FromArgb(161, 181, 196);
            lblCreatedDate.Font = new Font("Segoe UI", 9.5f);
            lblCreatedDate.UseMnemonic = false;

            panelInfo.Controls.Add(lblCreatedDate);
            panelInfo.Controls.Add(lblBalance);
            panelInfo.Controls.Add(lblRole);
            panelInfo.Controls.Add(lblUsername);

            // 2. KHUNG FORM CẬP NHẬT THÔNG TIN LIÊN HỆ
            panelProfileForm = new Panel();
            panelProfileForm.Location = new Point(25, 160);
            panelProfileForm.Size = new Size(480, 275);
            panelProfileForm.BackColor = Color.FromArgb(27, 40, 56);
            panelProfileForm.Padding = new Padding(20);

            Label lblForm1 = new Label();
            lblForm1.Text = "THAY ĐỔI THÔNG TIN CÁ NHÂN";
            lblForm1.Location = new Point(20, 15);
            lblForm1.AutoSize = true;
            lblForm1.ForeColor = Color.FromArgb(102, 192, 244);
            lblForm1.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblForm1.UseMnemonic = false;

            Label lblName = new Label();
            lblName.Text = "Họ và tên (*):";
            lblName.Location = new Point(20, 60);
            lblName.AutoSize = true;
            lblName.ForeColor = Color.White;
            lblName.Font = new Font("Segoe UI", 9.5f);
            lblName.UseMnemonic = false;

            txtFullName = new TextBox();
            txtFullName.Location = new Point(165, 57);
            txtFullName.Width = 285;
            txtFullName.Font = new Font("Segoe UI", 10);
            txtFullName.BackColor = Color.FromArgb(18, 25, 35);
            txtFullName.ForeColor = Color.White;
            txtFullName.BorderStyle = BorderStyle.FixedSingle;

            Label lblEmail = new Label();
            lblEmail.Text = "Địa chỉ Email (*):";
            lblEmail.Location = new Point(20, 110);
            lblEmail.AutoSize = true;
            lblEmail.ForeColor = Color.White;
            lblEmail.Font = new Font("Segoe UI", 9.5f);
            lblEmail.UseMnemonic = false;

            txtEmail = new TextBox();
            txtEmail.Location = new Point(165, 107);
            txtEmail.Width = 285;
            txtEmail.Font = new Font("Segoe UI", 10);
            txtEmail.BackColor = Color.FromArgb(18, 25, 35);
            txtEmail.ForeColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;

            Label lblPhone = new Label();
            lblPhone.Text = "Số điện thoại:";
            lblPhone.Location = new Point(20, 160);
            lblPhone.AutoSize = true;
            lblPhone.ForeColor = Color.White;
            lblPhone.Font = new Font("Segoe UI", 9.5f);
            lblPhone.UseMnemonic = false;

            txtPhone = new TextBox();
            txtPhone.Location = new Point(165, 157);
            txtPhone.Width = 285;
            txtPhone.Font = new Font("Segoe UI", 10);
            txtPhone.BackColor = Color.FromArgb(18, 25, 35);
            txtPhone.ForeColor = Color.White;
            txtPhone.BorderStyle = BorderStyle.FixedSingle;

            btnUpdateProfile = new Button();
            btnUpdateProfile.Text = "Cập nhật hồ sơ";
            btnUpdateProfile.Location = new Point(165, 215);
            btnUpdateProfile.Size = new Size(180, 38);
            btnUpdateProfile.BackColor = Color.FromArgb(49, 98, 130);
            btnUpdateProfile.ForeColor = Color.White;
            btnUpdateProfile.FlatStyle = FlatStyle.Flat;
            btnUpdateProfile.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnUpdateProfile.Cursor = Cursors.Hand;
            btnUpdateProfile.UseMnemonic = false;
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
            panelPasswordForm = new Panel();
            panelPasswordForm.Location = new Point(525, 160);
            panelPasswordForm.Size = new Size(480, 275);
            panelPasswordForm.BackColor = Color.FromArgb(27, 40, 56);
            panelPasswordForm.Padding = new Padding(20);

            Label lblForm2 = new Label();
            lblForm2.Text = "ĐỔI MẬT KHẨU BẢO MẬT";
            lblForm2.Location = new Point(20, 15);
            lblForm2.AutoSize = true;
            lblForm2.ForeColor = Color.FromArgb(164, 208, 7);
            lblForm2.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblForm2.UseMnemonic = false;

            Label lblOldPass = new Label();
            lblOldPass.Text = "Mật khẩu cũ:";
            lblOldPass.Location = new Point(20, 60);
            lblOldPass.AutoSize = true;
            lblOldPass.ForeColor = Color.White;
            lblOldPass.Font = new Font("Segoe UI", 9.5f);
            lblOldPass.UseMnemonic = false;

            txtOldPassword = new TextBox();
            txtOldPassword.Location = new Point(165, 57);
            txtOldPassword.Width = 285;
            txtOldPassword.Font = new Font("Segoe UI", 10);
            txtOldPassword.BackColor = Color.FromArgb(18, 25, 35);
            txtOldPassword.ForeColor = Color.White;
            txtOldPassword.BorderStyle = BorderStyle.FixedSingle;
            txtOldPassword.UseSystemPasswordChar = true;

            Label lblNewPass = new Label();
            lblNewPass.Text = "Mật khẩu mới:";
            lblNewPass.Location = new Point(20, 110);
            lblNewPass.AutoSize = true;
            lblNewPass.ForeColor = Color.White;
            lblNewPass.Font = new Font("Segoe UI", 9.5f);
            lblNewPass.UseMnemonic = false;

            txtNewPassword = new TextBox();
            txtNewPassword.Location = new Point(165, 107);
            txtNewPassword.Width = 285;
            txtNewPassword.Font = new Font("Segoe UI", 10);
            txtNewPassword.BackColor = Color.FromArgb(18, 25, 35);
            txtNewPassword.ForeColor = Color.White;
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.UseSystemPasswordChar = true;

            Label lblConfirmPass = new Label();
            lblConfirmPass.Text = "Nhập lại MK mới:";
            lblConfirmPass.Location = new Point(20, 160);
            lblConfirmPass.AutoSize = true;
            lblConfirmPass.ForeColor = Color.White;
            lblConfirmPass.Font = new Font("Segoe UI", 9.5f);
            lblConfirmPass.UseMnemonic = false;

            txtConfirmPassword = new TextBox();
            txtConfirmPassword.Location = new Point(165, 157);
            txtConfirmPassword.Width = 285;
            txtConfirmPassword.Font = new Font("Segoe UI", 10);
            txtConfirmPassword.BackColor = Color.FromArgb(18, 25, 35);
            txtConfirmPassword.ForeColor = Color.White;
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.UseSystemPasswordChar = true;

            btnChangePassword = new Button();
            btnChangePassword.Text = "Xác nhận đổi MK";
            btnChangePassword.Location = new Point(165, 215);
            btnChangePassword.Size = new Size(180, 38);
            btnChangePassword.BackColor = Color.FromArgb(90, 160, 45);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnChangePassword.Cursor = Cursors.Hand;
            btnChangePassword.UseMnemonic = false;
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

            // 4. KHUNG BẢO MẬT & ĐIỀU KHOẢN TÀI KHOẢN (TRÁNH TRỐNG DƯỚI)
            panelSecurityBanner = new Panel();
            panelSecurityBanner.Location = new Point(25, 450);
            panelSecurityBanner.Size = new Size(980, 225);
            panelSecurityBanner.BackColor = Color.FromArgb(27, 40, 56);
            panelSecurityBanner.Padding = new Padding(20);

            Label lblSecurityTitle = new Label();
            lblSecurityTitle.Text = "NGUYÊN TẮC BẢO MẬT TÀI KHOẢN DIGITAL STEAM";
            lblSecurityTitle.Location = new Point(20, 12);
            lblSecurityTitle.AutoSize = true;
            lblSecurityTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblSecurityTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSecurityTitle.UseMnemonic = false;

            lblSecurityContent = new Label();
            lblSecurityContent.Text = "• Mật khẩu nên có độ dài tối thiểu 6 ký tự, bao gồm cả chữ cái và số để đảm bảo tính an toàn cao nhất.\n" +
                                   "• Không chia sẻ thông tin tài khoản hoặc mã License sản phẩm cho bất kỳ ai. Nhân viên hệ thống không bao giờ hỏi mật khẩu của bạn.\n" +
                                   "• Tất cả giao dịch mua phần mềm số và nạp tiền ví được lưu trữ an toàn và bảo chứng bởi hệ thống hóa đơn điện tử.\n" +
                                   "• Khi có nhu cầu hỗ trợ kỹ thuật hoặc khôi phục quyền truy cập, vui lòng liên hệ Ban Quản Trị qua Email hệ thống.";
            lblSecurityContent.Location = new Point(20, 38);
            lblSecurityContent.Size = new Size(940, 85);
            lblSecurityContent.ForeColor = Color.FromArgb(199, 213, 224);
            lblSecurityContent.Font = new Font("Segoe UI", 9.25f);
            lblSecurityContent.UseMnemonic = false;

            panelSecurityBanner.Controls.Add(lblSecurityContent);
            panelSecurityBanner.Controls.Add(lblSecurityTitle);

            this.Controls.Add(panelSecurityBanner);
            this.Controls.Add(panelPasswordForm);
            this.Controls.Add(panelProfileForm);
            this.Controls.Add(panelInfo);
            this.Controls.Add(lblHeader);

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
            if (panelProfileForm == null || panelPasswordForm == null || panelInfo == null || panelSecurityBanner == null || txtFullName == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(400, this.ClientSize.Height);

            int fullW = totalW - 50;

            // 1. Panel Info (Top)
            panelInfo.Location = new Point(25, 55);
            panelInfo.Size = new Size(fullW, 90);
            int halfInfo = fullW / 2;
            lblRole.Location = new Point(Math.Max(320, halfInfo), 15);
            lblCreatedDate.Location = new Point(Math.Max(320, halfInfo), 50);

            // 2. Middle Row: 2 Cards (50% - 50%)
            int cardW = (fullW - 20) / 2;
            panelProfileForm.Location = new Point(25, 160);
            panelProfileForm.Size = new Size(cardW, 275);

            panelPasswordForm.Location = new Point(25 + cardW + 20, 160);
            panelPasswordForm.Size = new Size(cardW, 275);

            int fieldW = Math.Max(180, cardW - 195);
            txtFullName.Width = fieldW;
            txtEmail.Width = fieldW;
            txtPhone.Width = fieldW;
            txtOldPassword.Width = fieldW;
            txtNewPassword.Width = fieldW;
            txtConfirmPassword.Width = fieldW;

            // 3. Security Banner (Bottom)
            int bannerH = Math.Max(140, totalH - 450 - 25);
            panelSecurityBanner.Location = new Point(25, 450);
            panelSecurityBanner.Size = new Size(fullW, bannerH);
            lblSecurityContent.Size = new Size(fullW - 40, Math.Max(85, bannerH - 45));
        }

        public void LoadProfile()
        {
            if (!UserSession.IsLoggedIn)
            {
                lblUsername.Text = "Tài khoản: Khách";
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
