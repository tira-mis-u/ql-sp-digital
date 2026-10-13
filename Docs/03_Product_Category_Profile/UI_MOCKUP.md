# MÔ PHỎNG & MINH HỌA GIAO DIỆN (UI MOCKUP) - NHÓM 3
**Màn hình:** UI 10 (`ProductManageControl`), UI 11 (`CategoryManageControl`), UI 8 (`ProfileControl`)

---

## 1. Bản vẽ mô phỏng UI 10: Quản lý sản phẩm (`ProductManageControl`)

### 1.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  QUẢN LÝ SẢN PHẨM HỆ THỐNG                                                                        |
+---------------------------------------------------------------------------------------------------+
|  [ THÔNG TIN NHẬP LIỆU ]                                                                          |
|  Mã SP:     [ 101       ] (Chỉ đọc)       Tên sản phẩm: [ Cyber Adventure RPG                   ] |
|  Danh mục:  [ Game      v]                Giá bán (VNĐ):[ 450000                                ] |
|  Hình ảnh:  [ cyber.png                   ] [Chọn tệp...]  Trạng thái: [X] Đang mở bán            |
|  Mô tả:     [ Tựa game phiêu lưu thế giới mở tương lai hấp dẫn................................. ] |
|                                                                                                   |
|  [ ➕ Thêm mới ]   [ 💾 Lưu thay đổi ]   [ 🗑️ Xóa / Ngưng bán ]   [ 🔄 Làm mới ô nhập ]           |
+---------------------------------------------------------------------------------------------------+
|  [ BẢNG DỮ LIỆU SẢN PHẨM ]  Tìm kiếm: [ cyber               ]  Lọc: [ Tất cả danh mục v]          |
|  +-----+-------------------------+---------------+--------------+------------+------------------+ |
|  | Mã  | Tên sản phẩm            | Danh mục      | Giá bán      | Trạng thái | Ngày tạo         | |
|  +-----+-------------------------+---------------+--------------+------------+------------------+ |
|  | 101 | Cyber Adventure RPG     | Game          | 450.000 đ    | Đang bán   | 10/01/2026       | |
|  | 102 | Photo Studio Pro 2026   | Phần mềm      | 1.200.000 đ  | Đang bán   | 12/01/2026       | |
|  | 103 | Low Poly Sci-Fi Pack    | 3D Models     | 150.000 đ    | Đang bán   | 15/01/2026       | |
|  | 104 | Old Pixel Engine 2020   | Phần mềm      | 200.000 đ    | Ngưng bán  | 01/01/2024       | |
|  +-----+-------------------------+---------------+--------------+------------+------------------+ |
|  Tổng số sản phẩm: 4 bản ghi                                                                       |
+---------------------------------------------------------------------------------------------------+
```

---

## 2. Bản vẽ mô phỏng UI 11: Quản lý danh mục (`CategoryManageControl`)

### 2.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  QUẢN LÝ DANH MỤC THỂ LOẠI (CATEGORIES)                                                           |
+---------------------------------------------------------------------------------------------------+
|  +---------------------------------------------+  +---------------------------------------------+ |
|  | KHUNG NHẬP LIỆU DANH MỤC                    |  | BẢNG DANH SÁCH DANH MỤC (DataGridView)     | |
|  |---------------------------------------------|  |---------------------------------------------| |
|  | Mã danh mục: [ 1        ] (Tự tăng)         |  | Mã DM | Tên danh mục      | Số sản phẩm     | |
|  |                                             |  |-------|-------------------|-----------------| |
|  | Tên danh mục:                               |  | 1     | Game              | 12 sản phẩm     | |
|  | [ Trò chơi điện tử                        ] |  | 2     | Phần mềm          | 8 sản phẩm      | |
|  |                                             |  | 3     | Đồ họa 3D Models  | 15 sản phẩm     | |
|  | Mô tả ngắn:                                 |  | 4     | Hiệu ứng âm thanh | 6 sản phẩm      | |
|  | [ Các tựa game PC kỹ thuật số..........   ] |  | 5     | Tài liệu & Sách   | 3 sản phẩm      | |
|  |                                             |  |-------|-------------------|-----------------| |
|  | [ ➕ Thêm ]  [ ✏️ Sửa ]  [ 🗑️ Xóa ]  [ 🔄 Mới ] |  | Tổng cộng: 5 danh mục                       | |
|  +---------------------------------------------+  +---------------------------------------------+ |
+---------------------------------------------------------------------------------------------------+
```

---

## 3. Bản vẽ mô phỏng UI 8: Hồ sơ cá nhân (`ProfileControl`)

### 3.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  THÔNG TIN TÀI KHOẢN VÀ HỒ SƠ CÁ NHÂN                                                             |
+---------------------------------------------------------------------------------------------------+
|  +---------------------------------------------+  +---------------------------------------------+ |
|  | 1. THÔNG TIN CÁ NHÂN                        |  | 2. ĐỔI MẬT KHẨU                             | |
|  |---------------------------------------------|  |---------------------------------------------| |
|  | Tên đăng nhập:   [ nguyenvana             ] |  | Mật khẩu hiện tại:                          | |
|  | (Chỉ đọc)                                   |  | [ ****************                        ] | |
|  |                                             |  |                                             | |
|  | Vai trò:         [ KHÁCH HÀNG (Customer)  ] |  | Mật khẩu mới:                               | |
|  | (Chỉ đọc)                                   |  | [ ****************                        ] | |
|  |                                             |  |                                             | |
|  | Họ và tên:                                  |  | Xác nhận mật khẩu mới:                      | |
|  | [ Nguyễn Văn A                            ] |  | [ ****************                        ] | |
|  |                                             |  |                                             | |
|  | Email liên hệ:                              |  |                                             | |
|  | [ vana@example.com                        ] |  |                                             | |
|  |                                             |  |                                             | |
|  | Số điện thoại:                              |  |                                             | |
|  | [ 0987654321                              ] |  |                                             | |
|  |                                             |  |                                             | |
|  | [ 💾 Cập nhật thông tin ]                   |  | [ 🔑 Xác nhận đổi mật khẩu ]                | |
|  +---------------------------------------------+  +---------------------------------------------+ |
+---------------------------------------------------------------------------------------------------+
```

### 3.2. Danh sách WinForms Controls gợi ý cho Nhóm 3
| Tên Control | Loại Control | Màn hình | Chức năng & Thuộc tính |
|---|---|---|---|
| `dgvProducts` | `DataGridView` | UI 10 | Hiển thị danh sách sản phẩm. `SelectionMode = FullRowSelect`. |
| `txtProductName` | `TextBox` | UI 10 | Nhập tên sản phẩm. |
| `txtProductPrice` | `TextBox` | UI 10 | Nhập đơn giá sản phẩm. |
| `cboCategory` | `ComboBox` | UI 10 | Chọn danh mục sản phẩm. |
| `dgvCategories` | `DataGridView` | UI 11 | Hiển thị bảng thể loại. |
| `txtCategoryName` | `TextBox` | UI 11 | Nhập tên danh mục. |
| `txtFullName` | `TextBox` | UI 8 | Họ và tên người dùng. |
| `txtOldPassword` | `TextBox` | UI 8 | Mật khẩu cũ (`UseSystemPasswordChar = True`). |
| `txtNewPassword` | `TextBox` | UI 8 | Mật khẩu mới (`UseSystemPasswordChar = True`). |
