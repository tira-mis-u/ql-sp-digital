# ĐẶC TẢ KHUNG SƯỜN CHÍNH & LUỒNG ĐIỀU HƯỚNG MAINFORM
**Màn hình:** `MainForm.cs` (Application Shell phong cách Steam)  
**Phạm vi:** Điều hướng toàn bộ 15 màn hình `UserControl`, quản trị phiên làm việc, phân quyền RBAC và trạm thu phát sự kiện phản ứng toàn cục.

---

## 1. DANH SÁCH NÚT BẤM & TƯƠNG TÁC TRÊN MAINFORM

```text
+----------------------------------------------------------------------------------------------------+
| [DPMS LOGO]  CỬA HÀNG | GIỎ HÀNG (2) | VÍ: 500.000 ₫ | THƯ VIỆN | ... | Xin chào: Admin [ĐĂNG XUẤT]|
+----------------------------------------------------------------------------------------------------+
| MENU ĐIỀU HƯỚNG TRÁI (Sidebar)         | KHU VỰC HIỂN THỊ CHÍNH (panelMainContent)                 |
| - Cửa hàng sản phẩm                    |                                                            |
| - Đánh giá & Nhận xét                  |                                                            |
| - Giỏ hàng cá nhân                     |       [ NẠP ĐỘNG TỪNG MÀN HÌNH USERCONTROL ]               |
| - Ví tài khoản                         |                                                            |
| - Kho mã giảm giá                      |                                                            |
| - Thư viện game sở hữu                 |                                                            |
| - Lịch sử mua hàng                     |                                                            |
| - Hồ sơ & Đổi mật khẩu                 |                                                            |
| --- KHU VỰC QUẢN TRỊ (ADMIN) ---       |                                                            |
| - Bảng điều khiển (Dashboard)          |                                                            |
| - Quản lý sản phẩm & tệp               |                                                            |
| - Quản lý danh mục                     |                                                            |
| - Quản lý đơn hàng                     |                                                            |
| - Quản lý người dùng                   |                                                            |
+----------------------------------------------------------------------------------------------------+
```

---

## 2. CHI TIẾT TỪNG NÚT BẤM, HÀM GỌI & BỘ PHÂN QUYỀN RBAC

### 2.1. Nhóm Nút Điều Hướng Khách Hàng (Customer Navigation)

#### 1. Nút `btnMenuStore` ("Cửa hàng sản phẩm")
* **Hàm UI bắt sự kiện:** `BtnMenuStore_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Người dùng click chuột vào nút menu trên Sidebar trái.
* **Hàm thực thi tiếp theo:** `NavigateToStore()`
* **Quy trình hoạt động:**
  1. Đổi màu active cho nút bấm: `SetActiveMenu(btnMenuStore)`.
  2. Khởi tạo `StoreViewControl store = new StoreViewControl()`.
  3. Đăng ký sự kiện xem chi tiết: `store.OnViewProductDetail += (spId) => NavigateToProductDetail(spId)`.
  4. Nạp UserControl vào khung: `SwitchView(store)`.
  5. Cập nhật thanh trạng thái: `lblStatusText.Text = "Đang xem: Cửa hàng sản phẩm số..."`.
* **Phân quyền:** Không yêu cầu đăng nhập (Công khai cho cả Khách vãng lai).

#### 2. Nút `btnMenuReviews` ("Đánh giá & Nhận xét")
* **Hàm UI bắt sự kiện:** `BtnMenuReviews_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Người dùng click menu Sidebar hoặc từ nút "Xem đánh giá" trong `ProductDetailControl`.
* **Hàm thực thi tiếp theo:** `NavigateToReviews(maSanPham = 1)`
* **Quy trình hoạt động:**
  1. `SetActiveMenu(btnMenuReviews)`.
  2. Khởi tạo `ProductReviewControl reviews = new ProductReviewControl()`.
  3. Gọi hàm `reviews.SetSelectedProduct(maSanPham)` để tải danh sách đánh giá của sản phẩm đó.
  4. `SwitchView(reviews)`.
* **Phân quyền:** Xem công khai. Chỉ khi bấm gửi đánh giá mới yêu cầu đăng nhập và quyền sở hữu.

