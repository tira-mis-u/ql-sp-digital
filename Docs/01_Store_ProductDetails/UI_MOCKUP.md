# MÔ PHỎNG & MINH HỌA GIAO DIỆN (UI MOCKUP) - NHÓM 1
**Màn hình:** UI 2 (`StoreViewControl`), UI 3 (`ProductDetailControl`) & UI 14 (`ProductReviewControl`)

---

## 1. Bản vẽ mô phỏng UI 2: Cửa hàng / Trang chủ (`StoreViewControl`)

### 1.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  [🔍 Tìm kiếm sản phẩm...           ] [Nút: Tìm kiếm]  [Thể loại: Tất cả v]  [Sắp xếp: Mới nhất v]  [⭐ Đánh giá cao] |
+---------------------------------------------------------------------------------------------------+
|  DANH SÁCH SẢN PHẨM NỔI BẬT (FlowLayoutPanel / TableLayoutPanel)                                  |
|                                                                                                   |
|  +-----------------------+  +-----------------------+  +-----------------------+  +-------------+ |
|  | [ Ảnh Sản phẩm 1 ]    |  | [ Ảnh Sản phẩm 2 ]    |  | [ Ảnh Sản phẩm 3 ]    |  | [ Ảnh SP 4] | |
|  |                       |  |                       |  |                       |  |             | |
|  | Cyber Adventure RPG   |  | Photo Studio Pro 2026 |  | Low Poly Sci-Fi Pack  |  | Pixel Music | |
|  | Danh mục: Game        |  | Danh mục: Phần mềm    |  | Danh mục: 3D Models   |  | Danh mục:...| |
|  | Giá: 450.000 đ        |  | Giá: 1.200.000 đ      |  | Giá: 150.000 đ        |  | Giá: Miễn...| |
|  | ⭐ 4.8/5★   [Chi tiết]|  | ⭐ 4.5/5★   [Chi tiết]|  | ⭐ 5.0/5★   [Chi tiết]|  | ⭐ 4.0 [Xem]| |
|  +-----------------------+  +-----------------------+  +-----------------------+  +-------------+ |
|                                                                                                   |
|  +-----------------------+  +-----------------------+  +-----------------------+  +-------------+ |
|  | [ Ảnh Sản phẩm 5 ]    |  | [ Ảnh Sản phẩm 6 ]    |  | [ Ảnh Sản phẩm 7 ]    |  | [ Ảnh SP 8] | |
|  |                       |  |                       |  |                       |  |             | |
|  | Code Master Plugin    |  | Ambient Sounds FX     |  | Fantasy Hero Rig 3D   |  | Digital Art | |
|  | Giá: 300.000 đ        |  | Giá: 80.000 đ         |  | Giá: 520.000 đ        |  | Giá: 190.000| |
|  | ⭐ 4.2/5★   [Chi tiết]|  | ⭐ 4.9/5★   [Chi tiết]|  | ⭐ 4.7/5★   [Chi tiết]|  | ⭐ 4.1 [Xem]| |
|  +-----------------------+  +-----------------------+  +-----------------------+  +-------------+ |
+---------------------------------------------------------------------------------------------------+
| Trang 1 / 3   [<< Trước] [1] [2] [3] [Sau >>]                       Tổng cộng: 24 sản phẩm        |
+---------------------------------------------------------------------------------------------------+
```

### 1.2. Danh sách WinForms Controls gợi ý cho UI 2
| Tên Control | Loại Control | Chức năng & Thuộc tính |
|---|---|---|
| `panelTopBar` | `Panel` | `Dock = Top`, `Height = 50`. Chứa thanh tìm kiếm và bộ lọc. |
| `txtSearch` | `TextBox` | Nhập từ khóa tìm kiếm. `PlaceholderText = "Tìm kiếm sản phẩm..."` |
| `btnSearch` | `Button` | Nút "Tìm kiếm". Bắt sự kiện click để nạp lại danh sách. |
| `cboCategory` | `ComboBox` | Danh sách chọn danh mục: `DropDownStyle = DropDownList`. |
| `cboSort` | `ComboBox` | Sắp xếp: "Mới nhất", "Giá tăng dần", "Giá giảm dần", "Đánh giá cao nhất". |
| `chkHighRating` | `CheckBox` | Lọc nhanh các món được đánh giá cao (>= 4★). |
| `flowProductList` | `FlowLayoutPanel` | `Dock = Fill`, `AutoScroll = True`. Chứa các thẻ sản phẩm con. |
| `ProductCardControl` | `UserControl` con | Đại diện 1 thẻ sản phẩm: Chứa `PictureBox`, `Label` Tên, Giá, Đánh giá sao, `Button` Chi tiết. |

---

## 2. Bản vẽ mô phỏng UI 3: Chi tiết sản phẩm (`ProductDetailControl`)

### 2.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  [ < Quay lại Cửa hàng ]                                                                          |
+---------------------------------------------------------------------------------------------------+
|                                                                                                   |
|  +-------------------------------------+   +---------------------------------------------------+  |
|  |                                     |   | Tên: CYBER ADVENTURE RPG                          |  |
|  |                                     |   | Thể loại: Trò chơi điện tử (Game)                 |  |
|  |         [ ẢNH BANNER LỚN ]          |   | Nhà phát hành: Pixel Studio                       |  |
|  |            (PictureBox)             |   | Ngày ra mắt: 15/01/2026                           |  |
|  |                                     |   | Điểm đánh giá: ⭐⭐⭐⭐⭐ 4.8 / 5 (38 lượt review)  |  |
|  |                                     |   | Dung lượng tệp: 14.5 GB                           |  |
|  |                                     |   |---------------------------------------------------|  |
|  +-------------------------------------+   | GIÁ BÁN: 450.000 VNĐ                              |  |
|                                            |                                                   |  |
|                                            | [ 🛒 Thêm vào giỏ hàng ]    [ ⚡ Mua ngay ]       |  |
|                                            |                                                   |  |
|                                            | [ 💬 Xem & Viết đánh giá sản phẩm (UI 14) ]       |  |
|                                            +---------------------------------------------------+  |
|                                                                                                   |
|  +---------------------------------------------------------------------------------------------+  |
|  | MÔ TẢ CHI TIẾT SẢN PHẨM (RichTextBox / Label)                                               |  |
|  |---------------------------------------------------------------------------------------------|  |
|  | Cyber Adventure RPG là một tựa game nhập vai thế giới mở với đồ họa tương lai hiện đại...   |  |
|  | Định dạng bàn giao: Tệp cài đặt kỹ thuật số (.zip / .exe) + Khóa kích hoạt bản quyền.      |  |
|  | Hỗ trợ hệ điều hành: Windows 10, Windows 11 (64-bit).                                      |  |
|  +---------------------------------------------------------------------------------------------+  |
+---------------------------------------------------------------------------------------------------+
```

