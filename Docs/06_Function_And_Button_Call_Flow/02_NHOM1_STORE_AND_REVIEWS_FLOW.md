# ĐẶC TẢ LUỒNG NÚT BẤM & HÀM XỬ LÝ - NHÓM 1: CỬA HÀNG & ĐÁNH GIÁ
**Thành viên phụ trách:** Lê Nguyễn Minh Trí  
**Màn hình quản lý:**
1. UI 2: `StoreViewControl` (Mặt tiền Cửa hàng số) & `ProductCardControl` (Thẻ sản phẩm Steam)
2. UI 3: `ProductDetailControl` (Chi tiết cấu hình sản phẩm & Mô tả)
3. UI 14: `ProductReviewControl` (Cộng đồng Đánh giá & Nhận xét 1-5★)

---

## 1. MÀN HÌNH UI 2: `StoreViewControl` & `ProductCardControl`

### 1.1. Sơ đồ luồng bấm nút Thêm vào giỏ trên Thẻ Game

```text
[ Người dùng bấm nút "🛒 Mua ngay" trên ProductCardControl ]
                          |
                          v
         ProductCardControl.BtnAddToCart_Click()
                          |
                          | Kích hoạt Event Action: OnAddToCartClicked?.Invoke(MaSanPham)
                          v
         StoreViewControl (Event Subscriber)
                          |
                          | Kiểm tra UserSession.IsLoggedIn
                          v
        [ Chưa đăng nhập? ] ---- YES ----> [ Hiện thông báo yêu cầu đăng nhập ]
                 | NO
                 v
         GioHangService.ThemVaoGioHang(userId, spId, 1, out msg)
                          |
                          | 1. Kiểm tra ThuVienRepository.DaSoHuu(userId, spId)
                          |    -> Nếu đã sở hữu: Báo lỗi không cho mua đè!
                          | 2. Gọi GioHangRepository.ThemVaoGioHang(userId, spId, 1)
                          v
         Thực thi SQL: MERGE / INSERT vào bảng GioHang
                          |
                          v
         Bắn sự kiện: GioHangService.NotifyCartUpdated()
                          |
                          v
         MainForm.UpdateCartBadge() cập nhật badge số lượng tức thì trên Header
```

---

### 1.2. Chi tiết từng nút bấm & tương tác trên UI 2

#### 1. Nút `btnSearch` ("Tìm kiếm")
* **Hàm UI bắt sự kiện:** `BtnSearch_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):**
  * Người dùng bấm nút "Tìm kiếm" trên thanh công cụ Store.
  * Người dùng nhấn phím **Enter** trong ô nhập từ khóa (`TxtSearch_KeyDown`).
* **Hàm thực thi tiếp theo:** `LoadProducts()`
* **Dữ liệu trích xuất:**
  * Từ khóa: `txtSearch.Text.Trim()`.
  * Danh mục lọc: `(int)cboCategory.SelectedValue`.
* **Hàm BLL được gọi:** `SanPhamService.LayDanhSachSanPham(string? tuKhoa, int maDanhMuc)`
* **Hàm DAL được gọi:** `SanPhamRepository.LayDanhSachSanPham(tuKhoa, maDanhMuc)`
* **Câu lệnh SQL thực thi:**
  ```sql
  SELECT sp.MaSanPham, sp.TenSanPham, sp.GiaBan, sp.HinhAnh, sp.DungLuongMB, sp.PhienBan,
         dm.TenDanhMuc, ISNULL(AVG(dg.SoSao * 1.0), 0) AS DiemDanhGiaTrungBinh, COUNT(dg.MaDanhGia) AS SoLuongDanhGia
  FROM SanPham sp
  INNER JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
  LEFT JOIN DanhGia dg ON sp.MaSanPham = dg.MaSanPham
  WHERE sp.DangKinhDoanh = 1
    AND (@MaDanhMuc = 0 OR sp.MaDanhMuc = @MaDanhMuc)
    AND (@TuKhoa = '' OR sp.TenSanPham LIKE @TuKhoa OR dm.TenDanhMuc LIKE @TuKhoa)
  GROUP BY sp.MaSanPham, sp.TenSanPham, sp.GiaBan, sp.HinhAnh, sp.DungLuongMB, sp.PhienBan, dm.TenDanhMuc;
  ```
* **Phản hồi UI:** Xóa sạch các thẻ cũ trên `flowProducts`, tạo mới danh sách `ProductCardControl` tương ứng với kết quả trả về. Nếu kết quả = 0, hiển thị nhãn thông báo không tìm thấy sản phẩm.

#### 2. Nút `btnRefresh` ("Làm mới")
* **Hàm UI bắt sự kiện:** `BtnRefresh_Click(object? sender, EventArgs e)`
* **Hành vi:** Xóa trắng `txtSearch.Text = ""`, chọn lại `cboCategory.SelectedIndex = 0`, sau đó gọi lại `LoadProducts()`.

#### 3. Nút `btnDetail` trên `ProductCardControl` ("Chi tiết")
* **Hàm UI bắt sự kiện:** `BtnDetail_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Người dùng bấm vào nút "Chi tiết" hoặc bấm trực tiếp vào ảnh đại diện của game.
* **Hành vi phát sự kiện:** Kích hoạt `OnDetailClicked?.Invoke(MaSanPham)`.
* **Hàm xử lý tại MainForm:** `MainForm.NavigateToProductDetail(id)` $\to$ nạp màn hình UI 3.

