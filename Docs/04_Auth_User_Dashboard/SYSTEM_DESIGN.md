# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 4
**Phân hệ:** Xác thực, Quản trị người dùng & Dashboard thống kê (Auth, User Management & Admin Dashboard)  
**Màn hình phụ trách:** UI 1 (`LoginControl`, `RegisterControl`), UI 12 (`UserManageControl`), UI 9 (`DashboardControl`)  
**Thành viên phụ trách:** Trần Quốc Thuận  
**Kiến trúc áp dụng:** 3 tầng (Presentation - Business Logic - Data Access - SQL Server)  
**Quy chuẩn cơ sở dữ liệu:** 100% PascalCase Tiếng Việt Chuẩn Chuyên Ngành CNTT

---

## 1. Kiến trúc phân tầng & Luồng tương tác

```text
+----------------------------------------------------------------------------------------------------+
|                                         PRESENTATION LAYER                                         |
|  - LoginControl.cs (Đăng nhập, điều hướng phân quyền)  - RegisterControl.cs (Đăng ký tài khoản)    |
|  - UserManageControl.cs (Quản trị danh sách người dùng) - DashboardControl.cs (Thống kê chỉ số)    |
+--------------------------------------------------+-------------------------------------------------+
                                                   | DTOs (NguoiDungDTO, DashboardStatsDTO, TopProductDTO)
+--------------------------------------------------v-------------------------------------------------+
|                                        BUSINESS LOGIC (BLL)                                        |
|  - IAuthService / AuthService.cs                 - IUserManageService / UserManageService.cs       |
|  - IDashboardService / DashboardService.cs       - UserSession (Quản lý phiên làm việc tĩnh)       |
|  - Logic mã hóa băm mật khẩu SHA-256             - Kiểm tra khóa tài khoản, phân quyền Admin/User  |
+--------------------------------------------------+-------------------------------------------------+
                                                   | Entities / Parameters
+--------------------------------------------------v-------------------------------------------------+
|                                       DATA ACCESS LAYER (DAL)                                      |
|  - IAuthRepository / AuthRepository.cs           - IUserRepository / UserRepository.cs             |
|  - IDashboardRepository / DashboardRepository.cs - ADO.NET: SqlConnection, SqlCommand, SqlDataReader|
+--------------------------------------------------+-------------------------------------------------+
                                                   | T-SQL Statements & Aggregations
+--------------------------------------------------v-------------------------------------------------+
|                                        MICROSOFT SQL SERVER                                        |
|  - Bảng sử dụng chính: NguoiDung, DonHang, ChiTietDonHang, SanPham                                 |
+----------------------------------------------------------------------------------------------------+
```

---

## 2. Thiết kế Mô hình CSDL & Quan hệ Bảng cho Nhóm 4

### 2.1. Bảng `NguoiDung` (Trọng tâm UI 1 & UI 12)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaNguoiDung` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh người dùng toàn hệ thống. |
| `TenDangNhap` | `VARCHAR(50)` | | `NOT NULL, UNIQUE` | Tên đăng nhập (chỉ chữ và số, không trùng lặp). |
| `MatKhauHash` | `VARCHAR(256)` | | `NOT NULL` | Chuỗi mã băm bảo mật SHA-256 của mật khẩu. |
| `HoTen` | `NVARCHAR(100)` | | `NOT NULL` | Họ tên hiển thị trên thanh tiêu đề và bảng quản trị. |
| `Email` | `VARCHAR(100)` | | `NOT NULL, UNIQUE` | Hộp thư điện tử liên hệ và phục hồi tài khoản. |
| `SoDienThoai` | `VARCHAR(20)` | | `NULL` | Số điện thoại liên hệ cá nhân. |
| `VaiTro` | `NVARCHAR(20)` | | `NOT NULL, DEFAULT N'KhachHang', CHECK IN (N'QuanTriVien', N'KhachHang')` | Phân quyền: `QuanTriVien` (truy cập quản trị) hoặc `KhachHang` (mua sắm). |
| `TrangThai` | `BIT` | | `NOT NULL, DEFAULT 1` | Trạng thái tài khoản: 1 = Hoạt động bình thường, 0 = Đã bị khóa. |
| `SoDuVi` | `DECIMAL(18,2)` | | `NOT NULL, DEFAULT 0, CHECK >= 0` | Số dư tài khoản ví Steam simulation. |
| `NgayTao` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Ngày đăng ký tài khoản. |

