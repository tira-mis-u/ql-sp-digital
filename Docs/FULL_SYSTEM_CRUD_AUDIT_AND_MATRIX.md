# ĐẶC TẢ CHI TIẾT & MA TRẬN ĐỐI SOÁT TOÀN DIỆN VỀ CRUD (DPMS SYSTEM)
**Tài liệu tham chiếu chuẩn:** `Docs/FULL_SYSTEM_CRUD_AUDIT_AND_MATRIX.md`  
**Dự án:** Digital Product Management System (DPMS) - Nền tảng phân phối sản phẩm kỹ thuật số WinForms C# (.NET 10)  
**Mục đích:** Cung cấp bản đối soát kỹ thuật và nghiệp vụ chi tiết 100% về các thao tác **CRUD (Create - Read - Update - Delete / Thêm - Đọc/Xem - Sửa - Xóa)** cho toàn bộ 5 nhóm thành viên, phục vụ báo cáo bài tập lớn, giải trình đồ án và vấn đáp tự tin trước hội đồng phản biện.

---

## 1. TỔNG QUAN TRIẾT LÝ THIẾT KẾ CRUD TRONG HỆ THỐNG DPMS

Trong các hệ thống phân phối nội dung kỹ thuật số (tương tự Steam, Epic Games, GOG), các thao tác dữ liệu được chia làm 2 mô hình rõ rệt:
1. **Master Data CRUD (CRUD Quản trị Cổ điển):**
   - Áp dụng trên các bảng danh mục quản trị: `SanPham`, `TepSanPham`, `DanhMuc`.
   - Có đầy đủ 4 thao tác vật lý trực tiếp trên giao diện: Thêm mới (INSERT), Xem danh sách/lọc (SELECT), Chỉnh sửa thông số (UPDATE), Xóa dòng (DELETE).
2. **Business State Machine CRUD (CRUD Vòng đời Nghiệp vụ & Giao dịch):**
   - Áp dụng trên các thực thể khách hàng, tiền tệ và quyền sở hữu: `GioHang`, `MaGiamGiaNguoiDung`, `SoHuuSanPham`, `DanhGia`, `DonHang`, `NguoiDung`.
   - **Tuyệt đối tuân thủ nguyên tắc toàn vẹn kế toán & kiểm toán (Immutable Audit Trail):** Các thực thể tài chính như `DonHang`, `ThanhToan` không bao giờ được phép xóa cứng (Hard Delete). Thao tác "Delete" đối với tài khoản là **Khóa / Vô hiệu hóa (Soft Delete)**, đối với đơn hàng là **Hủy giao dịch (Soft Cancel)**.
   - Thao tác "Create" quyền sở hữu thư viện (`SoHuuSanPham`) diễn ra tự động thông qua **ACID Transaction** khi đơn hàng hoàn tất.

---

## 2. BẢNG MA TRẬN TRUY VẾT CRUD TỔNG HỢP (TRACEABILITY MATRIX)

