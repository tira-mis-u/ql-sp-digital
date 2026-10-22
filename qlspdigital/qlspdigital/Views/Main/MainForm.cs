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
        private Button? _activeMenuButton = null;
        public Button? ActiveMenuButton => _activeMenuButton;

        public MainForm()
        {
            InitializeComponent();
        }


        private void SetActiveMenu(Button? btn)
        {
            if (_activeMenuButton != null && _activeMenuButton != btn)
            {
                _activeMenuButton.BackColor = ThemeHelper.IsDarkMode ? Color.FromArgb(18, 25, 35) : Color.FromArgb(248, 249, 251);
                _activeMenuButton.ForeColor = ThemeHelper.IsDarkMode ? Color.FromArgb(199, 213, 224) : Color.FromArgb(71, 85, 105);
                _activeMenuButton.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            }

            _activeMenuButton = btn;

            if (_activeMenuButton != null)
            {
                _activeMenuButton.BackColor = ThemeHelper.IsDarkMode ? Color.FromArgb(49, 98, 130) : Color.FromArgb(14, 116, 144);
                _activeMenuButton.ForeColor = Color.White;
                _activeMenuButton.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            }
        }

        private void BtnMenu_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn != _activeMenuButton)
            {
                btn.BackColor = ThemeHelper.IsDarkMode ? Color.FromArgb(42, 71, 94) : Color.FromArgb(241, 245, 249);
                btn.ForeColor = ThemeHelper.IsDarkMode ? Color.White : Color.FromArgb(15, 23, 42);
            }
        }

        private void BtnMenu_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn != _activeMenuButton)
            {
                btn.BackColor = ThemeHelper.IsDarkMode ? Color.FromArgb(18, 25, 35) : Color.FromArgb(248, 249, 251);
                btn.ForeColor = ThemeHelper.IsDarkMode ? Color.FromArgb(199, 213, 224) : Color.FromArgb(71, 85, 105);
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

        public void UpdateCartBadge()
        {
            int count = 0;
            if (UserSession.IsLoggedIn)
            {
                GioHangService cartService = new GioHangService();
                count = cartService.LayTongSoLuongItem(UserSession.CurrentUserId);
            }
            btnMenuCart.Text = $"🛍️ Giỏ hàng ({count})";
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            UpdateThemeToggleVisuals();

            // Kiểm tra kết nối CSDL
            if (DatabaseHelper.TestConnection(out string msg))
            {
                lblDbStatus.Text = "🟢 CSDL: DPMS_Db (Kết nối thành công)";
                lblDbStatus.ForeColor = ThemeHelper.IsDarkMode ? Color.FromArgb(164, 208, 7) : Color.FromArgb(22, 163, 74);
            }
            else
            {
                lblDbStatus.Text = "🔴 CSDL: Lỗi kết nối DPMS_Db";
                lblDbStatus.ForeColor = ThemeHelper.IsDarkMode ? Color.FromArgb(231, 76, 60) : Color.FromArgb(220, 38, 38);
                MessageBox.Show($"Không thể kết nối CSDL SQL Server!\n{msg}\n\nVui lòng đảm bảo SQL Server đang chạy và đã thực thi file script 'Database/DPMS_Database_Master.sql'.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Tự động mở Trang chủ Cửa hàng khi khởi động
            NavigateToStore();
            UpdateUserInterfaceSession();
        }

        private void BtnThemeToggle_Click(object? sender, EventArgs e)
        {
            ThemeHelper.ToggleTheme(this);
            UpdateThemeToggleVisuals();
            UpdateUserInterfaceSession();
            SetActiveMenu(_activeMenuButton);
        }

        public void UpdateThemeToggleVisuals()
        {
            if (btnThemeToggle == null) return;
            if (ThemeHelper.IsDarkMode)
            {
                btnThemeToggle.Text = "☀️ Sáng";
                btnThemeToggle.BackColor = Color.FromArgb(27, 40, 56);
                btnThemeToggle.ForeColor = Color.FromArgb(102, 192, 244);
                btnThemeToggle.FlatAppearance.BorderColor = Color.FromArgb(42, 71, 94);
            }
            else
            {
                btnThemeToggle.Text = "🌙 Tối";
                btnThemeToggle.BackColor = Color.FromArgb(241, 245, 249);
                btnThemeToggle.ForeColor = Color.FromArgb(15, 23, 42);
                btnThemeToggle.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            }
        }

        public void UpdateUserInterfaceSession()
        {
            bool isAdmin = UserSession.IsLoggedIn && UserSession.IsAdmin;

            if (UserSession.IsLoggedIn)
            {
                lblUserGreeting.Text = $"{UserSession.CurrentFullName} ({(UserSession.IsAdmin ? "Admin" : "Khách Hàng")})";
                lblBalance.Text = $"Ví: {FormatHelper.FormatCurrency(UserSession.CurrentBalance)}";
                btnHeaderAuth.Text = "Đăng xuất";
                btnHeaderAuth.BackColor = ThemeHelper.GetButtonColor(Color.FromArgb(180, 40, 40));
            }
            else
            {
                lblUserGreeting.Text = "Khách (Chưa đăng nhập)";
                lblBalance.Text = "Ví: 0 ₫";
                btnHeaderAuth.Text = "Đăng nhập";
                btnHeaderAuth.BackColor = ThemeHelper.GetButtonColor(Color.FromArgb(49, 98, 130));
            }

            // Phân quyền chuẩn RBAC: Chỉ tài khoản Quản trị viên (QuanTriVien) mới hiển thị menu Admin
            lblAdminSection.Visible = isAdmin;
            btnMenuDashboard.Visible = isAdmin;
            btnMenuProductManage.Visible = isAdmin;
            btnMenuCategoryManage.Visible = isAdmin;
            btnMenuOrderManage.Visible = isAdmin;
            btnMenuUserManage.Visible = isAdmin;

            UpdateCartBadge();

            // Nếu đang xem Store, reload lại để ẩn/hiện sản phẩm đã mua theo session mới
            if (panelMainContent.Controls.Count > 0 &&
                panelMainContent.Controls[0] is qlspdigital.Views.Nhom1_Store.StoreViewControl activeStore)
            {
                activeStore.LoadProducts();
            }
        }

        // ==========================================
        // CƠ CHẾ BẢO VỆ PHÂN QUYỀN (RBAC GUARDS)
        // ==========================================

        // Kiểm tra quyền đăng nhập cho các tính năng cá nhân
        private bool EnsureLoggedIn(string tenChucNang)
        {
            if (!UserSession.IsLoggedIn)
            {
                DialogResult result = MessageBox.Show(
                    $"Bạn cần đăng nhập tài khoản để truy cập '{tenChucNang}'.\nBạn có muốn chuyển sang màn hình Đăng nhập ngay bây giờ không?",
                    "Yêu cầu đăng nhập",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes) NavigateToLogin();
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
            store.MainForm = this;
            SwitchView(store);
            lblStatusText.Text = "Đang xem: Cửa hàng sản phẩm số (Nhóm 1 - Lê Nguyễn Minh Trí)";
        }

        public void NavigateToProductDetail(int maSanPham)
        {
            SetActiveMenu(btnMenuStore);
            ProductDetailControl detail = new ProductDetailControl();
            detail.MainForm = this;
            detail.LoadProductDetail(maSanPham);
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
            cart.MainForm = this;
            if (initialVoucherCode != null && initialVoucherCode.Trim() != "")
            {
                cart.SetInitialVoucherCode(initialVoucherCode);
            }
            SwitchView(cart);
            lblStatusText.Text = "Đang xem: Giỏ hàng mua sắm & Thanh toán (Nhóm 2 - Nguyễn Quốc Trung)";
        }

        public void NavigateToWallet()
        {
            if (!EnsureLoggedIn("Ví tài khoản")) return;

            SetActiveMenu(btnMenuWallet);
            WalletControl wallet = new WalletControl();
            wallet.MainForm = this;
            SwitchView(wallet);
            lblStatusText.Text = "Đang xem: Ví tài khoản & Nạp tiền mô phỏng (Nhóm 2 - Nguyễn Quốc Trung)";
        }

        public void NavigateToVouchers()
        {
            if (!EnsureLoggedIn("Kho mã ưu đãi & Voucher")) return;

            SetActiveMenu(btnMenuVouchers);
            VoucherControl voucher = new VoucherControl();
            voucher.MainForm = this;
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
            login.MainForm = this;
            SwitchView(login);
            lblStatusText.Text = "Đang xem: Đăng nhập tài khoản (Nhóm 4 - Trần Quốc Thuận)";
        }

        public void NavigateToRegister()
        {
            SetActiveMenu(null);
            RegisterControl register = new RegisterControl();
            register.MainForm = this;
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
                    UpdateUserInterfaceSession();
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
