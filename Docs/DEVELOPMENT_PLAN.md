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
|  [Logo DPMS]   CỬA HÀNG SẢN PHẨM KỸ THUẬT SỐ        [User] [💰 Ví: 500.000đ] [🛒 Cart]        |  <- HEADER
+-----------------------+-----------------------------------------------------------------------+
|                       |                                                                       |
|  🔘 Cửa hàng (Store)  |                   VÙNG NỘI DUNG CHÍNH                                 |
|  🔘 Thư viện (Library)|                     (panelMainContent)                                |
|  🔘 Giỏ hàng (Cart)   |                                                                       |
|  🔘 Ví của tôi (Wallet)|  => Khi bấm menu bên trái, UserControl của từng người                |
|  🔘 Lịch sử đơn hàng  |     sẽ được tự động nạp và hiển thị tràn đầy vào đây.                 |
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
   - Đóng vai trò làm khung vỏ (Shell).
   - Chứa Header (Logo, trạng thái tài khoản, số dư ví `lblBalance`, nút giỏ hàng mini) và Sidebar (các nút menu điều hướng).
   - Chứa một `Panel` trung tâm đặt tên là `panelMainContent` (thuộc tính `Dock = DockStyle.Fill`).
2. **Các màn hình chức năng (`UserControl`):**
   - 13 màn hình chức năng của các thành viên được thiết kế dưới dạng **`UserControl`** thay vì từng `Form` riêng lẻ trôi nổi.
   - Khi bấm nút trên Sidebar, `UserControl` tương ứng sẽ được nạp trực tiếp vào `panelMainContent`.

### 2.3. Mã nguồn khởi chạy & Chuyển đổi màn hình động
Trong file `MainForm.cs`:

```csharp
using System;
using System.Windows.Forms;
using DPMS.WinForms.Views.Store;

namespace DPMS.WinForms.Views.Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Tự động mở Trang chủ Cửa hàng khi vừa bật ứng dụng
        private void MainForm_Load(object sender, EventArgs e)
        {
            SwitchView(new StoreViewControl());
        }

        // Hàm dùng chung để nạp UserControl bất kỳ vào vùng giữa
        public void SwitchView(UserControl newView)
        {
            panelMainContent.Controls.Clear();       // Xóa giao diện cũ
            newView.Dock = DockStyle.Fill;           // Tràn đầy khung panel
            panelMainContent.Controls.Add(newView);   // Thêm giao diện mới
            newView.BringToFront();
        }

        // Sự kiện click các mục trên Sidebar
        private void btnMenuStore_Click(object sender, EventArgs e)
        {
            SwitchView(new StoreViewControl());
        }

        private void btnMenuLibrary_Click(object sender, EventArgs e)
        {
            SwitchView(new Views.Customer.LibraryControl());
        }

        private void btnMenuCart_Click(object sender, EventArgs e)
        {
            SwitchView(new Views.Cart.CartControl());
        }

        private void btnMenuWallet_Click(object sender, EventArgs e)
        {
            SwitchView(new Views.Cart.WalletControl());
        }

        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            SwitchView(new Views.Admin.DashboardControl());
        }
    }
}
```

---

## 3. Cấu trúc thư mục mã nguồn WinForms

Để tránh xung đột Git giữa 5 người khi thiết kế giao diện (`.Designer.cs`), thư mục `DPMS.WinForms` được phân chia thành các thư mục độc lập theo module:

```text
DPMS.WinForms/
│
├── Program.cs                               # Điểm khởi chạy ứng dụng: Application.Run(new MainForm())
│
├── Assets/                                  # Tài nguyên tĩnh
│   ├── Icons/                               # Icon định dạng .png / .ico
│   ├── Images/                              # Logo, banner, ảnh placeholder
│   └── Fonts/                               # Font chữ tùy biến (nếu có)
│
├── Controls/                                # Các thành phần nhỏ tái sử dụng (Component)
│   ├── ProductCardControl.cs                # Thẻ sản phẩm trên lưới Store
│   └── RatingStarsControl.cs                # Điều khiển hiển thị đánh giá sao
│
└── Views/                                   # 13 MÀN HÌNH CHỨC NĂNG
    │
    ├── Main/                                # Khung sườn tổng thể
    │   ├── MainForm.cs                      # Form chính chứa Header, Sidebar và panelMainContent
    │   └── MainForm.Designer.cs
    │
    ├── Store/                               # Phân hệ Cửa hàng & Xem sản phẩm
    │   ├── StoreViewControl.cs              # UI 2: Trang chủ, lọc theo danh mục, tìm kiếm, lưới sản phẩm
    │   └── ProductDetailControl.cs          # UI 3: Chi tiết sản phẩm, ảnh mô tả, giá, nút mua
    │
    ├── Cart/                                # Phân hệ Mua hàng & Ví tài khoản
    │   ├── CartControl.cs                   # UI 4: Giỏ hàng, sửa số lượng, thanh toán 1-click bằng ví
    │   └── WalletControl.cs                 # UI 5: Ví tài khoản, nạp tiền mô phỏng (Steam Wallet)
    │
    ├── ProductAdmin/                        # Phân hệ Quản trị Hàng hóa & Hồ sơ
    │   ├── ProductManageControl.cs          # UI 10: CRUD thông tin sản phẩm, giá, upload metadata file
    │   ├── CategoryManageControl.cs         # UI 11: CRUD danh mục thể loại
    │   └── ProfileControl.cs                # UI 8: Xem và cập nhật thông tin cá nhân, đổi mật khẩu
    │
    ├── SystemAdmin/                         # Phân hệ Quản trị Hệ thống & Báo cáo
    │   ├── DashboardControl.cs              # UI 9: Thống kê doanh thu, số đơn, sản phẩm bán chạy
    │   ├── UserManageControl.cs             # UI 12: Danh sách tài khoản, phân quyền, khóa/mở tài khoản
    │   └── Auth/
    │       ├── LoginControl.cs              # UI 1: Đăng nhập
    │       └── RegisterControl.cs           # UI 1: Đăng ký tài khoản
    │
    └── Orders/                              # Phân hệ Sở hữu & Đơn hàng
        ├── LibraryControl.cs                # UI 6: Kho sản phẩm đã sở hữu của khách, nút tải file
        ├── OrderManageControl.cs            # UI 13: Quản trị viên theo dõi và duyệt đơn hàng
        └── OrderHistoryControl.cs           # UI 7: Khách hàng xem lịch sử đơn hàng cá nhân
```