### 2.2. Danh sách WinForms Controls gợi ý cho UI 3
| Tên Control | Loại Control | Chức năng & Thuộc tính |
|---|---|---|
| `btnBack` | `Button` | Trở về màn hình `StoreViewControl`. |
| `picProductLarge` | `PictureBox` | Hiển thị ảnh đại diện cỡ lớn (`SizeMode = Zoom`). |
| `lblProductName` | `Label` | Tên sản phẩm, Font Size 18pt Bold. |
| `lblCategory` | `Label` | Tên thể loại sản phẩm. |
| `lblPrice` | `Label` | Giá sản phẩm, màu sắc nổi bật (Xanh lá / Đỏ), Font 16pt Bold. |
| `rtbDescription` | `RichTextBox` | Hiển thị mô tả sản phẩm (`ReadOnly = True`, `BorderStyle = None`). |
| `btnAddToCart` | `Button` | Thêm vào giỏ hàng. Hiển thị thông báo khi thành công. |
| `btnBuyNow` | `Button` | Chuyển thẳng đến Giỏ hàng & Thanh toán. |
| `btnGoToReviews` | `Button` | Chuyển đến màn hình Đánh giá sản phẩm UI 14. |

---

## 3. Bản vẽ mô phỏng UI 14: Quản lý Đánh giá & Nhận xét sản phẩm (`ProductReviewControl`)

