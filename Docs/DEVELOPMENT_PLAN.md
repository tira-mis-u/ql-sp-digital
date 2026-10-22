# KẾ HOẠCH TRIỂN KHAI BÀI TẬP LỚN (DEVELOPMENT PLAN)
**Học phần:** Lập trình trực quan C#  
**Dự án:** Digital Product Management System (DPMS)  
**Quy mô nhóm:** 5 thành viên  

---

## 1. Mục tiêu và Định hướng của Plan

Tài liệu này được lập ra nhằm phục vụ quá trình thực hiện bài tập lớn của nhóm:
- **Thống nhất kiến trúc hiển thị:** Thống nhất mô hình Khung sườn ứng dụng (`MainForm` + `UserControl`) để cả nhóm làm việc độc lập mà không bị rời rạc giao diện.
- **Phân công theo module nghiệp vụ:** 13 giao diện được chia theo các luồng nghiệp vụ liền mạch, giúp mỗi thành viên phụ trách trọn vẹn một mảng để thuận tiện báo cáo và bảo vệ đồ án trước giảng viên.
- **Quy chuẩn mã nguồn & Tránh xung đột:** Phân chia thư mục độc lập để khi 5 người cùng đẩy code lên Git không bị xung đột (conflict) mã giao diện.

---

## 2. Kiến trúc giao diện: Mô hình Khung sườn (Application Shell)

Toàn bộ ứng dụng sử dụng một cửa sổ duy nhất (`MainForm`), lấy cảm hứng từ các nền tảng phân phối số như Steam.

### 2.1. Sơ đồ bố cục tổng thể
```text
+-----------------------------------------------------------------------------------------------+
|  [Logo DPMS]   CỬA HÀNG SẢN PHẨM KỸ THUẬT SỐ       [Khách/User] [☀️ Sáng / 🌙 Tối] [🚪 Đăng xuất] [💰 Ví: 0đ]   |  <- HEADER
+-----------------------+-----------------------------------------------------------------------+
|                       |                                                                       |
|  ▶️ Cửa hàng (Store)  |                   VÙNG NỘI DUNG CHÍNH                                 |
|  🔘 Đánh giá (Reviews)|                   (panelMainContent)                                  |
|  🔘 Giỏ hàng (Cart)   |                                                                       |
|  🔘 Ví của tôi (Wallet)|  => Khi bấm menu bên trái, UserControl của từng người                 |
|  🔘 Khuyến mãi (Voucher)|    sẽ được tự động nạp và hiển thị tràn đầy vào đây.                 |
|  🔘 Thư viện (Library)|     Nút được chọn sẽ kích hoạt trạng thái [ACTIVE] nổi bật.           |
|  🔘 Lịch sử đơn hàng  |                                                                       |
|  🔘 Quản trị (Admin)  |                                                                       |
|  🔘 Cài đặt cá nhân   |                                                                       |
|                       |                                                                       |
|  SIDEBAR (Cố định)    |                                                                       |
+-----------------------+-----------------------------------------------------------------------+
|  Trạng thái hệ thống: Sẵn sàng | Phiên bản 1.0.0                                              |  <- STATUSBAR
+-----------------------------------------------------------------------------------------------+
```

