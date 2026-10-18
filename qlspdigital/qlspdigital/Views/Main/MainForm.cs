using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.Shared;
using qlspdigital.Views.Nhom1_Store;
using qlspdigital.Views.Nhom2_Cart;
using qlspdigital.Views.Nhom3_ProductAdmin;
using qlspdigital.Views.Nhom4_SystemAdmin;
using qlspdigital.Views.Nhom5_Orders;
using qlspdigital.BLL.Nhom2_Cart;

namespace qlspdigital.Views.Main
{
    // Khung sườn giao diện chính (Application Shell) phong cách Steam
    // Kết nối toàn bộ 15 màn hình chức năng của 5 thành viên
    public partial class MainForm : Form
    {
        // Panel trung tâm chứa UserControl động
        private Panel panelMainContent = null!;
        private Panel panelHeader = null!;
        private Panel panelSidebar = null!;
        private StatusStrip statusStrip = null!;
        private ToolStripStatusLabel lblStatusText = null!;
        private ToolStripStatusLabel lblDbStatus = null!;

        // Header controls
        private Label lblUserGreeting = null!;
        private Label lblBalance = null!;
        private Button btnHeaderAuth = null!;

        // Sidebar navigation buttons & active state
        private Button? _activeMenuButton = null;
        private Button btnMenuStore = null!;
        private Button btnMenuReviews = null!;
        private Button btnMenuCart = null!;
        private Button btnMenuWallet = null!;
        private Button btnMenuVouchers = null!;
        private Button btnMenuLibrary = null!;
        private Button btnMenuOrderHistory = null!;

        // Admin section buttons
        private Label lblAdminSection = null!;
        private Button btnMenuProductManage = null!;
        private Button btnMenuCategoryManage = null!;
        private Button btnMenuOrderManage = null!;
        private Button btnMenuUserManage = null!;
        private Button btnMenuDashboard = null!;
        private Button btnMenuProfile = null!;

        public MainForm()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            SubscribeEvents();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "MainForm";
            this.Text = "Hệ Thống Phân Phối Sản Phẩm Kỹ Thuật Số (DPMS - Steam Edition)";
            this.Size = new Size(1300, 850);
            this.MinimumSize = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9.5f);

            // 1. THANH TIÊU ĐỀ HEADER TRÊN CÙNG
            panelHeader = new Panel();
            panelHeader.SuspendLayout();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 65;
            panelHeader.BackColor = Color.FromArgb(16, 24, 34);
            panelHeader.Padding = new Padding(15, 10, 15, 10);
            panelHeader.Paint += PanelHeader_Paint;

            Label lblLogo = new Label();
            lblLogo.Text = "⚡ DPMS";
            lblLogo.Dock = DockStyle.Left;
            lblLogo.Width = 110;
            lblLogo.ForeColor = Color.FromArgb(102, 192, 244);
            lblLogo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblLogo.TextAlign = ContentAlignment.MiddleLeft;
            lblLogo.UseMnemonic = false;

            // Vùng Header bên phải: Chào User, Nút Đăng nhập/Đăng xuất, Ví
            Panel panelHeaderRight = new Panel();
            panelHeaderRight.SuspendLayout();
            panelHeaderRight.Dock = DockStyle.Right;
            panelHeaderRight.Width = 520;
            panelHeaderRight.Padding = new Padding(0, 5, 0, 5);

            lblBalance = new Label();
            lblBalance.Text = "Ví: 0 ₫";
            lblBalance.Dock = DockStyle.Right;
            lblBalance.Width = 160;
            lblBalance.ForeColor = Color.FromArgb(164, 208, 7);
            lblBalance.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblBalance.TextAlign = ContentAlignment.MiddleRight;
            lblBalance.Cursor = Cursors.Hand;
            lblBalance.UseMnemonic = false;
            lblBalance.Click += LblBalance_Click;

            btnHeaderAuth = new Button();
            btnHeaderAuth.Text = "Đăng nhập";
            btnHeaderAuth.Dock = DockStyle.Right;
            btnHeaderAuth.Width = 145;
            btnHeaderAuth.BackColor = Color.FromArgb(49, 98, 130);
            btnHeaderAuth.ForeColor = Color.White;
            btnHeaderAuth.FlatStyle = FlatStyle.Flat;
            btnHeaderAuth.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnHeaderAuth.Cursor = Cursors.Hand;
            btnHeaderAuth.UseMnemonic = false;
            btnHeaderAuth.FlatAppearance.BorderSize = 0;
            btnHeaderAuth.Click += BtnHeaderAuth_Click;

