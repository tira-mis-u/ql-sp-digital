# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 3
**Phân hệ:** Quản lý sản phẩm & Tệp số, Quản lý danh mục, Hồ sơ cá nhân (Products & Digital Files, Categories, User Profile)  
**Màn hình phụ trách:** UI 10 (`ProductManageControl`), UI 11 (`CategoryManageControl`), UI 8 (`ProfileControl`)  
**Thành viên phụ trách:** Phạm Trung Hiếu  
**Kiến trúc áp dụng:** 3 tầng (Presentation - Business Logic - Data Access - SQL Server)  
**Quy chuẩn cơ sở dữ liệu:** 100% PascalCase Tiếng Việt Chuẩn Chuyên Ngành CNTT

---

## 0. Danh mục File & Thư mục phụ trách trực tiếp (Đường dẫn tương đối)

| Phân tầng kiến trúc | Tên File / Thư mục cụ thể | Đường dẫn tương đối từ tài liệu này | Ý nghĩa chức năng |
|---|---|---|---|
| **Giao diện (Views)** | `ProductManageControl.cs` | [`../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/ProductManageControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/ProductManageControl.cs) | UI 10: CRUD thông tin sản phẩm, giá bán, phiên bản, chọn tệp đính kèm |
| **Giao diện (Views)** | `CategoryManageControl.cs` | [`../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/CategoryManageControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/CategoryManageControl.cs) | UI 11: CRUD thể loại hàng hóa, đếm số SP trực thuộc, chặn xóa nếu có ràng buộc |
| **Giao diện (Views)** | `ProfileControl.cs` | [`../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/ProfileControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/ProfileControl.cs) | UI 8: Xem và cập nhật họ tên/email/SĐT, đổi mật khẩu băm SHA-256 |
| **Nghiệp vụ (BLL)** | `SanPhamAdminService.cs` | [`../../qlspdigital/qlspdigital/BLL/Nhom3_ProductAdmin/SanPhamAdminService.cs`](../../qlspdigital/qlspdigital/BLL/Nhom3_ProductAdmin/SanPhamAdminService.cs) | Xử lý logic sản phẩm (`SanPhamAdminService`), danh mục (`DanhMucService`) & hồ sơ (`HoSoService`) |
| **Truy vấn CSDL (DAL)** | `SanPhamAdminRepository.cs` | [`../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/SanPhamAdminRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/SanPhamAdminRepository.cs) | Lệnh ADO.NET CRUD trực tiếp bảng `SanPham` (SELECT, INSERT, UPDATE, DELETE) |
| **Truy vấn CSDL (DAL)** | `DanhMucRepository.cs` | [`../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/DanhMucRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/DanhMucRepository.cs) | Lệnh ADO.NET CRUD bảng `DanhMuc`, đếm `COUNT(sp)` |
| **Truy vấn CSDL (DAL)** | `HoSoRepository.cs` | [`../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/HoSoRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/HoSoRepository.cs) | Lệnh ADO.NET UPDATE hồ sơ, đổi mật khẩu `NguoiDung` & metadata `TepSanPham` |
| **Dữ liệu truyền tải (DTO)** | `SanPhamDTO.cs` | [`../../qlspdigital/qlspdigital/DTOs/SanPhamDTO.cs`](../../qlspdigital/qlspdigital/DTOs/SanPhamDTO.cs) | Định nghĩa cấu trúc `SanPhamDTO` và `TepSanPhamDTO` |
| **Dữ liệu truyền tải (DTO)** | `DanhMucDTO.cs` | [`../../qlspdigital/qlspdigital/DTOs/DanhMucDTO.cs`](../../qlspdigital/qlspdigital/DTOs/DanhMucDTO.cs) | Định nghĩa cấu trúc `DanhMucDTO` |
| **Dữ liệu truyền tải (DTO)** | `NguoiDungDTO.cs` | [`../../qlspdigital/qlspdigital/DTOs/NguoiDungDTO.cs`](../../qlspdigital/qlspdigital/DTOs/NguoiDungDTO.cs) | Định nghĩa cấu trúc `NguoiDungDTO` |
| **CSDL Master SQL** | `DPMS_Database_Master.sql` | [`../../Database/DPMS_Database_Master.sql`](../../Database/DPMS_Database_Master.sql) | Bảng sử dụng: `SanPham`, `TepSanPham`, `DanhMuc`, `NguoiDung` |

---


## 1. Kiến trúc phân tầng & Luồng tương tác

