# HỆ THỐNG TÀI LIỆU DỰ ÁN DPMS (DOCUMENTATION INDEX)

Tài liệu được phân cấp theo **5 Nhóm Nhiệm Vụ** tương ứng với 5 thành viên từ [`DEVELOPMENT_PLAN.md`](./DEVELOPMENT_PLAN.md).  
Mỗi thư mục đại diện cho một phân hệ độc lập, bao gồm trọn vẹn:
1. **PRD (Product Requirements Document):** Yêu cầu nghiệp vụ, luồng xử lý, đặc tả tính năng và tiêu chí nghiệm thu.
2. **UI Mockup & Wireframe:** Bản vẽ mô phỏng bố cục trực quan bằng sơ đồ ký tự (ASCII Art) cùng danh sách thuộc tính Controls WinForms để kéo thả trong Visual Studio.
3. **System Design:** Thiết kế kỹ thuật 3 tầng (UI -> BLL -> DAL -> SQL Server), cấu trúc DTO, các sự kiện C# và câu lệnh SQL truy vấn thực tế.

---

> [!CAUTION]
> ### ⛔ QUY ĐỊNH BẮT BUỘC: KHÔNG ĐƯỢC VIẾT CODE VÀO THƯ MỤC `Docs/`
> - **Thư mục `Docs/` chỉ là khung tài liệu kế hoạch, đặc tả và thiết kế kỹ thuật để tham khảo.**
> - **TUYỆT ĐỐI KHÔNG** tạo file `.cs`, `.Designer.cs`, `.resx`, form hay file mã nguồn Visual Studio trong thư mục `Docs/`.
> - **Vị trí viết code thực tế:** Mọi mã nguồn của 5 thành viên được triển khai đúng vào solution C# WinForms tại thư mục tương đối [`../qlspdigital/qlspdigital/`](../qlspdigital/qlspdigital/):
>   - `Views/`: Nơi chứa `MainForm.cs` và 15 `UserControl` giao diện phân theo từng nhóm ([`../qlspdigital/qlspdigital/Views/`](../qlspdigital/qlspdigital/Views/)).
>   - `BLL/`: Nơi viết logic nghiệp vụ phân theo từng nhóm ([`../qlspdigital/qlspdigital/BLL/`](../qlspdigital/qlspdigital/BLL/)).
>   - `DAL/`: Nơi viết các truy vấn ADO.NET SQL phân theo từng nhóm ([`../qlspdigital/qlspdigital/DAL/`](../qlspdigital/qlspdigital/DAL/)).
>   - `DTOs/`: Nơi định nghĩa các class DTO truyền dữ liệu giữa các tầng ([`../qlspdigital/qlspdigital/DTOs/`](../qlspdigital/qlspdigital/DTOs/)).
>   - `Shared/`: Nơi lưu các tiện ích dùng chung: kết nối CSDL `DatabaseHelper.cs`, phiên `UserSession.cs`, băm `SecurityHelper.cs` ([`../qlspdigital/qlspdigital/Shared/`](../qlspdigital/qlspdigital/Shared/)).
>   - `Database/`: Nơi lưu trữ kịch bản SQL thực thi trên SSMS ([`../Database/DPMS_Database_Master.sql`](../Database/DPMS_Database_Master.sql)).

---

## Bảng tra cứu phân công File & Thư mục chi tiết cho 5 thành viên (Đường dẫn tương đối)

