using System.Drawing;
using System.Windows.Forms;

namespace qlspdigital.Views.Main
{
    partial class MainForm
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

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            qlspdigital.Shared.ThemeHelper.ApplyTheme(this);
            if (System.Windows.Forms.Screen.PrimaryScreen != null && System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height <= 768)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        // Hàm chuyển đổi linh hoạt các UserControl vào vùng hiển thị chính
        public void SwitchView(UserControl newView)
        {
            panelMainContent.Controls.Clear();
            newView.Dock = DockStyle.Fill;
            panelMainContent.Controls.Add(newView);
            newView.BringToFront();
            qlspdigital.Shared.ThemeHelper.ApplyTheme(newView);
            UpdateCartBadge();
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.btnThemeToggle = new System.Windows.Forms.Button();
            this.panelHeaderRight = new System.Windows.Forms.Panel();
            this.lblUserGreeting = new System.Windows.Forms.Label();
            this.btnHeaderAuth = new System.Windows.Forms.Button();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.lblClientSection = new System.Windows.Forms.Label();
            this.btnMenuStore = new System.Windows.Forms.Button();
            this.btnMenuReviews = new System.Windows.Forms.Button();
            this.btnMenuCart = new System.Windows.Forms.Button();
            this.btnMenuWallet = new System.Windows.Forms.Button();
            this.btnMenuVouchers = new System.Windows.Forms.Button();
            this.btnMenuLibrary = new System.Windows.Forms.Button();
            this.btnMenuOrderHistory = new System.Windows.Forms.Button();
            this.btnMenuProfile = new System.Windows.Forms.Button();
            this.lblAdminSection = new System.Windows.Forms.Label();
            this.btnMenuDashboard = new System.Windows.Forms.Button();
            this.btnMenuProductManage = new System.Windows.Forms.Button();
            this.btnMenuCategoryManage = new System.Windows.Forms.Button();
            this.btnMenuOrderManage = new System.Windows.Forms.Button();
            this.btnMenuUserManage = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMainContent = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.panelHeaderRight.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(16, 24, 34);
            this.panelHeader.Controls.Add(this.lblAppTitle);
            this.panelHeader.Controls.Add(this.panelHeaderRight);
            this.panelHeader.Controls.Add(this.lblLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelHeader.Size = new System.Drawing.Size(1284, 65);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelHeader_Paint);

            // 
            // lblLogo
            // 
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblLogo.Location = new System.Drawing.Point(15, 10);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(110, 45);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "⚡ DPMS";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLogo.UseMnemonic = false;

            // 
            // panelHeaderRight
            // 
            this.panelHeaderRight.BackColor = System.Drawing.Color.FromArgb(16, 24, 34);
            this.panelHeaderRight.Controls.Add(this.lblUserGreeting);
            this.panelHeaderRight.Controls.Add(this.btnThemeToggle);
            this.panelHeaderRight.Controls.Add(this.btnHeaderAuth);
            this.panelHeaderRight.Controls.Add(this.lblBalance);
            this.panelHeaderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelHeaderRight.Location = new System.Drawing.Point(634, 10);
            this.panelHeaderRight.Name = "panelHeaderRight";
            this.panelHeaderRight.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.panelHeaderRight.Size = new System.Drawing.Size(635, 45);
            this.panelHeaderRight.TabIndex = 1;

            // 
            // btnThemeToggle
            // 
            this.btnThemeToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemeToggle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnThemeToggle.FlatAppearance.BorderSize = 1;
            this.btnThemeToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemeToggle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThemeToggle.Location = new System.Drawing.Point(225, 5);
            this.btnThemeToggle.Name = "btnThemeToggle";
            this.btnThemeToggle.Size = new System.Drawing.Size(130, 35);
            this.btnThemeToggle.TabIndex = 3;
            this.btnThemeToggle.Text = "☀️ Sáng";
            this.btnThemeToggle.UseMnemonic = false;
            this.btnThemeToggle.UseVisualStyleBackColor = false;
            this.btnThemeToggle.Click += new System.EventHandler(this.BtnThemeToggle_Click);

            // 
            // lblBalance
            // 
            this.lblBalance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBalance.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblBalance.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblBalance.ForeColor = System.Drawing.Color.FromArgb(164, 208, 7);
            this.lblBalance.Location = new System.Drawing.Point(360, 5);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(160, 35);
            this.lblBalance.TabIndex = 0;
            this.lblBalance.Text = "Ví: 0 ₫";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBalance.UseMnemonic = false;
            this.lblBalance.Click += new System.EventHandler(this.LblBalance_Click);

            // 
            // btnHeaderAuth
            // 
            this.btnHeaderAuth.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnHeaderAuth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeaderAuth.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnHeaderAuth.FlatAppearance.BorderSize = 0;
            this.btnHeaderAuth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeaderAuth.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHeaderAuth.ForeColor = System.Drawing.Color.White;
            this.btnHeaderAuth.Location = new System.Drawing.Point(215, 5);
            this.btnHeaderAuth.Name = "btnHeaderAuth";
            this.btnHeaderAuth.Size = new System.Drawing.Size(130, 35);
            this.btnHeaderAuth.TabIndex = 1;
            this.btnHeaderAuth.Text = "Đăng nhập";
            this.btnHeaderAuth.UseMnemonic = false;
            this.btnHeaderAuth.UseVisualStyleBackColor = false;
            this.btnHeaderAuth.Click += new System.EventHandler(this.BtnHeaderAuth_Click);

            // 
            // lblUserGreeting
            // 
            this.lblUserGreeting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserGreeting.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserGreeting.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblUserGreeting.Location = new System.Drawing.Point(0, 5);
            this.lblUserGreeting.Name = "lblUserGreeting";
            this.lblUserGreeting.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblUserGreeting.Size = new System.Drawing.Size(215, 35);
            this.lblUserGreeting.TabIndex = 2;
            this.lblUserGreeting.Text = "Khách (Chưa đăng nhập)";
            this.lblUserGreeting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUserGreeting.UseMnemonic = false;

            // 
            // lblAppTitle
            // 
            this.lblAppTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.lblAppTitle.Location = new System.Drawing.Point(125, 10);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lblAppTitle.Size = new System.Drawing.Size(624, 45);
            this.lblAppTitle.TabIndex = 2;
            this.lblAppTitle.Text = "CỬA HÀNG PHÂN PHỐI SẢN PHẨM KỸ THUẬT SỐ";
            this.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAppTitle.UseMnemonic = false;

            // 
            // panelSidebar
            // 
            this.panelSidebar.AutoScroll = true;
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.panelSidebar.Controls.Add(this.btnMenuUserManage);
            this.panelSidebar.Controls.Add(this.btnMenuOrderManage);
            this.panelSidebar.Controls.Add(this.btnMenuCategoryManage);
            this.panelSidebar.Controls.Add(this.btnMenuProductManage);
            this.panelSidebar.Controls.Add(this.btnMenuDashboard);
            this.panelSidebar.Controls.Add(this.lblAdminSection);
            this.panelSidebar.Controls.Add(this.btnMenuProfile);
            this.panelSidebar.Controls.Add(this.btnMenuOrderHistory);
            this.panelSidebar.Controls.Add(this.btnMenuLibrary);
            this.panelSidebar.Controls.Add(this.btnMenuVouchers);
            this.panelSidebar.Controls.Add(this.btnMenuWallet);
            this.panelSidebar.Controls.Add(this.btnMenuCart);
            this.panelSidebar.Controls.Add(this.btnMenuReviews);
            this.panelSidebar.Controls.Add(this.btnMenuStore);
            this.panelSidebar.Controls.Add(this.lblClientSection);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 65);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.panelSidebar.Size = new System.Drawing.Size(240, 724);
            this.panelSidebar.TabIndex = 1;

