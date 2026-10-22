# ĐẶC TẢ TOÀN BỘ NGHIỆP VỤ THEO TỪNG NHÓM CHỨC NĂNG (5 NHÓM - 15 MÀN HÌNH)
**Dự án:** Digital Product Management System (DPMS)  
**Quy mô triển khai:** 5 Nhóm nhiệm vụ độc lập - 15 Màn hình `UserControl` trên nền tảng WinForms C#  
**Kiến trúc:** 3-Layer Architecture (Presentation $\rightarrow$ Business Logic $\rightarrow$ Data Access $\rightarrow$ SQL Server)

---

## BẢNG TỔNG QUAN PHÂN CÔNG 5 NHÓM NHIỆM VỤ

| Nhóm | Thành viên phụ trách | Phân hệ nghiệp vụ | 3 Màn hình WinForms phụ trách | Thư mục mã nguồn giao diện | Thư mục tài liệu đặc tả |
|:---:|---|---|---|---|---|
| **1** | **Lê Nguyễn Minh Trí** | **Store, Details & Reviews** | UI 2: Cửa hàng / Trang chủ<br>UI 3: Chi tiết sản phẩm<br>UI 14: Đánh giá & Nhận xét (1-5★) | [`../../qlspdigital/qlspdigital/Views/Nhom1_Store/`](../../qlspdigital/qlspdigital/Views/Nhom1_Store/) | [`../01_Store_ProductDetails/`](../01_Store_ProductDetails/) |
| **2** | **Nguyễn Quốc Trung** | **Cart, Wallet & Vouchers** | UI 4: Giỏ hàng & Thanh toán 1-Click<br>UI 5: Ví tài khoản & Nạp tiền<br>UI 15: Kho Voucher & Mã ưu đãi | [`../../qlspdigital/qlspdigital/Views/Nhom2_Cart/`](../../qlspdigital/qlspdigital/Views/Nhom2_Cart/) | [`../02_Cart_Wallet/`](../02_Cart_Wallet/) |
| **3** | **Phạm Trung Hiếu** | **Product, Category & Profile** | UI 10: Quản lý sản phẩm & Tệp số<br>UI 11: Quản lý danh mục thể loại<br>UI 8: Hồ sơ cá nhân & Đổi mật khẩu | [`../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/`](../../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/) | [`../03_Product_Category_Profile/`](../03_Product_Category_Profile/) |
| **4** | **Trần Quốc Thuận** | **Auth, User Admin & Dashboard** | UI 1: Đăng nhập & Đăng ký tài khoản<br>UI 12: Quản lý người dùng toàn hệ thống<br>UI 9: Báo cáo chỉ số Dashboard | [`../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/`](../../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/) | [`../04_Auth_User_Dashboard/`](../04_Auth_User_Dashboard/) |
| **5** | **Phạm Văn Đam** | **Orders, History & Library** | UI 13: Quản lý đơn hàng toàn sàn<br>UI 7: Lịch sử mua hàng cá nhân<br>UI 6: Thư viện số & Tải tệp mô phỏng | [`../../qlspdigital/qlspdigital/Views/Nhom5_Orders/`](../../qlspdigital/qlspdigital/Views/Nhom5_Orders/) | [`../05_Orders_Library/`](../05_Orders_Library/) |

---

## NHÓM 1: PHÂN HỆ CỬA HÀNG, CHI TIẾT SẢN PHẨM & ĐÁNH GIÁ (STORE, PRODUCT DETAILS & REVIEWS)
**Thành viên phụ trách:** Lê Nguyễn Minh Trí  
**Mã nguồn liên quan:**  
- Giao diện: `StoreViewControl.cs` (UI 2), `ProductDetailControl.cs` (UI 3), `ProductReviewControl.cs` (UI 14), `ProductCardControl.cs` (Component dùng chung).
- Nghiệp vụ & Dữ liệu: `SanPhamService.cs`, `DanhGiaService.cs`, `SanPhamRepository.cs`, `DanhGiaRepository.cs`.
- Bảng CSDL thao tác: `SanPham`, `DanhMuc`, `NhaPhatHanh`, `DanhGia`.

### 1. Nghiệp vụ UI 2 - Cửa hàng / Trang chủ (`StoreViewControl`)
- **Hiển thị danh sách sản phẩm dạng thẻ lưới (Card Layout):**
  - Sử dụng `FlowLayoutPanel` nạp động các `ProductCardControl`.
  - Mỗi thẻ sản phẩm hiển thị: Ảnh bìa (tải từ đường dẫn tương đối `Assets/Images/...` hoặc ảnh mặc định nếu null), Tiêu đề sản phẩm, Tên danh mục, Giá tiền định dạng chuẩn Việt Nam Đồng (VD: `590.000 ₫` hoặc nhãn `"Miễn phí"` nếu giá bằng 0), Điểm đánh giá sao trung bình kèm biểu tượng ngôi sao vàng (`⭐ 4.8 / 5.0`).
- **Tìm kiếm theo từ khóa:**
  - Ô nhập liệu `txtSearch`: Lắng nghe sự kiện gõ phím Enter hoặc bấm nút "Tìm kiếm".
  - Thực hiện câu lệnh SQL `LIKE N'%' + @TuKhoa + '%'`. Tự động xóa khoảng trắng thừa ở hai đầu.
- **Bộ lọc theo danh mục thể loại:**
  - `cboCategory`: Nạp danh sách danh mục từ bảng `DanhMuc` (thêm tùy chọn `"Tất cả thể loại"` ở đầu).
  - Khi người dùng chọn một thể loại, danh sách sản phẩm lập tức được lọc lại mà không cần tải lại toàn bộ trang.
