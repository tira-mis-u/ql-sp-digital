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

### 2.1. Khách hàng (Customer)

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

### 2.2. Quản trị viên (Administrator)

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

| Module | Chức năng |
|---|---|
| Authentication | Đăng ký, đăng nhập, đăng xuất và phân quyền |
| Store | Hiển thị, tìm kiếm, lọc và sắp xếp sản phẩm |
| Product Details | Hiển thị thông tin, hình ảnh, giá và mô tả |
| Shopping Cart | Quản lý sản phẩm trong giỏ hàng |
| Order Management | Tạo đơn hàng và lưu lịch sử giao dịch |
| Payment Simulation | Mô phỏng quy trình thanh toán |
| Digital Library | Quản lý các sản phẩm đã sở hữu |
| Download Management | Kiểm tra quyền sở hữu và tải tệp |
| Product Management | CRUD sản phẩm |
| Category Management | CRUD danh mục |
| User Management | Quản lý tài khoản và trạng thái người dùng |
| Dashboard & Reports | Thống kê hoạt động kinh doanh |

### Quy trình mua hàng

1. Khách hàng lựa chọn sản phẩm.
2. Thêm sản phẩm vào giỏ hàng.
3. Kiểm tra giỏ hàng và xác nhận đơn hàng.
4. Hệ thống kiểm tra giá, trạng thái sản phẩm và quyền sở hữu.
5. Tạo đơn hàng và thực hiện thanh toán mô phỏng.
6. Nếu thanh toán thành công, cập nhật trạng thái đơn hàng.
7. Cấp quyền sở hữu sản phẩm cho khách hàng.
8. Hiển thị sản phẩm trong thư viện và cho phép tải xuống.

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

| Bảng | Mục đích |
|---|---|
| `Users` | Tài khoản và thông tin người dùng |
| `Categories` | Danh mục sản phẩm |
| `Publishers` | Nhà phát hành |
| `Products` | Thông tin sản phẩm |
| `ProductFiles` | Tệp kỹ thuật số và phiên bản |
| `CartItems` | Chi tiết giỏ hàng |
| `Orders` | Thông tin đơn hàng |
| `OrderItems` | Chi tiết sản phẩm trong đơn hàng |
| `Payments` | Thông tin thanh toán mô phỏng |
| `UserProducts` | Quyền sở hữu sản phẩm |
| `Reviews` | Đánh giá sản phẩm |
| `Favorites` | Danh sách sản phẩm yêu thích |

Các bảng như `Publishers`, `Reviews` và `Favorites` có thể được triển khai ở giai đoạn mở rộng nếu thời gian có hạn.

### Các nguyên tắc thiết kế

- Sử dụng khóa chính và khóa ngoại để duy trì tính toàn vẹn dữ liệu.
- Đặt ràng buộc duy nhất cho email và các cặp dữ liệu không được trùng lặp.
- Lưu giá sản phẩm tại thời điểm mua trong `OrderItems`.
- Sử dụng kiểu `DECIMAL` cho dữ liệu tiền tệ.
- Không xóa dây chuyền dữ liệu lịch sử đơn hàng khi sản phẩm ngừng kinh doanh.
- Sử dụng transaction cho các thao tác mua hàng cần cập nhật nhiều bảng.
- Lưu đường dẫn và metadata của tệp trong database thay vì lưu trực tiếp toàn bộ tệp lớn.

---

## 6. Giao diện dự kiến

Giao diện được định hướng theo phong cách cửa hàng kỹ thuật số hiện đại, ưu tiên khả năng sử dụng, tính nhất quán và trải nghiệm trực quan.

Các màn hình chính:

1. **Login / Register:** Đăng nhập và đăng ký.
2. **Home / Store:** Trang chủ và danh sách sản phẩm.
3. **Product Details:** Chi tiết sản phẩm.
4. **Shopping Cart:** Giỏ hàng.
5. **Checkout:** Xác nhận và thanh toán mô phỏng.
6. **My Library:** Thư viện sản phẩm đã sở hữu.
7. **Order History:** Lịch sử mua hàng.
8. **User Profile:** Thông tin cá nhân.
9. **Admin Dashboard:** Tổng quan hoạt động kinh doanh.
10. **Product Management:** Quản lý sản phẩm.
11. **Category Management:** Quản lý danh mục.
12. **User Management:** Quản lý người dùng.
13. **Order Management:** Quản lý đơn hàng.

Các màn hình có thể sử dụng `Form`, `UserControl`, `Panel`, `FlowLayoutPanel`, `DataGridView`, `PictureBox`, `ComboBox` và những control phù hợp của WinForms.

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

## 9. Phân công công việc dự kiến

| Thành viên | Trách nhiệm dự kiến |
|---|---|
| Phạm Trung Hiếu | Phân tích yêu cầu, thiết kế hệ thống và phối hợp tích hợp |
| Trần Quốc Thuận | Phát triển chức năng theo phân công của nhóm |
| Lê Nguyễn Minh Trí | Phát triển chức năng theo phân công của nhóm |
| Phạm Văn Đam | Phát triển chức năng theo phân công của nhóm |
| Nguyễn Quốc Trung | Phát triển chức năng theo phân công của nhóm |

*Phân công trên là khung ban đầu; nhiệm vụ cụ thể sẽ được thống nhất theo năng lực, tiến độ và khối lượng công việc của từng thành viên.*

---

## 10. Kiểm thử và tiêu chí hoàn thành

Các nhóm chức năng cần được kiểm thử trước khi nghiệm thu:

- [ ] Đăng ký, đăng nhập và phân quyền hoạt động đúng.
- [ ] CRUD sản phẩm và danh mục hoạt động đúng.
- [ ] Tìm kiếm và lọc sản phẩm chính xác.
- [ ] Giỏ hàng không tạo dữ liệu trùng ngoài ý muốn.
- [ ] Tổng tiền và giá tại thời điểm mua chính xác.
- [ ] Thanh toán thất bại không cấp quyền sở hữu.
- [ ] Thanh toán thành công tạo quyền sở hữu hợp lệ.
- [ ] Khách hàng chỉ tải xuống sản phẩm được phép truy cập.
- [ ] Dữ liệu đơn hàng và lịch sử mua hàng được bảo toàn.
- [ ] Dữ liệu được lưu và truy xuất từ SQL Server.
- [ ] Các lỗi nhập liệu và lỗi kết nối được xử lý phù hợp.
- [ ] Ứng dụng có thể khởi chạy trên môi trường demo đã chuẩn bị.

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