            lblUserGreeting = new Label();
            lblUserGreeting.Text = "Khách (Chưa đăng nhập)";
            lblUserGreeting.Dock = DockStyle.Fill;
            lblUserGreeting.ForeColor = Color.FromArgb(102, 192, 244);
            lblUserGreeting.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblUserGreeting.TextAlign = ContentAlignment.MiddleRight;
            lblUserGreeting.Padding = new Padding(0, 0, 10, 0);
            lblUserGreeting.UseMnemonic = false;

            panelHeaderRight.Controls.Add(lblUserGreeting);
            panelHeaderRight.Controls.Add(btnHeaderAuth);
            panelHeaderRight.Controls.Add(lblBalance);
            panelHeaderRight.ResumeLayout(false);

            btnHeaderAuth.SendToBack();
            lblBalance.SendToBack();
            lblUserGreeting.BringToFront();

            Label lblAppTitle = new Label();
            lblAppTitle.Text = "CỬA HÀNG PHÂN PHỐI SẢN PHẨM KỸ THUẬT SỐ";
            lblAppTitle.Dock = DockStyle.Fill;
            lblAppTitle.ForeColor = Color.FromArgb(199, 213, 224);
            lblAppTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblAppTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblAppTitle.Padding = new Padding(15, 0, 0, 0);
            lblAppTitle.UseMnemonic = false;

            panelHeader.Controls.Add(lblAppTitle);
            panelHeader.Controls.Add(panelHeaderRight);
            panelHeader.Controls.Add(lblLogo);
            lblLogo.SendToBack();
            panelHeaderRight.SendToBack();
            lblAppTitle.BringToFront();
            panelHeader.ResumeLayout(false);

            // 2. THANH ĐIỀU HƯỚNG SIDEBAR BÊN TRÁI
            panelSidebar = new Panel();
            panelSidebar.SuspendLayout();
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Width = 240;
            panelSidebar.BackColor = Color.FromArgb(18, 25, 35);
            panelSidebar.AutoScroll = true;
            panelSidebar.Padding = new Padding(10, 15, 10, 15);

            // Tiêu đề phân hệ Khách hàng
            Label lblClientSection = new Label();
            lblClientSection.Text = "MUA SẮM & CÁ NHÂN";
            lblClientSection.Dock = DockStyle.Top;
            lblClientSection.Height = 25;
            lblClientSection.ForeColor = Color.FromArgb(161, 181, 196);
            lblClientSection.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblClientSection.UseMnemonic = false;

            btnMenuStore = new Button();
            btnMenuStore.Text = "🛒 Cửa hàng (Store)";
            btnMenuStore.Dock = DockStyle.Top;
            btnMenuStore.Height = 38;
            btnMenuStore.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuStore.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuStore.FlatStyle = FlatStyle.Flat;
            btnMenuStore.Font = new Font("Segoe UI", 9.5f);
            btnMenuStore.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuStore.Padding = new Padding(10, 0, 0, 0);
            btnMenuStore.Cursor = Cursors.Hand;
            btnMenuStore.UseMnemonic = false;
            btnMenuStore.FlatAppearance.BorderSize = 0;
            btnMenuStore.MouseEnter += BtnMenu_MouseEnter;
            btnMenuStore.MouseLeave += BtnMenu_MouseLeave;
            btnMenuStore.Click += BtnMenuStore_Click;

            btnMenuReviews = new Button();
            btnMenuReviews.Text = "💬 Đánh giá sản phẩm";
            btnMenuReviews.Dock = DockStyle.Top;
            btnMenuReviews.Height = 38;
            btnMenuReviews.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuReviews.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuReviews.FlatStyle = FlatStyle.Flat;
            btnMenuReviews.Font = new Font("Segoe UI", 9.5f);
            btnMenuReviews.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuReviews.Padding = new Padding(10, 0, 0, 0);
            btnMenuReviews.Cursor = Cursors.Hand;
            btnMenuReviews.UseMnemonic = false;
            btnMenuReviews.FlatAppearance.BorderSize = 0;
            btnMenuReviews.MouseEnter += BtnMenu_MouseEnter;
            btnMenuReviews.MouseLeave += BtnMenu_MouseLeave;
            btnMenuReviews.Click += BtnMenuReviews_Click;

            btnMenuCart = new Button();
            btnMenuCart.Text = "🛍️ Giỏ hàng của tôi";
            btnMenuCart.Dock = DockStyle.Top;
            btnMenuCart.Height = 38;
            btnMenuCart.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuCart.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuCart.FlatStyle = FlatStyle.Flat;
            btnMenuCart.Font = new Font("Segoe UI", 9.5f);
            btnMenuCart.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuCart.Padding = new Padding(10, 0, 0, 0);
            btnMenuCart.Cursor = Cursors.Hand;
            btnMenuCart.UseMnemonic = false;
            btnMenuCart.FlatAppearance.BorderSize = 0;
            btnMenuCart.MouseEnter += BtnMenu_MouseEnter;
            btnMenuCart.MouseLeave += BtnMenu_MouseLeave;
            btnMenuCart.Click += BtnMenuCart_Click;

