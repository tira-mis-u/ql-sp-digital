# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 5
**Phân hệ:** Quản lý đơn hàng & Thư viện số (Orders Management & Digital Library)  
**Màn hình phụ trách:** UI 13 (`OrderManageControl`), UI 7 (`OrderHistoryControl`), UI 6 (`LibraryControl`)  
**Thành viên phụ trách:** [Chưa phân công / Điền sau]  
**Kiến trúc áp dụng:** 3 tầng (Presentation - Business Logic - Data Access - SQL Server)  
**Quy chuẩn cơ sở dữ liệu:** 100% PascalCase Tiếng Việt Chuẩn Chuyên Ngành CNTT

---

## 1. Kiến trúc phân tầng & Luồng tương tác

```text
+----------------------------------------------------------------------------------------------------+
|                                         PRESENTATION LAYER                                         |
|  - OrderManageControl.cs (Admin duyệt / hủy đơn hàng, lọc trạng thái, tra cứu chi tiết món)        |
|  - OrderHistoryControl.cs (Khách xem lịch sử giao dịch cá nhân & danh sách món đã mua)             |
|  - LibraryControl.cs (Thư viện game/phần mềm đã sở hữu, Tải tệp số về máy, Gỡ khỏi kho)           |
+--------------------------------------------------+-------------------------------------------------+
                                                   | DTOs (DonHangAdminDTO, ChiTietDonHangDTO, ThuVienItemDTO)
+--------------------------------------------------v-------------------------------------------------+
|                                        BUSINESS LOGIC (BLL)                                        |
|  - IOrderAdminService / OrderAdminService.cs     - IOrderHistoryService / OrderHistoryService.cs   |
|  - ILibraryService / LibraryService.cs           - Logic tải tệp từ Storage & Băm SHA-256 đối chiếu|
+--------------------------------------------------+-------------------------------------------------+
                                                   | Entities / Parameters
+--------------------------------------------------v-------------------------------------------------+
|                                       DATA ACCESS LAYER (DAL)                                      |
|  - IOrderRepository / OrderRepository.cs         - ILibraryRepository / LibraryRepository.cs       |
|  - ADO.NET: SqlConnection, SqlCommand, SqlDataReader, SqlTransaction                               |
+--------------------------------------------------+-------------------------------------------------+
                                                   | T-SQL Statements
+--------------------------------------------------v-------------------------------------------------+
|                                        MICROSOFT SQL SERVER                                        |
|  - Bảng sử dụng chính: DonHang, ChiTietDonHang, SoHuuSanPham, TepSanPham, SanPham, NguoiDung       |
+----------------------------------------------------------------------------------------------------+
```

---

## 2. Thiết kế Mô hình CSDL & Quan hệ Bảng cho Nhóm 5

### 2.1. Bảng `DonHang` (Trọng tâm UI 13 & UI 7)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaDonHang` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh nội bộ đơn hàng. |
| `MaDonHangCode` | `VARCHAR(50)` | | `NOT NULL, UNIQUE` | Mã đơn hiển thị (VD: `ORD-20261015-8821`). |
| `MaNguoiDung` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `NguoiDung(MaNguoiDung)` | Khách hàng đặt mua đơn hàng. |
| `MaGiamGiaId` | `INT` | **FK** | `NULL` $\rightarrow$ `MaGiamGia(MaGiamGiaId)` | Mã ưu đãi đã áp dụng (nếu có). |
| `TamTinh` | `DECIMAL(18,2)` | | `NOT NULL, CHECK >= 0` | Tổng giá trị gốc các món hàng. |
| `TienGiamGia` | `DECIMAL(18,2)` | | `NOT NULL, DEFAULT 0` | Số tiền được khấu trừ từ Voucher. |
| `TongThanhToan` | `DECIMAL(18,2)` | | `NOT NULL, CHECK >= 0` | Tiền thực trả của khách hàng (`TamTinh - TienGiamGia`). |
| `TrangThaiDonHang` | `NVARCHAR(50)` | | `NOT NULL, DEFAULT N'Chờ xử lý'` | Trạng thái: `N'Hoàn thành'`, `N'Chờ xử lý'`, `N'Đã hủy'`. |
| `GhiChu` | `NVARCHAR(255)` | | `NULL` | Phương thức hoặc ghi chú thanh toán. |
| `NgayTao` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Ngày đặt mua đơn hàng. |
| `NgayCapNhat` | `DATETIME` | | `NULL` | Thời điểm Admin cập nhật trạng thái đơn gần nhất. |