#### 3. Nút `btnMenuCart` ("Giỏ hàng")
* **Hàm UI bắt sự kiện:** `BtnMenuCart_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Người dùng click menu Giỏ hàng hoặc từ Voucher nạp cử voucher sang Cart.
* **Hàm thực thi tiếp theo:** `NavigateToCart(string? initialVoucherCode = null)`
* **Rào chắn phân quyền (RBAC Guard):**
  * Gọi `EnsureLoggedIn("Giỏ hàng của tôi")`. Nếu `UserSession.IsLoggedIn == false`, hiện `MessageBox` mời đăng nhập rồi gọi `NavigateToLogin()`, dừng luồng mở giỏ hàng.
* **Quy trình hoạt động:**
  1. `SetActiveMenu(btnMenuCart)`.
  2. Khởi tạo `CartControl cart = new CartControl(); cart.MainForm = this;`.
  3. Nếu có `initialVoucherCode`: gọi `cart.SetInitialVoucherCode(code)` để tự động nạp mã voucher từ `VoucherControl`.
  4. `SwitchView(cart)`.


#### 4. Nút `btnMenuWallet` ("Ví tài khoản")
* **Hàm UI bắt sự kiện:** `BtnMenuWallet_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Click menu Sidebar hoặc click vào nhãn số dư Header (`lblBalance_Click`).
* **Hàm thực thi tiếp theo:** `NavigateToWallet()`
* **Rào chắn:** Gọi `EnsureLoggedIn("Ví tài khoản")`.
* **Quy trình hoạt động:**
  1. `SetActiveMenu(btnMenuWallet)`.
  2. Khởi tạo `WalletControl wallet = new WalletControl()`.
  3. `SwitchView(wallet)`.

#### 5. Nút `btnMenuVouchers` ("Kho mã giảm giá")
* **Hàm UI bắt sự kiện:** `BtnMenuVouchers_Click(object? sender, EventArgs e)`
* **Hàm thực thi tiếp theo:** `NavigateToVouchers()`
* **Rào chắn:** Gọi `EnsureLoggedIn("Kho mã ưu đãi & Voucher")`.
* **Quy trình hoạt động:**
  1. `SetActiveMenu(btnMenuVouchers)`.
  2. Khởi tạo `VoucherControl voucher = new VoucherControl()`.
  3. Đăng ký sự kiện: `voucher.OnNavigateToCartWithVoucher += (code) => NavigateToCart(code);`.
  4. `SwitchView(voucher)`.

#### 6. Nút `btnMenuLibrary` ("Thư viện game sở hữu")
* **Hàm UI bắt sự kiện:** `BtnMenuLibrary_Click(object? sender, EventArgs e)`
* **Hàm thực thi tiếp theo:** `NavigateToLibrary()`
* **Rào chắn:** Gọi `EnsureLoggedIn("Thư viện game sở hữu")`.
* **Quy trình hoạt động:** Nạp `LibraryControl` hiển thị kho bản quyền cá nhân.

#### 7. Nút `btnMenuOrderHistory` ("Lịch sử mua hàng")
* **Hàm UI bắt sự kiện:** `BtnMenuOrderHistory_Click(object? sender, EventArgs e)`
* **Hàm thực thi tiếp theo:** `NavigateToOrderHistory()`
* **Rào chắn:** Gọi `EnsureLoggedIn("Lịch sử mua hàng")`.
* **Quy trình hoạt động:** Nạp `OrderHistoryControl`.

#### 8. Nút `btnMenuProfile` ("Hồ sơ & Đổi mật khẩu")
* **Hàm UI bắt sự kiện:** `BtnMenuProfile_Click(object? sender, EventArgs e)`
* **Hàm thực thi tiếp theo:** `NavigateToProfile()`
* **Rào chắn:** Gọi `EnsureLoggedIn("Hồ sơ & Đổi mật khẩu")`.
* **Quy trình hoạt động:** Nạp `ProfileControl`.

---

### 2.2. Nhóm Nút Quản Trị Hệ Thống (Admin Navigation)

Tất cả các nút này đều có thuộc tính `Visible = false` mặc định và được bảo vệ nghiêm ngặt bằng hàm `EnsureAdmin(string tenChucNang)`.

```csharp
// Thực tế trong MainForm.cs: EnsureAdmin không redirect sang Store
private bool EnsureAdmin(string tenChucNang)
{
    if (!UserSession.IsAdmin)
    {
        MessageBox.Show($"Chức năng '{tenChucNang}' chỉ dành cho Quản trị viên!",
            "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false; // Chỉ từ chối, không redirect - màn hình hiện tại giữ nguyên
    }
    return true;
}
```


