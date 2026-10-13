# MÔ PHỎNG & MINH HỌA GIAO DIỆN (UI MOCKUP) - NHÓM 5
**Màn hình:** UI 13 (`OrderManageControl`), UI 7 (`OrderHistoryControl`), UI 6 (`LibraryControl`)

---

## 1. Bản vẽ mô phỏng UI 13: Quản lý đơn hàng Admin (`OrderManageControl`)

### 1.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  QUẢN LÝ ĐƠN HÀNG TOÀN HỆ THỐNG (ADMIN)                                                           |
+---------------------------------------------------------------------------------------------------+
|  Tìm kiếm: [ Đơn #1052 hoặc tên KH   ] [Tìm]  Trạng thái: [ Tất cả v]  [ 🔄 Tải lại dữ liệu ]     |
|                                                                                                   |
|  DANH SÁCH ĐƠN HÀNG (DataGridView)                                                                |
|  +-----+-------------------+--------------+---------------+---------------+--------------------+  |
|  | Mã  | Tên khách hàng    | Tổng tiền    | Phương thức   | Trạng thái    | Ngày đặt           |  |
|  +-----+-------------------+--------------+---------------+---------------+--------------------+  |
|  | 1052| hieupm            | 450.000 đ    | MOMO          | Đang chờ (⏳) | 13/01/2026 10:20   |  |
|  | 1051| thuanqt           | 1.200.000 đ  | BANK_QR       | Hoàn thành(✅)| 13/01/2026 09:15   |  |
|  | 1050| trimn             | 150.000 đ    | MOMO          | Đã hủy (❌)   | 12/01/2026 18:30   |  |
|  +-----+-------------------+--------------+---------------+---------------+--------------------+  |
|                                                                                                   |
|  CHI TIẾT SẢN PHẨM TRONG ĐƠN ĐANG CHỌN (Đơn #1052):                                               |
|  +-------------------------------------+--------+-----------+-------------+                      |
|  | Tên sản phẩm                        | SL     | Đơn giá   | Thành tiền  |                      |
|  +-------------------------------------+--------+-----------+-------------+                      |
|  | 1. Cyber Adventure RPG              | 1      | 450.000 đ | 450.000 đ   |                      |
|  +-------------------------------------+--------+-----------+-------------+                      |
|  Hành động: [ ✅ DUYỆT ĐƠN (Hoàn tất) ]       [ ❌ HỦY ĐƠN HÀNG ]         [ 🖨️ In hóa đơn ]       |
+---------------------------------------------------------------------------------------------------+
```

---

## 2. Bản vẽ mô phỏng UI 7: Lịch sử đơn hàng khách hàng (`OrderHistoryControl`)

### 2.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  LỊCH SỬ GIAO DỊCH VÀ ĐƠN HÀNG CỦA TÔI                                                            |
+---------------------------------------------------------------------------------------------------+
|  +---------------------------------------------------------------------------------------------+  |
|  | BẢNG LỊCH SỬ ĐƠN HÀNG (DataGridView)                                                        |  |
|  |---------------------------------------------------------------------------------------------|  |
|  | Mã đơn | Ngày đặt           | Số món | Tổng thanh toán | Phương thức | Trạng thái            |  |
|  |--------|--------------------|--------|-----------------|-------------|-----------------------|  |
|  | #1052  | 13/01/2026 10:20   | 1 món  | 450.000 VNĐ     | Ví MoMo     | Đang xử lý (⏳)        |  |
|  | #1015  | 05/01/2026 14:10   | 2 món  | 600.000 VNĐ     | Chuyển khoản| Đã hoàn thành (✅)    |  |
|  | #980   | 20/12/2025 08:45   | 1 món  | 150.000 VNĐ     | Ví MoMo     | Đã hoàn thành (✅)    |  |
|  +---------------------------------------------------------------------------------------------+  |
|                                                                                                   |
|  [ Chi tiết đơn hàng #1015 ]                                                                      |
|  - Photo Studio Pro 2026  x 1  (450.000 đ)                                                        |
|  - Low Poly Sci-Fi Pack   x 1  (150.000 đ)                                                        |
|  [ 👉 Đi tới Thư viện để tải file đã mua ]                                                        |
+---------------------------------------------------------------------------------------------------+
```

---

## 3. Bản vẽ mô phỏng UI 6: Thư viện sản phẩm đã sở hữu (`LibraryControl`)

### 3.1. Bố cục trực quan (Wireframe Layout - Phong cách Thư viện Steam)
```text
+---------------------------------------------------------------------------------------------------+
|  THƯ VIỆN SẢN PHẨM CỦA TÔI (MY LIBRARY)                                                           |
+---------------------------------------------------------------------------------------------------+
|  +------------------------+  +------------------------------------------------------------------+ |
|  | [🔍 Tìm trong kho... ] |  |  CYBER ADVENTURE RPG                                             | |
|  |------------------------|  |  Nhà phát triển: Pixel Studio  |  Ngày mua: 13/01/2026           | |
|  | 🎮 Cyber Adventure RPG |  |------------------------------------------------------------------| |
|  | 💻 Photo Studio Pro    |  |                                                                  | |
|  | 🎨 Low Poly Sci-Fi     |  |  [ BANNER SẢN PHẨM / COVER IMAGE CỠ LỚN ]                        | |
|  | 🎵 Ambient Sounds FX   |  |                                                                  | |
|  |                        |  |------------------------------------------------------------------| |
|  |                        |  |  TRẠNG THÁI: [ Sẵn sàng tải xuống ]                              | |
|  |                        |  |  Dung lượng: 14.5 GB  |  Phiên bản: v1.2.0                       | |
|  |                        |  |                                                                  | |
|  |                        |  |  [ ⬇️ TẢI XUỐNG MÁY TÍNH ]      [ 🗑️ Gỡ khỏi thư viện ]           | |
|  |                        |  |                                                                  | |
|  |                        |  |  Tiến trình tải: [=======================>........] 68%          | |
|  |                        |  |  (Mô phỏng ProgressBar chạy trong 2-3 giây rồi báo xong)          | |
|  |                        |  |                                                                  | |
|  | (ListBox / ListView)   |  |  Mã kích hoạt (License Key): ABCD-1234-EFGH-5678                 | |
|  +------------------------+  +------------------------------------------------------------------+ |
+---------------------------------------------------------------------------------------------------+
```

### 3.2. Danh sách WinForms Controls gợi ý cho Nhóm 5
| Tên Control | Loại Control | Màn hình | Chức năng & Thuộc tính |
|---|---|---|---|
| `dgvAdminOrders` | `DataGridView` | UI 13 | Bảng đơn hàng Admin. |
| `dgvAdminOrderItems` | `DataGridView` | UI 13 | Bảng chi tiết sản phẩm thuộc đơn. |
| `btnApproveOrder` | `Button` | UI 13 | Duyệt đơn (`Completed`). |
| `btnCancelOrder` | `Button` | UI 13 | Hủy đơn (`Cancelled`). |
| `dgvMyOrders` | `DataGridView` | UI 7 | Lịch sử đơn hàng của cá nhân khách hàng. |
| `lstLibraryProducts` | `ListBox` | UI 6 | Danh sách các sản phẩm đang sở hữu bên trái. |
| `picLibBanner` | `PictureBox` | UI 6 | Ảnh minh họa sản phẩm đang chọn. |
| `btnDownloadFile` | `Button` | UI 6 | Nút bấm kích hoạt mô phỏng tải file. |
| `prgDownload` | `ProgressBar` | UI 6 | Thanh chạy tiến trình tải giả lập. |
| `btnRemoveFromLib` | `Button` | UI 6 | Gỡ game khỏi thư viện (`Delete`). |
