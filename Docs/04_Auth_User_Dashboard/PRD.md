# PRD: PHÂN HỆ XÁC THỰC, QUẢN TRỊ NGƯỜI DÙNG & DASHBOARD
**Nhiệm vụ:** STT 4  
**Màn hình phụ trách:**  
- **UI 1:** Đăng nhập & Đăng ký tài khoản (`LoginControl`, `RegisterControl`)
- **UI 12:** Quản lý người dùng (`UserManageControl`)
- **UI 9:** Dashboard thống kê tổng quan (`DashboardControl`)

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
1. **Bảo mật đăng nhập:** Không lưu mật khẩu thô dạng plaintext; sử dụng mã hóa băm SHA-256 an toàn.
2. **Phân quyền truy cập:** Nếu người dùng đăng nhập với quyền `KhachHang`, ẩn hoặc vô hiệu hóa các nút `QuanTriVien` trên Sidebar của `MainForm`.
3. **Thống kê đơn giản:** Không đòi hỏi tích hợp thư viện biểu đồ phức tạp; chỉ cần các ô thẻ thống kê (Metric Cards) với con số to rõ ràng.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Auth/Register):** Đăng ký tài khoản mới thành công và đăng nhập đúng quyền.
- [x] **CRUD 2 (NguoiDung):** Xem danh sách và thao tác Khóa/Mở khóa tài khoản trên DataGridView thành công.
- [x] **Dashboard:** Hiển thị chính xác các số liệu thống kê từ câu lệnh `COUNT` và `SUM` SQL.

