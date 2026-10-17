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

        private Panel panelCenter = null!;
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
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);

            // Khung form đăng nhập ở giữa màn hình
            panelCenter = new Panel();
            panelCenter.Size = new Size(450, 530);
            panelCenter.BackColor = Color.FromArgb(27, 40, 56);
            panelCenter.Padding = new Padding(35);
            int targetW = Math.Max(panelCenter.Width + 40, this.ClientSize.Width);
            int targetH = Math.Max(panelCenter.Height + 40, this.ClientSize.Height);
            panelCenter.Location = new Point(Math.Max(20, (targetW - panelCenter.Width) / 2), Math.Max(20, (targetH - panelCenter.Height) / 2));

            Label lblLogo = new Label();
            lblLogo.Text = "⚡ DPMS LOGIN";
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Height = 45;
            lblLogo.ForeColor = Color.FromArgb(102, 192, 244);
            lblLogo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblLogo.UseMnemonic = false;

            Label lblSub = new Label();
            lblSub.Text = "Cửa hàng phân phối sản phẩm số bản quyền";
            lblSub.Dock = DockStyle.Top;
            lblSub.Height = 25;
            lblSub.ForeColor = Color.FromArgb(161, 181, 196);
            lblSub.Font = new Font("Segoe UI", 9.5f);
            lblSub.TextAlign = ContentAlignment.MiddleCenter;
            lblSub.UseMnemonic = false;

            // Username
            Label lblUser = new Label();
            lblUser.Text = "Tên đăng nhập:";
            lblUser.Location = new Point(35, 100);
            lblUser.AutoSize = true;
            lblUser.ForeColor = Color.White;
            lblUser.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblUser.UseMnemonic = false;

            txtUsername = new TextBox();
            txtUsername.Location = new Point(35, 125);
            txtUsername.Width = 380;
            txtUsername.Font = new Font("Segoe UI", 11);
            txtUsername.BackColor = Color.FromArgb(18, 25, 35);
            txtUsername.ForeColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Text = "admin"; // Gợi ý mặc định

            // Password
            Label lblPass = new Label();
            lblPass.Text = "Mật khẩu:";
            lblPass.Location = new Point(35, 175);
            lblPass.AutoSize = true;
            lblPass.ForeColor = Color.White;
            lblPass.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPass.UseMnemonic = false;

            txtPassword = new TextBox();
            txtPassword.Location = new Point(35, 200);
            txtPassword.Width = 380;
            txtPassword.Font = new Font("Segoe UI", 11);
            txtPassword.BackColor = Color.FromArgb(18, 25, 35);
            txtPassword.ForeColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.Text = "123456"; // Gợi ý mặc định
            txtPassword.KeyDown += TxtPassword_KeyDown;

            // Nút Đăng nhập
            btnLogin = new Button();
            btnLogin.Text = "ĐĂNG NHẬP NGAY";
            btnLogin.Location = new Point(35, 260);
            btnLogin.Size = new Size(380, 45);
            btnLogin.BackColor = Color.FromArgb(49, 98, 130);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.UseMnemonic = false;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            // Nút Đăng ký
            btnGoToRegister = new Button();
            btnGoToRegister.Text = "Chưa có tài khoản? Đăng ký tại đây";
            btnGoToRegister.Location = new Point(35, 315);
            btnGoToRegister.Size = new Size(380, 36);
            btnGoToRegister.BackColor = Color.Transparent;
            btnGoToRegister.ForeColor = Color.FromArgb(102, 192, 244);
            btnGoToRegister.FlatStyle = FlatStyle.Flat;
            btnGoToRegister.Font = new Font("Segoe UI", 10f, FontStyle.Bold | FontStyle.Underline);
            btnGoToRegister.Cursor = Cursors.Hand;
            btnGoToRegister.UseMnemonic = false;
            btnGoToRegister.FlatAppearance.BorderSize = 0;
            btnGoToRegister.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 54, 75);
            btnGoToRegister.FlatAppearance.MouseDownBackColor = Color.FromArgb(28, 42, 60);
            btnGoToRegister.Click += BtnGoToRegister_Click;

            // Khung gợi ý tài khoản test cho Giảng viên / Nhóm
            Panel panelDemo = new Panel();
            panelDemo.Location = new Point(35, 360);
            panelDemo.Size = new Size(380, 140);
            panelDemo.BackColor = Color.FromArgb(18, 25, 35);
            panelDemo.Padding = new Padding(10);

            Label lblDemo = new Label();
            lblDemo.Text = "💡 Phím tắt kiểm thử phân quyền (Pass: 123456):";
            lblDemo.Location = new Point(10, 8);
            lblDemo.Size = new Size(360, 20);
            lblDemo.ForeColor = Color.FromArgb(164, 208, 7);
            lblDemo.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblDemo.UseMnemonic = false;

            Button btnDemoAdmin = new Button();
            btnDemoAdmin.Text = "⚡ Demo Admin";
            btnDemoAdmin.Location = new Point(10, 32);
            btnDemoAdmin.Size = new Size(175, 36);
            btnDemoAdmin.BackColor = Color.FromArgb(217, 119, 6);
            btnDemoAdmin.ForeColor = Color.White;
            btnDemoAdmin.FlatStyle = FlatStyle.Flat;
            btnDemoAdmin.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnDemoAdmin.Cursor = Cursors.Hand;
            btnDemoAdmin.UseMnemonic = false;
            btnDemoAdmin.FlatAppearance.BorderSize = 0;
            btnDemoAdmin.Click += BtnDemoAdmin_Click;

            Button btnDemoCustomer = new Button();
            btnDemoCustomer.Text = "⚡ Demo Khách Hàng";
            btnDemoCustomer.Location = new Point(195, 32);
            btnDemoCustomer.Size = new Size(175, 36);
            btnDemoCustomer.BackColor = Color.FromArgb(16, 120, 70);
            btnDemoCustomer.ForeColor = Color.White;
            btnDemoCustomer.FlatStyle = FlatStyle.Flat;
            btnDemoCustomer.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnDemoCustomer.Cursor = Cursors.Hand;
            btnDemoCustomer.UseMnemonic = false;
            btnDemoCustomer.FlatAppearance.BorderSize = 0;
            btnDemoCustomer.Click += BtnDemoCustomer_Click;

            Label lblDemoDesc = new Label();
            lblDemoDesc.Text = "• Admin: Toàn quyền Dashboard, Kho game, Quản trị user\n• Khách hàng: Mua sắm, nạp ví, thư viện game (Ẩn admin)";
            lblDemoDesc.Location = new Point(10, 76);
            lblDemoDesc.Size = new Size(360, 50);
            lblDemoDesc.ForeColor = Color.FromArgb(161, 181, 196);
            lblDemoDesc.Font = new Font("Segoe UI", 8.2f, FontStyle.Italic);
            lblDemoDesc.UseMnemonic = false;

            panelDemo.Controls.Add(lblDemoDesc);
            panelDemo.Controls.Add(btnDemoCustomer);
            panelDemo.Controls.Add(btnDemoAdmin);
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

            this.ResumeLayout(false);
            this.PerformLayout();
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