- **Sắp xếp linh hoạt (Sorting):**
  - Hỗ trợ 4 tiêu chí sắp xếp: Giá tăng dần, Giá giảm dần, Mới nhất (theo `NgayTao DESC`), và Đánh giá cao nhất (theo `DiemDanhGiaTrungBinh DESC`).
- **Điều hướng chuyển cảnh:**
  - Nhấp đúp hoặc bấm nút "Xem chi tiết" trên thẻ sản phẩm: Kích hoạt sự kiện `OnViewProductDetail(maSanPham)` để `MainForm` chuyển sang UI 3.
  - Nút liên kết nhanh "Trung tâm Đánh giá & Phản hồi": Chuyển sang UI 14 (`ProductReviewControl`).

### 2. Nghiệp vụ UI 3 - Chi tiết sản phẩm (`ProductDetailControl`)
- **Hiển thị thông số kỹ thuật và thương mại đầy đủ:**
  - Tên sản phẩm, banner hình ảnh kích thước lớn, nhà phát hành, ngày ra mắt, phiên bản (VD: `1.0.4`), dung lượng tệp (`DungLuongMB`).
  - Giá bán chính thức và nhãn trạng thái kinh doanh.
  - Vùng mô tả chi tiết: Tính năng sản phẩm, cấu hình máy tính yêu cầu (HĐH, CPU, RAM, Card đồ họa).
  - **Tóm tắt chỉ số đánh giá cộng đồng:** Điểm trung bình (làm tròn 1 chữ số thập phân, VD: `4.6`), tổng số lượt đánh giá, và phân loại cảm nhận theo chuẩn Steam (từ 4.5★ trở lên: *"Rất tích cực"*; từ 3.5★ đến 4.4★: *"Tích cực"*; từ 2.5★ đến 3.4★: *"Hỗn hợp"*; dưới 2.5★: *"Tiêu cực"*).
- **Hành động mua sắm và điều hướng:**
  - Nút **"Thêm vào giỏ hàng"**: Kiểm tra đăng nhập $\rightarrow$ Thêm sản phẩm vào bảng `GioHang` của tài khoản $\rightarrow$ Bắn sự kiện cập nhật số lượng huy hiệu giỏ hàng trên Header $\rightarrow$ Thông báo xác nhận.
  - Nút **"Mua ngay"**: Tự động thực hiện thao tác kép: Thêm vào giỏ và chuyển hướng ngay lập tức sang UI 4 (`CartControl`) để sẵn sàng thanh toán.
  - Nút **"⭐ Viết đánh giá sản phẩm"**: Chuyển thẳng sang UI 14 với sản phẩm hiện tại đã được chọn sẵn.
  - Nút **"Quay lại"**: Trở về danh sách Cửa hàng trước đó.

### 3. Nghiệp vụ UI 14 - Quản lý Đánh giá & Nhận xét (`ProductReviewControl`)
- **Xem danh sách và tỷ lệ phần trăm đánh giá (CRUD Read):**
  - ComboBox chọn sản phẩm muốn xem đánh giá.
  - Bảng thống kê trực quan: Điểm trung bình, tổng lượt đánh giá, thanh đo tỷ lệ % của các mức sao (5 sao, 4 sao, 3 sao, 2 sao, 1 sao).
  - Danh sách bài đánh giá: Tên người dùng, số sao chấm (⭐), nội dung bình luận, ngày đăng bài.
- **Viết đánh giá mới (CRUD Create):**
  - Chỉ cho phép người dùng đã đăng nhập và đã sở hữu sản phẩm.
  - Người dùng chọn mức sao (1 đến 5 sao) qua RadioButton hoặc ComboBox, nhập lời bình vào `txtReviewComment`.
  - Bấm nút **"Gửi đánh giá"** $\rightarrow$ Kiểm tra hợp lệ $\rightarrow$ `INSERT INTO DanhGia` $\rightarrow$ Tự động tính toán lại điểm trung bình của sản phẩm.
- **Chỉnh sửa và Xóa đánh giá của chính mình (CRUD Update & Delete):**
  - Hệ thống tự nhận diện bài đánh giá thuộc về `UserSession.CurrentUserId`.
  - Nút **"Sửa nhận xét"**: Đổ lại số sao và nội dung cũ lên form để chỉnh sửa $\rightarrow$ Bấm lưu $\rightarrow$ `UPDATE DanhGia`.
  - Nút **"Xóa đánh giá"**: Xác nhận hộp thoại $\rightarrow$ Thực thi `DELETE FROM DanhGia` $\rightarrow$ Cập nhật lại danh sách hiển thị và điểm số ngay lập tức.

### 4. Quy tắc nghiệp vụ (Business Rules) Nhóm 1:
- **BR1.1 (Sản phẩm kinh doanh):** Chỉ hiển thị các sản phẩm có `DangKinhDoanh = 1` tại Cửa hàng. Sản phẩm có `DangKinhDoanh = 0` bị ẩn khỏi Cửa hàng nhưng vẫn bảo lưu quyền tải về trong Thư viện của khách đã mua.
- **BR1.2 (Đánh giá độc nhất - Unique Review):** Mỗi người dùng chỉ được phép đánh giá tối đa 1 lần cho mỗi sản phẩm (`UNIQUE(MaNguoiDung, MaSanPham)`). Muốn thay đổi quan điểm, người dùng phải sử dụng chức năng Sửa đánh giá.
- **BR1.3 (Ràng buộc điểm sao):** Số sao bắt buộc phải là số nguyên từ 1 đến 5 (`SoSao BETWEEN 1 AND 5`).

---

