# ĐẶC TẢ LUỒNG NÚT BẤM & HÀM XỬ LÝ - NHÓM 2: GIỎ HÀNG, VÍ & VOUCHER
**Thành viên phụ trách:** Nguyễn Quốc Trung  
**Màn hình quản lý:**
1. UI 4: `CartControl` (Giỏ hàng mua sắm & Thanh toán 1-Click)
2. UI 5: `WalletControl` (Ví tài khoản Steam simulation & Nạp tiền)
3. UI 15: `VoucherControl` (Kho mã giảm giá & Thu thập ưu đãi cá nhân)

---

## 1. MÀN HÌNH UI 4: `CartControl`

### 1.1. Sơ đồ luồng Thanh toán 1-Click Giỏ hàng (ACID Transaction)

```text
[ Người dùng bấm nút "💳 THANH TOÁN 1-CLICK" trên CartControl ]
                                |
                                v
                     CartControl.BtnCheckout_Click()
                                |
                                | Kiểm tra giỏ hàng trống?
                                | Kiểm tra cờ xác nhận: CaiDatNguoiDung.XacNhanThanhToan
                                v
               [ Khách bấm "Đồng ý thanh toán" ]
                                |
                                v
         ThanhToanService.ThanhToanGioHang(userId, tienGiam, voucherId, out orderCode, out msg)
                                |
                                v
         DonHangRepository.ThucHienThanhToanDonHang(userId, items, tamTinh, tienGiam, voucherId...)
                                |
                                +--> BẮT ĐẦU SqlTransaction (ACID)
                                |
                                |-- 1. SELECT SoDuVi FROM NguoiDung (Kiểm tra đủ tiền?)
                                |      -> Nếu thiếu: trans.Rollback(), báo lỗi thiếu tiền
                                |-- 2. UPDATE NguoiDung SET SoDuVi = SoDuVi - tongTien
                                |-- 3. Sinh mã ORD-yyyyMMddHHmmss-XXXX
                                |-- 4. INSERT INTO DonHang (...)
                                |-- 5. Vòng lặp items:
                                |      INSERT INTO ChiTietDonHang (...)
                                |      INSERT INTO SoHuuSanPham (...) [Cấp quyền ngay lập tức]
                                |-- 6. INSERT INTO ThanhToan (Ghi sổ cái tài chính)
                                |-- 7. UPDATE MaGiamGiaNguoiDung (Đánh dấu voucher đã dùng)
                                |-- 8. DELETE FROM GioHang (Xóa sạch giỏ hàng)
                                |
                                +--> trans.Commit() HOÀN TẤT THÀNH CÔNG!
                                |
                                v
         ĐỒNG BỘ DỮ LIỆU THỜI GIAN THỰC (REACTIVE EVENT BUS):
         - UserSession.UpdateBalance(balanceMoi) -> lblBalance Header đổi số tiền
         - GioHangService.NotifyCartUpdated()   -> Badge giỏ hàng về 0
         - ThuVienService.NotifyLibraryUpdated() -> LibraryControl tự động nạp game mới
         - MaGiamGiaService.NotifyVoucherUpdated()
                                |
                                v
         CartControl: Reset giao diện, hỏi người dùng: "Mở Thư viện để tải game ngay?"
```

---

### 1.2. Chi tiết từng nút bấm trên UI 4