| Nhóm | Thành viên phụ trách | Phân hệ & Thực thể CSDL | **C** (Create - Thêm) | **R** (Read - Đọc/Xem) | **U** (Update - Sửa) | **D** (Delete - Xóa/Gỡ) | Trạng thái Nghiệp vụ |
|:---:|---|---|---|---|---|---|:---:|
| **1** | **Lê Nguyễn Minh Trí** | **Đánh giá & Nhận xét**<br>`DanhGia`<br>(Kèm `SanPham` Store) | `btnSubmit`<br>Thêm nhận xét & chấm sao mới | `dgvReviews`, `ProductDetail`<br>Xem danh sách & điểm TB | `btnSubmit`<br>Cập nhật số sao & lời bình | `btnDelete`<br>Xóa đánh giá khỏi CSDL | **FULL CRUD 100%**<br>*(Verified Purchase)* |
| **2** | **Nguyễn Quốc Trung** | **Giỏ hàng & Kho Voucher**<br>`GioHang`, `MaGiamGiaNguoiDung`<br>(Kèm `NguoiDung.SoDuVi`) | - Thêm SP vào giỏ<br>- `btnCollect` (Lưu voucher) | `dgvCart`, `dgvVouchers`<br>Xem giỏ, xem ví voucher | - Tự cộng dồn SL giỏ<br>- Đổi trạng thái `DaSuDung` | - `btnRemoveItem` (Xóa món)<br>- `btnClearCart` (Dọn giỏ)<br>- `btnDelete` (Xóa voucher) | **FULL CRUD 100%**<br>*(1-Click Wallet Pay)* |
| **3** | **Phạm Trung Hiếu** | **Sản phẩm & Danh mục**<br>`SanPham`, `TepSanPham`,<br>`DanhMuc`, `HoSo` | `btnNew` + `btnSave`<br>Thêm SP & thêm Danh mục | `dgvProducts`, `dgvCategories`<br>Xem bảng, tìm kiếm, lọc | `btnSave`<br>Sửa SP & sửa Danh mục | `btnDelete`<br>Xóa SP, xóa Danh mục | **FULL CRUD 100%**<br>*(Chuẩn mực Master Data)* |
| **4** | **Trần Quốc Thuận** | **Tài khoản người dùng**<br>`NguoiDung`<br>(Kèm `DashboardStats`) | `RegisterControl`<br>Đăng ký tài khoản mới | `dgvUsers`<br>Xem ds, lọc vai trò, tìm kiếm | `btnLock` / `btnUnlock`<br>Khóa / Mở khóa tài khoản | **Soft Delete**<br>Vô hiệu hóa tài khoản (Chặn xóa cứng) | **ĐỦ CRUD NGHIỆP VỤ**<br>*(Audit Protection)* |
| **5** | **Phạm Văn Đam** | **Thư viện số & Đơn hàng**<br>`SoHuuSanPham`, `DonHang`,<br>`ChiTietDonHang` | - Cấp game vào thư viện<br>- Tạo đơn hàng qua Ví | `dgvLibrary`, `dgvOrders`<br>Xem kho game, lịch sử đơn | `CapNhatLanTai()` khi tải file<br>`btnApprove` / `btnCancelOrder` | `btnUninstall`<br>Gỡ game khỏi thư viện | **FULL CRUD 100%**<br>*(Tải tệp & Quản lý bản quyền)* |

---

## 3. ĐẶC TẢ CHI TIẾT CRUD CHO TỪNG PHÂN HỆ

### 3.1. NHÓM 1 – LÊ NGUYỄN MINH TRÍ: CỬA HÀNG & ĐÁNH GIÁ SẢN PHẨM
- **Giao diện phụ trách:**
  - `StoreViewControl.cs` (UI 2: Cửa hàng sản phẩm số)
  - `ProductDetailControl.cs` (UI 3: Trang chi tiết sản phẩm)
  - `ProductReviewControl.cs` (UI 14: Đánh giá & Nhận xét sản phẩm)
- **Thực thể CSDL chính:** `DanhGia` (Khóa chính `MaDanhGia`, Khóa ngoại `MaNguoiDung`, `MaSanPham`)

| Thao tác | Tầng Giao diện (WinForms) | Tầng Nghiệp vụ (BLL) | Tầng Truy vấn (DAL) | Câu lệnh SQL tương ứng |
|---|---|---|---|---|
| **CREATE**<br>(Thêm mới) | Nút `btnSubmit` ("Gửi nhận xét") trên `ProductReviewControl`. | `DanhGiaService.LuuDanhGia(userId, spId, sao, comment, out msg)` | `DanhGiaRepository.ThemDanhGia(dto)` | `INSERT INTO DanhGia (MaNguoiDung, MaSanPham, SoSao, BinhLuan, NgayTao) VALUES (@UserId, @SpId, @SoSao, @BinhLuan, GETDATE())` |
| **READ**<br>(Đọc / Xem) | - `dgvReviews` trên `ProductReviewControl`.<br>- Danh sách tóm tắt & điểm sao tại `ProductDetailControl`. | `DanhGiaService.LayDanhGiaTheoSanPham(spId)`<br>`DanhGiaService.LayDanhGiaCuaUser(userId, spId)` | `DanhGiaRepository.LayDanhGiaTheoSanPham(spId)`<br>`DanhGiaRepository.LayDanhGiaCuaUser(userId, spId)` | `SELECT dg.*, nd.HoTen, sp.TenSanPham FROM DanhGia dg INNER JOIN NguoiDung nd ON dg.MaNguoiDung = nd.MaNguoiDung WHERE dg.MaSanPham = @MaSanPham ORDER BY dg.NgayTao DESC` |
| **UPDATE**<br>(Chỉnh sửa) | Khi user đã có đánh giá, form tự nạp nội dung cũ, nút `btnSubmit` đổi nhãn thành **"Cập nhật đánh giá"**. | `DanhGiaService.LuuDanhGia(...)` (tự rẽ nhánh khi `existing != null`) | `DanhGiaRepository.CapNhatDanhGia(dto)` | `UPDATE DanhGia SET SoSao = @SoSao, BinhLuan = @BinhLuan, NgayCapNhat = GETDATE() WHERE MaDanhGia = @MaDanhGia AND MaNguoiDung = @UserId` |
| **DELETE**<br>(Xóa bỏ) | Nút `btnDelete` ("Xóa đánh giá") kèm hộp thoại xác nhận Yes/No. | `DanhGiaService.XoaDanhGia(maDanhGia, userId, out msg)` | `DanhGiaRepository.XoaDanhGia(maDanhGia, userId)` | `DELETE FROM DanhGia WHERE MaDanhGia = @MaDanhGia AND MaNguoiDung = @UserId` |

