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
|  [ Quay lại Cửa hàng ]                                                                            |
|                                                                                                   |
|  +--------------------------------------------------------------------+   +--------------------+  |
|  | CYBER ADVENTURE RPG                                                |   |     450.000 đ      |  |
|  | Thể loại: Trò chơi điện tử                                         |   |--------------------|  |
|  | Nhà phát hành: Pixel Game Studio                                   |   | [Thêm vào Giỏ hàng]|  |
|  | Phiên bản: 1.2.0           Dung lượng: 14.5 GB                     |   |--------------------|  |
|  | ⭐ 5.0 / 5.0 (1 lượt nhận xét)                                      |   |[Đánh giá & Nhận xét|  |
|  +--------------------------------------------------------------------+   +--------------------+  |
|                                                                                                   |
|  +---------------------------------------------+   +-------------------------------------------+  |
|  | GIỚI THIỆU & ĐẶC TẢ TÍNH NĂNG               |   | ĐÁNH GIÁ TỪ CỘNG ĐỒNG (1)  [+ Viết nhận xét]|  |
|  |---------------------------------------------|   |-------------------------------------------|  |
|  | Tựa game nhập vai thế giới mở phong cách    |   | Người đánh giá | Số sao | Nhận xét | Ngày |  |
|  | Cyberpunk với đồ họa tương lai đỉnh cao...  |   |-------------------------------------------|  |
|  |                                             |   | Trần Quốc Thuận| ⭐ 5/5 | Game Cyb...|17/10 |  |
|  | =========================================== |   |                                           |  |
|  | THÔNG TIN BẢN QUYỀN & THÔNG SỐ KỸ THUẬT:    |   |                                           |  |
|  | • Nền tảng: Windows 10/11 (64-bit)          |   |                                           |  |
|  | • Hình thức: Bản quyền số (DRM-Free)        |   |                                           |  |
|  | • Bảo hành: Cấp lại tệp tải trọn đời        |   |                                           |  |
|  +---------------------------------------------+   +-------------------------------------------+  |
+---------------------------------------------------------------------------------------------------+
```

### 2.2. Danh sách WinForms Controls & Tọa độ căn chỉnh chuẩn Visual Studio Designer
| Tên Control | Loại Control | Container | Kích thước & Tọa độ | Chức năng & Tương thích Designer |
|---|---|---|---|---|
| `btnBack` | `Button` | Root | `(25, 14)`, `Size = 180x36` | Trở về Cửa hàng. Tách biệt hoàn toàn ở trên cùng, không bị card `panelTop` che khuất hay đè lên. |
| `panelTop` | `Panel` | Root | `(25, 62)`, `Height = 205` | Khung thẻ thông tin tiêu đề, thể loại, NPH, phiên bản, dung lượng, điểm sao và cụm mua sắm. |
| `lblTitle` | `Label` | `panelTop` | `(20, 14)`, `Font = 16pt Bold` | Tên sản phẩm kỹ thuật số. Co giãn linh hoạt theo chiều rộng cửa sổ. |
| `panelBuy` | `Panel` | `panelTop` | `Dock = Right`, `Width = 310` | Cụm mua sắm & đánh giá: gồm giá bán niêm yết, nút Thêm giỏ hàng và nút Đánh giá & Nhận xét. |
| `lblPrice` | `Label` | `panelBuy` | `Dock = Top`, `Height = 40` | Giá bán sản phẩm định dạng VNĐ (hoặc Miễn phí). |
| `btnAddToCart` | `Button` | `panelBuy` | `Dock = Top`, `Height = 48` | Thêm sản phẩm vào giỏ hàng cá nhân (`GioHangService.ThemVaoGioHang`). |
| `btnReviews` | `Button` | `panelBuy` | `Dock = Bottom`, `Height = 38` | Chuyển đến màn hình quản lý đánh giá UI 14 (`ProductReviewControl`). |
| `panelBottom` | `Panel` | Root | `(25, 279)`, `Height = dynamic` | Khung chứa 2 khối thông tin chi tiết: Giới thiệu (trái) và Đánh giá cộng đồng (phải). |
| `panelDesc` | `Panel` | `panelBottom` | `Left = 0`, `Width = 48%` | Khung hiển thị mô tả, yêu cầu kỹ thuật và bản quyền DRM-Free. |
| `lblDescTitle` | `Label` | `panelDesc` | `Dock = Top`, `Height = 36` | Tiêu đề "GIỚI THIỆU & ĐẶC TẢ TÍNH NĂNG" căn chỉnh chuẩn 36px. |
| `rtfDescription` | `RichTextBox` | `panelDesc` | `Dock = Fill` | Nội dung mô tả chi tiết và quy chuẩn kỹ thuật tệp số. |
| `panelReviews` | `Panel` | `panelBottom` | `Right = 50%`, `Width = 50%` | Khung danh sách đánh giá từ cộng đồng kèm nút viết nhận xét. |
| `panelReviewsHeader` | `Panel` | `panelReviews` | `Dock = Top`, `Height = 36` | Thanh tiêu đề đánh giá kèm nút hành động. |
| `lblReviewsTitle` | `Label` | `panelReviewsHeader` | `Dock = Fill`, `Height = 36` | Tiêu đề "ĐÁNH GIÁ TỪ CỘNG ĐỒNG (N)" căn giữa thẳng hàng với nút viết nhận xét. |
| `btnWriteReview` | `Button` | `panelReviewsHeader` | `Dock = Right`, `Size = 140x36` | Nút "+ Viết nhận xét" mở nhanh giao diện đánh giá UI 14, không bị DataGridView đè khuất. |
| `dgvReviewsDetail` | `DataGridView` | `panelReviews` | `(17, 66)`, `Size = dynamic` | Bảng danh sách nhận xét cộng đồng; cách header và nút nhận xét 10px khoảng đệm an toàn. |
| `lblNoReviews` | `Label` | `panelReviews` | `(17, 100)`, `Size = dynamic` | Thông báo hiển thị khi sản phẩm chưa có nhận xét nào từ cộng đồng. |

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