            btnMenuWallet = new Button();
            btnMenuWallet.Text = "💰 Ví tài khoản";
            btnMenuWallet.Dock = DockStyle.Top;
            btnMenuWallet.Height = 38;
            btnMenuWallet.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuWallet.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuWallet.FlatStyle = FlatStyle.Flat;
            btnMenuWallet.Font = new Font("Segoe UI", 9.5f);
            btnMenuWallet.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuWallet.Padding = new Padding(10, 0, 0, 0);
            btnMenuWallet.Cursor = Cursors.Hand;
            btnMenuWallet.UseMnemonic = false;
            btnMenuWallet.FlatAppearance.BorderSize = 0;
            btnMenuWallet.MouseEnter += BtnMenu_MouseEnter;
            btnMenuWallet.MouseLeave += BtnMenu_MouseLeave;
            btnMenuWallet.Click += BtnMenuWallet_Click;

            btnMenuVouchers = new Button();
            btnMenuVouchers.Text = "🎁 Mã giảm giá (Voucher)";
            btnMenuVouchers.Dock = DockStyle.Top;
            btnMenuVouchers.Height = 38;
            btnMenuVouchers.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuVouchers.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuVouchers.FlatStyle = FlatStyle.Flat;
            btnMenuVouchers.Font = new Font("Segoe UI", 9.5f);
            btnMenuVouchers.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuVouchers.Padding = new Padding(10, 0, 0, 0);
            btnMenuVouchers.Cursor = Cursors.Hand;
            btnMenuVouchers.UseMnemonic = false;
            btnMenuVouchers.FlatAppearance.BorderSize = 0;
            btnMenuVouchers.MouseEnter += BtnMenu_MouseEnter;
            btnMenuVouchers.MouseLeave += BtnMenu_MouseLeave;
            btnMenuVouchers.Click += BtnMenuVouchers_Click;

            btnMenuLibrary = new Button();
            btnMenuLibrary.Text = "🎮 Thư viện game sở hữu";
            btnMenuLibrary.Dock = DockStyle.Top;
            btnMenuLibrary.Height = 38;
            btnMenuLibrary.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuLibrary.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuLibrary.FlatStyle = FlatStyle.Flat;
            btnMenuLibrary.Font = new Font("Segoe UI", 9.5f);
            btnMenuLibrary.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuLibrary.Padding = new Padding(10, 0, 0, 0);
            btnMenuLibrary.Cursor = Cursors.Hand;
            btnMenuLibrary.UseMnemonic = false;
            btnMenuLibrary.FlatAppearance.BorderSize = 0;
            btnMenuLibrary.MouseEnter += BtnMenu_MouseEnter;
            btnMenuLibrary.MouseLeave += BtnMenu_MouseLeave;
            btnMenuLibrary.Click += BtnMenuLibrary_Click;

            btnMenuOrderHistory = new Button();
            btnMenuOrderHistory.Text = "📜 Lịch sử mua hàng";
            btnMenuOrderHistory.Dock = DockStyle.Top;
            btnMenuOrderHistory.Height = 38;
            btnMenuOrderHistory.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuOrderHistory.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuOrderHistory.FlatStyle = FlatStyle.Flat;
            btnMenuOrderHistory.Font = new Font("Segoe UI", 9.5f);
            btnMenuOrderHistory.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuOrderHistory.Padding = new Padding(10, 0, 0, 0);
            btnMenuOrderHistory.Cursor = Cursors.Hand;
            btnMenuOrderHistory.UseMnemonic = false;
            btnMenuOrderHistory.FlatAppearance.BorderSize = 0;
            btnMenuOrderHistory.MouseEnter += BtnMenu_MouseEnter;
            btnMenuOrderHistory.MouseLeave += BtnMenu_MouseLeave;
            btnMenuOrderHistory.Click += BtnMenuOrderHistory_Click;

            btnMenuProfile = new Button();
            btnMenuProfile.Text = "⚙️ Cài đặt & Hồ sơ";
            btnMenuProfile.Dock = DockStyle.Top;
            btnMenuProfile.Height = 38;
            btnMenuProfile.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuProfile.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuProfile.FlatStyle = FlatStyle.Flat;
            btnMenuProfile.Font = new Font("Segoe UI", 9.5f);
            btnMenuProfile.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuProfile.Padding = new Padding(10, 0, 0, 0);
            btnMenuProfile.Cursor = Cursors.Hand;
            btnMenuProfile.UseMnemonic = false;
            btnMenuProfile.FlatAppearance.BorderSize = 0;
            btnMenuProfile.MouseEnter += BtnMenu_MouseEnter;
            btnMenuProfile.MouseLeave += BtnMenu_MouseLeave;
            btnMenuProfile.Click += BtnMenuProfile_Click;

