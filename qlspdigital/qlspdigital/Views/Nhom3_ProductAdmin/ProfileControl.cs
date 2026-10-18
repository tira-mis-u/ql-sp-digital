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

        public ProfileControl()
        {
            _hoSoService = new HoSoService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadProfile();
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
