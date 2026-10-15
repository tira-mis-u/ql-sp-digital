# MÔ PHỎNG & MINH HỌA GIAO DIỆN (UI MOCKUP) - NHÓM 3
**Màn hình:** UI 10 (`ProductManageControl`), UI 11 (`CategoryManageControl`), UI 8 (`ProfileControl`)

---

## 1. Bản vẽ mô phỏng UI 10: Quản lý sản phẩm & Tệp kỹ thuật số (`ProductManageControl`)

### 1.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  QUẢN LÝ SẢN PHẨM & TỆP KỸ THUẬT SỐ (ADMIN PRODUCT & ASSET MANAGEMENT)                             |
+---------------------------------------------------------------------------------------------------+
|  [ 1. THÔNG TIN CƠ BẢN SẢN PHẨM ]                                                                 |
|  Mã SP:    [ 101       ] (Chỉ đọc)       Tên sản phẩm: [ Cyber Adventure RPG                    ] |
|  Thể loại: [ Game      v]                Nhà phát hành:[ Pixel Game Studio                    v] |
|  Giá bán:  [ 450000    ] VNĐ             Phiên bản:    [ 1.2.0     ]  Trạng thái: [X] Đang bán    |
|  Ảnh bìa:  [ Assets/Images/cyber.jpg     ] [ 🖼️ Chọn ảnh... ] (Kèm PictureBox xem trước bên cạnh) |
|  Mô tả:    [ Tựa game phiêu lưu thế giới mở tương lai hấp dẫn.................................. ] |
|---------------------------------------------------------------------------------------------------|
|  [ 2. TẢI LÊN & QUẢN LÝ TỆP KỸ THUẬT SỐ VẬT LÝ (Bảng TepSanPham) ]                                |
|  Chọn tệp: [ C:\Downloads\Setup_CyberAdventure_v1.2.0.exe      ] [ 📁 Duyệt tệp (.zip, .exe)... ] |
|  Thông số: Dung lượng: 14.5 GB (15.569.256.448 Bytes) | Đuôi: .exe | Mã SHA256: e3b0c44298fc1c...|
|  Tiến trình nạp: [===================================================>] 100% (Đã nạp vào Storage) |
|                                                                                                   |
|  Danh sách tệp đính kèm của sản phẩm này:                                                         |
|  +----+----------------------------------+-----------+------------+-----------+-----------------+ |
|  | Mã | Tên tệp                          | Định dạng | Dung lượng | Phiên bản | Thao tác        | |
|  +----+----------------------------------+-----------+------------+-----------+-----------------+ |
|  | 1  | Setup_CyberAdventure_v1.2.0.exe  | .exe      | 14.5 GB    | 1.2.0     | [ 🗑️ Xóa tệp ]  | |
|  | 2  | Game_Manual_DigitalArtBook.pdf   | .pdf      | 85.0 MB    | 1.0.0     | [ 🗑️ Xóa tệp ]  | |
|  +----+----------------------------------+-----------+------------+-----------+-----------------+ |
|                                                                                                   |
|  [ ➕ Thêm sản phẩm ]   [ 💾 Lưu thay đổi ]   [ 🚫 Ngưng kinh doanh ]   [ 🔄 Làm mới ô nhập ]     |
+---------------------------------------------------------------------------------------------------+
|  [ 3. BẢNG DỮ LIỆU SẢN PHẨM TOÀN SÀN ]  Tìm kiếm: [ cyber           ]  Lọc: [ Tất cả danh mục v]  |
|  +-----+-------------------------+---------------+--------------+------------+------------------+ |
|  | Mã  | Tên sản phẩm            | Danh mục      | Giá bán      | Trạng thái | Ngày tạo         | |
|  +-----+-------------------------+---------------+--------------+------------+------------------+ |
|  | 101 | Cyber Adventure RPG     | Game          | 450.000 đ    | Đang bán   | 10/01/2026       | |
|  | 102 | Photo Studio Pro 2026   | Phần mềm      | 1.200.000 đ  | Đang bán   | 12/01/2026       | |
|  | 103 | Low Poly Sci-Fi Pack    | 3D Models     | 150.000 đ    | Đang bán   | 15/01/2026       | |
|  | 104 | Old Pixel Engine 2020   | Phần mềm      | 200.000 đ    | Ngưng bán  | 01/01/2024       | |
|  +-----+-------------------------+---------------+--------------+------------+------------------+ |
|  Tổng cộng: 4 sản phẩm hiển thị                                                                   |
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
|  | Vai trò:         [ Khách hàng (KhachHang) ] |  | Mật khẩu mới:                               | |
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
| `cboPublisher` | `ComboBox` | UI 10 | Chọn nhà phát hành sản phẩm. |
| `btnBrowseFile` | `Button` | UI 10 | Mở `OpenFileDialog` chọn tệp cài đặt (.zip, .exe, .rar, .iso). |
| `txtSelectedFilePath` | `TextBox` | UI 10 | Hiển thị đường dẫn tệp nguồn vừa chọn (`ReadOnly = True`). |
| `lblFileDetails` | `Label` | UI 10 | Hiển thị dung lượng byte/MB và mã băm SHA-256 đã sinh. |
| `prgUpload` | `ProgressBar` | UI 10 | Hiển thị tiến trình copy tệp vật lý vào thư mục `Storage/`. |
| `dgvProductFiles` | `DataGridView` | UI 10 | Danh sách các tệp đính kèm của sản phẩm (`TepSanPham`). |
| `btnDeleteFile` | `Button` | UI 10 | Xóa tệp đính kèm đã chọn khỏi CSDL và thư mục `Storage/`. |
| `dgvCategories` | `DataGridView` | UI 11 | Hiển thị bảng thể loại. |
| `txtCategoryName` | `TextBox` | UI 11 | Nhập tên danh mục. |
| `txtFullName` | `TextBox` | UI 8 | Họ và tên người dùng. |
| `txtOldPassword` | `TextBox` | UI 8 | Mật khẩu cũ (`UseSystemPasswordChar = True`). |
| `txtNewPassword` | `TextBox` | UI 8 | Mật khẩu mới (`UseSystemPasswordChar = True`). |