### 3.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  TRUNG TÂM ĐÁNH GIÁ & NHẬN XÉT SẢN PHẨM (REVIEWS & RATINGS)                                       |
+---------------------------------------------------------------------------------------------------+
|  +--------------------------------------------+  +---------------------------------------------+  |
|  | [ KHU VỰC ĐÁNH GIÁ CỦA BẠN ]               |  | [ ĐÁNH GIÁ CỘNG ĐỒNG CHO SẢN PHẨM ]         |  |
|  |--------------------------------------------|  |---------------------------------------------|  |
|  | Chọn sản phẩm:                             |  | Sản phẩm đang xem: CYBER ADVENTURE RPG      |  |
|  | [ Cyber Adventure RPG                   v] |  | ĐIỂM TRUNG BÌNH: ⭐ 4.8 / 5.0 (12 đánh giá)  |  |
|  |                                            |  | Mức độ hài lòng: "Rất tích cực"             |  |
|  | Bạn đánh giá mức độ hài lòng:              |  |---------------------------------------------|  |
|  | ( ) 1 ⭐  ( ) 2 ⭐  ( ) 3 ⭐  ( ) 4 ⭐  (o) 5 ⭐|  | [Người dùng: thuantq] - ⭐⭐⭐⭐ (4/5)        |  |
|  |                                            |  | "Cốt truyện rất hay và cuốn hút, tối ưu..." |  |
|  | Lời nhận xét chi tiết:                     |  | Ngày: 12/03/2026                            |  |
|  | +----------------------------------------+ |  |---------------------------------------------|  |
|  | | Tựa game cực kỳ tuyệt vời! Đồ họa      | |  | [Người dùng: trimn] - ⭐⭐⭐⭐⭐ (5/5)         |  |
|  | | tương lai quá đỉnh, nhạc nền cũng rất  | |  | "Đồ họa đẹp mắt, đáng từng đồng tiền bát gạo|  |
|  | | bánh cuốn...                           | |  | Ngày: 10/03/2026                            |  |
|  | +----------------------------------------+ |  |---------------------------------------------|  |
|  |                                            |  | [BÀI ĐÁNH GIÁ CỦA BẠN] (ID: 105)            |  |
|  | [ ✍️ GỬI ĐÁNH GIÁ ]  [ 💾 LƯU THAY ĐỔI ]    |  | ⭐⭐⭐⭐⭐ (5/5) - "Tựa game cực kỳ tuyệt vời...|  |
|  |                                            |  | [ ✏️ Sửa bài viết ]       [ 🗑️ Xóa đánh giá ]|  |
|  +--------------------------------------------+  +---------------------------------------------+  |
+---------------------------------------------------------------------------------------------------+
```

### 3.2. Danh sách WinForms Controls gợi ý cho UI 14
| Tên Control | Loại Control | Chức năng & Thuộc tính |
|---|---|---|
| `lblReviewTitle` | `Label` | Tiêu đề: "TRUNG TÂM ĐÁNH GIÁ & NHẬN XÉT", Font Size 16pt Bold. |
| `cboProductSelect` | `ComboBox` | Danh sách chọn sản phẩm muốn đánh giá hoặc xem nhận xét. |
| `lblAvgRating` | `Label` | Hiển thị điểm trung bình: "⭐ 4.8 / 5.0", Font 18pt Bold, màu vàng cam. |
| `rdoRate1` -> `rdoRate5` | `RadioButton` | Chọn số sao đánh giá từ 1 đến 5 sao. |
| `txtReviewComment` | `TextBox` | Nhập nội dung nhận xét chi tiết (`Multiline = True`, `ScrollBars = Vertical`). |
| `btnSubmitReview` | `Button` | Nút gửi đánh giá mới (`INSERT INTO Reviews`). |
| `btnUpdateReview` | `Button` | Nút lưu chỉnh sửa nhận xét (`UPDATE Reviews`). |
| `btnDeleteReview` | `Button` | Nút xóa bài đánh giá của chính mình (`DELETE FROM Reviews`). |
| `flowReviewsList` / `dgvReviews` | `FlowLayoutPanel` / `DataGridView` | Hiển thị danh sách các bài nhận xét của cộng đồng (áp dụng Dark Mode scrollbar). |

*(Lưu ý: Màn hình không cần nút quay lại cửa hàng vì người dùng có thể điều hướng trực tiếp và linh hoạt qua thanh menu Sidebar bên trái).*
