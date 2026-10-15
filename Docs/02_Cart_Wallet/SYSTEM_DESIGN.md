# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 2
**Phân hệ:** Giỏ hàng, Ví tài khoản & Mã giảm giá (Cart, Steam Wallet & Vouchers)  
**Màn hình phụ trách:** UI 4 (`CartControl`), UI 5 (`WalletControl`), UI 15 (`VoucherControl`)  
**Thành viên phụ trách:** [Chưa phân công / Điền sau]  
**Kiến trúc áp dụng:** 3 tầng (Presentation - Business Logic - Data Access - SQL Server)  
**Quy chuẩn cơ sở dữ liệu:** 100% PascalCase Tiếng Việt Chuẩn Chuyên Ngành CNTT

---

## 1. Kiến trúc phân tầng & Luồng tương tác

```text
+----------------------------------------------------------------------------------------------------+
|                                         PRESENTATION LAYER                                         |
|  - CartControl.cs (Giỏ hàng, áp dụng Voucher, Thanh toán 1-Click)                                  |
|  - WalletControl.cs (Quản lý số dư, Nạp tiền gói nhanh, Biến động số dư)                           |
|  - VoucherControl.cs (Kho Voucher cá nhân, Thu thập mã mới, Dùng ngay)                             |
+--------------------------------------------------+-------------------------------------------------+
                                                   | DTOs (GioHangItemDTO, ViTaiKhoanDTO, MaGiamGiaDTO)
+--------------------------------------------------v-------------------------------------------------+
|                                        BUSINESS LOGIC (BLL)                                        |
|  - IGioHangService / GioHangService.cs           - IViTaiKhoanService / ViTaiKhoanService.cs       |
|  - IMaGiamGiaService / MaGiamGiaService.cs       - Quản lý Transaction mua hàng an toàn (ACID)     |
+--------------------------------------------------+-------------------------------------------------+
                                                   | Entities / Parameters
+--------------------------------------------------v-------------------------------------------------+
|                                       DATA ACCESS LAYER (DAL)                                      |
|  - IGioHangRepository / GioHangRepository.cs     - IViTaiKhoanRepository / ViTaiKhoanRepository.cs |
|  - IMaGiamGiaRepository / MaGiamGiaRepository.cs - SqlTransaction, SqlCommand, SqlDataReader       |
+--------------------------------------------------+-------------------------------------------------+
                                                   | T-SQL Statements / ACID Transaction
+--------------------------------------------------v-------------------------------------------------+
|                                        MICROSOFT SQL SERVER                                        |
|  - Bảng sử dụng: GioHang, NguoiDung (SoDuVi), MaGiamGia, MaGiamGiaNguoiDung,                       |
|                  DonHang, ChiTietDonHang, ThanhToan, SoHuuSanPham                                  |
+----------------------------------------------------------------------------------------------------+
```

---

## 2. Thiết kế Mô hình CSDL & Quan hệ Bảng cho Nhóm 2

### 2.1. Bảng `GioHang` (Trọng tâm UI 4)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaGioHang` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh dòng hàng trong giỏ. |
| `MaNguoiDung` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `NguoiDung(MaNguoiDung)` | Tài khoản sở hữu giỏ hàng (lấy từ phiên đăng nhập). |
| `MaSanPham` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `SanPham(MaSanPham)` | Sản phẩm số được thêm vào giỏ. |
| `SoLuong` | `INT` | | `NOT NULL, DEFAULT 1, CHECK > 0` | Số lượng bản sao cần mua (mặc định 1 cho sản phẩm số). |
| `NgayThem` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Thời điểm thêm vào giỏ, hỗ trợ sắp xếp. |

### 2.2. Thuộc tính Ví trong Bảng `NguoiDung` (Trọng tâm UI 5 & UI 4)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaNguoiDung` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh người dùng. |
| `HoTen` | `NVARCHAR(100)` | | `NOT NULL` | Tên chủ sở hữu ví hiển thị trên UI 5. |
| `TenDangNhap` | `VARCHAR(50)` | | `NOT NULL, UNIQUE` | Tài khoản đăng nhập. |
| `SoDuVi` | `DECIMAL(18,2)` | | `NOT NULL, DEFAULT 0, CHECK >= 0` | Số dư tiền hiện có trong ví. Tuyệt đối không cho phép âm. |

