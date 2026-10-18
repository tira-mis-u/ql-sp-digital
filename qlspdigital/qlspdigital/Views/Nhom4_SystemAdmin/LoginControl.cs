using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 1: Màn hình Đăng nhập tài khoản hệ thống
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public partial class LoginControl : UserControl
    {
        private readonly AuthService _authService;

        public event Action? OnLoginSuccess;
        public event Action? OnNavigateToRegister;

        public LoginControl()
        {
            _authService = new AuthService();
            InitializeComponent();
        }

        private void PerformLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (_authService.DangNhap(username, password, out string msg))
            {
                MessageBox.Show(msg, "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnLoginSuccess?.Invoke();
            }
            else
            {
                MessageBox.Show(msg, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void TxtPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) PerformLogin();
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            PerformLogin();
        }

        private void BtnGoToRegister_Click(object? sender, EventArgs e)
        {
            OnNavigateToRegister?.Invoke();
        }

        private void BtnDemoAdmin_Click(object? sender, EventArgs e)
        {
            txtUsername.Text = "admin";
            txtPassword.Text = "123456";
            PerformLogin();
        }

        private void BtnDemoCustomer_Click(object? sender, EventArgs e)
        {
            txtUsername.Text = "hieu_pham";
            txtPassword.Text = "123456";
            PerformLogin();
        }
    }
}