> **Điểm sáng bảo vệ đồ án:**  
> Hệ thống áp dụng quy tắc **Verified Purchase**: Hàm `DanhGiaService.DaMuaSanPham()` móc nối trực tiếp với `ThuVienRepository.DaSoHuu()`. Khách hàng bắt buộc phải sở hữu game trong Thư viện mới được phép mở form gửi đánh giá, ngăn chặn 100% review rác và đánh giá ảo.

---

### 3.2. NHÓM 2 – NGUYỄN QUỐC TRUNG: GIỎ HÀNG, VÍ TÀI KHOẢN & MÃ GIẢM GIÁ
- **Giao diện phụ trách:**
  - `CartControl.cs` (UI 4: Giỏ hàng & Thanh toán 1-Click)
  - `WalletControl.cs` (UI 5: Quản lý Ví Steam Wallet & Nạp tiền)
  - `VoucherControl.cs` (UI 15: Kho Voucher ưu đãi & Thu thập mã)
- **Thực thể CSDL chính:** `GioHang`, `MaGiamGiaNguoiDung`, `NguoiDung.SoDuVi`, `ThanhToan`

| Thực thể | Thao tác | Tầng Giao diện | Tầng Nghiệp vụ (BLL) & DAL | Câu lệnh SQL tương ứng |
|---|---|---|---|---|
| **Giỏ hàng**<br>(`GioHang`) | **C (Thêm)** | Bấm "Thêm vào giỏ" tại Store/Detail | `GioHangService.ThemVaoGioHang()`<br>`GioHangRepository.ThemVaoGioHang()` | `INSERT INTO GioHang (MaNguoiDung, MaSanPham, SoLuong, NgayThem) VALUES (@UserId, @SpId, 1, GETDATE())` |
| | **R (Xem)** | `dgvCart` trên `CartControl` | `GioHangService.LayGioHang(userId)`<br>`GioHangRepository.LayGioHang(userId)` | `SELECT gh.*, sp.TenSanPham, sp.GiaBan FROM GioHang gh INNER JOIN SanPham sp ON gh.MaSanPham = sp.MaSanPham WHERE gh.MaNguoiDung = @UserId` |
| | **U (Sửa)** | Thêm trùng SP trong Store sẽ tự động tăng số lượng | `GioHangRepository.CapNhatSoLuong(maGioHang, currentQty + soLuong)` | `UPDATE GioHang SET SoLuong = @SoLuong WHERE MaGioHang = @MaGioHang` |
| | **D (Xóa)** | - Nút `btnRemoveItem` (Xóa món lẻ)<br>- Nút `btnClearCart` (Dọn sạch giỏ) | `GioHangService.XoaKhoiGioHang()`<br>`GioHangRepository.XoaToanBoGioHang()` | `DELETE FROM GioHang WHERE MaGioHang = @MaGioHang`<br>`DELETE FROM GioHang WHERE MaNguoiDung = @UserId` |
| **Voucher**<br>(`MaGiamGiaNguoiDung`) | **C (Thêm)** | - Nhập mã `txtVoucherCode` + `btnCollect`<br>- Nút `btnCollectSelected` | `MaGiamGiaService.ThuThapVoucher()`<br>`MaGiamGiaRepository.LuuVoucherNguoiDung()` | `INSERT INTO MaGiamGiaNguoiDung (MaNguoiDung, MaGiamGiaId, DaSuDung, NgayNhan) VALUES (@UserId, @VoucherId, 0, GETDATE())` |
| | **R (Xem)** | `dgvVouchers` phân loại: *Chưa lưu, Đã trong ví, Đã sử dụng* | `MaGiamGiaService.LayDanhSachVoucher()`<br>`MaGiamGiaRepository.LayDanhSachVoucher()` | `SELECT mgg.*, ISNULL(ndv.DaSuDung, 0) AS DaSuDung FROM MaGiamGia mgg LEFT JOIN MaGiamGiaNguoiDung ndv ON mgg.MaGiamGiaId = ndv.MaGiamGiaId AND ndv.MaNguoiDung = @UserId` |
| | **U (Sửa)** | Áp dụng mã khi thanh toán đơn hàng | `ThucHienThanhToanDonHang()` tự cập nhật voucher | `UPDATE MaGiamGiaNguoiDung SET DaSuDung = 1, NgaySuDung = GETDATE() WHERE MaNguoiDung = @UserId AND MaGiamGiaId = @VoucherId` |
| | **D (Xóa)** | Nút `btnDelete` ("Xóa khỏi ví") | `MaGiamGiaService.XoaVoucherCaNhan()`<br>`MaGiamGiaRepository.XoaVoucherNguoiDung()` | `DELETE FROM MaGiamGiaNguoiDung WHERE MaGiamGiaId = @VoucherId AND MaNguoiDung = @UserId` |
| **Ví Steam**<br>(`SoDuVi`) | **Nạp/Trừ** | Nút `btnDeposit` / Nút `btnCheckout` | `ViTaiKhoanService.NapTien()`<br>`ThanhToanService.ThanhToanGioHang()` | `UPDATE NguoiDung SET SoDuVi = SoDuVi + @Amount WHERE MaNguoiDung = @UserId`<br>`UPDATE NguoiDung SET SoDuVi = SoDuVi - @Total WHERE MaNguoiDung = @UserId` |