## NHÓM 2: PHÂN HỆ GIỎ HÀNG, VÍ TÀI KHOẢN & MÃ GIẢM GIÁ (CART, STEAM WALLET & VOUCHERS)
**Thành viên phụ trách:** Nguyễn Quốc Trung  
**Mã nguồn liên quan:**  
- Giao diện: `CartControl.cs` (UI 4), `WalletControl.cs` (UI 5), `VoucherControl.cs` (UI 15).
- Nghiệp vụ & Dữ liệu: `GioHangService.cs`, `ViTaiKhoanService.cs`, `MaGiamGiaService.cs`, `ThanhToanService.cs`, `GioHangRepository.cs`, `ViTaiKhoanRepository.cs`, `MaGiamGiaRepository.cs`.
- Bảng CSDL thao tác: `GioHang`, `NguoiDung` (`SoDuVi`), `MaGiamGia`, `MaGiamGiaNguoiDung`, `DonHang`, `ChiTietDonHang`, `ThanhToan`, `SoHuuSanPham`.

### 1. Nghiệp vụ UI 4 - Giỏ hàng & Thanh toán 1-Click (`CartControl`)
- **Hiển thị danh sách món hàng trong giỏ (CRUD Read):**
  - DataGridView danh sách: Tên sản phẩm, Danh mục, Đơn giá, Số lượng, Thành tiền, Nút xóa món.
  - Khung tổng kết tài chính:
    - **Tạm tính:** Tổng tiền gốc của toàn bộ sản phẩm trong giỏ (`SUM(DonGia * SoLuong)`).
    - **Giảm giá Voucher:** Số tiền được chiết khấu từ mã ưu đãi đang áp dụng.
    - **Tổng thanh toán thực tế:** `Tạm tính - Giảm giá` (tối thiểu là 0đ).
    - **Số dư ví khả dụng:** Hiển thị nổi bật số dư hiện có của tài khoản để đối chiếu.
- **Cập nhật số lượng và Xóa món (CRUD Update & Delete):**
  - **Quy tắc bản quyền số độc bản:** Số lượng trong giỏ hàng luôn cố định là 1 bản quyền (`SoLuong = 1`). Hệ thống kiểm tra trùng lặp (`DaCoTrongGioHang`) và từ chối thêm nếu sản phẩm đã có trong giỏ hàng của tài khoản.
  - Nút "Xóa" từng dòng khỏi giỏ hàng.
  - Nút "Xóa tất cả": Dọn sạch giỏ hàng với một cú click.
  - Mỗi khi thay đổi giỏ hàng, `MainForm.UpdateCartBadge()` được gọi tường minh để đồng bộ huy hiệu số lượng trên thanh Sidebar.
- **Áp dụng Mã giảm giá (Voucher Application):**
  - Ô nhập mã code hoặc chọn từ kho voucher khả dụng.
  - Bấm nút **"Áp dụng"**: Hệ thống kiểm tra tính hợp lệ (mã tồn tại, còn hạn, chưa sử dụng, đạt giá trị `DonToiThieu`). Nếu thỏa mãn, tính toán số tiền khấu trừ (theo % có khống chế `GiamToiDa` hoặc trừ tiền mặt trực tiếp).
  - Nút **"Hủy mã"**: Hủy áp dụng voucher, trả về tổng tiền thanh toán ban đầu.
- **Thanh toán 1-Click bằng số dư ví (Transaction Mua hàng ACID):**
  - Nút **"⚡ THANH TOÁN BẰNG SỐ DƯ VÍ"**:
    - **Kịch bản Đủ tiền (`SoDuVi >= TongThanhToan`):**
      1. Khởi tạo một `SqlTransaction` cấp độ bảo đảm toàn vẹn dữ liệu cao nhất.
      2. Trừ tiền ví tài khoản: `UPDATE NguoiDung SET SoDuVi = SoDuVi - @TongThanhToan`.
      3. Ghi nhận giao dịch trừ ví vào bảng `ThanhToan` (`PhuongThucThanhToan = N'ViTaiKhoan'`, `TrangThaiThanhToan = N'Thanh công'`).
      4. Tạo đơn hàng mới trong bảng `DonHang` (`TrangThaiDonHang = N'Hoàn thành'`, ghi nhận `MaDonHangCode`, `TamTinh`, `TienGiamGia`, `TongThanhToan`).
      5. Lưu từng sản phẩm vào bảng `ChiTietDonHang` (lưu giá tại thời điểm mua).
      6. Cấp quyền sở hữu vĩnh viễn: Thực thi `INSERT INTO SoHuuSanPham` cho từng sản phẩm trong đơn.
      7. Nếu có dùng voucher: Đánh dấu `DaSuDung = 1` và cập nhật `NgaySuDung = GETDATE()` trong bảng `MaGiamGiaNguoiDung`.
      8. Xóa sạch giỏ hàng `DELETE FROM GioHang WHERE MaNguoiDung = @UserId`.
      9. `CommitTransaction()`.
      10. Bắn sự kiện `UserSession.UpdateBalance()` cập nhật số dư hiển thị trên Header `MainForm`.
      11. Thông báo chúc mừng và cung cấp nút chuyển nhanh sang Thư viện để tải game ngay.
    - **Kịch bản Thiếu tiền (`SoDuVi < TongThanhToan`):**
      - Hệ thống chặn giao dịch, tính toán số tiền còn thiếu (`Thieu = TongThanhToan - SoDuVi`).
      - Hiển thị hộp thoại cảnh báo: *"Số dư ví không đủ (Bạn còn thiếu X ₫)! Vui lòng nạp thêm tiền vào ví để mua sắm."*.
      - Cung cấp nút bấm chuyển thẳng sang UI 5 (`WalletControl`) để nạp tiền.