### 2.3. Bảng `MaGiamGia` (Hệ thống Ưu đãi - UI 4 & UI 15)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaGiamGiaId` | `INT` | **PK** | `IDENTITY(1,1)` | Mã định danh khuyến mãi. |
| `MaCode` | `VARCHAR(50)` | | `NOT NULL, UNIQUE` | Chuỗi ký tự nhập (VD: `CHAOBANMOI`, `STEAM50K`). |
| `TieuDe` | `NVARCHAR(150)` | | `NOT NULL` | Tên chương trình ưu đãi hiển thị trên thẻ Voucher UI 15. |
| `MoTa` | `NVARCHAR(255)` | | `NULL` | Điều kiện chi tiết của ưu đãi. |
| `LoaiGiamGia` | `VARCHAR(20)` | | `NOT NULL, CHECK IN ('PhanTram', 'TienMat')` | Hình thức giảm: theo % hoặc trừ số tiền cố định. |
| `GiaTriGiam` | `DECIMAL(18,2)` | | `NOT NULL, CHECK > 0` | Giá trị giảm: 10 (nghĩa là 10%) hoặc 50000 (nghĩa là 50.000đ). |
| `GiamToiDa` | `DECIMAL(18,2)` | | `NULL` | Mức giảm kịch kim cho voucher % (VD: Giảm 20% tối đa 100k). |
| `DonToiThieu` | `DECIMAL(18,2)` | | `NOT NULL, DEFAULT 0` | Giá trị tạm tính tối thiểu của giỏ hàng để được áp dụng. |
| `NgayBatDau` | `DATETIME` | | `NOT NULL` | Thời điểm voucher bắt đầu có hiệu lực. |
| `NgayKetThuc` | `DATETIME` | | `NOT NULL` | Hạn chót sử dụng voucher. |
| `KichHoat` | `BIT` | | `NOT NULL, DEFAULT 1` | Trạng thái phát hành của hệ thống. |

### 2.4. Bảng `MaGiamGiaNguoiDung` (Kho Voucher cá nhân - UI 15 & UI 4)
| Tên cột | Kiểu dữ liệu | Khóa | Ràng buộc / Điều kiện | Ý nghĩa nghiệp vụ trong UI |
|---|---|:---:|---|---|
| `MaNguoiDungVoucherId` | `INT` | **PK** | `IDENTITY(1,1)` | Mã lưu trữ trong kho cá nhân. |
| `MaNguoiDung` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `NguoiDung(MaNguoiDung)` | Chủ nhân của voucher đã thu thập. |
| `MaGiamGiaId` | `INT` | **FK** | `NOT NULL` $\rightarrow$ `MaGiamGia(MaGiamGiaId)` | Mã ưu đãi được gán vào kho cá nhân. |
| `DaSuDung` | `BIT` | | `NOT NULL, DEFAULT 0` | Đánh dấu: 0 = Chưa dùng (khả dụng), 1 = Đã sử dụng. |
| `NgayNhan` | `DATETIME` | | `NOT NULL, DEFAULT GETDATE()` | Ngày thu thập mã vào kho. |
| `NgaySuDung` | `DATETIME` | | `NULL` | Thời điểm áp dụng thành công vào đơn hàng. |

### 2.5. Bảng liên đới trong Giao dịch Thanh toán 1-Click
| Bảng CSDL | Cột tác động chính | Ràng buộc nghiệp vụ trong Transaction |
|---|---|---|
| `DonHang` | `MaDonHangCode`, `TamTinh`, `TienGiamGia`, `TongThanhToan`, `TrangThaiDonHang` | Tạo bản ghi đơn hàng với `TrangThaiDonHang = N'Hoàn thành'`. |
| `ChiTietDonHang` | `MaDonHang`, `MaSanPham`, `SoLuong`, `DonGia` | Chuyển toàn bộ danh sách giỏ hàng sang chi tiết đơn (chốt giá tại thời điểm mua). |
| `ThanhToan` | `SoTien`, `LoaiGiaoDich`, `PhuongThuc`, `TrangThai` | Ghi nhận lịch sử giao dịch: N'Thanh toán đơn hàng', N'Ví Steam Simulation'. |
| `SoHuuSanPham` | `MaNguoiDung`, `MaSanPham`, `MaDonHang`, `SoLanTai` | Cấp quyền sở hữu vĩnh viễn vào kho Thư viện cá nhân của khách. |

