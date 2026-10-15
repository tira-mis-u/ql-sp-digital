# Digital Product Management System (DPMS)

<img align="right" width="12%" src="https://thumb.wikimedia.org/wikipedia/commons/thumb/4/41/Microsoft_SQL_Server_2025_icon.svg/960px-Microsoft_SQL_Server_2025_icon.svg.png?utm_source=commons.wikimedia.org&utm_campaign=index&utm_content=thumbnail">
<img align="right" width="12%" src="https://cdn3d.iconscout.com/3d/free/thumb/free-visual-studio-3d-icon-png-download-7578028.png">
<img align="right" width="12%" src="https://cdn3d.iconscout.com/3d/free/thumb/free-c-sharp-3d-icon-png-download-7577997.png">

**Bài tập lớn học phần Lập trình trực quan C#**

Digital Product Management System (DPMS) là phần mềm cửa hàng trực tuyến chuyên quản lý và phân phối các sản phẩm kỹ thuật số, lấy cảm hứng từ mô hình cửa hàng của Steam.

Dự án hướng đến việc xây dựng một ứng dụng desktop cho phép người dùng khám phá, mua và quản lý các sản phẩm số, đồng thời cung cấp hệ thống quản trị để quản lý sản phẩm, tài khoản, đơn hàng và hoạt động kinh doanh.

## Thông tin dự án

| Hạng mục | Công nghệ |
|---|---|
| Học phần | Lập trình trực quan C# |
| Ngôn ngữ lập trình | C# |
| Framework | .NET |
| Giao diện | Windows Forms (WinForms) |
| Hệ quản trị cơ sở dữ liệu | Microsoft SQL Server |
| Công cụ quản trị CSDL | SQL Server Management Studio (SSMS) |
| IDE | Visual Studio |
| Kết nối cơ sở dữ liệu | ADO.NET |
| Kiến trúc | 3-Layer Architecture |
| Quản lý mã nguồn | Git & GitHub |

> **Lưu ý:** SQL Server là hệ quản trị cơ sở dữ liệu; SQL Server Management Studio (SSMS) là công cụ dùng để quản lý và thao tác với SQL Server.

---

## Thành viên

<img width="20%" align="right" src="https://media.tenor.com/Xclc9qdBAUsAAAAM/work.gif">
<img width="30%" align="right" src="https://raw.githubusercontent.com/BhavyaCodes/BhavyaCodes/master/.github/cat.gif">

1. Phạm Trung Hiếu
2. Trần Quốc Thuận
3. Lê Nguyễn Minh Trí
4. Phạm Văn Đam
5. Nguyễn Quốc Trung

*Cùng với sự hỗ trợ của những con nô lệ trí tuệ nhân tạo.*

---

## 1. Mục tiêu dự án

DPMS được xây dựng nhằm vận dụng kiến thức của học phần Lập trình trực quan C# vào một sản phẩm có nghiệp vụ thực tế.

Các mục tiêu chính:

- Xây dựng ứng dụng desktop bằng C# và WinForms.
- Vận dụng lập trình hướng đối tượng (OOP).
- Thiết kế và quản lý cơ sở dữ liệu quan hệ bằng SQL Server.
- Thực hiện các thao tác CRUD: Create, Read, Update, Delete.
- Xây dựng quy trình mua hàng, thanh toán mô phỏng và cấp quyền sở hữu sản phẩm.
- Áp dụng phân quyền người dùng và kiểm soát truy cập.
- Tổ chức mã nguồn theo kiến trúc phân lớp.
- Hoàn thiện giao diện, kiểm thử và tài liệu dự án.

## 2. Phạm vi hệ thống

DPMS tập trung vào hai nhóm người dùng chính.

### 2.1. Khách hàng (KhachHang)

- Đăng ký, đăng nhập và đăng xuất.
- Quản lý thông tin cá nhân.
- Duyệt danh mục sản phẩm.
- Tìm kiếm, lọc và sắp xếp sản phẩm.
- Xem thông tin chi tiết sản phẩm.
- Thêm và xóa sản phẩm trong giỏ hàng.
- Đặt hàng và thanh toán mô phỏng.
- Theo dõi lịch sử đơn hàng.
- Quản lý thư viện sản phẩm đã sở hữu.
- Tải xuống các sản phẩm đã mua.

### 2.2. Quản trị viên (QuanTriVien)

