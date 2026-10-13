# PRD: PHÂN HỆ GIỎ HÀNG & VÍ TÀI KHOẢN (CART & STEAM WALLET)
**Nhiệm vụ:** STT 2  
**Màn hình phụ trách:**  
- **UI 4:** Giỏ hàng & Mua hàng 1-Click (`CartControl`)
- **UI 5:** Quản lý Ví tài khoản & Nạp tiền mô phỏng (`WalletControl`)

---

## 1. Mục tiêu phân hệ
Áp dụng cơ chế **Số dư ví tài khoản (Steam Wallet)** tương tự nền tảng Steam: Khách hàng quản lý giỏ hàng và thanh toán trực tiếp trong 1 cú click bằng số dư ví. Khi số dư không đủ, khách hàng truy cập màn hình Ví tài khoản để nạp thêm tiền mô phỏng (chọn các gói mệnh giá 50k, 100k, 200k, 500k).

Mô hình này giúp loại bỏ hoàn toàn màn hình Checkout rườm rà, phản ánh đúng thực tế của sản phẩm số (không cần địa chỉ nhận hàng, không cần phí ship) và giúp kịch bản demo bài tập lớn có logic xác thực số dư thật 100%.

---

## 2. Người dùng mục tiêu & Vai trò
- **Khách hàng đã đăng nhập:**
  - Theo dõi danh sách sản phẩm trong giỏ hàng, tăng/giảm số lượng, xóa món.
  - Theo dõi số dư ví hiện tại ngay trên giỏ hàng và Header.
  - Bấm thanh toán ngay bằng số dư ví (1-Click Pay).
  - Nạp tiền vào ví bằng các gói mệnh giá nhanh.

---

## 3. Đặc tả yêu cầu chức năng (Functional Requirements)

### 3.1. UI 4 - Giỏ hàng & Thanh toán 1-Click (`CartControl`)
1. **Hiển thị giỏ hàng (CRUD Read 1):**
   - Danh sách món trong giỏ (DataGridView hoặc Panel): Tên sản phẩm, Thể loại, Đơn giá, Số lượng, Thành tiền, Nút xóa.
   - Thống kê: Tổng tiền thanh toán.
   - Hiển thị số dư ví hiện tại của người dùng: `Số dư ví của bạn: 500.000 VNĐ`.
2. **Cập nhật số lượng & Xóa món (CRUD Update & Delete 1):**
   - Nút `+` / `-` để tăng/giảm số lượng món hàng.
   - Nút "Xóa" từng món khỏi `CartItems`.
   - Nút "Xóa tất cả".
3. **Thanh toán 1-Click bằng số dư ví (CRUD Create 2 cho BTL):**
   - Nút **[ ⚡ THANH TOÁN BẰNG SỐ DƯ VÍ ]**:
     + **Trường hợp 1 (Số dư >= Tổng tiền):**
       - Trừ tiền ví tài khoản: `Balance = Balance - Tổng tiền`.
       - Tạo đơn hàng `Orders` (Status = 'Completed', PaymentMethod = 'WALLET').
       - Cấp quyền vào kho `UserProducts` (sở hữu vĩnh viễn).
       - Xóa sạch giỏ hàng `CartItems`.
       - Hiển thị thông báo: *"Thanh toán thành công! Sản phẩm đã được thêm vào Thư viện. Số dư còn lại: X VNĐ"*.
       - Cập nhật lại số dư ví hiển thị trên Header `MainForm`.
     + **Trường hợp 2 (Số dư < Tổng tiền):**
       - Báo lỗi: *"Số dư ví không đủ (Bạn còn thiếu Y VNĐ)! Vui lòng nạp thêm tiền vào ví để mua sắm."*
       - Cung cấp nút bấm: *"Đi tới Nạp tiền ngay"* -> Chuyển sang UI 5 (`WalletControl`).

### 3.2. UI 5 - Quản lý Ví tài khoản & Nạp tiền (`WalletControl`)
1. **Hiển thị thông tin ví (CRUD Read 2):**
   - Tên chủ tài khoản, Username, Trạng thái ví.
   - Số dư khả dụng hiện tại (Hiển thị to, nổi bật).
2. **Nạp tiền mô phỏng vào ví (CRUD Update 2 cho BTL):**
   - Các nút bấm chọn nhanh gói mệnh giá:
     + `[ + 50.000 VNĐ ]`
     + `[ + 100.000 VNĐ ]`
     + `[ + 200.000 VNĐ ]`
     + `[ + 500.000 VNĐ ]`
     + `[ + 1.000.000 VNĐ ]`
   - Ô nhập số tiền tùy chọn (nếu muốn nạp con số khác).
   - Nút **[ 💰 XÁC NHẬN NẠP TIỀN VÀO VÍ ]**:
     + Thực hiện cộng tiền vào tài khoản: `UPDATE Users SET Balance = Balance + @Amount`.
     + Thông báo: *"Nạp tiền thành công! Số dư mới của bạn là: X VNĐ"*.
     + Đồng bộ số dư lên Header của `MainForm`.
3. **Lịch sử biến động số dư (Đơn giản hóa):**
   - Hiển thị danh sách các lần trừ tiền mua hàng từ bảng `Orders` (Thời gian, Mã đơn, Số tiền trừ).

---

## 4. Quy tắc nghiệp vụ (Business Rules)
1. **Ràng buộc số dư:** Số dư tài khoản không bao giờ được phép âm (`Balance >= 0`).
2. **Tính toàn vẹn giao dịch (Transaction):** Thao tác trừ tiền ví, tạo đơn hàng và cấp quyền thư viện phải nằm chung trong 1 Transaction SQL. Nếu có lỗi xảy ra, hệ thống tự động Rollback (không bị trừ tiền oan).
3. **Đồng bộ giao diện:** Khi số dư ví thay đổi (sau khi nạp tiền hoặc mua hàng), nhãn hiển thị số dư trên Header của `MainForm` phải được cập nhật ngay lập tức.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Giỏ hàng):** Xem danh sách giỏ, sửa số lượng (Update), xóa món (Delete).
- [x] **CRUD 2 (Ví tài khoản):** Nạp tiền tăng số dư (Update `Balance`), mua hàng trừ số dư (Update `Balance`) và chặn thành công khi số dư không đủ.
- [x] Kịch bản demo bài tập lớn mượt mà, thuyết phục và logic 100%.