### 2.2. Các bảng phục vụ tổng hợp chỉ số Dashboard (Trọng tâm UI 9)
| Bảng CSDL | Cột truy vấn chính | Điều kiện & Hàm tổng hợp | Ý nghĩa hiển thị trên Thẻ Dashboard |
|---|---|---|---|
| `DonHang` | `TongThanhToan` | `SUM(TongThanhToan) WHERE TrangThaiDonHang = N'Hoàn thành'` | **Tổng doanh thu thực tế** của toàn sàn (VNĐ). |
| `DonHang` | `MaDonHang` | `COUNT(1) WHERE TrangThaiDonHang = N'Hoàn thành'` | **Tổng số đơn hàng thành công**. |
| `NguoiDung` | `MaNguoiDung` | `COUNT(1) WHERE VaiTro = N'KhachHang'` | **Tổng số khách hàng đăng ký**. |
| `SanPham` | `MaSanPham` | `COUNT(1) WHERE DangKinhDoanh = 1` | **Tổng sản phẩm đang mở bán**. |
| `ChiTietDonHang` + `SanPham` | `MaSanPham`, `SoLuong` | `SUM(SoLuong) GROUP BY SanPham ORDER BY DESC TOP 5` | **Top 5 sản phẩm bán chạy nhất**. |

### 2.3. Quan hệ bảng & Quy tắc toàn vẹn dữ liệu
```text
[ NguoiDung ] (1) <---------- (N) [ DonHang ] (1) <---------- (N) [ ChiTietDonHang ] (N) ----------> (1) [ SanPham ]
```
- **Ràng buộc duy nhất:** `CONSTRAINT UQ_NguoiDung_TenDangNhap UNIQUE (TenDangNhap)` và `CONSTRAINT UQ_NguoiDung_Email UNIQUE (Email)` ngăn chặn triệt để hành vi đăng ký trùng lặp.
- **Ràng buộc phân quyền hợp lệ:** Cột `VaiTro` chỉ chấp nhận một trong hai giá trị định trước: `N'QuanTriVien'` hoặc `N'KhachHang'`.
- **Quy tắc an toàn quản trị:** Không cho phép tài khoản Quản trị viên tự khóa tài khoản của chính mình thông qua giao diện UI 12.

---

## 3. Bảng Ma trận Thao tác CRUD theo từng UI

| Giao diện | Thao tác CRUD | Bảng CSDL tác động | Điều kiện lọc & Tham số nghiệp vụ | Kết quả trả về giao diện |
|---|---|---|---|---|
| **UI 1: Xác thực** (`LoginControl`) | **Read** (Đăng nhập) | `NguoiDung` | `TenDangNhap = @User AND MatKhauHash = @Hash` | Trả về `NguoiDungDTO` nếu hợp lệ; kiểm tra `TrangThai = 1`. Lưu `UserSession`. |
| **UI 1: Xác thực** (`RegisterControl`) | **Create** (Đăng ký) | `NguoiDung` | `@TenDangNhap`, `@MatKhauHash`, `@HoTen`, `@Email`, `@SoDienThoai`<br>`VaiTro = N'KhachHang'`, `TrangThai = 1` | Thêm tài khoản mới vào CSDL; thông báo thành công và chuyển sang form Đăng nhập. |
| **UI 1: Xác thực** (`RegisterControl`) | **Read** (Kiểm tra trùng) | `NguoiDung` | `TenDangNhap = @User OR Email = @Email` | Báo lỗi ngay nếu tên đăng nhập hoặc email đã tồn tại trước khi tạo. |
| **UI 12: Quản lý người dùng** (`UserManageControl`) | **Read** (Duyệt danh sách) | `NguoiDung` | `@VaiTro` (N'Tất cả', N'KhachHang', N'QuanTriVien')<br>`@TuKhoa` (tìm theo `TenDangNhap` hoặc `HoTen`)<br>`ORDER BY MaNguoiDung DESC` | Danh sách `List<NguoiDungDTO>` nạp vào DataGridView danh sách người dùng. |
| **UI 12: Quản lý người dùng** (`UserManageControl`) | **Update** (Khóa tài khoản) | `NguoiDung` | `MaNguoiDung = @UserId`<br>`TrangThai = 0` (Chặn không khóa chính mình) | Khóa tài khoản thành công; người dùng bị khóa sẽ không thể đăng nhập. |
| **UI 12: Quản lý người dùng** (`UserManageControl`) | **Update** (Mở khóa tài khoản) | `NguoiDung` | `MaNguoiDung = @UserId`<br>`TrangThai = 1` | Khôi phục trạng thái hoạt động bình thường cho tài khoản. |
| **UI 9: Dashboard** (`DashboardControl`) | **Read** (Tổng hợp chỉ số) | `DonHang`, `NguoiDung`, `SanPham` | `SUM(TongThanhToan)`, `COUNT(DonHang)`, `COUNT(NguoiDung)`, `COUNT(SanPham)` | Nạp các con số tổng quan vào 4 Metric Cards lớn trên Dashboard. |
| **UI 9: Dashboard** (`DashboardControl`) | **Read** (Top bán chạy) | `ChiTietDonHang`, `SanPham` | `INNER JOIN`, `GROUP BY`, `ORDER BY SUM(SoLuong) DESC`, `TOP 5` | Hiển thị bảng 5 sản phẩm có doanh số bán ra cao nhất sàn. |