#### 9. Nút `btnMenuDashboard` ("Bảng điều khiển kinh doanh")
* **Hàm bắt sự kiện:** `BtnMenuDashboard_Click(object? sender, EventArgs e)` $\to$ gọi `NavigateToDashboard()`.
* **Rào chắn:** `EnsureAdmin("Báo cáo kinh doanh")`. Nạp `DashboardControl`.

#### 10. Nút `btnMenuProductManage` ("Quản lý sản phẩm & Tệp số")
* **Hàm bắt sự kiện:** `BtnMenuProductManage_Click(object? sender, EventArgs e)` $\to$ gọi `NavigateToProductManage()`.
* **Rào chắn:** `EnsureAdmin("Quản trị kho hàng hóa & Tệp số")`. Nạp `ProductManageControl`.

#### 11. Nút `btnMenuCategoryManage` ("Quản lý danh mục")
* **Hàm bắt sự kiện:** `BtnMenuCategoryManage_Click(object? sender, EventArgs e)` $\to$ gọi `NavigateToCategoryManage()`.
* **Rào chắn:** `EnsureAdmin("Quản lý danh mục thể loại")`. Nạp `CategoryManageControl`.

#### 12. Nút `btnMenuOrderManage` ("Duyệt & Quản lý đơn hàng")
* **Hàm bắt sự kiện:** `BtnMenuOrderManage_Click(object? sender, EventArgs e)` $\to$ gọi `NavigateToOrderManage()`.
* **Rào chắn:** `EnsureAdmin("Quản lý và duyệt đơn hàng")`. Nạp `OrderManageControl`.

#### 13. Nút `btnMenuUserManage` ("Quản lý tài khoản người dùng")
* **Hàm bắt sự kiện:** `BtnMenuUserManage_Click(object? sender, EventArgs e)` $\to$ gọi `NavigateToUserManage()`.
* **Rào chắn:** `EnsureAdmin("Quản lý tài khoản người dùng")`. Nạp `UserManageControl`.

---

### 2.3. Nhóm Nút & Nhãn Header Tương Tác

#### 14. Nút `btnHeaderAuth` ("Đăng nhập" / "Đăng xuất")
* **Hàm bắt sự kiện:** `BtnHeaderAuth_Click(object? sender, EventArgs e)`
* **Quy trình hoạt động:**
  * Nếu `UserSession.IsLoggedIn == false`:
    $\to$ Gọi `NavigateToLogin()` nạp màn hình Đăng nhập.
  * Nếu `UserSession.IsLoggedIn == true`:
    $\to$ Hiển thị hộp thoại: *"Bạn có muốn đăng xuất khỏi hệ thống?"*.
    $\to$ Nếu chọn Yes: Gọi trực tiếp `UserSession.Logout()` (reset tất cả fields về mặc định).
    $\to$ Gọi tường minh `UpdateUserInterfaceSession()` để ẩn menu Admin, reset số dư ví.
    $\to$ Chuyển về màn hình `NavigateToStore()`.

> **⚠️ Lưu ý:** Không gọi `AuthService.DangXuat()` — `UserSession.Logout()` được gọi trực tiếp từ UI. Không có `UserSession.SessionChanged` event — `UpdateUserInterfaceSession()` được gọi tường minh sau mỗi login/logout.

#### 15. Nhãn `lblBalance` (Hiển thị số dư ví Header)
* **Hàm bắt sự kiện:** `LblBalance_Click(object? sender, EventArgs e)`
* **Hành vi:** Cho phép bấm trực tiếp vào số dư ví trên thanh tiêu đề để chuyển nhanh đến màn hình `NavigateToWallet()`.

