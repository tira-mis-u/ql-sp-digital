# ĐẶC TẢ LUỒNG NÚT BẤM & HÀM XỬ LÝ - NHÓM 5: ĐƠN HÀNG & THƯ VIỆN SỐ
**Thành viên phụ trách:** Phạm Văn Đam  
**Màn hình quản lý:**
1. UI 13: `OrderManageControl` (Quản trị đơn hàng toàn sàn & Kích hoạt bù thủ công)
2. UI 7: `OrderHistoryControl` (Lịch sử giao dịch mua sắm cá nhân)
3. UI 6: `LibraryControl` (Thư viện game sở hữu, Tải tệp nén PK-Zip & Sinh chứng chỉ bản quyền)

---

## 1. MÀN HÌNH UI 13: `OrderManageControl`

### 1.1. Sơ đồ luồng Kích hoạt Bù thủ công & Hủy đơn Hoàn tiền ví

```text
[ Admin chọn đơn "Chờ xử lý" và bấm "✅ DUYỆT ĐƠN / KÍCH HOẠT THỦ CÔNG" ]
                               |
                               v
               OrderManageControl.BtnApprove_Click()
                               |
                               v
               DonHangService.DuyetDonHang(maDonHang, out msg)
                               |
                               v
               DonHangRepository.CapNhatTrangThaiDonHang(maDonHang, N'Hoàn thành', out msg)
                               |
                               +--> Mở SqlTransaction
                               |
                               |-- 1. UPDATE DonHang 
                               |      SET TrangThaiDonHang = N'Hoàn thành' WHERE MaDonHang = @Id
                               |
                               |-- 2. TỰ ĐỘNG CẤP BẢN QUYỀN VÀO THƯ VIỆN SỐ:
                               |      INSERT INTO SoHuuSanPham (MaNguoiDung, MaSanPham, MaDonHang, NgaySoHuu, SoLanTai)
                               |      SELECT dh.MaNguoiDung, ct.MaSanPham, dh.MaDonHang, GETDATE(), 0
                               |      FROM DonHang dh
                               |      INNER JOIN ChiTietDonHang ct ON dh.MaDonHang = ct.MaDonHang
                               |      WHERE dh.MaDonHang = @Id
                               |        AND NOT EXISTS (
                               |            SELECT 1 FROM SoHuuSanPham sh 
                               |            WHERE sh.MaNguoiDung = dh.MaNguoiDung AND sh.MaSanPham = ct.MaSanPham
                               |        );
                               |
                               +--> trans.Commit()
                               |
                               v
               Cập nhật giao diện: LoadOrders() tải lại danh sách đơn hàng
                               |
                               v
               Khách hàng truy cập LibraryControl thấy ngay game vừa được cấp!
```

---

### 1.2. Chi tiết từng nút bấm trên UI 13