---

## 4. Quản lý Phiên làm việc tĩnh & Thiết kế Hướng đối tượng (OOP Design)

### 4.1. Lớp Phiên làm việc tĩnh (`UserSession.cs`)
```csharp
namespace DPMS.Shared
{
    public static class UserSession
    {
        public static int CurrentUserId { get; set; } = 0;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentFullName { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = string.Empty; // "QuanTriVien" hoặc "KhachHang"
        public static decimal CurrentBalance { get; set; } = 0;

        public static bool IsLoggedIn => CurrentUserId > 0;
        public static bool IsAdmin => CurrentRole.Equals("QuanTriVien", System.StringComparison.OrdinalIgnoreCase);

        public static void SetSession(int id, string user, string fullName, string role, decimal balance)
        {
            CurrentUserId = id;
            CurrentUsername = user;
            CurrentFullName = fullName;
            CurrentRole = role;
            CurrentBalance = balance;
        }

        public static void Clear()
        {
            CurrentUserId = 0;
            CurrentUsername = string.Empty;
            CurrentFullName = string.Empty;
            CurrentRole = string.Empty;
            CurrentBalance = 0;
        }
    }
}
```

### 4.2. Thiết kế DTOs (`DPMS.Shared.DTOs`)
```csharp
namespace DPMS.Shared.DTOs
{
    // DTO thông tin tài khoản người dùng
    public class NguoiDungDTO
    {
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string VaiTro { get; set; }
        public bool TrangThai { get; set; }
        public decimal SoDuVi { get; set; }
        public DateTime NgayTao { get; set; }
    }

    // DTO đăng ký tài khoản mới
    public class DangKyDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
    }

    // DTO tổng hợp chỉ số Dashboard
    public class DashboardStatsDTO
    {
        public decimal TongDoanhThu { get; set; }
        public int TongDonHang { get; set; }
        public int TongKhachHang { get; set; }
        public int TongSanPhamDangBan { get; set; }
    }

    // DTO top sản phẩm bán chạy
    public class SanPhamBanChayDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuongDaBan { get; set; }
        public decimal DoanhThuUocTinh { get; set; }
    }
}
```

### 4.3. Hợp đồng Giao diện Dịch vụ (Service Interfaces)
```csharp
namespace DPMS.BLL.Interfaces
{
    // Nghiệp vụ Xác thực & Đăng nhập
    public interface IAuthService
    {
        (bool Success, string Message, NguoiDungDTO User) Login(string username, string password);
        (bool Success, string Message) Register(DangKyDTO dto);
        bool CheckUsernameOrEmailExists(string username, string email);
        string ComputeHashSHA256(string plainPassword);
    }

    // Nghiệp vụ Quản trị Người dùng (Admin)
    public interface IUserManageService
    {
        List<NguoiDungDTO> GetUsers(string vaiTro = null, string tuKhoa = null);
        bool SetUserStatus(int maNguoiDung, bool trangThai, int currentAdminId, out string errorMessage);
    }

    // Nghiệp vụ Thống kê Dashboard (Admin)
    public interface IDashboardService
    {
        DashboardStatsDTO GetDashboardStats();
        List<SanPhamBanChayDTO> GetTopSellingProducts(int topN = 5);
        List<DonHangDTO> GetRecentOrders(int topN = 5);
    }
}
```

---

## 5. Thiết kế Luồng Logic Xử lý (Logic Workflows)

