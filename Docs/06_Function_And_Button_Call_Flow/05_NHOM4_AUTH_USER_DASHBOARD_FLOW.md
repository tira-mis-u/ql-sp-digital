# ĐẶC TẢ LUỒNG NÚT BẤM & HÀM XỬ LÝ - NHÓM 4: BẢO MẬT, TÀI KHOẢN & DASHBOARD
**Thành viên phụ trách:** Trần Quốc Thuận  
**Màn hình quản lý:**
1. UI 1: `LoginControl` (Xác thực đăng nhập tài khoản)
2. UI 1.1: `RegisterControl` (Đăng ký tài khoản thành viên mới)
3. UI 12: `UserManageControl` (Quản trị người dùng & Khóa tài khoản)
4. UI 9: `DashboardControl` (Trung tâm phân tích KPI & Top bán chạy)

---

## 1. MÀN HÌNH UI 1 & UI 1.1: `LoginControl` & `RegisterControl`

### 1.1. Sơ đồ luồng Đăng nhập & Khởi tạo Phiên làm việc (Session Initialization)

```text
[ Khách hàng bấm nút "ĐĂNG NHẬP" trên LoginControl ]
                         |
                         v
               LoginControl.BtnLogin_Click()
                         |
                         | Lấy username, password từ textbox
                         v
         AuthService.DangNhap(username, password, out userDto, out msg)
                         |
                         v
         AuthRepository.DangNhap(username, password, out userDto, out msg)
                         |
                          | 1. SELECT thông tin tài khoản:
                          |    SELECT * FROM NguoiDung WHERE TenDangNhap = @User;
                          | 2. Đối chiếu mật khẩu nhập với mật khẩu lưu trữ trong CSDL
                          | 3. Kiểm tra trạng thái tài khoản:
                          |    -> Nếu TrangThai == 0: Báo lỗi 'Tài khoản của bạn đã bị khóa!'
                          v
                         v
         [ Đăng nhập thành công ]
                         |
                         v
         UserSession.Login(id, username, fullName, email, role, balance)
                         |
                         v
         GỊi TƯỜNG MINH (không có static event):
         mainForm.UpdateUserInterfaceSession()
                         |
                         v
         MainForm.UpdateUserInterfaceSession()
         - Đổi lời chào: "Xin chào, {FullName} ({Role})"
         - Đổi số dư ví: "Ví: {SoDuVi}"
         - Nếu là QuanTriVien: Hiển thị ngay 5 nút Menu Quản trị!
         - Nút Header đổi sang "ĐĂNG XUẤT" màu đỏ
                         |
                         v
         Chuyển hướng người dùng về màn hình StoreViewControl
```

---

### 1.2. Chi tiết từng nút bấm trên UI 1 & UI 1.1