            // 
            // lblClientSection
            // 
            this.lblClientSection.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblClientSection.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblClientSection.ForeColor = System.Drawing.Color.FromArgb(161, 181, 196);
            this.lblClientSection.Location = new System.Drawing.Point(10, 15);
            this.lblClientSection.Name = "lblClientSection";
            this.lblClientSection.Size = new System.Drawing.Size(220, 25);
            this.lblClientSection.TabIndex = 0;
            this.lblClientSection.Text = "MUA SẮM & CÁ NHÂN";
            this.lblClientSection.UseMnemonic = false;

            // 
            // btnMenuStore
            // 
            this.btnMenuStore.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuStore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuStore.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuStore.FlatAppearance.BorderSize = 0;
            this.btnMenuStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuStore.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuStore.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuStore.Location = new System.Drawing.Point(10, 40);
            this.btnMenuStore.Name = "btnMenuStore";
            this.btnMenuStore.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuStore.Size = new System.Drawing.Size(220, 38);
            this.btnMenuStore.TabIndex = 1;
            this.btnMenuStore.Text = "🛒 Cửa hàng (Store)";
            this.btnMenuStore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuStore.UseMnemonic = false;
            this.btnMenuStore.UseVisualStyleBackColor = false;
            this.btnMenuStore.Click += new System.EventHandler(this.BtnMenuStore_Click);
            this.btnMenuStore.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuStore.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuReviews
            // 
            this.btnMenuReviews.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuReviews.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuReviews.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuReviews.FlatAppearance.BorderSize = 0;
            this.btnMenuReviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuReviews.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuReviews.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuReviews.Location = new System.Drawing.Point(10, 78);
            this.btnMenuReviews.Name = "btnMenuReviews";
            this.btnMenuReviews.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuReviews.Size = new System.Drawing.Size(220, 38);
            this.btnMenuReviews.TabIndex = 2;
            this.btnMenuReviews.Text = "💬 Đánh giá sản phẩm";
            this.btnMenuReviews.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuReviews.UseMnemonic = false;
            this.btnMenuReviews.UseVisualStyleBackColor = false;
            this.btnMenuReviews.Click += new System.EventHandler(this.BtnMenuReviews_Click);
            this.btnMenuReviews.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuReviews.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuCart
            // 
            this.btnMenuCart.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuCart.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuCart.FlatAppearance.BorderSize = 0;
            this.btnMenuCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuCart.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuCart.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuCart.Location = new System.Drawing.Point(10, 116);
            this.btnMenuCart.Name = "btnMenuCart";
            this.btnMenuCart.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuCart.Size = new System.Drawing.Size(220, 38);
            this.btnMenuCart.TabIndex = 3;
            this.btnMenuCart.Text = "🛍️ Giỏ hàng của tôi";
            this.btnMenuCart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuCart.UseMnemonic = false;
            this.btnMenuCart.UseVisualStyleBackColor = false;
            this.btnMenuCart.Click += new System.EventHandler(this.BtnMenuCart_Click);
            this.btnMenuCart.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuCart.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuWallet
            // 
            this.btnMenuWallet.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuWallet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuWallet.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuWallet.FlatAppearance.BorderSize = 0;
            this.btnMenuWallet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuWallet.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuWallet.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuWallet.Location = new System.Drawing.Point(10, 154);
            this.btnMenuWallet.Name = "btnMenuWallet";
            this.btnMenuWallet.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuWallet.Size = new System.Drawing.Size(220, 38);
            this.btnMenuWallet.TabIndex = 4;
            this.btnMenuWallet.Text = "💰 Ví tài khoản";
            this.btnMenuWallet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuWallet.UseMnemonic = false;
            this.btnMenuWallet.UseVisualStyleBackColor = false;
            this.btnMenuWallet.Click += new System.EventHandler(this.BtnMenuWallet_Click);
            this.btnMenuWallet.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuWallet.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuVouchers
            // 
            this.btnMenuVouchers.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuVouchers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuVouchers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuVouchers.FlatAppearance.BorderSize = 0;
            this.btnMenuVouchers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuVouchers.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuVouchers.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuVouchers.Location = new System.Drawing.Point(10, 192);
            this.btnMenuVouchers.Name = "btnMenuVouchers";
            this.btnMenuVouchers.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuVouchers.Size = new System.Drawing.Size(220, 38);
            this.btnMenuVouchers.TabIndex = 5;
            this.btnMenuVouchers.Text = "🎁 Mã giảm giá (Voucher)";
            this.btnMenuVouchers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuVouchers.UseMnemonic = false;
            this.btnMenuVouchers.UseVisualStyleBackColor = false;
            this.btnMenuVouchers.Click += new System.EventHandler(this.BtnMenuVouchers_Click);
            this.btnMenuVouchers.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuVouchers.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuLibrary
            // 
            this.btnMenuLibrary.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuLibrary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuLibrary.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuLibrary.FlatAppearance.BorderSize = 0;
            this.btnMenuLibrary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuLibrary.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuLibrary.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuLibrary.Location = new System.Drawing.Point(10, 230);
            this.btnMenuLibrary.Name = "btnMenuLibrary";
            this.btnMenuLibrary.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuLibrary.Size = new System.Drawing.Size(220, 38);
            this.btnMenuLibrary.TabIndex = 6;
            this.btnMenuLibrary.Text = "🎮 Thư viện game sở hữu";
            this.btnMenuLibrary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuLibrary.UseMnemonic = false;
            this.btnMenuLibrary.UseVisualStyleBackColor = false;
            this.btnMenuLibrary.Click += new System.EventHandler(this.BtnMenuLibrary_Click);
            this.btnMenuLibrary.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuLibrary.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuOrderHistory
            // 
            this.btnMenuOrderHistory.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuOrderHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuOrderHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuOrderHistory.FlatAppearance.BorderSize = 0;
            this.btnMenuOrderHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuOrderHistory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuOrderHistory.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuOrderHistory.Location = new System.Drawing.Point(10, 268);
            this.btnMenuOrderHistory.Name = "btnMenuOrderHistory";
            this.btnMenuOrderHistory.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuOrderHistory.Size = new System.Drawing.Size(220, 38);
            this.btnMenuOrderHistory.TabIndex = 7;
            this.btnMenuOrderHistory.Text = "📜 Lịch sử mua hàng";
            this.btnMenuOrderHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuOrderHistory.UseMnemonic = false;
            this.btnMenuOrderHistory.UseVisualStyleBackColor = false;
            this.btnMenuOrderHistory.Click += new System.EventHandler(this.BtnMenuOrderHistory_Click);
            this.btnMenuOrderHistory.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuOrderHistory.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuProfile
            // 
            this.btnMenuProfile.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuProfile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuProfile.FlatAppearance.BorderSize = 0;
            this.btnMenuProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuProfile.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuProfile.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuProfile.Location = new System.Drawing.Point(10, 306);
            this.btnMenuProfile.Name = "btnMenuProfile";
            this.btnMenuProfile.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuProfile.Size = new System.Drawing.Size(220, 38);
            this.btnMenuProfile.TabIndex = 8;
            this.btnMenuProfile.Text = "⚙️ Cài đặt & Hồ sơ";
            this.btnMenuProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuProfile.UseMnemonic = false;
            this.btnMenuProfile.UseVisualStyleBackColor = false;
            this.btnMenuProfile.Click += new System.EventHandler(this.BtnMenuProfile_Click);
            this.btnMenuProfile.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuProfile.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // lblAdminSection
            // 
            this.lblAdminSection.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAdminSection.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblAdminSection.ForeColor = System.Drawing.Color.FromArgb(243, 156, 18);
            this.lblAdminSection.Location = new System.Drawing.Point(10, 344);
            this.lblAdminSection.Name = "lblAdminSection";
            this.lblAdminSection.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblAdminSection.Size = new System.Drawing.Size(220, 30);
            this.lblAdminSection.TabIndex = 9;
            this.lblAdminSection.Text = "QUẢN TRỊ VIÊN (ADMIN)";
            this.lblAdminSection.UseMnemonic = false;