#### 1. Nút `btnCheckout` ("Thanh toán 1-Click bằng Ví")
* **Hàm UI bắt sự kiện:** `BtnCheckout_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Khách hàng nhấn nút thanh toán trên giỏ hàng sau khi xem tổng tiền và voucher.
* **Xác thực trước khi gọi:**
  * Giỏ hàng không được rỗng (`_cartItems.Count > 0`).
  * Đọc cấu hình `HoSoRepository.LayCaiDat(userId)`. Nếu `XacNhanThanhToan == true`, hiển thị hộp thoại Dialog:
    *"Bạn có chắc chắn muốn thanh toán đơn hàng này với tổng số tiền {tongTien} qua Ví Steam không?"*.
* **Hàm BLL được gọi:** `ThanhToanService.ThanhToanGioHang(int maNguoiDung, decimal tienGiam, int? maGiamGiaId, out string maDonHangCode, out string thongBao)`
* **Hàm DAL được gọi:** `DonHangRepository.ThucHienThanhToanDonHang(...)`
* **Xử lý ngoại lệ:**
  * Nếu số dư ví không đủ: Hiện thông báo thiếu bao nhiêu tiền và hỏi: *"Bạn có muốn nạp thêm tiền vào ví ngay bây giờ không?"*. Nếu chọn Yes $\to$ Kích hoạt sự kiện `OnNavigateToWallet?.Invoke()`.
* **Phản hồi sau thành công:** Bắn sự kiện `OnNavigateToLibrary?.Invoke()` nếu khách muốn đến thư viện ngay.

#### 2. Nút `btnApplyVoucher` ("Áp dụng mã")
* **Hàm UI bắt sự kiện:** `BtnApplyVoucher_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Khách hàng nhập mã voucher vào ô `txtVoucherCode` và bấm Áp dụng.
* **Hàm BLL được gọi:** `MaGiamGiaService.KiemTraVaApDung(string maCode, int maNguoiDung, decimal tamTinh, out voucher, out tienGiam, out thongBao)`
* **Hàm DAL được gọi:** `MaGiamGiaRepository.KiemTraVaApDung(...)`
* **Các luật kiểm tra (Rules):**
  1. Mã có tồn tại trong `MaGiamGia` không?
  2. Đã hết hạn (`NgayKetThuc < GETDATE()`) hay hết lượt (`SoLuongConLai <= 0`) chưa?
  3. Tài khoản đã thu thập vào kho chưa (`DaThuThap == true`)?
  4. Đã từng dùng mã này chưa (`DaSuDung == false`)?
  5. Đơn hàng đạt mức tối thiểu (`tamTinh >= DonToiThieu`) chưa?
* **Phản hồi UI:** Cập nhật nhãn `lblDiscountAmount` và `lblTotalAmount` tương ứng.

#### 3. Nút `btnRemoveItem` ("Xóa món hàng")
* **Hàm UI bắt sự kiện:** `BtnRemoveItem_Click(object? sender, EventArgs e)`
* **Hàm BLL được gọi:** `GioHangService.XoaKhoiGioHang(int maGioHang)`
* **Hàm DAL được gọi:** `GioHangRepository.XoaKhoiGioHang(maGioHang)`
* **SQL thực thi:** `DELETE FROM GioHang WHERE MaGioHang = @MaGioHang;`
* **Sự kiện bắn ra:** `GioHangService.NotifyCartUpdated()`.

#### 4. Nút `btnClearCart` ("Xóa sạch giỏ hàng")
* **Hàm UI bắt sự kiện:** `BtnClearCart_Click(object? sender, EventArgs e)`
* **Hành vi:** Xóa toàn bộ sản phẩm trong giỏ của `UserSession.CurrentUserId` sau khi người dùng xác nhận "Yes".

#### 5. Nút `btnDeposit` ("Nạp thêm tiền")
* **Hàm UI bắt sự kiện:** `BtnDeposit_Click(object? sender, EventArgs e)`
* **Hành vi:** Kích hoạt `OnNavigateToWallet?.Invoke()` chuyển hướng sang `WalletControl`.

#### 6. Nút `btnOpenVouchers` ("Chọn từ kho mã ưu đãi")
* **Hàm UI bắt sự kiện:** `BtnOpenVouchers_Click(object? sender, EventArgs e)`
* **Hành vi:** Kích hoạt `OnNavigateToVouchers?.Invoke()` chuyển hướng sang `VoucherControl`.

---

## 2. MÀN HÌNH UI 5: `WalletControl`

### 2.1. Chi tiết các nút nạp tiền ví

#### 7. Nhóm nút mệnh giá cố định (`btnTopUp50k`, `btnTopUp100k`, `btnTopUp200k`, `btnTopUp500k`)
* **Hàm UI bắt sự kiện:**
  * `BtnTopUp50k_Click` $\to$ `NapTien(50000)`
  * `BtnTopUp100k_Click` $\to$ `NapTien(100000)`
  * `BtnTopUp200k_Click` $\to$ `NapTien(200000)`
  * `BtnTopUp500k_Click` $\to$ `NapTien(500000)`