---

## 4. Bảng phân công 13 UI & Tiêu chí mỗi người tối thiểu 2 CRUD (Đã đơn giản hóa)

> **Tiêu chí cốt lõi:** Mỗi thành viên đều có **ít nhất 2 chức năng CRUD (Create - Read - Update - Delete)** thao tác trực tiếp với SQL Server bằng các lệnh cơ bản (`SELECT`, `INSERT`, `UPDATE`, `DELETE`) để tự tin trả lời khi giảng viên hỏi vấn đáp.  
> Các nghiệp vụ phức tạp đều được **đơn giản hóa và mô phỏng** để vừa sức sinh viên, không gây quá tải.

### 4.1. Bảng phân công chi tiết

| STT | Thành viên phụ trách | Số UI | Danh sách UI cụ thể | Chi tiết 2 CRUD đơn giản để báo cáo BTL | Cách đơn giản hóa |
|:---:|---|:---:|---|---|---|
| **1** | *(Điền tên thành viên 1)* | **2 UI** | 1. **Store / Trang chủ** (UI 2)<br>2. **Product Details** (UI 3) | **CRUD 1: Lọc & Tìm kiếm sản phẩm (`Products`)**<br>- Xem danh sách (Read), tìm theo tên, lọc theo combobox danh mục.<br>**CRUD 2: Danh sách Yêu thích (`Favorites`)**<br>- Bấm nút "Thích" để thêm vào danh sách yêu thích (Create), xem danh sách đã thích (Read), bấm bỏ thích (Delete). | **Rất trực quan:**<br>Chỉ cần 1 nút hình trái tim để thêm/xóa khỏi bảng Yêu thích, không cần làm hệ thống đánh giá sao hay bình luận phức tạp. |
| **2** | *(Điền tên thành viên 2)* | **2 UI** | 1. **Shopping Cart** (UI 4)<br>2. **My Wallet / Nạp tiền** (UI 5) | **CRUD 1: Quản lý Giỏ hàng (`CartItems`)**<br>- Thêm món vào giỏ (Create), xem giỏ hàng (Read), nút +/- sửa số lượng (Update), xóa món khỏi giỏ (Delete).<br>**CRUD 2: Ví tài khoản & Nạp tiền (`Users.Balance`)**<br>- Xem số dư ví (Read), nạp tiền vào ví bằng nút chọn mệnh giá 50k, 100k, 200k, 500k (Update: `Balance = Balance + @Amount`). Bấm thanh toán trong giỏ hàng để trừ tiền ví (Update: `Balance = Balance - @Total`) và tự động thêm game vào kho sở hữu `UserProducts`. | **Mô phỏng Ví Steam (Steam Wallet):**<br>Không tích hợp cổng ngân hàng hay tạo màn hình Checkout cồng kềnh; người dùng nạp tiền trước vào ví tài khoản, sau đó thanh toán 1-click ngay trong giỏ hàng. Kiểm tra `Số dư ví >= Tổng tiền` trước khi trừ tiền. |
| **3** | *(Điền tên thành viên 3)* | **3 UI** | 1. **Product Management** (UI 10)<br>2. **Category Management** (UI 11)<br>3. **User Profile** (UI 8) | **CRUD 1: Quản lý Sản phẩm (`Products`)**<br>- Thêm, xem, sửa giá/tên, xóa sản phẩm cơ bản.<br>**CRUD 2: Quản lý Danh mục (`Categories`)**<br>- Thêm, xem, sửa, xóa thể loại (bảng cực kỳ dễ: Mã, Tên danh mục).<br>**CRUD 3: Hồ sơ cá nhân (`User Profile`)**<br>- Xem thông tin, sửa họ tên, đổi mật khẩu. | **Form nhập liệu tiêu chuẩn:**<br>Thao tác thêm/sửa/xóa trên các TextBox thông thường học trên lớp; CRUD Danh mục và Profile làm rất nhanh. |
| **4** | *(Điền tên thành viên 4)* | **3 UI** | 1. **Login / Register** (UI 1)<br>2. **User Management** (UI 12)<br>3. **Admin Dashboard** (UI 9) | **CRUD 1: Quản lý Người dùng (`Users`)**<br>- Xem bảng user trên DataGridView, nút bấm Khóa/Mở tài khoản (`IsActive = 0/1`).<br>**CRUD 2: Đăng ký tài khoản (`Auth`)**<br>- Form đăng ký: Nhập tên, email, mật khẩu -> INSERT tài khoản mới.<br>*(Màn Dashboard: Xem thống kê tổng hợp)* | **Không cần vẽ biểu đồ phức tạp:**<br>Dashboard chỉ cần 3 ô chữ to hiển thị 3 con số thống kê bằng câu lệnh SQL cơ bản: Tổng doanh thu (`SUM`), Tổng số đơn (`COUNT`), Tổng user (`COUNT`). |
| **5** | *(Điền tên thành viên 5)* | **3 UI** | 1. **Order Management** (UI 13)<br>2. **Order History** (UI 7)<br>3. **My Library** (UI 6) | **CRUD 1: Quản lý Đơn hàng (`Orders`)**<br>- Xem bảng đơn hàng, nút "Duyệt đơn" (chuyển trạng thái sang Hoàn thành) hoặc "Hủy đơn".<br>**CRUD 2: Thư viện sở hữu (`UserProducts`)**<br>- Xem kho game đã mua, nút "Tải về" (mô phỏng tải file), nút ẩn/gỡ game khỏi thư viện. | **Tận dụng chung 1 bảng Orders:**<br>Màn "Quản lý đơn (Admin)" và màn "Lịch sử đơn (Khách)" dùng chung 1 bảng dữ liệu `Orders`, chỉ khác nhau ở câu lệnh `WHERE UserId = ...`. |