### 2. Nghiệp vụ UI 5 - Quản lý Ví tài khoản & Nạp tiền mô phỏng (`WalletControl`)
- **Hiển thị thông tin ví:**
  - Tên chủ tài khoản, Username, Trạng thái hoạt động của ví.
  - Số dư khả dụng hiện tại (font chữ lớn, màu xanh lục nổi bật).
- **Nạp tiền mô phỏng vào ví (CRUD Update):**
  - Cung cấp các nút bấm chọn nhanh gói mệnh giá chuẩn Steam: `+ 50.000 ₫`, `+ 100.000 ₫`, `+ 200.000 ₫`, `+ 500.000 ₫`, `+ 1.000.000 ₫`.
  - Ô nhập số tiền tùy chọn nếu người dùng muốn nạp con số cụ thể khác (ràng buộc tối thiểu 10.000đ).
  - Bấm nút **"XÁC NHẬN NẠP TIỀN VÀO VÍ"**:
    - Thực hiện `UPDATE NguoiDung SET SoDuVi = SoDuVi + @SoTienNap`.
    - Ghi nhận lịch sử giao dịch nạp tiền vào bảng `ThanhToan`.
    - Gọi `UserSession.UpdateBalance(newBalance)` để cập nhật số dư trong phiên làm việc, sau đó `mainForm.UpdateUserInterfaceSession()` được gọi để làm mới `lblBalance` trên Header ngay tức khắc.
    - Cập nhật số dư trên Header ngay tức khắc.
- **Xem lịch sử biến động số dư:**
  - Bảng DataGridView hiển thị danh sách các lần nạp tiền và trừ tiền mua sắm (Mã giao dịch, Thời gian, Loại giao dịch, Số tiền biến động, Trạng thái).

### 3. Nghiệp vụ UI 15 - Quản lý Kho Voucher & Mã ưu đãi (`VoucherControl`)
- **Vòng đời và 3 trạng thái Voucher:**
  - **Trạng thái 1: "Chưa lưu" (Chưa vào ví):** Mã khuyến mãi hệ thống phát hành trong bảng `MaGiamGia` mà tài khoản chưa thu thập. Hiển thị ở bảng gợi ý để người dùng bấm "Thu thập mã vào ví".
  - **Trạng thái 2: "Đã trong ví" (Sẵn sàng sử dụng):** Mã đã được thu thập vào bảng `MaGiamGiaNguoiDung` với `DaSuDung = 0`. Người dùng có thể bấm "Dùng ngay" để kích hoạt vào giỏ hàng hoặc bấm "Xóa khỏi ví".
  - **Trạng thái 3: "Đã sử dụng" (Vô hiệu hóa):** Mã đã được áp dụng vào đơn hàng thanh toán thành công (`DaSuDung = 1`). Không cho phép tái sử dụng.
- **Thu thập mã ưu đãi mới (CRUD Create):**
  - Ô nhập ký tự code: Người dùng nhập mã (VD: `CHAOBANMOI`, `SALE10`, `STEAM50K`).
  - Bấm nút **"Thu thập mã vào ví"**: Kiểm tra mã có tồn tại, còn hạn và tài khoản chưa từng thu thập $\rightarrow$ Thực thi `INSERT INTO MaGiamGiaNguoiDung`.
- **Thao tác nhanh:**
  - Nút **"Dùng ngay (Áp dụng vào giỏ)"**: Tự động mở UI 4 và điền sẵn mã ưu đãi cho giỏ hàng hiện hành.
  - Nút **"Xóa khỏi ví"**: Xóa mã không có nhu cầu khỏi kho cá nhân.

### 4. Quy tắc nghiệp vụ (Business Rules) Nhóm 2:
- **BR2.1 (Số dư không âm):** Cột `SoDuVi` có ràng buộc kiểm tra `CHECK (SoDuVi >= 0)`. Tuyệt đối không cho phép tài khoản bị số dư âm dưới bất kỳ hình thức nào.
- **BR2.2 (Tính toàn vẹn giao dịch mua hàng):** Toàn bộ chuỗi thao tác thanh toán (trừ tiền ví, tạo đơn, lưu chi tiết, cấp thư viện, đánh dấu voucher, xóa giỏ) bắt buộc phải nằm trong **1 Transaction duy nhất**. Bất kỳ bước nào gặp lỗi phải thực hiện `Rollback` toàn bộ để tránh mất tiền oan cho khách hàng.
- **BR2.3 (Chống trùng lặp sản phẩm trong giỏ):** Cặp `(MaNguoiDung, MaSanPham)` trong bảng `GioHang` có ràng buộc duy nhất. Nếu thêm lại món đã có, hệ thống chỉ tăng số lượng hoặc giữ nguyên.

---

## NHÓM 3: PHÂN HỆ QUẢN TRỊ SẢN PHẨM, DANH MỤC & HỒ SƠ CÁ NHÂN (PRODUCT, CATEGORY & PROFILE)
**Thành viên phụ trách:** Phạm Trung Hiếu  
**Mã nguồn liên quan:**  
- Giao diện: `ProductManageControl.cs` (UI 10), `CategoryManageControl.cs` (UI 11), `ProfileControl.cs` (UI 8).
- Nghiệp vụ & Dữ liệu: `SanPhamAdminService.cs`, `DanhMucService.cs`, `HoSoService.cs`, `SanPhamAdminRepository.cs`, `DanhMucRepository.cs`, `HoSoRepository.cs`.
- Bảng CSDL thao tác: `SanPham`, `TepSanPham`, `DanhMuc`, `NguoiDung`, `CaiDatNguoiDung`.