---

### 3.3. NHÓM 3 – PHẠM TRUNG HIẾU: QUẢN TRỊ SẢN PHẨM, DANH MỤC & HỒ SƠ
- **Giao diện phụ trách:**
  - `ProductManageControl.cs` (UI 10: Quản lý sản phẩm & upload tệp số)
  - `CategoryManageControl.cs` (UI 11: Quản trị danh mục/thể loại)
  - `ProfileControl.cs` (UI 8: Hồ sơ cá nhân, đổi mật khẩu & cấu hình)
- **Thực thể CSDL chính:** `SanPham`, `TepSanPham`, `DanhMuc`, `CaiDatNguoiDung`

| Thực thể | Thao tác | Tầng Giao diện | Tầng Nghiệp vụ (BLL) & DAL | Câu lệnh SQL tương ứng |
|---|---|---|---|---|
| **Sản phẩm**<br>(`SanPham` & `TepSanPham`) | **CREATE** | Nút `btnNew` + `btnSave` (khi `txtId == 0`), chọn file qua `btnBrowseFile` | `SanPhamAdminService.LuuSanPham()`<br>`SanPhamAdminRepository.ThemSanPham()`<br>`TepSanPhamRepository.LuuTepSanPham()` | `INSERT INTO SanPham (TenSanPham, MoTa, GiaBan, HinhAnh, DungLuongMB, PhienBan, MaDanhMuc, DangKinhDoanh, NgayTao) VALUES (...); SELECT SCOPE_IDENTITY();`<br>`INSERT INTO TepSanPham (MaSanPham, TenTep, DuongDanTep, DinhDangTep, KichThuocByte, PhienBan) VALUES (...)` |
| | **READ** | `dgvProducts`, ô tìm kiếm `txtSearch` | `SanPhamAdminService.LayDanhSachSanPham()`<br>`SanPhamAdminRepository.LayDanhSachAdmin()` | `SELECT sp.*, dm.TenDanhMuc FROM SanPham sp INNER JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc WHERE sp.TenSanPham LIKE @TuKhoa ORDER BY sp.MaSanPham DESC` |
| | **UPDATE** | Chọn dòng trên lưới -> sửa ô nhập -> bấm `btnSave` (khi `txtId > 0`) | `SanPhamAdminService.LuuSanPham()`<br>`SanPhamAdminRepository.CapNhatSanPham()` | `UPDATE SanPham SET TenSanPham = @Ten, MoTa = @MoTa, GiaBan = @Gia, DungLuongMB = @DungLuong, PhienBan = @PhienBan, MaDanhMuc = @MaDanhMuc, DangKinhDoanh = @Status, NgayCapNhat = GETDATE() WHERE MaSanPham = @Id` |
| | **DELETE** | Chọn dòng -> bấm `btnDelete` (Popup Yes/No) | `SanPhamAdminService.XoaSanPham()`<br>`SanPhamAdminRepository.XoaSanPham()` | `DELETE FROM SanPham WHERE MaSanPham = @MaSanPham` |
| **Danh mục**<br>(`DanhMuc`) | **CREATE** | `btnNew` + `btnSave` (khi `txtId == 0`) | `DanhMucService.LuuDanhMuc()`<br>`DanhMucRepository.ThemDanhMuc()` | `INSERT INTO DanhMuc (TenDanhMuc, MoTa, TrangThai) VALUES (@Ten, @MoTa, @TrangThai)` |
| | **READ** | `dgvCategories` hiển thị kèm số lượng SP trực thuộc | `DanhMucService.LayTatCaDanhMuc()`<br>`DanhMucRepository.LayTatCaDanhMuc()` | `SELECT dm.*, COUNT(sp.MaSanPham) AS SoLuongSanPham FROM DanhMuc dm LEFT JOIN SanPham sp ON dm.MaDanhMuc = sp.MaDanhMuc GROUP BY dm.MaDanhMuc, dm.TenDanhMuc, dm.MoTa, dm.TrangThai` |
| | **UPDATE** | Chọn dòng -> sửa dữ liệu -> bấm `btnSave` (khi `txtId > 0`) | `DanhMucService.LuuDanhMuc()`<br>`DanhMucRepository.CapNhatDanhMuc()` | `UPDATE DanhMuc SET TenDanhMuc = @Ten, MoTa = @MoTa, TrangThai = @TrangThai WHERE MaDanhMuc = @MaDanhMuc` |
| | **DELETE** | Chọn dòng -> bấm `btnDelete` | `DanhMucService.XoaDanhMuc()`<br>`DanhMucRepository.XoaDanhMuc()` | `DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc` *(Có bẫy kiểm tra: nếu `COUNT(SanPham) > 0` thì chặn xóa và cảnh báo an toàn)* |