---

## 2. MÀN HÌNH UI 3: `ProductDetailControl`

#### 4. Nút `btnAddToCart` ("Thêm vào giỏ hàng")
* **Hàm UI bắt sự kiện:** `BtnAddToCart_Click(object? sender, EventArgs e)`
* **Hàm BLL được gọi:** `GioHangService.ThemVaoGioHang(UserSession.CurrentUserId, _maSanPham, 1, out msg)`
* **Hàm DAL được gọi:** `ThuVienRepository.DaSoHuu` và `GioHangRepository.ThemVaoGioHang`
* **Quy tắc chặn:**
  * Nếu chưa đăng nhập $\to$ Báo lỗi *"Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng!"*.
  * Nếu đã có trong thư viện $\to$ Báo lỗi *"Bạn đã sở hữu sản phẩm này trong Thư viện rồi! Không cần mua lại."*.
* **Sự kiện bắn ra:** `GioHangService.NotifyCartUpdated()`.

#### 5. Nút `btnBack` ("← Quay lại cửa hàng")
* **Hàm UI bắt sự kiện:** `BtnBack_Click(object? sender, EventArgs e)`
* **Hành vi phát sự kiện:** `OnBackToStore?.Invoke()`.
* **Nơi nhận tại MainForm:** Kích hoạt `MainForm.NavigateToStore()`.

#### 6. Nút `btnReviews` / `btnWriteReview` ("Xem đánh giá & Viết nhận xét")
* **Hàm UI bắt sự kiện:** `BtnReviews_Click(object? sender, EventArgs e)`
* **Hành vi phát sự kiện:** `OnViewReviews?.Invoke(_maSanPham)`.
* **Nơi nhận tại MainForm:** Kích hoạt `MainForm.NavigateToReviews(_maSanPham)`.

---

## 3. MÀN HÌNH UI 14: `ProductReviewControl`

### 3.1. Sơ đồ luồng gửi đánh giá & tự động tính lại điểm sao

```text
[ Người dùng bấm nút "Gửi nhận xét" / "Cập nhật đánh giá" ]
                          |
                          v
         ProductReviewControl.BtnSubmit_Click()
                          |
                          | Kiểm tra UserSession.IsLoggedIn
                          | Kiểm tra comment.Length (1 - 2000 ký tự)
                          v
         DanhGiaService.LuuDanhGia(userId, spId, star, comment, out msg)
                          |
                          | 1. Kiểm tra quyền sở hữu bản quyền:
                          |    ThuVienRepository.DaSoHuu(userId, spId)
                          |    -> Nếu CHƯA MUA: Báo lỗi "Chỉ người đã mua mới được đánh giá!"
                          v
         DanhGiaRepository.LuuDanhGia(dto)
                          |
                          | Thực thi SQL UPSERT vào bảng DanhGia:
                          | IF EXISTS -> UPDATE SoSao, BinhLuan, NgayCapNhat
                          | ELSE      -> INSERT (MaNguoiDung, MaSanPham, SoSao, BinhLuan)
                          v
         DanhGiaRepository.CapNhatDiemTrungBinhSanPham(spId)
                          |
                          | UPDATE SanPham SET DiemTrungBinh = (SELECT AVG(CAST(SoSao AS DECIMAL(3,2))))
                          v
         Bắn sự kiện: SanPhamService.NotifyReviewUpdated(spId)
                          |
                          v
         StoreViewControl & ProductDetailControl tự động cập nhật số sao mới trên giao diện
```

