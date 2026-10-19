# ĐẶC TẢ PHÁT BIỂU BÀI TOÁN HỆ THỐNG DPMS
**Dự án:** Digital Product Management System (DPMS)  
**Mô hình tham chiếu:** Nền tảng phân phối trò chơi và phần mềm số Steam (Valve Corporation)  
**Nền tảng công nghệ:** Windows Forms (.NET C#), Kiến trúc 3 tầng (3-Layer Architecture), Microsoft SQL Server  
**Phân loại tài liệu:** Tài liệu Phân tích Nghiệp vụ & Yêu cầu Hệ thống (Business Requirements Specification - BRS)

---

## 1. Bối cảnh và Tính cấp thiết của Bài toán

### 1.1. Bối cảnh chuyển dịch số
Trong kỷ nguyên số hóa, phương thức phân phối các sản phẩm phần mềm, trò chơi điện tử (games), tài nguyên số (digital assets, 3D models, graphics, audio, ebooks) đã chuyển dịch hoàn toàn từ hình thức vật lý (đĩa CD/DVD, USB, hộp đóng gói tại cửa hàng truyền thống) sang mô hình **phân phối kỹ thuật số trực tuyến (Digital Distribution Platforms)**. Các nền tảng tiêu biểu như Steam, Epic Games Store, GOG, Itch.io đã chứng minh tính hiệu quả vượt bậc về chi phí vận hành, tốc độ phát hành và khả năng tiếp cận người dùng toàn cầu.

### 1.2. Thách thức đặt ra đối với bài toán phân phối sản phẩm số
So với các hệ thống thương mại điện tử hàng hóa vật lý thông thường (quần áo, đồ gia dụng, đồ ăn), một hệ thống phân phối sản phẩm số đặt ra những bài toán nghiệp vụ và kỹ thuật hoàn toàn khác biệt:

1. **Khái niệm "Quyền sở hữu số" (Entitlement / Digital License):**
   - Hàng hóa số không có giới hạn tồn kho vật lý (không có khái niệm "hết hàng trong kho" đối với bản sao kỹ thuật số).
   - "Mua hàng" trong nền tảng số không phải là vận chuyển một kiện hàng đến cửa nhà khách, mà là **cấp quyền truy cập và sở hữu vĩnh viễn (Grant Ownership License)** cho tài khoản người dùng trong cơ sở dữ liệu.
2. **Loại bỏ quy trình logistics cồng kềnh:**
   - Không cần địa chỉ giao hàng, không tính phí vận chuyển (shipping fee), không có thời gian chờ giao hàng.
   - Luồng mua hàng đòi hỏi trải nghiệm **Thanh toán 1-Click tức thì (1-Click Instant Fulfillment)**: Ngay khi xác nhận giao dịch thành công, sản phẩm phải lập tức xuất hiện trong Kho Thư viện cá nhân của người dùng để sẵn sàng tải về.
3. **Bài toán lưu trữ và toàn vẹn tệp kỹ thuật số (Hybrid Storage & Data Integrity):**
   - Các bộ cài đặt trò chơi, phần mềm, mô hình 3D có dung lượng từ hàng trăm Megabyte đến hàng chục Gigabyte. Nếu lưu trữ trực tiếp các tệp nhị phân lớn này vào cơ sở dữ liệu quan hệ (SQL Server `VARBINARY(MAX)`) sẽ gây ra hiện tượng **phình to CSDL (Database Bloat)**, suy giảm nghiêm trọng hiệu năng truy vấn và gây khó khăn khi sao lưu/phục hồi.
   - Tệp phân phối qua mạng hoặc lưu trữ lâu dài có nguy cơ bị lỗi dữ liệu (bit rot) hoặc bị chỉnh sửa trái phép. Hệ thống bắt buộc phải có cơ chế **kiểm tra tính toàn vẹn (Integrity Verification)** bằng thuật toán băm mật mã học (Cryptographic Hash SHA-256).
4. **Mô hình tiền tệ nội bộ và kích cầu mua sắm:**
   - Người dùng cần một cơ chế thanh toán nhanh chóng, an toàn, không phải nhập lại thông tin thẻ ngân hàng ở mỗi lượt mua: Mô phỏng **Ví tài khoản (Steam Wallet Simulation)** với tính năng nạp tiền nhanh theo các gói mệnh giá.
   - Hệ thống khuyến mại số linh hoạt: Kho mã giảm giá (**Vouchers / Coupons**) với khả năng chiết khấu theo tỷ lệ phần trăm hoặc số tiền mặt trực tiếp trên giỏ hàng.
5. **Cộng đồng và tương tác chất lượng:**
   - Khách hàng cần một kênh độc lập để phản hồi về chất lượng sản phẩm: Hệ thống **Đánh giá & Nhận xét (Reviews & Ratings 1-5★)** bám sát nguyên tắc chống đánh giá ảo (chỉ tài khoản sở hữu sản phẩm mới được đăng nhận xét).

---

## 2. Mục tiêu Tổng thể của Hệ thống (DPMS)

Dự án **Digital Product Management System (DPMS)** được thiết kế và triển khai nhằm giải quyết trọn vẹn chuỗi giá trị phân phối sản phẩm số trên nền tảng ứng dụng máy bàn Windows (Desktop Application):

- **Đối với Khách hàng (End-Users):**
  - Cung cấp không gian Cửa hàng trực quan (Storefront) để tìm kiếm, phân loại theo thể loại, tra cứu thông tin chi tiết cấu hình và xem đánh giá cộng đồng.
  - Cung cấp trải nghiệm mua sắm nhanh gọn với Giỏ hàng thông minh, hỗ trợ thu thập và áp dụng Mã giảm giá.
  - Tích hợp Ví tài khoản cá nhân, cho phép nạp tiền mô phỏng và thanh toán 1-Click an toàn, minh bạch.
  - Quản lý kho game/sản phẩm đã mua trong Thư viện số (My Library), hỗ trợ tải tệp về máy tính có thanh tiến trình (ProgressBar) và tự động đối chiếu mã băm SHA-256 để đảm bảo tệp không bị lỗi.
  - Quản lý lịch sử giao dịch và hồ sơ cá nhân bảo mật (đổi mật khẩu băm SHA-256, tùy biến cấu hình hệ thống).

- **Đối với Quản trị viên (Administrators / Platform Operators):**
  - Cung cấp bảng điều khiển trung tâm (Admin Dashboard) hiển thị các chỉ số kinh doanh cốt lõi (KPIs): Tổng doanh thu thực tế, số lượng đơn hàng hoàn tất, số lượng thành viên, số lượng sản phẩm mở bán và danh sách Top sản phẩm bán chạy nhất.
  - Quản trị toàn diện vòng đời sản phẩm: Thêm mới, cập nhật giá bán, phiên bản, hình ảnh, ngưng kinh doanh (xóa mềm bảo toàn dữ liệu lịch sử).
  - Quản trị danh mục thể loại với ràng buộc toàn vẹn khóa ngoại (chặn xóa danh mục đang có sản phẩm liên kết).
  - Quản trị tải lên siêu dữ liệu tệp số (Digital Files Management) và tính toán mã băm SHA-256 tự động.
  - Quản lý và điều hành đơn hàng toàn hệ thống: Theo dõi trạng thái, xem chi tiết hóa đơn, duyệt/kích hoạt cấp quyền thư viện thủ công (cơ chế dự phòng khi có sự cố) hoặc hủy đơn hàng.
  - Quản lý người dùng: Tra cứu danh sách tài khoản toàn sàn, kiểm soát phân quyền (RBAC: `QuanTriVien` / `KhachHang`) và thực thi khóa/mở khóa tài khoản vi phạm.

---

## 3. Bản chất Nghiệp vụ Đặc thù của Sản phẩm Kỹ thuật số trong DPMS

Hệ thống DPMS thiết lập các nguyên tắc nghiệp vụ cốt lõi sau để đảm bảo tính đúng đắn của một nền tảng sản phẩm số:

| Tiêu chí | Cửa hàng thương mại vật lý | Nền tảng sản phẩm số DPMS |
|---|---|---|
| **Số lượng tồn kho (Inventory)** | Bị giới hạn vật lý trong kho; giảm dần khi xuất hàng; có nguy cơ hết hàng. | **Vô hạn bản sao**. Cột trạng thái `DangKinhDoanh` (1: Đang bán / 0: Ngưng bán) quyết định sản phẩm có được hiển thị hay không, không phụ thuộc số lượng tồn. |
| **Quy trình Giao nhận (Fulfillment)** | Đóng gói $\rightarrow$ Giao hàng $\rightarrow$ Thu tiền (COD) $\rightarrow$ Khách nhận hàng. | **Instant Entitlement**. Sau khi thanh toán ví thành công trong cùng 1 transaction, hệ thống tự động ghi bản ghi vào bảng `SoHuuSanPham`. Khách nhận game ngay tức khắc. |
| **Chi phí vận chuyển & Địa chỉ** | Bắt buộc nhập địa chỉ, số nhà, tỉnh thành, tính phí ship. | **Không có địa chỉ nhận hàng, phí vận chuyển luôn bằng 0đ**. |
| **Lưu trữ tệp sản phẩm** | Kho hàng vật lý (kệ, pallet). | **Mô hình Hybrid Storage**: Tệp `.zip`/`.exe` lưu tại thư mục hệ thống `Storage/Products/{MaSanPham}/`, CSDL chỉ lưu đường dẫn tương đối, định dạng, dung lượng byte và mã hash SHA-256. |
| **Kiểm tra tính toàn vẹn** | Kiểm tra bao bì, niêm phong tem vỡ. | **Đối chiếu mã băm mật mã học SHA-256**: Hệ thống tính toán hash của tệp nguồn lúc Admin upload và so sánh với tệp khi Khách hàng tải về máy. |
| **Hủy bán sản phẩm** | Thanh lý hoặc tiêu hủy hàng tồn. | **Xóa mềm (Soft Delete)**: Đặt `DangKinhDoanh = 0`. Các khách hàng đã mua trong quá khứ vẫn giữ nguyên quyền tải tệp trong Thư viện, các hóa đơn lịch sử không bị gãy khóa ngoại. |
| **Phương thức thanh toán** | COD, thẻ tín dụng, chuyển khoản ngoài. | **Mô phỏng Ví tài khoản (Steam Wallet Simulation)**: Số dư `SoDuVi` lưu trong bảng `NguoiDung`, nạp tiền tức thì qua các gói mệnh giá 50k - 1.000k và trừ tiền 1-Click. |

---

## 4. Xác định Phạm vi Tác nhân (Actor Boundary) của Hệ thống

Dựa trên cơ sở dữ liệu chuẩn ([Database/DPMS_Database_Master.sql](../../Database/DPMS_Database_Master.sql)) và mã nguồn phân quyền phiên ([qlspdigital/qlspdigital/Shared/UserSession.cs](../../qlspdigital/qlspdigital/Shared/UserSession.cs)), hệ thống xác lập đúng **3 tác nhân**:

```
                       ┌─────────────────────────────────────┐
                       │     KHÁCH VÃNG LAI (GUEST)          │
                       │ - Chưa đăng nhập                    │
                       │ - Duyệt Cửa hàng, Tìm kiếm, Đăng ký │
                       └──────────────────▲──────────────────┘
                                          │
                                          │ (Kế thừa quyền xem Store)
                                          │
                       ┌──────────────────┴──────────────────┐
                       │      KHÁCH HÀNG (CUSTOMER)          │
                       │ - Đã đăng nhập (VaiTro = KhachHang) │
                       │ - Giỏ hàng, Ví, Voucher, Đánh giá,  │
                       │   Thư viện số, Lịch sử mua hàng     │
                       └─────────────────────────────────────┘

                       ┌─────────────────────────────────────┐
                       │    QUẢN TRỊ VIÊN (ADMINISTRATOR)    │
                       │ - Đã đăng nhập (VaiTro = QuanTriVien)│
                       │ - Quản trị Hàng hóa, Tệp số, DM,    │
                       │   Duyệt đơn, Quản lý User, Dashboard│
                       └─────────────────────────────────────┘
```

1. **Khách vãng lai (`Guest`):**
   - Trạng thái hệ thống: `UserSession.IsLoggedIn == false`.
   - Quyền hạn: Duyệt danh mục sản phẩm dạng thẻ lưới tại Trang chủ Cửa hàng, tìm kiếm theo từ khóa, lọc theo danh mục thể loại, xem trang chi tiết mô tả và cấu hình, đọc các đánh giá của cộng đồng, thực hiện Đăng ký tài khoản mới (`RegisterControl`) hoặc Đăng nhập vào hệ thống (`LoginControl`).
2. **Khách hàng (`KhachHang` / Customer):**
   - Trạng thái hệ thống: `UserSession.IsLoggedIn == true` và `UserSession.CurrentRole == "KhachHang"`.
   - Quyền hạn: Kế thừa toàn bộ quyền của Khách vãng lai, đồng thời được mở khóa các phân hệ giao dịch: Quản lý giỏ hàng cá nhân, nạp tiền vào ví ảo, thu thập mã giảm giá (Voucher), thanh toán đơn hàng bằng số dư ví, sở hữu vĩnh viễn sản phẩm trong Thư viện số, tải tệp kỹ thuật số về máy tính kèm thanh tiến trình và kiểm tra mã băm SHA-256, viết/chỉnh sửa/xóa nhận xét đánh giá sản phẩm (1-5 sao), xem lịch sử mua sắm và hóa đơn chi tiết, cập nhật hồ sơ cá nhân, đổi mật khẩu và lưu cấu hình ứng dụng.
3. **Quản trị viên (`QuanTriVien` / Administrator):**
   - Trạng thái hệ thống: `UserSession.IsLoggedIn == true` và `UserSession.CurrentRole == "QuanTriVien"`.
   - Quyền hạn: Toàn quyền quản trị phân hệ Backoffice: Theo dõi chỉ số kinh doanh tổng quan tại Dashboard (doanh thu, đơn hàng, khách hàng, sản phẩm đang bán, top 5 bán chạy), thực hiện CRUD sản phẩm và upload tệp số kèm mã băm SHA-256, thực hiện CRUD danh mục thể loại kèm kiểm tra ràng buộc khóa ngoại, quản lý và điều hành trạng thái đơn hàng toàn sàn (duyệt đơn, kích hoạt cấp thư viện thủ công, hủy đơn), tra cứu danh sách người dùng toàn sàn, thực thi Khóa/Mở khóa tài khoản (ngăn chặn tự khóa chính mình), cập nhật hồ sơ cá nhân và đổi mật khẩu an toàn.
