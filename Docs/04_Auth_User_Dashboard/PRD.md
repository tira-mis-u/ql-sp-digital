# PRD: PHÂN HỆ XÁC THỰC, QUẢN TRỊ NGƯỜI DÙNG & DASHBOARD
**Nhiệm vụ:** STT 4  
**Thành viên phụ trách:** Trần Quốc Thuận (Nhóm 4)  
**Màn hình phụ trách:**  
- **UI 1:** Đăng nhập & Đăng ký ([`../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/LoginControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/LoginControl.cs), [`../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/RegisterControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/RegisterControl.cs))
- **UI 12:** Quản lý người dùng ([`../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/UserManageControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/UserManageControl.cs))
- **UI 9:** Dashboard thống kê tổng quan ([`../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/DashboardControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/DashboardControl.cs))
**Mã nguồn nghiệp vụ, truy vấn & tiện ích:**
- BLL Service: [`../../qlspdigital/qlspdigital/BLL/Nhom4_SystemAdmin/AuthService.cs`](../../qlspdigital/qlspdigital/BLL/Nhom4_SystemAdmin/AuthService.cs)
- DAL Repository: [`../../qlspdigital/qlspdigital/DAL/Nhom4_SystemAdmin/AuthRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom4_SystemAdmin/AuthRepository.cs), [`../../qlspdigital/qlspdigital/DAL/Nhom4_SystemAdmin/NguoiDungRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom4_SystemAdmin/NguoiDungRepository.cs)
- Phiên tĩnh: [`../../qlspdigital/qlspdigital/Shared/UserSession.cs`](../../qlspdigital/qlspdigital/Shared/UserSession.cs)

---

## 1. Mục tiêu phân hệ
Quản lý vòng đời xác thực của người dùng (Đăng ký tài khoản mới, Đăng nhập, kiểm tra tài khoản khóa), phân quyền hiển thị (QuanTriVien / KhachHang), cho phép Quản trị viên quản lý danh sách tài khoản (Khóa/Mở tài khoản) và theo dõi các chỉ số thống kê kinh doanh cốt lõi (Doanh thu, Đơn hàng, Thành viên).

---

## 2. Người dùng mục tiêu & Vai trò
- **Khách truy cập:** Đăng ký tài khoản mới (`RegisterControl`), Đăng nhập vào hệ thống (`LoginControl`).
- **Quản trị viên (Admin):**
  - Xem danh sách người dùng, Khóa hoặc Mở khóa tài khoản người dùng (`UserManageControl`).
  - Xem Dashboard thống kê các chỉ số kinh doanh tổng quan (`DashboardControl`).

---

## 3. Đặc tả yêu cầu chức năng (Functional Requirements)

### 3.1. UI 1 - Đăng nhập & Đăng ký (`Auth`)
1. **Đăng nhập (`LoginControl`):**
   - Nhập Username và Password -> Bấm "Đăng nhập".
   - Kiểm tra thông tin trong CSDL: Nếu sai thông báo lỗi; nếu tài khoản bị khóa (`TrangThai = 0`) thì báo "Tài khoản của bạn đã bị khóa"; nếu hợp lệ thì lưu session và chuyển về màn hình chính (Store hoặc Dashboard).
   - Nút chuyển nhanh sang form "Chưa có tài khoản? Đăng ký ngay".
2. **Đăng ký (`RegisterControl` - CRUD Create 1 cho BTL):**
   - Form nhập: Username, Mật khẩu, Xác nhận mật khẩu, Họ tên, Email, Số điện thoại.
   - Kiểm tra Username/Email không trùng lặp -> Thực hiện `INSERT INTO NguoiDung`.
   - Thông báo đăng ký thành công và tự động chuyển về form Đăng nhập.

### 3.2. UI 12 - Quản lý người dùng (`UserManageControl`)
1. **Hiển thị danh sách tài khoản (CRUD Read 2 cho BTL):**
   - Bảng DataGridView: Mã người dùng (`MaNguoiDung`), Tên đăng nhập (`TenDangNhap`), Họ tên (`HoTen`), Email, Vai trò (`VaiTro`), Trạng thái (`TrangThai`), Ngày tạo.
   - Ô tìm kiếm theo Username hoặc Email.
2. **Khóa / Mở khóa tài khoản (CRUD Update 2 cho BTL):**
   - Chọn một người dùng trong bảng.
   - Nút **"Khóa tài khoản"** (`UPDATE NguoiDung SET TrangThai = 0`).
   - Nút **"Mở khóa tài khoản"** (`UPDATE NguoiDung SET TrangThai = 1`).
   - Cảnh báo: Không cho phép tự khóa tài khoản Admin đang đăng nhập.