```text
+----------------------------------------------------------------------------------------------------+
|                                         PRESENTATION LAYER                                         |
|  - ProductManageControl.cs (Quản lý thông tin SP & Upload tệp số kèm ProgressBar)                 |
|  - CategoryManageControl.cs (Quản lý thể loại, kiểm tra số lượng SP trực thuộc)                    |
|  - ProfileControl.cs (Xem thông tin tài khoản, cập nhật SĐT/Email, Đổi mật khẩu)                  |
+--------------------------------------------------+-------------------------------------------------+
                                                   | DTOs (SanPhamManageDTO, TepSanPhamDTO, DanhMucDTO)
+--------------------------------------------------v-------------------------------------------------+
|                                        BUSINESS LOGIC (BLL)                                        |
|  - ISanPhamAdminService / SanPhamAdminService.cs - ITepSanPhamService / TepSanPhamService.cs       |
|  - IDanhMucService / DanhMucService.cs           - IProfileService / ProfileService.cs             |
|  - IFileStorageManager (Tính băm SHA-256, Đọc ghi FileStream 64KB Async, Dọn file mồ côi)         |
+--------------------------------------------------+-------------------------------------------------+
                                                   | Entities / Parameters
+--------------------------------------------------v-------------------------------------------------+
|                                       DATA ACCESS LAYER (DAL)                                      |
|  - ISanPhamRepository / SanPhamRepository.cs     - ITepSanPhamRepository / TepSanPhamRepo.cs       |
|  - IDanhMucRepository / DanhMucRepository.cs     - INguoiDungRepository / NguoiDungRepo.cs         |
|  - ADO.NET: SqlConnection, SqlCommand, SqlDataReader, SqlTransaction                               |
+--------------------------------------------------+-------------------------------------------------+
                                                   | T-SQL Statements
+--------------------------------------------------v-------------------------------------------------+
|                                        MICROSOFT SQL SERVER                                        |
|  - Bảng sử dụng chính: SanPham, TepSanPham, DanhMuc, NhaPhatHanh, NguoiDung                       |
+----------------------------------------------------------------------------------------------------+
```

---

## 2. Thiết kế Mô hình CSDL & Quan hệ Bảng cho Nhóm 3

### 2.1. Bảng `SanPham` (Quản trị CRUD - UI 10)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaSanPham` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh duy nhất của sản phẩm. |
| `TenSanPham` | `NVARCHAR(200)` | | `NOT NULL` | Tên sản phẩm hiển thị trên lưới và form nhập liệu. |
| `MoTa` | `NVARCHAR(MAX)` | | `NULL` | Chi tiết tính năng, yêu cầu phần cứng. |
| `GiaBan` | `DECIMAL(18,2)` | | `NOT NULL, DEFAULT 0, CHECK >= 0` | Đơn giá niêm yết (VNĐ). |
| `HinhAnh` | `NVARCHAR(500)` | | `NULL` | Đường dẫn tương đối của ảnh bìa (`Assets/Images/...`). |
| `DungLuongMB` | `DECIMAL(10,2)` | | `NOT NULL, DEFAULT 0` | Dung lượng ước tính hiển thị cho khách hàng. |
| `PhienBan` | `NVARCHAR(50)` | | `NOT NULL, DEFAULT '1.0.0'` | Phiên bản phát hành chính của sản phẩm. |
| `MaDanhMuc` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `DanhMuc(MaDanhMuc)` | Danh mục thể loại (chọn từ ComboBox trên UI 10). |
| `MaNhaPhatHanh` | `INT` | **FK** | `NULL` $\rightarrow$ `NhaPhatHanh(MaNhaPhatHanh)` | Nhà sáng tạo / Studio phát hành sản phẩm. |
| `DangKinhDoanh` | `BIT` | | `NOT NULL, DEFAULT 1` | Trạng thái mở bán: 1 = Đang bán, 0 = Ngưng kinh doanh. |
| `NgayTao` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Ngày tạo sản phẩm. |
| `NgayCapNhat` | `DATETIME` | | `NULL` | Thời điểm chỉnh sửa thông tin gần nhất. |

### 2.2. Bảng `TepSanPham` (Đính kèm Tệp kỹ thuật số - Trọng tâm Upload UI 10)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaTep` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh tệp kỹ thuật số. |
| `MaSanPham` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `SanPham(MaSanPham) ON DELETE CASCADE` | Thuộc về sản phẩm nào. Xóa sản phẩm sẽ xóa tệp con. |
| `TenTep` | `NVARCHAR(255)` | | `NOT NULL` | Tên file vật lý lưu trên đĩa (VD: `Setup_v1.0.zip`). |
| `DuongDanTep` | `NVARCHAR(500)` | | `NOT NULL` | Đường dẫn tương đối lưu trữ (VD: `Storage/Products/1/Setup.zip`). |
| `DinhDangTep` | `VARCHAR(20)` | | `NOT NULL` | Đuôi mở rộng (.zip, .rar, .exe, .iso, .pdf). |
| `KichThuocByte` | `BIGINT` | | `NOT NULL, CHECK > 0` | Kích thước chính xác tính theo Byte của tệp. |
| `MaHashSHA256` | `VARCHAR(64)` | | `NOT NULL` | Chuỗi 64 ký tự băm kiểm tra toàn vẹn chống hỏng file. |
| `PhienBan` | `NVARCHAR(50)` | | `NOT NULL, DEFAULT '1.0.0'` | Phiên bản riêng của tệp đính kèm. |
| `NgayTaiLen` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Thời điểm tải tệp lên hệ thống. |