### 2.2. Bảng `ChiTietDonHang` (Xem danh sách sản phẩm trong đơn)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaChiTietDonHang` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh dòng chi tiết đơn. |
| `MaDonHang` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `DonHang(MaDonHang) ON DELETE CASCADE` | Thuộc về đơn hàng nào. |
| `MaSanPham` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `SanPham(MaSanPham)` | Sản phẩm số được mua. |
| `SoLuong` | `INT` | | `NOT NULL, DEFAULT 1, CHECK > 0` | Số lượng bản sao mua. |
| `DonGia` | `DECIMAL(18,2)` | | `NOT NULL, CHECK >= 0` | Giá niêm yết tại đúng thời điểm mua (chống lệch giá). |

### 2.3. Bảng `SoHuuSanPham` (Trọng tâm Thư viện - UI 6)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaSoHuu` | `INT` | **PK** | `IDENTITY(1,1)` | Mã quyền sở hữu số. |
| `MaNguoiDung` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `NguoiDung(MaNguoiDung)` | Khách hàng đang sở hữu sản phẩm trong thư viện. |
| `MaSanPham` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `SanPham(MaSanPham)` | Sản phẩm số có trong thư viện. |
| `MaDonHang` | `INT` | **FK** | `NULL` $\rightarrow$ `DonHang(MaDonHang)` | Gắn với đơn hàng mua (chứng minh giao dịch hợp lệ). |
| `NgaySoHuu` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Thời điểm cấp quyền vào Thư viện. |
| `SoLanTai` | `INT` | | `NOT NULL, DEFAULT 0` | Bộ đếm số lần khách đã tải sản phẩm này về máy. |

### 2.4. Bảng `TepSanPham` (Đọc để tải file cho Khách tại UI 6)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaTep` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh tệp số. |
| `MaSanPham` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `SanPham(MaSanPham)` | Xác định tệp thuộc sản phẩm nào. |
| `TenTep` | `NVARCHAR(255)` | | `NOT NULL` | Tên tệp lưu file vật lý. |
| `DuongDanTep` | `NVARCHAR(500)` | | `NOT NULL` | Đường dẫn tương đối (`Storage/Products/...`) dùng để copy ra máy khách. |
| `DinhDangTep` | `VARCHAR(20)` | | `NOT NULL` | Định dạng tệp (.zip, .exe, .iso, .pdf). |
| `KichThuocByte` | `BIGINT` | | `NOT NULL` | Dung lượng tệp tính chính xác bằng byte. |
| `MaHashSHA256` | `VARCHAR(64)` | | `NOT NULL` | Checksum SHA-256 để kiểm tra tệp tải về có toàn vẹn không. |