            // 
            // btnMenuDashboard
            // 
            this.btnMenuDashboard.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuDashboard.FlatAppearance.BorderSize = 0;
            this.btnMenuDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuDashboard.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuDashboard.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuDashboard.Location = new System.Drawing.Point(10, 374);
            this.btnMenuDashboard.Name = "btnMenuDashboard";
            this.btnMenuDashboard.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuDashboard.Size = new System.Drawing.Size(220, 38);
            this.btnMenuDashboard.TabIndex = 10;
            this.btnMenuDashboard.Text = "📊 Thống kê Dashboard";
            this.btnMenuDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuDashboard.UseMnemonic = false;
            this.btnMenuDashboard.UseVisualStyleBackColor = false;
            this.btnMenuDashboard.Click += new System.EventHandler(this.BtnMenuDashboard_Click);
            this.btnMenuDashboard.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuDashboard.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuProductManage
            // 
            this.btnMenuProductManage.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuProductManage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuProductManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuProductManage.FlatAppearance.BorderSize = 0;
            this.btnMenuProductManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuProductManage.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuProductManage.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuProductManage.Location = new System.Drawing.Point(10, 412);
            this.btnMenuProductManage.Name = "btnMenuProductManage";
            this.btnMenuProductManage.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuProductManage.Size = new System.Drawing.Size(220, 38);
            this.btnMenuProductManage.TabIndex = 11;
            this.btnMenuProductManage.Text = "📦 Quản lý hàng hóa số";
            this.btnMenuProductManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuProductManage.UseMnemonic = false;
            this.btnMenuProductManage.UseVisualStyleBackColor = false;
            this.btnMenuProductManage.Click += new System.EventHandler(this.BtnMenuProductManage_Click);
            this.btnMenuProductManage.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuProductManage.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuCategoryManage
            // 
            this.btnMenuCategoryManage.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuCategoryManage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuCategoryManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuCategoryManage.FlatAppearance.BorderSize = 0;
            this.btnMenuCategoryManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuCategoryManage.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuCategoryManage.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuCategoryManage.Location = new System.Drawing.Point(10, 450);
            this.btnMenuCategoryManage.Name = "btnMenuCategoryManage";
            this.btnMenuCategoryManage.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuCategoryManage.Size = new System.Drawing.Size(220, 38);
            this.btnMenuCategoryManage.TabIndex = 12;
            this.btnMenuCategoryManage.Text = "📁 Quản lý danh mục";
            this.btnMenuCategoryManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuCategoryManage.UseMnemonic = false;
            this.btnMenuCategoryManage.UseVisualStyleBackColor = false;
            this.btnMenuCategoryManage.Click += new System.EventHandler(this.BtnMenuCategoryManage_Click);
            this.btnMenuCategoryManage.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuCategoryManage.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuOrderManage
            // 
            this.btnMenuOrderManage.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuOrderManage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuOrderManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuOrderManage.FlatAppearance.BorderSize = 0;
            this.btnMenuOrderManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuOrderManage.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuOrderManage.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuOrderManage.Location = new System.Drawing.Point(10, 488);
            this.btnMenuOrderManage.Name = "btnMenuOrderManage";
            this.btnMenuOrderManage.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuOrderManage.Size = new System.Drawing.Size(220, 38);
            this.btnMenuOrderManage.TabIndex = 13;
            this.btnMenuOrderManage.Text = "📑 Quản lý đơn hàng";
            this.btnMenuOrderManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuOrderManage.UseMnemonic = false;
            this.btnMenuOrderManage.UseVisualStyleBackColor = false;
            this.btnMenuOrderManage.Click += new System.EventHandler(this.BtnMenuOrderManage_Click);
            this.btnMenuOrderManage.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuOrderManage.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // btnMenuUserManage
            // 
            this.btnMenuUserManage.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.btnMenuUserManage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuUserManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenuUserManage.FlatAppearance.BorderSize = 0;
            this.btnMenuUserManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuUserManage.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMenuUserManage.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.btnMenuUserManage.Location = new System.Drawing.Point(10, 526);
            this.btnMenuUserManage.Name = "btnMenuUserManage";
            this.btnMenuUserManage.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMenuUserManage.Size = new System.Drawing.Size(220, 38);
            this.btnMenuUserManage.TabIndex = 14;
            this.btnMenuUserManage.Text = "👥 Quản lý người dùng";
            this.btnMenuUserManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuUserManage.UseMnemonic = false;
            this.btnMenuUserManage.UseVisualStyleBackColor = false;
            this.btnMenuUserManage.Click += new System.EventHandler(this.BtnMenuUserManage_Click);
            this.btnMenuUserManage.MouseEnter += new System.EventHandler(this.BtnMenu_MouseEnter);
            this.btnMenuUserManage.MouseLeave += new System.EventHandler(this.BtnMenu_MouseLeave);

            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.statusStrip.ForeColor = System.Drawing.Color.FromArgb(161, 181, 196);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusText,
            this.lblDbStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 789);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1284, 22);
            this.statusStrip.TabIndex = 2;

            // 
            // lblStatusText
            // 
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(1171, 17);
            this.lblStatusText.Spring = true;
            this.lblStatusText.Text = "Trạng thái hệ thống: Sẵn sàng | Phiên bản: 1.0.0";
            this.lblStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblDbStatus
            // 
            this.lblDbStatus.Name = "lblDbStatus";
            this.lblDbStatus.Size = new System.Drawing.Size(98, 17);
            this.lblDbStatus.Text = "CSDL: DPMS_Db (Đang kết nối...)";
            this.lblDbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // panelMainContent
            // 
            this.panelMainContent.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.panelMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContent.Location = new System.Drawing.Point(240, 65);
            this.panelMainContent.Name = "panelMainContent";
            this.panelMainContent.Size = new System.Drawing.Size(1044, 724);
            this.panelMainContent.TabIndex = 3;

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.ClientSize = new System.Drawing.Size(1200, 680);
            this.Controls.Add(this.panelMainContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Phân Phối Sản Phẩm Kỹ Thuật Số (DPMS - Steam Edition)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeaderRight.ResumeLayout(false);
            this.panelSidebar.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Panel panelMainContent;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Panel panelHeaderRight;
        private System.Windows.Forms.Label lblUserGreeting;
        private System.Windows.Forms.Button btnHeaderAuth;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblClientSection;
        private System.Windows.Forms.Button btnMenuStore;
        private System.Windows.Forms.Button btnMenuReviews;
        private System.Windows.Forms.Button btnMenuCart;
        private System.Windows.Forms.Button btnMenuWallet;
        private System.Windows.Forms.Button btnMenuVouchers;
        private System.Windows.Forms.Button btnMenuLibrary;
        private System.Windows.Forms.Button btnMenuOrderHistory;
        private System.Windows.Forms.Button btnMenuProfile;
        private System.Windows.Forms.Label lblAdminSection;
        private System.Windows.Forms.Button btnMenuDashboard;
        private System.Windows.Forms.Button btnMenuProductManage;
        private System.Windows.Forms.Button btnMenuCategoryManage;
        private System.Windows.Forms.Button btnMenuOrderManage;
        private System.Windows.Forms.Button btnMenuUserManage;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusText;
        private System.Windows.Forms.ToolStripStatusLabel lblDbStatus;
        public System.Windows.Forms.Button btnThemeToggle;

        private void PanelHeader_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (sender is System.Windows.Forms.Panel pnl)
            {
                using (System.Drawing.Pen p = new System.Drawing.Pen(qlspdigital.Shared.ThemeHelper.GetHeaderBorderColor(), 1))
                {
                    e.Graphics.DrawLine(p, 0, pnl.Height - 1, pnl.Width, pnl.Height - 1);
                }
            }
        }
    }
}
