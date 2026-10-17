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

        private Panel panelCenter = null!;

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
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);

            panelCenter = new Panel();
            panelCenter.Size = new Size(480, 480);
            panelCenter.BackColor = Color.FromArgb(27, 40, 56);
            int targetW = Math.Max(panelCenter.Width + 40, this.ClientSize.Width);
            int targetH = Math.Max(panelCenter.Height + 40, this.ClientSize.Height);
            panelCenter.Location = new Point(Math.Max(20, (targetW - panelCenter.Width) / 2), Math.Max(20, (targetH - panelCenter.Height) / 2));

            Label lblLogo = new Label();
            lblLogo.Text = "📝 ĐĂNG KÝ THÀNH VIÊN";
            lblLogo.Location = new Point(0, 15);
            lblLogo.Size = new Size(480, 35);
            lblLogo.ForeColor = Color.FromArgb(102, 192, 244);
            lblLogo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblLogo.UseMnemonic = false;

            Label lblUsername = new Label();
            lblUsername.Text = "Tên đăng nhập (*):";
            lblUsername.Location = new Point(35, 55);
            lblUsername.AutoSize = true;
            lblUsername.ForeColor = Color.White;
            lblUsername.Font = new Font("Segoe UI", 9.5f);
            lblUsername.UseMnemonic = false;

            txtUsername = new TextBox();
            txtUsername.Location = new Point(35, 78);
            txtUsername.Width = 410;
            txtUsername.Font = new Font("Segoe UI", 10f);
            txtUsername.BackColor = Color.FromArgb(18, 25, 35);
            txtUsername.ForeColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;

            Label lblFullName = new Label();
            lblFullName.Text = "Họ và tên (*):";
            lblFullName.Location = new Point(35, 109);
            lblFullName.AutoSize = true;
            lblFullName.ForeColor = Color.White;
            lblFullName.Font = new Font("Segoe UI", 9.5f);
            lblFullName.UseMnemonic = false;

            txtFullName = new TextBox();
            txtFullName.Location = new Point(35, 132);
            txtFullName.Width = 410;
            txtFullName.Font = new Font("Segoe UI", 10f);
            txtFullName.BackColor = Color.FromArgb(18, 25, 35);
            txtFullName.ForeColor = Color.White;
            txtFullName.BorderStyle = BorderStyle.FixedSingle;

            Label lblEmail = new Label();
            lblEmail.Text = "Địa chỉ Email (*):";
            lblEmail.Location = new Point(35, 163);
            lblEmail.AutoSize = true;
            lblEmail.ForeColor = Color.White;
            lblEmail.Font = new Font("Segoe UI", 9.5f);
            lblEmail.UseMnemonic = false;

            txtEmail = new TextBox();
            txtEmail.Location = new Point(35, 186);
            txtEmail.Width = 410;
            txtEmail.Font = new Font("Segoe UI", 10f);
            txtEmail.BackColor = Color.FromArgb(18, 25, 35);
            txtEmail.ForeColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;

            Label lblPhone = new Label();
            lblPhone.Text = "Số điện thoại:";
            lblPhone.Location = new Point(35, 217);
            lblPhone.AutoSize = true;
            lblPhone.ForeColor = Color.White;
            lblPhone.Font = new Font("Segoe UI", 9.5f);
            lblPhone.UseMnemonic = false;

            txtPhone = new TextBox();
            txtPhone.Location = new Point(35, 240);
            txtPhone.Width = 410;
            txtPhone.Font = new Font("Segoe UI", 10f);
            txtPhone.BackColor = Color.FromArgb(18, 25, 35);
            txtPhone.ForeColor = Color.White;
            txtPhone.BorderStyle = BorderStyle.FixedSingle;

            Label lblPassword = new Label();
            lblPassword.Text = "Mật khẩu (*):";
            lblPassword.Location = new Point(35, 271);
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = Color.White;
            lblPassword.Font = new Font("Segoe UI", 9.5f);
            lblPassword.UseMnemonic = false;

            txtPassword = new TextBox();
            txtPassword.Location = new Point(35, 294);
            txtPassword.Width = 410;
            txtPassword.Font = new Font("Segoe UI", 10f);
            txtPassword.BackColor = Color.FromArgb(18, 25, 35);
            txtPassword.ForeColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.UseSystemPasswordChar = true;

            Label lblConfirmPassword = new Label();
            lblConfirmPassword.Text = "Nhập lại mật khẩu (*):";
            lblConfirmPassword.Location = new Point(35, 325);
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.ForeColor = Color.White;
            lblConfirmPassword.Font = new Font("Segoe UI", 9.5f);
            lblConfirmPassword.UseMnemonic = false;

            txtConfirmPassword = new TextBox();
            txtConfirmPassword.Location = new Point(35, 348);
            txtConfirmPassword.Width = 410;
            txtConfirmPassword.Font = new Font("Segoe UI", 10f);
            txtConfirmPassword.BackColor = Color.FromArgb(18, 25, 35);
            txtConfirmPassword.ForeColor = Color.White;
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.UseSystemPasswordChar = true;

            btnRegister = new Button();
            btnRegister.Text = "TẠO TÀI KHOẢN MỚI";
            btnRegister.Location = new Point(35, 387);
            btnRegister.Size = new Size(410, 40);
            btnRegister.BackColor = Color.FromArgb(90, 160, 45);
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.UseMnemonic = false;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            btnBackToLogin = new Button();
            btnBackToLogin.Text = "← Đã có tài khoản? Quay lại Đăng nhập";
            btnBackToLogin.Location = new Point(35, 433);
            btnBackToLogin.Size = new Size(410, 34);
            btnBackToLogin.BackColor = Color.Transparent;
            btnBackToLogin.ForeColor = Color.FromArgb(102, 192, 244);
            btnBackToLogin.FlatStyle = FlatStyle.Flat;
            btnBackToLogin.Font = new Font("Segoe UI", 10f, FontStyle.Bold | FontStyle.Underline);
            btnBackToLogin.Cursor = Cursors.Hand;
            btnBackToLogin.UseMnemonic = false;
            btnBackToLogin.FlatAppearance.BorderSize = 0;
            btnBackToLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 54, 75);
            btnBackToLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(28, 42, 60);
            btnBackToLogin.Click += BtnBackToLogin_Click;

            panelCenter.Controls.Add(btnBackToLogin);
            panelCenter.Controls.Add(btnRegister);
            panelCenter.Controls.Add(txtConfirmPassword);
            panelCenter.Controls.Add(lblConfirmPassword);
            panelCenter.Controls.Add(txtPassword);
            panelCenter.Controls.Add(lblPassword);
            panelCenter.Controls.Add(txtPhone);
            panelCenter.Controls.Add(lblPhone);
            panelCenter.Controls.Add(txtEmail);
            panelCenter.Controls.Add(lblEmail);
            panelCenter.Controls.Add(txtFullName);
            panelCenter.Controls.Add(lblFullName);
            panelCenter.Controls.Add(txtUsername);
            panelCenter.Controls.Add(lblUsername);
            panelCenter.Controls.Add(lblLogo);

            this.Controls.Add(panelCenter);

            this.ResumeLayout(false);
            this.PerformLayout();
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
