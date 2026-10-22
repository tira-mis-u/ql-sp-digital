# PRD: PHÂN HỆ QUẢN TRỊ SẢN PHẨM, DANH MỤC & HỒ SƠ CÁ NHÂN
**Nhiệm vụ:** STT 3  
**Thành viên phụ trách:** Phạm Trung Hiếu (Nhóm 3)  
**Màn hình phụ trách:**  
- **UI 10:** Quản lý sản phẩm & Tệp kỹ thuật số ([`../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/ProductManageControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/ProductManageControl.cs))
- **UI 11:** Quản lý danh mục thể loại ([`../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/CategoryManageControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/CategoryManageControl.cs))
- **UI 8:** Hồ sơ cá nhân & Đổi mật khẩu ([`../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/ProfileControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/ProfileControl.cs))
**Mã nguồn nghiệp vụ & truy vấn:**
- BLL Service: [`../../qlspdigital/qlspdigital/BLL/Nhom3_ProductAdmin/SanPhamAdminService.cs`](../../qlspdigital/qlspdigital/BLL/Nhom3_ProductAdmin/SanPhamAdminService.cs)
- DAL Repository: [`../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/SanPhamAdminRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/SanPhamAdminRepository.cs), [`../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/DanhMucRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/DanhMucRepository.cs), [`../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/HoSoRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/HoSoRepository.cs)

---

## 1. Mục tiêu phân hệ
Cung cấp bộ công cụ quản trị dữ liệu cốt lõi cho Administrator:
1. Quản lý thông tin thương mại sản phẩm (thêm, sửa, ngưng bán sản phẩm, cập nhật giá).
2. **Quản lý & Tải lên tệp kỹ thuật số (Digital Files Management):** Upload tệp cài đặt (`.zip`, `.exe`, `.iso`), đo dung lượng byte, phiên bản, lưu trữ vật lý vào thư mục hệ thống và ghi nhận siêu dữ liệu vào bảng `TepSanPham`.
3. Quản lý danh mục thể loại hàng hóa (`DanhMuc`).
4. Quản lý hồ sơ cá nhân và đổi mật khẩu an toàn (`NguoiDung`).

---

## 2. Người dùng mục tiêu & Vai trò
- **Quản trị viên (Admin):**
  - Toàn quyền CRUD trên sản phẩm (`SanPham`): Thêm game/phần mềm mới, sửa giá, ngưng kinh doanh.
  - Quản lý tệp kỹ thuật số (`TepSanPham`): Tải lên bộ cài đặt, bản cập nhật phần mềm, tài liệu đính kèm.
  - Quản trị danh mục thể loại (`DanhMuc`): Thêm, sửa, xóa danh mục.
- **Mọi người dùng (Khách hàng & Admin):**
  - Quản lý hồ sơ cá nhân (`NguoiDung`): Xem thông tin, sửa họ tên, số điện thoại, đổi mật khẩu.

---

## 3. Đặc tả yêu cầu chức năng (Functional Requirements)

### 3.1. UI 10 - Quản lý sản phẩm & Tệp kỹ thuật số (`ProductManageControl`)
1. **Hiển thị danh sách sản phẩm (CRUD Read 1):**
   - DataGridView danh sách sản phẩm: Mã SP (`MaSanPham`), Tên sản phẩm, Danh mục, Nhà phát hành, Giá bán, Phiên bản, Trạng thái kinh doanh (`DangKinhDoanh = 1: Đang bán / 0: Ngưng bán`).
   - Thanh công cụ: Ô tìm kiếm nhanh theo tên sản phẩm (`txtSearch`) và ComboBox lọc theo danh mục (`cboFilterCategory`).
2. **Thêm mới sản phẩm (CRUD Create 1):**
   - Form nhập liệu thông tin chung: Tên sản phẩm, ComboBox chọn danh mục, ComboBox chọn nhà phát hành, Giá bán (VNĐ), Phiên bản (VD: `1.0.0`), Mô tả chi tiết.
   - Nút **"Chọn ảnh bìa"** (`OpenFileDialog`): Chọn file ảnh `.jpg`/`.png` $\rightarrow$ Tự động copy vào `Assets/Images/` và lưu đường dẫn tương đối.
   - Bấm **"➕ Thêm sản phẩm"** $\rightarrow$ Kiểm tra hợp lệ dữ liệu $\rightarrow$ Thực thi `INSERT INTO SanPham` $\rightarrow$ Lấy về `MaSanPham` vừa tạo (`SCOPE_IDENTITY()`).
3. **Quản lý Tải lên tệp số vật lý (Digital File Upload & Metadata Storage):**
   - Nút **"📁 Chọn tệp kỹ thuật số..."** (`OpenFileDialog` lọc `*.zip;*.rar;*.exe;*.iso;*.pdf`):
     + Người dùng chọn file bộ cài hoặc tài nguyên từ máy tính.
     + Hệ thống tự động trích xuất: Tên file gốc, định dạng mở rộng (extension), dung lượng chính xác theo Byte/MB.
     + Copy tệp vật lý vào thư mục lưu trữ ứng dụng: `Storage/Products/{MaSanPham}/{TenTep}` (chạy luồng nền hoặc thanh ProgressBar).
     + Lưu siêu dữ liệu vào bảng `TepSanPham`: `INSERT INTO TepSanPham (MaSanPham, TenTep, DuongDanTep, DinhDangTep, KichThuocByte, PhienBan)`.
   - Danh sách tệp đính kèm của sản phẩm: Hiển thị bảng con các file thuộc về sản phẩm hiện tại, cho phép xóa tệp cũ hoặc cập nhật phiên bản mới.
4. **Cập nhật sản phẩm (CRUD Update 1):**
   - Click chọn dòng trên DataGridView $\rightarrow$ Dữ liệu đổ ngược về Form nhập liệu và nạp danh sách tệp đính kèm.
   - Sửa thông tin $\rightarrow$ Bấm **"💾 Lưu thay đổi"** $\rightarrow$ Thực thi `UPDATE SanPham`.
5. **Xóa / Ngưng kinh doanh (CRUD Delete 1):**
   - Nút **"Ngưng bán / Xóa"**: Chuyển trạng thái `DangKinhDoanh = 0` (Xóa mềm) để đảm bảo không làm gãy các hóa đơn lịch sử đã mua trong quá khứ.
   - Nút **"Làm mới"**: Xóa trắng toàn bộ form để chuẩn bị thêm sản phẩm mới.

### 3.2. UI 11 - Quản lý danh mục thể loại (`CategoryManageControl`)
1. **Hiển thị danh sách danh mục (CRUD Read 2 cho BTL):**
   - Bảng DataGridView hiển thị: Mã danh mục (`MaDanhMuc`), Tên danh mục (`TenDanhMuc`), Mô tả, Số lượng sản phẩm hiện có.
2. **Thao tác Thêm / Sửa / Xóa danh mục (CRUD Create / Update / Delete 2):**
   - Form nhập: Tên danh mục, Mô tả danh mục.
   - Thêm danh mục mới (`INSERT INTO DanhMuc`).
   - Cập nhật thông tin danh mục (`UPDATE DanhMuc`).
   - Xóa danh mục (`DELETE FROM DanhMuc` - ràng buộc kiểm tra nếu có sản phẩm thuộc danh mục thì cảnh báo chặn không cho xóa).

### 3.3. UI 8 - Hồ sơ cá nhân, Đổi mật khẩu & Cài đặt hệ thống (`ProfileControl`)
1. **Xem thông tin tài khoản (CRUD Read 3):**
   - Hiển thị: Tên đăng nhập (chỉ đọc), Vai trò (`QuanTriVien`/`KhachHang`), Số dư ví (`SoDuVi`), Họ và tên, Email, Số điện thoại.
2. **Cập nhật thông tin cá nhân & Đồng bộ Realtime (CRUD Update 3):**
   - Cho phép chỉnh sửa: Họ tên, Email, Số điện thoại $\rightarrow$ Bấm nút **"Lưu thay đổi hồ sơ"** $\rightarrow$ Thực thi `UPDATE NguoiDung`.
   - **Đồng bộ thời gian thực (Real-time Sync):** Khi cập nhật họ tên thành công, hệ thống gọi `UserSession.UpdateProfile(hoTen, email)` để kích hoạt sự kiện `SessionChanged`. Thanh tiêu đề Header trên `MainForm` (`lblUserGreeting`) lập tức lắng nghe sự kiện và cập nhật lại câu chào cùng họ tên mới ngay trong tích tắc mà không cần khởi động lại ứng dụng.
3. **Đổi mật khẩu an toàn:**
   - Nhập: Mật khẩu cũ, Mật khẩu mới, Xác nhận mật khẩu mới.
   - Kiểm tra mật khẩu cũ $\rightarrow$ Nếu đúng cập nhật mật khẩu mới $\rightarrow$ Bấm nút **"Lưu mật khẩu mới"** $\rightarrow$ `UPDATE NguoiDung SET MatKhau = @MatKhauMoi`.
4. **Tùy chọn cài đặt ứng dụng & Lưu trữ CSDL thật (Settings Persistence):**
   - Cung cấp các cấu hình tùy chọn:
     + Nhận thông báo giao dịch & ưu đãi qua Email (`ThongBaoEmail`).
     + Ghi nhớ phiên đăng nhập trên thiết bị (`GhiNhoDangNhap`).
     + Tự động kiểm tra bản cập nhật tệp kỹ thuật số (`TuDongCapNhat`).
     + Luôn hiển thị hộp thoại xác nhận trước khi trừ ví thanh toán (`XacNhanThanhToan`).
   - Bấm nút **"Lưu cài đặt ứng dụng"** $\rightarrow$ Lưu bền vững vào bảng CSDL `CaiDatNguoiDung` (cơ chế UPSERT - nếu chưa có thì INSERT, đã có thì UPDATE). Mỗi khi người dùng mở màn hình Hồ sơ, hệ thống tự động nạp trạng thái đã lưu từ CSDL.
5. **Banner Nguyên tắc bảo mật tài khoản Digital Steam:**
   - Tích hợp khung thông tin khuyến cáo an toàn tài khoản số chuẩn Steam với 4 quy tắc cốt lõi: Mật khẩu phức tạp độc nhất, Tuyệt đối không chia sẻ mã xác thực OTP/Email, Cảnh báo giao dịch nạp/trừ ví bất thường, Bảo vệ an toàn tài khoản và thiết bị.
   - Cơ chế layout động (`AutoSize` và tính toán lại `Height` theo độ rộng thực tế `lblSecurityContent.Bottom + 25px`), đảm bảo hiển thị trọn vẹn toàn bộ các mục danh sách và không bị che khuất phần chân ở bất kỳ kích thước màn hình nào.
6. **Quy chuẩn nhãn nút bấm:**
   - Tất cả các nút bấm hành động đều sử dụng nhãn chữ thuần (plain text), tuyệt đối không thêm icon emoji để giữ tính trang trọng, chuyên nghiệp và đồng bộ chuẩn ứng dụng Desktop.

---

## 4. Quy tắc nghiệp vụ & Quản lý Tệp số (Business Rules)
1. **Ràng buộc giá:** Giá sản phẩm phải là số không âm (`GiaBan >= 0`).
2. **Toàn vẹn danh mục:** Không cho phép xóa danh mục nếu đang có ít nhất 1 sản phẩm liên kết với nó.
3. **Quy tắc lưu trữ tệp số (Hybrid Storage):**
   - Tuyệt đối không lưu tệp nhị phân trực tiếp vào SQL Server để tránh phình to CSDL (database bloat).
   - Tệp vật lý phải được lưu trong thư mục: `Storage/Products/{MaSanPham}/{TenTep}`.
   - CSDL chỉ lưu **đường dẫn tương đối** và các siêu dữ liệu kỹ thuật.
4. **Quản lý siêu dữ liệu tệp:** Mỗi tệp upload được ghi nhận metadata (tên tệp, đường dẫn, định dạng, dung lượng byte, phiên bản) làm cơ sở cho phân hệ Thư viện (Nhóm 5) quản lý khi người dùng tải tệp về.
5. **Ràng buộc mật khẩu:** Mật khẩu cũ phải trùng khớp với CSDL; mật khẩu mới phải có độ dài tối thiểu 6 ký tự.
6. **Bảo toàn cài đặt người dùng:** Bảng `CaiDatNguoiDung` liên kết khóa ngoại với `NguoiDung(MaNguoiDung)` với `ON DELETE CASCADE`, đảm bảo cấu hình cá nhân luôn đi liền với tài khoản.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Sản phẩm & Tệp số - UI 10):** Thêm, sửa, ngưng bán sản phẩm mượt mà; chọn và upload tệp kỹ thuật số vào thư mục `Storage/` và ghi metadata vào `TepSanPham` thành công.
- [x] **CRUD 2 (Danh mục thể loại - UI 11):** Thêm, xem, sửa, xóa danh mục trên DataGridView thành công.
- [x] **CRUD 3 (Hồ sơ, Đổi mật khẩu & Cài đặt - UI 8):** Cập nhật họ tên, email, đồng bộ Header realtime qua `SessionChanged`, đổi mật khẩu an toàn và lưu bền vững cấu hình vào `CaiDatNguoiDung` trong CSDL. Banner bảo mật hiển thị trọn vẹn không bị cắt dòng.

