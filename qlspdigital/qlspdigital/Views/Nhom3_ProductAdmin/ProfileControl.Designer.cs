using System.Drawing;
using System.Windows.Forms;

namespace qlspdigital.Views.Nhom3_ProductAdmin
{
    partial class ProfileControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblCreatedDate = new System.Windows.Forms.Label();
            this.panelProfileForm = new System.Windows.Forms.Panel();
            this.lblForm1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnUpdateProfile = new System.Windows.Forms.Button();
            this.panelPasswordForm = new System.Windows.Forms.Panel();
            this.lblForm2 = new System.Windows.Forms.Label();
            this.lblOldPass = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblNewPass = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPass = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.chkEmailNotify = new System.Windows.Forms.CheckBox();
            this.chkRememberSession = new System.Windows.Forms.CheckBox();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.chkOrderConfirm = new System.Windows.Forms.CheckBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.panelSecurityBanner = new System.Windows.Forms.Panel();
            this.lblSecurityTitle = new System.Windows.Forms.Label();
            this.lblSecurityContent = new System.Windows.Forms.Label();
            this.panelInfo.SuspendLayout();
            this.panelProfileForm.SuspendLayout();
            this.panelPasswordForm.SuspendLayout();
            this.panelSettings.SuspendLayout();
            this.panelSecurityBanner.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(25, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(437, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "HỒ SƠ TÀI KHOẢN & CÀI ĐẶT BẢO MẬT";
            this.lblHeader.UseMnemonic = false;

            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelInfo.Controls.Add(this.lblCreatedDate);
            this.panelInfo.Controls.Add(this.lblBalance);
            this.panelInfo.Controls.Add(this.lblRole);
            this.panelInfo.Controls.Add(this.lblUsername);
            this.panelInfo.Location = new System.Drawing.Point(25, 55);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Padding = new System.Windows.Forms.Padding(20);
            this.panelInfo.Size = new System.Drawing.Size(980, 90);
            this.panelInfo.TabIndex = 1;

            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblUsername.Location = new System.Drawing.Point(20, 15);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(124, 20);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Tài khoản: ---";
            this.lblUsername.UseMnemonic = false;

            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.lblRole.Location = new System.Drawing.Point(320, 15);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(107, 19);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "Quyền hạn: ---";
            this.lblRole.UseMnemonic = false;

            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBalance.ForeColor = System.Drawing.Color.FromArgb(164, 208, 7);
            this.lblBalance.Location = new System.Drawing.Point(20, 50);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(109, 21);
            this.lblBalance.TabIndex = 2;
            this.lblBalance.Text = "Số dư ví: 0 ₫";
            this.lblBalance.UseMnemonic = false;

            // 
            // lblCreatedDate
            // 
            this.lblCreatedDate.AutoSize = true;
            this.lblCreatedDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCreatedDate.ForeColor = System.Drawing.Color.FromArgb(161, 181, 196);
            this.lblCreatedDate.Location = new System.Drawing.Point(320, 50);
            this.lblCreatedDate.Name = "lblCreatedDate";
            this.lblCreatedDate.Size = new System.Drawing.Size(130, 17);
            this.lblCreatedDate.TabIndex = 3;
            this.lblCreatedDate.Text = "Ngày tham gia: ---";
            this.lblCreatedDate.UseMnemonic = false;

            // 
            // panelProfileForm
            // 
            this.panelProfileForm.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelProfileForm.Controls.Add(this.btnUpdateProfile);
            this.panelProfileForm.Controls.Add(this.txtPhone);
            this.panelProfileForm.Controls.Add(this.lblPhone);
            this.panelProfileForm.Controls.Add(this.txtEmail);
            this.panelProfileForm.Controls.Add(this.lblEmail);
            this.panelProfileForm.Controls.Add(this.txtFullName);
            this.panelProfileForm.Controls.Add(this.lblName);
            this.panelProfileForm.Controls.Add(this.lblForm1);
            this.panelProfileForm.Location = new System.Drawing.Point(25, 160);
            this.panelProfileForm.Name = "panelProfileForm";
            this.panelProfileForm.Padding = new System.Windows.Forms.Padding(20);
            this.panelProfileForm.Size = new System.Drawing.Size(480, 275);
            this.panelProfileForm.TabIndex = 2;

            // 
            // lblForm1
            // 
            this.lblForm1.AutoSize = true;
            this.lblForm1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblForm1.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblForm1.Location = new System.Drawing.Point(20, 15);
            this.lblForm1.Name = "lblForm1";
            this.lblForm1.Size = new System.Drawing.Size(256, 20);
            this.lblForm1.TabIndex = 0;
            this.lblForm1.Text = "THAY ĐỔI THÔNG TIN CÁ NHÂN";
            this.lblForm1.UseMnemonic = false;

            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(20, 58);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(89, 17);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Họ và tên (*):";
            this.lblName.UseMnemonic = false;

            // 
            // txtFullName
            // 
            this.txtFullName.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFullName.ForeColor = System.Drawing.Color.White;
            this.txtFullName.Location = new System.Drawing.Point(165, 54);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(285, 25);
            this.txtFullName.TabIndex = 2;

            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblEmail.ForeColor = System.Drawing.Color.White;
            this.lblEmail.Location = new System.Drawing.Point(20, 106);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(107, 17);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Địa chỉ Email (*):";
            this.lblEmail.UseMnemonic = false;

            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(165, 102);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(285, 25);
            this.txtEmail.TabIndex = 4;

            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPhone.ForeColor = System.Drawing.Color.White;
            this.lblPhone.Location = new System.Drawing.Point(20, 154);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(88, 17);
            this.lblPhone.TabIndex = 5;
            this.lblPhone.Text = "Số điện thoại:";
            this.lblPhone.UseMnemonic = false;

            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.ForeColor = System.Drawing.Color.White;
            this.txtPhone.Location = new System.Drawing.Point(165, 150);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(285, 25);
            this.txtPhone.TabIndex = 6;

            // 
            // btnUpdateProfile
            // 
            this.btnUpdateProfile.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnUpdateProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateProfile.FlatAppearance.BorderSize = 0;
            this.btnUpdateProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateProfile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdateProfile.ForeColor = System.Drawing.Color.White;
            this.btnUpdateProfile.Location = new System.Drawing.Point(165, 206);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new System.Drawing.Size(220, 42);
            this.btnUpdateProfile.TabIndex = 7;
            this.btnUpdateProfile.Text = "Lưu thay đổi hồ sơ";
            this.btnUpdateProfile.UseMnemonic = false;
            this.btnUpdateProfile.UseVisualStyleBackColor = false;
            this.btnUpdateProfile.Click += new System.EventHandler(this.BtnUpdateProfile_Click);

            // 
            // panelPasswordForm
            // 
            this.panelPasswordForm.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelPasswordForm.Controls.Add(this.btnChangePassword);
            this.panelPasswordForm.Controls.Add(this.txtConfirmPassword);
            this.panelPasswordForm.Controls.Add(this.lblConfirmPass);
            this.panelPasswordForm.Controls.Add(this.txtNewPassword);
            this.panelPasswordForm.Controls.Add(this.lblNewPass);
            this.panelPasswordForm.Controls.Add(this.txtOldPassword);
            this.panelPasswordForm.Controls.Add(this.lblOldPass);
            this.panelPasswordForm.Controls.Add(this.lblForm2);
            this.panelPasswordForm.Location = new System.Drawing.Point(525, 160);
            this.panelPasswordForm.Name = "panelPasswordForm";
            this.panelPasswordForm.Padding = new System.Windows.Forms.Padding(20);
            this.panelPasswordForm.Size = new System.Drawing.Size(480, 275);
            this.panelPasswordForm.TabIndex = 3;

            // 
            // lblForm2
            // 
            this.lblForm2.AutoSize = true;
            this.lblForm2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblForm2.ForeColor = System.Drawing.Color.FromArgb(164, 208, 7);
            this.lblForm2.Location = new System.Drawing.Point(20, 15);
            this.lblForm2.Name = "lblForm2";
            this.lblForm2.Size = new System.Drawing.Size(199, 20);
            this.lblForm2.TabIndex = 0;
            this.lblForm2.Text = "ĐỔI MẬT KHẨU BẢO MẬT";
            this.lblForm2.UseMnemonic = false;

            // 
            // lblOldPass
            // 
            this.lblOldPass.AutoSize = true;
            this.lblOldPass.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblOldPass.ForeColor = System.Drawing.Color.White;
            this.lblOldPass.Location = new System.Drawing.Point(20, 58);
            this.lblOldPass.Name = "lblOldPass";
            this.lblOldPass.Size = new System.Drawing.Size(83, 17);
            this.lblOldPass.TabIndex = 1;
            this.lblOldPass.Text = "Mật khẩu cũ:";
            this.lblOldPass.UseMnemonic = false;

            // 
            // txtOldPassword
            // 
            this.txtOldPassword.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtOldPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtOldPassword.ForeColor = System.Drawing.Color.White;
            this.txtOldPassword.Location = new System.Drawing.Point(165, 54);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.Size = new System.Drawing.Size(285, 25);
            this.txtOldPassword.TabIndex = 2;
            this.txtOldPassword.UseSystemPasswordChar = true;

            // 
            // lblNewPass
            // 
            this.lblNewPass.AutoSize = true;
            this.lblNewPass.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNewPass.ForeColor = System.Drawing.Color.White;
            this.lblNewPass.Location = new System.Drawing.Point(20, 106);
            this.lblNewPass.Name = "lblNewPass";
            this.lblNewPass.Size = new System.Drawing.Size(91, 17);
            this.lblNewPass.TabIndex = 3;
            this.lblNewPass.Text = "Mật khẩu mới:";
            this.lblNewPass.UseMnemonic = false;

            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewPassword.ForeColor = System.Drawing.Color.White;
            this.txtNewPassword.Location = new System.Drawing.Point(165, 102);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(285, 25);
            this.txtNewPassword.TabIndex = 4;
            this.txtNewPassword.UseSystemPasswordChar = true;

            // 
            // lblConfirmPass
            // 
            this.lblConfirmPass.AutoSize = true;
            this.lblConfirmPass.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblConfirmPass.ForeColor = System.Drawing.Color.White;
            this.lblConfirmPass.Location = new System.Drawing.Point(20, 154);
            this.lblConfirmPass.Name = "lblConfirmPass";
            this.lblConfirmPass.Size = new System.Drawing.Size(110, 17);
            this.lblConfirmPass.TabIndex = 5;
            this.lblConfirmPass.Text = "Nhập lại MK mới:";
            this.lblConfirmPass.UseMnemonic = false;

            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPassword.ForeColor = System.Drawing.Color.White;
            this.txtConfirmPassword.Location = new System.Drawing.Point(165, 150);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(285, 25);
            this.txtConfirmPassword.TabIndex = 6;
            this.txtConfirmPassword.UseSystemPasswordChar = true;

            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(165, 206);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(220, 42);
            this.btnChangePassword.TabIndex = 7;
            this.btnChangePassword.Text = "Lưu mật khẩu mới";
            this.btnChangePassword.UseMnemonic = false;
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.BtnChangePassword_Click);

            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelSettings.Controls.Add(this.btnSaveSettings);
            this.panelSettings.Controls.Add(this.chkOrderConfirm);
            this.panelSettings.Controls.Add(this.chkAutoUpdate);
            this.panelSettings.Controls.Add(this.chkRememberSession);
            this.panelSettings.Controls.Add(this.chkEmailNotify);
            this.panelSettings.Controls.Add(this.lblSettingsTitle);
            this.panelSettings.Location = new System.Drawing.Point(25, 450);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Padding = new System.Windows.Forms.Padding(20);
            this.panelSettings.Size = new System.Drawing.Size(980, 155);
            this.panelSettings.TabIndex = 4;

            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.AutoSize = true;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblSettingsTitle.Location = new System.Drawing.Point(20, 14);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(349, 20);
            this.lblSettingsTitle.TabIndex = 0;
            this.lblSettingsTitle.Text = "CÀI ĐẶT ỨNG DỤNG & TRẢI NGHIỆM TÀI KHOẢN";
            this.lblSettingsTitle.UseMnemonic = false;

            // 
            // chkEmailNotify
            // 
            this.chkEmailNotify.AutoSize = true;
            this.chkEmailNotify.Checked = true;
            this.chkEmailNotify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEmailNotify.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkEmailNotify.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.chkEmailNotify.Location = new System.Drawing.Point(25, 48);
            this.chkEmailNotify.Name = "chkEmailNotify";
            this.chkEmailNotify.Size = new System.Drawing.Size(425, 21);
            this.chkEmailNotify.TabIndex = 1;
            this.chkEmailNotify.Text = "Nhận thông báo đơn hàng và mã kích hoạt bản quyền qua Email";
            this.chkEmailNotify.UseVisualStyleBackColor = true;

            // 
            // chkRememberSession
            // 
            this.chkRememberSession.AutoSize = true;
            this.chkRememberSession.Checked = true;
            this.chkRememberSession.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRememberSession.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkRememberSession.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.chkRememberSession.Location = new System.Drawing.Point(25, 80);
            this.chkRememberSession.Name = "chkRememberSession";
            this.chkRememberSession.Size = new System.Drawing.Size(332, 21);
            this.chkRememberSession.TabIndex = 2;
            this.chkRememberSession.Text = "Ghi nhớ phiên đăng nhập an toàn trên thiết bị này";
            this.chkRememberSession.UseVisualStyleBackColor = true;

            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Checked = true;
            this.chkAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoUpdate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkAutoUpdate.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.chkAutoUpdate.Location = new System.Drawing.Point(25, 112);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(401, 21);
            this.chkAutoUpdate.TabIndex = 3;
            this.chkAutoUpdate.Text = "Tự động kiểm tra bản cập nhật và mã kích hoạt khi khởi chạy";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;

            // 
            // chkOrderConfirm
            // 
            this.chkOrderConfirm.AutoSize = true;
            this.chkOrderConfirm.Checked = true;
            this.chkOrderConfirm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOrderConfirm.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkOrderConfirm.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.chkOrderConfirm.Location = new System.Drawing.Point(480, 48);
            this.chkOrderConfirm.Name = "chkOrderConfirm";
            this.chkOrderConfirm.Size = new System.Drawing.Size(336, 21);
            this.chkOrderConfirm.TabIndex = 4;
            this.chkOrderConfirm.Text = "Hiển thị hộp thoại xác nhận số tiền trước khi trừ ví";
            this.chkOrderConfirm.UseVisualStyleBackColor = true;

            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnSaveSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveSettings.FlatAppearance.BorderSize = 0;
            this.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveSettings.ForeColor = System.Drawing.Color.White;
            this.btnSaveSettings.Location = new System.Drawing.Point(480, 84);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(240, 42);
            this.btnSaveSettings.TabIndex = 5;
            this.btnSaveSettings.Text = "Lưu cài đặt ứng dụng";
            this.btnSaveSettings.UseMnemonic = false;
            this.btnSaveSettings.UseVisualStyleBackColor = false;
            this.btnSaveSettings.Click += new System.EventHandler(this.BtnSaveSettings_Click);

            // 
            // panelSecurityBanner
            // 
            this.panelSecurityBanner.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelSecurityBanner.Controls.Add(this.lblSecurityContent);
            this.panelSecurityBanner.Controls.Add(this.lblSecurityTitle);
            this.panelSecurityBanner.Location = new System.Drawing.Point(25, 620);
            this.panelSecurityBanner.Name = "panelSecurityBanner";
            this.panelSecurityBanner.Padding = new System.Windows.Forms.Padding(20);
            this.panelSecurityBanner.Size = new System.Drawing.Size(980, 180);
            this.panelSecurityBanner.TabIndex = 5;

            // 
            // lblSecurityTitle
            // 
            this.lblSecurityTitle.AutoSize = true;
            this.lblSecurityTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSecurityTitle.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblSecurityTitle.Location = new System.Drawing.Point(20, 12);
            this.lblSecurityTitle.Name = "lblSecurityTitle";
            this.lblSecurityTitle.Size = new System.Drawing.Size(389, 20);
            this.lblSecurityTitle.TabIndex = 0;
            this.lblSecurityTitle.Text = "NGUYÊN TẮC BẢO MẬT TÀI KHOẢN DIGITAL STEAM";
            this.lblSecurityTitle.UseMnemonic = false;

            // 
            // lblSecurityContent
            // 
            this.lblSecurityContent.AutoSize = true;
            this.lblSecurityContent.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.lblSecurityContent.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.lblSecurityContent.Location = new System.Drawing.Point(20, 38);
            this.lblSecurityContent.Name = "lblSecurityContent";
            this.lblSecurityContent.Size = new System.Drawing.Size(900, 68);
            this.lblSecurityContent.TabIndex = 1;
            this.lblSecurityContent.Text = "• Mật khẩu nên có độ dài tối thiểu 6 ký tự, bao gồm cả chữ cái và số để đảm bảo tính an toàn cao nhất.\r\n• Không chia sẻ thông tin tài khoản hoặc mã License sản phẩm cho bất kỳ ai. Nhân viên hệ thống không bao giờ hỏi mật khẩu của bạn.\r\n• Tất cả giao dịch mua phần mềm số và nạp tiền ví được lưu trữ an toàn và bảo chứng bởi hệ thống hóa đơn điện tử.\r\n• Khi có nhu cầu hỗ trợ kỹ thuật hoặc khôi phục quyền truy cập, vui lòng liên hệ Ban Quản Trị qua Email hệ thống.";
            this.lblSecurityContent.UseMnemonic = false;

            // 
            // ProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.Controls.Add(this.panelSecurityBanner);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.panelPasswordForm);
            this.Controls.Add(this.panelProfileForm);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.lblHeader);
            this.Name = "ProfileControl";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Size = new System.Drawing.Size(1040, 750);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelProfileForm.ResumeLayout(false);
            this.panelProfileForm.PerformLayout();
            this.panelPasswordForm.ResumeLayout(false);
            this.panelPasswordForm.PerformLayout();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            this.panelSecurityBanner.ResumeLayout(false);
            this.panelSecurityBanner.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblCreatedDate;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Panel panelProfileForm;
        private System.Windows.Forms.Panel panelPasswordForm;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Panel panelSecurityBanner;
        private System.Windows.Forms.Label lblSecurityTitle;
        private System.Windows.Forms.Label lblSecurityContent;
        private System.Windows.Forms.Label lblForm1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnUpdateProfile;
        private System.Windows.Forms.Label lblForm2;
        private System.Windows.Forms.Label lblOldPass;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.Label lblNewPass;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblConfirmPass;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.CheckBox chkEmailNotify;
        private System.Windows.Forms.CheckBox chkRememberSession;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.CheckBox chkOrderConfirm;
        private System.Windows.Forms.Button btnSaveSettings;
    }
}
