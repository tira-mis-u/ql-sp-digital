# BẢNG MA TRẬN TRUY VẾT TỔNG THỂ NÚT BẤM & HÀM XỬ LÝ (MASTER TRACEABILITY MATRIX)
**Dự án:** DPMS (Digital Product Management System)  
**Phạm vi:** 100% các nút bấm trên Khung sườn `MainForm` và 15 màn hình `UserControl` của 5 thành viên.

---

## 📊 BẢNG MA TRẬN TRUY VẾT TOÀN DIỆN (END-TO-END CALL FLOW)

| STT | Màn hình (UI) | Tên Nút / Control ID | Hàm Bắt Sự Kiện (UI Event Handler) | Nơi Gọi (Caller) | Hàm BLL Được Gọi (Service) | Hàm DAL Được Gọi (Repository) | Lệnh SQL / CSDL Thực Thi | Sự Kiện Bắn Ra & Giao Diện Bị Ảnh Hưởng |
|:---:|---|---|---|---|---|---|---|---|
| **01** | `MainForm` | `btnMenuStore` | `BtnMenuStore_Click` | Click menu sidebar | *(Điều hướng)* | *(Không)* | *(Không)* | Nạp `StoreViewControl` vào `panelMainContent` |
| **02** | `MainForm` | `btnMenuReviews` | `BtnMenuReviews_Click` | Click menu sidebar | *(Điều hướng)* | *(Không)* | *(Không)* | Nạp `ProductReviewControl` |
| **03** | `MainForm` | `btnMenuCart` | `BtnMenuCart_Click` | Click menu sidebar / nút cart | `EnsureLoggedIn` | `GioHangRepository.LayGioHang` | `SELECT ... FROM GioHang` | Nạp `CartControl` |
| **04** | `MainForm` | `btnMenuWallet` | `BtnMenuWallet_Click` / `LblBalance_Click` | Click menu sidebar / click số dư | `EnsureLoggedIn` | `ViTaiKhoanRepository.LaySoDuVi` | `SELECT SoDuVi FROM NguoiDung` | Nạp `WalletControl` |
| **05** | `MainForm` | `btnMenuVouchers` | `BtnMenuVouchers_Click` | Click menu sidebar | `EnsureLoggedIn` | `MaGiamGiaRepository.LayDanhSachVoucher` | `SELECT ... FROM MaGiamGia` | Nạp `VoucherControl` |
| **06** | `MainForm` | `btnMenuLibrary` | `BtnMenuLibrary_Click` | Click menu sidebar | `EnsureLoggedIn` | `ThuVienRepository.LayKhoThuVien` | `SELECT ... FROM SoHuuSanPham` | Nạp `LibraryControl` |
| **07** | `MainForm` | `btnMenuOrderHistory`| `BtnMenuOrderHistory_Click` | Click menu sidebar | `EnsureLoggedIn` | `DonHangRepository.LayLichSuDonHang` | `SELECT ... FROM DonHang` | Nạp `OrderHistoryControl` |
| **08** | `MainForm` | `btnMenuProfile` | `BtnMenuProfile_Click` | Click menu sidebar | `EnsureLoggedIn` | `HoSoRepository.LayThongTinHoSo` | `SELECT ... FROM NguoiDung` | Nạp `ProfileControl` |
| **09** | `MainForm` | `btnMenuDashboard` | `BtnMenuDashboard_Click` | Click menu sidebar | `EnsureAdmin` | `DashboardRepository.LayChiSoTongHop` | `SELECT SUM, COUNT ...` | Nạp `DashboardControl` (Chỉ Admin) |
| **10** | `MainForm` | `btnMenuProductManage`| `BtnMenuProductManage_Click`| Click menu sidebar | `EnsureAdmin` | `SanPhamAdminRepository.LayTatCa` | `SELECT ... FROM SanPham` | Nạp `ProductManageControl` (Chỉ Admin) |
| **11** | `MainForm` | `btnMenuCategoryManage`| `BtnMenuCategoryManage_Click`| Click menu sidebar | `EnsureAdmin` | `DanhMucRepository.LayTatCaDanhMuc`| `SELECT ... FROM DanhMuc` | Nạp `CategoryManageControl` (Chỉ Admin) |
| **12** | `MainForm` | `btnMenuOrderManage` | `BtnMenuOrderManage_Click` | Click menu sidebar | `EnsureAdmin` | `DonHangRepository.LayTatCaDonHang` | `SELECT ... FROM DonHang` | Nạp `OrderManageControl` (Chỉ Admin) |
| **13** | `MainForm` | `btnMenuUserManage` | `BtnMenuUserManage_Click` | Click menu sidebar | `EnsureAdmin` | `NguoiDungRepository.LayDanhSach` | `SELECT ... FROM NguoiDung` | Nạp `UserManageControl` (Chỉ Admin) |
| **14** | `MainForm` | `btnHeaderAuth` | `BtnHeaderAuth_Click` | Click nút góc phải Header | `AuthService.DangXuat` | *(Không)* | *(Không)* | Bắn `UserSession.SessionChanged`, nạp `LoginControl` hoặc `StoreViewControl` |
| **15** | UI 2 (`StoreView`) | `btnSearch` | `BtnSearch_Click` / Enter | Người dùng tìm kiếm | `SanPhamService.LayDanhSachSanPham`| `SanPhamRepository.LayDanhSach` | `SELECT ... FROM SanPham WHERE DangKinhDoanh=1` | Vẽ lại các thẻ `ProductCardControl` |
| **16** | UI 2 (`StoreView`) | `btnRefresh` | `BtnRefresh_Click` | Bấm Làm mới Store | `SanPhamService.LayDanhSachSanPham`| `SanPhamRepository.LayDanhSach` | `SELECT ...` | Xóa ô tìm kiếm, nạp lại toàn bộ game |
| **17** | UI 2 (`Card`) | `btnAddToCart` | `BtnAddToCart_Click` | Bấm mua trên thẻ game | `GioHangService.ThemVaoGioHang` | `ThuVienRepo.DaSoHuu` & `GioHangRepo` | `INSERT INTO GioHang` | Bắn `GioHangService.CartUpdated` $\to$ Nhảy Badge Header |
| **18** | UI 2 (`Card`) | `btnDetail` | `BtnDetail_Click` | Bấm chi tiết trên thẻ | `OnDetailClicked` callback | `MainForm.NavigateToProductDetail` | *(Không)* | Chuyển sang màn hình UI 3 |
| **19** | UI 3 (`Detail`) | `btnAddToCart` | `BtnAddToCart_Click` | Bấm mua trong trang chi tiết | `GioHangService.ThemVaoGioHang` | `ThuVienRepo.DaSoHuu` & `GioHangRepo` | `INSERT INTO GioHang` | Bắn `GioHangService.CartUpdated` |
| **20** | UI 3 (`Detail`) | `btnBack` | `BtnBack_Click` | Bấm quay lại Store | `OnBackToStore` callback | `MainForm.NavigateToStore` | *(Không)* | Quay về màn hình UI 2 |
| **21** | UI 3 (`Detail`) | `btnReviews` | `BtnReviews_Click` | Bấm xem đánh giá | `OnViewReviews` callback | `MainForm.NavigateToReviews` | *(Không)* | Chuyển sang màn hình UI 14 |
| **22** | UI 14 (`Reviews`) | `btnSubmit` | `BtnSubmit_Click` | Bấm gửi/sửa nhận xét | `DanhGiaService.LuuDanhGia` | `ThuVienRepo.DaSoHuu` & `DanhGiaRepo` | `UPSERT INTO DanhGia` & `UPDATE SanPham.DiemTrungBinh` | Bắn `SanPhamService.ReviewUpdated` $\to$ Đổi sao tức thì |
| **23** | UI 14 (`Reviews`) | `btnDelete` | `BtnDelete_Click` | Bấm xóa nhận xét của tôi | `DanhGiaService.XoaDanhGia` | `DanhGiaRepository.XoaDanhGia` | `DELETE FROM DanhGia` & `UPDATE DiemTrungBinh` | Bắn `ReviewUpdated`, xóa sạch comment của user |
| **24** | UI 4 (`Cart`) | `btnCheckout` | `BtnCheckout_Click` | Bấm Thanh toán 1-Click | `ThanhToanService.ThanhToanGioHang`| `DonHangRepository.ThucHienThanhToan` | **SqlTransaction 8 bước (Trừ ví, tạo đơn, cấp thư viện, xóa giỏ)** | Bắn `BalanceChanged`, `CartUpdated`, `LibraryUpdated`, `VoucherUpdated` |
| **25** | UI 4 (`Cart`) | `btnApplyVoucher`| `BtnApplyVoucher_Click` | Bấm áp dụng mã code | `MaGiamGiaService.KiemTraVaApDung` | `MaGiamGiaRepository.KiemTraVaApDung`| `SELECT ... FROM MaGiamGia / NguoiDung`| Tính tiền giảm có trần, đổi `lblTotalAmount` |
| **26** | UI 4 (`Cart`) | `btnRemoveItem` | `BtnRemoveItem_Click` | Bấm xóa 1 món giỏ | `GioHangService.XoaKhoiGioHang` | `GioHangRepository.XoaKhoiGioHang` | `DELETE FROM GioHang WHERE MaGioHang=@Id` | Bắn `CartUpdated`, tính lại tổng tiền giỏ |
| **27** | UI 4 (`Cart`) | `btnClearCart` | `BtnClearCart_Click` | Bấm xóa sạch giỏ | `GioHangService.XoaKhoiGioHang` | `GioHangRepository` | `DELETE FROM GioHang WHERE MaNguoiDung=@Uid` | Bắn `CartUpdated`, giỏ về 0 |
| **28** | UI 4 (`Cart`) | `btnDeposit` | `BtnDeposit_Click` | Bấm nạp tiền từ giỏ | `OnNavigateToWallet` callback | `MainForm.NavigateToWallet` | *(Không)* | Chuyển sang màn hình UI 5 |
| **29** | UI 4 (`Cart`) | `btnOpenVouchers`| `BtnOpenVouchers_Click` | Bấm tìm voucher | `OnNavigateToVouchers` callback | `MainForm.NavigateToVouchers` | *(Không)* | Chuyển sang màn hình UI 15 |
| **30** | UI 5 (`Wallet`) | `btnTopUp50k..500k`| `BtnTopUp*_Click` | Bấm nút nạp nhanh | `ViTaiKhoanService.NapTien` | `ViTaiKhoanRepository.NapTienVi` | `UPDATE NguoiDung.SoDuVi` & `INSERT ThanhToan` | Bắn `UserSession.BalanceChanged` $\to$ Cập nhật ví Header |
| **31** | UI 5 (`Wallet`) | `btnCustomTopUp` | `BtnCustomTopUp_Click` | Bấm nạp tùy chọn | `ViTaiKhoanService.NapTien` | `ViTaiKhoanRepository.NapTienVi` | `UPDATE NguoiDung.SoDuVi` & `INSERT ThanhToan` | Bắn `UserSession.BalanceChanged` |
| **32** | UI 15 (`Vouchers`) | `btnCollect` | `BtnCollect_Click` | Bấm thu thập voucher | `MaGiamGiaService.ThuThapVoucher` | `MaGiamGiaRepository.ThuThapVoucher` | `INSERT MaGiamGiaNguoiDung` & `UPDATE SoLuongConLai` | Bắn `MaGiamGiaService.VoucherUpdated` |
| **33** | UI 15 (`Vouchers`) | `btnApplySelected`| `BtnApplySelected_Click` | Bấm dùng ngay cho giỏ | `OnNavigateToCartWithVoucher` | `MainForm.NavigateToCart(code)` | *(Không)* | Chuyển sang giỏ, điền sẵn code |
| **34** | UI 15 (`Vouchers`) | `btnDelete` | `BtnDelete_Click` | Bấm bỏ lưu voucher | `MaGiamGiaService.XoaVoucherCaNhan`| `MaGiamGiaRepository.XoaVoucher` | `DELETE FROM MaGiamGiaNguoiDung` | Bắn `MaGiamGiaService.VoucherUpdated` |
| **35** | UI 10 (`ProdAdmin`)| `btnBrowseFile` | `BtnBrowseFile_Click` | Bấm chọn tệp số upload | `SecurityHelper.ComputeFileSha256` | `System.IO.FileStream` | *(Đọc file đĩa OS)* | Hiển thị DungLuongMB & Hash SHA-256 |
| **36** | UI 10 (`ProdAdmin`)| `btnSave` | `BtnSave_Click` | Bấm Lưu sản phẩm | `SanPhamAdminService.Them/CapNhat` | `SanPhamAdminRepo` & `TepSanPhamRepo`| `INSERT/UPDATE SanPham` & `TepSanPham` | Bắn `ProductCatalogChanged` $\to$ Store nạp lại game |
| **37** | UI 10 (`ProdAdmin`)| `btnDelete` | `BtnDelete_Click` | Bấm Ngừng bán (Soft delete) | `SanPhamAdminService.DoiTrangThai` | `SanPhamAdminRepository` | `UPDATE SanPham SET DangKinhDoanh=~DangKinhDoanh` | Bắn `ProductCatalogChanged` |
| **38** | UI 11 (`CatAdmin`) | `btnSave` | `BtnSave_Click` | Bấm Lưu danh mục | `DanhMucService.Them/CapNhat` | `DanhMucRepository` | `INSERT/UPDATE DanhMuc` | Bắn `CategoryCatalogChanged` $\to$ Cập nhật bộ lọc Store |
| **39** | UI 11 (`CatAdmin`) | `btnDelete` | `BtnDelete_Click` | Bấm Xóa danh mục | `DanhMucService.XoaDanhMuc` | `DanhMucRepository.XoaDanhMuc` | `DELETE FROM DanhMuc WHERE MaDanhMuc=@Id` | Bắn `CategoryCatalogChanged` |
| **40** | UI 8 (`Profile`) | `btnUpdateProfile`| `BtnUpdateProfile_Click` | Bấm Sửa thông tin | `HoSoRepository.CapNhatHoSo` | `HoSoRepository` | `UPDATE NguoiDung SET HoTen, Email, SDT` | Bắn `UserSession.UpdateProfile` $\to$ Đổi tên Header |
| **41** | UI 8 (`Profile`) | `btnChangePassword`| `BtnChangePassword_Click` | Bấm Đổi mật khẩu | `HoSoRepository.DoiMatKhau` | `HoSoRepository` | `UPDATE NguoiDung SET MatKhauHash=@NewHash` | Thông báo thành công |
| **42** | UI 8 (`Profile`) | `btnSaveSettings` | `BtnSaveSettings_Click` | Bấm Lưu cài đặt UX | `HoSoRepository.LuuCaiDat` | `HoSoRepository` | `UPSERT INTO CaiDatNguoiDung` | Lưu cờ `XacNhanThanhToan` cho lần mua sau |
| **43** | UI 1 (`Login`) | `btnLogin` | `BtnLogin_Click` / Enter | Bấm Đăng nhập | `AuthService.DangNhap` | `AuthRepository.DangNhap` | `SELECT ... FROM NguoiDung WHERE Hash=@Hash` | Bắn `UserSession.Login` $\to$ Bật sáng menu Admin/User |
| **44** | UI 1.1 (`Register`)| `btnRegister` | `BtnRegister_Click` | Bấm Đăng ký | `AuthService.DangKy` | `AuthRepository.DangKy` | `INSERT INTO NguoiDung (VaiTro='KhachHang')` | Chuyển sang màn hình Đăng nhập |
| **45** | UI 12 (`UserAdmin`)| `btnLock` | `BtnLock_Click` | Bấm Khóa tài khoản | `NguoiDungService.KhoaTaiKhoan` | `NguoiDungRepository` | `UPDATE NguoiDung SET TrangThai=0` | Kiểm tra **Anti-Lockout Rule**, khóa tài khoản vi phạm |
| **46** | UI 12 (`UserAdmin`)| `btnUnlock` | `BtnUnlock_Click` | Bấm Mở khóa | `NguoiDungService.MoKhoaTaiKhoan`| `NguoiDungRepository` | `UPDATE NguoiDung SET TrangThai=1` | Mở lại quyền truy cập cho user |
| **47** | UI 9 (`Dashboard`) | `btnRefresh` | `BtnRefresh_Click` | Bấm Làm mới KPI | `DashboardService.LayChiSo` | `DashboardRepository` | `SELECT SUM(TongThanhToan), TOP 5 ...` | Vẽ lại 4 Card KPI và Bảng Top 5 bán chạy |
| **48** | UI 13 (`OrderAdmin`)| `btnApprove` | `BtnApprove_Click` | Bấm Duyệt / Kích hoạt bù | `DonHangService.DuyetDonHang` | `DonHangRepository.CapNhatTrangThai` | `UPDATE DonHang` & **`INSERT SoHuuSanPham`** | Bắn `ThuVienService.LibraryUpdated` $\to$ Game vào thư viện |
| **49** | UI 13 (`OrderAdmin`)| `btnCancelOrder` | `BtnCancelOrder_Click` | Bấm Hủy đơn hoàn tiền | `DonHangService.HuyDonHang` | `DonHangRepository` | `UPDATE DonHang='Đã hủy'`, **Hoàn tiền `SoDuVi`** | Bắn `LibraryUpdated` & `BalanceChanged` |
| **50** | UI 6 (`Library`) | `btnDownload` | `BtnDownload_Click` | Bấm Tải xuống | `ThuVienService.TaiXuongTepAsync` | `ThuVienRepository.CapNhatLanTai` | `UPDATE SoHuuSanPham (SoLanTai=SoLanTai+1)` | Chạy nén **PK-Zip bất đồng bộ, băm SHA-256, sinh `LICENSE_AND_KEY.txt`** |
| **51** | UI 6 (`Library`) | `btnUninstall` | `BtnUninstall_Click` | Bấm Gỡ cài đặt | `ThuVienService.GoKhoiThuVien` | `ThuVienRepository.GoKhoiThuVien` | `DELETE FROM SoHuuSanPham WHERE MaSoHuu=@Id` | Gỡ file máy khách, xóa quyền thư viện cá nhân |
