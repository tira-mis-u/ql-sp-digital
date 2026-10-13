# PRD: PHÂN HỆ QUẢN TRỊ SẢN PHẨM, DANH MỤC & HỒ SƠ CÁ NHÂN
**Nhiệm vụ:** STT 3  
**Màn hình phụ trách:**  
- **UI 10:** Quản lý sản phẩm (`ProductManageControl`)
- **UI 11:** Quản lý danh mục (`CategoryManageControl`)
- **UI 8:** Hồ sơ cá nhân (`ProfileControl`)

---

## 1. Mục tiêu phân hệ
Cung cấp bộ công cụ quản trị dữ liệu cốt lõi cho Administrator để thêm, sửa, xóa sản phẩm và thể loại hàng hóa; đồng thời cung cấp màn hình quản lý hồ sơ cho người dùng cá nhân (xem thông tin, chỉnh sửa họ tên, đổi mật khẩu).

---

## 2. Người dùng mục tiêu & Vai trò
- **Quản trị viên (Admin):**
  - Thực hiện toàn quyền CRUD trên danh sách sản phẩm (thêm game/phần mềm mới, sửa giá, ngưng kinh doanh).
  - Quản lý danh mục thể loại (tạo danh mục mới, chỉnh sửa tên thể loại).
- **Mọi người dùng (Khách hàng & Admin):**
  - Truy cập xem hồ sơ cá nhân và đổi mật khẩu tài khoản.

---

## 3. Đặc tả yêu cầu chức năng (Functional Requirements)

### 3.1. UI 10 - Quản lý sản phẩm (`ProductManageControl`)
1. **Hiển thị danh sách sản phẩm (CRUD Read 1):**
   - DataGridView danh sách sản phẩm: ID, Tên sản phẩm, Danh mục, Giá, Trạng thái (Đang bán / Ngưng bán), Ngày tạo.
   - Ô tìm kiếm nhanh theo tên sản phẩm và ComboBox lọc theo danh mục.
2. **Thêm mới sản phẩm (CRUD Create 1):**
   - Form nhập liệu: Tên sản phẩm, Chọn danh mục, Giá bán (VNĐ), URL ảnh / Nút chọn ảnh, Mô tả ngắn.
   - Nút "Thêm mới" -> Kiểm tra rỗng -> Thực hiện câu lệnh `INSERT INTO Products`.
3. **Cập nhật sản phẩm (CRUD Update 1):**
   - Click chọn dòng trên DataGridView -> Dữ liệu đổ ngược về các ô TextBox.
   - Sửa thông tin -> Bấm nút "Lưu thay đổi" -> Thực hiện câu lệnh `UPDATE Products`.
4. **Xóa / Ngưng bán (CRUD Delete 1):**
   - Bấm nút "Xóa" -> Xóa mềm (`UPDATE Products SET IsActive = 0`) hoặc xóa cứng (`DELETE FROM Products`) nếu chưa có đơn hàng liên kết.
   - Nút "Làm mới" (Reset Form) để xóa trắng các ô nhập liệu.

### 3.2. UI 11 - Quản lý danh mục (`CategoryManageControl`)
1. **Hiển thị danh sách danh mục (CRUD Read 2 cho BTL):**
   - Bảng DataGridView hiển thị: Mã danh mục (`CategoryId`), Tên danh mục (`CategoryName`), Mô tả.
2. **Thao tác Thêm / Sửa / Xóa danh mục (CRUD Create / Update / Delete 2):**
   - Form nhập: Tên danh mục, Mô tả danh mục.
   - Thêm danh mục mới (`INSERT INTO Categories`).
   - Cập nhật tên danh mục (`UPDATE Categories`).
   - Xóa danh mục (`DELETE FROM Categories` - kiểm tra nếu có sản phẩm thuộc danh mục thì cảnh báo không cho xóa).

### 3.3. UI 8 - Hồ sơ cá nhân (`ProfileControl`)
1. **Xem thông tin (CRUD Read 3):**
   - Hiển thị: Username (chỉ đọc), Vai trò (Role), Ngày tham gia, Họ tên đầy đủ, Email, Số điện thoại.
2. **Cập nhật thông tin & Đổi mật khẩu (CRUD Update 3):**
   - Cập nhật Họ tên, Email, Số điện thoại -> Bấm "Lưu thông tin" -> `UPDATE Users`.
   - Đổi mật khẩu: Nhập Mật khẩu cũ, Mật khẩu mới, Xác nhận mật khẩu mới -> Bấm "Đổi mật khẩu".

---

## 4. Quy tắc nghiệp vụ (Business Rules)
1. **Ràng buộc giá:** Giá sản phẩm phải là số không âm (`Price >= 0`).
2. **Toàn vẹn danh mục:** Không cho phép xóa danh mục nếu đang có ít nhất 1 sản phẩm liên kết với nó.
3. **Đổi mật khẩu:** Mật khẩu cũ phải trùng khớp với dữ liệu hiện tại trong database; mật khẩu mới phải tối thiểu 6 ký tự.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Products):** Thêm, sửa, xóa sản phẩm mượt mà với DataGridView.
- [x] **CRUD 2 (Categories):** Thao tác Thêm/Sửa/Xóa thể loại thành công.
- [x] **CRUD 3 (User Profile):** Cập nhật họ tên và đổi mật khẩu hoạt động chuẩn xác.