---

### 3.2. Chi tiết từng nút bấm trên UI 14

#### 7. Nút `btnSubmit` ("Gửi nhận xét" / "Cập nhật đánh giá")
* **Hàm UI bắt sự kiện:** `BtnSubmit_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Khách hàng nhấn nút sau khi chọn số sao (1-5) và nhập nội dung nhận xét.
* **Xác thực hợp lệ (UI Validation):**
  * `UserSession.IsLoggedIn == true`.
  * `comment.Length > 0 && comment.Length <= 2000`.
* **Hàm BLL được gọi:** `DanhGiaService.LuuDanhGia(int maNguoiDung, int maSanPham, int soSao, string binhLuan, out string thongBao)`
* **Hàm DAL được gọi:**
  1. `ThuVienRepository.DaSoHuu(maNguoiDung, maSanPham)` $\to$ Phải trả về `true` (Quy tắc BR1-3: Verified Purchase).
  2. `DanhGiaRepository.LuuDanhGia(DanhGiaDTO dto)`.
  3. `DanhGiaRepository.CapNhatDiemTrungBinh(maSanPham)`.
* **Câu lệnh SQL thực thi:**
  ```sql
  -- 1. Lưu nhận xét (UPSERT)
  IF EXISTS (SELECT 1 FROM DanhGia WHERE MaNguoiDung = @MaNguoiDung AND MaSanPham = @MaSanPham)
  BEGIN
      UPDATE DanhGia 
      SET SoSao = @SoSao, BinhLuan = @BinhLuan, NgayCapNhat = GETDATE()
      WHERE MaNguoiDung = @MaNguoiDung AND MaSanPham = @MaSanPham;
  END
  ELSE
  BEGIN
      INSERT INTO DanhGia (MaNguoiDung, MaSanPham, SoSao, BinhLuan, NgayTao)
      VALUES (@MaNguoiDung, @MaSanPham, @SoSao, @BinhLuan, GETDATE());
  END;

  -- 2. Tái tính toán điểm trung bình
  UPDATE SanPham
  SET DiemTrungBinh = (
      SELECT ISNULL(ROUND(AVG(CAST(SoSao AS DECIMAL(3,2))), 1), 0)
      FROM DanhGia WHERE MaSanPham = @MaSanPham
  )
  WHERE MaSanPham = @MaSanPham;
  ```
* **Sự kiện bắn ra:** `SanPhamService.NotifyReviewUpdated(maSanPham)`.
* **Phản hồi UI:** Tải lại lưới danh sách đánh giá `LoadReviews()`.

#### 8. Nút `btnDelete` ("Xóa đánh giá của tôi")
* **Hàm UI bắt sự kiện:** `BtnDelete_Click(object? sender, EventArgs e)`
* **Nơi gọi (Caller):** Người dùng muốn thu hồi nhận xét của mình.
* **Hàm BLL được gọi:** `DanhGiaService.XoaDanhGia(int maDanhGia, int maNguoiDung, out string msg)`
* **Hàm DAL được gọi:** `DanhGiaRepository.XoaDanhGia(maDanhGia, maNguoiDung)`
* **Câu lệnh SQL thực thi:**
  ```sql
  DELETE FROM DanhGia WHERE MaDanhGia = @MaDanhGia AND MaNguoiDung = @MaNguoiDung;
  ```
* **Kích hoạt tiếp theo:** Tự động tính toán lại điểm sao trung bình mới của sản phẩm sau khi nhận xét bị xóa.