### 2.3. Bảng `DanhMuc` (Quản trị CRUD - UI 11)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaDanhMuc` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh thể loại. |
| `TenDanhMuc` | `NVARCHAR(100)` | | `NOT NULL, UNIQUE` | Tên thể loại (Game Hành Động, Đồ Họa 3D, v.v.). |
| `MoTa` | `NVARCHAR(255)` | | `NULL` | Diễn giải nội dung của danh mục. |
| `TrangThai` | `BIT` | | `NOT NULL, DEFAULT 1` | Trạng thái sử dụng (1 = Hoạt động, 0 = Tạm ẩn). |

### 2.4. Bảng `NguoiDung` (Cập nhật hồ sơ & Đổi mật khẩu - UI 8)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaNguoiDung` | `INT` | **PK** | `IDENTITY(1,1)` | Mã tài khoản (lấy từ phiên đăng nhập). |
| `TenDangNhap` | `VARCHAR(50)` | | `NOT NULL, UNIQUE` | Tên đăng nhập (chỉ đọc trên UI 8, không cho sửa). |
| `MatKhauHash` | `VARCHAR(256)` | | `NOT NULL` | Mật khẩu mã hóa băm SHA-256 (cập nhật khi đổi mật khẩu). |
| `HoTen` | `NVARCHAR(100)` | | `NOT NULL` | Họ và tên hiển thị của người dùng. |
| `Email` | `VARCHAR(100)` | | `NOT NULL, UNIQUE` | Thư điện tử nhận thông báo. |
| `VaiTro` | `NVARCHAR(20)` | | `NOT NULL, DEFAULT N'KhachHang'` | Vai trò tài khoản (`N'QuanTriVien'` hoặc `N'KhachHang'`). |
| `TrangThai` | `BIT` | | `NOT NULL, DEFAULT 1` | Trạng thái tài khoản: 1 = Hoạt động, 0 = Đã bị khóa. |
| `SoDuVi` | `DECIMAL(18,2)` | | `NOT NULL, DEFAULT 0` | Số dư ví hiện tại. |
| `NgayTao` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Ngày đăng ký thành viên. |

### 2.5. Bảng `CaiDatNguoiDung` (Tùy chọn cấu hình hệ thống & Bảo mật cá nhân - UI 8)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaCaiDat` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh bản ghi cấu hình cài đặt. |
| `MaNguoiDung` | `INT` | **FK** | `NOT NULL, UNIQUE, FK -> NguoiDung(MaNguoiDung) ON DELETE CASCADE` | Khóa ngoại liên kết 1-1 với tài khoản người dùng. |
| `ThongBaoEmail` | `BIT` | | `NOT NULL, DEFAULT 1` | Tùy chọn nhận thông báo giao dịch & khuyến mãi qua Email. |
| `GhiNhoDangNhap` | `BIT` | | `NOT NULL, DEFAULT 1` | Tùy chọn lưu phiên đăng nhập cục bộ trên thiết bị. |
| `TuDongCapNhat` | `BIT` | | `NOT NULL, DEFAULT 1` | Tự động kiểm tra phiên bản mới của tệp kỹ thuật số. |
| `XacNhanThanhToan` | `BIT` | | `NOT NULL, DEFAULT 1` | Bật hộp thoại hỏi xác nhận trước khi trừ số dư ví. |
| `NgayCapNhat` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Thời điểm cập nhật cấu hình gần nhất. |

### 2.6. Quan hệ bảng & Quy tắc toàn vẹn dữ liệu
```text
[ DanhMuc ] (1) <---------- (N) [ SanPham ] (1) <---------- (N) [ TepSanPham ]
                                     ^
                                     | (N)
                              [ NhaPhatHanh ] (1)

[ NguoiDung ] (1) <---------- (1) [ CaiDatNguoiDung ] (ON DELETE CASCADE)
```
- **Ràng buộc khóa ngoại khi xóa Danh mục:** `FK_SanPham_DanhMuc` cấu hình `ON DELETE NO ACTION`. Hệ thống sẽ chặn không cho phép xóa danh mục nếu đang có ít nhất một sản phẩm trực thuộc.
- **Ràng buộc Cascade cho Tệp số:** `FK_TepSanPham_SanPham` cấu hình `ON DELETE CASCADE`. Khi một sản phẩm bị xóa hẳn, toàn bộ các bản ghi tệp số con sẽ tự động được dọn trong CSDL.
- **Ràng buộc Cascade cho Cài đặt cá nhân:** `FK_CaiDatNguoiDung_NguoiDung` cấu hình `ON DELETE CASCADE`. Khi xóa tài khoản người dùng, toàn bộ cấu hình tùy chọn liên kết sẽ tự động bị xóa theo.
- **Ràng buộc tên danh mục duy nhất:** `CONSTRAINT UQ_DanhMuc_Ten UNIQUE (TenDanhMuc)` chống trùng tên danh mục.

---