- Quản lý sản phẩm.
- Quản lý danh mục sản phẩm.
- Quản lý tài khoản khách hàng.
- Quản lý đơn hàng và trạng thái thanh toán.
- Quản lý thông tin tệp kỹ thuật số.
- Theo dõi doanh thu và số lượng đơn hàng.
- Thống kê sản phẩm bán chạy.

### 2.3. Các loại sản phẩm hỗ trợ

Hệ thống được thiết kế để quản lý nhiều loại sản phẩm kỹ thuật số, chẳng hạn:

- **Games:** Trò chơi điện tử.
- **Software:** Phần mềm và ứng dụng.
- **3D Models:** Mô hình 3D.
- **Digital Art:** Tài nguyên đồ họa và thiết kế.
- **Audio:** Nhạc, âm thanh và hiệu ứng.
- **Documents:** Tài liệu điện tử và sách số.
- **Assets:** Các gói tài nguyên phục vụ phát triển game.

Danh mục có thể được mở rộng hoặc thay đổi thông qua chức năng quản trị.

---

## 3. Chức năng chính

| Module | Chức năng | Phụ trách |
|---|---|---|
| Authentication | Đăng ký, đăng nhập, đăng xuất và phân quyền | Nhóm 4 |
| Store | Hiển thị, tìm kiếm, lọc và sắp xếp sản phẩm | Nhóm 1 |
| Product Details | Hiển thị thông tin, hình ảnh, giá và mô tả | Nhóm 1 |
| Product Reviews & Ratings | Đánh giá sao (1-5★), viết nhận xét phản hồi và xem đánh giá cộng đồng | Nhóm 1 |
| Shopping Cart | Quản lý sản phẩm trong giỏ hàng, cập nhật số lượng, xóa món | Nhóm 2 |
| Payment & Wallet Simulation | Mô phỏng ví tài khoản (Steam Wallet), nạp tiền mệnh giá và thanh toán 1-Click | Nhóm 2 |
| Vouchers & Promotions | Quản lý mã giảm giá, thu thập khuyến mãi và áp dụng khấu trừ đơn hàng | Nhóm 2 |
| Product Management | CRUD sản phẩm (thêm, sửa, xóa, tìm kiếm sản phẩm) | Nhóm 3 |
| Category Management | CRUD danh mục thể loại sản phẩm | Nhóm 3 |
| User Profile | Quản lý hồ sơ cá nhân và đổi mật khẩu | Nhóm 3 |
| User Management | Quản lý danh sách tài khoản, phân quyền và khóa/mở người dùng | Nhóm 4 |
| Dashboard & Reports | Thống kê doanh thu, số lượng đơn hàng, số người dùng | Nhóm 4 |
| Order Management | Duyệt và quản lý trạng thái đơn hàng (Admin) | Nhóm 5 |
| Order History | Theo dõi lịch sử mua hàng và hóa đơn cá nhân | Nhóm 5 |
| Digital Library & Downloads | Quản lý kho sản phẩm đã sở hữu và mô phỏng tải tệp kỹ thuật số | Nhóm 5 |

### Quy trình mua hàng & Áp dụng ưu đãi

1. Khách hàng lựa chọn sản phẩm từ Cửa hàng (Store), xem đánh giá cộng đồng và chi tiết cấu hình.
2. Thêm sản phẩm vào giỏ hàng (Shopping Cart).
3. Kiểm tra giỏ hàng, áp dụng mã giảm giá (Voucher) để được khấu trừ tiền.
4. Hệ thống kiểm tra giá, trạng thái sản phẩm, hạn mức voucher và số dư ví tài khoản.
5. Xác nhận thanh toán 1-Click bằng số dư Ví (nếu thiếu tiền, chuyển sang nạp thêm vào ví).
6. Trừ tiền ví, tạo đơn hàng thành công và đánh dấu voucher đã sử dụng.
7. Cấp quyền sở hữu vĩnh viễn sản phẩm cho khách hàng.
8. Hiển thị sản phẩm trong Thư viện (Library) và cho phép tải xuống.

**Nguyên tắc nghiệp vụ:** Chỉ cấp quyền sở hữu sau khi thanh toán được xác nhận thành công. Người dùng không được tải xuống sản phẩm mà mình chưa có quyền truy cập.

---

## 4. Kiến trúc phần mềm