            // Phân hệ Quản trị Admin
            lblAdminSection = new Label();
            lblAdminSection.Text = "QUẢN TRỊ VIÊN (ADMIN)";
            lblAdminSection.Dock = DockStyle.Top;
            lblAdminSection.Height = 30;
            lblAdminSection.ForeColor = Color.FromArgb(243, 156, 18);
            lblAdminSection.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblAdminSection.Padding = new Padding(0, 10, 0, 0);
            lblAdminSection.UseMnemonic = false;

            btnMenuDashboard = new Button();
            btnMenuDashboard.Text = "📊 Thống kê Dashboard";
            btnMenuDashboard.Dock = DockStyle.Top;
            btnMenuDashboard.Height = 38;
            btnMenuDashboard.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuDashboard.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuDashboard.FlatStyle = FlatStyle.Flat;
            btnMenuDashboard.Font = new Font("Segoe UI", 9.5f);
            btnMenuDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuDashboard.Padding = new Padding(10, 0, 0, 0);
            btnMenuDashboard.Cursor = Cursors.Hand;
            btnMenuDashboard.UseMnemonic = false;
            btnMenuDashboard.FlatAppearance.BorderSize = 0;
            btnMenuDashboard.MouseEnter += BtnMenu_MouseEnter;
            btnMenuDashboard.MouseLeave += BtnMenu_MouseLeave;
            btnMenuDashboard.Click += BtnMenuDashboard_Click;

            btnMenuProductManage = new Button();
            btnMenuProductManage.Text = "📦 Quản lý hàng hóa số";
            btnMenuProductManage.Dock = DockStyle.Top;
            btnMenuProductManage.Height = 38;
            btnMenuProductManage.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuProductManage.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuProductManage.FlatStyle = FlatStyle.Flat;
            btnMenuProductManage.Font = new Font("Segoe UI", 9.5f);
            btnMenuProductManage.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuProductManage.Padding = new Padding(10, 0, 0, 0);
            btnMenuProductManage.Cursor = Cursors.Hand;
            btnMenuProductManage.UseMnemonic = false;
            btnMenuProductManage.FlatAppearance.BorderSize = 0;
            btnMenuProductManage.MouseEnter += BtnMenu_MouseEnter;
            btnMenuProductManage.MouseLeave += BtnMenu_MouseLeave;
            btnMenuProductManage.Click += BtnMenuProductManage_Click;

            btnMenuCategoryManage = new Button();
            btnMenuCategoryManage.Text = "📁 Quản lý danh mục";
            btnMenuCategoryManage.Dock = DockStyle.Top;
            btnMenuCategoryManage.Height = 38;
            btnMenuCategoryManage.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuCategoryManage.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuCategoryManage.FlatStyle = FlatStyle.Flat;
            btnMenuCategoryManage.Font = new Font("Segoe UI", 9.5f);
            btnMenuCategoryManage.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuCategoryManage.Padding = new Padding(10, 0, 0, 0);
            btnMenuCategoryManage.Cursor = Cursors.Hand;
            btnMenuCategoryManage.UseMnemonic = false;
            btnMenuCategoryManage.FlatAppearance.BorderSize = 0;
            btnMenuCategoryManage.MouseEnter += BtnMenu_MouseEnter;
            btnMenuCategoryManage.MouseLeave += BtnMenu_MouseLeave;
            btnMenuCategoryManage.Click += BtnMenuCategoryManage_Click;

            btnMenuOrderManage = new Button();
            btnMenuOrderManage.Text = "📑 Quản lý đơn hàng";
            btnMenuOrderManage.Dock = DockStyle.Top;
            btnMenuOrderManage.Height = 38;
            btnMenuOrderManage.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuOrderManage.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuOrderManage.FlatStyle = FlatStyle.Flat;
            btnMenuOrderManage.Font = new Font("Segoe UI", 9.5f);
            btnMenuOrderManage.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuOrderManage.Padding = new Padding(10, 0, 0, 0);
            btnMenuOrderManage.Cursor = Cursors.Hand;
            btnMenuOrderManage.UseMnemonic = false;
            btnMenuOrderManage.FlatAppearance.BorderSize = 0;
            btnMenuOrderManage.MouseEnter += BtnMenu_MouseEnter;
            btnMenuOrderManage.MouseLeave += BtnMenu_MouseLeave;
            btnMenuOrderManage.Click += BtnMenuOrderManage_Click;

