# PRD: PHÂN HỆ CỬA HÀNG & CHI TIẾT SẢN PHẨM (STORE & PRODUCT DETAILS)
**Nhiệm vụ:** STT 1  
**Màn hình phụ trách:**  
- **UI 2:** Cửa hàng / Trang chủ (`StoreViewControl`)
- **UI 3:** Chi tiết sản phẩm (`ProductDetailControl`)

---

## 1. Mục tiêu phân hệ
Cung cấp cho khách hàng trải nghiệm duyệt danh mục, tìm kiếm sản phẩm kỹ thuật số nhanh chóng và xem thông tin chi tiết đầy đủ (mô tả, giá tiền, hình ảnh minh họa, thông số tệp). Đồng thời hỗ trợ tính năng lưu sản phẩm yêu thích (Favorites).

---

## 2. Người dùng mục tiêu & Vai trò
- **Khách hàng vãng lai / Đã đăng nhập:** Duyệt danh sách sản phẩm, lọc theo thể loại, tìm kiếm theo tên, xem trang chi tiết.
- **Khách hàng đã đăng nhập:** Thêm/Bỏ sản phẩm yêu thích (Favorites), bấm "Thêm vào giỏ" để chuyển món hàng sang giỏ mua sắm.

---

## 3. Đặc tả yêu cầu chức năng (Functional Requirements)

### 3.1. UI 2 - Cửa hàng / Trang chủ (`StoreViewControl`)
1. **Hiển thị danh sách sản phẩm:**
   - Dạng thẻ lưới (Card Layout) hiển thị: Ảnh bìa, Tên sản phẩm, Danh mục, Giá tiền (VNĐ).
   - Nếu sản phẩm miễn phí hiển thị nhãn "Miễn phí".
2. **Tìm kiếm & Bộ lọc (CRUD Read 1):**
   - Ô tìm kiếm theo từ khóa (Tên sản phẩm): Tìm kiếm tức thì khi nhấn nút "Tìm kiếm" hoặc bấm Enter.
   - Hộp chọn ComboBox "Thể loại": Tất cả, Games, Phần mềm, Đồ họa 3D, Âm thanh...
   - Sắp xếp (Sort): Theo giá tăng dần, giá giảm dần, mới nhất.
3. **Danh sách yêu thích - Favorites (CRUD 2 cho BTL):**
   - Trên mỗi thẻ sản phẩm có nút biểu tượng Trái tim (Yêu thích).
   - Nếu sản phẩm chưa có trong danh sách thích: Bấm vào -> Thêm vào bảng `Favorites` (Create).
   - Nếu đã thích: Trái tim hiển thị trạng thái kích hoạt (Đỏ/Đầy) -> Bấm vào -> Xóa khỏi bảng `Favorites` (Delete).
   - Nút lọc nhanh: "Chỉ hiện sản phẩm yêu thích" (Read).
4. **Điều hướng:**
   - Click đúp hoặc click nút "Xem chi tiết" trên thẻ -> Chuyển sang màn hình UI 3 (`ProductDetailControl`).

### 3.2. UI 3 - Chi tiết sản phẩm (`ProductDetailControl`)
1. **Thông tin hiển thị:**
   - Tiêu đề sản phẩm, nhà phát hành / người tạo, ngày phát hành.
   - Ảnh đại diện lớn (Banner / Cover Image).
   - Giá bán chính thức và trạng thái còn kinh doanh hay không.
   - Mô tả chi tiết sản phẩm (Mô tả tính năng, cấu hình yêu cầu, định dạng tệp số).
2. **Hành động mua sắm:**
   - Nút **"Thêm vào giỏ hàng"**: Thêm sản phẩm này vào giỏ hàng của tài khoản đang đăng nhập. Nếu đã có trong giỏ, hiển thị thông báo.
   - Nút **"Mua ngay"**: Thêm vào giỏ và tự động chuyển ngay sang màn hình Giỏ hàng / Thanh toán.
   - Nút **"Quay lại"**: Trở về danh sách Cửa hàng trước đó.

---

## 4. Quy tắc nghiệp vụ (Business Rules)
1. **Hiển thị sản phẩm hợp lệ:** Chỉ hiển thị các sản phẩm có trạng thái `IsActive = 1`.
2. **Kiểm tra trạng thái sở hữu:** Nếu người dùng đã mua sản phẩm này rồi (đã có trong kho `UserProducts`), nút "Thêm vào giỏ" sẽ đổi thành "Đã sở hữu - Đi tới Thư viện".
3. **Giá hiển thị:** Định dạng theo chuẩn tiền tệ Việt Nam (VD: `250.000 đ`).

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Read/Filter):** Lọc theo ít nhất 3 danh mục và tìm kiếm theo chuỗi ký tự thành công trên database.
- [x] **CRUD 2 (Favorites):** Thêm và xóa thành công một bản ghi trong bảng `Favorites` (dùng câu lệnh `INSERT INTO Favorites...` và `DELETE FROM Favorites...`).
- [x] Chuyển đổi mượt mà giữa Store và Product Details trong khung sườn `MainForm`.