#### 16. Nút `btnThemeToggle` (Chuyển đổi giao diện Tối / Sáng)
* **Hàm bắt sự kiện:** `BtnThemeToggle_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Người dùng bấm nút biểu tượng góc trên bên phải Header (`panelHeaderRight`).
* **Hàm thực thi tiếp theo:**
  1. `ThemeHelper.ToggleTheme(this)`: Đảo ngược cờ `ThemeHelper.IsDarkMode`, lưu cấu hình vào `theme.cfg`, cập nhật màu viền/title bar Windows qua DWM API (`DwmSetWindowAttribute`), và duyệt đệ quy toàn bộ cây giao diện (`ApplyThemeToControlTree`) để đổi màu nền, viền, phông chữ.
  2. `UpdateThemeToggleVisuals()`: Cập nhật nhãn nút (`☀️ Sáng` khi đang Dark Mode, hoặc `🌙 Tối` khi đang Light Mode) và màu sắc nút tương ứng.
  3. `UpdateUserInterfaceSession()`: Đồng bộ lại màu sắc các thành phần session (số dư, trạng thái đăng nhập).
  4. `SetActiveMenu(_activeMenuButton)`: Duy trì highlight menu đang chọn theo bảng màu theme mới.

---

## 3. CƠ CHẾCẬP NHẬT GIAO DIỆN THEO PHIÊN (SESSION-DRIVEN UI REFRESH)

> **Quan trọng:** Trong phiên bản hiện tại, `UserSession` **không có** static events (`SessionChanged`, `BalanceChanged`). `GioHangService.CartUpdated` static event **không tồn tại**. Không có `SubscribeEvents()` method trong `MainForm`. Mọi cập nhật UI được kích hoạt bằng cách gọi **tường minh (explicit call)** sau mỗi hành động có tác động phó.

### Các điểm kích hoạt `UpdateUserInterfaceSession()` trong cây gọi hàm:

| Trigger | Nơi gọi | Mô tả |
|---|---|---|
| Đăng nhập thành công | `LoginControl.cs` gọi `mainForm.UpdateUserInterfaceSession()` | Hiện menu Admin nếu là `QuanTriVien`, cập nhật số dư ví |
| Đăng xuất | `MainForm.BtnHeaderAuth_Click` gọi sau `UserSession.Logout()` | Reset số dư về `0 ₫`, ẩn menu Admin, đổi nút thành "Đăng nhập" |
| Nạp tiền ví | `WalletControl.cs` gọi `mainForm.UpdateUserInterfaceSession()` sau nạp | Cập nhật `lblBalance` trên Header |
| Thanh toán giỏ hàng | `CartControl.cs` gọi `mainForm.UpdateUserInterfaceSession()` sau checkout | Trừ số dư ví, làm mới badge giỏ hàng |
| Chuyển đổi theme | `MainForm.BtnThemeToggle_Click` | Giữ header đồng bộ màu sắc theo theme mới |
| Khở động | `MainForm_Load` | Khởi tạo trạng thái ban đầu (guest mode) |

### Hàm `UpdateUserInterfaceSession()` — Logic chi tiết:
```csharp
public void UpdateUserInterfaceSession()
{
    bool isAdmin = UserSession.IsLoggedIn && UserSession.IsAdmin;

    // 1. Cập nhật lời chào và số dư ví trên Header
    lblUserGreeting.Text = UserSession.IsLoggedIn
        ? $"{UserSession.CurrentFullName} ({(UserSession.IsAdmin ? "Admin" : "Khách Hàng")})"
        : "Khách (Chưa đăng nhập)";
    lblBalance.Text = UserSession.IsLoggedIn
        ? $"Ví: {FormatHelper.FormatCurrency(UserSession.CurrentBalance)}"
        : "Ví: 0 ₫";
    btnHeaderAuth.Text = UserSession.IsLoggedIn ? "Đăng xuất" : "Đăng nhập";

    // 2. Phân quyền RBAC: Chỉ hiện menu Admin cho QuanTriVien
    lblAdminSection.Visible      = isAdmin;
    btnMenuDashboard.Visible     = isAdmin;
    btnMenuProductManage.Visible = isAdmin;
    btnMenuCategoryManage.Visible= isAdmin;
    btnMenuOrderManage.Visible   = isAdmin;
    btnMenuUserManage.Visible    = isAdmin;

    // 3. Cập nhật badge số món trong nút Giỏ hàng
    UpdateCartBadge();

    // 4. Nếu đang đứng ở Store, reload lại lưới (thả nhãn "Đã sở hữu" khi đắng nhập mới)
    if (panelMainContent.Controls.Count > 0 &&
        panelMainContent.Controls[0] is StoreViewControl activeStore)
    {
        activeStore.LoadProducts();
    }
}
```

### Hàm `UpdateCartBadge()`:
```csharp
public void UpdateCartBadge()
{
    int count = 0;
    if (UserSession.IsLoggedIn)
    {
        GioHangService cartService = new GioHangService();
        count = cartService.LayTongSoLuongItem(UserSession.CurrentUserId);
    }
    btnMenuCart.Text = $"🛒 Giỏ hàng ({count})";
}
```
> Gọi `new GioHangService()` (không dùng singleton) và đếm trực tiếp từ CSDL mỗi khi refresh, không dùng cached event.