            btnMenuUserManage = new Button();
            btnMenuUserManage.Text = "👥 Quản lý người dùng";
            btnMenuUserManage.Dock = DockStyle.Top;
            btnMenuUserManage.Height = 38;
            btnMenuUserManage.BackColor = Color.FromArgb(18, 25, 35);
            btnMenuUserManage.ForeColor = Color.FromArgb(199, 213, 224);
            btnMenuUserManage.FlatStyle = FlatStyle.Flat;
            btnMenuUserManage.Font = new Font("Segoe UI", 9.5f);
            btnMenuUserManage.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuUserManage.Padding = new Padding(10, 0, 0, 0);
            btnMenuUserManage.Cursor = Cursors.Hand;
            btnMenuUserManage.UseMnemonic = false;
            btnMenuUserManage.FlatAppearance.BorderSize = 0;
            btnMenuUserManage.MouseEnter += BtnMenu_MouseEnter;
            btnMenuUserManage.MouseLeave += BtnMenu_MouseLeave;
            btnMenuUserManage.Click += BtnMenuUserManage_Click;

            // Thêm vào sidebar theo thứ tự
            panelSidebar.Controls.Add(btnMenuUserManage);
            panelSidebar.Controls.Add(btnMenuOrderManage);
            panelSidebar.Controls.Add(btnMenuCategoryManage);
            panelSidebar.Controls.Add(btnMenuProductManage);
            panelSidebar.Controls.Add(btnMenuDashboard);
            panelSidebar.Controls.Add(lblAdminSection);
            panelSidebar.Controls.Add(btnMenuProfile);
            panelSidebar.Controls.Add(btnMenuOrderHistory);
            panelSidebar.Controls.Add(btnMenuLibrary);
            panelSidebar.Controls.Add(btnMenuVouchers);
            panelSidebar.Controls.Add(btnMenuWallet);
            panelSidebar.Controls.Add(btnMenuCart);
            panelSidebar.Controls.Add(btnMenuReviews);
            panelSidebar.Controls.Add(btnMenuStore);
            panelSidebar.Controls.Add(lblClientSection);
            panelSidebar.ResumeLayout(false);

            // 3. THANH TRẠNG THÁI STATUSBAR DƯỚI CÙNG
            statusStrip = new StatusStrip();
            statusStrip.SuspendLayout();
            statusStrip.BackColor = Color.FromArgb(18, 25, 35);
            statusStrip.ForeColor = Color.FromArgb(161, 181, 196);

            lblStatusText = new ToolStripStatusLabel();
            lblStatusText.Text = "Trạng thái hệ thống: Sẵn sàng | Phiên bản: 1.0.0";
            lblStatusText.Spring = true;
            lblStatusText.TextAlign = ContentAlignment.MiddleLeft;

            lblDbStatus = new ToolStripStatusLabel();
            lblDbStatus.Text = "CSDL: DPMS_Db (Đang kết nối...)";
            lblDbStatus.TextAlign = ContentAlignment.MiddleRight;

            statusStrip.Items.Add(lblStatusText);
            statusStrip.Items.Add(lblDbStatus);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();

            // 4. VÙNG NỘI DUNG CHÍNH (panelMainContent)
            panelMainContent = new Panel();
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.BackColor = Color.FromArgb(23, 26, 33);

            this.Controls.Add(panelMainContent);
            this.Controls.Add(panelSidebar);
            this.Controls.Add(statusStrip);
            this.Controls.Add(panelHeader);

            statusStrip.SendToBack();
            panelSidebar.SendToBack();
            panelHeader.SendToBack();
            panelMainContent.BringToFront();

