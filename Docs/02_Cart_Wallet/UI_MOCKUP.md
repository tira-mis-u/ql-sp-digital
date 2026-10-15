# MÔ PHỎNG & MINH HỌA GIAO DIỆN (UI MOCKUP) - NHÓM 2
**Màn hình:** UI 4 (`CartControl`), UI 5 (`WalletControl`) & UI 15 (`VoucherControl`)

---

## 1. Bản vẽ mô phỏng UI 4: Giỏ hàng, Mã giảm giá & Mua hàng 1-Click (`CartControl`)

### 1.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  GIỎ HÀNG CỦA BẠN (3 món)                                                    [🗑️ Xóa tất cả]       |
+---------------------------------------------------------------------------------------------------+
|  +-----------------------------------------------------------------+  +-------------------------+ |
|  | DANH SÁCH MÓN HÀNG (DataGridView / Panel Container)             |  | TÓM TẮT THANH TOÁN      | |
|  |-----------------------------------------------------------------|  |-------------------------| |
|  | [Ảnh] Cyber Adventure RPG                                       |  | Tạm tính tiền hàng:     | |
|  |       Đơn giá: 450.000 đ    Số lượng: [-] [ 1 ] [+]             |  | 1.800.000 VNĐ           | |
|  |       Thành tiền: 450.000 đ                           [Xóa 🗑️]  |  |                         | |
|  |-----------------------------------------------------------------|  | MÃ GIẢM GIÁ (VOUCHER):  | |
|  | [Ảnh] Photo Studio Pro 2026                                     |  | [ SALE10             v] | |
|  |       Đơn giá: 1.200.000 đ  Số lượng: [-] [ 1 ] [+]             |  | [ Áp dụng ] [ Hủy mã ]  | |
|  |       Thành tiền: 1.200.000 đ                         [Xóa 🗑️]  |  | Giảm giá: - 180.000 VNĐ | |
|  |-----------------------------------------------------------------|  |-------------------------| |
|  | [Ảnh] Low Poly Sci-Fi Pack                                      |  | TỔNG THANH TOÁN:        | |
|  |       Đơn giá: 150.000 đ    Số lượng: [-] [ 1 ] [+]             |  | 🔥 1.620.000 VNĐ        | |
|  |       Thành tiền: 150.000 đ                           [Xóa 🗑️]  |  |                         | |
|  |-----------------------------------------------------------------|  | SỐ DƯ VÍ CỦA BẠN:       | |
|  |                                                                 |  | 💰 2.000.000 VNĐ (Đủ ✅)| |
|  |                                                                 |  |-------------------------| |
|  |                                                                 |  | [⚡ THANH TOÁN BẰNG VÍ ]| |
|  |                                                                 |  | (1 Cú click - Mua ngay) | |
|  |                                                                 |  |                         | |
|  |                                                                 |  | [ ➕ Nạp thêm tiền ví ] | |
|  |                                                                 |  | [ 🎁 Kho Voucher của tôi| |
|  +-----------------------------------------------------------------+  +-------------------------+ |
+---------------------------------------------------------------------------------------------------+
```

> **Ghi chú trải nghiệm (UX):**
> - Nếu `Số dư ví < Tổng thanh toán sau giảm`, dòng trạng thái sẽ đổi thành `(Thiếu: X VNĐ ⚠️)`, khi bấm thanh toán sẽ hiện cảnh báo và chuyển sang màn nạp tiền UI 5.
> - Bấm nút *[ 🎁 Kho Voucher của tôi ]* sẽ chuyển sang màn hình UI 15 để lấy thêm mã ưu đãi.

### 1.2. Danh sách WinForms Controls gợi ý cho UI 4
| Tên Control | Loại Control | Chức năng & Thuộc tính |
|---|---|---|
| `lblCartTitle` | `Label` | Tiêu đề "GIỎ HÀNG CỦA BẠN", Font 16pt Bold. |
| `dgvCart` | `DataGridView` | Hiển thị danh sách sản phẩm trong giỏ. |
| `panelCartSummary` | `Panel` | Panel tóm tắt thanh toán bên phải. |
| `lblSubTotal` | `Label` | Tiền tạm tính trước giảm giá. |
| `cboVouchers` | `ComboBox` | Danh sách chọn nhanh mã voucher khả dụng của người dùng. |
| `txtVoucherCode` | `TextBox` | Ô nhập mã giảm giá tùy ý. |
| `btnApplyVoucher` | `Button` | Nút kích hoạt kiểm tra và áp dụng voucher. |
| `lblDiscountAmount` | `Label` | Số tiền được giảm (Màu xanh lá / Đỏ nổi bật). |
| `lblCartTotal` | `Label` | Tổng tiền thanh toán thực tế (Font 16pt Bold, màu đỏ). |
| `lblUserBalance` | `Label` | Hiển thị số dư ví hiện tại của user (Font 14pt Bold, màu xanh lá). |
| `lblBalanceStatus`| `Label` | Nhãn báo đủ tiền hay thiếu tiền. |
| `btnPayWithWallet`| `Button` | Nút "THANH TOÁN BẰNG VÍ" (Nổi bật, kích thước lớn). |
| `btnGoToTopUp` | `Button` | Nút "Nạp thêm tiền ví" (Chuyển sang UI 5 `WalletControl`). |
| `btnGoToVouchers`| `Button` | Nút chuyển sang UI 15 `VoucherControl`. |
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
|  | Chủ tài khoản:  Trần Quốc Thuận (Username: thuantq)                                         |  |
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

---

## 3. Bản vẽ mô phỏng UI 15: Kho Mã giảm giá & Ưu đãi (`VoucherControl`)

### 3.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  KHO MÃ GIẢM GIÁ & ƯU ĐÃI CỦA BẠN                                                                 |
+---------------------------------------------------------------------------------------------------+
|  +---------------------------------------------------------------------------------------------+  |
|  | THU THẬP MÃ ƯU ĐÃI MỚI                                                                      |  |
|  | Nhập mã khuyến mãi: [ STEAM2026                   ]   [ 🎁 THU THẬP VOUCHER NGAY ]          |  |
|  +---------------------------------------------------------------------------------------------+  |
|                                                                                                   |
|  LỌC ƯU ĐÃI: (o) Tất cả   ( ) Khả dụng   ( ) Đã dùng                                              |
|                                                                                                   |
|  +---------------------------------------------------------------------------------------------+  |
|  | [🎟️ VOUCHER]  MÃ: CHAOBANMOI                     GIẢM: 20.000 VNĐ                           |  |
|  |              Áp dụng cho đơn từ 100.000 VNĐ      HSD: 31/12/2026                             |  |
|  |              Trạng thái: Khả dụng                [ 🛒 Dùng cho giỏ hàng ]  [ ❌ Xóa ]        |  |
|  +---------------------------------------------------------------------------------------------+  |
|  | [🎟️ VOUCHER]  MÃ: SALE10                         GIẢM: 10% (Tối đa 100k)                    |  |
|  |              Áp dụng cho mọi đơn hàng            HSD: 15/06/2026                             |  |
|  |              Trạng thái: Khả dụng                [ 🛒 Dùng cho giỏ hàng ]  [ ❌ Xóa ]        |  |
|  +---------------------------------------------------------------------------------------------+  |
|  | [🎟️ VOUCHER]  MÃ: TRIAN2025                      GIẢM: 50.000 VNĐ                           |  |
|  |              Áp dụng cho đơn từ 200.000 VNĐ      Ngày dùng: 10/01/2026                       |  |
|  |              Trạng thái: Đã sử dụng              [ ĐÃ DÙNG ]               [ ❌ Xóa ]        |  |
|  +---------------------------------------------------------------------------------------------+  |
+---------------------------------------------------------------------------------------------------+
|  [ < Trở về Giỏ hàng ]                                             Tổng cộng: 3 Voucher           |
+---------------------------------------------------------------------------------------------------+
```

### 3.2. Danh sách WinForms Controls gợi ý cho UI 15
| Tên Control | Loại Control | Chức năng & Thuộc tính |
|---|---|---|
| `lblVoucherTitle` | `Label` | Tiêu đề: "KHO MÃ GIẢM GIÁ & ƯU ĐÃI", Font 16pt Bold. |
| `txtNewVoucherCode` | `TextBox` | Ô nhập mã voucher mới muốn thu thập. |
| `btnCollectVoucher` | `Button` | Nút "Thu thập Voucher ngay". |
| `rdoAll` / `rdoAvailable` | `RadioButton` | Lọc voucher theo trạng thái (Tất cả / Khả dụng / Đã dùng). |
| `flowVouchers` | `FlowLayoutPanel` | Chứa các thẻ voucher trực quan. |
| `dgvVouchers` | `DataGridView` (Tùy chọn) | Dạng bảng: Mã Code, Mức giảm, Điều kiện, Hạn dùng, Trạng thái, Thao tác. |
| `btnUseVoucher` | `Button` | Điền mã này sang UI 4 Giỏ hàng và mở giỏ hàng. |
| `btnDeleteVoucher` | `Button` | Xóa voucher khỏi danh sách cá nhân (`DELETE FROM MaGiamGiaNguoiDung`). |
| `btnBackToCart` | `Button` | Quay lại màn hình `CartControl`. |