### 2.6. Quan hệ bảng & Quy tắc toàn vẹn dữ liệu
```text
[ NguoiDung ] (1) <---------- (N) [ GioHang ] (N) ----------> (1) [ SanPham ]
      |
      | (1)
      +------------------------ (N) [ MaGiamGiaNguoiDung ] (N) ----> (1) [ MaGiamGia ]
      |
      | (1)
      +------------------------ (N) [ ThanhToan ]
      |
      | (1)
      +------------------------ (N) [ DonHang ] (1) <-------- (N) [ ChiTietDonHang ]
      |
      | (1)
      +------------------------ (N) [ SoHuuSanPham ]
```
- **Ràng buộc cặp duy nhất Giỏ hàng:** `CONSTRAINT UQ_GioHang UNIQUE (MaNguoiDung, MaSanPham)` $\rightarrow$ Nếu thêm trùng sản phẩm thì chỉ tăng số lượng.
- **Ràng buộc cặp duy nhất Kho Voucher:** `CONSTRAINT UQ_NguoiDung_Voucher UNIQUE (MaNguoiDung, MaGiamGiaId)` $\rightarrow$ Mỗi tài khoản chỉ được thu thập 1 mã ưu đãi cùng loại một lần.
- **Quy tắc an toàn số dư:** Ràng buộc `CHECK (SoDuVi >= 0)` ngăn chặn hoàn toàn tình trạng tài khoản bị âm tiền khi gặp tranh chấp đồng thời (Race Condition).

---

## 3. Bảng Ma trận Thao tác CRUD theo từng UI

| Giao diện | Thao tác CRUD | Bảng CSDL tác động | Điều kiện lọc & Tham số nghiệp vụ | Kết quả trả về giao diện |
|---|---|---|---|---|
| **UI 4: Giỏ hàng** (`CartControl`) | **Read** (Xem giỏ) | `GioHang`, `SanPham` | `MaNguoiDung = @UserId`<br>`INNER JOIN SanPham` | Danh sách `List<GioHangItemDTO>`, Tạm tính `SubTotal`. |
| **UI 4: Giỏ hàng** (`CartControl`) | **Update** (Đổi số lượng) | `GioHang` | `MaGioHang = @MaGioHang`<br>Cập nhật `SoLuong = @SoLuong` | Cập nhật lại số lượng và tổng thành tiền dòng hàng. |
| **UI 4: Giỏ hàng** (`CartControl`) | **Delete** (Xóa món) | `GioHang` | `MaGioHang = @MaGioHang` hoặc `MaNguoiDung = @UserId` (Xóa hết) | Xóa dòng khỏi DataGridView và tính lại tổng hóa đơn. |
| **UI 4: Giỏ hàng** (`CartControl`) | **Create** (Thanh toán 1-Click) | `NguoiDung`, `DonHang`, `ChiTietDonHang`, `SoHuuSanPham`, `MaGiamGiaNguoiDung`, `ThanhToan`, `GioHang` | Kiểm tra `SoDuVi >= TongThanhToan`<br>Thực thi Transaction 8 bước | Trừ số dư ví, cấp game vào Thư viện, làm sạch giỏ, báo thành công. |
| **UI 5: Ví tài khoản** (`WalletControl`) | **Read** (Xem số dư) | `NguoiDung`, `ThanhToan` | `MaNguoiDung = @UserId` | Hiển thị `SoDuVi` to rõ ràng và bảng lịch sử nạp/trừ tiền. |
| **UI 5: Ví tài khoản** (`WalletControl`) | **Update** (Nạp tiền gói) | `NguoiDung`, `ThanhToan` | `MaNguoiDung = @UserId`<br>`SoDuVi = SoDuVi + @SoTien`<br>Tạo bản ghi `ThanhToan` nạp ví | Tăng số dư ví, cập nhật số dư Header MainForm. |
| **UI 15: Kho Voucher** (`VoucherControl`) | **Read** (Xem danh sách) | `MaGiamGiaNguoiDung`, `MaGiamGia` | `MaNguoiDung = @UserId`<br>Lọc theo `DaSuDung` (0/1) | Danh sách `List<MaGiamGiaDTO>` hiển thị trên lưới hoặc thẻ voucher. |
| **UI 15: Kho Voucher** (`VoucherControl`) | **Create** (Thu thập mã) | `MaGiamGiaNguoiDung`, `MaGiamGia` | `@MaCode`, `KichHoat = 1`, `NgayKetThuc >= GETDATE()`<br>Chưa có trong kho cá nhân | Thêm voucher vào kho cá nhân; sẵn sàng dùng trong Giỏ hàng. |
| **UI 15: Kho Voucher** (`VoucherControl`) | **Delete** (Xóa voucher) | `MaGiamGiaNguoiDung` | `MaNguoiDung = @UserId AND MaGiamGiaId = @VoucherId` | Gỡ bỏ mã voucher đã hết hạn hoặc không muốn lưu trữ. |