---

### 3.4. NHÓM 4 – TRẦN QUỐC THUẬN: XÁC THỰC, QUẢN TRỊ USER & DASHBOARD
- **Giao diện phụ trách:**
  - `LoginControl.cs` & `RegisterControl.cs` (UI 1: Đăng nhập & Đăng ký)
  - `UserManageControl.cs` (UI 12: Quản lý danh sách tài khoản & Khóa/Mở khóa)
  - `DashboardControl.cs` (UI 9: Dashboard thống kê KPI toàn sàn)
- **Thực thể CSDL chính:** `NguoiDung` (Kèm bảng thống kê tổng hợp `DonHang`, `ChiTietDonHang`)

| Thao tác | Tầng Giao diện (WinForms) | Tầng Nghiệp vụ (BLL) | Tầng Truy vấn (DAL) | Câu lệnh SQL tương ứng |
|---|---|---|---|---|
| **CREATE**<br>(Tạo mới) | Màn hình `RegisterControl` -> nhập Username, Password, Email, Họ tên -> bấm nút Đăng ký. | `AuthService.DangKy(dto, password, out msg)` (Mật khẩu lưu trực tiếp dễ hiểu) | `AuthRepository.DangKy(user, password)` | `INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, VaiTro, SoDuVi, TrangThai, NgayTao) VALUES (@User, @MatKhau, @Ten, @Email, @Phone, 'KhachHang', 0, 1, GETDATE())` |
| **READ**<br>(Đọc / Xem) | - Màn hình `LoginControl` (Đăng nhập).<br>- `dgvUsers` trên `UserManageControl` (Lọc ComboBox `VaiTro` + ô tìm kiếm `txtSearch`). | `AuthService.DangNhap()`<br>`NguoiDungService.LayDanhSachNguoiDung(vaiTro, tuKhoa)` | `AuthRepository.DangNhap()`<br>`NguoiDungRepository.LayDanhSachNguoiDung()` | `SELECT MaNguoiDung, TenDangNhap, HoTen, Email, SoDienThoai, VaiTro, SoDuVi, TrangThai, NgayTao FROM NguoiDung WHERE (@VaiTro IS NULL OR VaiTro = @VaiTro) AND (@TuKhoa IS NULL OR TenDangNhap LIKE @TuKhoa OR HoTen LIKE @TuKhoa) ORDER BY MaNguoiDung DESC` |
| **UPDATE**<br>(Cập nhật) | Nút `btnLock` ("Khóa tài khoản") và `btnUnlock` ("Mở khóa tài khoản") trên `UserManageControl`. | `NguoiDungService.KhoaTaiKhoan(id, out msg)`<br>`NguoiDungService.MoKhoaTaiKhoan(id, out msg)` | `NguoiDungRepository.CapNhatTrangThaiKhoa(id, trangThai, out msg)` | `UPDATE NguoiDung SET TrangThai = @TrangThai WHERE MaNguoiDung = @MaNguoiDung` |
| **DELETE**<br>(Vô hiệu hóa) | **Áp dụng Soft Delete chuẩn doanh nghiệp:** Nhấn `btnLock` -> Vô hiệu hóa quyền truy cập, đưa tài khoản về `TrangThai = 0`. | Bẫy chặn: Cấm Admin tự khóa chính mình (`UserSession.CurrentUserId == id`). | Được xử lý an toàn qua `CapNhatTrangThaiKhoa`. | `UPDATE NguoiDung SET TrangThai = 0 WHERE MaNguoiDung = @MaNguoiDung` |

