# ĐẶC TẢ LUỒNG NÚT BẤM & HÀM XỬ LÝ - NHÓM 3: SẢN PHẨM, DANH MỤC & HỒ SƠ
**Thành viên phụ trách:** Phạm Trung Hiếu  
**Màn hình quản lý:**
1. UI 10: `ProductManageControl` (Quản trị kho hàng hóa số & Tệp phần mềm)
2. UI 11: `CategoryManageControl` (Quản lý danh mục thể loại)
3. UI 8: `ProfileControl` (Hồ sơ người dùng, Đổi mật khẩu & Cài đặt trải nghiệm)

---

## 1. MÀN HÌNH UI 10: `ProductManageControl`

### 1.1. Sơ đồ luồng Tải tệp lên & Tính mã SHA-256

```text
[ Admin bấm nút "📂 Chọn tệp số..." trên ProductManageControl ]
                               |
                               v
               Mở OpenFileDialog (Chọn .zip, .exe, .iso, .rar)
                               |
                               v
         ProductManageControl.BtnBrowseFile_Click()
                               |
                               | 1. Đọc kích thước FileInfo: file.Length (Bytes)
                               | 2. Tính toán mã băm toàn vẹn:
                               |    SecurityHelper.ComputeFileSha256(filePath)
                               |    -> Dùng luồng FileStream + SHA256.Create()
                               v
         Hiển thị lên giao diện:
         - txtFileSize: Quy đổi sang MB (DungLuongMB)
         - txtFileHash: Chuỗi Hex 64 ký tự SHA-256
                               |
                               v
         [ Admin điền Tên, Giá, Mô tả và bấm "💾 LƯU SẢN PHẨM" ]
                               |
                               v
         SanPhamAdminService.ThemSanPham(...) / CapNhatSanPham(...)
                               |
                               v
         1. SanPhamAdminRepository: INSERT / UPDATE bảng SanPham
         2. TepSanPhamRepository:   INSERT / UPDATE bảng TepSanPham (Lưu Metadata & Hash)
                               |
                               v
         Bắn sự kiện: SanPhamAdminService.NotifyProductCatalogChanged()
                               |
                               v
         StoreViewControl tự động tải lại lưới game mới nhất cho khách hàng
```

---

### 1.2. Chi tiết từng nút bấm trên UI 10

