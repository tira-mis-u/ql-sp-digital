# PRD: PHÂN HỆ QUẢN LÝ ĐƠN HÀNG & THƯ VIỆN SỐ (ORDERS & LIBRARY)
**Nhiệm vụ:** STT 5  
**Màn hình phụ trách:**  
- **UI 13:** Quản lý đơn hàng Admin (`OrderManageControl`)
- **UI 7:** Lịch sử đơn hàng cá nhân (`OrderHistoryControl`)
- **UI 6:** Thư viện sản phẩm đã sở hữu (`LibraryControl`)

---

## 1. Mục tiêu phân hệ
Quản lý toàn bộ vòng đời sau mua hàng: Cho phép Admin theo dõi, lọc và thay đổi trạng thái đơn hàng; cho phép khách hàng tra cứu lại các giao dịch đã thực hiện; và đặc biệt là không gian Thư viện số (My Library) để khách hàng quản lý các game/phần mềm/tài nguyên đã mua kèm tính năng mô phỏng tải tệp kỹ thuật số về máy tính.

---

## 2. Người dùng mục tiêu & Vai trò
- **Quản trị viên (Admin):**
  - Xem danh sách tất cả đơn hàng trong hệ thống, xem chi tiết từng sản phẩm trong đơn, duyệt đơn hàng hoặc hủy đơn hàng (`OrderManageControl`).
- **Khách hàng (Customer):**
  - Xem lịch sử các đơn hàng của riêng mình (`OrderHistoryControl`).
  - Truy cập Thư viện số (`LibraryControl`), tìm kiếm sản phẩm đã mua, bấm nút "Tải xuống" để mô phỏng tải file và xem key kích hoạt.

---

## 3. Đặc tả yêu cầu chức năng (Functional Requirements)

### 3.1. UI 13 - Quản lý đơn hàng Admin (`OrderManageControl`)
1. **Hiển thị danh sách đơn hàng toàn hệ thống (CRUD Read 1 cho BTL):**
   - DataGridView danh sách: Mã đơn (`OrderId`), Khách hàng, Tổng tiền, Trạng thái (Pending / Completed / Cancelled), Phương thức thanh toán, Ngày tạo.
   - Bộ lọc theo Trạng thái đơn hàng và tìm kiếm theo mã đơn hoặc tên khách hàng.
2. **Xem chi tiết sản phẩm trong đơn:**
   - Khi chọn một đơn hàng -> Bảng con bên cạnh/phía dưới hiển thị danh sách các món hàng thuộc đơn đó (Tên sản phẩm, Đơn giá, Số lượng, Thành tiền).
3. **Cập nhật trạng thái đơn (CRUD Update 1 cho BTL):**
   - Nút **"Duyệt đơn"**: Chuyển trạng thái sang `Completed`.
   - Nút **"Hủy đơn"**: Chuyển trạng thái sang `Cancelled`.

### 3.2. UI 7 - Lịch sử đơn hàng khách hàng (`OrderHistoryControl`)
1. **Hiển thị lịch sử mua của cá nhân (CRUD Read 1 phía Client):**
   - Tận dụng chung cấu trúc bảng `Orders` nhưng lọc `WHERE UserId = @CurrentUserId`.
   - Xem mã đơn, ngày mua, tổng tiền, trạng thái đơn hàng.
   - Click đúp xem danh sách sản phẩm đã mua.

### 3.3. UI 6 - Thư viện sản phẩm đã sở hữu (`LibraryControl`)
1. **Hiển thị kho sản phẩm đã mua (CRUD Read 2 cho BTL):**
   - Lấy danh sách từ bảng `UserProducts` kết hợp bảng `Products`.
   - Hiển thị dạng lưới thẻ hoặc danh sách Sidebar: Ảnh bìa, Tên sản phẩm, Ngày mua, Dung lượng tệp, Phiên bản.
2. **Mô phỏng tải xuống sản phẩm số (Download Simulation):**
   - Nút **"Tải về máy" (Download)**:
     + Khi bấm, hiển thị thanh tiến trình giả lập (ProgressBar chạy từ 0% -> 100% trong 2 giây) hoặc mở hộp thoại `FolderBrowserDialog` cho người dùng chọn nơi lưu file.
     + Hiển thị thông báo: `MessageBox.Show("Tải xuống hoàn tất! Tệp mẫu đã được lưu vào máy tính của bạn.")`.
3. **Quản lý hiển thị kho (CRUD Delete 2 cho BTL):**
   - Nút **"Gỡ khỏi thư viện"** (Xóa bản ghi khỏi `UserProducts` hoặc đánh dấu ẩn).

---

## 4. Quy tắc nghiệp vụ (Business Rules)
1. **Đơn giản hóa dữ liệu:** Cả hai màn hình UI 13 (Admin) và UI 7 (Customer) dùng chung một cấu trúc bảng `Orders` và `OrderItems`, chỉ khác nhau ở tham số truy vấn `UserId`.
2. **Đơn giản hóa tải tệp:** Do phạm vi bài tập lớn trên máy cá nhân, hệ thống không cần máy chủ lưu trữ file hàng GB, chỉ cần mô phỏng thao tác tải hoặc copy một file text/zip mẫu có sẵn trong thư mục ứng dụng.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Orders):** Xem danh sách đơn và cập nhật trạng thái đơn (Pending -> Completed/Cancelled).
- [x] **CRUD 2 (UserProducts):** Xem thư viện cá nhân và gỡ/xóa sản phẩm khỏi thư viện thành công.
- [x] Mô phỏng tải file số mượt mà, trực quan với người dùng.