---

## 4. Thiết kế Hướng đối tượng (OOP Design) & Hợp đồng Dịch vụ

### 4.1. Thiết kế DTOs (`DPMS.Shared.DTOs`)
```csharp
namespace DPMS.Shared.DTOs
{
    // DTO đại diện cho một sản phẩm trong giỏ hàng
    public class GioHangItemDTO
    {
        public int MaGioHang { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => GiaBan * SoLuong;
    }

    // DTO thông tin ví và lịch sử nạp
    public class ViTaiKhoanDTO
    {
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public decimal SoDuVi { get; set; }
    }

    // DTO thông tin mã giảm giá
    public class MaGiamGiaDTO
    {
        public int MaGiamGiaId { get; set; }
        public string MaCode { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string LoaiGiamGia { get; set; } // "PhanTram" hoặc "TienMat"
        public decimal GiaTriGiam { get; set; } // VD: 10 (%) hoặc 50000 (VNĐ)
        public decimal? GiamToiDa { get; set; }
        public decimal DonToiThieu { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool DaSuDung { get; set; }
    }

    // DTO kết quả thanh toán 1-Click
    public class KetQuaThanhToanDTO
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; }
        public int? MaDonHang { get; set; }
        public decimal SoDuConLai { get; set; }
    }
}
```

### 4.2. Hợp đồng Giao diện Dịch vụ (Service Interfaces)
```csharp
namespace DPMS.BLL.Interfaces
{
    // Nghiệp vụ Giỏ hàng & Thanh toán
    public interface IGioHangService
    {
        List<GioHangItemDTO> GetGioHangByUserId(int maNguoiDung);
        bool AddToGioHang(int maNguoiDung, int maSanPham, int soLuong = 1);
        bool UpdateSoLuong(int maGioHang, int soLuongMoi);
        bool RemoveFromGioHang(int maGioHang);
        bool ClearGioHang(int maNguoiDung);
        KetQuaThanhToanDTO ProcessOneClickWalletPayment(int maNguoiDung, int? maGiamGiaId, decimal tamTinh, decimal tienGiam, decimal tongThanhToan);
    }

    // Nghiệp vụ Ví tài khoản
    public interface IViTaiKhoanService
    {
        ViTaiKhoanDTO GetThongTinVi(int maNguoiDung);
        bool NapTienVi(int maNguoiDung, decimal soTienNap);
        List<ThanhToanDTO> GetLichSuGiaoDich(int maNguoiDung);
    }

    // Nghiệp vụ Mã giảm giá & Kho Voucher
    public interface IMaGiamGiaService
    {
        List<MaGiamGiaDTO> GetVouchersByUser(int maNguoiDung, bool? daSuDung = null);
        MaGiamGiaDTO ValidateVoucher(int maNguoiDung, string maCode, decimal tongTienTamTinh);
        bool ThuThapVoucher(int maNguoiDung, string maCode);
        bool XoaVoucherKho(int maNguoiDung, int maGiamGiaId);
    }
}
```

---

## 5. Thiết kế Luồng Logic Xử lý (Logic Workflows)

### Luồng 1: Áp dụng mã giảm giá và tính toán giỏ hàng (UI 4)
1. **Presentation (`CartControl`):** Người dùng nhập chuỗi mã code (hoặc chọn từ ComboBox) và bấm **[ Áp dụng ]**.
2. **BLL (`MaGiamGiaService.ValidateVoucher`):**
   - Kiểm tra mã code có tồn tại trong kho cá nhân của tài khoản (`MaGiamGiaNguoiDung.DaSuDung = 0`).
   - Kiểm tra hạn dùng (`NgayKetThuc >= GETDATE()`) và giá trị đơn hàng tối thiểu (`TamTinh >= DonToiThieu`).
   - Tính mức giảm:
     - Nếu `LoaiGiamGia == "PhanTram"`: `TienGiam = TamTinh * (GiaTriGiam / 100)`. Nếu có `GiamToiDa`, lấy $\min(\text{TienGiam}, \text{GiamToiDa})$.
     - Nếu `LoaiGiamGia == "TienMat"`: `TienGiam = GiaTriGiam`.
   - Tính tổng tiền mới: `TongThanhToan = TamTinh - TienGiam`.