* **Nơi gọi hàm nghiệp vụ:** `NapTien(decimal soTien)`
* **Hàm BLL được gọi:** `ViTaiKhoanService.NapTien(int maNguoiDung, decimal soTien, out string thongBao)`
* **Hàm DAL được gọi:** `ViTaiKhoanRepository.NapTienVi(maNguoiDung, soTien)`
* **Câu lệnh SQL thực thi:**
  ```sql
  -- 1. Cộng dồn số dư ví
  UPDATE NguoiDung 
  SET SoDuVi = SoDuVi + @SoTien 
  WHERE MaNguoiDung = @MaNguoiDung;

  -- 2. Ghi nhật ký vào Sổ cái ThanhToan
  INSERT INTO ThanhToan (MaNguoiDung, MaDonHang, SoTien, LoaiGiaoDich, PhuongThuc, TrangThai, NgayTao)
  VALUES (@MaNguoiDung, NULL, @SoTien, N'Nạp tiền ví', N'Ví Steam Simulation', N'Thành công', GETDATE());
  ```
* **Sự kiện bắn ra:** `UserSession.UpdateBalance(balanceMoi)`.
* **Phản hồi UI:** Cập nhật nhãn số dư lớn `lblCurrentBalance` và làm mới bảng sao kê `dgvTransactions`.

#### 8. Nút `btnCustomTopUp` ("Nạp số tiền tùy chọn")
* **Hàm UI bắt sự kiện:** `BtnCustomTopUp_Click(object? sender, EventArgs e)`
* **Xác thực:** Kiểm tra số tiền nhập từ ô `txtCustomAmount`:
  * Tối thiểu $10.000$đ.
  * Tối đa $50.000.000$đ/lần nạp.

---

## 3. MÀN HÌNH UI 15: `VoucherControl`

#### 9. Nút `btnCollect` ("Thu thập vào ví" bằng mã nhập)
* **Hàm UI bắt sự kiện:** `BtnCollect_Click(object? sender, EventArgs e)`
* **Hàm BLL được gọi:** `MaGiamGiaService.ThuThapVoucher(int maNguoiDung, string maCode, out string thongBao)`
* **Hàm DAL được gọi:** `MaGiamGiaRepository.ThuThapVoucher(...)`
* **SQL thực thi:**
  ```sql
  INSERT INTO MaGiamGiaNguoiDung (MaNguoiDung, MaGiamGiaId, DaSuDung, NgayNhan)
  VALUES (@MaNguoiDung, @MaGiamGiaId, 0, GETDATE());

  UPDATE MaGiamGia 
  SET SoLuongConLai = SoLuongConLai - 1 
  WHERE MaGiamGiaId = @MaGiamGiaId;
  ```
* **Sự kiện bắn ra:** `MaGiamGiaService.NotifyVoucherUpdated()`.

#### 10. Nút `btnApplySelected` ("Dùng ngay cho Giỏ hàng")
* **Hàm UI bắt sự kiện:** `BtnApplySelected_Click(object? sender, EventArgs e)`
* **Hành vi phát sự kiện:** `OnNavigateToCartWithVoucher?.Invoke(voucherCode)`.
* **Nơi nhận tại MainForm:** Kích hoạt `MainForm.NavigateToCart(voucherCode)` $\to$ nạp giỏ hàng và tự động điền sẵn mã ưu đãi vào ô áp dụng!

#### 11. Nút `btnDelete` ("Bỏ lưu voucher")
* **Hàm UI bắt sự kiện:** `BtnDelete_Click(object? sender, EventArgs e)`
* **Hàm BLL:** `MaGiamGiaService.XoaVoucherCaNhan(maGiamGiaId, userId)`.
* **SQL thực thi:** `DELETE FROM MaGiamGiaNguoiDung WHERE MaGiamGiaId = @Id AND MaNguoiDung = @UserId;`.
