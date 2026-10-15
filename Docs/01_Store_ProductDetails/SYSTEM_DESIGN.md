# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 1
**Phân hệ:** Cửa hàng, Chi tiết sản phẩm & Đánh giá (Store, Product Details & Reviews)  
**Màn hình phụ trách:** UI 2 (`StoreViewControl`), UI 3 (`ProductDetailControl`), UI 14 (`ProductReviewControl`)  
**Thành viên phụ trách:** [Chưa phân công / Điền sau]  
**Kiến trúc áp dụng:** 3 tầng (Presentation - Business Logic - Data Access - SQL Server)

---

## 1. Kiến trúc phân tầng & Luồng tương tác

```text
+----------------------------------------------------------------------------------------------------+
|                                         PRESENTATION LAYER                                         |
|  - StoreViewControl.cs (Duyệt thẻ, tìm kiếm, lọc)     - ProductDetailControl.cs (Xem chi tiết SP)  |
|  - ProductReviewControl.cs (Viết & Quản lý nhận xét)  - ProductCardControl.cs (Thẻ sản phẩm con)   |
+--------------------------------------------------+-------------------------------------------------+
                                                   | DTOs (SanPhamDTO, DanhMucDTO, DanhGiaDTO)
+--------------------------------------------------v-------------------------------------------------+
|                                        BUSINESS LOGIC (BLL)                                        |
|  - ISanPhamService / SanPhamService.cs           - IDanhGiaService / DanhGiaService.cs             |
|  - Logic tính điểm sao trung bình AVG, kiểm tra điều kiện đánh giá (1 user / 1 review / 1 SP)      |
+--------------------------------------------------+-------------------------------------------------+
                                                   | Entities / Parameters
+--------------------------------------------------v-------------------------------------------------+
|                                       DATA ACCESS LAYER (DAL)                                      |
|  - ISanPhamRepository / SanPhamRepository.cs     - IDanhGiaRepository / DanhGiaRepository.cs       |
|  - ADO.NET: SqlConnection, SqlCommand, SqlDataReader, DbConnectionFactory                          |
+--------------------------------------------------+-------------------------------------------------+
                                                   | T-SQL Statements
+--------------------------------------------------v-------------------------------------------------+
|                                        MICROSOFT SQL SERVER                                        |
|  - Bảng sử dụng chính: SanPham, DanhMuc, NhaPhatHanh, DanhGia, NguoiDung                          |
+----------------------------------------------------------------------------------------------------+
```

---

## 2. Thiết kế Mô hình CSDL & Quan hệ Bảng cho Nhóm 1

### 2.1. Bảng `SanPham` (Dùng trong UI 2 & UI 3)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaSanPham` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh duy nhất của sản phẩm số. |
| `TenSanPham` | `NVARCHAR(200)` | | `NOT NULL` | Tên hiển thị trên thẻ Store và trang Chi tiết. |
| `MoTa` | `NVARCHAR(MAX)` | | `NULL` | Bài viết giới thiệu tính năng, cấu hình trên UI 3. |
| `GiaBan` | `DECIMAL(18,2)` | | `NOT NULL, DEFAULT 0, CHECK >= 0` | Giá niêm yết chính thức (VNĐ). Hiển thị "Miễn phí" nếu = 0. |
| `HinhAnh` | `NVARCHAR(500)` | | `NULL` | Đường dẫn ảnh tương đối (`Assets/Images/...`) nạp vào PictureBox. |
| `DungLuongMB` | `DECIMAL(10,2)` | | `NOT NULL, DEFAULT 0` | Dung lượng tệp hiển thị cho khách biết cấu hình yêu cầu. |
| `PhienBan` | `NVARCHAR(50)` | | `NOT NULL, DEFAULT '1.0.0'` | Phiên bản phát hành hiển thị trên UI 3. |
| `MaDanhMuc` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `DanhMuc(MaDanhMuc)` | Thể loại sản phẩm (Game, Phần mềm, 3D...). |
| `MaNhaPhatHanh` | `INT` | **FK** | `NULL` $\rightarrow$ `NhaPhatHanh(MaNhaPhatHanh)` | Nhà sáng tạo / Studio phát hành sản phẩm. |
| `DangKinhDoanh` | `BIT` | | `NOT NULL, DEFAULT 1` | Bộ lọc: Store chỉ hiển thị các sản phẩm có `DangKinhDoanh = 1`. |
| `NgayTao` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Dùng để sắp xếp sản phẩm "Mới nhất" trên UI 2. |

### 2.2. Bảng `DanhMuc` (Dùng trong UI 2 ComboBox Filter)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaDanhMuc` | `INT` | **PK** | `IDENTITY(1,1)` | Mã phân loại thể loại hàng hóa. |
| `TenDanhMuc` | `NVARCHAR(100)` | | `NOT NULL, UNIQUE` | Tên thể loại nạp vào ComboBox danh mục trên UI 2. |
| `MoTa` | `NVARCHAR(255)` | | `NULL` | Ghi chú diễn giải danh mục. |
| `TrangThai` | `BIT` | | `NOT NULL, DEFAULT 1` | Trạng thái hiển thị danh mục. |

