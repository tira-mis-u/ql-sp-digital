using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 1: Màn hình Đăng nhập tài khoản hệ thống
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class LoginControl : UserControl
    {
        private readonly AuthService _authService;

        public event Action? OnLoginSuccess;
        public event Action? OnNavigateToRegister;

        private TextBox txtUsername = null!;
        private TextBox txtPassword = null!;
        private Button btnLogin = null!;
        private Button btnGoToRegister = null!;

        public LoginControl()
        {
            _authService = new AuthService();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;

            // Khung form đăng nhập ở giữa màn hình
            Panel panelCenter = new Panel
            {
                Size = new Size(450, 480),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(35)
            };
            panelCenter.Location = new Point((this.Width - panelCenter.Width) / 2, 80);
            this.Resize += (s, e) =>
            {
                panelCenter.Location = new Point(Math.Max(20, (this.Width - panelCenter.Width) / 2), Math.Max(20, (this.Height - panelCenter.Height) / 2));
            };

            Label lblLogo = new Label
            {
                Text = "⚡ DPMS LOGIN",
                Dock = DockStyle.Top,
                Height = 45,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblSub = new Label
            {
                Text = "Cửa hàng phân phối sản phẩm số bản quyền",
                Dock = DockStyle.Top,
                Height = 25,
                ForeColor = Color.FromArgb(161, 181, 196),
                Font = new Font("Segoe UI", 9.5f),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Username
            Label lblUser = new Label { Text = "Tên đăng nhập:", Location = new Point(35, 100), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtUsername = new TextBox
            {
                Location = new Point(35, 125),
                Width = 380,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                Text = "admin" // Gợi ý mặc định
            };

            // Password
            Label lblPass = new Label { Text = "Mật khẩu:", Location = new Point(35, 175), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtPassword = new TextBox
            {
                Location = new Point(35, 200),
                Width = 380,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                UseSystemPasswordChar = true,
                Text = "123456" // Gợi ý mặc định
            };
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) PerformLogin(); };

            // Nút Đăng nhập
            btnLogin = new Button
            {
                Text = "ĐĂNG NHẬP NGAY",
                Location = new Point(35, 260),
                Size = new Size(380, 45),
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += (s, e) => PerformLogin();

            // Nút Đăng ký
            btnGoToRegister = new Button
            {
                Text = "Chưa có tài khoản? Đăng ký tại đây",
                Location = new Point(35, 315),
                Size = new Size(380, 35),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(102, 192, 244),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Underline),
                Cursor = Cursors.Hand
            };
            btnGoToRegister.FlatAppearance.BorderSize = 0;
            btnGoToRegister.Click += (s, e) => OnNavigateToRegister?.Invoke();

            // Khung gợi ý tài khoản test cho Giảng viên / Nhóm
            Panel panelDemo = new Panel
            {
                Location = new Point(35, 365),
                Size = new Size(380, 80),
                BackColor = Color.FromArgb(18, 25, 35),
                Padding = new Padding(10)
            };

            Label lblDemo = new Label
            {
                Text = "💡 Tài khoản kiểm thử nhanh (Mật khẩu: 123456):\n• Admin: admin\n• Khách hàng: hieu_pham hoặc tri_le",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(164, 208, 7),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic)
            };
            panelDemo.Controls.Add(lblDemo);

            panelCenter.Controls.Add(panelDemo);
            panelCenter.Controls.Add(btnGoToRegister);
            panelCenter.Controls.Add(btnLogin);
            panelCenter.Controls.Add(txtPassword);
            panelCenter.Controls.Add(lblPass);
            panelCenter.Controls.Add(txtUsername);
            panelCenter.Controls.Add(lblUser);
            panelCenter.Controls.Add(lblSub);
            panelCenter.Controls.Add(lblLogo);

            this.Controls.Add(panelCenter);
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
    }
}