### 2.5. Quan hệ bảng & Quy tắc toàn vẹn dữ liệu
```text
[ NguoiDung ] (1) <---------- (N) [ DonHang ] (1) <---------- (N) [ ChiTietDonHang ]
      |                                | (1)                               | (N)
      |                                v                                   v
      +------------------------ (N) [ SoHuuSanPham ] (N) ------------> (1) [ SanPham ] (1)
                                                                           |
                                                                           v (N)
                                                                    [ TepSanPham ]
```
- **Ràng buộc duy nhất Thư viện:** `CONSTRAINT UQ_SoHuu UNIQUE (MaNguoiDung, MaSanPham)` $\rightarrow$ Khách hàng không thể sở hữu trùng 2 lần cùng một sản phẩm số.
- **Ràng buộc toàn vẹn Đơn hàng:** `ON DELETE CASCADE` trên `ChiTietDonHang` đảm bảo không bao giờ tồn tại các dòng chi tiết mồ côi nếu đơn hàng bị xóa khỏi hệ thống.
- **Quy tắc duyệt đơn:** Khi Admin duyệt đơn sang `N'Hoàn thành'`, hệ thống tự động kiểm tra và chèn bản ghi vào `SoHuuSanPham` nếu khách hàng chưa sở hữu sản phẩm đó.

---

## 3. Bảng Ma trận Thao tác CRUD theo từng UI

| Giao diện | Thao tác CRUD | Bảng CSDL tác động | Điều kiện lọc & Tham số nghiệp vụ | Kết quả trả về giao diện |
|---|---|---|---|---|
| **UI 13: Quản trị Đơn hàng** (`OrderManageControl`) | **Read** (Xem danh sách đơn) | `DonHang`, `NguoiDung` | `@TrangThai` ('Tất cả', 'Hoàn thành', 'Chờ xử lý', 'Đã hủy')<br>`@TuKhoa` (tìm theo mã đơn hoặc tên khách)<br>`ORDER BY MaDonHang DESC` | Danh sách `List<DonHangAdminDTO>` nạp vào DataGridView đơn hàng. |
| **UI 13: Quản trị Đơn hàng** (`OrderManageControl`) | **Read** (Xem chi tiết đơn) | `ChiTietDonHang`, `SanPham` | `MaDonHang = @MaDonHang` | Danh sách các món hàng (Tên, Ảnh, Giá mua, SL, Thành tiền). |
| **UI 13: Quản trị Đơn hàng** (`OrderManageControl`) | **Update** (Duyệt đơn) | `DonHang`, `SoHuuSanPham` | `MaDonHang = @MaDonHang`<br>Cập nhật `TrangThaiDonHang = N'Hoàn thành'`<br>Tự động cấp vào `SoHuuSanPham` | Cập nhật trạng thái đơn; cấp game vào Thư viện của khách. |
| **UI 13: Quản trị Đơn hàng** (`OrderManageControl`) | **Update** (Hủy đơn) | `DonHang` | `MaDonHang = @MaDonHang`<br>Cập nhật `TrangThaiDonHang = N'Đã hủy'` | Hủy đơn; không cấp quyền sở hữu. |
| **UI 7: Lịch sử đơn hàng** (`OrderHistoryControl`) | **Read** (Lịch sử cá nhân) | `DonHang`, `ChiTietDonHang`, `SanPham` | `MaNguoiDung = @UserId`<br>`ORDER BY NgayTao DESC` | Danh sách các đơn hàng cá nhân đã mua, tổng tiền, ngày giờ chi tiết. |
| **UI 6: Thư viện số** (`LibraryControl`) | **Read** (Duyệt kho sở hữu) | `SoHuuSanPham`, `SanPham`, `DanhMuc`, `TepSanPham` | `MaNguoiDung = @UserId`<br>`@TuKhoa` (tìm tên game/app trong kho) | Danh sách `List<ThuVienItemDTO>` nạp vào lưới thẻ Thư viện số. |
| **UI 6: Thư viện số** (`LibraryControl`) | **Update** (Tải tệp & Tăng bộ đếm) | `SoHuuSanPham`, `TepSanPham` | `MaNguoiDung = @UserId AND MaSanPham = @MaSanPham`<br>Tăng `SoLanTai = SoLanTai + 1` | Đọc tệp từ `Storage/Products/...`, copy ra máy khách có ProgressBar; tăng bộ đếm tải. |
| **UI 6: Thư viện số** (`LibraryControl`) | **Delete** (Gỡ khỏi kho) | `SoHuuSanPham` | `MaNguoiDung = @UserId AND MaSanPham = @MaSanPham` | Xóa bản ghi sở hữu; sản phẩm biến mất khỏi Thư viện của người dùng. |