| STT | Thành viên phụ trách | Phân hệ nhiệm vụ | Thư mục giao diện (Views & Controls) | Thư mục nghiệp vụ (BLL) | Thư mục truy vấn (DAL) | DTOs liên quan | Bảng CSDL thao tác |
|:---:|---|---|---|---|---|---|---|
| **1** | **Lê Nguyễn Minh Trí** | **Store & Reviews**<br>(Cửa hàng, Chi tiết SP & Đánh giá) | [`../qlspdigital/qlspdigital/Views/Nhom1_Store/`](../qlspdigital/qlspdigital/Views/Nhom1_Store/)<br>- `StoreViewControl.cs` (UI 2: Cửa hàng)<br>- `ProductDetailControl.cs` (UI 3: Chi tiết SP)<br>- `ProductReviewControl.cs` (UI 14: Đánh giá)<br>[`../qlspdigital/qlspdigital/Controls/`](../qlspdigital/qlspdigital/Controls/)<br>- `ProductCardControl.cs` (Thẻ sản phẩm) | [`../qlspdigital/qlspdigital/BLL/Nhom1_Store/`](../qlspdigital/qlspdigital/BLL/Nhom1_Store/)<br>- `SanPhamService.cs`<br>  + Class `SanPhamService`<br>  + Class `DanhGiaService` | [`../qlspdigital/qlspdigital/DAL/Nhom1_Store/`](../qlspdigital/qlspdigital/DAL/Nhom1_Store/)<br>- `SanPhamRepository.cs`<br>- `DanhGiaRepository.cs` | [`../qlspdigital/qlspdigital/DTOs/`](../qlspdigital/qlspdigital/DTOs/)<br>- `SanPhamDTO.cs`<br>- `DanhMucDTO.cs`<br>- `ThuVienItemDTO.cs` (`DanhGiaDTO`) | `SanPham`<br>`DanhMuc`<br>`NhaPhatHanh`<br>`DanhGia` |
| **2** | **Nguyễn Quốc Trung** | **Cart & Wallet**<br>(Giỏ hàng, Ví tài khoản & Voucher) | [`../qlspdigital/qlspdigital/Views/Nhom2_Cart/`](../qlspdigital/qlspdigital/Views/Nhom2_Cart/)<br>- `CartControl.cs` (UI 4: Giỏ hàng)<br>- `WalletControl.cs` (UI 5: Ví tài khoản)<br>- `VoucherControl.cs` (UI 15: Kho Voucher) | [`../qlspdigital/qlspdigital/BLL/Nhom2_Cart/`](../qlspdigital/qlspdigital/BLL/Nhom2_Cart/)<br>- `GioHangService.cs`<br>  + Class `GioHangService`<br>  + Class `ViTaiKhoanService`<br>  + Class `MaGiamGiaService`<br>  + Class `ThanhToanService` | [`../qlspdigital/qlspdigital/DAL/Nhom2_Cart/`](../qlspdigital/qlspdigital/DAL/Nhom2_Cart/)<br>- `GioHangRepository.cs`<br>- `ViTaiKhoanRepository.cs`<br>- `MaGiamGiaRepository.cs` | [`../qlspdigital/qlspdigital/DTOs/`](../qlspdigital/qlspdigital/DTOs/)<br>- `GioHangItemDTO.cs`<br>- `DonHangDTO.cs` (`ThanhToanDTO`) | `GioHang`<br>`NguoiDung.SoDuVi`<br>`MaGiamGia`<br>`MaGiamGiaNguoiDung`<br>`ThanhToan` |
| **3** | **Phạm Trung Hiếu** | **Product & Profile**<br>(Quản trị Sản phẩm, Danh mục & Hồ sơ) | [`../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/`](../qlspdigital/qlspdigital/Views/Nhom3_ProductAdmin/)<br>- `ProductManageControl.cs` (UI 10: Quản lý SP)<br>- `CategoryManageControl.cs` (UI 11: Quản lý DM)<br>- `ProfileControl.cs` (UI 8: Hồ sơ cá nhân) | [`../qlspdigital/qlspdigital/BLL/Nhom3_ProductAdmin/`](../qlspdigital/qlspdigital/BLL/Nhom3_ProductAdmin/)<br>- `SanPhamAdminService.cs`<br>  + Class `SanPhamAdminService`<br>  + Class `DanhMucService`<br>  + Class `HoSoService` | [`../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/`](../qlspdigital/qlspdigital/DAL/Nhom3_ProductAdmin/)<br>- `SanPhamAdminRepository.cs`<br>- `DanhMucRepository.cs`<br>- `HoSoRepository.cs`<br>  + Chứa class `TepSanPhamRepository` | [`../qlspdigital/qlspdigital/DTOs/`](../qlspdigital/qlspdigital/DTOs/)<br>- `SanPhamDTO.cs` (`TepSanPhamDTO`)<br>- `DanhMucDTO.cs`<br>- `NguoiDungDTO.cs` | `SanPham`<br>`TepSanPham`<br>`DanhMuc`<br>`NguoiDung`<br>`CaiDatNguoiDung` |
| **4** | **Trần Quốc Thuận** | **Auth & Dashboard**<br>(Xác thực, Quản lý tài khoản & Thống kê) | [`../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/`](../qlspdigital/qlspdigital/Views/Nhom4_SystemAdmin/)<br>- `LoginControl.cs` (UI 1: Đăng nhập)<br>- `RegisterControl.cs` (UI 1: Đăng ký)<br>- `UserManageControl.cs` (UI 12: Quản lý User)<br>- `DashboardControl.cs` (UI 9: Dashboard) | [`../qlspdigital/qlspdigital/BLL/Nhom4_SystemAdmin/`](../qlspdigital/qlspdigital/BLL/Nhom4_SystemAdmin/)<br>- `AuthService.cs`<br>  + Class `AuthService`<br>  + Class `NguoiDungService`<br>  + Class `DashboardService` | [`../qlspdigital/qlspdigital/DAL/Nhom4_SystemAdmin/`](../qlspdigital/qlspdigital/DAL/Nhom4_SystemAdmin/)<br>- `AuthRepository.cs`<br>- `NguoiDungRepository.cs`<br>  + Chứa class `DashboardRepository` | [`../qlspdigital/qlspdigital/DTOs/`](../qlspdigital/qlspdigital/DTOs/)<br>- `NguoiDungDTO.cs`<br>- `ThuVienItemDTO.cs`<br>  (`DashboardStatsDTO`, `TopSanPhamDTO`) | `NguoiDung`<br>`DonHang`<br>`ChiTietDonHang`<br>`SanPham` |
| **5** | **Phạm Văn Đam** | **Orders & Library**<br>(Quản lý đơn hàng & Kho thư viện số) | [`../qlspdigital/qlspdigital/Views/Nhom5_Orders/`](../qlspdigital/qlspdigital/Views/Nhom5_Orders/)<br>- `OrderManageControl.cs` (UI 13: Quản lý đơn)<br>- `OrderHistoryControl.cs` (UI 7: Lịch sử đơn)<br>- `LibraryControl.cs` (UI 6: Thư viện số) | [`../qlspdigital/qlspdigital/BLL/Nhom5_Orders/`](../qlspdigital/qlspdigital/BLL/Nhom5_Orders/)<br>- `DonHangService.cs`<br>  + Class `DonHangService`<br>  + Class `ThuVienService` | [`../qlspdigital/qlspdigital/DAL/Nhom5_Orders/`](../qlspdigital/qlspdigital/DAL/Nhom5_Orders/)<br>- `DonHangRepository.cs`<br>  + Chứa class `ThuVienRepository` | [`../qlspdigital/qlspdigital/DTOs/`](../qlspdigital/qlspdigital/DTOs/)<br>- `DonHangDTO.cs`<br>  (`ChiTietDonHangDTO`)<br>- `ThuVienItemDTO.cs` | `DonHang`<br>`ChiTietDonHang`<br>`SoHuuSanPham`<br>`TepSanPham` |


