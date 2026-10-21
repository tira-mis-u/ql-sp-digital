using System;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 1: Màn hình Đăng nhập tài khoản hệ thống
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public partial class LoginControl : UserControl
    {
        private readonly AuthService _authService;

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
                MainForm?.UpdateUserInterfaceSession();
                MainForm?.UpdateCartBadge();
                MainForm?.NavigateToStore();
            }
            else
            {
                MessageBox.Show(msg, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            MainForm?.NavigateToRegister();
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
