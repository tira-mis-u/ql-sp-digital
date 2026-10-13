# MÔ PHỎNG & MINH HỌA GIAO DIỆN (UI MOCKUP) - NHÓM 4
**Màn hình:** UI 1 (`LoginControl`, `RegisterControl`), UI 12 (`UserManageControl`), UI 9 (`DashboardControl`)

---

## 1. Bản vẽ mô phỏng UI 1: Đăng nhập & Đăng ký (`Auth`)

### 1.1. Bố cục trực quan màn Đăng nhập (`LoginControl`)
```text
+---------------------------------------------------------------------------------------------------+
|                                                                                                   |
|                             +---------------------------------------+                             |
|                             |        ĐĂNG NHẬP HỆ THỐNG DPMS        |                             |
|                             |---------------------------------------|                             |
|                             | Tên đăng nhập:                        |                             |
|                             | [ nguyenvana                        ] |                             |
|                             |                                       |                             |
|                             | Mật khẩu:                             |                             |
|                             | [ ****************                  ] |                             |
|                             |                                       |                             |
|                             | [X] Ghi nhớ đăng nhập                 |                             |
|                             |                                       |                             |
|                             | [      👉 ĐĂNG NHẬP NGAY       ]      |                             |
|                             |                                       |                             |
|                             | Chưa có tài khoản? [Đăng ký tại đây]  |                             |
|                             +---------------------------------------+                             |
|                                                                                                   |
+---------------------------------------------------------------------------------------------------+
```

### 1.2. Bố cục trực quan màn Đăng ký (`RegisterControl`)
```text
+---------------------------------------------------------------------------------------------------+
|                                                                                                   |
|                             +---------------------------------------+                             |
|                             |        ĐĂNG KÝ TÀI KHOẢN MỚI          |                             |
|                             |---------------------------------------|                             |
|                             | Tên đăng nhập:    [ hieupm          ] |                             |
|                             | Họ và tên:        [ Phạm Trung Hiếu ] |                             |
|                             | Email:            [ hieu@gmail.com  ] |                             |
|                             | Số điện thoại:    [ 0912345678      ] |                             |
|                             | Mật khẩu:         [ *************** ] |                             |
|                             | Xác nhận MK:      [ *************** ] |                             |
|                             |                                       |                             |
|                             | [      ✨ TẠO TÀI KHOẢN NGAY    ]     |                             |
|                             |                                       |                             |
|                             | Đã có tài khoản? [Quay lại Đăng nhập] |                             |
|                             +---------------------------------------+                             |
|                                                                                                   |
+---------------------------------------------------------------------------------------------------+
```

---

## 2. Bản vẽ mô phỏng UI 12: Quản lý người dùng (`UserManageControl`)

### 2.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  DANH SÁCH VÀ QUẢN LÝ TÀI KHOẢN NGƯỜI DÙNG                                                        |
+---------------------------------------------------------------------------------------------------+
|  Tìm kiếm user: [ hieu              ] [Tìm]  Lọc vai trò: [ Tất cả vai trò v]  [ 🔄 Làm mới ]     |
|                                                                                                   |
|  +-----+------------+----------------------+--------------------+-----------+------------+-------+ |
|  | ID  | Tên đăng nhập | Họ và tên          | Email              | Vai trò   | Trạng thái | Ngày  | |
|  +-----+------------+----------------------+--------------------+-----------+------------+-------+ |
|  | 1   | admin      | Quản trị viên        | admin@dpms.com     | Admin     | Hoạt động  | 01/01 | |
|  | 2   | hieupm     | Phạm Trung Hiếu      | hieu@gmail.com     | Customer  | Hoạt động  | 10/01 | |
|  | 3   | thuanqt    | Trần Quốc Thuận      | thuan@gmail.com    | Customer  | Hoạt động  | 12/01 | |
|  | 4   | baduser99  | Kẻ Phá Hoại          | spam@bad.com       | Customer  | ĐÃ KHÓA ⛔  | 15/01 | |
|  +-----+------------+----------------------+--------------------+-----------+------------+-------+ |
|                                                                                                   |
|  Đang chọn tài khoản: [ baduser99 - Kẻ Phá Hoại ]                                                 |
|  [ 🔒 KHÓA TÀI KHOẢN NÀY ]            [ 🔓 MỞ KHÓA TÀI KHOẢN ]            [ 🔄 ĐẶT LẠI MẬT KHẨU ]  |
+---------------------------------------------------------------------------------------------------+
```

---

## 3. Bản vẽ mô phỏng UI 9: Admin Dashboard (`DashboardControl`)

### 3.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  TỔNG QUAN KINH DOANH (ADMIN DASHBOARD)                                      [ 🔄 Cập nhật số liệu] |
+---------------------------------------------------------------------------------------------------+
|  +-----------------------+  +-----------------------+  +-----------------------+  +-------------+ |
|  | 💵 TỔNG DOANH THU     |  | 📦 TỔNG ĐƠN HÀNG      |  | 👥 TỔNG KHÁCH HÀNG    |  | 🎮 SẢN PHẨM | |
|  |                       |  |                       |  |                       |  |             | |
|  |    24.500.000 VNĐ     |  |       152 ĐƠN         |  |      85 TÀI KHOẢN     |  |   42 MÓN    | |
|  +-----------------------+  +-----------------------+  +-----------------------+  +-------------+ |
|                                                                                                   |
|  +-------------------------------------------------+  +-----------------------------------------+ |
|  | 5 ĐƠN HÀNG MỚI NHẤT (DataGridView)              |  | TOP 5 SẢN PHẨM BÁN CHẠY NHẤT            | |
|  |-------------------------------------------------|  |-----------------------------------------| |
|  | Mã ĐH | Khách hàng   | Số tiền     | Thời gian  |  | 1. Cyber Adventure RPG   (45 lượt mua)  | |
|  | #1052 | hieupm       | 450.000 đ   | 10 phút tr |  | 2. Photo Studio Pro 2026 (30 lượt mua)  | |
|  | #1051 | thuanqt      | 1.200.000 đ | 1 giờ tr   |  | 3. Low Poly Sci-Fi Pack  (18 lượt mua)  | |
|  | #1050 | trimn        | 150.000 đ   | Hôm nay    |  | 4. Fantasy Hero Rig 3D   (12 lượt mua)  | |
|  +-------------------------------------------------+  +-----------------------------------------+ |
+---------------------------------------------------------------------------------------------------+
```

### 3.2. Danh sách WinForms Controls gợi ý cho Nhóm 4
| Tên Control | Loại Control | Màn hình | Chức năng & Thuộc tính |
|---|---|---|---|
| `txtLoginUsername` | `TextBox` | UI 1 | Nhập Username. |
| `txtLoginPassword` | `TextBox` | UI 1 | Nhập Password (`PasswordChar = '*'`). |
| `btnLogin` | `Button` | UI 1 | Nút thực thi đăng nhập. |
| `dgvUsers` | `DataGridView` | UI 12 | Hiển thị danh sách người dùng. |
| `btnLockUser` | `Button` | UI 12 | Khóa tài khoản đang chọn. |
| `btnUnlockUser` | `Button` | UI 12 | Mở khóa tài khoản đang chọn. |
| `lblMetricRevenue` | `Label` | UI 9 | Hiển thị tổng doanh thu (Font 18pt Bold, Xanh lá). |
| `lblMetricOrders` | `Label` | UI 9 | Hiển thị tổng số đơn hàng. |
| `lblMetricUsers` | `Label` | UI 9 | Hiển thị tổng số user. |