## 3. Bảng Ma trận Thao tác CRUD theo từng UI

| Giao diện | Thao tác CRUD | Bảng CSDL tác động | Điều kiện lọc & Tham số nghiệp vụ | Kết quả trả về giao diện |
|---|---|---|---|---|
| **UI 10: Quản lý SP** (`ProductManageControl`) | **Read** (Duyệt & Tìm kiếm) | `SanPham`, `DanhMuc`, `NhaPhatHanh` | `@TuKhoa` (tìm theo `TenSanPham`)<br>`@MaDanhMuc` (0 = Tất cả)<br>`ORDER BY MaSanPham DESC` | Danh sách `List<SanPhamManageDTO>` nạp vào DataGridView sản phẩm. |
| **UI 10: Quản lý SP** (`ProductManageControl`) | **Create** (Thêm sản phẩm) | `SanPham` | `@TenSanPham`, `@MoTa`, `@GiaBan`, `@HinhAnh`, `@DungLuongMB`, `@PhienBan`, `@MaDanhMuc`, `@MaNhaPhatHanh` | Thêm sản phẩm mới; sinh `MaSanPham` để tiếp tục gắn tệp số. |
| **UI 10: Quản lý SP** (`ProductManageControl`) | **Update** (Sửa sản phẩm) | `SanPham` | `MaSanPham = @MaSanPham`<br>Cập nhật thông tin giá, mô tả, thể loại | Cập nhật lại thông tin sản phẩm và hiển thị thông báo thành công. |
| **UI 10: Quản lý SP** (`ProductManageControl`) | **Delete** (Ngưng kinh doanh) | `SanPham` | `MaSanPham = @MaSanPham`<br>Cập nhật `DangKinhDoanh = 0` (Xóa mềm) | Ẩn sản phẩm khỏi Store UI 2 nhưng giữ nguyên lịch sử đơn hàng. |
| **UI 10: Quản lý Tệp số** (`ProductManageControl`) | **Read** (Danh sách tệp đính kèm) | `TepSanPham` | `MaSanPham = @MaSanPham`<br>`ORDER BY NgayTaiLen DESC` | Danh sách `List<TepSanPhamDTO>` nạp vào DataGridView tệp con. |
| **UI 10: Quản lý Tệp số** (`ProductManageControl`) | **Create** (Upload tệp số) | `TepSanPham` | `@MaSanPham`, `@TenTep`, `@DuongDanTep`, `@DinhDangTep`, `@KichThuocByte`, `@MaHashSHA256`, `@PhienBan` | Ghi nhận metadata tệp; copy file vật lý bất đồng bộ có ProgressBar. |
| **UI 10: Quản lý Tệp số** (`ProductManageControl`) | **Delete** (Xóa tệp đính kèm) | `TepSanPham` | `MaTep = @MaTep` | Xóa bản ghi CSDL và dọn sạch file vật lý trên ổ cứng. |
| **UI 11: Quản lý Danh mục** (`CategoryManageControl`) | **Read** (Xem danh mục) | `DanhMuc`, `SanPham` | `COUNT(sp.MaSanPham)` theo từng danh mục | Lưới danh mục kèm cột số lượng sản phẩm đang có. |
| **UI 11: Quản lý Danh mục** (`CategoryManageControl`) | **Create** (Thêm danh mục) | `DanhMuc` | `@TenDanhMuc`, `@MoTa`, `TrangThai = 1` | Bổ sung thể loại mới vào ComboBox lọc của toàn hệ thống. |
| **UI 11: Quản lý Danh mục** (`CategoryManageControl`) | **Update** (Sửa danh mục) | `DanhMuc` | `MaDanhMuc = @MaDanhMuc`<br>Cập nhật `@TenDanhMuc`, `@MoTa` | Cập nhật tên thể loại hiển thị. |
| **UI 11: Quản lý Danh mục** (`CategoryManageControl`) | **Delete** (Xóa danh mục) | `DanhMuc` | `MaDanhMuc = @MaDanhMuc` VÀ không có sản phẩm liên kết | Xóa danh mục nếu rỗng; cảnh báo lỗi nếu còn sản phẩm. |
| **UI 8: Hồ sơ cá nhân** (`ProfileControl`) | **Read** (Xem hồ sơ & Cài đặt) | `NguoiDung`, `CaiDatNguoiDung` | `MaNguoiDung = @UserId` | Điền thông tin lên form: Họ tên, Email, SĐT, Số dư ví, Ngày tạo và nạp 4 tùy chọn cài đặt hệ thống. |
| **UI 8: Hồ sơ cá nhân** (`ProfileControl`) | **Update** (Sửa thông tin & Realtime) | `NguoiDung` | `MaNguoiDung = @UserId`<br>Cập nhật `@HoTen`, `@Email`, `@SoDienThoai` | Cập nhật CSDL và gọi `UserSession.UpdateProfile()` bắn sự kiện `SessionChanged` đồng bộ tức thì lên Header `MainForm`. |
| **UI 8: Hồ sơ cá nhân** (`ProfileControl`) | **Update** (Đổi mật khẩu) | `NguoiDung` | `MaNguoiDung = @UserId AND MatKhauHash = @HashCu`<br>Cập nhật `MatKhauHash = @HashMoi` | Xác nhận đổi mật khẩu thành công bằng mã băm SHA-256 an toàn. |
| **UI 8: Hồ sơ cá nhân** (`ProfileControl`) | **Create/Update** (Lưu cài đặt ứng dụng) | `CaiDatNguoiDung` | `MaNguoiDung = @UserId`<br>Cập nhật `@ThongBaoEmail`, `@GhiNhoDangNhap`, `@TuDongCapNhat`, `@XacNhanThanhToan` | Thực thi UPSERT vào CSDL, bảo đảm các checkbox cấu hình được lưu bền vững vào database thực tế. |