3. **Presentation:** Cập nhật nhãn `lblDiscount` và `lblTotalAmount`.

---

### Luồng 2: Nạp tiền mô phỏng vào ví tài khoản (UI 5)
1. **Presentation (`WalletControl`):** Người dùng chọn một gói nạp nhanh (VD: 200.000 VNĐ) hoặc nhập số tiền tùy ý $\rightarrow$ Bấm **[ Xác nhận nạp tiền ]**.
2. **BLL (`ViTaiKhoanService.NapTienVi`):**
   - Kiểm tra điều kiện đầu vào: `soTienNap > 0` và là bội số hợp lệ.
3. **DAL (`ViTaiKhoanRepository`):**
   - Mở kết nối, bắt đầu giao dịch nạp tiền.
   - Lệnh 1: `UPDATE NguoiDung SET SoDuVi = SoDuVi + @SoTien WHERE MaNguoiDung = @MaNguoiDung`.
   - Lệnh 2: `INSERT INTO ThanhToan` với `LoaiGiaoDich = N'Nạp tiền ví'`, `PhuongThuc = N'Ví Steam Simulation'`, `TrangThai = N'Thành công'`.
   - Commit giao dịch.
4. **Presentation:** Hiển thị thông báo nạp thành công, làm mới thẻ số dư ví và phát sự kiện cập nhật số dư trên Header `MainForm`.

---

### Luồng 3: Quy trình Giao dịch Thanh toán 1-Click bằng Ví (UI 4)
Quy trình đảm bảo tính toàn vẹn tuyệt đối qua Transaction:
```text
[Khách bấm: ⚡ THANH TOÁN BẰNG SỐ DƯ VÍ]
       |
       v
[BLL: Kiểm tra SoDuVi >= TongThanhToan?]
  |-- (Không đủ tiền) --> Báo lỗi thiếu tiền, gợi ý mở UI 5 nạp thêm.
  |-- (Đủ tiền) --------> Bắt đầu SqlTransaction:
                             |
                             +--> 1. UPDATE NguoiDung: Trừ SoDuVi = SoDuVi - @TongThanhToan
                             +--> 2. INSERT DonHang: Tạo đơn mới (TrangThaiDonHang = N'Hoàn thành')
                             +--> 3. INSERT ChiTietDonHang: Lưu danh sách SP từ GioHang
                             +--> 4. INSERT SoHuuSanPham: Cấp quyền vĩnh viễn vào Thư viện
                             +--> 5. UPDATE MaGiamGiaNguoiDung: Đánh dấu DaSuDung = 1 (nếu có)
                             +--> 6. INSERT ThanhToan: Ghi nhận nhật ký thanh toán đơn
                             +--> 7. DELETE GioHang: Làm sạch giỏ hàng của người dùng
                             |
                             +--> Commit Transaction -> Trả về KetQuaThanhToanDTO thành công
       |
       v
[Presentation: Báo mua thành công, làm sạch bảng giỏ hàng, cập nhật lại số dư trên Header]
```

---

### Luồng 4: Thu thập và Quản lý Kho Voucher cá nhân (UI 15)
1. **Presentation (`VoucherControl`):** Khách hàng nhập mã khuyến mãi (VD: `CHAOBANMOI`) vào ô và bấm **[ Thu thập ]**.
2. **BLL & DAL:**
   - Tra cứu trong bảng `MaGiamGia` để xác định mã tồn tại, `KichHoat = 1` và chưa hết hạn.
   - Kiểm tra mã này đã có trong `MaGiamGiaNguoiDung` của khách hay chưa (chống spam thu thập nhiều lần).
   - Thêm bản ghi mới vào `MaGiamGiaNguoiDung` với `DaSuDung = 0`.
3. **Presentation:** Hiển thị thẻ voucher mới trong danh sách "Voucher khả dụng". Nút **[ Dùng ngay ]** sẽ tự động chuyển view sang UI 4 và điền sẵn mã ưu đãi.
