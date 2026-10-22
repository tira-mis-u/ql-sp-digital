# PRD: PHÂN HỆ CỬA HÀNG, CHI TIẾT SẢN PHẨM & ĐÁNH GIÁ (STORE, PRODUCT DETAILS & REVIEWS)
**Nhiệm vụ:** STT 1  
**Thành viên phụ trách:** Lê Nguyễn Minh Trí (Nhóm 1)  
**Màn hình phụ trách:**  
- **UI 2:** Cửa hàng / Trang chủ ([`../../qlspdigital/qlspdigital/Views/Nhom1_Store/StoreViewControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom1_Store/StoreViewControl.cs))
- **UI 3:** Chi tiết sản phẩm ([`../../qlspdigital/qlspdigital/Views/Nhom1_Store/ProductDetailControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom1_Store/ProductDetailControl.cs))
- **UI 14:** Quản lý Đánh giá & Nhận xét sản phẩm ([`../../qlspdigital/qlspdigital/Views/Nhom1_Store/ProductReviewControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom1_Store/ProductReviewControl.cs))
- **Component dùng chung:** Thẻ sản phẩm Card ([`../../qlspdigital/qlspdigital/Controls/ProductCardControl.cs`](../../qlspdigital/qlspdigital/Controls/ProductCardControl.cs))
**Mã nguồn nghiệp vụ & truy vấn:**
- BLL Service: [`../../qlspdigital/qlspdigital/BLL/Nhom1_Store/SanPhamService.cs`](../../qlspdigital/qlspdigital/BLL/Nhom1_Store/SanPhamService.cs)
- DAL Repository: [`../../qlspdigital/qlspdigital/DAL/Nhom1_Store/SanPhamRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom1_Store/SanPhamRepository.cs), [`../../qlspdigital/qlspdigital/DAL/Nhom1_Store/DanhGiaRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom1_Store/DanhGiaRepository.cs)

---

## 1. Mục tiêu phân hệ
Cung cấp cho khách hàng trải nghiệm duyệt danh mục, tìm kiếm sản phẩm kỹ thuật số nhanh chóng và xem thông tin chi tiết đầy đủ (mô tả, giá tiền, hình ảnh minh họa, thông số tệp). Đồng thời hỗ trợ tính năng **Đánh giá & Nhận xét cộng đồng (Reviews & Ratings)** theo chuẩn Steam: cho phép người dùng chấm điểm sao, viết bài nhận xét, chỉnh sửa/xóa đánh giá của mình và theo dõi mức độ hài lòng của cộng đồng đối với từng sản phẩm số.

---

## 2. Người dùng mục tiêu & Vai trò
- **Khách / Đã đăng nhập:** Duyệt danh sách sản phẩm, lọc theo thể loại, tìm kiếm theo tên, xem trang chi tiết và đọc các đánh giá của cộng đồng.
- **Khách hàng đã đăng nhập:**
  - Viết đánh giá mới (chấm điểm từ 1 đến 5 sao và viết nhận xét chi tiết) cho các sản phẩm đã sở hữu.
  - Quản lý các bài đánh giá của chính mình: xem lại, chỉnh sửa nội dung, đổi số sao, hoặc xóa đánh giá.
  - Thêm sản phẩm vào giỏ hàng hoặc bấm mua ngay từ trang chi tiết.

---

## 3. Đặc tả yêu cầu chức năng (Functional Requirements)

### 3.1. UI 2 - Cửa hàng / Trang chủ (`StoreViewControl`)
1. **Hiển thị danh sách sản phẩm:**
   - Dạng thẻ lưới (Card Layout) hiển thị: Ảnh bìa, Tên sản phẩm, Danh mục, Giá tiền (VNĐ), Điểm đánh giá sao trung bình (⭐ 4.5/5).
   - Nếu sản phẩm miễn phí hiển thị nhãn "Miễn phí".
2. **Tìm kiếm & Bộ lọc (CRUD Read 1):**
   - Ô tìm kiếm theo từ khóa (Tên sản phẩm): Tìm kiếm tức thì khi nhấn nút "Tìm kiếm" hoặc bấm Enter.
   - Hộp chọn ComboBox "Thể loại": Tất cả, Games, Phần mềm, Đồ họa 3D, Âm thanh...
   - Sắp xếp (Sort): Theo giá tăng dần, giá giảm dần, mới nhất, hoặc theo đánh giá cao nhất.
3. **Điều hướng:**
   - Click đúp hoặc click nút "Xem chi tiết" trên thẻ -> Chuyển sang màn hình UI 3 (`ProductDetailControl`).
   - Nút liên kết nhanh trên menu: *"Trung tâm Đánh giá & Phản hồi"* -> Chuyển sang UI 14 (`ProductReviewControl`).

### 3.2. UI 3 - Chi tiết sản phẩm (`ProductDetailControl`)
1. **Thông tin hiển thị:**
   - Tiêu đề sản phẩm, nhà phát hành / người tạo, ngày phát hành, phiên bản, dung lượng tệp.
   - Ảnh đại diện lớn (Banner / Cover Image).
   - Giá bán chính thức và trạng thái còn kinh doanh (`DangKinhDoanh = 1`).
   - Mô tả chi tiết sản phẩm (Mô tả tính năng, cấu hình yêu cầu, định dạng tệp số).
   - **Tóm tắt đánh giá cộng đồng:** Điểm trung bình (VD: `⭐ 4.8 / 5.0 (150 đánh giá)` - Mức độ: *"Rất tích cực"*).
2. **Hành động mua sắm & Đánh giá:**
   - Nút **"Thêm vào giỏ hàng"**: Thêm sản phẩm này vào giỏ hàng của tài khoản đang đăng nhập.
   - Nút **"Mua ngay"**: Thêm vào giỏ và tự động chuyển ngay sang màn hình Giỏ hàng / Thanh toán.
   - Nút **"⭐ Viết đánh giá sản phẩm"**: Chuyển nhanh sang UI 14 để chấm điểm và viết nhận xét.
   - Nút **"Quay lại"**: Trở về danh sách Cửa hàng trước đó.

### 3.3. UI 14 - Quản lý Đánh giá & Nhận xét sản phẩm (`ProductReviewControl` - Bổ sung cho Nhóm 1)
1. **Hiển thị danh sách đánh giá (CRUD Read 3 cho BTL):**
   - ComboBox chọn sản phẩm muốn xem đánh giá (hoặc mặc định xem đánh giá của sản phẩm vừa xem).
   - Bảng tóm tắt: Điểm số trung bình (`AVG(SoSao)`), tổng số lượt đánh giá (`COUNT`), thanh phần trăm tỉ lệ sao (5 sao, 4 sao, 3 sao...).
   - Danh sách các bài nhận xét (DataGridView hoặc Panel): Tên người đánh giá, số sao chấm (⭐ 1-5), nội dung lời bình, ngày đăng bài.
2. **Viết đánh giá mới (CRUD Create 3):**
   - Form nhập đánh giá:
     + Chọn mức đánh giá: 1 ⭐ (Rất tệ), 2 ⭐ (Tệ), 3 ⭐ (Bình thường), 4 ⭐ (Tốt), 5 ⭐ (Tuyệt vời).
     + Ô nhập nội dung nhận xét chi tiết (`txtReviewComment`, MultiLine).
     + Nút **[ ✍️ Gửi Đánh Giá ]**: Kiểm tra điều kiện (mỗi tài khoản chỉ đánh giá 1 lần cho mỗi sản phẩm) -> Thực thi câu lệnh `INSERT INTO DanhGia`.
     + Thông báo: *"Đánh giá của bạn đã được đăng tải thành công!"*.
3. **Chỉnh sửa & Xóa bài đánh giá của chính mình (CRUD Update & Delete 3):**
   - Nhận diện bài đánh giá của tài khoản đang đăng nhập (`UserSession.CurrentUserId`).
   - Nút **[ ✏️ Sửa nhận xét ]**: Đổ lại nội dung và số sao cũ vào form để người dùng chỉnh sửa -> Bấm **[ Lưu thay đổi ]** -> Thực thi `UPDATE DanhGia SET SoSao = @SoSao, BinhLuan = @BinhLuan, NgayCapNhat = GETDATE()`.
   - Nút **[ 🗑️ Xóa đánh giá ]**: Thực thi `DELETE FROM DanhGia WHERE MaNguoiDung = @MaNguoiDung AND MaSanPham = @MaSanPham`.
   - Cập nhật lại ngay lập tức điểm số trung bình và danh sách hiển thị.

---

## 4. Quy tắc nghiệp vụ (Business Rules)
1. **Hiển thị sản phẩm hợp lệ:** Chỉ hiển thị các sản phẩm có trạng thái `DangKinhDoanh = 1`.
2. **Quy tắc đánh giá (Unique Review):** Mỗi tài khoản người dùng chỉ được phép đánh giá tối đa 1 lần cho mỗi sản phẩm (`UNIQUE(MaNguoiDung, MaSanPham)`). Muốn đổi ý kiến thì dùng tính năng Sửa đánh giá.
3. **Ràng buộc điểm sao:** Điểm đánh giá bắt buộc phải là số nguyên từ 1 đến 5 (`SoSao BETWEEN 1 AND 5`).
4. **Bảo toàn dữ liệu:** Khi xóa bài đánh giá, điểm trung bình của sản phẩm phải được tính toán lại ngay lập tức.
5. **Quy tắc Người mua xác thực (Verified Purchase):** Chỉ người dùng đã thực sự sở hữu sản phẩm trong Thư viện (`DaMuaSanPham` / `ThuVienRepository.DaSoHuu`) mới được phép đăng hoặc sửa bài đánh giá, đảm bảo tính khách quan và uy tín của nền tảng.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Store Read/Filter):** Lọc theo danh mục và tìm kiếm theo chuỗi ký tự thành công trên database.
- [x] **CRUD 2 (Product Details):** Hiển thị chi tiết cấu hình, mô tả và điểm số đánh giá trung bình.
- [x] **CRUD 3 (Reviews Management - UI 14):** Thực hiện đầy đủ 4 thao tác CRUD trên bảng `DanhGia`: Đọc danh sách đánh giá (Read), Viết đánh giá mới (Create), Chỉnh sửa nhận xét/số sao (Update), và Xóa bài nhận xét (Delete).
- [x] Chuyển đổi mượt mà giữa Store, Product Details và Reviews trong khung sườn `MainForm`.

---

## 6. Ma trận Đặc tả Nghiệp vụ CRUD Chuyên sâu (CRUD Matrix & Defense Rationale)

| Thao tác | Tầng Giao diện (`Views/Nhom1_Store`) | Tầng Nghiệp vụ (`BLL/Nhom1_Store`) | Tầng Truy vấn (`DAL/Nhom1_Store`) | Bản chất Lệnh SQL |
|---|---|---|---|---|
| **CREATE** | Nút `btnSubmit` ("Gửi nhận xét") trên `ProductReviewControl` | `DanhGiaService.LuuDanhGia(...)` | `DanhGiaRepository.ThemDanhGia(...)` | `INSERT INTO DanhGia (MaNguoiDung, MaSanPham, SoSao, BinhLuan, NgayTao) VALUES (...)` |
| **READ** | `dgvReviews` & Điểm sao trung bình trên `ProductDetailControl` | `DanhGiaService.LayDanhGiaTheoSanPham()`<br>`LayDanhGiaCuaUser()` | `DanhGiaRepository.LayDanhGiaTheoSanPham()`<br>`LayDanhGiaCuaUser()` | `SELECT dg.*, nd.HoTen, sp.TenSanPham FROM DanhGia dg INNER JOIN NguoiDung nd ... WHERE dg.MaSanPham = @Id` |
| **UPDATE** | Tự động tải đánh giá cũ, đổi nhãn `btnSubmit` thành "Cập nhật đánh giá" | `DanhGiaService.LuuDanhGia(...)` (rẽ nhánh update) | `DanhGiaRepository.CapNhatDanhGia(...)` | `UPDATE DanhGia SET SoSao = @SoSao, BinhLuan = @BinhLuan, NgayCapNhat = GETDATE() WHERE MaDanhGia = @Id` |
| **DELETE** | Nút `btnDelete` ("Xóa đánh giá") kèm popup xác nhận Yes/No | `DanhGiaService.XoaDanhGia(...)` | `DanhGiaRepository.XoaDanhGia(...)` | `DELETE FROM DanhGia WHERE MaDanhGia = @MaDanhGia AND MaNguoiDung = @UserId` |

> **Phòng thủ vấn đáp BTL:** Khẳng định với Hội đồng rằng phân hệ của Lê Nguyễn Minh Trí đạt **FULL CRUD 100%** trên thực thể `DanhGia`. Đặc biệt có cơ chế bảo vệ **Verified Purchase** (`DaMuaSanPham()`), ngăn chặn người dùng chưa mua game gửi đánh giá bừa bãi.