---

## 4. Phân tích Chuyên sâu: Bài toán Lưu trữ Tệp số cho UI 10 (Admin Upload)

Khi phát triển chức năng thêm sản phẩm số tại **UI 10**, Admin bắt buộc phải đính kèm file game, phần mềm hoặc tệp tài nguyên số để khách hàng mua xong có thể tải về (ở UI 6 của Nhóm 5). Đây là bài toán kỹ thuật đặc thù và khó nhất của phân hệ quản trị.

### 4.1. Phân tích & So sánh 3 Phương án kỹ thuật:
1. **Phương án 1: Lưu toàn bộ tệp nhị phân vào Database (`VARBINARY(MAX)` / BLOB):**
   - *Cơ chế:* Đọc file cài đặt `.exe`/`.zip` thành mảng byte khổng lồ rồi lưu vào 1 cột trong SQL Server.
   - *Đánh giá:* **RẤT XẤU / TỒI TỆ.** Game và phần mềm có dung lượng từ vài trăm MB đến vài GB. Nếu lưu vào CSDL, file `.mdf` sẽ phình to khủng khiếp (Database Bloat). RAM của máy chủ SQL Server sẽ bị cạn kiệt, các lệnh query thông thường bị nghẽn I/O và backup CSDL mất nhiều giờ đồng hồ.
2. **Phương án 2: Chỉ lưu đuôi file mở rộng (`.zip`, `.exe`):**
   - *Đánh giá:* **KHÔNG KHẢ THI.** Đuôi file chỉ là định dạng. Nếu chỉ lưu đuôi file, ứng dụng sẽ hoàn toàn mù mờ: File nằm ở đâu trên ổ đĩa? Tên file gốc là gì? Dung lượng bao nhiêu byte? Làm sao để người dùng tải về?
3. **Phương án 3 (CHỌN): HYBRID STORAGE — LƯU ĐƯỜNG DẪN TƯƠNG ĐỐI & METADATA VÀO CSDL, LƯU TỆP VẬT LÝ VÀO THƯ MỤC LƯU TRỮ HỆ THỐNG:**
   - *Đánh giá:* **CHUẨN CÔNG NGHIỆP (BEST PRACTICE).** 
     + CSDL cực kỳ nhẹ và nhanh (mỗi bản ghi `TepSanPham` chỉ chiếm vài chục byte).
     + File vật lý được lưu trữ nguyên vẹn trên ổ cứng/SSD trong thư mục: `Storage/Products/{MaSanPham}/{TenTep}`.
     + Đây là kiến trúc của mọi nền tảng lớn như Steam, Epic Games, GitHub Releases và Google Drive.

---

### 4.2. Khai thác Triệt để 5 "Cái khó" và Bài toán Thực tế của Nhóm 3:

```text
+----------------------------------------------------------------------------------------------------+
|                                QUY TRÌNH UPLOAD TỆP KỸ THUẬT SỐ (UI 10)                            |
|                                                                                                    |
| [Admin bấm: 📁 Chọn tệp...]                                                                        |
|         |                                                                                          |
|         v                                                                                          |
| [OpenFileDialog: Lọc *.zip;*.exe;*.rar;*.iso;*.pdf] -> Lấy SourcePath                              |
|         |                                                                                          |
|         v                                                                                          |
| [BLL: Tự động trích xuất Tên, Đuôi file, Dung lượng FileInfo.Length (Byte)]                        |
|         |                                                                                          |
|         v                                                                                          |
| [BLL: Tính mã băm SHA-256 của tệp nguồn (Đảm bảo chống lỗi tệp sau này)]                           |
|         |                                                                                          |
|         v                                                                                          |
| [Tạo thư mục đích: Path.Combine(Application.StartupPath, "Storage", "Products", MaSanPham)]        |
|         |                                                                                          |
|         v                                                                                          |
| [Async Chunk Copy: FileStream đọc ghi khối 64KB kèm ProgressBar trên UI (Không đơ Form)]          |
|         |                                                                                          |
|         v                                                                                          |
| [DAL: INSERT INTO TepSanPham (Lưu đường dẫn tương đối, tên, kích thước, hash, phiên bản)]          |
|         |                                                                                          |
|         v                                                                                          |
| [UI 10: Nạp lại DataGridView danh sách tệp đính kèm -> Hoàn tất!]                                  |
+----------------------------------------------------------------------------------------------------+
```