### 1. Nghiệp vụ UI 10 - Quản lý sản phẩm & Tệp kỹ thuật số (`ProductManageControl`)
- **Xem và tìm kiếm danh sách sản phẩm (CRUD Read):**
  - Bảng DataGridView: Mã SP, Tên sản phẩm, Danh mục, Nhà phát hành, Giá bán, Phiên bản, Trạng thái kinh doanh (`DangKinhDoanh = 1: Đang bán / 0: Ngưng bán`).
  - Thanh công cụ lọc: Lọc nhanh theo danh mục và tìm kiếm theo tên sản phẩm.
- **Thêm sản phẩm mới (CRUD Create):**
  - Form nhập thông tin: Tên sản phẩm, chọn danh mục, chọn nhà phát hành, giá bán, phiên bản, mô tả.
  - Nút **"Chọn ảnh bìa"** (`OpenFileDialog`): Chọn ảnh `.jpg`/`.png` $\rightarrow$ Tự động sao chép vào thư mục `Assets/Images/` và lưu đường dẫn tương đối vào database.
  - Bấm **"Thêm sản phẩm"** $\rightarrow$ Thực thi `INSERT INTO SanPham` $\rightarrow$ Lấy về mã định danh vừa tạo qua `SCOPE_IDENTITY()`.
- **Quản lý Tải lên tệp số vật lý & Lưu siêu dữ liệu (Digital Files Upload):**
  - Nút **"📁 Chọn tệp kỹ thuật số..."** (`OpenFileDialog` hỗ trợ `.zip`, `.rar`, `.exe`, `.iso`, `.pdf`):
    1. Quản trị viên chọn file bộ cài đặt từ máy tính (`OpenFileDialog`).
    2. Hệ thống tự động trích xuất: Tên file gốc (`SafeFileName`), phần mở rộng, dung lượng byte chính xác và quy đổi ra Megabyte (MB).
    3. Sao chép tệp vật lý vào thư mục lưu trữ ứng dụng: `Storage/Products/{TenTep}`.
    4. Lưu siêu dữ liệu vào bảng `TepSanPham`: `INSERT INTO TepSanPham (MaSanPham, TenTep, DuongDanTep, DinhDangTep, KichThuocByte, PhienBan)`.
  - Bảng hiển thị thông tin tệp số liên kết, cho phép tải lên tệp mới để cập nhật phiên bản.
- **Cập nhật và Xóa mềm sản phẩm (CRUD Update & Delete):**
  - Chọn sản phẩm trên bảng $\rightarrow$ Nạp ngược dữ liệu lên form để chỉnh sửa thông tin/giá bán $\rightarrow$ Bấm nút lưu thay đổi.
  - Nút **"Ngưng bán / Xóa"**: Thực hiện **Xóa mềm (Soft Delete)** bằng câu lệnh `UPDATE SanPham SET DangKinhDoanh = 0`. Tuyệt đối không xóa cứng (Hard Delete) khỏi database để đảm bảo toàn vẹn dữ liệu cho các đơn hàng đã mua trong quá khứ.
  - Nút **"Làm mới"**: Xóa trắng form để sẵn sàng thêm sản phẩm mới.

### 2. Nghiệp vụ UI 11 - Quản lý danh mục thể loại (`CategoryManageControl`)
- **Hiển thị danh sách danh mục (CRUD Read):**
  - DataGridView: Mã danh mục, Tên danh mục, Mô tả, Số lượng sản phẩm hiện có trực thuộc danh mục đó (`COUNT(MaSanPham)`).
- **Thêm và Cập nhật danh mục (CRUD Create & Update):**
  - Nhập tên danh mục (ràng buộc không được trùng lặp `UNIQUE`), nhập mô tả.
  - Bấm nút Thêm mới hoặc Cập nhật thông tin.
- **Xóa danh mục có ràng buộc khóa ngoại (CRUD Delete):**
  - Bấm nút **"Xóa danh mục"**:
    - Hệ thống thực hiện kiểm tra nghiệp vụ: `SELECT COUNT(1) FROM SanPham WHERE MaDanhMuc = @MaDanhMuc`.
    - **Nếu `COUNT > 0` (đang có sản phẩm trực thuộc):** Lập tức chặn hành động và hiển thị cảnh báo: *"Không thể xóa danh mục này vì đang có X sản phẩm liên kết! Vui lòng chuyển các sản phẩm sang danh mục khác trước khi xóa."*.
    - **Nếu `COUNT == 0` (danh mục rỗng):** Cho phép thực thi `DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc`.

### 3. Nghiệp vụ UI 8 - Hồ sơ cá nhân, Đổi mật khẩu & Cài đặt hệ thống (`ProfileControl`)
- **Xem thông tin tài khoản (CRUD Read):**
  - Tên đăng nhập (chỉ đọc), Vai trò (`QuanTriVien` hoặc `KhachHang`), Số dư ví hiện tại, Họ và tên, Email, Số điện thoại.
- **Cập nhật thông tin cá nhân & Đồng bộ Realtime (CRUD Update):**
  - Người dùng chỉnh sửa Họ tên, Email, Số điện thoại $\rightarrow$ Bấm **"Lưu thay đổi hồ sơ"** $\rightarrow$ `UPDATE NguoiDung`.
  - **Cơ chế cập nhật phiên:** Hệ thống gọi `UserSession.UpdateProfile(hoTen, email)` để cập nhật thông tin trong phiên hiện tại, sau đó gọi tường minh `mainForm.UpdateUserInterfaceSession()` để nhãn chào trên thanh Header cập nhật lại họ tên mới ngay tức khắc mà không cần đăng nhập lại.
- **Đổi mật khẩu an toàn:**
  - Nhập Mật khẩu cũ, Mật khẩu mới, Xác nhận mật khẩu mới.
  - Hệ thống đối chiếu mật khẩu cũ với cột `MatKhau` trong database qua `HoSoRepository.DoiMatKhau`. Nếu trùng khớp và mật khẩu mới đạt độ dài tối thiểu 6 ký tự $\rightarrow$ Cập nhật `MatKhau` mới vào CSDL.