            this.Load += MainForm_Load;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void PanelHeader_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                using var p = new Pen(Color.FromArgb(35, 48, 68), 1);
                e.Graphics.DrawLine(p, 0, pnl.Height - 1, pnl.Width, pnl.Height - 1);
            }
        }

        private void SetActiveMenu(Button? btn)
        {
            if (_activeMenuButton != null && _activeMenuButton != btn)
            {
                _activeMenuButton.BackColor = Color.FromArgb(18, 25, 35);
                _activeMenuButton.ForeColor = Color.FromArgb(199, 213, 224);
                _activeMenuButton.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            }

            _activeMenuButton = btn;

            if (_activeMenuButton != null)
            {
                _activeMenuButton.BackColor = Color.FromArgb(49, 98, 130);
                _activeMenuButton.ForeColor = Color.White;
                _activeMenuButton.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            }
        }

        private void BtnMenu_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn != _activeMenuButton)
            {
                btn.BackColor = Color.FromArgb(42, 71, 94);
                btn.ForeColor = Color.White;
            }
        }

        private void BtnMenu_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn != _activeMenuButton)
            {
                btn.BackColor = Color.FromArgb(18, 25, 35);
                btn.ForeColor = Color.FromArgb(199, 213, 224);
            }
        }

        private void BtnMenuStore_Click(object? sender, EventArgs e) => NavigateToStore();
        private void BtnMenuReviews_Click(object? sender, EventArgs e) => NavigateToReviews(1);
        private void BtnMenuCart_Click(object? sender, EventArgs e) => NavigateToCart();
        private void BtnMenuWallet_Click(object? sender, EventArgs e) => NavigateToWallet();
        private void BtnMenuVouchers_Click(object? sender, EventArgs e) => NavigateToVouchers();
        private void BtnMenuLibrary_Click(object? sender, EventArgs e) => NavigateToLibrary();
        private void BtnMenuOrderHistory_Click(object? sender, EventArgs e) => NavigateToOrderHistory();
        private void BtnMenuProfile_Click(object? sender, EventArgs e) => NavigateToProfile();
        private void BtnMenuDashboard_Click(object? sender, EventArgs e) => NavigateToDashboard();
        private void BtnMenuProductManage_Click(object? sender, EventArgs e) => NavigateToProductManage();
        private void BtnMenuCategoryManage_Click(object? sender, EventArgs e) => NavigateToCategoryManage();
        private void BtnMenuOrderManage_Click(object? sender, EventArgs e) => NavigateToOrderManage();
        private void BtnMenuUserManage_Click(object? sender, EventArgs e) => NavigateToUserManage();
        private void LblBalance_Click(object? sender, EventArgs e) => NavigateToWallet();

        private void SubscribeEvents()
        {
            UserSession.SessionChanged += UpdateUserInterfaceSession;
            UserSession.BalanceChanged += (bal) =>
            {
                lblBalance.Text = $"Ví: {FormatHelper.FormatCurrency(bal)}";
            };
            GioHangService.CartUpdated += UpdateCartBadge;
        }

        private void UpdateCartBadge()
        {
            int count = 0;
            if (UserSession.IsLoggedIn)
            {
                var cartService = new GioHangService();
                count = cartService.LayTongSoLuongItem(UserSession.CurrentUserId);
            }
            btnMenuCart.Text = $"🛍️ Giỏ hàng ({count})";
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;

            // Kích hoạt giao diện Dark Mode (thanh cuộn và thanh tiêu đề)
            ThemeHelper.ApplyDarkMode(this);

            // Kiểm tra kết nối CSDL
            if (DatabaseHelper.TestConnection(out string msg))
            {
                lblDbStatus.Text = "🟢 CSDL: DPMS_Db (Kết nối thành công)";
                lblDbStatus.ForeColor = Color.FromArgb(164, 208, 7);
            }
            else
            {
                lblDbStatus.Text = "🔴 CSDL: Lỗi kết nối DPMS_Db";
                lblDbStatus.ForeColor = Color.FromArgb(231, 76, 60);
                MessageBox.Show($"Không thể kết nối CSDL SQL Server!\n{msg}\n\nVui lòng đảm bảo SQL Server đang chạy và đã thực thi file script 'Database/DPMS_Database_Master.sql'.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Tự động mở Trang chủ Cửa hàng khi khởi động
            NavigateToStore();
            UpdateUserInterfaceSession();
        }

        // Hàm dùng chung chuyển đổi linh hoạt các UserControl vào vùng chính
        public void SwitchView(UserControl newView)
        {
            panelMainContent.Controls.Clear();
            newView.Dock = DockStyle.Fill;
            panelMainContent.Controls.Add(newView);
            newView.BringToFront();
            ThemeHelper.ApplyDarkMode(newView);
        }

        private void UpdateUserInterfaceSession()
        {
            bool isAdmin = UserSession.IsLoggedIn && UserSession.IsAdmin;

            if (UserSession.IsLoggedIn)
            {
                lblUserGreeting.Text = $"{UserSession.CurrentFullName} ({(UserSession.IsAdmin ? "Admin" : "Khách Hàng")})";
                lblBalance.Text = $"Ví: {FormatHelper.FormatCurrency(UserSession.CurrentBalance)}";
                btnHeaderAuth.Text = "Đăng xuất";
                btnHeaderAuth.BackColor = Color.FromArgb(180, 40, 40);
            }
            else
            {
                lblUserGreeting.Text = "Khách (Chưa đăng nhập)";
                lblBalance.Text = "Ví: 0 ₫";
                btnHeaderAuth.Text = "Đăng nhập";
                btnHeaderAuth.BackColor = Color.FromArgb(49, 98, 130);
            }

            // Phân quyền chuẩn RBAC: Chỉ tài khoản Quản trị viên (QuanTriVien) mới hiển thị menu Admin
            lblAdminSection.Visible = isAdmin;
            btnMenuDashboard.Visible = isAdmin;
            btnMenuProductManage.Visible = isAdmin;
            btnMenuCategoryManage.Visible = isAdmin;
            btnMenuOrderManage.Visible = isAdmin;
            btnMenuUserManage.Visible = isAdmin;

            UpdateCartBadge();
        }

        // ==========================================
        // CƠ CHẾ BẢO VỆ PHÂN QUYỀN (RBAC GUARDS)
        // ==========================================

        // Kiểm tra quyền đăng nhập cho các tính năng cá nhân
        private bool EnsureLoggedIn(string tenChucNang)
        {
            if (!UserSession.IsLoggedIn)
            {
                var result = MessageBox.Show(
                    $"Bạn cần đăng nhập tài khoản để truy cập '{tenChucNang}'.\nBạn có muốn chuyển sang màn hình Đăng nhập ngay bây giờ không?",
                    "Yêu cầu đăng nhập",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    NavigateToLogin();
                }
                return false;
            }
            return true;
        }

        // Kiểm tra quyền Quản trị viên cho các chức năng nhạy cảm của hệ thống
        private bool EnsureAdmin(string tenChucNang)
        {
            if (!UserSession.IsLoggedIn)
            {
                EnsureLoggedIn(tenChucNang);
                return false;
            }

            if (!UserSession.IsAdmin)
            {
                MessageBox.Show(
                    $"Tài khoản hiện tại không có quyền Quản trị viên để truy cập '{tenChucNang}'!",
                    "Từ chối truy cập (Access Denied)",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                NavigateToStore();
                return false;
            }
            return true;
        }

        // ==========================================
        // CÁC HÀM ĐIỀU HƯỚNG MÀN HÌNH CHỨC NĂNG
        // ==========================================

        public void NavigateToStore()
        {
            SetActiveMenu(btnMenuStore);
            StoreViewControl store = new StoreViewControl();
            store.OnViewProductDetail += (spId) => NavigateToProductDetail(spId);
            SwitchView(store);
            lblStatusText.Text = "Đang xem: Cửa hàng sản phẩm số (Nhóm 1 - Lê Nguyễn Minh Trí)";
        }

        public void NavigateToProductDetail(int maSanPham)
        {
            SetActiveMenu(btnMenuStore);
            ProductDetailControl detail = new ProductDetailControl();
            detail.LoadProductDetail(maSanPham);
            detail.OnBackToStore += () => NavigateToStore();
            detail.OnViewReviews += (id) => NavigateToReviews(id);
            SwitchView(detail);
            lblStatusText.Text = "Đang xem: Chi tiết sản phẩm số (Nhóm 1 - Lê Nguyễn Minh Trí)";
        }

        public void NavigateToReviews(int maSanPham = 1)
        {
            SetActiveMenu(btnMenuReviews);
            ProductReviewControl reviews = new ProductReviewControl();
            reviews.SetSelectedProduct(maSanPham);
            SwitchView(reviews);
            lblStatusText.Text = "Đang xem: Đánh giá & Nhận xét sản phẩm (Nhóm 1 - Lê Nguyễn Minh Trí)";
        }

        public void NavigateToCart(string? initialVoucherCode = null)
        {
            if (!EnsureLoggedIn("Giỏ hàng của tôi")) return;

            SetActiveMenu(btnMenuCart);
            CartControl cart = new CartControl();
            if (!string.IsNullOrWhiteSpace(initialVoucherCode))
            {
                cart.SetInitialVoucherCode(initialVoucherCode);
            }
            cart.OnNavigateToLibrary += () => NavigateToLibrary();
            cart.OnNavigateToWallet += () => NavigateToWallet();
            cart.OnNavigateToVouchers += () => NavigateToVouchers();
            SwitchView(cart);
            lblStatusText.Text = "Đang xem: Giỏ hàng mua sắm & Thanh toán (Nhóm 2 - Nguyễn Quốc Trung)";
        }

        public void NavigateToWallet()
        {
            if (!EnsureLoggedIn("Ví tài khoản")) return;

            SetActiveMenu(btnMenuWallet);
            WalletControl wallet = new WalletControl();
            SwitchView(wallet);
            lblStatusText.Text = "Đang xem: Ví tài khoản & Nạp tiền mô phỏng (Nhóm 2 - Nguyễn Quốc Trung)";
        }

        public void NavigateToVouchers()
        {
            if (!EnsureLoggedIn("Kho mã ưu đãi & Voucher")) return;

            SetActiveMenu(btnMenuVouchers);
            VoucherControl voucher = new VoucherControl();
            voucher.OnNavigateToCart += () => NavigateToCart();
            voucher.OnNavigateToCartWithVoucher += (code) => NavigateToCart(code);
            SwitchView(voucher);
            lblStatusText.Text = "Đang xem: Kho mã ưu đãi & Voucher giảm giá (Nhóm 2 - Nguyễn Quốc Trung)";
        }

        public void NavigateToLibrary()
        {
            if (!EnsureLoggedIn("Thư viện game sở hữu")) return;

            SetActiveMenu(btnMenuLibrary);
            LibraryControl library = new LibraryControl();
            SwitchView(library);
            lblStatusText.Text = "Đang xem: Thư viện sản phẩm đã mua & Tải tệp (Nhóm 5 - Phạm Văn Đam)";
        }

        public void NavigateToOrderHistory()
        {
            if (!EnsureLoggedIn("Lịch sử mua hàng")) return;

            SetActiveMenu(btnMenuOrderHistory);
            OrderHistoryControl history = new OrderHistoryControl();
            SwitchView(history);
            lblStatusText.Text = "Đang xem: Lịch sử đơn hàng cá nhân (Nhóm 5 - Phạm Văn Đam)";
        }

        public void NavigateToProfile()
        {
            if (!EnsureLoggedIn("Hồ sơ & Đổi mật khẩu")) return;

            SetActiveMenu(btnMenuProfile);
            ProfileControl profile = new ProfileControl();
            SwitchView(profile);
            lblStatusText.Text = "Đang xem: Hồ sơ tài khoản & Đổi mật khẩu (Nhóm 3 - Phạm Trung Hiếu)";
        }

        public void NavigateToOrderManage()
        {
            if (!EnsureAdmin("Quản lý và duyệt đơn hàng toàn hệ thống")) return;

            SetActiveMenu(btnMenuOrderManage);
            OrderManageControl orders = new OrderManageControl();
            SwitchView(orders);
            lblStatusText.Text = "Đang xem: Quản lý và duyệt đơn hàng toàn hệ thống (Nhóm 5 - Phạm Văn Đam)";
        }

        public void NavigateToProductManage()
        {
            if (!EnsureAdmin("Quản trị kho hàng hóa & Tệp số")) return;

            SetActiveMenu(btnMenuProductManage);
            ProductManageControl prod = new ProductManageControl();
            SwitchView(prod);
            lblStatusText.Text = "Đang xem: Quản trị kho hàng hóa & Tệp số (Nhóm 3 - Phạm Trung Hiếu)";
        }

        public void NavigateToCategoryManage()
        {
            if (!EnsureAdmin("Quản lý danh mục thể loại")) return;

            SetActiveMenu(btnMenuCategoryManage);
            CategoryManageControl cat = new CategoryManageControl();
            SwitchView(cat);
            lblStatusText.Text = "Đang xem: Quản lý danh mục thể loại (Nhóm 3 - Phạm Trung Hiếu)";
        }

        public void NavigateToDashboard()
        {
            if (!EnsureAdmin("Báo cáo chỉ số kinh doanh Dashboard")) return;

            SetActiveMenu(btnMenuDashboard);
            DashboardControl dash = new DashboardControl();
            SwitchView(dash);
            lblStatusText.Text = "Đang xem: Báo cáo chỉ số kinh doanh Dashboard (Nhóm 4 - Trần Quốc Thuận)";
        }

        public void NavigateToUserManage()
        {
            if (!EnsureAdmin("Quản lý danh sách người dùng")) return;

            SetActiveMenu(btnMenuUserManage);
            UserManageControl userMgmt = new UserManageControl();
            SwitchView(userMgmt);
            lblStatusText.Text = "Đang xem: Quản lý danh sách người dùng (Nhóm 4 - Trần Quốc Thuận)";
        }

        public void NavigateToLogin()
        {
            SetActiveMenu(null);
            LoginControl login = new LoginControl();
            login.OnLoginSuccess += () => NavigateToStore();
            login.OnNavigateToRegister += () => NavigateToRegister();
            SwitchView(login);
            lblStatusText.Text = "Đang xem: Đăng nhập tài khoản (Nhóm 4 - Trần Quốc Thuận)";
        }

        public void NavigateToRegister()
        {
            SetActiveMenu(null);
            RegisterControl register = new RegisterControl();
            register.OnNavigateToLogin += () => NavigateToLogin();
            SwitchView(register);
            lblStatusText.Text = "Đang xem: Đăng ký thành viên mới (Nhóm 4 - Trần Quốc Thuận)";
        }

        private void BtnHeaderAuth_Click(object? sender, EventArgs e)
        {
            if (UserSession.IsLoggedIn)
            {
                if (MessageBox.Show("Bạn có muốn đăng xuất khỏi hệ thống?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UserSession.Logout();
                    NavigateToStore();
                }
            }
            else
            {
                NavigateToLogin();
            }
        }
    }
}