#### Bài toán 1: Tránh đơ màn hình UI 10 khi Admin copy tệp dung lượng lớn (UI Freezing)
- **Cái khó:** Khi Admin chọn một bộ cài đặt game nặng 2 GB - 10 GB, nếu dùng lệnh `File.Copy(source, dest)` đồng bộ, tiến trình sẽ chiếm trọn luồng Main UI Thread. Form Admin sẽ hiện `(Not Responding)`, không thể bấm nút khác hay hủy thao tác, tạo cảm giác phần mềm bị treo.
- **Giải pháp:**
   - Viết hàm `CopyFileWithProgressAsync` dùng `FileStream` đọc/ghi từng khối buffer 64 KB trong `Task.Run`.
   - Báo cáo tiến độ về giao diện qua `IProgress<int>` để thanh `ProgressBar` tăng dần từ 0% đến 100% mượt mà.

#### Bài toán 2: Chống gãy đường dẫn khi chuyển máy (Relative Path Resolution)
- **Cái khó:** Nếu lưu đường dẫn cứng `C:\Users\ADMIN\Desktop\ql-sp-digital\Storage\...` vào SQL Server, khi giảng viên mang project sang máy khác chấm điểm, toàn bộ chức năng tải tệp sẽ báo lỗi "Không tìm thấy file" (`FileNotFoundException`).
- **Giải pháp:**
   - CSDL **chỉ lưu đường dẫn tương đối**: `Storage/Products/{MaSanPham}/{TenTep}`.
   - Mã C# luôn phân giải động qua `Path.Combine(Application.StartupPath, duongDanTep.Replace("/", "\\"))`.

#### Bài toán 3: Tự động tính toán Checksum SHA-256 phòng ngừa file hỏng
- **Cái khó:** File cài đặt số kích thước lớn rất dễ bị hỏng (corrupt) trong quá trình truyền dữ liệu mạng hoặc sao chép ổ cứng. Nếu không có cơ chế đối chiếu, khách hàng tải về sẽ không biết tại sao không cài được game.
- **Giải pháp:**
   - Ngay khi Admin up tệp tại UI 10, hệ thống tự động băm file bằng `SHA256CryptoServiceProvider` và lưu chuỗi 64 ký tự hex vào `TepSanPham.MaHashSHA256`.
   - Phân hệ Thư viện (Nhóm 5) khi tải file về máy khách sẽ băm lại và so khớp. Nếu khớp 100% mới báo hoàn tất.

#### Bài toán 4: Quản lý quan hệ 1-N (1 Sản phẩm có nhiều tệp số)
- **Cái khó:** Một sản phẩm số không chỉ có 1 file. Ví dụ một tựa game có thể gồm: Bộ cài chính (`Setup.exe`), Bản vá cập nhật (`Patch_v1.1.zip`), Ebook hướng dẫn (`Guide.pdf`), và Nhạc nền (`Soundtrack.mp3`).
- **Giải pháp:**
   - Thiết kế bảng riêng `TepSanPham` có khóa ngoại `MaSanPham` trỏ về `SanPham(MaSanPham)`.
   - UI 10 hiển thị một bảng con `dgvProductFiles`. Admin có thể up nhiều file độc lập, mỗi file có tên, phiên bản và dung lượng riêng.

#### Bài toán 5: Dọn dẹp tệp vật lý khi xóa sản phẩm hoặc xóa tệp
- **Cái khó:** Nếu chỉ dùng lệnh `DELETE FROM TepSanPham` trong SQL Server, file vật lý nặng hàng GB vẫn nằm lại trên ổ đĩa máy chủ $\rightarrow$ Lâu ngày gây rác ổ đĩa trầm trọng (Orphaned Files).
- **Giải pháp:**
   - Viết nghiệp vụ trong `TepSanPhamService`: Khi xóa 1 tệp hoặc xóa sản phẩm, trước tiên xóa bản ghi trong CSDL, sau đó kiểm tra và thực hiện `File.Delete(fullPath)` trên ổ cứng để thu hồi dung lượng đĩa.

---

## 5. Thiết kế Hướng đối tượng (OOP Design) & Hợp đồng Dịch vụ

