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
* **Nơi gọi (Caller):** Người dùng click menu Giỏ hàng hoặc bấm nút "Đi tới Giỏ hàng" từ `VoucherControl`.
* **Hàm thực thi tiếp theo:** `NavigateToCart(string? initialVoucherCode = null)`
* **Rào chắn phân quyền (RBAC Guard):**
  * Gọi `EnsureLoggedIn("Giỏ hàng của tôi")`. Nếu `UserSession.IsLoggedIn == false`, hiện Popup hỏi người dùng có muốn chuyển sang màn hình Đăng nhập không. Nếu chọn "Yes" $\to$ chuyển hướng `NavigateToLogin()`, dừng luồng mở giỏ hàng.
* **Quy trình hoạt động:**
  1. `SetActiveMenu(btnMenuCart)`.
  2. Khởi tạo `CartControl cart = new CartControl()`.
  3. Đăng ký các sự kiện callback từ giỏ:
     * `cart.OnNavigateToLibrary += () => NavigateToLibrary();` (sau khi thanh toán thành công).
     * `cart.OnNavigateToWallet += () => NavigateToWallet();` (khi số dư không đủ).
     * `cart.OnNavigateToVouchers += () => NavigateToVouchers();` (khi muốn tìm voucher).
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
private bool EnsureAdmin(string tenChucNang)
{
    if (!UserSession.IsLoggedIn) { EnsureLoggedIn(tenChucNang); return false; }
    if (!UserSession.IsAdmin)
    {
        MessageBox.Show($"Tài khoản không có quyền Quản trị viên để truy cập '{tenChucNang}'!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        NavigateToStore();
        return false;
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
    $\to$ Hiển thị hộp thoại: *"Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?"*.
    $\to$ Nếu chọn Yes: Gọi `new AuthService().DangXuat()` $\implies$ kích hoạt `UserSession.Logout()`.
    $\to$ Sự kiện `UserSession.SessionChanged` tự động kích hoạt `UpdateUserInterfaceSession()`.
    $\to$ Chuyển về màn hình `NavigateToStore()`.

#### 15. Nhãn `lblBalance` (Hiển thị số dư ví Header)
* **Hàm bắt sự kiện:** `LblBalance_Click(object? sender, EventArgs e)`
* **Hành vi:** Cho phép bấm trực tiếp vào số dư ví trên thanh tiêu đề để chuyển nhanh đến màn hình `NavigateToWallet()`.

---

## 3. TRẠM THU PHÁT SỰ KIỆN PHẢN ỨNG TOÀN CỤC (GLOBAL EVENT LISTENERS)

Được khởi tạo tại `SubscribeEvents()` trong hàm khởi tạo của `MainForm`:

```csharp
private void SubscribeEvents()
{
    // 1. Khi trạng thái phiên (Đăng nhập / Đăng xuất / Đổi vai trò) thay đổi
    UserSession.SessionChanged += UpdateUserInterfaceSession;

    // 2. Khi số dư ví thay đổi ở bất kỳ màn hình nào (Nạp ví, Thanh toán giỏ, Hoàn tiền)
    UserSession.BalanceChanged += (bal) =>
    {
        lblBalance.Text = $"Ví: {FormatHelper.FormatCurrency(bal)}";
    };

    // 3. Khi giỏ hàng có thêm/bớt món hàng
    GioHangService.CartUpdated += UpdateCartBadge;
}
```

* **Hàm `UpdateUserInterfaceSession()`:**
  * Nếu là Khách: Tắt toàn bộ 5 nút Admin (`Visible = false`), đặt số dư = `0 ₫`, nút Header hiện "Đăng nhập".
  * Nếu là Khách hàng: Giữ ẩn nút Admin, hiển thị số dư thực tế, nút Header hiện "Đăng xuất" màu đỏ.
  * Nếu là Quản trị viên (`QuanTriVien`): Bật sáng toàn bộ 5 nút Admin (`Visible = true`), hiển thị nhãn "(Admin)".
* **Hàm `UpdateCartBadge()`:**
  * Gọi `_gioHangService.LayTongSoLuongItem(UserSession.CurrentUserId)`.
  * Nếu `count > 0`: Cập nhật text nút giỏ hàng `🛒 Giỏ hàng ({count})`.
  * Nếu `count == 0`: Hiển thị `🛒 Giỏ hàng`.
