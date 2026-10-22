using System;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 1: Màn hình Đăng ký tài khoản khách hàng mới
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public partial class RegisterControl : UserControl
    {
        private readonly AuthService _authService;

        public RegisterControl()
        {
            _authService = new AuthService();
            InitializeComponent();
        }


        private void BtnBackToLogin_Click(object? sender, EventArgs e)
        {
            MainForm?.NavigateToLogin();
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
                MainForm?.NavigateToLogin();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