### 5.1. Thiết kế DTOs (`DPMS.Shared.DTOs`)
```csharp
namespace DPMS.Shared.DTOs
{
    // DTO phục vụ quản lý sản phẩm trên DataGridView Admin
    public class SanPhamManageDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public int? MaNhaPhatHanh { get; set; }
        public string TenNhaPhatHanh { get; set; }
        public decimal GiaBan { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public decimal DungLuongMB { get; set; }
        public string PhienBan { get; set; }
        public bool DangKinhDoanh { get; set; }
        public DateTime NgayTao { get; set; }
    }

    // DTO đại diện cho một tệp số đính kèm
    public class TepSanPhamDTO
    {
        public int MaTep { get; set; }
        public int MaSanPham { get; set; }
        public string TenTep { get; set; }
        public string DuongDanTep { get; set; }  // Đường dẫn tương đối: Storage/Products/1/Setup.zip
        public string DinhDangTep { get; set; }  // .zip, .exe, .rar, .iso, .pdf
        public long KichThuocByte { get; set; }
        public string MaHashSHA256 { get; set; }
        public string PhienBan { get; set; }
        public DateTime NgayTaiLen { get; set; }

        public string DungLuongHienThi => KichThuocByte >= 1024 * 1024 * 1024 
            ? $"{KichThuocByte / (1024.0 * 1024.0 * 1024.0):N2} GB" 
            : $"{KichThuocByte / (1024.0 * 1024.0):N2} MB";
    }

    // DTO quản lý danh mục
    public class DanhMucDTO
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public string MoTa { get; set; }
        public int SoLuongSanPham { get; set; }
        public bool TrangThai { get; set; }
    }

    // DTO quản lý hồ sơ cá nhân & đổi mật khẩu
    public class NguoiDungProfileDTO
    {
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string VaiTro { get; set; }
        public decimal SoDuVi { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
```

### 5.2. Hợp đồng Giao diện Dịch vụ (Service Interfaces)
```csharp
namespace DPMS.BLL.Interfaces
{
    // Nghiệp vụ Quản trị Sản phẩm
    public interface ISanPhamAdminService
    {
        List<SanPhamManageDTO> GetAllSanPham(string tuKhoa = null, int maDanhMuc = 0);
        int CreateSanPham(SanPhamManageDTO dto);
        bool UpdateSanPham(SanPhamManageDTO dto);
        bool ToggleKinhDoanh(int maSanPham, bool dangKinhDoanh);
    }

    // Nghiệp vụ Quản trị Tệp kỹ thuật số & Upload
    public interface ITepSanPhamService
    {
        List<TepSanPhamDTO> GetTepBySanPhamId(int maSanPham);
        Task<bool> UploadTepAsync(int maSanPham, string sourceFilePath, string phienBan, IProgress<int> progress);
        bool DeleteTep(int maTep);
    }

    // Nghiệp vụ Quản trị Danh mục
    public interface IDanhMucService
    {
        List<DanhMucDTO> GetAllDanhMuc();
        bool CreateDanhMuc(DanhMucDTO dto);
        bool UpdateDanhMuc(DanhMucDTO dto);
        bool DeleteDanhMuc(int maDanhMuc, out string errorMessage);
    }

    // Nghiệp vụ Hồ sơ cá nhân
    public interface IProfileService
    {
        NguoiDungProfileDTO GetProfile(int maNguoiDung);
        bool UpdateProfile(int maNguoiDung, string hoTen, string email, string soDienThoai);
        bool DoiMatKhau(int maNguoiDung, string matKhauCuPlain, string matKhauMoiPlain);
    }

    // Tiện ích File Storage Engine
    public interface IFileStorageManager
    {
        string ComputeSha256(string filePath);
        Task CopyFileWithProgressAsync(string sourcePath, string destPath, IProgress<int> progress);
        void DeletePhysicalFile(string relativeDbPath);
        string GetAbsolutePath(string relativeDbPath);
    }
}
```

---

## 6. Thiết kế Luồng Logic Xử lý (Logic Workflows)

### Luồng 1: Thêm/Sửa sản phẩm kèm Upload tệp kỹ thuật số (UI 10)
1. **Thêm sản phẩm gốc:**
   - Admin nhập Tên, Giá bán, Thể loại, Mô tả và bấm **[ ➕ Thêm sản phẩm ]**.
   - `ISanPhamAdminService.CreateSanPham` thực thi `INSERT INTO SanPham` và lấy lại `MaSanPham` vừa sinh (`SCOPE_IDENTITY()`).
2. **Upload tệp đính kèm bất đồng bộ:**
   - Admin bấm **[ 📁 Chọn tệp... ]** $\rightarrow$ chọn file `.zip` / `.exe` / `.rar`.
   - Giao diện gọi `ITepSanPhamService.UploadTepAsync`:
     - BLL tính toán mã băm SHA-256 ngầm qua `IFileStorageManager.ComputeSha256`.
     - Tạo thư mục tương đối `Storage/Products/{MaSanPham}`.
     - Sao chép file nhị phân theo khối buffer 64KB không làm đơ giao diện (`Task.Run` + `IProgress<int>` cập nhật `ProgressBar`).
     - Ghi nhận bản ghi mới vào bảng `TepSanPham`.
   - Nạp lại DataGridView tệp đính kèm con trên UI 10.

---

### Luồng 2: Quản lý danh mục & Kiểm tra ràng buộc khi xóa (UI 11)
1. **Thao tác Thêm/Sửa:**
   - Nhập Tên danh mục, Mô tả $\rightarrow$ BLL kiểm tra trùng lặp $\rightarrow$ DAL thực thi `INSERT` hoặc `UPDATE DanhMuc`.