> **Giải trình phản biện đồ án (Tại sao không dùng Hard Delete `DELETE FROM NguoiDung`):**  
> *Trong hệ thống thương mại điện tử thực tế, tài khoản người dùng liên kết trực tiếp với bảng `DonHang`, `ThanhToan`, `SoHuuSanPham`, `DanhGia`. Nếu thực hiện lệnh `DELETE` vật lý, cơ sở dữ liệu sẽ lập tức kích hoạt lỗi xung đột ràng buộc khóa ngoại (Foreign Key Reference Constraint). Việc xóa cứng còn phá hủy tính toàn vẹn của sổ cái kế toán và lịch sử giao dịch nạp tiền. Do đó, hệ thống bắt buộc áp dụng **Soft Delete (Khóa tài khoản)** để vừa đình chỉ người dùng vi phạm, vừa bảo lưu 100% bằng chứng kiểm toán pháp lý.*

---

### 3.5. NHÓM 5 – PHẠM VĂN ĐAM: ĐƠN HÀNG, LỊCH SỬ GIAO DỊCH & THƯ VIỆN BẢN QUYỀN
- **Giao diện phụ trách:**
  - `LibraryControl.cs` (UI 6: Kho game & sản phẩm số đã sở hữu)
  - `OrderHistoryControl.cs` (UI 7: Lịch sử đơn hàng cá nhân)
  - `OrderManageControl.cs` (UI 13: Quản lý và duyệt đơn hàng toàn sàn)
- **Thực thể CSDL chính:** `SoHuuSanPham`, `DonHang`, `ChiTietDonHang`