### 4.2. Nguyên tắc "Đơn giản hóa" cho cả nhóm
- **Thanh toán & Nạp tiền (Mô hình Steam Wallet):** Mô phỏng nạp tiền vào ví tài khoản bằng các nút bấm mệnh giá cố định; mua hàng bằng cách trừ thẳng số dư ví và tự động ghi quyền vào thư viện `UserProducts`.
- **Tải file kỹ thuật số:** Không cần tải tệp nặng thật; chỉ cần hiển thị thông báo `MessageBox.Show("Tải xuống thành công!")` hoặc mở một thư mục mẫu trên máy.
- **Thống kê Dashboard:** Không cần thư viện biểu đồ phức tạp; dùng các `Label` hiển thị con số đếm từ database.
- **Các thao tác CRUD:** Chỉ tập trung vào 4 câu lệnh SQL cốt lõi: `SELECT`, `INSERT`, `UPDATE`, `DELETE`.

## 5. Kế hoạch tiến độ thực hiện bài tập lớn

| Giai đoạn | Nội dung công việc chính | Sản phẩm bàn giao | Người phụ trách chính |
|---|---|---|---|
| **Giai đoạn 1** | - Chốt yêu cầu & kịch bản bài tập lớn.<br>- Thiết kế cơ sở dữ liệu (ERD) và chạy script tạo bảng SQL Server.<br>- Dựng Solution đa dự án và khung sườn `MainForm`. | Database SQL hoàn chỉnh, `MainForm` khung sườn rỗng trên Git. | Cả nhóm |
| **Giai đoạn 2** | - Từng thành viên tạo thư mục view của mình.<br>- Kéo thả giao diện `UserControl` theo phân công.<br>- Ghép các nút bấm menu trên `MainForm` để chuyển đổi qua lại giữa các màn hình. | Giao diện tĩnh của 13 UI hiển thị mượt mà trên khung sườn. | 5 thành viên theo phân công |
| **Giai đoạn 3** | - Viết tầng DAL (kết nối ADO.NET) và BLL (nghiệp vụ).<br>- Đổ dữ liệu thật từ SQL Server lên giao diện (Load DataGridView, nạp thẻ Store...). | Các chức năng CRUD và hiển thị dữ liệu hoạt động đúng. | 5 thành viên theo phân công |
| **Giai đoạn 4** | - Nối luồng hoàn chỉnh: Chọn sản phẩm -> Giỏ hàng -> Thanh toán -> Cấp quyền sở hữu -> Thư viện tải file.<br>- Hoàn thiện Dashboard và báo cáo. | Toàn bộ luồng mua hàng và quản trị chạy thông suốt. | Cả nhóm |
| **Giai đoạn 5** | - Kiểm thử tổng thể, xử lý lỗi nhập liệu, bẫy lỗi kết nối DB.<br>- Viết báo cáo Word/Slide thuyết trình và chuẩn bị dữ liệu demo bảo vệ bài tập lớn. | Bản báo cáo bài tập lớn hoàn thiện và bản demo sẵn sàng. | Cả nhóm |