### Luồng 1: Quy trình Xác thực Đăng nhập & Phân quyền giao diện (UI 1)
```text
[Khách nhập Username & Password] -> Bấm [ Đăng nhập ]
          |
          v
[BLL: Băm mật khẩu nhập vào bằng SHA-256]
          |
          v
[DAL: Truy vấn NguoiDung theo TenDangNhap & MatKhauHash]
  |-- Không khớp tài khoản/mật khẩu -> Báo lỗi: "Tên đăng nhập hoặc mật khẩu không chính xác!"
  |-- Khớp bản ghi nhưng TrangThai == 0 -> Báo lỗi: "Tài khoản của bạn hiện đang bị khóa!"
  |-- Hợp lệ ->
          |
          +--> Thiết lập UserSession (MaNguoiDung, TenDangNhap, HoTen, VaiTro, SoDuVi)
          +--> Gọi MainForm.Instance.ApplyUserPermissions():
                 * Nếu VaiTro == "QuanTriVien": Hiển thị toàn bộ menu Quản trị (Dashboard, Quản lý SP, Đơn hàng, Người dùng).
                 * Nếu VaiTro == "KhachHang": Ẩn các menu Admin, chỉ hiện Store, Thư viện, Giỏ hàng, Ví, Hồ sơ.
          +--> Chuyển view hiển thị màn hình chính (StoreViewControl hoặc DashboardControl).
```

---

### Luồng 2: Quy trình Đăng ký tài khoản mới & Mã hóa mật khẩu (UI 1)
1. **Presentation (`RegisterControl`):** Người dùng nhập thông tin: Tên đăng nhập, Mật khẩu, Nhập lại mật khẩu, Họ tên, Email, Số điện thoại $\rightarrow$ Bấm **[ Đăng ký ]**.
2. **BLL (`AuthService.Register`):**
   - Kiểm tra rỗng và ràng buộc độ dài tối thiểu (Mật khẩu $\ge$ 6 ký tự).
   - Kiểm tra mật khẩu và xác nhận mật khẩu khớp nhau 100%.
   - Gọi `CheckUsernameOrEmailExists`: Nếu trùng tên đăng nhập hoặc email thì trả về lỗi thông báo người dùng đổi giá trị khác.
   - Băm mật khẩu bằng thuật toán SHA-256: `MatKhauHash = ComputeHashSHA256(dto.MatKhau)`.
3. **DAL (`AuthRepository`):**
   - Thực thi chèn bản ghi mới với `VaiTro = N'KhachHang'`, `TrangThai = 1`, `SoDuVi = 0`.
4. **Presentation:** Hiển thị thông báo đăng ký thành công và tự động chuyển về giao diện `LoginControl` để người dùng đăng nhập.

---

### Luồng 3: Quản lý danh sách tài khoản & Khóa / Mở khóa người dùng (UI 12)
1. **Presentation (`UserManageControl`):** Admin chọn một dòng tài khoản trên DataGridView và bấm **[ Khóa tài khoản ]** hoặc **[ Mở khóa ]**.
2. **BLL (`UserManageService.SetUserStatus`):**
   - Kiểm tra an toàn: `if (maNguoiDung == currentAdminId) return false;` (Chặn hành vi tự khóa tài khoản Quản trị viên đang thao tác).
3. **DAL (`UserRepository`):**
   - Thực thi cập nhật: `UPDATE NguoiDung SET TrangThai = @TrangThai WHERE MaNguoiDung = @MaNguoiDung`.
4. **Presentation:** Nạp lại DataGridView danh sách người dùng và cập nhật màu sắc trạng thái (Xanh lá: Hoạt động, Đỏ: Bị khóa).

---

### Luồng 4: Tính toán và Làm mới Dashboard thống kê kinh doanh (UI 9)
1. **Presentation (`DashboardControl`):** Màn hình được mở hoặc Admin bấm nút **[ 🔄 Làm mới dữ liệu ]**.
2. **BLL & DAL (`DashboardService`):**
   - Thực thi truy vấn tổng hợp đồng thời:
     - Tính tổng doanh thu: `SUM(TongThanhToan)` từ các đơn hàng có trạng thái `'Hoàn thành'`.
     - Đếm tổng số đơn hàng: `COUNT(1) FROM DonHang`.
     - Đếm tổng số khách hàng: `COUNT(1) FROM NguoiDung WHERE VaiTro = N'KhachHang'`.
     - Đếm tổng sản phẩm mở bán: `COUNT(1) FROM SanPham WHERE DangKinhDoanh = 1`.
     - Lấy Top 5 sản phẩm bán chạy nhất gom nhóm theo số lượng bán và doanh thu.
3. **Presentation:** Hiển thị số liệu lên 4 Card lớn có màu sắc gradient hiện đại, cập nhật bảng Top 5 sản phẩm bán chạy nhất.