---

## Cây thư mục tài liệu

```text
Docs/
├── DEVELOPMENT_PLAN.md                     # Kế hoạch triển khai tổng thể & kiến trúc khung sườn MainForm
├── README.md                               # (Tài liệu này) Quy chuẩn làm việc, tra cứu CSDL & mục lục
│
├── 01_Store_ProductDetails/                # [NHIỆM VỤ 1] Cửa hàng, Chi tiết sản phẩm & Đánh giá (Reviews)
│   ├── PRD.md                              # Đặc tả yêu cầu: Store, Product Details & Đánh giá sao (3 UI)
│   ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 2 (Store), UI 3 (ProductDetail) & UI 14 (Reviews)
│   └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, DTO, Sự kiện C# & SQL truy vấn Reviews
│
├── 02_Cart_Wallet/                         # [NHIỆM VỤ 2] Giỏ hàng, Ví tài khoản (Steam Wallet) & Mã giảm giá
│   ├── PRD.md                              # Đặc tả yêu cầu: Giỏ hàng, Nạp tiền ví & Quản lý Voucher (3 UI)
│   ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 4 (Cart), UI 5 (Wallet) & UI 15 (Vouchers)
│   └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, DTO, Kiểm tra số dư, Chiết khấu & Transaction SQL ví
│
├── 03_Product_Category_Profile/            # [NHIỆM VỤ 3] Quản trị Sản phẩm, Danh mục & Hồ sơ
│   ├── PRD.md                              # Đặc tả yêu cầu: CRUD Hàng hóa, Thể loại & User Profile (3 UI)
│   ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 10 (ProductMgmt), UI 11 (CategoryMgmt), UI 8 (Profile)
│   └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, Form nhập liệu & Lệnh CRUD SQL
│
├── 04_Auth_User_Dashboard/                 # [NHIỆM VỤ 4] Xác thực, Phân quyền & Quản trị thống kê
│   ├── PRD.md                              # Đặc tả yêu cầu: Đăng nhập/Đăng ký, Quản lý tài khoản & Thống kê (3 UI)
│   ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 1 (Auth), UI 12 (UserMgmt), UI 9 (Dashboard)
│   └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, Logic phân quyền Session & SQL thống kê SUM/COUNT
│
└── 05_Orders_Library/                      # [NHIỆM VỤ 5] Quản lý Đơn hàng & Kho thư viện số
    ├── PRD.md                              # Đặc tả yêu cầu: Duyệt đơn, Lịch sử mua hàng & Tải file sở hữu (3 UI)
    ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 13 (OrderAdmin), UI 7 (OrderHistory), UI 6 (MyLibrary)
    └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, Quản lý trạng thái đơn & Quyền sở hữu file
```

