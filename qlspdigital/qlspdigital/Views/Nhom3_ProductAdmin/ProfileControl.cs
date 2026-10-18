using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom3_ProductAdmin;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom3_ProductAdmin
{
    // UI 8: Xem và cập nhật Hồ sơ cá nhân, Đổi mật khẩu bảo mật & Cài đặt hệ thống
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public partial class ProfileControl : UserControl
    {
        private readonly HoSoService _hoSoService;

        private Label lblHeader = null!;
        private Label lblUsername = null!;
        private Label lblRole = null!;
        private Label lblBalance = null!;
        private Label lblCreatedDate = null!;

        private Panel panelInfo = null!;
        private Panel panelProfileForm = null!;
        private Panel panelPasswordForm = null!;
        private Panel panelSettings = null!;
        private Panel panelSecurityBanner = null!;
        private Label lblSecurityTitle = null!;
        private Label lblSecurityContent = null!;

        private Label lblForm1 = null!;
        private Label lblName = null!;
        private TextBox txtFullName = null!;
        private Label lblEmail = null!;
        private TextBox txtEmail = null!;
        private Label lblPhone = null!;
        private TextBox txtPhone = null!;
        private Button btnUpdateProfile = null!;

        private Label lblForm2 = null!;
        private Label lblOldPass = null!;
        private TextBox txtOldPassword = null!;
        private Label lblNewPass = null!;
        private TextBox txtNewPassword = null!;
        private Label lblConfirmPass = null!;
        private TextBox txtConfirmPassword = null!;
        private Button btnChangePassword = null!;

        private Label lblSettingsTitle = null!;
        private CheckBox chkEmailNotify = null!;
        private CheckBox chkRememberSession = null!;
        private CheckBox chkAutoUpdate = null!;
        private CheckBox chkOrderConfirm = null!;
        private Button btnSaveSettings = null!;

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
            this.AutoScaleMode = AutoScaleMode.Inherit;
            this.Size = new Size(1040, 750);
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(25);

            // Tiêu đề trang
            lblHeader = new Label();
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

            lblForm1 = new Label();
            lblForm1.Text = "THAY ĐỔI THÔNG TIN CÁ NHÂN";
            lblForm1.Location = new Point(20, 15);
            lblForm1.AutoSize = true;
            lblForm1.ForeColor = Color.FromArgb(102, 192, 244);
            lblForm1.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblForm1.UseMnemonic = false;

            lblName = new Label();
            lblName.Text = "Họ và tên (*):";
            lblName.Location = new Point(20, 58);
            lblName.AutoSize = true;
            lblName.ForeColor = Color.White;
            lblName.Font = new Font("Segoe UI", 9.5f);
            lblName.UseMnemonic = false;

            txtFullName = new TextBox();
            txtFullName.Location = new Point(165, 54);
            txtFullName.Width = 285;
            txtFullName.Font = new Font("Segoe UI", 10);
            txtFullName.BackColor = Color.FromArgb(18, 25, 35);
            txtFullName.ForeColor = Color.White;
            txtFullName.BorderStyle = BorderStyle.FixedSingle;

            lblEmail = new Label();
            lblEmail.Text = "Địa chỉ Email (*):";
            lblEmail.Location = new Point(20, 106);
            lblEmail.AutoSize = true;
            lblEmail.ForeColor = Color.White;
            lblEmail.Font = new Font("Segoe UI", 9.5f);
            lblEmail.UseMnemonic = false;

            txtEmail = new TextBox();
            txtEmail.Location = new Point(165, 102);
            txtEmail.Width = 285;
            txtEmail.Font = new Font("Segoe UI", 10);
            txtEmail.BackColor = Color.FromArgb(18, 25, 35);
            txtEmail.ForeColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;

            lblPhone = new Label();
            lblPhone.Text = "Số điện thoại:";
            lblPhone.Location = new Point(20, 154);
            lblPhone.AutoSize = true;
            lblPhone.ForeColor = Color.White;
            lblPhone.Font = new Font("Segoe UI", 9.5f);
            lblPhone.UseMnemonic = false;

            txtPhone = new TextBox();
            txtPhone.Location = new Point(165, 150);
            txtPhone.Width = 285;
            txtPhone.Font = new Font("Segoe UI", 10);
            txtPhone.BackColor = Color.FromArgb(18, 25, 35);
            txtPhone.ForeColor = Color.White;
            txtPhone.BorderStyle = BorderStyle.FixedSingle;

            btnUpdateProfile = new Button();
            btnUpdateProfile.Text = "Lưu thay đổi hồ sơ";
            btnUpdateProfile.Location = new Point(165, 206);
            btnUpdateProfile.Size = new Size(220, 42);
            btnUpdateProfile.BackColor = Color.FromArgb(49, 98, 130);
            btnUpdateProfile.ForeColor = Color.White;
            btnUpdateProfile.FlatStyle = FlatStyle.Flat;
            btnUpdateProfile.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
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

            lblForm2 = new Label();
            lblForm2.Text = "ĐỔI MẬT KHẨU BẢO MẬT";
            lblForm2.Location = new Point(20, 15);
            lblForm2.AutoSize = true;
            lblForm2.ForeColor = Color.FromArgb(164, 208, 7);
            lblForm2.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblForm2.UseMnemonic = false;

            lblOldPass = new Label();
            lblOldPass.Text = "Mật khẩu cũ:";
            lblOldPass.Location = new Point(20, 58);
            lblOldPass.AutoSize = true;
            lblOldPass.ForeColor = Color.White;
            lblOldPass.Font = new Font("Segoe UI", 9.5f);
            lblOldPass.UseMnemonic = false;

            txtOldPassword = new TextBox();
            txtOldPassword.Location = new Point(165, 54);
            txtOldPassword.Width = 285;
            txtOldPassword.Font = new Font("Segoe UI", 10);
            txtOldPassword.BackColor = Color.FromArgb(18, 25, 35);
            txtOldPassword.ForeColor = Color.White;
            txtOldPassword.BorderStyle = BorderStyle.FixedSingle;
            txtOldPassword.UseSystemPasswordChar = true;

            lblNewPass = new Label();
            lblNewPass.Text = "Mật khẩu mới:";
            lblNewPass.Location = new Point(20, 106);
            lblNewPass.AutoSize = true;
            lblNewPass.ForeColor = Color.White;
            lblNewPass.Font = new Font("Segoe UI", 9.5f);
            lblNewPass.UseMnemonic = false;

            txtNewPassword = new TextBox();
            txtNewPassword.Location = new Point(165, 102);
            txtNewPassword.Width = 285;
            txtNewPassword.Font = new Font("Segoe UI", 10);
            txtNewPassword.BackColor = Color.FromArgb(18, 25, 35);
            txtNewPassword.ForeColor = Color.White;
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.UseSystemPasswordChar = true;

            lblConfirmPass = new Label();
            lblConfirmPass.Text = "Nhập lại MK mới:";
            lblConfirmPass.Location = new Point(20, 154);
            lblConfirmPass.AutoSize = true;
            lblConfirmPass.ForeColor = Color.White;
            lblConfirmPass.Font = new Font("Segoe UI", 9.5f);
            lblConfirmPass.UseMnemonic = false;

            txtConfirmPassword = new TextBox();
            txtConfirmPassword.Location = new Point(165, 150);
            txtConfirmPassword.Width = 285;
            txtConfirmPassword.Font = new Font("Segoe UI", 10);
            txtConfirmPassword.BackColor = Color.FromArgb(18, 25, 35);
            txtConfirmPassword.ForeColor = Color.White;
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.UseSystemPasswordChar = true;

            btnChangePassword = new Button();
            btnChangePassword.Text = "Lưu mật khẩu mới";
            btnChangePassword.Location = new Point(165, 206);
            btnChangePassword.Size = new Size(220, 42);
            btnChangePassword.BackColor = Color.FromArgb(90, 160, 45);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
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

            // 4. KHUNG CÀI ĐẶT ỨNG DỤNG & TÙY CHỌN TÀI KHOẢN
            panelSettings = new Panel();
            panelSettings.Location = new Point(25, 450);
            panelSettings.Size = new Size(980, 155);
            panelSettings.BackColor = Color.FromArgb(27, 40, 56);
            panelSettings.Padding = new Padding(20);

            lblSettingsTitle = new Label();
            lblSettingsTitle.Text = "CÀI ĐẶT ỨNG DỤNG & TRẢI NGHIỆM TÀI KHOẢN";
            lblSettingsTitle.Location = new Point(20, 14);
            lblSettingsTitle.AutoSize = true;
            lblSettingsTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblSettingsTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSettingsTitle.UseMnemonic = false;

            chkEmailNotify = new CheckBox();
            chkEmailNotify.Text = "Nhận thông báo đơn hàng và mã kích hoạt bản quyền qua Email";
            chkEmailNotify.Location = new Point(25, 48);
            chkEmailNotify.AutoSize = true;
            chkEmailNotify.ForeColor = Color.FromArgb(199, 213, 224);
            chkEmailNotify.Font = new Font("Segoe UI", 9.5f);
            chkEmailNotify.Checked = true;

            chkRememberSession = new CheckBox();
            chkRememberSession.Text = "Ghi nhớ phiên đăng nhập an toàn trên thiết bị này";
            chkRememberSession.Location = new Point(25, 80);
            chkRememberSession.AutoSize = true;
            chkRememberSession.ForeColor = Color.FromArgb(199, 213, 224);
            chkRememberSession.Font = new Font("Segoe UI", 9.5f);
            chkRememberSession.Checked = true;

            chkAutoUpdate = new CheckBox();
            chkAutoUpdate.Text = "Tự động kiểm tra bản cập nhật và mã kích hoạt khi khởi chạy";
            chkAutoUpdate.Location = new Point(25, 112);
            chkAutoUpdate.AutoSize = true;
            chkAutoUpdate.ForeColor = Color.FromArgb(199, 213, 224);
            chkAutoUpdate.Font = new Font("Segoe UI", 9.5f);
            chkAutoUpdate.Checked = true;

            chkOrderConfirm = new CheckBox();
            chkOrderConfirm.Text = "Hiển thị hộp thoại xác nhận số tiền trước khi trừ ví";
            chkOrderConfirm.Location = new Point(480, 48);
            chkOrderConfirm.AutoSize = true;
            chkOrderConfirm.ForeColor = Color.FromArgb(199, 213, 224);
            chkOrderConfirm.Font = new Font("Segoe UI", 9.5f);
            chkOrderConfirm.Checked = true;

            btnSaveSettings = new Button();
            btnSaveSettings.Text = "Lưu cài đặt ứng dụng";
            btnSaveSettings.Location = new Point(480, 84);
            btnSaveSettings.Size = new Size(240, 42);
            btnSaveSettings.BackColor = Color.FromArgb(49, 98, 130);
            btnSaveSettings.ForeColor = Color.White;
            btnSaveSettings.FlatStyle = FlatStyle.Flat;
            btnSaveSettings.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            btnSaveSettings.Cursor = Cursors.Hand;
            btnSaveSettings.UseMnemonic = false;
            btnSaveSettings.FlatAppearance.BorderSize = 0;
            btnSaveSettings.Click += BtnSaveSettings_Click;

            panelSettings.Controls.Add(btnSaveSettings);
            panelSettings.Controls.Add(chkOrderConfirm);
            panelSettings.Controls.Add(chkAutoUpdate);
            panelSettings.Controls.Add(chkRememberSession);
            panelSettings.Controls.Add(chkEmailNotify);
            panelSettings.Controls.Add(lblSettingsTitle);

            // 5. KHUNG BẢO MẬT & ĐIỀU KHOẢN TÀI KHOẢN (TRÁNH TRỐNG DƯỚI)
            panelSecurityBanner = new Panel();
            panelSecurityBanner.Location = new Point(25, 620);
            panelSecurityBanner.Size = new Size(980, 180);
            panelSecurityBanner.BackColor = Color.FromArgb(27, 40, 56);
            panelSecurityBanner.Padding = new Padding(20);

            lblSecurityTitle = new Label();
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
            lblSecurityContent.AutoSize = true;
            lblSecurityContent.ForeColor = Color.FromArgb(199, 213, 224);
            lblSecurityContent.Font = new Font("Segoe UI", 9.25f);
            lblSecurityContent.UseMnemonic = false;

            panelSecurityBanner.Controls.Add(lblSecurityContent);
            panelSecurityBanner.Controls.Add(lblSecurityTitle);

            this.Controls.Add(panelSecurityBanner);
            this.Controls.Add(panelSettings);
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
            if (panelProfileForm == null || panelPasswordForm == null || panelInfo == null || panelSettings == null || panelSecurityBanner == null || txtFullName == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);

            int fullW = totalW - 50;

            // 1. Panel Info (Top)
            panelInfo.Location = new Point(25, 55);
            panelInfo.Size = new Size(fullW, 90);
            int halfInfo = fullW / 2;
            lblRole.Location = new Point(Math.Max(320, halfInfo), 15);
            lblCreatedDate.Location = new Point(Math.Max(320, halfInfo), 50);

            // 2. Middle Row: 2 Cards (50% - 50%)
            int cardW = (fullW - 20) / 2;
            int cardH = 275;
            panelProfileForm.Location = new Point(25, 160);
            panelProfileForm.Size = new Size(cardW, cardH);

            panelPasswordForm.Location = new Point(25 + cardW + 20, 160);
            panelPasswordForm.Size = new Size(cardW, cardH);

            int fieldW = Math.Max(180, cardW - 195);
            txtFullName.Width = fieldW;
            txtEmail.Width = fieldW;
            txtPhone.Width = fieldW;
            btnUpdateProfile.Location = new Point(165, 206);
            btnUpdateProfile.Width = Math.Min(250, fieldW);

            txtOldPassword.Width = fieldW;
            txtNewPassword.Width = fieldW;
            txtConfirmPassword.Width = fieldW;
            btnChangePassword.Location = new Point(165, 206);
            btnChangePassword.Width = Math.Min(250, fieldW);

            // 3. Settings Card
            int settingsY = panelProfileForm.Bottom + 16;
            panelSettings.Location = new Point(25, settingsY);
            panelSettings.Size = new Size(fullW, 155);

            int col2X = Math.Max(380, fullW / 2);
            chkOrderConfirm.Location = new Point(col2X, 48);
            btnSaveSettings.Location = new Point(col2X, 84);

            // 4. Security Banner (Bottom)
            lblSecurityContent.MaximumSize = new Size(fullW - 40, 0);
            int bannerY = panelSettings.Bottom + 16;
            panelSecurityBanner.Location = new Point(25, bannerY);
            panelSecurityBanner.Size = new Size(fullW, Math.Max(180, lblSecurityContent.Bottom + 25));
        }

        public void LoadProfile()
        {
            if (!UserSession.IsLoggedIn)
            {
                lblUsername.Text = "Tài khoản: Khách (Chưa đăng nhập)";
                lblRole.Text = "Quyền hạn: Chưa đăng nhập";
                lblBalance.Text = "Số dư ví: 0 ₫";
                lblCreatedDate.Text = "Ngày tham gia: ---";
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

                var settings = _hoSoService.LayCaiDat(UserSession.CurrentUserId);
                chkEmailNotify.Checked = settings.ThongBaoEmail;
                chkRememberSession.Checked = settings.GhiNhoDangNhap;
                chkAutoUpdate.Checked = settings.TuDongCapNhat;
                chkOrderConfirm.Checked = settings.XacNhanThanhToan;
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
                UserSession.UpdateProfile(name, email);
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

        private void BtnSaveSettings_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để lưu tùy chọn cài đặt tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool email = chkEmailNotify.Checked;
            bool remember = chkRememberSession.Checked;
            bool auto = chkAutoUpdate.Checked;
            bool confirm = chkOrderConfirm.Checked;

            if (_hoSoService.LuuCaiDat(UserSession.CurrentUserId, email, remember, auto, confirm, out string msg))
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(msg, "Lỗi lưu cài đặt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