Dự án dự kiến sử dụng kiến trúc 3 lớp (3-Layer Architecture) nhằm tách biệt giao diện, nghiệp vụ và truy cập dữ liệu.

```text
DPMS
│
├── Presentation Layer
│   ├── Forms
│   ├── UserControls
│   └── UI Components
│
├── Business Logic Layer
│   ├── Services
│   ├── Validation
│   └── Business Rules
│
├── Data Access Layer
│   ├── Repositories
│   ├── SQL Queries
│   └── Database Connection
│
└── Database Layer
    └── Microsoft SQL Server
```

### Presentation Layer

Chịu trách nhiệm hiển thị giao diện, nhận thao tác người dùng và trình bày kết quả xử lý.

### Business Logic Layer

Xử lý nghiệp vụ, xác thực dữ liệu, tính toán tổng tiền, kiểm tra điều kiện mua hàng, xử lý thanh toán và cấp quyền sở hữu.

### Data Access Layer

Thực hiện các truy vấn SQL, kết nối cơ sở dữ liệu, ánh xạ dữ liệu và xử lý các thao tác đọc, ghi.

### Database Layer

Lưu trữ thông tin tài khoản, sản phẩm, giỏ hàng, đơn hàng, thanh toán, quyền sở hữu và các dữ liệu liên quan.

---

## 5. Thiết kế cơ sở dữ liệu dự kiến

Các bảng dữ liệu chính:

| Bảng (Tiếng Việt) | Mục đích | Phân hệ sử dụng |
|---|---|---|
| `NguoiDung` | Tài khoản, vai trò và số dư ví tài khoản | Auth / Profile / Wallet / UserMgmt |
| `DanhMuc` | Danh mục thể loại sản phẩm số | Store / ProductAdmin |
| `NhaPhatHanh` | Nhà phát hành / Studio sáng tạo | Store / ProductAdmin |
| `SanPham` | Thông tin sản phẩm kỹ thuật số, giá bán, phiên bản | Store / ProductDetail / ProductAdmin |
| `TepSanPham` | Metadata tệp kỹ thuật số và đường dẫn tương đối lưu tệp | ProductAdmin / Library |
| `GioHang` | Chi tiết giỏ hàng của từng tài khoản | Cart (Nhóm 2) |
| `MaGiamGia` | Mã giảm giá, loại giảm (%, tiền mặt), đơn tối thiểu | Cart / Voucher (Nhóm 2) |
| `MaGiamGiaNguoiDung` | Kho voucher người dùng đã thu thập và trạng thái sử dụng | Cart / Voucher (Nhóm 2) |
| `DonHang` | Thông tin đơn hàng và tổng tiền thanh toán | Cart / OrderHistory / OrderAdmin |
| `ChiTietDonHang` | Chi tiết sản phẩm trong đơn hàng và đơn giá lúc mua | OrderHistory / OrderAdmin |
| `ThanhToan` | Lịch sử giao dịch thanh toán ví và nạp tiền | Cart / Wallet (Nhóm 2) |
| `SoHuuSanPham` | Quyền sở hữu sản phẩm vĩnh viễn và lịch sử tải tệp | Library (Nhóm 5) |
| `DanhGia` | Đánh giá sao (1-5★) & nhận xét phản hồi sản phẩm | Store & Reviews (Nhóm 1) |

### Các nguyên tắc thiết kế

- Sử dụng khóa chính và khóa ngoại để duy trì tính toàn vẹn dữ liệu.
- Đặt ràng buộc duy nhất cho email và các cặp dữ liệu không được trùng lặp.
- Lưu giá sản phẩm tại thời điểm mua trong `ChiTietDonHang`.
- Sử dụng kiểu `DECIMAL` cho dữ liệu tiền tệ.
- Không xóa dây chuyền dữ liệu lịch sử đơn hàng khi sản phẩm ngừng kinh doanh.
- Sử dụng transaction cho các thao tác mua hàng cần cập nhật nhiều bảng.
- Lưu đường dẫn và metadata của tệp trong database thay vì lưu trực tiếp toàn bộ tệp lớn.

---

## 6. Giao diện dự kiến (15 Màn hình - 5 Nhóm nhiệm vụ)

Giao diện được định hướng theo phong cách cửa hàng kỹ thuật số hiện đại, ưu tiên khả năng sử dụng, tính nhất quán và trải nghiệm trực quan. Hệ thống bao gồm **15 màn hình chức năng**, phân đều cho 5 thành viên (mỗi người 3 màn hình / 3 chức năng độc lập):