---

## Mô hình Cơ sở Dữ liệu & Kịch bản T-SQL tổng quát

Toàn bộ hệ thống cơ sở dữ liệu đã được chuẩn hóa và viết sẵn thành kịch bản T-SQL tổng quát có thể **sao chép trực tiếp vào SQL Server Management Studio (SSMS) để thực thi chỉ với 1 cú click F5**:

- **Vị trí file script:** [`../Database/DPMS_Database_Master.sql`](../Database/DPMS_Database_Master.sql)

### Bảng tổng hợp Thực thể & Mối quan hệ trong CSDL (Chuẩn PascalCase Tiếng Việt CNTT):

| STT | Tên bảng | Khóa chính (PK) | Khóa ngoại (FK) & Liên kết | Mục đích sử dụng | Phân hệ phụ trách |
|:---:|---|---|---|---|---|
| 1 | `NguoiDung` | `MaNguoiDung` (IDENTITY) | *Không có* | Lưu tài khoản, mật khẩu hash, quyền (`QuanTriVien`/`KhachHang`) và số dư ví `SoDuVi`. | Nhóm 4 / Nhóm 3 / Nhóm 2 |
| 2 | `DanhMuc` | `MaDanhMuc` (IDENTITY) | *Không có* | Danh mục phân loại sản phẩm số (Game, Phần mềm, Mô hình 3D, Đồ họa). | Nhóm 3 |
| 3 | `NhaPhatHanh` | `MaNhaPhatHanh` (IDENTITY) | *Không có* | Nhà phát hành / Studio sáng tạo sản phẩm. | Nhóm 1 / Nhóm 3 |
| 4 | `SanPham` | `MaSanPham` (IDENTITY) | `MaDanhMuc` -> `DanhMuc`<br>`MaNhaPhatHanh` -> `NhaPhatHanh` | Thông tin sản phẩm số, tiêu đề, giá bán, phiên bản, dung lượng, đường dẫn ảnh. | Nhóm 1 / Nhóm 3 |
| 5 | `TepSanPham` | `MaTep` (IDENTITY) | `MaSanPham` -> `SanPham` | Siêu dữ liệu tệp số (tên file, đường dẫn tương đối lưu file vật lý, định dạng `.zip`/`.exe`, kích thước byte, mã SHA256). | Nhóm 3 / Nhóm 5 |
| 6 | `GioHang` | `MaGioHang` (IDENTITY) | `MaNguoiDung` -> `NguoiDung`<br>`MaSanPham` -> `SanPham` | Giỏ hàng tạm thời của khách hàng (ràng buộc `UNIQUE(MaNguoiDung, MaSanPham)`). | Nhóm 2 |
| 7 | `MaGiamGia` | `MaGiamGiaId` (IDENTITY) | `MaNhaPhatHanh` -> `NhaPhatHanh` (NULL) | Mã ưu đãi: toàn hệ thống (NULL) hoặc theo Studio, loại giảm (`PhanTram`/`TienMat`), đơn tối thiểu, hạn dùng. | Nhóm 2 |
| 8 | `MaGiamGiaNguoiDung` | `MaNguoiDungVoucherId` (IDENTITY) | `MaNguoiDung` -> `NguoiDung`<br>`MaGiamGiaId` -> `MaGiamGia` | Kho voucher cá nhân đã thu thập, trạng thái `DaSuDung`, ngày dùng. | Nhóm 2 |
| 9 | `DonHang` | `MaDonHang` (IDENTITY) | `MaNguoiDung` -> `NguoiDung`<br>`MaGiamGiaId` -> `MaGiamGia` (NULL) | Đơn hàng mua sắm, mã hóa đơn `MaDonHangCode`, tạm tính, tiền giảm giá, tổng thanh toán, trạng thái. | Nhóm 2 / Nhóm 5 |
| 10 | `ChiTietDonHang` | `MaChiTietDonHang` (IDENTITY) | `MaDonHang` -> `DonHang`<br>`MaSanPham` -> `SanPham` | Chi tiết mặt hàng trong đơn, số lượng, lưu đơn giá tại thời điểm mua. | Nhóm 5 |
| 11 | `ThanhToan` | `MaThanhToan` (IDENTITY) | `MaNguoiDung` -> `NguoiDung`<br>`MaDonHang` -> `DonHang` (NULL) | Lịch sử nạp tiền vào ví ảo hoặc trừ tiền thanh toán đơn hàng. | Nhóm 2 |
| 12 | `SoHuuSanPham` | `MaSoHuu` (IDENTITY) | `MaNguoiDung` -> `NguoiDung`<br>`MaSanPham` -> `SanPham` | Kho game/sản phẩm đã sở hữu vĩnh viễn sau thanh toán, ngày mua, số lần tải. | Nhóm 5 |
| 13 | `DanhGia` | `MaDanhGia` (IDENTITY) | `MaNguoiDung` -> `NguoiDung`<br>`MaSanPham` -> `SanPham` | Đánh giá sao (1-5★) và nhận xét của khách hàng cho sản phẩm (Ràng buộc `UNIQUE(MaNguoiDung, MaSanPham)`). | Nhóm 1 |
| 14 | `CaiDatNguoiDung` | `MaCaiDat` (IDENTITY) | `MaNguoiDung` -> `NguoiDung` | Tùy chọn cài đặt người dùng (Thông báo email, ghi nhớ đăng nhập, cập nhật tự động, xác nhận thanh toán). | Nhóm 3 |

---

## Hướng dẫn quy trình 4 bước cho từng thành viên

1. **Bước 1 - Khởi tạo CSDL:** Mở SSMS, mở file [`../Database/DPMS_Database_Master.sql`](../Database/DPMS_Database_Master.sql) và nhấn `F5` để tạo CSDL kèm dữ liệu mẫu.
2. **Bước 2 - Đọc PRD:** Mở file `PRD.md` trong thư mục nhiệm vụ của mình để nắm rõ luồng nghiệp vụ và kịch bản vấn đáp.
3. **Bước 3 - Xem UI Mockup & Tạo giao diện:** Mở file `UI_MOCKUP.md`, vào Visual Studio trong project `DPMS.WinForms/Views/...` tạo `UserControl` và kéo thả Controls theo đúng danh sách thuộc tính gợi ý.
4. **Bước 4 - Lập trình 3 tầng:** Mở `SYSTEM_DESIGN.md`, tạo DTO trong `DPMS.Shared`, viết truy vấn ADO.NET trong `DPMS.DAL`, viết xử lý nghiệp vụ trong `DPMS.BLL`, và gán sự kiện cho các nút trên `UserControl`.

