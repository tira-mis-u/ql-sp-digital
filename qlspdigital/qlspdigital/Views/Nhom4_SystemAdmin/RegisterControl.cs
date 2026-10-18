using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 1: Màn hình Đăng ký tài khoản khách hàng mới
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public partial class RegisterControl : UserControl
    {
        private readonly AuthService _authService;

        public event Action? OnNavigateToLogin;

        public RegisterControl()
        {
            _authService = new AuthService();
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (panelCenter != null)
            {
                int w = Math.Max(panelCenter.Width + 40, this.ClientSize.Width);
                int h = Math.Max(panelCenter.Height + 40, this.ClientSize.Height);
                panelCenter.Location = new Point(Math.Max(20, (w - panelCenter.Width) / 2), Math.Max(20, (h - panelCenter.Height) / 2));
            }
        }

        private void BtnBackToLogin_Click(object? sender, EventArgs e)
        {
            OnNavigateToLogin?.Invoke();
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullname = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (_authService.DangKy(username, password, confirmPassword, fullname, email, phone, out string msg))
            {
                MessageBox.Show(msg, "Đăng ký thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnNavigateToLogin?.Invoke();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