### 2.3. Bảng `NhaPhatHanh` (Dùng hiển thị trên UI 3)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaNhaPhatHanh` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh nhà phát hành. |
| `TenNhaPhatHanh` | `NVARCHAR(100)` | | `NOT NULL, UNIQUE` | Tên Studio phát hành hiển thị trong UI 3. |
| `QuocGia` | `NVARCHAR(50)` | | `NULL` | Quốc gia xuất xứ của Studio. |
| `Website` | `NVARCHAR(255)` | | `NULL` | Trang chủ của nhà phát hành. |

### 2.4. Bảng `DanhGia` (Trọng tâm CRUD UI 14 & Tóm tắt sao UI 3)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaDanhGia` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh bài đánh giá. |
| `MaNguoiDung` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `NguoiDung(MaNguoiDung)` | Tài khoản người dùng viết đánh giá (lấy từ `UserSession`). |
| `MaSanPham` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `SanPham(MaSanPham)` | Sản phẩm được đánh giá. |
| `SoSao` | `INT` | | `NOT NULL, CHECK (SoSao BETWEEN 1 AND 5)` | Mức điểm chấm từ 1 đến 5 sao. |
| `BinhLuan` | `NVARCHAR(MAX)` | | `NOT NULL` | Lời nhận xét chi tiết của người dùng. |
| `NgayTao` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Thời điểm đăng bài nhận xét. |
| `NgayCapNhat` | `DATETIME` | | `NULL` | Thời điểm chỉnh sửa nhận xét gần nhất. |

### 2.5. Quan hệ bảng & Quy tắc toàn vẹn dữ liệu
```text
[ DanhMuc ] (1) <---------- (N) [ SanPham ] (1) <---------- (N) [ DanhGia ]
[ NhaPhatHanh ] (1) <------ (N)      ^
                                     |
                                     +--------------------- (N) [ NguoiDung ] (1)
```
- **Ràng buộc duy nhất (Unique Review):** `CONSTRAINT UQ_DanhGia UNIQUE (MaNguoiDung, MaSanPham)` $\rightarrow$ Đảm bảo mỗi khách hàng chỉ được đánh giá tối đa 1 lần cho mỗi sản phẩm.
- **Quy tắc tính toán sao trung bình:** Điểm sao trung bình được tính toán động thông qua hàm tổng hợp:
  $$\text{DiemTrungBinh} = \text{ISNULL}(\text{AVG}(\text{CAST}(\text{SoSao AS DECIMAL}(3,2))), 0.0)$$

---

## 3. Bảng Ma trận Thao tác CRUD theo từng UI

| Giao diện | Thao tác CRUD | Bảng CSDL tác động | Điều kiện lọc & Tham số nghiệp vụ | Kết quả trả về giao diện |
|---|---|---|---|---|
| **UI 2: Cửa hàng** (`StoreViewControl`) | **Read** (Duyệt & Lọc) | `SanPham`, `DanhMuc`, `NhaPhatHanh`, `DanhGia` | `DangKinhDoanh = 1`<br>`@MaDanhMuc` (0 = Tất cả)<br>`@TuKhoa` (tìm tương đối theo `TenSanPham`)<br>`ORDER BY` (Mới nhất, Giá tăng/giảm, Sao cao nhất) | Danh sách `List<SanPhamDTO>` nạp vào các thẻ `ProductCardControl`. |
| **UI 3: Chi tiết SP** (`ProductDetailControl`) | **Read** (Xem chi tiết) | `SanPham`, `DanhMuc`, `NhaPhatHanh`, `DanhGia` | `MaSanPham = @MaSanPham` | Đối tượng `SanPhamDetailDTO` đầy đủ: Ảnh lớn, Giá, Mô tả, `DiemTrungBinh`, `TongDanhGia`. |
| **UI 14: Đánh giá** (`ProductReviewControl`) | **Read** (Xem danh sách) | `DanhGia`, `NguoiDung`, `SanPham` | `MaSanPham = @MaSanPham`<br>`ORDER BY NgayTao DESC` | Danh sách `List<DanhGiaDTO>` hiển thị bài nhận xét cộng đồng kèm điểm tổng quan. |
| **UI 14: Đánh giá** (`ProductReviewControl`) | **Create** (Gửi đánh giá) | `DanhGia` | `@MaNguoiDung`, `@MaSanPham`, `@SoSao (1-5)`, `@BinhLuan` | Thêm bản ghi mới vào CSDL; báo thành công và tính lại điểm sao trung bình. |
| **UI 14: Đánh giá** (`ProductReviewControl`) | **Update** (Sửa bài) | `DanhGia` | `MaNguoiDung = @UserId AND MaSanPham = @ProductId`<br>Cập nhật `@SoSao`, `@BinhLuan`, `NgayCapNhat = GETDATE()` | Cập nhật nội dung nhận xét cũ của chính mình; tính lại điểm trung bình. |
| **UI 14: Đánh giá** (`ProductReviewControl`) | **Delete** (Xóa bài) | `DanhGia` | `MaNguoiDung = @UserId AND MaSanPham = @ProductId` | Xóa bài đánh giá; xóa sạch form và cập nhật lại điểm trung bình. |