### 3.3. UI 9 - Admin Dashboard (`DashboardControl`)
1. **Các thẻ chỉ số thống kê cốt lõi (SQL Aggregations):**
   - **Tổng doanh thu:** Tính bằng `SUM(TongThanhToan)` từ bảng `DonHang` (`TrangThaiDonHang = N'Hoàn thành'`).
   - **Tổng số đơn hàng thành công:** Tính bằng `COUNT(1)` từ bảng `DonHang`.
   - **Tổng số khách hàng:** Tính bằng `COUNT(1)` từ bảng `NguoiDung` (`VaiTro = N'KhachHang'`).
   - **Tổng sản phẩm đang bán:** Tính bằng `COUNT(1)` từ bảng `SanPham` (`DangKinhDoanh = 1`).
2. **Danh sách gần đây (Đơn giản hóa cho BTL):**
   - DataGridView hiển thị 5 đơn hàng mới nhất và 5 sản phẩm bán chạy nhất.
   - Nút "Làm mới dữ liệu thống kê" để cập nhật số liệu mới nhất.

---

## 4. Quy tắc nghiệp vụ (Business Rules)
1. **Xác thực đăng nhập:** Kiểm tra chính xác Tên đăng nhập và Mật khẩu, đồng thời kiểm tra trạng thái hoạt động của tài khoản (`TrangThai = 1`).
2. **Phân quyền truy cập:** Nếu người dùng đăng nhập với quyền `KhachHang`, ẩn hoặc vô hiệu hóa các nút `QuanTriVien` trên Sidebar của `MainForm`.
3. **Thống kê đơn giản:** Không đòi hỏi tích hợp thư viện biểu đồ phức tạp; chỉ cần các ô thẻ thống kê (Metric Cards) với con số to rõ ràng.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Auth/Register):** Đăng ký tài khoản mới thành công và đăng nhập đúng quyền.
- [x] **CRUD 2 (NguoiDung):** Xem danh sách và thao tác Khóa/Mở khóa tài khoản trên DataGridView thành công.
- [x] **Dashboard:** Hiển thị chính xác các số liệu thống kê từ câu lệnh `COUNT` và `SUM` SQL.

---

## 6. Ma trận Đặc tả Nghiệp vụ CRUD Chuyên sâu (CRUD Matrix & Defense Rationale)

| Thao tác | Tầng Giao diện (`Views/Nhom4_SystemAdmin`) | Tầng Nghiệp vụ (`BLL/Nhom4_SystemAdmin`) | Tầng Truy vấn (`DAL/Nhom4_SystemAdmin`) | Bản chất Lệnh SQL |
|---|---|---|---|---|
| **CREATE** | Màn hình `RegisterControl` -> Đăng ký người dùng mới | `AuthService.DangKy(...)` | `AuthRepository.DangKy(...)` | `INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, ...) VALUES (...)` |
| **READ** | `dgvUsers` trên `UserManageControl` (lọc vai trò, tìm kiếm) | `NguoiDungService.LayDanhSachNguoiDung()` | `NguoiDungRepository.LayDanhSachNguoiDung()` | `SELECT MaNguoiDung, TenDangNhap, HoTen, Email, VaiTro, SoDuVi, TrangThai FROM NguoiDung WHERE ...` |
| **UPDATE** | Nút `btnLock` / `btnUnlock` thay đổi trạng thái hoạt động | `NguoiDungService.KhoaTaiKhoan()`<br>`NguoiDungService.MoKhoaTaiKhoan()` | `NguoiDungRepository.CapNhatTrangThaiKhoa()` | `UPDATE NguoiDung SET TrangThai = @TrangThai WHERE MaNguoiDung = @MaNguoiDung` |
| **DELETE** | **Cơ chế Soft Delete:** Bấm `btnLock` để vô hiệu hóa tài khoản (Chặn xóa vật lý) | Ngăn chặn xóa cứng; có bẫy cấm Admin tự khóa chính mình | `CapNhatTrangThaiKhoa(id, false)` | `UPDATE NguoiDung SET TrangThai = 0 WHERE MaNguoiDung = @MaNguoiDung` |

> **Phòng thủ vấn đáp BTL:** Giải trình với Hội đồng tại sao Trần Quốc Thuận không dùng `DELETE FROM NguoiDung`: Trong hệ thống TMĐT thực tế, tài khoản liên kết chặt chẽ với đơn hàng, lịch sử nạp ví và bản quyền game. Xóa cứng sẽ vi phạm ràng buộc khóa ngoại (Foreign Key Constraint) và hủy hoại bằng chứng kiểm toán tài chính. Do đó, hệ thống bắt buộc áp dụng **Soft Delete (Khóa tài khoản)** để đình chỉ người dùng vi phạm mà vẫn giữ trọn vẹn toàn vẹn dữ liệu.