#### 1. Nút `btnBrowseFile` ("Chọn tệp tải lên...")
* **Hàm UI bắt sự kiện:** `BtnBrowseFile_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Admin chuẩn bị xuất bản hoặc cập nhật bản vá cho một tựa game/phần mềm.
* **Quy trình hoạt động:**
  1. Mở `OpenFileDialog` với bộ lọc: `Tệp nén/Cài đặt (*.zip;*.exe;*.rar;*.iso;*.pdf)|*.zip;*.exe;*.rar;*.iso;*.pdf`.
  2. Lấy thông tin `FileInfo info = new FileInfo(ofd.FileName)`.
  3. Tính mã băm SHA-256 nguồn thông qua thuật toán mã hóa đối soát:
     ```csharp
     using (var sha = SHA256.Create())
     using (var stream = File.OpenRead(ofd.FileName))
     {
         byte[] hash = sha.ComputeHash(stream);
         _uploadedFileHash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
     }
     ```
  4. Lưu đường dẫn tương đối dự kiến: `Storage/Products/{fileName}`.
  5. Cập nhật các ô Textbox hiển thị: Tên tệp, Định dạng, Dung lượng MB, Mã Hash SHA-256.

#### 2. Nút `btnSave` ("Lưu thông tin sản phẩm & Tệp số")
* **Hàm UI bắt sự kiện:** `BtnSave_Click(object? sender, EventArgs e)`
* **Xác thực trước khi lưu:**
  * Tên sản phẩm không rỗng, dưới 200 ký tự.
  * Giá bán $\ge 0$ (cho phép sản phẩm 0đ miễn phí).
  * Đã chọn danh mục hợp lệ.
* **Hàm BLL được gọi:**
  * Nếu là tạo mới (`_selectedProductId == 0`): `SanPhamAdminService.ThemSanPham(dto, tepDto, out thongBao)`
  * Nếu là cập nhật (`_selectedProductId > 0`): `SanPhamAdminService.CapNhatSanPham(dto, tepDto, out thongBao)`
* **Hàm DAL được gọi:**
  1. `SanPhamAdminRepository.ThemSanPham` / `CapNhatSanPham`.
  2. `TepSanPhamRepository.LuuThongTinTep(tepDto)`.
* **Câu lệnh SQL thực thi:**
  ```sql
  -- Cập nhật thông tin sản phẩm
  UPDATE SanPham
  SET TenSanPham = @TenSanPham, GiaBan = @GiaBan, MoTa = @MoTa,
      DungLuongMB = @DungLuongMB, PhienBan = @PhienBan, MaDanhMuc = @MaDanhMuc, NgayCapNhat = GETDATE()
  WHERE MaSanPham = @MaSanPham;

  -- Lưu Metadata tệp vật lý và mã băm
  IF EXISTS (SELECT 1 FROM TepSanPham WHERE MaSanPham = @MaSanPham)
      UPDATE TepSanPham 
      SET TenTep = @TenTep, DuongDanTep = @DuongDan, DinhDangTep = @DinhDang,
          KichThuocByte = @Byte, MaHashSHA256 = @Hash, PhienBan = @PhienBan, NgayTaiLen = GETDATE()
      WHERE MaSanPham = @MaSanPham;
  ELSE
      INSERT INTO TepSanPham (MaSanPham, TenTep, DuongDanTep, DinhDangTep, KichThuocByte, MaHashSHA256, PhienBan, NgayTaiLen)
      VALUES (@MaSanPham, @TenTep, @DuongDan, @DinhDang, @Byte, @Hash, @PhienBan, GETDATE());
  ```
* **Sự kiện bắn ra:** `SanPhamAdminService.NotifyProductCatalogChanged()`.

#### 3. Nút `btnDelete` ("Ngừng kinh doanh / Mở lại" - Soft Delete)
* **Hàm UI bắt sự kiện:** `BtnDelete_Click(object? sender, EventArgs e)`
* **Nghiệp vụ bảo vệ:** Không bao giờ dùng lệnh `DELETE FROM SanPham` vì sẽ vi phạm khóa ngoại với `ChiTietDonHang` và tước đoạt quyền tải game của những khách hàng đã mua trước đó!
* **Hàm BLL được gọi:** `SanPhamAdminService.DoiTrangThaiKinhDoanh(int maSanPham, out string thongBao)`
* **Hàm DAL được gọi:** `SanPhamAdminRepository.CapNhatTrangThaiKinhDoanh(maSanPham, !dangKinhDoanh)`
* **SQL thực thi:** `UPDATE SanPham SET DangKinhDoanh = ~DangKinhDoanh WHERE MaSanPham = @MaSanPham;`
* **Sự kiện bắn ra:** `SanPhamAdminService.NotifyProductCatalogChanged()`.

---

## 2. MÀN HÌNH UI 11: `CategoryManageControl`

#### 4. Nút `btnSave` ("Lưu danh mục")
* **Hàm UI bắt sự kiện:** `BtnSave_Click(object? sender, EventArgs e)`
* **Hàm BLL:** `DanhMucService.ThemDanhMuc` / `DanhMucService.CapNhatDanhMuc`.
* **Hàm DAL:** `DanhMucRepository.ThemDanhMuc` / `DanhMucRepository.CapNhatDanhMuc`.
* **Sự kiện bắn ra:** `SanPhamAdminService.NotifyCategoryCatalogChanged()`.

#### 5. Nút `btnDelete` ("Xóa danh mục")
* **Hàm UI bắt sự kiện:** `BtnDelete_Click(object? sender, EventArgs e)`
* **Luật nghiệp vụ:** Kiểm tra `SELECT COUNT(1) FROM SanPham WHERE MaDanhMuc = @MaDanhMuc`. Nếu có ít nhất 1 sản phẩm đang thuộc danh mục này $\to$ Từ chối xóa và báo lỗi: *"Danh mục đang có sản phẩm liên kết, không thể xóa!"*.

---

## 3. MÀN HÌNH UI 8: `ProfileControl`

#### 6. Nút `btnUpdateProfile` ("Cập nhật thông tin cá nhân")
* **Hàm UI bắt sự kiện:** `BtnUpdateProfile_Click(object? sender, EventArgs e)`
* **Hàm BLL & DAL:** `HoSoRepository.CapNhatHoSo(userId, hoTen, email, sdt, out msg)`.
* **Kiểm tra trùng lặp:** `SELECT COUNT(1) FROM NguoiDung WHERE Email = @Email AND MaNguoiDung <> @MaNguoiDung;`.
* **Sự kiện bắn ra:** `UserSession.UpdateProfile(hoTen, email)` $\to$ Header tự động đổi tên hiển thị người dùng mà không cần đăng nhập lại.

#### 7. Nút `btnChangePassword` ("Đổi mật khẩu")
* **Hàm UI bắt sự kiện:** `BtnChangePassword_Click(object? sender, EventArgs e)`
* **Quy trình bảo mật:**
  1. Đối chiếu mật khẩu cũ với `MatKhauHash` trong CSDL bằng `SecurityHelper.VerifyPassword(matKhauCu, storedHash)`.
  2. Băm SHA-256 mật khẩu mới: `SecurityHelper.ComputeSha256Hash(matKhauMoi)`.
  3. Cập nhật `UPDATE NguoiDung SET MatKhauHash = @NewHash WHERE MaNguoiDung = @UserId;`.

#### 8. Nút `btnSaveSettings` ("Lưu cài đặt trải nghiệm")
* **Hàm UI bắt sự kiện:** `BtnSaveSettings_Click(object? sender, EventArgs e)`
* **Dữ liệu lưu trữ:** 4 thuộc tính trải nghiệm người dùng trong bảng `CaiDatNguoiDung`:
  * `chkEmailNotify.Checked` $\to$ `ThongBaoEmail`
  * `chkRememberLogin.Checked` $\to$ `GhiNhoDangNhap`
  * `chkAutoUpdate.Checked` $\to$ `TuDongCapNhat`
  * `chkConfirmPayment.Checked` $\to$ `XacNhanThanhToan` (Bật/tắt hộp thoại xác nhận khi bấm 1-Click Checkout).
* **Hàm DAL:** `HoSoRepository.LuuCaiDat(...)`.