---

## 4. Thiết kế Hướng đối tượng (OOP Design) & Hợp đồng Dịch vụ

### 4.1. Thiết kế DTOs (`DPMS.Shared.DTOs`)
```csharp
namespace DPMS.Shared.DTOs
{
    // DTO hiển thị danh sách sản phẩm trên Store và Chi tiết
    public class SanPhamDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public decimal GiaBan { get; set; }
        public string HinhAnh { get; set; }
        public decimal DungLuongMB { get; set; }
        public string PhienBan { get; set; }
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public string TenNhaPhatHanh { get; set; }
        public double DiemDanhGiaTrungBinh { get; set; }
        public int TongLuotDanhGia { get; set; }
    }

    // DTO danh mục cho ComboBox lọc
    public class DanhMucDTO
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
    }

    // DTO bài đánh giá chi tiết
    public class DanhGiaDTO
    {
        public int MaDanhGia { get; set; }
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string HoTen { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoSao { get; set; } // 1 - 5 sao
        public string BinhLuan { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public bool LaBaiVietCuaToi { get; set; }
    }
}
```

### 4.2. Hợp đồng Tầng Nghiệp vụ (`DPMS.BLL.Interfaces`)
```csharp
namespace DPMS.BLL.Interfaces
{
    public interface ISanPhamService
    {
        List<SanPhamDTO> GetDanhSachSanPham(string tuKhoa, int maDanhMuc, string kieuSapXep);
        SanPhamDTO GetChiTietSanPham(int maSanPham);
        List<DanhMucDTO> GetDanhSachDanhMuc();
    }

    public interface IDanhGiaService
    {
        List<DanhGiaDTO> GetDanhGiaTheoSanPham(int maSanPham, int currentUserId);
        DanhGiaDTO GetDanhGiaCuaToi(int maNguoiDung, int maSanPham);
        bool GuiDanhGiaMoi(int maNguoiDung, int maSanPham, int soSao, string binhLuan, out string thongBaoLoi);
        bool CapNhatDanhGia(int maNguoiDung, int maSanPham, int soSao, string binhLuan, out string thongBaoLoi);
        bool XoaDanhGia(int maNguoiDung, int maSanPham);
    }
}
```

---

## 5. Luồng Logic Xử lý Nghiệp vụ (Logic Workflow)

### 5.1. Luồng tải dữ liệu & Lọc trên Store (UI 2)
1. **Khởi tạo:** `StoreViewControl_Load` gọi `ISanPhamService.GetDanhSachDanhMuc()` $\rightarrow$ Đổ vào `cboCategory`.
2. **Kích hoạt tìm kiếm/lọc:** Khi người dùng đổi ComboBox danh mục hoặc bấm "Tìm kiếm" $\rightarrow$ Lấy giá trị `tuKhoa`, `maDanhMuc`, `kieuSapXep`.
3. **Thực thi:** `BLL` gọi `DAL` thực thi câu lệnh SQL với các tham số tương ứng $\rightarrow$ Trả về `List<SanPhamDTO>`.
4. **Hiển thị:** Duyệt qua danh sách, mỗi sản phẩm tạo một thể hiện con `ProductCardControl`, gán sự kiện click `OnViewDetailClicked` $\rightarrow$ Chuyển sang UI 3.

### 5.2. Luồng gửi / cập nhật đánh giá trên UI 14
1. **Kiểm tra đăng nhập:** Kiểm tra `UserSession.IsLoggedIn`. Nếu chưa đăng nhập $\rightarrow$ Chặn và thông báo yêu cầu đăng nhập.
2. **Kiểm tra hợp lệ:** Số sao phải từ 1 đến 5; Bình luận không được để trống.
3. **Phân nhánh nghiệp vụ:**
   - Nếu người dùng chưa từng đánh giá sản phẩm này $\rightarrow$ Gọi `GuiDanhGiaMoi` $\rightarrow$ Thực thi `INSERT`.
   - Nếu người dùng đã có bài đánh giá trước đó $\rightarrow$ Gọi `CapNhatDanhGia` $\rightarrow$ Thực thi `UPDATE`.
4. **Phản hồi:** Thông báo thành công $\rightarrow$ Nạp lại danh sách đánh giá cộng đồng $\rightarrow$ Cập nhật lại số sao trung bình trên UI.