#### 1. Nút `btnApprove` ("Duyệt đơn / Kích hoạt bản quyền thủ công")
* **Hàm UI bắt sự kiện:** `BtnApprove_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Admin xử lý khiếu nại CSKH trong 15s cho khách hàng bị rớt mạng khi thanh toán khiến đơn kẹt ở `Chờ xử lý`.
* **Hàm BLL được gọi:** `DonHangService.DuyetDonHang(int maDonHang, out string thongBao)`
* **Hàm DAL được gọi:** `DonHangRepository.CapNhatTrangThaiDonHang(maDonHang, "Hoàn thành", out thongBao)`
* **Cập nhật giao diện:** Gọi `LoadOrders()` nạp lại danh sách đơn hàng.
* **Phản hồi UI:** Tải lại lưới `dgvOrders`, hiển thị trạng thái `Hoàn thành` màu xanh lá cây.

#### 2. Nút `btnCancelOrder` ("Hủy đơn & Hoàn tiền ví")
* **Hàm UI bắt sự kiện:** `BtnCancelOrder_Click(object? sender, EventArgs e)`
* **Hàm BLL được gọi:** `DonHangService.HuyDonHang(int maDonHang, out string thongBao)`
* **Quy trình hoàn tiền:**
  1. Cập nhật `DonHang.TrangThaiDonHang = N'Đã hủy'`.
  2. Xóa bản ghi cấp quyền trong `SoHuuSanPham` (thu hồi game).
  3. Hoàn lại số tiền `TongThanhToan` vào `NguoiDung.SoDuVi`.
  4. Ghi sổ nhật ký `ThanhToan (LoaiGiaoDich = N'Hoàn tiền')`.
* **Cập nhật giao diện:** Gọi `LoadOrders()` làm mới danh sách đơn hàng.

---

## 2. MÀN HÌNH UI 7: `OrderHistoryControl`

#### 3. Bảng lưới `dgvMyOrders` (Sự kiện chọn dòng)
* **Hàm UI bắt sự kiện:** `DgvMyOrders_SelectionChanged(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Khách hàng click chọn một dòng đơn hàng trong bảng lịch sử.
* **Hàm BLL được gọi:** `DonHangService.LayChiTietDonHang(maDonHang)`
* **Hàm DAL được gọi:** `DonHangRepository.LayChiTietDonHang(maDonHang)`
* **SQL thực thi:**
  ```sql
  SELECT ct.MaChiTietDonHang, ct.MaDonHang, ct.MaSanPham, ct.SoLuong, ct.DonGia, sp.TenSanPham
  FROM ChiTietDonHang ct
  INNER JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
  WHERE ct.MaDonHang = @MaDonHang;
  ```
* **Phản hồi UI:** Nạp danh sách các game đã mua trong hóa đơn đó vào bảng con `dgvOrderDetails` ở phía dưới.

---

## 3. MÀN HÌNH UI 6: `LibraryControl`

### 3.1. Sơ đồ luồng Tải tệp nén PK-Zip bất đồng bộ & Sinh Giấy phép bản quyền

```text
[ Khách hàng bấm nút "⬇️ TẢI XUỐNG" trên LibraryControl ]
                             |
                             v
               Mở FolderBrowserDialog (Chọn thư mục lưu trên PC)
                             |
                             v
               LibraryControl.BtnDownload_Click()
                             |
                             v
               ThuVienService.TaiXuongTepAsync(item, thuMucLuu, onProgress)
                             |
                             | Task.Run chạy bất đồng bộ ngầm:
                             | - Tiến độ 10%: Tạo thư mục đích
                             | - Tiến độ 35%: Đọc metadata & đường dẫn tệp gốc
                             | - Tiến độ 60%: Khởi tạo FileStream nén:
                             |   ZipArchive(fs, ZipArchiveMode.Create, CompressionLevel.Optimal)
                             |
                             | 1. Đóng gói tệp thực thi / dữ liệu chính (.exe/.iso)
                             | 2. Tự động sinh tệp LICENSE_AND_KEY.txt đính kèm:
                             |    - Tên sản phẩm, Phiên bản
                             |    - Mã đơn hàng, Mã người dùng sở hữu
                             |    - Thời điểm cấp quyền
                             | - Tiến độ 85%: Flush stream & đóng gói chuẩn PK-Zip
                             | - Tiến độ 100%: Cập nhật CSDL
                             v
               ThuVienRepository.CapNhatLanTai(item.MaSoHuu)
                             |
                             | UPDATE SoHuuSanPham 
                             | SET SoLanTai = SoLanTai + 1, LanTaiCuoi = GETDATE()
                             | WHERE MaSoHuu = @MaSoHuu;
                             v
               Giao diện cập nhật: "Lượt tải: X, Lần tải cuối: vừa xong"
               Hiện thông báo: "Đã tải xuống thành công gói cài đặt sản phẩm!"
```

---

### 3.2. Chi tiết từng nút bấm trên UI 6

#### 4. Nút `btnDownload` ("Tải xuống gói cài đặt")
* **Hàm UI bắt sự kiện:** `async void BtnDownload_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Khách hàng chọn một tựa game trong Thư viện cá nhân và bấm Tải xuống.
* **Quy trình hoạt động:**
  1. Mở `FolderBrowserDialog` cho khách chọn nơi lưu trên ổ cứng (ví dụ: `D:\Games\`).
  2. Bật thanh tiến trình `progressBarDownload.Visible = true`.
  3. Gọi hàm bất đồng bộ: `await _thuVienService.TaiXuongTepAsync(selectedItem, folder, progressCallback)`.
  4. Sau khi hoàn thành 100%: Hiển thị nút "Mở thư mục" (`btnOpenFolder`) để khách truy cập trực tiếp file `.zip`.

#### 5. Nút `btnUninstall` ("Gỡ cài đặt khỏi máy khách")
* **Hàm UI bắt sự kiện:** `BtnUninstall_Click(object? sender, EventArgs e)`
* **Hành vi nghiệp vụ:**
  * Chỉ gỡ cài đặt cục bộ và dọn dẹp bộ nhớ máy khách.
  * Nếu khách bấm chọn xóa khỏi Thư viện: Gọi `ThuVienService.GoKhoiThuVien(maSoHuu, userId, out msg)` $\to$ Xóa bản ghi trong `SoHuuSanPham`.
  * Khách có thể liên hệ Admin để khôi phục hoặc tải lại nếu hóa đơn trong `DonHang` vẫn còn trạng thái `Hoàn thành`.