- **Lưu cấu hình cài đặt ứng dụng bền vững (Settings Persistence):**
  - Cung cấp các tùy chọn cấu hình: Nhận thông báo qua Email, Ghi nhớ đăng nhập trên thiết bị, Tự động kiểm tra bản cập nhật tệp số, Luôn hiển thị hộp thoại xác nhận trước khi trừ ví.
  - Bấm **"Lưu cài đặt ứng dụng"** $\rightarrow$ Lưu vào bảng `CaiDatNguoiDung` theo cơ chế **UPSERT** (nếu chưa có thì INSERT, đã có thì UPDATE). Mỗi khi người dùng mở màn hình Hồ sơ, hệ thống tự động nạp trạng thái đã lưu từ CSDL.

### 4. Quy tắc nghiệp vụ (Business Rules) Nhóm 3:
- **BR3.1 (Nguyên tắc Hybrid Storage):** Tuyệt đối không lưu tệp nhị phân trực tiếp vào SQL Server để tránh phình to CSDL (database bloat). File lưu ở thư mục hệ thống `Storage/Products/{MaSanPham}/`, CSDL chỉ lưu đường dẫn tương đối và metadata.
- **BR3.2 (Bảo toàn khóa ngoại danh mục):** Nghiêm cấm xóa danh mục khi còn sản phẩm tham chiếu.
- **BR3.3 (Bảo toàn lịch sử sản phẩm):** Khi sản phẩm ngừng kinh doanh, phải sử dụng cơ chế Xóa mềm (`DangKinhDoanh = 0`), không được dùng `DELETE FROM SanPham`.

---

## NHÓM 4: PHÂN HỆ XÁC THỰC, QUẢN TRỊ NGƯỜI DÙNG & DASHBOARD (AUTH, USER ADMIN & DASHBOARD)
**Thành viên phụ trách:** Trần Quốc Thuận  
**Mã nguồn liên quan:**  
- Giao diện: `LoginControl.cs` (UI 1), `RegisterControl.cs` (UI 1), `UserManageControl.cs` (UI 12), `DashboardControl.cs` (UI 9).
- Nghiệp vụ & Dữ liệu: `AuthService.cs`, `NguoiDungService.cs`, `DashboardService.cs`, `AuthRepository.cs`, `NguoiDungRepository.cs`, `DashboardRepository.cs`.
- Bảng CSDL thao tác: `NguoiDung`, `DonHang`, `ChiTietDonHang`, `SanPham`.

### 1. Nghiệp vụ UI 1 - Đăng nhập & Đăng ký (`Auth`)
- **Đăng ký tài khoản mới (`RegisterControl` - CRUD Create):**
  - Form nhập: Username, Mật khẩu, Xác nhận mật khẩu, Họ tên, Email, Số điện thoại.
  - Kiểm tra hợp lệ dữ liệu: Username và Email không được để trống, kiểm tra định dạng email chuẩn, mật khẩu khớp nhau và dài tối thiểu 6 ký tự.
  - Kiểm tra trùng lặp trên CSDL: Nếu Username hoặc Email đã tồn tại $\rightarrow$ Báo lỗi cụ thể.
  - Mật khẩu được lưu trực tiếp dưới dạng văn bản thuần (plaintext) trong cột `MatKhau` (không dùng băm hash — được đơn giản hóa cho mục đích bài tập lớn).
  - Thực thi `INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, VaiTro, SoDuVi, TrangThai, NgayTao)` với giá trị mặc định: `VaiTro = 'KhachHang'`, `SoDuVi = 0.00`, `TrangThai = 1` (Hoạt động).
  - Thông báo đăng ký thành công và tự động chuyển về form Đăng nhập.
- **Đăng nhập hệ thống (`LoginControl`):**
  - Nhập Username và Mật khẩu.
  - Đối chiếu mật khẩu người dùng vừa nhập với cột `MatKhau` trong database.
  - **Kiểm tra trạng thái tài khoản:**
    - Nếu thông tin sai $\rightarrow$ Báo *"Tên đăng nhập hoặc mật khẩu không chính xác"*.
    - Nếu tài khoản có `TrangThai = 0` (Bị khóa) $\rightarrow$ Chặn đăng nhập và hiển thị thông báo: *"Tài khoản của bạn đã bị khóa bởi Quản trị viên do vi phạm chính sách! Vui lòng liên hệ hỗ trợ."*.
    - Nếu hợp lệ $\rightarrow$ Khởi tạo phiên làm việc tĩnh: `UserSession.Login(maNguoiDung, tenDangNhap, hoTen, email, vaiTro, soDuVi)`.
  - Sau khi đăng nhập thành công, gọi `mainForm.UpdateUserInterfaceSession()` tường minh: Thanh Sidebar kiểm tra quyền ngay lập tức. Nếu là `QuanTriVien` thì hiển thị nhóm menu Admin (Dashboard, Quản lý SP, Danh mục, Đơn hàng, User); nếu là `KhachHang` thì tự động ẩn các menu Admin.
- **Đăng xuất:**
  - Bấm nút "Đăng xuất" trên Header $\rightarrow$ Hộp thoại xác nhận $\rightarrow$ Gọi `UserSession.Logout()` $\rightarrow$ Xóa trắng phiên làm việc, hoàn nguyên giao diện về trạng thái Khách vãng lai và chuyển về Cửa hàng.

