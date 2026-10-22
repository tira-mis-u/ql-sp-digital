# ĐẶC TẢ LUỒNG NÚT BẤM & HÀM XỬ LÝ - NHÓM 3: SẢN PHẨM, DANH MỤC & HỒ SƠ
**Thành viên phụ trách:** Phạm Trung Hiếu  
**Màn hình quản lý:**
1. UI 10: `ProductManageControl` (Quản trị kho hàng hóa số & Tệp phần mềm)
2. UI 11: `CategoryManageControl` (Quản lý danh mục thể loại)
3. UI 8: `ProfileControl` (Hồ sơ người dùng, Đổi mật khẩu & Cài đặt trải nghiệm)

---

## 1. MÀN HÌNH UI 10: `ProductManageControl`

### 1.1. Sơ đồ luồng Tải tệp lên & Lưu siêu dữ liệu sản phẩm

```text
[ Admin bấm nút "📂 Chọn tệp số..." trên ProductManageControl ]
                               |
                               v
               Mở OpenFileDialog (Chọn .zip, .exe, .iso, .rar)
                               |
                               v
         ProductManageControl.BtnBrowseFile_Click()
                               |
                                | 1. Mở OpenFileDialog chọn tệp (.zip, .exe, .rar, .iso)
                                | 2. Đọc thông tin FileInfo:
                                |    - txtFilePath.Text = ofd.SafeFileName
                                |    - txtSize.Text = Math.Round(fi.Length / (1024 * 1024), 2)
                                v
          Hiển thị lên giao diện:
          - txtFilePath: Tên tệp số đã chọn
          - txtSize: Dung lượng tệp quy đổi sang MB
                               |
                               v
         [ Admin điền Tên, Giá, Mô tả và bấm "💾 LƯU SẢN PHẨM" ]
                               |
                               v
         SanPhamAdminService.ThemSanPham(...) / CapNhatSanPham(...)
                               |
                               v
         1. SanPhamAdminRepository: INSERT / UPDATE bảng SanPham
         2. TepSanPhamRepository:   INSERT / UPDATE bảng TepSanPham (Lưu Metadata: Tên, Đường dẫn, Kích thước)
                               |
                               v
         Reload lưới sản phẩm và thông báo thành công (không dùng static event)
                               |
                               v
         StoreViewControl tự động tải lại lưới game mới nhất cho khách hàng
```

---

### 1.2. Chi tiết từng nút bấm trên UI 10

#### 1. Nút `btnBrowseFile` ("Chọn...")
* **Hàm UI bắt sự kiện:** `BtnBrowseFile_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Admin chuẩn bị xuất bản hoặc cập nhật bản vá cho một tựa game/phần mềm.
* **Quy trình hoạt động:**
  1. Mở `OpenFileDialog` với bộ lọc: `Tệp phần mềm / dữ liệu (*.zip;*.exe;*.rar;*.iso)|*.zip;*.exe;*.rar;*.iso|Tất cả tệp (*.*)|*.*`.
  2. Lấy thông tin `FileInfo fi = new FileInfo(ofd.FileName)`.
  3. Ghi nhớ đường dẫn tệp nguồn `_selectedSourceFilePath = ofd.FileName`.
  4. Cập nhật các ô Textbox hiển thị:
     * `txtFilePath.Text = ofd.SafeFileName`: Tên tệp gốc.
     * `txtSize.Text = Math.Round((decimal)fi.Length / (1024 * 1024), 2).ToString()`: Dung lượng tính bằng MB.

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

  -- Lưu Metadata tệp phần mềm
  IF EXISTS (SELECT 1 FROM TepSanPham WHERE MaSanPham = @MaSanPham)
  BEGIN
      UPDATE TepSanPham 
      SET TenTep = @TenTep, DuongDanTep = @DuongDanTep, DinhDangTep = @DinhDangTep,
          KichThuocByte = @KichThuocByte, PhienBan = @PhienBan, NgayTaiLen = GETDATE()
      WHERE MaSanPham = @MaSanPham;
  END
  ELSE
  BEGIN
      INSERT INTO TepSanPham (MaSanPham, TenTep, DuongDanTep, DinhDangTep, KichThuocByte, PhienBan, NgayTaiLen)
      VALUES (@MaSanPham, @TenTep, @DuongDanTep, @DinhDangTep, @KichThuocByte, @PhienBan, GETDATE());
  END;
  ```
* **Cập nhật giao diện:** Gọi `LoadProducts()` nạp lại danh sách sản phẩm.

#### 3. Nút `btnDelete` ("Ngừng kinh doanh / Mở lại" - Soft Delete)
* **Hàm UI bắt sự kiện:** `BtnDelete_Click(object? sender, EventArgs e)`
* **Nghiệp vụ bảo vệ:** Không bao giờ dùng lệnh `DELETE FROM SanPham` vì sẽ vi phạm khóa ngoại với `ChiTietDonHang` và tước đoạt quyền tải game của những khách hàng đã mua trước đó!
* **Hàm BLL được gọi:** `SanPhamAdminService.DoiTrangThaiKinhDoanh(int maSanPham, out string thongBao)`
* **Hàm DAL được gọi:** `SanPhamAdminRepository.CapNhatTrangThaiKinhDoanh(maSanPham, !dangKinhDoanh)`
* **SQL thực thi:** `UPDATE SanPham SET DangKinhDoanh = ~DangKinhDoanh WHERE MaSanPham = @MaSanPham;`
* **Cập nhật giao diện:** Gọi `LoadProducts()` nạp lại danh sách sản phẩm.

---

## 2. MÀN HÌNH UI 11: `CategoryManageControl`

#### 4. Nút `btnSave` ("Lưu danh mục")
* **Hàm UI bắt sự kiện:** `BtnSave_Click(object? sender, EventArgs e)`
* **Hàm BLL:** `DanhMucService.ThemDanhMuc` / `DanhMucService.CapNhatDanhMuc`.
* **Hàm DAL:** `DanhMucRepository.ThemDanhMuc` / `DanhMucRepository.CapNhatDanhMuc`.
* **Cập nhật giao diện:** Gọi `LoadCategories()` nạp lại danh mục.

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
* **Quy trình thực thi:**
  1. Kiểm tra mật khẩu hiện tại với `MatKhau` trong CSDL qua `HoSoRepository.DoiMatKhau`.
  2. Kiểm tra mật khẩu mới và xác nhận mật khẩu mới trùng khớp (tối thiểu 6 ký tự).
  3. Cập nhật `UPDATE NguoiDung SET MatKhau = @NewPass WHERE MaNguoiDung = @MaNguoiDung`.

#### 8. Nút `btnSaveSettings` ("Lưu cài đặt trải nghiệm")
* **Hàm UI bắt sự kiện:** `BtnSaveSettings_Click(object? sender, EventArgs e)`
* **Dữ liệu lưu trữ:** 4 thuộc tính trải nghiệm người dùng trong bảng `CaiDatNguoiDung`:
  * `chkEmailNotify.Checked` $\to$ `ThongBaoEmail`
  * `chkRememberLogin.Checked` $\to$ `GhiNhoDangNhap`
  * `chkAutoUpdate.Checked` $\to$ `TuDongCapNhat`
  * `chkConfirmPayment.Checked` $\to$ `XacNhanThanhToan` (Bật/tắt hộp thoại xác nhận khi bấm 1-Click Checkout).
* **Hàm DAL:** `HoSoRepository.LuuCaiDat(...)`.