### Nhóm 1: Cửa hàng, Chi tiết & Đánh giá sản phẩm (3 UI)
1. **Home / Store (UI 2):** Trang chủ, duyệt sản phẩm dạng thẻ, tìm kiếm và lọc danh mục.
2. **Product Details (UI 3):** Chi tiết sản phẩm, xem mô tả, ảnh lớn, thêm vào giỏ / mua ngay.
3. **Product Reviews & Ratings (UI 14 - Bổ sung):** Đánh giá sao (1-5★), viết nhận xét phản hồi, quản lý đánh giá của tôi & xem nhận xét cộng đồng.

### Nhóm 2: Mua hàng, Ví tài khoản & Mã giảm giá (3 UI)
4. **Shopping Cart (UI 4):** Giỏ hàng, sửa số lượng, xóa món và thanh toán 1-Click bằng ví.
5. **My Wallet / Top-up (UI 5):** Ví tài khoản (Steam Wallet), xem số dư và nạp tiền theo mệnh giá.
6. **Vouchers & Promotions (UI 15 - Bổ sung):** Kho mã giảm giá, thu thập khuyến mãi mới và áp dụng giảm tiền giỏ hàng.

### Nhóm 3: Quản trị Sản phẩm, Danh mục & Hồ sơ cá nhân (3 UI)
7. **Product Management (UI 10):** Quản lý danh sách sản phẩm (CRUD sản phẩm, giá, trạng thái kinh doanh).
8. **Category Management (UI 11):** Quản lý thể loại hàng hóa (CRUD danh mục).
9. **User Profile (UI 8):** Xem hồ sơ cá nhân, cập nhật thông tin và đổi mật khẩu.

### Nhóm 4: Xác thực, Quản lý tài khoản & Thống kê hệ thống (3 UI)
10. **Login / Register (UI 1):** Đăng nhập và đăng ký tài khoản.
11. **User Management (UI 12):** Danh sách người dùng, phân quyền Quản trị viên / Khách hàng (`QuanTriVien` / `KhachHang`) và khóa/mở tài khoản.
12. **Admin Dashboard (UI 9):** Báo cáo số liệu tổng quan (Tổng doanh thu, số đơn, lượng người dùng).

### Nhóm 5: Đơn hàng & Kho thư viện số (3 UI)
13. **Order Management (UI 13):** Quản trị viên duyệt và quản lý trạng thái các đơn hàng.
14. **Order History (UI 7):** Khách hàng tra cứu lịch sử mua hàng và hóa đơn cá nhân.
15. **My Library (UI 6):** Kho sản phẩm đã sở hữu của khách hàng và mô phỏng tải tệp.

Các màn hình được thiết kế dưới dạng `UserControl` WinForms, nhúng linh hoạt vào khung sườn chính `MainForm`.

---

## 7. Cấu trúc thư mục dự kiến

```text
DPMS/
│
├── DPMS.sln
│
├── DPMS.WinForms/
│   ├── Forms/
│   │   ├── Auth/
│   │   ├── Store/
│   │   ├── Cart/
│   │   ├── Library/
│   │   └── Admin/
│   ├── Controls/
│   ├── Assets/
│   └── Program.cs
│
├── DPMS.BLL/
│   └── Services/
│
├── DPMS.DAL/
│   ├── Repositories/
│   └── DbConnectionFactory.cs
│
├── DPMS.Shared/
│   ├── Models/
│   ├── DTOs/
│   └── Enums/
│
├── Database/
│   ├── 01_CreateDatabase.sql
│   ├── 02_CreateTables.sql
│   ├── 03_SeedData.sql
│   └── 04_ConstraintsAndIndexes.sql
│
├── Docs/
│   ├── Requirements.md
│   ├── DEVELOPMENT_PLAN.md
│   ├── ERD.png
│   └── TestCases.xlsx
│
└── README.md
```

Cấu trúc trên là định hướng ban đầu. Các project và thư mục sẽ được điều chỉnh trong quá trình phát triển để phù hợp với quy mô thực tế.

---

## 8. Roadmap phát triển

Dự án dự kiến triển khai trong 6 tuần.

- [ ] **Giai đoạn 1 — Phân tích và thiết kế**
  - Xác định yêu cầu và phạm vi.
  - Xây dựng use case và sơ đồ ERD.
  - Thiết kế database.
  - Khởi tạo solution và repository.