### 2. Nghiệp vụ UI 12 - Quản lý người dùng toàn hệ thống (`UserManageControl`)
- **Xem danh sách tài khoản (CRUD Read):**
  - DataGridView danh sách: Mã người dùng, Tên đăng nhập, Họ và tên, Email, Vai trò (`QuanTriVien` / `KhachHang`), Trạng thái (`1: Hoạt động` / `0: Bị khóa`), Ngày tạo tài khoản.
  - Thanh công cụ: Ô tìm kiếm theo Username hoặc Email; ComboBox lọc theo Vai trò (Tất cả, Quản trị viên, Khách hàng).
- **Khóa và Mở khóa tài khoản (CRUD Update):**
  - Chọn một người dùng trong bảng DataGridView.
  - **Nút "Khóa tài khoản" (`TrangThai = 0`):**
    - **Ràng buộc an toàn cốt lõi:** Hệ thống kiểm tra nếu tài khoản được chọn trùng với tài khoản đang đăng nhập (`user.MaNguoiDung == UserSession.CurrentUserId`) $\rightarrow$ Lập tức chặn lại và cảnh báo: *"Bạn không thể tự khóa tài khoản Quản trị viên đang đăng nhập của chính mình!"*.
    - Nếu là tài khoản khác: Thực thi `UPDATE NguoiDung SET TrangThai = 0 WHERE MaNguoiDung = @MaNguoiDung`. Tài khoản này sẽ bị chặn đăng nhập ngay ở phiên sau.
  - **Nút "Mở khóa tài khoản" (`TrangThai = 1`):**
    - Thực thi `UPDATE NguoiDung SET TrangThai = 1 WHERE MaNguoiDung = @MaNguoiDung`. Phục hồi quyền truy cập cho người dùng.

### 3. Nghiệp vụ UI 9 - Báo cáo chỉ số kinh doanh Dashboard (`DashboardControl`)
- **4 Thẻ chỉ số KPI cốt lõi (SQL Aggregations):**
  1. **Tổng doanh thu hệ thống:** Câu lệnh `SELECT ISNULL(SUM(TongThanhToan), 0) FROM DonHang WHERE TrangThaiDonHang = N'Hoàn thành'`.
  2. **Tổng số đơn hàng hoàn tất:** Câu lệnh `SELECT COUNT(1) FROM DonHang WHERE TrangThaiDonHang = N'Hoàn thành'`.
  3. **Tổng số khách hàng thành viên:** Câu lệnh `SELECT COUNT(1) FROM NguoiDung WHERE VaiTro = 'KhachHang'`.
  4. **Tổng sản phẩm đang mở bán:** Câu lệnh `SELECT COUNT(1) FROM SanPham WHERE DangKinhDoanh = 1`.
- **Danh sách theo dõi nhanh:**
  - Bảng Top 5 sản phẩm bán chạy nhất: Truy vấn kết hợp `SUM(ChiTietDonHang.SoLuong)` sắp xếp giảm dần.
  - Bảng 5 đơn hàng giao dịch mới nhất trên toàn sàn.
  - Nút **"Làm mới dữ liệu thống kê"**: Thực thi lại toàn bộ các câu lệnh SQL tổng hợp để phản ánh số liệu doanh thu mới nhất tức thì.

### 4. Quy tắc nghiệp vụ (Business Rules) Nhóm 4:
- **BR4.1 (Xác thực đăng nhập & Đổi mật khẩu):** Mật khẩu được quản lý trực tiếp qua cột `MatKhau NVARCHAR(255)`, kiểm tra tính hợp lệ về độ dài (tối thiểu 6 ký tự) và khớp nối khi thay đổi.
- **BR4.2 (Phân quyền chuẩn RBAC):** Chỉ tài khoản có `VaiTro = 'QuanTriVien'` mới được phép truy cập UI 9, UI 10, UI 11, UI 12, UI 13.
- **BR4.3 (Chống tự khóa tài khoản):** Quản trị viên tuyệt đối không thể tự khóa tài khoản của chính mình.

---

## NHÓM 5: PHÂN HỆ QUẢN LÝ ĐƠN HÀNG, LỊCH SỬ MUA & KHO THƯ VIỆN SỐ (ORDERS, HISTORY & LIBRARY)
**Thành viên phụ trách:** Phạm Văn Đam  
**Mã nguồn liên quan:**  
- Giao diện: `OrderManageControl.cs` (UI 13), `OrderHistoryControl.cs` (UI 7), `LibraryControl.cs` (UI 6).
- Nghiệp vụ & Dữ liệu: `DonHangService.cs`, `ThuVienService.cs`, `DonHangRepository.cs`, `ThuVienRepository.cs`.
- Bảng CSDL thao tác: `DonHang`, `ChiTietDonHang`, `SoHuuSanPham`, `TepSanPham`, `SanPham`.

### 1. Nghiệp vụ UI 13 - Quản trị đơn hàng toàn sàn (`OrderManageControl`)
- **Hiển thị danh sách đơn hàng toàn hệ thống (CRUD Read):**
  - DataGridView danh sách: Mã đơn hiển thị (`MaDonHangCode`), Tên khách hàng, Tạm tính, Tiền giảm giá, Tổng thanh toán, Trạng thái đơn hàng (`Hoàn thành`, `Chờ xử lý`, `Đã hủy`), Ghi chú, Ngày tạo đơn.
  - Bộ lọc trạng thái đơn hàng (Tất cả, Hoàn thành, Chờ xử lý, Đã hủy) và thanh tìm kiếm theo mã hóa đơn hoặc tên khách hàng.
- **Xem chi tiết mặt hàng trong đơn:**
  - Khi click chọn một dòng đơn hàng, DataGridView con bên cạnh lập tức hiển thị danh sách các sản phẩm thuộc về đơn đó: Tên sản phẩm, Đơn giá mua, Số lượng, Thành tiền từ bảng `ChiTietDonHang`.
