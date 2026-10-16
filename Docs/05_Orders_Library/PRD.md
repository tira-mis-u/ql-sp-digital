# PRD: PHÂN HỆ QUẢN LÝ ĐƠN HÀNG & THƯ VIỆN SỐ (ORDERS & LIBRARY)
**Nhiệm vụ:** STT 5  
**Thành viên phụ trách:** Phạm Văn Đam (Nhóm 5)  
**Màn hình phụ trách:**  
- **UI 13:** Quản lý đơn hàng Admin ([`../../qlspdigital/qlspdigital/Views/Nhom5_Orders/OrderManageControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom5_Orders/OrderManageControl.cs))
- **UI 7:** Lịch sử đơn hàng cá nhân ([`../../qlspdigital/qlspdigital/Views/Nhom5_Orders/OrderHistoryControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom5_Orders/OrderHistoryControl.cs))
- **UI 6:** Thư viện sản phẩm đã sở hữu ([`../../qlspdigital/qlspdigital/Views/Nhom5_Orders/LibraryControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom5_Orders/LibraryControl.cs))
**Mã nguồn nghiệp vụ & truy vấn:**
- BLL Service: [`../../qlspdigital/qlspdigital/BLL/Nhom5_Orders/DonHangService.cs`](../../qlspdigital/qlspdigital/BLL/Nhom5_Orders/DonHangService.cs)
- DAL Repository: [`../../qlspdigital/qlspdigital/DAL/Nhom5_Orders/DonHangRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom5_Orders/DonHangRepository.cs)

---

## 1. Mục tiêu phân hệ
Quản lý toàn bộ vòng đời sau mua hàng: Cho phép Admin theo dõi, lọc và thay đổi trạng thái đơn hàng; cho phép khách hàng tra cứu lại các giao dịch đã thực hiện; và đặc biệt là không gian Thư viện số (My Library) để khách hàng quản lý các game/phần mềm/tài nguyên đã mua kèm tính năng mô phỏng tải tệp kỹ thuật số về máy tính.

---

## 2. Người dùng mục tiêu & Vai trò
- **Quản trị viên (Admin):**
  - Xem danh sách tất cả đơn hàng trong hệ thống, xem chi tiết từng sản phẩm trong đơn, duyệt đơn hàng hoặc hủy đơn hàng (`OrderManageControl`).
- **Khách hàng (`KhachHang`):**
  - Xem lịch sử các đơn hàng của riêng mình (`OrderHistoryControl`).
  - Truy cập Thư viện số (`LibraryControl`), tìm kiếm sản phẩm đã mua, bấm nút "Tải xuống" để mô phỏng tải file và xem key kích hoạt.

---

## 3. Đặc tả yêu cầu chức năng (Functional Requirements)

### 3.1. UI 13 - Quản lý đơn hàng Admin (`OrderManageControl`)
1. **Hiển thị danh sách đơn hàng toàn hệ thống (CRUD Read 1 cho BTL):**
   - DataGridView danh sách: Mã đơn (`MaDonHangCode`), Khách hàng, Tổng tiền (`TongThanhToan`), Trạng thái (`TrangThaiDonHang`: Hoàn thành / Chờ xử lý / Đã hủy), Ghi chú, Ngày tạo.
   - Bộ lọc theo Trạng thái đơn hàng và tìm kiếm theo mã đơn hoặc tên khách hàng.
2. **Xem chi tiết sản phẩm trong đơn:**
   - Khi chọn một đơn hàng -> Bảng con bên cạnh/phía dưới hiển thị danh sách các món hàng thuộc đơn đó (Tên sản phẩm, Đơn giá, Số lượng, Thành tiền) từ bảng `ChiTietDonHang`.
3. **Cập nhật trạng thái đơn (CRUD Update 1 cho BTL):**
   - Nút **"Duyệt đơn"**: Chuyển trạng thái sang `N'Hoàn thành'` và tự động cấp sản phẩm vào Thư viện của khách (`SoHuuSanPham`).
   - Nút **"Hủy đơn"**: Chuyển trạng thái sang `N'Đã hủy'`.

### 3.2. UI 7 - Lịch sử đơn hàng khách hàng (`OrderHistoryControl`)
1. **Hiển thị lịch sử mua của cá nhân (CRUD Read 1 phía Client):**
   - Tận dụng chung cấu trúc bảng `DonHang` nhưng lọc `WHERE MaNguoiDung = @CurrentUserId`.
   - Xem mã đơn, ngày mua, tổng tiền, trạng thái đơn hàng.
   - Click đúp xem danh sách chi tiết các món hàng đã mua.

### 3.3. UI 6 - Thư viện sản phẩm đã sở hữu (`LibraryControl`)
1. **Hiển thị kho sản phẩm đã mua (CRUD Read 2 cho BTL):**
   - Lấy danh sách từ bảng `SoHuuSanPham` kết hợp bảng `SanPham` và `TepSanPham`.
   - Hiển thị dạng lưới thẻ hoặc danh sách Sidebar: Ảnh bìa, Tên sản phẩm, Ngày sở hữu, Dung lượng tệp, Phiên bản.
2. **Tải xuống sản phẩm số (Download File):**
   - Nút **"Tải về máy" (Download)**:
     + Khi bấm, mở hộp thoại `FolderBrowserDialog` cho người dùng chọn nơi lưu file trên máy tính.
     + Hiển thị thanh tiến trình `ProgressBar` sao chép tệp thực tế/mô phỏng từ thư mục `Storage/Products/...` sang thư mục đích, tăng dần từ 0% đến 100%.
     + Kiểm tra mã băm SHA-256 đối chiếu và tăng bộ đếm `SoLanTai`.
     + Hiển thị thông báo: `MessageBox.Show("Tải xuống hoàn tất! Tệp sản phẩm kỹ thuật số đã được lưu vào máy tính của bạn.")`.
3. **Quản lý hiển thị kho (CRUD Delete 2 cho BTL):**
   - Nút **"Gỡ khỏi thư viện"** (Xóa bản ghi khỏi `SoHuuSanPham`).

---

## 4. Quy tắc nghiệp vụ (Business Rules)
1. **Đơn giản hóa dữ liệu:** Cả hai màn hình UI 13 (`QuanTriVien`) và UI 7 (`KhachHang`) dùng chung cấu trúc bảng `DonHang` và `ChiTietDonHang`, chỉ khác nhau ở tham số truy vấn `MaNguoiDung`.
2. **Quản lý tệp số thông minh:** Tệp kỹ thuật số được đọc trực tiếp từ thư mục `Storage/Products/{MaSanPham}/` theo đường dẫn tương đối lưu trong bảng `TepSanPham`, cho phép copy sang máy khách bất kỳ mà không bị gãy đường dẫn.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (DonHang):** Xem danh sách đơn và cập nhật trạng thái đơn (Chờ xử lý -> Hoàn thành / Đã hủy).
- [x] **CRUD 2 (SoHuuSanPham):** Xem thư viện cá nhân và gỡ/xóa sản phẩm khỏi thư viện thành công.
- [x] Tải tệp kỹ thuật số mượt mà có thanh ProgressBar trực quan và kiểm tra toàn vẹn SHA-256.