---

## 4. Thiết kế Hướng đối tượng (OOP Design) & Hợp đồng Dịch vụ

### 4.1. Thiết kế DTOs (`DPMS.Shared.DTOs`)
```csharp
namespace DPMS.Shared.DTOs
{
    // DTO hiển thị đơn hàng trên DataGridView Admin
    public class DonHangAdminDTO
    {
        public int MaDonHang { get; set; }
        public string MaDonHangCode { get; set; }
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string HoTen { get; set; }
        public decimal TamTinh { get; set; }
        public decimal TienGiamGia { get; set; }
        public decimal TongThanhToan { get; set; }
        public string TrangThaiDonHang { get; set; } // "Hoàn thành", "Chờ xử lý", "Đã hủy"
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
    }

    // DTO chi tiết từng món hàng trong đơn
    public class ChiTietDonHangDTO
    {
        public int MaChiTietDonHang { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }

    // DTO sản phẩm trong kho Thư viện số của người dùng
    public class ThuVienItemDTO
    {
        public int MaSoHuu { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
        public string TenDanhMuc { get; set; }
        public string PhienBan { get; set; }
        public decimal DungLuongMB { get; set; }
        public DateTime NgaySoHuu { get; set; }
        public int SoLanTai { get; set; }
        public List<TepSanPhamDTO> DanhSachTep { get; set; } = new List<TepSanPhamDTO>();
    }
}
```

### 4.2. Hợp đồng Giao diện Dịch vụ (Service Interfaces)
```csharp
namespace DPMS.BLL.Interfaces
{
    // Nghiệp vụ Quản trị Đơn hàng (Admin)
    public interface IOrderAdminService
    {
        List<DonHangAdminDTO> GetAllOrders(string trangThai = null, string tuKhoa = null);
        List<ChiTietDonHangDTO> GetOrderDetails(int maDonHang);
        bool ApproveOrder(int maDonHang);
        bool CancelOrder(int maDonHang);
    }

    // Nghiệp vụ Lịch sử Đơn hàng (Khách hàng)
    public interface IOrderHistoryService
    {
        List<DonHangAdminDTO> GetCustomerOrders(int maNguoiDung);
        List<ChiTietDonHangDTO> GetCustomerOrderDetails(int maNguoiDung, int maDonHang);
    }

    // Nghiệp vụ Thư viện số & Tải tệp (Khách hàng)
    public interface ILibraryService
    {
        List<ThuVienItemDTO> GetUserLibrary(int maNguoiDung, string tuKhoa = null);
        Task<bool> DownloadProductFileAsync(int maNguoiDung, int maTep, string localDestFolder, IProgress<int> progress);
        bool RemoveFromLibrary(int maNguoiDung, int maSanPham);
    }
}
```

---

## 5. Thiết kế Luồng Logic Xử lý (Logic Workflows)

### Luồng 1: Admin Duyệt / Hủy đơn hàng và tự động kích hoạt sở hữu (UI 13)
```text
[Admin chọn đơn hàng trên DataGridView] -> Bấm [ Duyệt đơn hàng ]
          |
          v
[BLL: IOrderAdminService.ApproveOrder(maDonHang)]
          |
          v
[DAL: Mở SqlTransaction an toàn]
  +--> Bước 1: UPDATE DonHang SET TrangThaiDonHang = N'Hoàn thành', NgayCapNhat = GETDATE() WHERE MaDonHang = @MaDonHang
  +--> Bước 2: Lấy danh sách MaSanPham từ ChiTietDonHang của đơn này
  +--> Bước 3: INSERT INTO SoHuuSanPham (MaNguoiDung, MaSanPham, MaDonHang, NgaySoHuu, SoLanTai)
               chọn từ các món chưa có trong SoHuuSanPham của khách
  +--> Bước 4: Commit Transaction -> Báo thành công
          |
          v
[Presentation: Cập nhật lại màu sắc trạng thái dòng đơn sang Xanh lá; Khách hàng sẽ thấy game trong Thư viện ngay lập tức]
```