#### 1. Nút `btnLogin` ("Đăng nhập")
* **Hàm UI bắt sự kiện:** `BtnLogin_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Người dùng bấm nút hoặc nhấn phím **Enter** trên ô mật khẩu.
* **Hàm BLL được gọi:** `AuthService.DangNhap(string tenDangNhap, string matKhau, out string thongBao)`
* **Hàm DAL được gọi:** `AuthRepository.DangNhap(...)`
* **Cơ chế cập nhật:** Sau khi gọi `UserSession.Login(...)`, `LoginControl` gọi `mainForm.UpdateUserInterfaceSession()` tường minh để đồng bộ menu RBAC và số dư ví, sau đó chuyển hướng về `NavigateToStore()`.

#### 2. Nút `btnDemoAdmin` & `btnDemoCustomer` (Điền nhanh tài khoản Demo)
* **Hàm UI bắt sự kiện:**
  * `BtnDemoAdmin_Click` $\to$ Điền sẵn: `txtUsername.Text = "admin"`, `txtPassword.Text = "123456"`.
  * `BtnDemoCustomer_Click` $\to$ Điền sẵn: `txtUsername.Text = "hieu_pham"`, `txtPassword.Text = "123456"`.
* **Mục đích:** Phục vụ chấm thi và bảo vệ đồ án trực tiếp trước hội đồng, giúp chuyển đổi nhanh vai trò mà không cần gõ phím.

#### 3. Nút `btnGoToRegister` & `btnBackToLogin`
* **Hành vi phát sự kiện:**
  * `btnGoToRegister` kích hoạt `OnNavigateToRegister?.Invoke()` $\to$ MainForm nạp `RegisterControl`.
  * `btnBackToLogin` kích hoạt `OnNavigateToLogin?.Invoke()` $\to$ MainForm nạp `LoginControl`.

#### 4. Nút `btnRegister` ("Đăng ký tài khoản")
* **Hàm UI bắt sự kiện:** `BtnRegister_Click(object? sender, EventArgs e)`
* **Xác thực hợp lệ:**
  * `tenDangNhap.Length >= 3 && tenDangNhap.Length <= 50`, không chứa khoảng trắng.
  * `matKhau.Length >= 6` và trùng khớp với `xacNhanMatKhau`.
  * `email` chứa ký tự `@`.
* **Hàm BLL:** `AuthService.DangKy(...)`.
* **Hàm DAL:** `AuthRepository.DangKy(...)`.
* **SQL thực thi:**
  ```sql
  -- Kiểm tra trùng lặp
  IF EXISTS (SELECT 1 FROM NguoiDung WHERE TenDangNhap = @User OR Email = @Email)
      RAISERROR(N'Tên đăng nhập hoặc Email đã tồn tại!', 16, 1);
  ELSE
      INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, VaiTro, SoDuVi, TrangThai)
      VALUES (@User, @MatKhau, @HoTen, @Email, @Phone, N'KhachHang', 0, 1);
  ```

---

## 2. MÀN HÌNH UI 12: `UserManageControl`

### 2.1. Quy tắc Phòng vệ Tối cao Chống Tự khóa Admin (Anti-Lockout Rule)

```csharp
public bool CapNhatTrangThaiKhoa(int maNguoiDung, bool trangThai, out string thongBao)
{
    // Kiểm tra quy tắc an toàn: Không cho phép Admin tự khóa tài khoản của chính mình
    if (UserSession.CurrentUserId == maNguoiDung && !trangThai)
    {
        thongBao = "Bạn không thể tự khóa tài khoản của chính mình!";
        return false;
    }
    // Thực thi UPDATE TrangThai...
}
```

#### 5. Nút `btnLock` ("Khóa tài khoản")
* **Hàm UI bắt sự kiện:** `BtnLock_Click(object? sender, EventArgs e)` $\to$ gọi `ChangeStatus(false)`.
* **Hàm BLL:** `NguoiDungService.KhoaTaiKhoan(int maNguoiDung, out string msg)`.
* **Hàm DAL:** `NguoiDungRepository.CapNhatTrangThaiKhoa(maNguoiDung, false, out msg)`.
* **SQL thực thi:** `UPDATE NguoiDung SET TrangThai = 0 WHERE MaNguoiDung = @MaNguoiDung;`.

#### 6. Nút `btnUnlock` ("Mở khóa tài khoản")
* **Hàm UI bắt sự kiện:** `BtnUnlock_Click(object? sender, EventArgs e)` $\to$ gọi `ChangeStatus(true)`.
* **SQL thực thi:** `UPDATE NguoiDung SET TrangThai = 1 WHERE MaNguoiDung = @MaNguoiDung;`.

#### 7. Nút `btnSearch` & `btnRefresh`
* **Hành vi:** Lọc danh sách theo vai trò (`cboRoleFilter`) và từ khóa tài khoản (`txtSearchUser`), nạp lại `dgvUsers`.

---

## 3. MÀN HÌNH UI 9: `DashboardControl`

#### 8. Nút `btnRefresh` ("Làm mới dữ liệu kinh doanh")
* **Hàm UI bắt sự kiện:** `BtnRefresh_Click(object? sender, EventArgs e)` $\to$ gọi `LoadDashboardData()`.
* **Hàm BLL được gọi:**
  1. `DashboardService.LayChiSoTongHop()` $\to$ trả về `DashboardStatsDTO`.
  2. `DashboardService.LayTop5BanChay()` $\to$ trả về danh sách `TopSanPhamDTO`.
* **Hàm DAL được gọi:** `DashboardRepository.LayChiSoTongHop()` và `LayTop5BanChay()`.
* **Các câu truy vấn SQL thống kê (KPI Aggregations):**
  ```sql
  -- 1. Tổng doanh thu (Chỉ tính đơn Hoàn thành)
  SELECT ISNULL(SUM(TongThanhToan), 0) FROM DonHang WHERE TrangThaiDonHang = N'Hoàn thành';

  -- 2. Tổng đơn hàng thành công
  SELECT COUNT(1) FROM DonHang WHERE TrangThaiDonHang = N'Hoàn thành';

  -- 3. Tổng số khách hàng
  SELECT COUNT(1) FROM NguoiDung WHERE VaiTro = 'KhachHang';

  -- 4. Tổng sản phẩm đang mở bán
  SELECT COUNT(1) FROM SanPham WHERE DangKinhDoanh = 1;

  -- 5. Top 5 sản phẩm bán chạy nhất
  SELECT TOP 5 sp.MaSanPham, sp.TenSanPham,
         SUM(ct.SoLuong) AS SoLuongDaBan,
         SUM(ct.SoLuong * ct.DonGia) AS DoanhThu
  FROM ChiTietDonHang ct
  INNER JOIN DonHang dh ON ct.MaDonHang = dh.MaDonHang
  INNER JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
  WHERE dh.TrangThaiDonHang = N'Hoàn thành'
  GROUP BY sp.MaSanPham, sp.TenSanPham
  ORDER BY SoLuongDaBan DESC;
  ```
* **Phản hồi UI:** Gán số liệu vào 4 Card KPI và vẽ lại bảng xếp hạng Top 5 (🥇 Top 1, 🥈 Top 2, 🥉 Top 3).
