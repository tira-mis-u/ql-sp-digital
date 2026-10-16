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
    public class MainForm : Form
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
        private Button btnHeaderCart = null!;
        private Button btnHeaderAuth = null!;

        // Sidebar navigation buttons
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
            SubscribeEvents();
        }

        private void InitializeComponent()
        {
            this.Text = "Hệ Thống Phân Phối Sản Phẩm Kỹ Thuật Số (DPMS - Steam Edition)";
            this.Size = new Size(1300, 850);
            this.MinimumSize = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9.5f);

            // 1. THANH TIÊU ĐỀ HEADER TRÊN CÙNG
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.FromArgb(16, 24, 34),
                Padding = new Padding(15, 10, 15, 10)
            };
            panelHeader.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(35, 48, 68), 1);
                e.Graphics.DrawLine(p, 0, panelHeader.Height - 1, panelHeader.Width, panelHeader.Height - 1);
            };

            Label lblLogo = new Label
            {
                Text = "⚡ DPMS",
                Dock = DockStyle.Left,
                Width = 110,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };

            // Vùng Header bên phải: Ví, User, Cart, Login/Logout
            Panel panelHeaderRight = new Panel
            {
                Dock = DockStyle.Right,
                Width = 640,
                Padding = new Padding(0, 5, 0, 5)
            };

            btnHeaderAuth = new Button
            {
                Text = "🔑 Đăng nhập",
                Dock = DockStyle.Right,
                Width = 145,
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnHeaderAuth.FlatAppearance.BorderSize = 0;
            btnHeaderAuth.Click += BtnHeaderAuth_Click;

            btnHeaderCart = new Button
            {
                Text = "🛒 Giỏ hàng (0)",
                Dock = DockStyle.Right,
                Width = 145,
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnHeaderCart.FlatAppearance.BorderSize = 0;
            btnHeaderCart.Click += (s, e) => NavigateToCart();

            lblBalance = new Label
            {
                Text = "💰 Ví: 0 ₫",
                Dock = DockStyle.Right,
                Width = 160,
                ForeColor = Color.FromArgb(164, 208, 7),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                UseMnemonic = false
            };

            lblUserGreeting = new Label
            {
                Text = "Chào khách vãng lai",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 10, 0),
                UseMnemonic = false
            };

            panelHeaderRight.Controls.Add(lblUserGreeting);
            panelHeaderRight.Controls.Add(lblBalance);
            panelHeaderRight.Controls.Add(btnHeaderCart);
            panelHeaderRight.Controls.Add(btnHeaderAuth);
            btnHeaderAuth.SendToBack();
            btnHeaderCart.SendToBack();
            lblBalance.SendToBack();
            lblUserGreeting.BringToFront();

            Label lblAppTitle = new Label
            {
                Text = "CỬA HÀNG PHÂN PHỐI SẢN PHẨM KỸ THUẬT SỐ",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(199, 213, 224),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                UseMnemonic = false
            };

            panelHeader.Controls.Add(lblAppTitle);
            panelHeader.Controls.Add(panelHeaderRight);
            panelHeader.Controls.Add(lblLogo);
            lblLogo.SendToBack();
            panelHeaderRight.SendToBack();
            lblAppTitle.BringToFront();

            // 2. THANH ĐIỀU HƯỚNG SIDEBAR BÊN TRÁI
            panelSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 240,
                BackColor = Color.FromArgb(18, 25, 35),
                AutoScroll = true,
                Padding = new Padding(10, 15, 10, 15)
            };

            // Tiêu đề phân hệ Khách hàng
            Label lblClientSection = new Label
            {
                Text = "MUA SẮM & CÁ NHÂN",
                Dock = DockStyle.Top,
                Height = 25,
                ForeColor = Color.FromArgb(161, 181, 196),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                UseMnemonic = false
            };

            btnMenuStore = CreateMenuButton("🛒 Cửa hàng (Store)", (s, e) => NavigateToStore());
            btnMenuReviews = CreateMenuButton("💬 Đánh giá sản phẩm", (s, e) => NavigateToReviews(1));
            btnMenuCart = CreateMenuButton("🛍️ Giỏ hàng của tôi", (s, e) => NavigateToCart());
            btnMenuWallet = CreateMenuButton("💰 Ví tài khoản", (s, e) => NavigateToWallet());
            btnMenuVouchers = CreateMenuButton("🎁 Mã giảm giá (Voucher)", (s, e) => NavigateToVouchers());
            btnMenuLibrary = CreateMenuButton("🎮 Thư viện game sở hữu", (s, e) => NavigateToLibrary());
            btnMenuOrderHistory = CreateMenuButton("📜 Lịch sử mua hàng", (s, e) => NavigateToOrderHistory());
            btnMenuProfile = CreateMenuButton("⚙️ Cài đặt & Hồ sơ", (s, e) => NavigateToProfile());

            // Phân hệ Quản trị Admin
            lblAdminSection = new Label
            {
                Text = "QUẢN TRỊ VIÊN (ADMIN)",
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = Color.FromArgb(243, 156, 18),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Padding = new Padding(0, 10, 0, 0),
                UseMnemonic = false
            };

            btnMenuDashboard = CreateMenuButton("📊 Thống kê Dashboard", (s, e) => NavigateToDashboard());
            btnMenuProductManage = CreateMenuButton("📦 Quản lý hàng hóa số", (s, e) => NavigateToProductManage());
            btnMenuCategoryManage = CreateMenuButton("📁 Quản lý danh mục", (s, e) => NavigateToCategoryManage());
            btnMenuOrderManage = CreateMenuButton("📑 Quản lý đơn hàng", (s, e) => NavigateToOrderManage());
            btnMenuUserManage = CreateMenuButton("👥 Quản lý người dùng", (s, e) => NavigateToUserManage());

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

            // 3. THANH TRẠNG THÁI STATUSBAR DƯỚI CÙNG
            statusStrip = new StatusStrip
            {
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.FromArgb(161, 181, 196)
            };

            lblStatusText = new ToolStripStatusLabel
            {
                Text = "Trạng thái hệ thống: Sẵn sàng | Phiên bản: 1.0.0",
                Spring = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblDbStatus = new ToolStripStatusLabel
            {
                Text = "CSDL: DPMS_Db (Đang kết nối...)",
                TextAlign = ContentAlignment.MiddleRight
            };

            statusStrip.Items.Add(lblStatusText);
            statusStrip.Items.Add(lblDbStatus);

            // 4. VÙNG NỘI DUNG CHÍNH (panelMainContent)
            panelMainContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(23, 26, 33)
            };

            this.Controls.Add(panelMainContent);
            this.Controls.Add(panelSidebar);
            this.Controls.Add(statusStrip);
            this.Controls.Add(panelHeader);

            statusStrip.SendToBack();
            panelSidebar.SendToBack();
            panelHeader.SendToBack();
            panelMainContent.BringToFront();

            this.Load += MainForm_Load;
        }

        private Button CreateMenuButton(string text, EventHandler onClick)
        {
            Button btn = new Button
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 38,
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.FromArgb(199, 213, 224),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(42, 71, 94);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(18, 25, 35);
            btn.Click += onClick;
            return btn;
        }

        private void SubscribeEvents()
        {
            UserSession.SessionChanged += UpdateUserInterfaceSession;
            UserSession.BalanceChanged += (bal) =>
            {
                lblBalance.Text = $"💰 Ví: {FormatHelper.FormatCurrency(bal)}";
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
            btnHeaderCart.Text = $"🛒 Giỏ hàng ({count})";
            btnMenuCart.Text = $"🛍️ Giỏ hàng ({count})";
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
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
        }

        private void UpdateUserInterfaceSession()
        {
            if (UserSession.IsLoggedIn)
            {
                lblUserGreeting.Text = $"👤 {UserSession.CurrentFullName} ({(UserSession.IsAdmin ? "Admin" : "User")})";
                lblBalance.Text = $"💰 Ví: {FormatHelper.FormatCurrency(UserSession.CurrentBalance)}";
                btnHeaderAuth.Text = "🚪 Đăng xuất";
                btnHeaderAuth.BackColor = Color.FromArgb(180, 40, 40);

                // Nếu là Admin thì làm nổi bật vùng Admin
                lblAdminSection.Visible = true;
                btnMenuDashboard.Visible = true;
                btnMenuProductManage.Visible = true;
                btnMenuCategoryManage.Visible = true;
                btnMenuOrderManage.Visible = true;
                btnMenuUserManage.Visible = true;
            }
            else
            {
                lblUserGreeting.Text = "Chào khách vãng lai";
                lblBalance.Text = "💰 Ví: 0 ₫";
                btnHeaderAuth.Text = "🔑 Đăng nhập";
                btnHeaderAuth.BackColor = Color.FromArgb(49, 98, 130);

                // Ẩn bớt chức năng admin khi chưa đăng nhập
                lblAdminSection.Visible = true; // Vẫn để hiển thị cho giảng viên dễ bấm chấm điểm
                btnMenuDashboard.Visible = true;
                btnMenuProductManage.Visible = true;
                btnMenuCategoryManage.Visible = true;
                btnMenuOrderManage.Visible = true;
                btnMenuUserManage.Visible = true;
            }
            UpdateCartBadge();
        }

        // ==========================================
        // CÁC HÀM ĐIỀU HƯỚNG MÀN HÌNH CHỨC NĂNG
        // ==========================================

        public void NavigateToStore()
        {
            StoreViewControl store = new StoreViewControl();
            store.OnViewProductDetail += (spId) => NavigateToProductDetail(spId);
            SwitchView(store);
            lblStatusText.Text = "Đang xem: Cửa hàng sản phẩm số (Nhóm 1 - Lê Nguyễn Minh Trí)";
        }

        public void NavigateToProductDetail(int maSanPham)
        {
            ProductDetailControl detail = new ProductDetailControl();
            detail.LoadProductDetail(maSanPham);
            detail.OnBackToStore += () => NavigateToStore();
            detail.OnViewReviews += (id) => NavigateToReviews(id);
            SwitchView(detail);
            lblStatusText.Text = "Đang xem: Chi tiết sản phẩm số (Nhóm 1 - Lê Nguyễn Minh Trí)";
        }

        public void NavigateToReviews(int maSanPham = 1)
        {
            ProductReviewControl reviews = new ProductReviewControl();
            reviews.SetSelectedProduct(maSanPham);
            reviews.OnBackToStore += () => NavigateToStore();
            SwitchView(reviews);
            lblStatusText.Text = "Đang xem: Đánh giá & Nhận xét sản phẩm (Nhóm 1 - Lê Nguyễn Minh Trí)";
        }

        public void NavigateToCart()
        {
            CartControl cart = new CartControl();
            cart.OnNavigateToLibrary += () => NavigateToLibrary();
            cart.OnNavigateToWallet += () => NavigateToWallet();
            SwitchView(cart);
            lblStatusText.Text = "Đang xem: Giỏ hàng mua sắm & Thanh toán (Nhóm 2 - Nguyễn Quốc Trung)";
        }

        public void NavigateToWallet()
        {
            WalletControl wallet = new WalletControl();
            SwitchView(wallet);
            lblStatusText.Text = "Đang xem: Ví tài khoản & Nạp tiền mô phỏng (Nhóm 2 - Nguyễn Quốc Trung)";
        }

        public void NavigateToVouchers()
        {
            VoucherControl voucher = new VoucherControl();
            voucher.OnNavigateToCart += () => NavigateToCart();
            SwitchView(voucher);
            lblStatusText.Text = "Đang xem: Kho mã ưu đãi & Voucher giảm giá (Nhóm 2 - Nguyễn Quốc Trung)";
        }

        public void NavigateToLibrary()
        {
            LibraryControl library = new LibraryControl();
            SwitchView(library);
            lblStatusText.Text = "Đang xem: Thư viện sản phẩm đã mua & Tải tệp (Nhóm 5 - Phạm Văn Đam)";
        }

        public void NavigateToOrderHistory()
        {
            OrderHistoryControl history = new OrderHistoryControl();
            SwitchView(history);
            lblStatusText.Text = "Đang xem: Lịch sử đơn hàng cá nhân (Nhóm 5 - Phạm Văn Đam)";
        }

        public void NavigateToOrderManage()
        {
            OrderManageControl orders = new OrderManageControl();
            SwitchView(orders);
            lblStatusText.Text = "Đang xem: Quản lý và duyệt đơn hàng toàn hệ thống (Nhóm 5 - Phạm Văn Đam)";
        }

        public void NavigateToProductManage()
        {
            ProductManageControl prod = new ProductManageControl();
            SwitchView(prod);
            lblStatusText.Text = "Đang xem: Quản trị kho hàng hóa & Tệp số (Nhóm 3 - Phạm Trung Hiếu)";
        }

        public void NavigateToCategoryManage()
        {
            CategoryManageControl cat = new CategoryManageControl();
            SwitchView(cat);
            lblStatusText.Text = "Đang xem: Quản lý danh mục thể loại (Nhóm 3 - Phạm Trung Hiếu)";
        }

        public void NavigateToProfile()
        {
            ProfileControl profile = new ProfileControl();
            SwitchView(profile);
            lblStatusText.Text = "Đang xem: Hồ sơ tài khoản & Đổi mật khẩu (Nhóm 3 - Phạm Trung Hiếu)";
        }

        public void NavigateToDashboard()
        {
            DashboardControl dash = new DashboardControl();
            SwitchView(dash);
            lblStatusText.Text = "Đang xem: Báo cáo chỉ số kinh doanh Dashboard (Nhóm 4 - Trần Quốc Thuận)";
        }

        public void NavigateToUserManage()
        {
            UserManageControl userMgmt = new UserManageControl();
            SwitchView(userMgmt);
            lblStatusText.Text = "Đang xem: Quản lý danh sách người dùng (Nhóm 4 - Trần Quốc Thuận)";
        }

        public void NavigateToLogin()
        {
            LoginControl login = new LoginControl();
            login.OnLoginSuccess += () => NavigateToStore();
            login.OnNavigateToRegister += () => NavigateToRegister();
            SwitchView(login);
            lblStatusText.Text = "Đang xem: Đăng nhập tài khoản (Nhóm 4 - Trần Quốc Thuận)";
        }

        public void NavigateToRegister()
        {
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