### 2.2. Cách thức hoạt động
1. **Khung sườn chính (`MainForm.cs`):** 
   - Đóng vai trò làm khung vỏ (Shell) chuẩn Steam Dark Theme.
   - **Thanh Header:** Chứa Logo, tiêu đề ứng dụng, lời chào tài khoản (mặc định hiển thị `"Khách"`, sau khi đăng nhập hiển thị tên người dùng), nút **Chuyển đổi giao diện Sáng / Tối** (`btnThemeToggle`), nút **Đăng nhập / Đăng xuất** (`btnHeaderAuth`), và số dư ví `lblBalance` ở góc phải ngoài cùng (hỗ trợ click chuột để chuyển nhanh sang Ví).
   - **Cơ chế Dual Theme Engine (`ThemeHelper.cs`):** Quản lý trạng thái Dark/Light mode toàn cục, lưu cấu hình vào `theme.cfg`, cập nhật tiêu đề/viền cửa sổ qua DWM native API (`DwmSetWindowAttribute`), và duyệt đệ quy cây điều khiển (`ApplyThemeToControlTree`) để chuyển đổi bảng màu mượt mà.
   - **Tương thích Visual Studio Designer & DPI Scaling:** Chuẩn hóa `AutoScaleMode` và cấu hình DPI trong `app.manifest` (`DpiUnawareGdiScaled`) đảm bảo hiển thị sắc nét, không bị vỡ layout, tràn chữ hoặc mất tương thích giữa các máy tính khác nhau.
   - **Thanh Sidebar (Cơ chế Active/Selected State):**
     + Quản lý tập trung qua phương thức `SetActiveMenu(Button? btn)`.
     + Khi người dùng chuyển sang bất kỳ màn hình nào, nút Sidebar tương ứng sẽ được kích hoạt trạng thái **Selected (Active)** với màu nền xanh điểm nhấn (`Color.FromArgb(49, 98, 130)` / `#316282`) cùng phông chữ đậm màu trắng (`Font = FontStyle.Bold`, `ForeColor = Color.White`).
     + Các nút không được chọn (Inactive) tự động hoàn nguyên màu nền tối (`#121923`) và chữ thường. Sự kiện rê chuột (`MouseEnter`/`MouseLeave`) giữ nguyên màu cho nút đang Active và chỉ tạo hiệu ứng hover nhẹ (`#2A475E`) cho các nút Inactive.
   - **Đồng bộ Dark Mode Scrollbar & Titlebar (Tích hợp trong `MainForm.Designer.cs`):**
     + Tích hợp Win32 API native `SetWindowTheme(ctrl.Handle, "DarkMode_Explorer", null)` và `DwmSetWindowAttribute` (`DWMWA_USE_IMMERSIVE_DARK_MODE`).
     + Toàn bộ thanh cuộn của Form, Panel, DataGridView, FlowLayoutPanel và TextBox đều hiển thị theme tối hiện đại chuẩn Windows 10/11, xóa bỏ thanh cuộn màu trắng thô ráp.
   - Chứa một `Panel` trung tâm đặt tên là `panelMainContent` (thuộc tính `Dock = DockStyle.Fill`).
2. **Các màn hình chức năng (`UserControl`):**
   - **15 màn hình chức năng** của 5 thành viên (mỗi người 3 màn hình độc lập) được thiết kế dưới dạng **`UserControl`** thay vì từng `Form` riêng lẻ trôi nổi.
   - Khi bấm nút trên Sidebar, `UserControl` tương ứng sẽ được nạp trực tiếp vào `panelMainContent`.
   - Màn hình Đánh giá sản phẩm (`ProductReviewControl`) không cần nút quay lại cửa hàng vì người dùng có thể chuyển đổi tức thì giữa các phân hệ thông qua Sidebar cố định.

### 2.3. Mã nguồn khởi chạy & Chuyển đổi màn hình động
Trong file `MainForm.cs` (namespace thực tế: `qlspdigital.Views.Main`):

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.Shared;          // ThemeHelper, UserSession, FormatHelper, DatabaseHelper
using qlspdigital.Views.Nhom1_Store;
using qlspdigital.Views.Nhom2_Cart;
using qlspdigital.Views.Nhom3_ProductAdmin;
using qlspdigital.Views.Nhom4_SystemAdmin;
using qlspdigital.Views.Nhom5_Orders;
using qlspdigital.BLL.Nhom2_Cart; // GioHangService (để cập nhật badge giỏ hàng)

namespace qlspdigital.Views.Main
{
    public partial class MainForm : Form
    {
        private Button? _activeMenuButton = null;

        public MainForm()
        {
            InitializeComponent();
        }

