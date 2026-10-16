using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 1: Màn hình Đăng ký tài khoản khách hàng mới
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class RegisterControl : UserControl
    {
        private readonly AuthService _authService;

        public event Action? OnNavigateToLogin;

        private TextBox txtUsername = null!;
        private TextBox txtFullName = null!;
        private TextBox txtEmail = null!;
        private TextBox txtPhone = null!;
        private TextBox txtPassword = null!;
        private TextBox txtConfirmPassword = null!;
        private Button btnRegister = null!;
        private Button btnBackToLogin = null!;

        public RegisterControl()
        {
            _authService = new AuthService();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;

            Panel panelCenter = new Panel
            {
                Size = new Size(480, 560),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(35)
            };
            panelCenter.Location = new Point((this.Width - panelCenter.Width) / 2, 40);
            this.Resize += (s, e) =>
            {
                panelCenter.Location = new Point(Math.Max(20, (this.Width - panelCenter.Width) / 2), Math.Max(20, (this.Height - panelCenter.Height) / 2));
            };

            Label lblLogo = new Label
            {
                Text = "📝 ĐĂNG KÝ THÀNH VIÊN",
                Dock = DockStyle.Top,
                Height = 40,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            int y = 50;
            AddInput(panelCenter, "Tên đăng nhập (*):", out txtUsername, ref y);
            AddInput(panelCenter, "Họ và tên (*):", out txtFullName, ref y);
            AddInput(panelCenter, "Địa chỉ Email (*):", out txtEmail, ref y);
            AddInput(panelCenter, "Số điện thoại:", out txtPhone, ref y);
            AddInput(panelCenter, "Mật khẩu (*):", out txtPassword, ref y, true);
            AddInput(panelCenter, "Nhập lại mật khẩu (*):", out txtConfirmPassword, ref y, true);

            btnRegister = new Button
            {
                Text = "TẠO TÀI KHOẢN MỚI",
                Location = new Point(35, y + 10),
                Size = new Size(410, 42),
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            btnBackToLogin = new Button
            {
                Text = "← Đã có tài khoản? Quay lại Đăng nhập",
                Location = new Point(35, y + 60),
                Size = new Size(410, 32),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(102, 192, 244),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Underline),
                Cursor = Cursors.Hand
            };
            btnBackToLogin.FlatAppearance.BorderSize = 0;
            btnBackToLogin.Click += (s, e) => OnNavigateToLogin?.Invoke();

            panelCenter.Controls.Add(btnBackToLogin);
            panelCenter.Controls.Add(btnRegister);
            panelCenter.Controls.Add(lblLogo);

            this.Controls.Add(panelCenter);
        }

        private void AddInput(Panel panel, string label, out TextBox txt, ref int y, bool isPassword = false)
        {
            Label lbl = new Label { Text = label, Location = new Point(35, y), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txt = new TextBox
            {
                Location = new Point(35, y + 22),
                Width = 410,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                UseSystemPasswordChar = isPassword
            };
            panel.Controls.Add(lbl);
            panel.Controls.Add(txt);
            y += 56;
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
