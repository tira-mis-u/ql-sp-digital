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
2. **Quản lý & Tải lên tệp kỹ thuật số (Digital Files Management):** Upload tệp cài đặt (`.zip`, `.exe`, `.iso`), đo dung lượng byte, tự động sinh mã băm SHA-256 chống lỗi file, lưu trữ vật lý vào thư mục hệ thống và ghi nhận siêu dữ liệu vào bảng `TepSanPham`.
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
     + Tự động tính mã băm kiểm tra toàn vẹn **`SHA-256`** của tệp nguồn.
     + Copy tệp vật lý vào thư mục lưu trữ ứng dụng: `Storage/Products/{MaSanPham}/{TenTep}` (chạy luồng nền hoặc thanh ProgressBar).
     + Lưu siêu dữ liệu vào bảng `TepSanPham`: `INSERT INTO TepSanPham (MaSanPham, TenTep, DuongDanTep, DinhDangTep, KichThuocByte, MaHashSHA256, PhienBan)`.
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

### 3.3. UI 8 - Hồ sơ cá nhân & Đổi mật khẩu (`ProfileControl`)
1. **Xem thông tin tài khoản (CRUD Read 3):**
   - Hiển thị: Tên đăng nhập (chỉ đọc), Vai trò (`QuanTriVien`/`KhachHang`), Số dư ví (`SoDuVi`), Họ và tên, Email, Số điện thoại.
2. **Cập nhật thông tin cá nhân (CRUD Update 3):**
   - Cho phép chỉnh sửa: Họ tên, Email, Số điện thoại $\rightarrow$ Bấm **"Cập nhật hồ sơ"** $\rightarrow$ `UPDATE NguoiDung`.
3. **Đổi mật khẩu an toàn:**
   - Nhập: Mật khẩu cũ, Mật khẩu mới, Xác nhận mật khẩu mới.
   - Kiểm tra hash SHA-256 mật khẩu cũ $\rightarrow$ Nếu đúng cập nhật mật khẩu mới $\rightarrow$ `UPDATE NguoiDung SET MatKhauHash = @MatKhauHashMoi`.

---

## 4. Quy tắc nghiệp vụ & Quản lý Tệp số (Business Rules)
1. **Ràng buộc giá:** Giá sản phẩm phải là số không âm (`GiaBan >= 0`).
2. **Toàn vẹn danh mục:** Không cho phép xóa danh mục nếu đang có ít nhất 1 sản phẩm liên kết với nó.
3. **Quy tắc lưu trữ tệp số (Hybrid Storage):**
   - Tuyệt đối không lưu tệp nhị phân trực tiếp vào SQL Server để tránh phình to CSDL (database bloat).
   - Tệp vật lý phải được lưu trong thư mục: `Storage/Products/{MaSanPham}/{TenTep}`.
   - CSDL chỉ lưu **đường dẫn tương đối** và các siêu dữ liệu kỹ thuật.
4. **Kiểm tra toàn vẹn (SHA-256):** Mỗi tệp upload bắt buộc phải được tính toán mã hash SHA-256 để làm cơ sở cho phân hệ Thư viện (Nhóm 5) đối chiếu kiểm tra khi người dùng tải tệp về.
5. **Ràng buộc bảo mật mật khẩu:** Mật khẩu cũ phải trùng khớp với hash trong CSDL; mật khẩu mới phải có độ dài tối thiểu 6 ký tự.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Sản phẩm & Tệp số - UI 10):** Thêm, sửa, ngưng bán sản phẩm mượt mà; chọn và upload tệp kỹ thuật số vào thư mục `Storage/` và ghi metadata vào `TepSanPham` thành công.
- [x] **CRUD 2 (Danh mục thể loại - UI 11):** Thêm, xem, sửa, xóa danh mục trên DataGridView thành công.
- [x] **CRUD 3 (Hồ sơ & Đổi mật khẩu - UI 8):** Cập nhật họ tên, email và đổi mật khẩu thành công.