---

## 6. Ma trận Đặc tả Nghiệp vụ CRUD Chuyên sâu (CRUD Matrix & Defense Rationale)

| Thực thể | **C** (Create) | **R** (Read) | **U** (Update) | **D** (Delete) |
|---|---|---|---|---|
| **Sản phẩm & File** (`SanPham`, `TepSanPham`) | `btnNew` + `btnSave` (upload file, lưu metadata) | `dgvProducts` + ô tìm kiếm `txtSearch` | `btnSave` (khi `txtId > 0`) cập nhật giá, phiên bản, trạng thái | `btnDelete` xóa sản phẩm khỏi CSDL |
| **Danh mục** (`DanhMuc`) | `btnNew` + `btnSave` thêm danh mục mới | `dgvCategories` hiển thị kèm số lượng SP | `btnSave` sửa tên và mô tả danh mục | `btnDelete` xóa danh mục (chặn nếu còn sản phẩm) |
| **Hồ sơ cá nhân** (`HoSo`) | Đăng ký tài khoản (tại Nhóm 4) | `LoadProfile` nạp thông tin cá nhân | `btnUpdateProfile` đổi họ tên, email, SĐT; `btnChangePassword` đổi mật khẩu | Không áp dụng (Hồ sơ gắn liền phiên đăng nhập) |

> **Phòng thủ vấn đáp BTL:** Khẳng định với Hội đồng rằng phân hệ của Phạm Trung Hiếu là **chuẩn mực Master Data CRUD hoàn hảo 100%**: hỗ trợ cả upload tệp nhị phân Hybrid Storage bên ngoài CSDL và bẫy kiểm tra toàn vẹn danh mục (Cascade Check) ngăn chặn xóa nhầm danh mục đang chứa sản phẩm.