        // Tự động mở Trang chủ Cửa hàng khi vừa bật ứng dụng
        private void MainForm_Load(object? sender, EventArgs e)
        {
            UpdateThemeToggleVisuals();          // Đồng bộ nút theme toggle
            if (DatabaseHelper.TestConnection(out string msg))
                lblDbStatus.Text = "🟢 CSDL: DPMS_Db (Kết nối thành công)";
            else
            {
                lblDbStatus.Text = "🔴 CSDL: Lỗi kết nối DPMS_Db";
                MessageBox.Show(msg, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            NavigateToStore();
            UpdateUserInterfaceSession();
        }

        // Hàm dùng chung để nạp UserControl bất kỳ vào vùng giữa
        private void SwitchView(UserControl newView)
        {
            panelMainContent.Controls.Clear();
            newView.Dock = DockStyle.Fill;
            panelMainContent.Controls.Add(newView);
            newView.BringToFront();
        }

        // Cập nhật trạng thái nút Active trên Sidebar
        private void SetActiveMenu(Button? btn)
        {
            if (_activeMenuButton != null && _activeMenuButton != btn)
            {
                _activeMenuButton.BackColor = ThemeHelper.IsDarkMode
                    ? Color.FromArgb(18, 25, 35) : Color.FromArgb(248, 249, 251);
                _activeMenuButton.ForeColor = ThemeHelper.IsDarkMode
                    ? Color.FromArgb(199, 213, 224) : Color.FromArgb(71, 85, 105);
            }
            _activeMenuButton = btn;
            if (_activeMenuButton != null)
            {
                _activeMenuButton.BackColor = ThemeHelper.IsDarkMode
                    ? Color.FromArgb(49, 98, 130) : Color.FromArgb(14, 116, 144);
                _activeMenuButton.ForeColor = Color.White;
                _activeMenuButton.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            }
        }

        // Sự kiện click 14 nút Sidebar (delegate pattern chuẩn C#)
        private void BtnMenuStore_Click(object? s, EventArgs e)        => NavigateToStore();
        private void BtnMenuReviews_Click(object? s, EventArgs e)      => NavigateToReviews(1);
        private void BtnMenuCart_Click(object? s, EventArgs e)         => NavigateToCart();
        private void BtnMenuWallet_Click(object? s, EventArgs e)       => NavigateToWallet();
        private void BtnMenuVouchers_Click(object? s, EventArgs e)     => NavigateToVouchers();
        private void BtnMenuLibrary_Click(object? s, EventArgs e)      => NavigateToLibrary();
        private void BtnMenuOrderHistory_Click(object? s, EventArgs e) => NavigateToOrderHistory();
        private void BtnMenuProfile_Click(object? s, EventArgs e)      => NavigateToProfile();
        private void BtnMenuDashboard_Click(object? s, EventArgs e)    => NavigateToDashboard();
        private void BtnMenuProductManage_Click(object? s, EventArgs e)=> NavigateToProductManage();
        private void BtnMenuCategoryManage_Click(object? s, EventArgs e)=>NavigateToCategoryManage();
        private void BtnMenuOrderManage_Click(object? s, EventArgs e)  => NavigateToOrderManage();
        private void BtnMenuUserManage_Click(object? s, EventArgs e)   => NavigateToUserManage();
        private void LblBalance_Click(object? s, EventArgs e)          => NavigateToWallet();

        // Nút chuyển đổi giao diện Sáng / Tối
        private void BtnThemeToggle_Click(object? sender, EventArgs e)
        {
            ThemeHelper.ToggleTheme(this);
            UpdateThemeToggleVisuals();
            UpdateUserInterfaceSession();
            SetActiveMenu(_activeMenuButton);
        }

        // RBAC Guards: chặn truy cập trái phép
        private bool EnsureLoggedIn(string feature)
        {
            if (!UserSession.IsLoggedIn)
            { MessageBox.Show($"Vui lòng đăng nhập để sử dụng '{feature}'."); NavigateToLogin(); return false; }
            return true;
        }
        private bool EnsureAdmin(string feature)
        {
            if (!UserSession.IsAdmin)
            { MessageBox.Show($"Chức năng '{feature}' chỉ dành cho Quản trị viên."); return false; }
            return true;
        }

        // Cập nhật hiển thị tên/số dư/quyền Admin sau mỗi sự kiện session
        public void UpdateUserInterfaceSession()
        {
            bool isAdmin = UserSession.IsLoggedIn && UserSession.IsAdmin;
            lblUserGreeting.Text = UserSession.IsLoggedIn
                ? $"{UserSession.CurrentFullName} ({(UserSession.IsAdmin ? "Admin" : "Khách Hàng")})"
                : "Khách (Chưa đăng nhập)";
            lblBalance.Text = UserSession.IsLoggedIn
                ? $"Ví: {FormatHelper.FormatCurrency(UserSession.CurrentBalance)}"
                : "Ví: 0 ₫";
            btnHeaderAuth.Text = UserSession.IsLoggedIn ? "Đăng xuất" : "Đăng nhập";
            lblAdminSection.Visible = isAdmin;
            btnMenuDashboard.Visible = isAdmin;
            btnMenuProductManage.Visible = isAdmin;
            btnMenuCategoryManage.Visible = isAdmin;
            btnMenuOrderManage.Visible = isAdmin;
            btnMenuUserManage.Visible = isAdmin;
            UpdateCartBadge();
        }
    }
}
```

---

## 3. Cấu trúc thư mục mã nguồn WinForms & Đường dẫn tương đối

Dự án được triển khai tập trung tại thư mục tương đối [`../qlspdigital/qlspdigital/`](../qlspdigital/qlspdigital/) với phân chia thư mục độc lập cho 5 thành viên nhằm tránh xung đột Git:

```text
qlspdigital/qlspdigital/
│
├── Program.cs                               # Điểm khởi chạy: Application.Run(new MainForm())
│
├── Shared/                                  # Tiện ích dùng chung
│   ├── DatabaseHelper.cs                    # Kết nối ADO.NET SQL Server (GetConnection, ExecuteQuery, ExecuteNonQuery)
│   ├── UserSession.cs                       # Quản lý phiên làm việc tĩnh (Login, Logout, UpdateBalance, UpdateProfile)
│   ├── ThemeHelper.cs                       # Hệ thống Dual Theme Engine (Dark/Light toggle, DWM API, theme.cfg persistence)
│   └── FormatHelper.cs                      # Tiện ích định dạng (FormatCurrency VNĐ, FormatDateTime, FormatFileSize)
│
├── DTOs/                                    # Data Transfer Objects
│   ├── NguoiDungDTO.cs                      # DTO người dùng & tài khoản
│   ├── SanPhamDTO.cs                        # DTO sản phẩm & TepSanPhamDTO
│   ├── DanhMucDTO.cs                        # DTO thể loại & NhaPhatHanhDTO
│   ├── GioHangItemDTO.cs                    # DTO giỏ hàng & MaGiamGiaDTO
│   ├── DonHangDTO.cs                        # DTO hóa đơn, ChiTietDonHangDTO & ThanhToanDTO
│   └── ThuVienItemDTO.cs                    # DTO thư viện, DanhGiaDTO & DashboardStatsDTO
│
├── DAL/                                     # Data Access Layer (Truy vấn ADO.NET SQL Server)
│   ├── Nhom1_Store/                         # [Lê Nguyễn Minh Trí]: SanPhamRepository.cs, DanhGiaRepository.cs
│   ├── Nhom2_Cart/                          # [Nguyễn Quốc Trung]: GioHangRepository.cs, ViTaiKhoanRepository.cs, MaGiamGiaRepository.cs
│   ├── Nhom3_ProductAdmin/                  # [Phạm Trung Hiếu]: SanPhamAdminRepository.cs, DanhMucRepository.cs, HoSoRepository.cs (chứa TepSanPhamRepository)
│   ├── Nhom4_SystemAdmin/                   # [Trần Quốc Thuận]: AuthRepository.cs, NguoiDungRepository.cs (chứa DashboardRepository)
│   └── Nhom5_Orders/                        # [Phạm Văn Đam]: DonHangRepository.cs (chứa ThuVienRepository)
│
├── BLL/                                     # Business Logic Layer (Xử lý nghiệp vụ & Ràng buộc)
│   ├── Nhom1_Store/                         # [Lê Nguyễn Minh Trí]: SanPhamService.cs (chứa SanPhamService, DanhGiaService)
│   ├── Nhom2_Cart/                          # [Nguyễn Quốc Trung]: GioHangService.cs (chứa GioHangService, ViTaiKhoanService, MaGiamGiaService, ThanhToanService)
│   ├── Nhom3_ProductAdmin/                  # [Phạm Trung Hiếu]: SanPhamAdminService.cs (chứa SanPhamAdminService, DanhMucService, HoSoService)
│   ├── Nhom4_SystemAdmin/                   # [Trần Quốc Thuận]: AuthService.cs (chứa AuthService, NguoiDungService, DashboardService)
│   └── Nhom5_Orders/                        # [Phạm Văn Đam]: DonHangService.cs (chứa DonHangService, ThuVienService)
│
├── Controls/                                # Thành phần giao diện tái sử dụng
│   ├── ProductCardControl.cs                # Thẻ sản phẩm trên lưới Store (Logic & State)
│   ├── ProductCardControl.Designer.cs       # Khởi tạo Controls & Visual Studio Designer
│   └── ProductCardControl.resx              # Resource đính kèm
│
└── Views/                                   # 15 MÀN HÌNH CHỨC NĂNG (Tất cả phân tách chuẩn .cs & .Designer.cs)
    │
    ├── Main/                                # Khung sườn tổng thể
    │   ├── MainForm.cs                      # Form chính chứa Header, Sidebar và panelMainContent
    │   ├── MainForm.Designer.cs             # Layout Visual Studio Designer
    │   └── MainForm.resx
    │
    ├── Nhom1_Store/                         # [Lê Nguyễn Minh Trí]
    │   ├── StoreViewControl.cs (.Designer.cs / .resx)      # UI 2: Trang chủ, lọc danh mục, tìm kiếm, lưới sản phẩm
    │   ├── ProductDetailControl.cs (.Designer.cs / .resx)  # UI 3: Chi tiết sản phẩm, nút Quay lại & Nhận xét không bị che khuất
    │   └── ProductReviewControl.cs (.Designer.cs / .resx)  # UI 14: Đánh giá & Nhận xét sản phẩm (1-5 sao)
    │
    ├── Nhom2_Cart/                          # [Nguyễn Quốc Trung]
    │   ├── CartControl.cs (.Designer.cs / .resx)           # UI 4: Giỏ hàng, sửa số lượng, áp dụng voucher, thanh toán ví
    │   ├── WalletControl.cs (.Designer.cs / .resx)         # UI 5: Ví tài khoản, nạp tiền mô phỏng theo mệnh giá
    │   └── VoucherControl.cs (.Designer.cs / .resx)        # UI 15: Kho mã giảm giá, thu thập ưu đãi, xóa voucher
    │
    ├── Nhom3_ProductAdmin/                  # [Phạm Trung Hiếu]
    │   ├── ProductManageControl.cs (.Designer.cs / .resx)  # UI 10: CRUD thông tin sản phẩm, giá, upload metadata tệp số
    │   ├── CategoryManageControl.cs (.Designer.cs / .resx) # UI 11: CRUD danh mục thể loại
    │   └── ProfileControl.cs (.Designer.cs / .resx)        # UI 8: Xem và cập nhật thông tin cá nhân, đổi mật khẩu
    │
    ├── Nhom4_SystemAdmin/                   # [Trần Quốc Thuận]
    │   ├── LoginControl.cs (.Designer.cs / .resx)          # UI 1: Đăng nhập hệ thống
    │   ├── RegisterControl.cs (.Designer.cs / .resx)       # UI 1: Đăng ký tài khoản
    │   ├── UserManageControl.cs (.Designer.cs / .resx)     # UI 12: Danh sách tài khoản, phân quyền, khóa/mở tài khoản
    │   └── DashboardControl.cs (.Designer.cs / .resx)      # UI 9: Thống kê doanh thu, số đơn, top sản phẩm bán chạy
    │
    └── Nhom5_Orders/                        # [Phạm Văn Đam]
        ├── LibraryControl.cs (.Designer.cs / .resx)        # UI 6: Kho sản phẩm đã sở hữu của khách, nút tải file ProgressBar
        ├── OrderManageControl.cs (.Designer.cs / .resx)    # UI 13: Quản trị viên theo dõi và duyệt đơn hàng
        └── OrderHistoryControl.cs (.Designer.cs / .resx)   # UI 7: Khách hàng xem lịch sử đơn hàng cá nhân
```

> **Quy chuẩn Thiết kế Giao diện WinForms & Visual Studio Designer:**
> - Toàn bộ 18 Form / UserControl trong dự án đều tuân thủ cấu trúc phân tách song song: file `*.cs` (chứa logic tương tác, điều hướng, sự kiện BLL/DAL) và `*.Designer.cs` (chứa mã khởi tạo đồ họa `InitializeComponent()`, tọa độ, Docking, Anchor).
> - Mọi thành phần đều được cấu hình khoảng cách an toàn (Safety Margin >= 10px), tuyệt đối không để các Panel hay DataGridView đè khuất các nút bấm điều hướng (`btnBack`) hoặc nút hành động (`btnWriteReview`), đảm bảo tương thích 100% với giao diện kéo thả trực quan của Visual Studio.

---

## 4. Bảng phân công 15 UI & Tiêu chí mỗi người tối thiểu 3 chức năng CRUD (Đã đơn giản hóa)

> **Tiêu chí cốt lõi:** Mỗi thành viên đều có **ít nhất 3 chức năng / màn hình với các thao tác CRUD (Create - Read - Update - Delete)** thao tác trực tiếp với SQL Server bằng các lệnh cơ bản (`SELECT`, `INSERT`, `UPDATE`, `DELETE`) để tự tin trả lời khi giảng viên hỏi vấn đáp.  
> Các nghiệp vụ phức tạp đều được **đơn giản hóa và mô phỏng** để vừa sức sinh viên, không gây quá tải.

### 4.1. Bảng phân công chi tiết

| STT | Thành viên phụ trách | Số UI | Danh sách UI cụ thể | Chi tiết 3 chức năng CRUD đơn giản để báo cáo BTL | Cách đơn giản hóa |
|:---:|---|:---:|---|---|---|
| **1** | **Lê Nguyễn Minh Trí** | **3 UI** | 1. **Store / Trang chủ** (UI 2)<br>2. **Product Details** (UI 3)<br>3. **Reviews & Ratings** (UI 14) | **CRUD 1: Lọc & Tìm kiếm sản phẩm (`SanPham`)**<br>- Xem danh sách thẻ sản phẩm (Read), tìm theo từ khóa, lọc theo ComboBox danh mục.<br>**CRUD 2: Xem chi tiết & Tóm tắt đánh giá (`SanPham`)**<br>- Xem chi tiết thông số, cấu hình, điểm sao trung bình (`AVG(SoSao)`).<br>**CRUD 3: Quản lý Đánh giá & Nhận xét (`DanhGia`)**<br>- Xem danh sách đánh giá của sản phẩm (Read), chấm điểm từ 1-5 sao và viết nhận xét mới (Create), chỉnh sửa đánh giá của mình (Update), xóa đánh giá (Delete). | **Chuẩn mô hình Steam Reviews:**<br>Màn hình Đánh giá mang tính tương tác cộng đồng cao, thao tác CRUD trực tiếp trên bảng `DanhGia` liên kết `SanPham` và `NguoiDung`. |
| **2** | **Nguyễn Quốc Trung** | **3 UI** | 1. **Shopping Cart** (UI 4)<br>2. **My Wallet / Nạp tiền** (UI 5)<br>3. **Vouchers & Ưu đãi** (UI 15) | **CRUD 1: Quản lý Giỏ hàng (`GioHang`)**<br>- Thêm sản phẩm vào giỏ có kiểm tra chặn trùng lặp (Create), xem danh sách giỏ hàng & tổng tiền tạm tính (Read), kiểm tra điều kiện bản quyền số cố định Quantity = 1 và ràng buộc không mua đè game đã sở hữu (Business Validation), xóa sản phẩm hoặc làm rỗng giỏ hàng (Delete).<br>**CRUD 2: Ví tài khoản & Nạp tiền (`NguoiDung.SoDuVi`)**<br>- Xem số dư ví (Read), nạp tiền vào ví bằng nút chọn mệnh giá 50k, 100k, 200k, 500k (Update: `SoDuVi = SoDuVi + @SoTien`). Bấm thanh toán trong giỏ hàng để trừ tiền ví và tự động cấp quyền vào `SoHuuSanPham`.<br>**CRUD 3: Quản lý Voucher & Giảm giá (`MaGiamGia` & `MaGiamGiaNguoiDung`)**<br>- Xem danh sách voucher ưu đãi (Read), nhập mã code để thu thập voucher mới (Create), chọn voucher để chiết khấu trực tiếp tổng tiền giỏ hàng (Update `DaSuDung = 1`), xóa voucher khỏi ví cá nhân (Delete). | **Mô phỏng Ví Steam & Hệ thống khuyến mãi:**<br>Thanh toán 1-Click bằng số dư ví kết hợp trừ tiền chiết khấu từ mã giảm giá. Các mã voucher định nghĩa sẵn (VD: `WELCOME2026`, `SUMMERSALE`). Kiểm tra `Số dư ví >= Tổng tiền sau giảm giá` trước khi trừ tiền. |
| **3** | **Phạm Trung Hiếu** | **3 UI** | 1. **Product Management** (UI 10)<br>2. **Category Management** (UI 11)<br>3. **User Profile** (UI 8) | **CRUD 1: Quản lý Sản phẩm (`SanPham` & `TepSanPham`)**<br>- Thêm, xem, sửa giá/tên, xóa sản phẩm và cập nhật metadata tệp tải về.<br>**CRUD 2: Quản lý Danh mục (`DanhMuc`)**<br>- Thêm, xem, sửa, xóa thể loại (bảng cực kỳ dễ: Mã, Tên danh mục).<br>**CRUD 3: Hồ sơ cá nhân (`NguoiDung`)**<br>- Xem thông tin, sửa họ tên, đổi mật khẩu. | **Form nhập liệu tiêu chuẩn:**<br>Thao tác thêm/sửa/xóa trên các TextBox thông thường học trên lớp; CRUD Danh mục và Profile làm rất nhanh. |
| **4** | **Trần Quốc Thuận** | **3 UI** | 1. **Login / Register** (UI 1)<br>2. **User Management** (UI 12)<br>3. **Admin Dashboard** (UI 9) | **CRUD 1: Quản lý Người dùng (`NguoiDung`)**<br>- Xem bảng user trên DataGridView, tìm kiếm, nút bấm Khóa/Mở tài khoản (`TrangThai = 0/1`).<br>**CRUD 2: Đăng ký tài khoản (`NguoiDung`)**<br>- Form đăng ký: Nhập tên đăng nhập, email, mật khẩu -> Lưu trực tiếp tài khoản mới vào CSDL.<br>**CRUD 3: Bảng chỉ số Dashboard (`DonHang`, `NguoiDung`, `SanPham`)**<br>- Tính toán các chỉ số thống kê tổng hợp: Tổng doanh thu (`SUM`), Tổng số đơn (`COUNT`), Tổng khách hàng (`COUNT`), Tổng sản phẩm mở bán (`COUNT`), và Top 5 sản phẩm bán chạy nhất sàn. | **Không cần vẽ biểu đồ phức tạp:**<br>Dashboard chỉ cần các ô thẻ Metric Cards hiển thị con số thống kê to rõ ràng từ các câu lệnh SQL tổng hợp `SUM` và `COUNT`. |
| **5** | **Phạm Văn Đam** | **3 UI** | 1. **Order Management** (UI 13)<br>2. **Order History** (UI 7)<br>3. **My Library** (UI 6) | **CRUD 1: Quản lý Đơn hàng Admin (`DonHang` & `ChiTietDonHang`)**<br>- Xem bảng đơn hàng toàn hệ thống (Read), lọc theo trạng thái, xem danh sách sản phẩm con trong đơn, nút "Kích hoạt thủ công" (dùng cho đơn ngoại lệ/chờ xử lý để cấp game vào Thư viện, tự động làm mờ nếu đơn đã Hoàn thành) hoặc "Hủy đơn" (Update).<br>**CRUD 2: Lịch sử đơn hàng cá nhân (`DonHang` & `ChiTietDonHang`)**<br>- Xem danh sách các đơn hàng đã mua của chính mình (`WHERE MaNguoiDung = @UserId`), click đúp xem chi tiết hóa đơn (Read).<br>**CRUD 3: Thư viện sở hữu & Tải tệp số (`SoHuuSanPham` & `TepSanPham`)**<br>- Xem kho game đã mua (Read), nút "Tải xuống về máy" (Update `SoLanTai`, sao chép file từ `Storage/Products/...` sang máy khách có ProgressBar), nút "Gỡ khỏi thư viện" (Delete). | **Chuẩn nền tảng Steam Library:**<br>Màn "Quản lý đơn (Admin)" và "Lịch sử đơn (Khách)" dùng chung cấu trúc `DonHang`. Phân hệ kết nối trực tiếp với file upload của Nhóm 3 để tải tệp kỹ thuật số về máy tính khách hàng. |

### 4.2. Rà soát & Đảm bảo tính độc lập (Không trùng lặp giữa 5 nhóm)
- **Nhóm 1 (Trải nghiệm Cửa hàng & Đánh giá):** Tập trung vào mặt tiền cửa hàng (Store, Product Details) và sự tương tác cảm nhận của khách hàng (Reviews). Không đụng chạm vào quản trị Admin hay tiền nong.
- **Nhóm 2 (Thanh toán & Ưu đãi):** Tập trung vào túi tiền của khách (Giỏ hàng, Nạp tiền ví, Khấu trừ mã giảm giá). Độc lập hoàn toàn với việc duyệt đơn của Admin hay quản trị sản phẩm.
- **Nhóm 3 (Quản trị Hàng hóa & Hồ sơ):** Đóng vai trò thủ kho Admin thêm/sửa sản phẩm, danh mục; tải lên tệp kỹ thuật số vật lý; kiêm quản lý thông tin tài khoản cá nhân.
- **Nhóm 4 (Hạ tầng Xác thực & Báo cáo):** Đóng vai trò bảo mật hệ thống (Đăng nhập/Đăng ký), giám sát người dùng (phân quyền/khóa user) và báo cáo số liệu tổng kết toàn sàn (Dashboard).
- **Nhóm 5 (Đơn hàng & Kho thư viện số):** Tập trung vào việc bàn giao sản phẩm sau mua (Quản lý trạng thái đơn hàng, Lịch sử hóa đơn cá nhân và Kho game sở hữu có nút tải file).

### 4.3. Nguyên tắc "Đơn giản hóa" cho cả nhóm
- **Thanh toán & Nạp tiền (Mô hình Steam Wallet):** Mô phỏng nạp tiền vào ví tài khoản bằng các nút bấm mệnh giá cố định; mua hàng bằng cách trừ thẳng số dư ví và tự động ghi quyền vào thư viện `SoHuuSanPham`.
- **Tải file kỹ thuật số:** Lưu đường dẫn tương đối và metadata trong CSDL, đọc file nhị phân từ thư mục hệ thống có thanh tiến trình `ProgressBar`.
- **Thống kê Dashboard:** Không cần thư viện biểu đồ phức tạp; dùng các `Label` hiển thị con số đếm từ database.
- **Các thao tác CRUD:** Chỉ tập trung vào 4 câu lệnh SQL cốt lõi: `SELECT`, `INSERT`, `UPDATE`, `DELETE`.

## 5. Kế hoạch tiến độ thực hiện bài tập lớn

| Giai đoạn | Nội dung công việc chính | Sản phẩm bàn giao | Người phụ trách chính |
|---|---|---|---|
| **Giai đoạn 1** | - Chốt yêu cầu & kịch bản bài tập lớn.<br>- Thiết kế cơ sở dữ liệu (ERD) và chạy script tạo bảng SQL Server.<br>- Dựng Solution đa dự án và khung sườn `MainForm`. | Database SQL hoàn chỉnh, `MainForm` khung sườn rỗng trên Git. | Cả nhóm |
| **Giai đoạn 2** | - Từng thành viên tạo thư mục view của mình.<br>- Kéo thả giao diện `UserControl` theo phân công.<br>- Ghép các nút bấm menu trên `MainForm` để chuyển đổi qua lại giữa các màn hình. | Giao diện tĩnh của 15 UI hiển thị mượt mà trên khung sườn. | 5 thành viên theo phân công |
| **Giai đoạn 3** | - Viết tầng DAL (kết nối ADO.NET) và BLL (nghiệp vụ).<br>- Đổ dữ liệu thật từ SQL Server lên giao diện (Load DataGridView, nạp thẻ Store...). | Các chức năng CRUD và hiển thị dữ liệu hoạt động đúng. | 5 thành viên theo phân công |
| **Giai đoạn 4** | - Nối luồng hoàn chỉnh: Chọn sản phẩm -> Giỏ hàng -> Thanh toán -> Cấp quyền sở hữu -> Thư viện tải file.<br>- Hoàn thiện Dashboard và báo cáo. | Toàn bộ luồng mua hàng và quản trị chạy thông suốt. | Cả nhóm |
| **Giai đoạn 5** | - Kiểm thử tổng thể, xử lý lỗi nhập liệu, bẫy lỗi kết nối DB.<br>- Viết báo cáo Word/Slide thuyết trình và chuẩn bị dữ liệu demo bảo vệ bài tập lớn. | Bản báo cáo bài tập lớn hoàn thiện và bản demo sẵn sàng. | Cả nhóm |

