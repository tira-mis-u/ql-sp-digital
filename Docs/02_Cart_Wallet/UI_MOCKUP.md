# MÔ PHỎNG & MINH HỌA GIAO DIỆN (UI MOCKUP) - NHÓM 2
**Màn hình:** UI 4 (`CartControl`) & UI 5 (`WalletControl`)

---

## 1. Bản vẽ mô phỏng UI 4: Giỏ hàng & Mua hàng 1-Click (`CartControl`)

### 1.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  GIỎ HÀNG CỦA BẠN (3 món)                                                    [🗑️ Xóa tất cả]       |
+---------------------------------------------------------------------------------------------------+
|  +-----------------------------------------------------------------+  +-------------------------+ |
|  | DANH SÁCH MÓN HÀNG (DataGridView / Panel Container)             |  | TÓM TẮT THANH TOÁN      | |
|  |-----------------------------------------------------------------|  |-------------------------| |
|  | [Ảnh] Cyber Adventure RPG                                       |  | Tổng tiền món hàng:     | |
|  |       Đơn giá: 450.000 đ    Số lượng: [-] [ 1 ] [+]             |  | 1.800.000 VNĐ           | |
|  |       Thành tiền: 450.000 đ                           [Xóa 🗑️]  |  |                         | |
|  |-----------------------------------------------------------------|  | SỐ DƯ VÍ CỦA BẠN:       | |
|  | [Ảnh] Photo Studio Pro 2026                                     |  | 💰 2.000.000 VNĐ        | |
|  |       Đơn giá: 1.200.000 đ  Số lượng: [-] [ 1 ] [+]             |  | (Trạng thái: Đủ tiền ✅)| |
|  |       Thành tiền: 1.200.000 đ                         [Xóa 🗑️]  |  |-------------------------| |
|  |-----------------------------------------------------------------|  | [⚡ THANH TOÁN BẰNG VÍ ]| |
|  | [Ảnh] Low Poly Sci-Fi Pack                                      |  | (1 Cú click - Mua ngay) | |
|  |       Đơn giá: 150.000 đ    Số lượng: [-] [ 1 ] [+]             |  |                         | |
|  |       Thành tiền: 150.000 đ                           [Xóa 🗑️]  |  | [ ➕ Nạp thêm tiền ví ] | |
|  |-----------------------------------------------------------------|  |                         | |
|  |                                                                 |  | [ < Tiếp tục mua sắm ]  | |
|  +-----------------------------------------------------------------+  +-------------------------+ |
+---------------------------------------------------------------------------------------------------+
```

> **Ghi chú trải nghiệm (UX):**
> - Nếu `Số dư ví < Tổng tiền`, dòng trạng thái sẽ đổi thành `(Thiếu: 300.000 VNĐ ⚠️)`, nút *Thanh toán* sẽ bị mờ hoặc khi bấm vào sẽ tự động hiện thông báo hỏi: *"Số dư ví của bạn không đủ, bạn có muốn nạp tiền ngay không?"* và chuyển sang UI 5.

### 1.2. Danh sách WinForms Controls gợi ý cho UI 4
| Tên Control | Loại Control | Chức năng & Thuộc tính |
|---|---|---|
| `lblCartTitle` | `Label` | Tiêu đề "GIỎ HÀNG CỦA BẠN", Font 16pt Bold. |
| `dgvCart` | `DataGridView` | Hiển thị danh sách sản phẩm trong giỏ. |
| `panelCartSummary` | `Panel` | Panel tóm tắt thanh toán bên phải. |
| `lblCartTotal` | `Label` | Tổng tiền thanh toán (Font 16pt, màu đỏ). |
| `lblUserBalance` | `Label` | Hiển thị số dư ví hiện tại của user (Font 14pt Bold, màu xanh lá). |
| `lblBalanceStatus`| `Label` | Nhãn báo đủ tiền hay thiếu tiền. |
| `btnPayWithWallet`| `Button` | Nút "THANH TOÁN BẰNG VÍ" (Nổi bật, kích thước lớn). |
| `btnGoToTopUp` | `Button` | Nút "Nạp thêm tiền ví" (Chuyển sang UI 5 `WalletControl`). |
| `btnClearCart` | `Button` | Nút xóa toàn bộ sản phẩm khỏi giỏ. |

---

## 2. Bản vẽ mô phỏng UI 5: Quản lý Ví & Nạp tiền (`WalletControl`)

### 2.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  VÍ TÀI KHOẢN DPMS (STEAM WALLET SIMULATION)                                                      |
+---------------------------------------------------------------------------------------------------+
|  +---------------------------------------------------------------------------------------------+  |
|  | THẺ THÔNG TIN SỐ DƯ VÍ                                                                      |  |
|  |---------------------------------------------------------------------------------------------|  |
|  | Chủ tài khoản:  Phạm Trung Hiếu (Username: hieupm)                                          |  |
|  | SỐ DƯ KHẢ DỤNG: 2.000.000 VNĐ 💰                                                            |  |
|  | (Dùng để mua game, phần mềm và tài nguyên số tức thì không cần thẻ tín dụng)                |  |
|  +---------------------------------------------------------------------------------------------+  |
|                                                                                                   |
|  +----------------------------------------------------+  +-------------------------------------+  |
|  | 1. CHỌN MỆNH GIÁ NẠP TIỀN MÔ PHỎNG                 |  | 2. LỊCH SỬ BIẾN ĐỘNG SỐ DƯ VÍ       |  |
|  |----------------------------------------------------|  |-------------------------------------|  |
|  | Bấm chọn nhanh các gói tiền:                       |  | Mã ĐH | Biến động    | Thời gian   |  |
|  | [ + 50.000 đ   ]      [ + 100.000 đ  ]             |  |-------|--------------|-------------|  |
|  | [ + 200.000 đ  ]      [ + 500.000 đ  ]             |  | #1052 | - 450.000 đ  | 10 phút tr  |  |
|  | [ + 1.000.000 đ]      [ + 2.000.000 đ]             |  | Nạp   | + 1.000.000 đ| 1 giờ tr    |  |
|  |                                                    |  | #1048 | - 150.000 đ  | Hôm qua     |  |
|  | Hoặc nhập số tiền tùy chọn:                        |  |-------|--------------|-------------|  |
|  | Số tiền nạp: [ 500000                            ] |  | Hiển thị các giao dịch gần nhất     |  |
|  |                                                    |  |                                     |  |
|  | [ 💳 XÁC NHẬN NẠP TIỀN VÀO TÀI KHOẢN NGAY ]        |  | [ 🔄 Tải lại lịch sử ]              |  |
|  +----------------------------------------------------+  +-------------------------------------+  |
+---------------------------------------------------------------------------------------------------+
```

### 2.2. Danh sách WinForms Controls gợi ý cho UI 5
| Tên Control | Loại Control | Chức năng & Thuộc tính |
|---|---|---|
| `lblAccountName` | `Label` | Tên chủ tài khoản. |
| `lblCurrentBalance` | `Label` | Số dư ví hiện tại, Font 22pt Bold, Màu xanh lá. |
| `btnPkg50k` -> `btnPkg2m` | `Button` | Các nút chọn gói tiền nạp nhanh (50k, 100k, 200k, 500k, 1tr, 2tr). |
| `txtCustomAmount` | `TextBox` | Ô nhập số tiền tùy chọn (Chỉ cho nhập số). |
| `btnConfirmDeposit` | `Button` | Nút kích hoạt lệnh nạp tiền vào database. |
| `dgvWalletHistory` | `DataGridView` | Hiển thị bảng biến động số dư / đơn hàng. |
| `btnRefreshHistory` | `Button` | Tải lại số dư và lịch sử giao dịch. |
