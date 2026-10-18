namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    partial class LoginControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelCenter = new System.Windows.Forms.Panel();
            this.panelDemo = new System.Windows.Forms.Panel();
            this.lblDemoDesc = new System.Windows.Forms.Label();
            this.btnDemoCustomer = new System.Windows.Forms.Button();
            this.btnDemoAdmin = new System.Windows.Forms.Button();
            this.lblDemo = new System.Windows.Forms.Label();
            this.btnGoToRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.panelCenter.SuspendLayout();
            this.panelDemo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.panelCenter.Controls.Add(this.panelDemo);
            this.panelCenter.Controls.Add(this.btnGoToRegister);
            this.panelCenter.Controls.Add(this.btnLogin);
            this.panelCenter.Controls.Add(this.txtPassword);
            this.panelCenter.Controls.Add(this.lblPass);
            this.panelCenter.Controls.Add(this.txtUsername);
            this.panelCenter.Controls.Add(this.lblUser);
            this.panelCenter.Controls.Add(this.lblSub);
            this.panelCenter.Controls.Add(this.lblLogo);
            this.panelCenter.Location = new System.Drawing.Point(295, 85);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Padding = new System.Windows.Forms.Padding(35);
            this.panelCenter.Size = new System.Drawing.Size(450, 530);
            this.panelCenter.TabIndex = 0;
            // 
            // panelDemo
            // 
            this.panelDemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.panelDemo.Controls.Add(this.lblDemoDesc);
            this.panelDemo.Controls.Add(this.btnDemoCustomer);
            this.panelDemo.Controls.Add(this.btnDemoAdmin);
            this.panelDemo.Controls.Add(this.lblDemo);
            this.panelDemo.Location = new System.Drawing.Point(35, 360);
            this.panelDemo.Name = "panelDemo";
            this.panelDemo.Padding = new System.Windows.Forms.Padding(10);
            this.panelDemo.Size = new System.Drawing.Size(380, 140);
            this.panelDemo.TabIndex = 8;
            // 
            // lblDemoDesc
            // 
            this.lblDemoDesc.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Italic);
            this.lblDemoDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(181)))), ((int)(((byte)(196)))));
            this.lblDemoDesc.Location = new System.Drawing.Point(10, 76);
            this.lblDemoDesc.Name = "lblDemoDesc";
            this.lblDemoDesc.Size = new System.Drawing.Size(360, 50);
            this.lblDemoDesc.TabIndex = 3;
            this.lblDemoDesc.Text = "• Admin: Toàn quyền Dashboard, Kho game, Quản trị user\r\n• Khách hàng: Mua sắm, n" +
    "ạp ví, thư viện game (Ẩn admin)";
            this.lblDemoDesc.UseMnemonic = false;
            // 
            // btnDemoCustomer
            // 
            this.btnDemoCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(120)))), ((int)(((byte)(70)))));
            this.btnDemoCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDemoCustomer.FlatAppearance.BorderSize = 0;
            this.btnDemoCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemoCustomer.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDemoCustomer.ForeColor = System.Drawing.Color.White;
            this.btnDemoCustomer.Location = new System.Drawing.Point(195, 32);
            this.btnDemoCustomer.Name = "btnDemoCustomer";
            this.btnDemoCustomer.Size = new System.Drawing.Size(175, 36);
            this.btnDemoCustomer.TabIndex = 2;
            this.btnDemoCustomer.Text = "Demo Khách Hàng";
            this.btnDemoCustomer.UseMnemonic = false;
            this.btnDemoCustomer.UseVisualStyleBackColor = false;
            this.btnDemoCustomer.Click += new System.EventHandler(this.BtnDemoCustomer_Click);
            // 
            // btnDemoAdmin
            // 
            this.btnDemoAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.btnDemoAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDemoAdmin.FlatAppearance.BorderSize = 0;
            this.btnDemoAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemoAdmin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDemoAdmin.ForeColor = System.Drawing.Color.White;
            this.btnDemoAdmin.Location = new System.Drawing.Point(10, 32);
            this.btnDemoAdmin.Name = "btnDemoAdmin";
            this.btnDemoAdmin.Size = new System.Drawing.Size(175, 36);
            this.btnDemoAdmin.TabIndex = 1;
            this.btnDemoAdmin.Text = "Demo Admin";
            this.btnDemoAdmin.UseMnemonic = false;
            this.btnDemoAdmin.UseVisualStyleBackColor = false;
            this.btnDemoAdmin.Click += new System.EventHandler(this.BtnDemoAdmin_Click);
            // 
            // lblDemo
            // 
            this.lblDemo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(208)))), ((int)(((byte)(7)))));
            this.lblDemo.Location = new System.Drawing.Point(10, 8);
            this.lblDemo.Name = "lblDemo";
            this.lblDemo.Size = new System.Drawing.Size(360, 20);
            this.lblDemo.TabIndex = 0;
            this.lblDemo.Text = "Phím tắt kiểm thử phân quyền (Pass: 123456):";
            this.lblDemo.UseMnemonic = false;
            // 
            // btnGoToRegister
            // 
            this.btnGoToRegister.BackColor = System.Drawing.Color.Transparent;
            this.btnGoToRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoToRegister.FlatAppearance.BorderSize = 0;
            this.btnGoToRegister.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.btnGoToRegister.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(54)))), ((int)(((byte)(75)))));
            this.btnGoToRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToRegister.Font = new System.Drawing.Font("Segoe UI", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.btnGoToRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.btnGoToRegister.Location = new System.Drawing.Point(35, 315);
            this.btnGoToRegister.Name = "btnGoToRegister";
            this.btnGoToRegister.Size = new System.Drawing.Size(380, 36);
            this.btnGoToRegister.TabIndex = 7;
            this.btnGoToRegister.Text = "Chưa có tài khoản? Đăng ký tại đây";
            this.btnGoToRegister.UseMnemonic = false;
            this.btnGoToRegister.UseVisualStyleBackColor = false;
            this.btnGoToRegister.Click += new System.EventHandler(this.BtnGoToRegister_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(98)))), ((int)(((byte)(130)))));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(35, 260);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(380, 45);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "ĐĂNG NHẬP NGAY";
            this.btnLogin.UseMnemonic = false;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(35, 200);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(380, 27);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.Text = "123456";
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPassword_KeyDown);
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPass.ForeColor = System.Drawing.Color.White;
            this.lblPass.Location = new System.Drawing.Point(35, 175);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(76, 19);
            this.lblPass.TabIndex = 4;
            this.lblPass.Text = "Mật khẩu:";
            this.lblPass.UseMnemonic = false;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsername.ForeColor = System.Drawing.Color.White;
            this.txtUsername.Location = new System.Drawing.Point(35, 125);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(380, 27);
            this.txtUsername.TabIndex = 3;
            this.txtUsername.Text = "admin";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(35, 100);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(111, 19);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Tên đăng nhập:";
            this.lblUser.UseMnemonic = false;
            // 
            // lblSub
            // 
            this.lblSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(181)))), ((int)(((byte)(196)))));
            this.lblSub.Location = new System.Drawing.Point(35, 80);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(380, 25);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "Cửa hàng phân phối sản phẩm số bản quyền";
            this.lblSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSub.UseMnemonic = false;
            // 
            // lblLogo
            // 
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.lblLogo.Location = new System.Drawing.Point(35, 35);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(380, 45);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "DPMS LOGIN";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogo.UseMnemonic = false;
            // 
            // LoginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.panelCenter);
            this.Name = "LoginControl";
            this.Size = new System.Drawing.Size(1040, 700);
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            this.panelDemo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnGoToRegister;
        private System.Windows.Forms.Panel panelDemo;
        private System.Windows.Forms.Label lblDemo;
        private System.Windows.Forms.Button btnDemoAdmin;
        private System.Windows.Forms.Button btnDemoCustomer;
        private System.Windows.Forms.Label lblDemoDesc;
    }
}