| Thực thể | Thao tác | Tầng Giao diện | Tầng Nghiệp vụ (BLL) & DAL | Câu lệnh SQL tương ứng |
|---|---|---|---|---|
| **Thư viện số**<br>(`SoHuuSanPham`) | **CREATE** | Tự động sinh ra khi đơn hàng hoàn tất hoặc Admin duyệt thủ công đơn hàng | `ThucHienThanhToanDonHang()`<br>`CapNhatTrangThaiDonHang()` | `INSERT INTO SoHuuSanPham (MaNguoiDung, MaSanPham, MaDonHang, NgaySoHuu, SoLanTai) VALUES (@UserId, @SpId, @OrderId, GETDATE(), 0)` |
| | **READ** | `dgvLibrary` trên `LibraryControl`, hiển thị phiên bản, dung lượng, số lần tải | `ThuVienService.LayKhoThuVien(userId)`<br>`ThuVienRepository.LayKhoThuVien(userId)` | `SELECT sh.*, sp.TenSanPham, sp.PhienBan, sp.DungLuongMB, dm.TenDanhMuc, tp.DuongDanTep FROM SoHuuSanPham sh INNER JOIN SanPham sp ON sh.MaSanPham = sp.MaSanPham INNER JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc LEFT JOIN TepSanPham tp ON sp.MaSanPham = tp.MaSanPham WHERE sh.MaNguoiDung = @UserId` |
| | **UPDATE** | Bấm `btnDownload`: Tải tệp về máy và tự động cập nhật số lần tải | `ThuVienService.TaiXuongTepAsync()`<br>`ThuVienRepository.CapNhatLanTai(maSoHuu)` | `UPDATE SoHuuSanPham SET SoLanTai = SoLanTai + 1, LanTaiCuoi = GETDATE() WHERE MaSoHuu = @MaSoHuu` |
| | **DELETE** | Chọn game -> Bấm nút `btnUninstall` ("Gỡ khỏi thư viện") -> Xác nhận | `ThuVienService.GoKhoiThuVien()`<br>`ThuVienRepository.GoKhoiThuVien()` | `DELETE FROM SoHuuSanPham WHERE MaSoHuu = @MaSoHuu AND MaNguoiDung = @UserId` |
| **Đơn hàng**<br>(`DonHang`) | **CREATE** | Khách thanh toán giỏ hàng bằng Ví Steam | `ThanhToanService.ThanhToanGioHang()`<br>`DonHangRepository.ThucHienThanhToanDonHang()` | Khởi tạo giao dịch ACID tạo mã `ORD-yyyyMMddHHmmss-XXXX`, lưu `DonHang` và `ChiTietDonHang` |
| | **READ** | `dgvOrders` trên `OrderManageControl` & `OrderHistoryControl` (Chống lỗ hổng IDOR) | `DonHangService.LayTatCaDonHang()`<br>`DonHangService.LayLichSuDonHang()` | `SELECT dh.*, nd.HoTen, mgg.MaCode FROM DonHang dh INNER JOIN NguoiDung nd ON dh.MaNguoiDung = nd.MaNguoiDung LEFT JOIN MaGiamGia mgg ON dh.MaGiamGiaId = mgg.MaGiamGiaId ORDER BY dh.NgayTao DESC` |
| | **UPDATE** | Nút `btnApprove` (Kích hoạt đơn sang "Hoàn thành") và `btnCancelOrder` (Chuyển sang "Đã hủy") | `DonHangService.DuyetDonHang()`<br>`DonHangService.HuyDonHang()` | `UPDATE DonHang SET TrangThaiDonHang = @Status WHERE MaDonHang = @MaDonHang` |
| | **DELETE** | **Cơ chế Soft Cancel:** Không bao giờ xóa chứng từ tài chính. Đơn bị từ chối chuyển sang trạng thái "Đã hủy". | Được quản lý thông qua `HuyDonHang()`. | `UPDATE DonHang SET TrangThaiDonHang = N'Đã hủy' WHERE MaDonHang = @MaDonHang` |

---

## 4. BỘ CÂU HỎI VẤN ĐÁP PHÒNG THỦ CRUD TRƯỚC HỘI ĐỒNG