2. **Thao tác Xóa (Chặn lỗi toàn vẹn):**
   - Admin chọn một danh mục và bấm **[ ❌ Xóa ]**.
   - `IDanhMucService.DeleteDanhMuc` kiểm tra trong CSDL: `SELECT COUNT(1) FROM SanPham WHERE MaDanhMuc = @MaDanhMuc`.
   - Nếu $\text{Số lượng} > 0$: Báo lỗi *"Không thể xóa danh mục này vì đang có X sản phẩm trực thuộc. Vui lòng chuyển danh mục sản phẩm trước!"*.
   - Nếu $\text{Số lượng} == 0$: Thực thi xóa an toàn.

---

### Luồng 3: Cập nhật thông tin hồ sơ, Đổi mật khẩu & Tùy chọn cài đặt (UI 8)
1. **Cập nhật thông tin cá nhân & Đồng bộ Realtime:**
   - Khách/Admin sửa Họ tên, Email, Số điện thoại $\rightarrow$ BLL kiểm tra định dạng email và số điện thoại hợp lệ $\rightarrow$ Cập nhật vào CSDL (`UPDATE NguoiDung`).
   - Gọi `UserSession.UpdateProfile(hoTen, email)` kích hoạt sự kiện `UserSession.SessionChanged`.
   - `MainForm` lắng nghe sự kiện qua `SessionChanged += UpdateUserInterfaceSession` và cập nhật tức thì nhãn câu chào `lblUserGreeting` ("Xin chào, {HoTen}") trên Header thanh điều hướng mà không cần người dùng thao tác F5 hay đăng nhập lại.
2. **Đổi mật khẩu bảo mật:**
   - Khách nhập Mật khẩu hiện tại, Mật khẩu mới, Xác nhận mật khẩu mới.
   - BLL băm mật khẩu cũ bằng SHA-256: `Hash(MatKhauCu)`.
   - Đối chiếu với `MatKhauHash` hiện tại trong CSDL. Nếu khớp, tính `Hash(MatKhauMoi)` và thực thi cập nhật `MatKhauHash`.
   - Báo đổi mật khẩu thành công và thông báo lưu ý bảo mật.
3. **Lưu cấu hình cài đặt ứng dụng vào CSDL:**
   - Người dùng tick/untick 4 tùy chọn (Thông báo email, Ghi nhớ đăng nhập, Tự động cập nhật, Xác nhận thanh toán).
   - Khi bấm nút **"Lưu cài đặt ứng dụng"**, `HoSoService.LuuCaiDat` thực thi truy vấn UPSERT vào bảng `CaiDatNguoiDung`:
     ```sql
     MERGE INTO CaiDatNguoiDung WITH (HOLDLOCK) AS target
     USING (SELECT @MaNguoiDung AS MaNguoiDung) AS source
     ON (target.MaNguoiDung = source.MaNguoiDung)
     WHEN MATCHED THEN
         UPDATE SET ThongBaoEmail = @ThongBaoEmail, GhiNhoDangNhap = @GhiNhoDangNhap,
                    TuDongCapNhat = @TuDongCapNhat, XacNhanThanhToan = @XacNhanThanhToan,
                    NgayCapNhat = GETDATE()
     WHEN NOT MATCHED THEN
         INSERT (MaNguoiDung, ThongBaoEmail, GhiNhoDangNhap, TuDongCapNhat, XacNhanThanhToan, NgayCapNhat)
         VALUES (@MaNguoiDung, @ThongBaoEmail, @GhiNhoDangNhap, @TuDongCapNhat, @XacNhanThanhToan, GETDATE());
     ```
   - Khi mở lại tab Hồ sơ, hệ thống tự động nạp cấu hình đã lưu để hiển thị lại chính xác trạng thái checkbox của người dùng.
4. **Hiển thị khung Nguyên tắc bảo mật tài khoản Digital Steam:**
   - Sử dụng layout tự co giãn linh hoạt: `lblSecurityContent.AutoSize = true`, giới hạn `MaximumSize = new Size(panelWidth - 40, 0)` và gán `panelSecurityBanner.Height = Math.Max(180, lblSecurityContent.Bottom + 25)`.
   - Đảm bảo hiển thị trọn vẹn 4 điều khoản bảo mật chuẩn Steam (Mật khẩu mạnh, Không chia sẻ OTP, Cảnh báo giao dịch ví, Kiểm tra hash SHA-256) mà không bao giờ bị cắt cụt phần bullet list bên dưới.
5. **Quy tắc thiết kế nút bấm (Button Policy):**
   - Không lạm dụng icon emoji trong text của các nút bấm hành động (Ví dụ dùng: `Lưu thay đổi hồ sơ`, `Lưu mật khẩu mới`, `Lưu cài đặt ứng dụng`). Giữ giao diện chuẩn mực, thanh lịch và đồng nhất giữa các màn hình.