- [ ] **Giai đoạn 2 — Nền tảng hệ thống**
  - Kết nối SQL Server.
  - Xây dựng kiến trúc phân lớp.
  - Đăng ký, đăng nhập và phân quyền.
  - Xây dựng giao diện điều hướng.

- [ ] **Giai đoạn 3 — Cửa hàng và quản trị**
  - CRUD sản phẩm và danh mục.
  - Tìm kiếm, lọc và sắp xếp.
  - Hiển thị chi tiết sản phẩm.
  - Quản lý thông tin tệp kỹ thuật số.

- [ ] **Giai đoạn 4 — Mua hàng và thư viện**
  - Hoàn thiện giỏ hàng.
  - Tạo đơn hàng.
  - Thanh toán mô phỏng.
  - Cấp quyền sở hữu.
  - Xây dựng thư viện và tải xuống.

- [ ] **Giai đoạn 5 — Báo cáo và hoàn thiện**
  - Thống kê hoạt động kinh doanh.
  - Tối ưu giao diện.
  - Kiểm thử nghiệp vụ.
  - Xử lý lỗi và hoàn thiện tích hợp.

- [ ] **Giai đoạn 6 — Nghiệm thu**
  - Hoàn thiện báo cáo.
  - Chuẩn bị dữ liệu demo.
  - Viết hướng dẫn cài đặt và chạy ứng dụng.
  - Chuẩn bị kịch bản thuyết trình.

---

## 9. Phân công công việc dự kiến (Mỗi thành viên tối thiểu 3 chức năng)

| STT | Thành viên phụ trách | Nhóm phụ trách | Danh sách 3 Chức năng / UI chính | Trách nhiệm chính |
|:---:|---|---|---|---|
| 1 | *[Chưa phân công / Điền sau]* | **Nhóm 1:** Store & Reviews | 1. **Store / Trang chủ (UI 2)**: Duyệt sản phẩm, tìm kiếm, lọc thể loại.<br>2. **Product Details (UI 3)**: Xem chi tiết cấu hình, mô tả, mua hàng.<br>3. **Product Reviews (UI 14)**: Đánh giá sao, viết nhận xét, xem phản hồi cộng đồng. | Phân tích yêu cầu, thiết kế kiến trúc hệ thống, phát triển phân hệ Cửa hàng & Đánh giá sản phẩm |
| 2 | *[Chưa phân công / Điền sau]* | **Nhóm 2:** Cart & Wallet | 1. **Shopping Cart (UI 4)**: Quản lý giỏ hàng, cập nhật số lượng, thanh toán ví.<br>2. **My Wallet (UI 5)**: Xem số dư ví, nạp tiền các gói mệnh giá.<br>3. **Vouchers (UI 15)**: Quản lý mã giảm giá, áp dụng ưu đãi đơn hàng. | Thiết kế và phát triển luồng Giỏ hàng, mô phỏng Ví tài khoản & Hệ thống mã giảm giá |
| 3 | *[Chưa phân công / Điền sau]* | **Nhóm 3:** Product & Admin | 1. **Product Management (UI 10)**: CRUD sản phẩm, giá bán, trạng thái.<br>2. **Category Management (UI 11)**: CRUD danh mục thể loại hàng.<br>3. **User Profile (UI 8)**: Quản lý hồ sơ cá nhân và đổi mật khẩu. | Thiết kế và phát triển phân hệ Quản trị Hàng hóa, Thể loại và Hồ sơ người dùng |
| 4 | *[Chưa phân công / Điền sau]* | **Nhóm 4:** Auth & Dashboard | 1. **Login / Register (UI 1)**: Đăng ký, đăng nhập tài khoản, bảo mật.<br>2. **User Management (UI 12)**: Quản lý tài khoản, phân quyền, khóa tài khoản.<br>3. **Admin Dashboard (UI 9)**: Thống kê doanh thu, số đơn, người dùng. | Thiết kế và phát triển phân hệ Xác thực, Phân quyền và Báo cáo số liệu quản trị |
| 5 | *[Chưa phân công / Điền sau]* | **Nhóm 5:** Orders & Library | 1. **Order Management (UI 13)**: Quản lý và duyệt trạng thái đơn hàng.<br>2. **Order History (UI 7)**: Tra cứu lịch sử mua hàng, chi tiết đơn.<br>3. **My Library (UI 6)**: Kho sản phẩm kỹ thuật số sở hữu, mô phỏng tải file. | Thiết kế và phát triển phân hệ Đơn hàng, Lịch sử mua sắm và Kho thư viện số |