### Câu hỏi 1: "Tại sao nhóm 4 phụ trách Quản lý người dùng mà không thấy nút Xóa người dùng trên giao diện?"
- **Trả lời đanh thép:**  
  *"Thưa thầy/cô, trong các hệ thống phần mềm thương mại thực tế, tài khoản người dùng gắn liền với lịch sử nạp tiền vào ví, hóa đơn mua hàng và bản quyền game đã cấp. Nếu ta thực hiện lệnh `DELETE FROM NguoiDung`, SQL Server sẽ lập tức báo lỗi vi phạm khóa ngoại (Foreign Key Constraint) với bảng `DonHang` và `SoHuuSanPham`. Quan trọng hơn, xóa vật lý sẽ làm sai lệch sổ cái kế toán và mất dữ liệu đối soát tài chính. Vì vậy, em thiết kế chức năng theo chuẩn **Soft Delete (Khóa tài khoản)**: tài khoản bị khóa (`TrangThai = 0`) sẽ không thể đăng nhập hay mua hàng, nhưng toàn bộ lịch sử giao dịch của họ vẫn được bảo lưu nguyên vẹn để kiểm toán."*

### Câu hỏi 2: "Tại sao đơn hàng ở nhóm 5 không có nút Xóa đơn hàng?"
- **Trả lời đanh thép:**  
  *"Thưa thầy/cô, đơn hàng là chứng từ kế toán pháp lý có phát sinh dòng tiền. Không một sàn thương mại điện tử nào (như Shopee, Steam, Amazon) cho phép xóa một đơn hàng đã được tạo ra. Thay vào đó, nhóm em cài đặt thao tác **Hủy đơn hàng (Soft Cancel)** để chuyển trạng thái sang `Đã hủy`. Toàn bộ dữ liệu doanh thu, mã giảm giá và chi tiết món hàng vẫn được lưu giữ phục vụ việc đối soát và lập báo cáo tài chính trên Dashboard của Nhóm 4."*

### Câu hỏi 3: "Nhóm 1 làm phân hệ Cửa hàng cho khách thì CRUD nằm ở đâu?"
- **Trả lời đanh thép:**  
  *"Thưa thầy/cô, trên góc độ cửa hàng, khách hàng duyệt xem, tìm kiếm và lọc danh mục sản phẩm (Read). Nhưng phân hệ của em còn có chức năng chuyên sâu là **Đánh giá & Nhận xét sản phẩm (Reviews & Ratings)** theo chuẩn Steam với **đầy đủ 100% 4 thao tác CRUD**: Khách hàng tạo nhận xét mới (Create), xem điểm sao và bài đánh giá cộng đồng (Read), chỉnh sửa lại bài nhận xét và số sao đã chấm (Update), và xóa bài đánh giá của chính mình (Delete). Đặc biệt, hệ thống có bẫy nghiệp vụ **Verified Purchase**: chỉ những tài khoản đã thực sự mua và sở hữu game trong Thư viện mới được phép gửi đánh giá."*

### Câu hỏi 4: "Giỏ hàng ở nhóm 2 có thao tác Update số lượng không?"
- **Trả lời đanh thép:**  
  *"Thưa thầy/cô, có ạ. Ở tầng BLL và DAL, hàm `CapNhatSoLuong()` được cài đặt đầy đủ. Khi khách hàng nhấn thêm một sản phẩm đã có sẵn trong giỏ, hệ thống tự động kiểm tra sự tồn tại và gọi lệnh UPDATE tăng số lượng `currentQty + soLuong`. Khi thanh toán thành công, hệ thống tự động gọi lệnh DELETE dọn sạch giỏ hàng. Ngoài ra, phân hệ của em còn quản lý **Kho Voucher cá nhân** với đầy đủ CRUD: Thu thập mã vào ví (Create), Xem danh sách ưu đãi (Read), Cập nhật trạng thái đã dùng khi thanh toán (Update), và Xóa mã khỏi ví (Delete)."*

---

## 5. KẾT LUẬN KIỂM ĐỊNH TÍNH ĐẦY ĐỦ

Qua quá trình rà soát và đối chiếu trực tiếp từ mã nguồn C# và lược đồ CSDL:
- **100% các thành viên (Nhóm 1 đến Nhóm 5) đều hoàn thành trọn vẹn và vượt chuẩn yêu cầu CRUD của đề tài.**
- Hệ thống phân định rạch ròi giữa **CRUD quản trị danh mục (Master Data)** và **CRUD vòng đời giao dịch (Business Lifecycle)**, thể hiện tư duy thiết kế phần mềm doanh nghiệp trưởng thành, an toàn và sẵn sàng bảo vệ đạt điểm tuyệt đối trước bất kỳ hội đồng phản biện nào.