- **Cơ chế cấp quyền tức thì (Instant Fulfillment) & Duyệt đơn dự phòng (CRUD Update):**
  - **Luồng mua tự động qua ví:** Khách mua hàng online qua ví sẽ được tự động chuyển sang trạng thái `Hoàn thành` và tự động cấp game vào Thư viện số (`SoHuuSanPham`) trong cùng 1 transaction.
  - **Nút "Kích hoạt thủ công (Cấp Thư viện)":** Đóng vai trò là công cụ can thiệp dự phòng (Manual Override / Fallback). Dùng cho các trường hợp: khách thanh toán chuyển khoản ngoài cần Admin duyệt, tra soát khiếu nại hoặc xử lý sự cố mạng. Khi Admin bấm nút:
    - Cập nhật trạng thái đơn: `UPDATE DonHang SET TrangThaiDonHang = N'Hoàn thành'`.
    - Tự động quét các sản phẩm trong đơn và thực thi `INSERT INTO SoHuuSanPham` để cấp quyền cho khách.
    - **Cơ chế bảo vệ giao diện (Contextual Disabled State):** Nếu đơn hàng vốn dĩ đã ở trạng thái `Hoàn thành`, nút này tự động áp dụng trạng thái vô hiệu hóa có ngữ cảnh: làm mờ màu sắc, đặt con trỏ `Cursors.No` và hiển thị ToolTip thông báo lý do không thể duyệt lại.
  - **Nút "Hủy đơn hàng":** Chuyển trạng thái đơn sang `N'Đã hủy'`. Dùng cho nghiệp vụ hoàn tiền (Refund) hoặc hủy đơn gian lận.

### 2. Nghiệp vụ UI 7 - Lịch sử đơn hàng cá nhân (`OrderHistoryControl`)
- **Tra cứu lịch sử mua hàng cá nhân (CRUD Read):**
  - Tận dụng chung cấu trúc bảng `DonHang` nhưng bổ sung điều kiện lọc `WHERE MaNguoiDung = @CurrentUserId`.
  - Hiển thị danh sách hóa đơn cá nhân: Mã đơn hàng, Ngày mua sắm, Tiền chiết khấu voucher, Tổng tiền thanh toán ví, Trạng thái đơn.
  - Click chọn dòng hóa đơn $\rightarrow$ Hiển thị chi tiết từng món hàng đã mua, hỗ trợ khách hàng kiểm tra lại chi tiêu và minh chứng sở hữu.

### 3. Nghiệp vụ UI 6 - Thư viện sản phẩm đã sở hữu (`LibraryControl`)
- **Hiển thị kho sản phẩm sở hữu vĩnh viễn (CRUD Read):**
  - Lấy dữ liệu từ bảng `SoHuuSanPham` kết hợp bảng `SanPham` và `TepSanPham` theo `MaNguoiDung = @CurrentUserId`.
  - Hiển thị dạng danh sách / lưới thẻ: Ảnh bìa, Tên sản phẩm, Ngày sở hữu, Dung lượng tệp (MB), Phiên bản hiện hành, Số lần đã tải (`SoLanTai`).
  - Ô tìm kiếm nhanh game trong thư viện theo tên.
- **Tải xuống sản phẩm số mô phỏng (Download File Flow):**
  - Nút **"Tải về máy" (Download)**:
    1. Khi người dùng bấm nút, mở hộp thoại `FolderBrowserDialog` cho người dùng chọn thư mục lưu file trên máy tính.
    2. Hiển thị thanh tiến trình `ProgressBar` mô phỏng/sao chép tệp từ thư mục `Storage/Products/{MaSanPham}/` sang thư mục người dùng đã chọn, tiến trình tăng dần từ 0% đến 100%.
    3. **Đóng gói PK-Zip & Đính kèm chứng chỉ sở hữu:** Hệ thống tự động nén đóng gói tệp cài đặt vào kho nén `.zip`, sinh kèm chứng chỉ bản quyền độc bản `LICENSE_AND_KEY.txt` (ghi nhận Mã đơn hàng, Tên sản phẩm, Phiên bản, Thời điểm cấp quyền).
    4. Cập nhật số lần tải: Thực thi `UPDATE SoHuuSanPham SET SoLanTai = SoLanTai + 1, LanTaiCuoi = GETDATE()`.
    5. Hiển thị thông báo hoàn tất: *"Tải tệp thành công về thư mục đã chọn!"*.
- **Quản lý danh sách thư viện (CRUD Delete):**
  - Nút **"Gỡ khỏi thư viện"**: Cho phép người dùng dọn dẹp danh sách hiển thị bằng cách xóa bản ghi khỏi bảng `SoHuuSanPham`.

### 4. Quy tắc nghiệp vụ (Business Rules) Nhóm 5:
- **BR5.1 (Quyền sở hữu vĩnh viễn):** Khách hàng chỉ có thể tải tệp của các sản phẩm có bản ghi trong bảng `SoHuuSanPham` thuộc quyền sở hữu của chính mình.
- **BR5.2 (Bảo toàn lịch sử hóa đơn):** Đơn hàng và chi tiết đơn hàng là chứng từ kế toán, tuyệt đối không được xóa cứng (No Hard Delete). Khi hủy đơn chỉ cập nhật trạng thái `TrangThaiDonHang = N'Đã hủy'`.
- **BR5.3 (Đóng gói nén độc bản):** Mọi tệp tải về từ Thư viện đều được nén chuẩn PK-Zip và tích hợp chứng chỉ số `LICENSE_AND_KEY.txt` chứng minh bản quyền sở hữu hợp pháp.