---

## 10. Kiểm thử và tiêu chí hoàn thành

Các nhóm chức năng cần được kiểm thử trước khi nghiệm thu:

- [ ] Đăng ký, đăng nhập và phân quyền (`QuanTriVien` / `KhachHang`) hoạt động đúng.
- [ ] CRUD sản phẩm và danh mục hoạt động đúng, dữ liệu phản ánh ngay lập tức.
- [ ] Tìm kiếm theo từ khóa và lọc sản phẩm theo thể loại chính xác.
- [ ] Đánh giá sao (1-5 sao) và gửi nhận xét sản phẩm (Reviews) thành công, cập nhật điểm đánh giá trung bình chính xác.
- [ ] Giỏ hàng thêm, sửa số lượng, xóa món không tạo dữ liệu trùng lặp ngoài ý muốn.
- [ ] Áp dụng mã giảm giá (Voucher) tính toán chính xác số tiền được chiết khấu và chặn mã không hợp lệ.
- [ ] Nạp tiền mô phỏng vào ví tài khoản cập nhật số dư chính xác.
- [ ] Thanh toán 1-Click bằng số dư ví kiểm tra điều kiện số dư: đủ thì trừ tiền & cấp quyền, thiếu thì chặn và thông báo.
- [ ] Đơn hàng tạo thành công lưu đầy đủ chi tiết mặt hàng và giá lúc mua trong SQL Server.
- [ ] Khách hàng chỉ xem và tải xuống sản phẩm đã thanh toán thành công trong Thư viện cá nhân.
- [ ] Dashboard hiển thị các con số tổng kết chính xác từ câu lệnh truy vấn SQL Server.
- [ ] Dữ liệu được lưu trữ, kiểm tra ràng buộc và truy xuất từ Microsoft SQL Server qua ADO.NET.
- [ ] Các lỗi nhập liệu, ngoại lệ dữ liệu và lỗi kết nối được bẫy bắt và xử lý phù hợp.
- [ ] Ứng dụng khởi chạy ổn định trên môi trường demo của nhóm.

---

## 11. Giới hạn của phiên bản đầu

Để phù hợp với quy mô bài tập lớn, DPMS không triển khai các chức năng đòi hỏi hạ tầng lớn trong phiên bản đầu:

- Không tích hợp thanh toán tiền thật.
- Không xây dựng hệ thống multiplayer hoặc cộng đồng trực tuyến.
- Không triển khai DRM hoặc cơ chế chống sao chép chuyên nghiệp.
- Không xây dựng hệ thống phân phối nội dung đa máy chủ.
- Không triển khai marketplace cho nhà phát hành bên ngoài trong MVP.

Việc mở rộng các chức năng này chỉ được xem xét sau khi các nghiệp vụ cốt lõi hoàn thiện.

## 12. Hướng phát triển

Trong các phiên bản tiếp theo, hệ thống có thể được mở rộng với:

- Wishlist và hệ thống đánh giá sản phẩm.
- Mã giảm giá và chương trình khuyến mại.
- Quản lý phiên bản sản phẩm.
- Báo cáo doanh thu bằng biểu đồ.
- Cổng quản lý dành cho nhà phát hành.
- Hệ thống cập nhật phiên bản và quản lý tải xuống nâng cao.
- Backend API để mở rộng sang nền tảng web hoặc mobile.

---

## 13. Kết luận

Digital Product Management System là dự án thực hành nhằm xây dựng một ứng dụng quản lý và phân phối sản phẩm kỹ thuật số bằng C# và .NET.

Thông qua dự án, nhóm hướng đến việc vận dụng kiến thức lập trình trực quan, lập trình hướng đối tượng, thiết kế cơ sở dữ liệu và tổ chức kiến trúc phần mềm vào một hệ thống có nghiệp vụ hoàn chỉnh.

**Mục tiêu cuối cùng:** xây dựng được một ứng dụng desktop có giao diện trực quan, dữ liệu nhất quán, quy trình mua hàng hoạt động đúng và có thể trình diễn trong buổi bảo vệ bài tập lớn.

---

*Digital Product Management System (DPMS) — C# Visual Programming Project.*
