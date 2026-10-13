# MÔ PHỎNG & MINH HỌA GIAO DIỆN (UI MOCKUP) - NHÓM 1
**Màn hình:** UI 2 (`StoreViewControl`) & UI 3 (`ProductDetailControl`)

---

## 1. Bản vẽ mô phỏng UI 2: Cửa hàng / Trang chủ (`StoreViewControl`)

### 1.1. Bố cục trực quan (Wireframe Layout)
```text
+---------------------------------------------------------------------------------------------------+
|  [🔍 Tìm kiếm sản phẩm...           ] [Nút: Tìm kiếm]  [Thể loại: Tất cả v]  [Sắp xếp: Mới nhất v]  [❤️ Yêu thích] |
+---------------------------------------------------------------------------------------------------+
|  DANH SÁCH SẢN PHẨM NỔI BẬT (FlowLayoutPanel / TableLayoutPanel)                                  |
|                                                                                                   |
|  +-----------------------+  +-----------------------+  +-----------------------+  +-------------+ |
|  | [ Ảnh Sản phẩm 1 ]    |  | [ Ảnh Sản phẩm 2 ]    |  | [ Ảnh Sản phẩm 3 ]    |  | [ Ảnh SP 4] | |
|  |                       |  |                       |  |                       |  |             | |
|  | Cyber Adventure RPG   |  | Photo Studio Pro 2026 |  | Low Poly Sci-Fi Pack  |  | Pixel Music | |
|  | Danh mục: Game        |  | Danh mục: Phần mềm    |  | Danh mục: 3D Models   |  | Danh mục:...| |
|  | Giá: 450.000 đ        |  | Giá: 1.200.000 đ      |  | Giá: 150.000 đ        |  | Giá: Miễn...| |
|  | [❤️ Thích]  [Chi tiết] |  | [🤍 Thích]  [Chi tiết] |  | [❤️ Thích]  [Chi tiết] |  | [🤍] [Xem]  | |
|  +-----------------------+  +-----------------------+  +-----------------------+  +-------------+ |
|                                                                                                   |
|  +-----------------------+  +-----------------------+  +-----------------------+  +-------------+ |
|  | [ Ảnh Sản phẩm 5 ]    |  | [ Ảnh Sản phẩm 6 ]    |  | [ Ảnh Sản phẩm 7 ]    |  | [ Ảnh SP 8] | |
|  |                       |  |                       |  |                       |  |             | |
|  | Code Master Plugin    |  | Ambient Sounds FX     |  | Fantasy Hero Rig 3D   |  | Digital Art | |
|  | Giá: 300.000 đ        |  | Giá: 80.000 đ         |  | Giá: 520.000 đ        |  | Giá: 190.000| |
|  | [🤍 Thích]  [Chi tiết] |  | [🤍 Thích]  [Chi tiết] |  | [❤️ Thích]  [Chi tiết] |  | [🤍] [Xem]  | |
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
| `cboSort` | `ComboBox` | Sắp xếp: "Mới nhất", "Giá tăng dần", "Giá giảm dần". |
| `chkOnlyFavorites` | `CheckBox` / `Button` | Lọc nhanh các món đã thích. |
| `flowProductList` | `FlowLayoutPanel` | `Dock = Fill`, `AutoScroll = True`. Chứa các thẻ sản phẩm con. |
| `ProductCardControl` | `UserControl` con | Đại diện 1 thẻ sản phẩm: Chứa `PictureBox`, `Label` Tên, Giá, `Button` Thích, `Button` Xem. |

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
|  |                                     |   | Dung lượng tệp: 14.5 GB                           |  |
|  |                                     |   |---------------------------------------------------|  |
|  +-------------------------------------+   | GIÁ BÁN: 450.000 VNĐ                              |  |
|                                            |                                                   |  |
|                                            | [ 🛒 Thêm vào giỏ hàng ]    [ ⚡ Mua ngay ]       |  |
|                                            |                                                   |  |
|                                            | [ ❤️ Thêm vào danh sách yêu thích ]               |  |
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
| `btnToggleFavorite`| `Button` | Thêm/Xóa khỏi bảng Yêu thích. |