---

### Luồng 2: Khách hàng tra cứu lịch sử mua và chi tiết hóa đơn (UI 7)
1. **Presentation (`OrderHistoryControl`):** Khi người dùng mở màn hình, giao diện gửi mã `UserSession.CurrentUserId` tới BLL.
2. **BLL (`OrderHistoryService.GetCustomerOrders`):**
   - Truy vấn danh sách hóa đơn theo `MaNguoiDung = @UserId` sắp xếp theo thời gian mới nhất.
   - Nạp vào bảng tóm tắt: Mã đơn code, Ngày mua, Phương thức, Tiền giảm giá, Tổng thanh toán.
3. **Xem chi tiết:** Khách click chọn 1 đơn hàng $\rightarrow$ DataGridView con hiển thị toàn bộ danh sách game trong đơn hàng đó kèm hình ảnh và giá mua chốt tại thời điểm thanh toán.

---

### Luồng 3: Quản lý Thư viện số và Tải tệp kỹ thuật số về máy tính (UI 6)
1. **Presentation (`LibraryControl`):**
   - Khách hàng xem kho game/phần mềm dạng lưới thẻ hoặc danh sách Sidebar.
   - Chọn một sản phẩm $\rightarrow$ Xem thông tin tệp cài đặt (Tên file, Dung lượng, Phiên bản).
   - Bấm nút **[ ⬇️ Tải xuống về máy ]**.
2. **Chọn thư mục lưu:** Hệ thống mở `FolderBrowserDialog` cho khách chọn nơi lưu trên máy tính cá nhân.
3. **BLL (`LibraryService.DownloadProductFileAsync`):**
   - Lấy thông tin bản ghi `TepSanPham` (đường dẫn tương đối `Storage/Products/...`).
   - Phân giải đường dẫn nguồn tuyệt đối trên máy chủ / ứng dụng.
   - Sử dụng `FileStream` đọc/ghi từng khối nhị phân 64KB sang thư mục khách chọn.
   - Báo cáo tiến độ về giao diện qua `IProgress<int>` để thanh `ProgressBar` tăng mượt mà từ 0% đến 100%.
   - Sau khi copy xong, băm lại SHA-256 tệp đích để đối chiếu với `MaHashSHA256` trong CSDL nhằm đảm bảo tệp nguyên vẹn 100%.
4. **DAL:** Tăng bộ đếm: `UPDATE SoHuuSanPham SET SoLanTai = SoLanTai + 1`.
5. **Presentation:** Thông báo tải tệp thành công, mở thư mục chứa tệp đã tải nếu khách yêu cầu.

---

### Luồng 4: Gỡ bỏ sản phẩm khỏi Thư viện cá nhân (UI 6)
1. **Presentation (`LibraryControl`):** Khách hàng click chuột phải vào thẻ game hoặc bấm nút **[ 🗑️ Gỡ khỏi thư viện ]**.
2. **Xác nhận người dùng:** Hiển thị hộp thoại cảnh báo: *"Bạn có chắc chắn muốn gỡ sản phẩm này khỏi Thư viện cá nhân?"*.
3. **BLL & DAL (`LibraryService.RemoveFromLibrary`):**
   - Thực thi xóa an toàn: `DELETE FROM SoHuuSanPham WHERE MaNguoiDung = @UserId AND MaSanPham = @MaSanPham`.
   - Lưu ý: Bản ghi `DonHang` và `ChiTietDonHang` trong quá khứ vẫn được giữ nguyên vẹn để đối chiếu hóa đơn tài chính.
4. **Presentation:** Xóa thẻ sản phẩm khỏi giao diện Thư viện, làm mới số lượng game đang sở hữu.
