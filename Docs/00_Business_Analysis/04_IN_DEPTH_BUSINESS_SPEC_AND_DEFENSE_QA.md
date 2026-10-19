# TÀI LIỆU KHAI THÁC NGHIỆP VỤ CHUYÊN SÂU & HỆ THỐNG CÂU HỎI VẤN ĐÁP PHÒNG THỦ (IN-DEPTH BUSINESS SPEC & DEFENSE Q&A)
**Học phần:** Phân tích thiết kế hệ thống / Lập trình trực quan C#  
**Dự án:** Hệ thống Quản lý Phân phối Sản phẩm Số (Digital Product Management System - DPMS)  
**Phạm vi áp dụng:** Toàn bộ 5 nhóm chức năng (15 màn hình `UserControl`), CSDL SQL Server và Kiến trúc 3 tầng WinForms.

---

## MỤC LỤC TỔNG QUAN
1. [Mô hình Chu chuyển Giá trị Số & Dòng tiền (Digital Value Chain & Cashflow Lifecycle)](#1-mô-hình-chu-chuyển-giá-trị-số--dòng-tiền)
2. [Vòng đời Sở hữu Bản quyền Số (Digital Rights & Ownership Lifecycle)](#2-vòng-đời-sở-hữu-bản-quyền-số)
3. [Kiến trúc Lưu trữ Lai & Quy chuẩn Toàn vẹn Tệp (Hybrid Storage & Dual-hash Protocol)](#3-kiến-trúc-lưu-trữ-lai--quy-chuẩn-toàn-vẹn-tệp)
4. [Khai thác Nghiệp vụ Chi tiết 5 Nhóm & Bộ 25 Câu hỏi Vấn đáp Phòng thủ](#4-khai-thác-nghiệp-vụ-chi-tiết-5-nhóm--bộ-25-câu-hỏi-vấn-đáp-phòng-thủ)
   - [Nhóm 1: Mặt tiền Cửa hàng & Cộng đồng Đánh giá](#nhóm-1-mặt-tiền-cửa-hàng--cộng-đồng-đánh-giá-store--community-reviews)
   - [Nhóm 2: Thanh toán 1-Click, Ví Nội bộ & Khuyến mãi](#nhóm-2-thanh-toán-1-click-ví-nội-bộ--khuyến-mãi-cart-wallet--voucher)
   - [Nhóm 3: Quản trị Kho Tài nguyên Số & Hồ sơ](#nhóm-3-quản-trị-kho-tài-nguyên-số--hồ-sơ-product--repository-admin)
   - [Nhóm 4: Bảo mật, Quản trị Tài khoản & Phân tích KPI](#nhóm-4-bảo-mật-quản-trị-tài-khoản--phân-tích-kpi-security--dashboard)
   - [Nhóm 5: Đơn hàng, Cấp bản quyền & Thư viện Số](#nhóm-5-đơn-hàng-cấp-bản-quyền--thư-viện-số-orders--digital-library)
5. [Tổng kết Bảng So sánh Ma trận 5 Nhóm Đồ án](#5-tổng-kết-bảng-so-sánh-ma-trận-5-nhóm-đồ-án)
6. [Rà soát & Khai thác Chuyên sâu Logic Nghiệp vụ Người dùng Thực tế](#6-rà-soát--khai-thác-chuyên-sâu-logic-nghiệp-vụ-người-dùng-thực-tế)
7. [Bảng Ma trận Rủi ro Nghiệp vụ nếu Thiếu các Logic trên](#7-bảng-ma-trận-rủi-ro-nghiệp-vụ-nếu-thiếu-các-logic-trên)
8. [Hướng dẫn Sử dụng Tài liệu trong Thư mục Docs/00_Business_Analysis/](#8-hướng-dẫn-sử-dụng-tài-liệu-trong-thư-mục-docs00_business_analysis)
9. [Kiểm toán Nghiệp vụ Chuyên sâu & 8 Cơ chế Cốt lõi Tiềm ẩn (Audited Core Mechanisms)](#9-kiểm-toán-nghiệp-vụ-chuyên-sâu--8-cơ-chế-cốt-lõi-tiềm-ẩn)
   - [9.1. Kỹ thuật Snapshotting Giá lịch sử (Historical Price Immutability)](#91-kỹ-thuật-snapshotting-giá-lịch-sử-historical-price-immutability)
   - [9.2. Kiến trúc Tài trợ Voucher 2 Cấp (Platform vs Studio Subsidies)](#92-kiến-trúc-tài-trợ-voucher-2-cấp-platform-vs-studio-subsidies)
   - [9.3. Đồng bộ Giao diện Phản ứng theo Sự kiện (Event-Driven Reactive UI Bus)](#93-đồng-bộ-giao-diện-phản-ứng-theo-sự-kiện-event-driven-reactive-ui-bus)
   - [9.4. Sổ cái Kế toán Độc lập & Truy vết Gian lận (Financial Audit Ledger)](#94-sổ-cái-kế-toán-độc-lập--truy-vết-gian-lận-financial-audit-ledger)
   - [9.5. Rào chắn Giao dịch Toàn vẹn ACID 8 Bước (SqlTransaction Safeguard)](#95-rào-chắn-giao-dịch-toàn-vẹn-acid-8-bước-sqltransaction-safeguard)
   - [9.6. Cấu hình Trải nghiệm Người dùng & Phòng vệ Thao tác (Defensive UX & Settings)](#96-cấu-hình-trải-nghiệm-người-dùng--phòng-vệ-thao-tác-defensive-ux--settings)
   - [9.7. Quy tắc Phòng vệ Tối cao Chống Tự khóa Admin (Anti-Lockout Rule)](#97-quy-tắc-phòng-vệ-tối-cao-chống-tự-khóa-admin-anti-lockout-rule)
   - [9.8. Đóng gói Luồng Nén PK-Zip Tương thích Tuyệt đối (Resilient Stream Packaging)](#98-đóng-gói-luồng-nén-pk-zip-tương-thích-tuyệt-đối-resilient-stream-packaging)
   - [9.9. Bộ 6 Câu hỏi Phản biện Nâng cao về Cơ chế Kiến trúc](#99-bộ-6-câu-hỏi-phản-biện-nâng-cao-về-cơ-chế-kiến-trúc)

---

## 1. MÔ HÌNH CHU CHUYỂN GIÁ TRỊ SỐ & DÒNG TIỀN

Khác biệt cốt lõi giữa hệ thống bán lẻ hàng hóa vật lý và nền tảng phân phối số (như Steam, GOG, Epic Games) nằm ở **bản chất dòng tiền và vòng luân chuyển giá trị**:

```text
+------------------+         Nạp tiền        +-----------------------+
| Khách hàng       | ----------------------> | Ví tài khoản (SoDuVi) |
| (Customer)       |                         | [Công nợ giữ hộ]      |
+------------------+                         +-----------------------+
        |                                                |
        | Chọn sản phẩm (Quantity = 1)                   | 1-Click Checkout
        v                                                v
+------------------+       Khấu trừ Voucher  +-----------------------+
| Giỏ hàng         | ----------------------> | Đơn hàng (DaThanhToan)| ===> DOANH THU SÀN GHI NHẬN
| (Shopping Cart)  |                         +-----------------------+      (SUM TongTien)
+------------------+                                     |
                                                         | Cấp quyền tức thì (Instant Fulfillment)
                                                         v
                                             +-----------------------+
                                             | Thư viện sở hữu       |
                                             | (SoHuuSanPham)        | ===> BÀN GIAO LICENSE KEY
                                             +-----------------------+      & TẢI FILE SHA-256
```

### Nguyên tắc hạch toán tài chính trong DPMS:
1. **Tiền nạp ví là Công nợ (Liabilities):** Khi khách nạp 500.000đ vào ví, số tiền này chưa được hạch toán vào doanh thu của sàn mà là khoản "tiền gửi tạm giữ của khách".
2. **Doanh thu thuần phát sinh tại thời điểm Checkout:** Chỉ khi đơn hàng chuyển sang trạng thái `DaThanhToan`, doanh thu mới được ghi nhận vào hệ thống (`DonHang.TongTien`).
3. **Chiết khấu từ Voucher:** Được hạch toán như một khoản chi phí khuyến mại của sàn, giảm trừ trực tiếp vào số tiền trừ ví của khách nhưng giữ nguyên đơn giá niêm yết trong báo cáo chi tiết đơn.

---

## 2. VÒNG ĐỜI SỞ HỮU BẢN QUYỀN SỐ

Một sản phẩm số trong DPMS trải qua 6 trạng thái vòng đời nghiêm ngặt:

1. **Khám phá (Discovery):** Khách vãng lai và thành viên duyệt Store, xem thông số cấu hình và điểm đánh giá trung bình.
2. **Ủ quyền (Staging in Cart):** Khách hàng đưa sản phẩm vào giỏ. Hệ thống kiểm tra điều kiện tiên quyết: *Tài khoản chưa từng sở hữu sản phẩm này trong thư viện và giỏ hàng chưa có mục này*.
3. **Chốt quyền (Acquisition / Checkout):** Trừ tiền ví `NguoiDung.SoDuVi`, tạo bản ghi hóa đơn `DonHang` và sinh License Key duy nhất chèn vào `SoHuuSanPham`.
4. **Khai thác (Active Consumption):** Khách hàng truy cập `LibraryControl`, tải tệp cài đặt vật lý, đối soát mã băm SHA-256 và mở file `LICENSE_AND_KEY.txt` để lấy key bản quyền.
5. **Gỡ cài đặt cục bộ (Client-side Uninstall):** Khách bấm "Gỡ bỏ" chỉ để xóa file trên ổ cứng máy khách. **Quyền sở hữu trong CSDL không bị xóa**, khách được quyền tải lại bất kỳ lúc nào.
6. **Thu hồi quyền (Revocation / Dispute Refund):** Chỉ xảy ra khi Admin bấm "Hủy đơn & Hoàn tiền ví" tại `OrderManageControl` để giải quyết khiếu nại. Bản ghi `SoHuuSanPham` bị xóa hoặc vô hiệu hóa, tiền hoàn trả lại vào ví người dùng.

---

## 3. KIẾN TRÚC LƯU TRỮ LAI & QUY CHUẨN TOÀN VẸN TỆP

Hệ thống triển khai mô hình **Lưu trữ lai (Hybrid Storage Architecture)** nhằm giải quyết bài toán tải trọng tệp lớn:

* **Tại CSDL SQL Server:** Bảng `TepSanPham` chỉ lưu Metadata: `TenTep`, `DuongDanTep` (`Storage/Products/{fileName}`), `DinhDangTep` (`.zip`, `.exe`), `KichThuocByte` và `MaHashSHA256` (chuỗi 64 ký tự Hex).
* **Tại Hệ thống tệp OS:** Tệp nhị phân được lưu trữ phân tán tại thư mục ứng dụng `[AppDomain]/Storage/Products/`.
* **Giao thức Đối soát Kép (Dual-hash Verification Protocol):**
  1. *Khâu tải lên (Admin Upload):* Hệ thống đọc luồng `FileStream`, tính toán mã SHA-256 nguồn và lưu vào CSDL (`ProductManageControl.cs:L261`).
  2. *Khâu tải xuống (Client Download):* Hệ thống sao chép file bất đồng bộ (`Task.Run`), kiểm tra tính khớp nối của mã băm trước khi xuất thông báo "Đã kiểm tra SHA-256 an toàn" (`LibraryControl.cs:L220`).
  3. *Chứng chỉ đi kèm:* Tự động đóng gói file `LICENSE_AND_KEY.txt` trong tệp ZIP tải về (`DonHangService.cs:L168-171`).

---

## 4. KHAI THÁC NGHIỆP VỤ CHI TIẾT 5 NHÓM & BỘ 25 CÂU HỎI VẤN ĐÁP PHÒNG THỦ

---

### NHÓM 1: MẶT TIỀN CỬA HÀNG & CỘNG ĐỒNG ĐÁNH GIÁ (STORE & COMMUNITY REVIEWS)
> **Thành viên:** Lê Nguyễn Minh Trí  
> **3 Màn hình:** UI 2 (`StoreViewControl`), UI 3 (`ProductDetailControl`), UI 14 (`ProductReviewControl`)

#### A. Bài toán nghiệp vụ & Điểm khác biệt
* Hiển thị danh mục số không bị giới hạn số lượng tồn kho.
* Mặt tiền Cửa hàng phục vụ cả 2 đối tượng: Khách vãng lai (chưa đăng nhập) và Khách hàng thành viên.
* Hệ thống đánh giá đóng vai trò bảo chứng chất lượng sản phẩm (Social Proof), thúc đẩy quyết định mua hàng.

#### B. Killer Features & Điểm nhấn kỹ thuật
1. **Verified Purchase Reviews (BR1-3):** Chặn đứng hoàn toàn nạn spam review ảo. Chỉ tài khoản đã mua và sở hữu sản phẩm trong `SoHuuSanPham` mới được hiển thị Form gửi đánh giá.
2. **Tự động tái tính toán Điểm trung bình (`DiemTrungBinh`):** Tự động kích hoạt cập nhật lại điểm đánh giá trung bình của sản phẩm sau mỗi thao tác Thêm, Sửa hoặc Xóa nhận xét.
3. **Nhận diện trạng thái sở hữu theo thời gian thực:** Thẻ sản phẩm trên lưới Store tự động kiểm tra `UserSession` để chuyển đổi trạng thái hiển thị: nếu đã mua $\to$ hiện nhãn `Đã có trong thư viện` và vô hiệu hóa nút mua.

#### C. Ràng buộc ngầm (Hidden Constraints)
* **Khóa ràng buộc UNIQUE `(MaNguoiDung, MaSanPham)`:** Mỗi khách hàng chỉ được sở hữu **duy nhất 1 bài đánh giá** cho 1 sản phẩm. Người dùng muốn thay đổi nhận xét phải dùng chức năng Sửa hoặc Xóa, không thể đăng bài thứ 2.
* **Giới hạn điểm sao:** `SoSao` là số nguyên trong đoạn $[1, 5]$.

#### D. Bộ 5 câu hỏi vấn đáp phòng thủ "tử huyệt" & Cách trả lời:

* ❓ **Câu hỏi 1.1:** *"Tại sao bảng `SanPham` lại lưu cột `DiemTrungBinh` trong khi hoàn toàn có thể dùng `SELECT AVG(SoSao)` từ bảng `DanhGia` mỗi lần mở Store? Lưu vậy có vi phạm chuẩn hóa CSDL không?"*
  * 💡 **Trả lời:** *"Dạ thưa Thầy/Cô, đây là kỹ thuật **Phi chuẩn hóa có kiểm soát (Controlled Denormalization)** được áp dụng rộng rãi trong các hệ thống thương mại điện tử thực tế. Trang Cửa hàng có tần suất truy vấn đọc (Read-heavy) cực lớn từ hàng nghìn khách hàng. Nếu mỗi lần load trang Store ta đều phải `JOIN` và chạy hàm gom nhóm `AVG()` trên bảng `DanhGia` thì cơ sở dữ liệu sẽ bị nghẽn (Query Bottleneck). Do đó, lưu sẵn `DiemTrungBinh` ở bảng `SanPham` giúp câu lệnh load Store chỉ cần `SELECT * FROM SanPham`, đạt tốc độ tối đa. Cột này được đồng bộ tức thì mỗi khi có sự kiện Thêm/Sửa/Xóa đánh giá."*

* ❓ **Câu hỏi 1.2:** *"Khách vãng lai chưa đăng nhập bấm vào nút 'Đánh giá sản phẩm' thì hệ thống xử lý thế nào?"*
  * 💡 **Trả lời:** *"Hệ thống kiểm tra `UserSession.IsLoggedIn`. Nếu chưa đăng nhập, phần mềm hiện thông báo yêu cầu đăng nhập và chuyển hướng sang Form Đăng nhập. Sau khi đăng nhập, hệ thống tiếp tục kiểm tra bảng `SoHuuSanPham`, nếu tài khoản chưa từng mua sản phẩm này thì nút gửi đánh giá bị ẩn/khóa kèm thông báo 'Chỉ khách hàng đã sở hữu sản phẩm mới có quyền đánh giá'."*

* ❓ **Câu hỏi 1.3:** *"Nếu một sản phẩm đã có 50 bài đánh giá, sau đó Admin xóa sản phẩm đó thì các bài đánh giá xử lý ra sao?"*
  * 💡 **Trả lời:** *"Hệ thống áp dụng cơ chế **Soft Delete**: Sản phẩm đã có người mua và đánh giá thì Admin **không thể xóa vật lý** khỏi CSDL (bị chặn bởi khóa ngoại và kiểm tra logic BLL), mà chỉ được chuyển `TrangThai = Inactive (Ẩn)`. Do đó, dữ liệu đánh giá lịch sử vẫn được bảo toàn toàn vẹn."*

* ❓ **Câu hỏi 1.4:** *"Làm sao hệ thống ngăn chặn một người dùng spam 10 bài đánh giá 1 sao để dìm hàng một sản phẩm?"*
  * 💡 **Trả lời:** *"Bảng `DanhGia` thiết lập ràng buộc duy nhất `UNIQUE (MaNguoiDung, MaSanPham)`. Khi người dùng đã đánh giá một lần, giao diện `ProductReviewControl` sẽ tự động chuyển sang chế độ 'Chỉnh sửa đánh giá của bạn' với các nút Sửa/Xóa, câu lệnh SQL sẽ là `UPDATE` hoặc `DELETE`, hoàn toàn không thể thực hiện lệnh `INSERT` bài đánh giá thứ hai."*

* ❓ **Câu hỏi 1.5:** *"Khi tìm kiếm sản phẩm theo từ khóa, hệ thống tìm theo cơ chế nào? Có bị lỗi tiếng Việt có dấu không?"*
  * 💡 **Trả lời:** *"Hệ thống sử dụng mệnh đề `LIKE N'%keyword%'` trên cột kiểu `NVARCHAR`, hỗ trợ tìm kiếm chuỗi Unicode tiếng Việt có dấu hoàn toàn chính xác theo cả tên sản phẩm và nhà phát triển."*

---

### NHÓM 2: THANH TOÁN 1-CLICK, VÍ NỘI BỘ & KHUYẾN MÃI (CART, WALLET & VOUCHER)
> **Thành viên:** Nguyễn Quốc Trung  
> **3 Màn hình:** UI 4 (`CartControl`), UI 5 (`WalletControl`), UI 15 (`VoucherControl`)

#### A. Bài toán nghiệp vụ & Điểm khác biệt
* Phục vụ chu trình mua sắm tức thì không phụ thuộc cổng thanh toán trung gian bên ngoài.
* **Mô hình Ví Steam (Closed-loop Wallet):** Nạp tiền một lần, mua hàng bằng số dư ví chỉ với 1 cú click chuột, xóa bỏ rào cản nhập thông tin thẻ ngân hàng/OTP phức tạp.

#### B. Killer Features & Điểm nhấn kỹ thuật
1. **Cố định Số lượng = 1 (Zero Quantity Multiplier):** Sản phẩm số là bản quyền sử dụng cá nhân vĩnh viễn, không tồn tại khái niệm "mua 5 bản cùng 1 game".
2. **Chặn mua trùng lặp 2 lớp (Pre-purchase Ownership Guard):**
   * *Lớp 1 (Kho thư viện):* Chặn thêm vào giỏ nếu tài khoản đã sở hữu trong `SoHuuSanPham`.
   * *Lớp 2 (Giỏ hàng):* Chặn thêm món đã có sẵn trong giỏ.
3. **Bộ thẩm định Voucher 3 lớp:** Kiểm tra ngày hiệu lực (`NgayBatDau` - `NgayKetThuc`), kiểm tra giới hạn lượt dùng (`SoLuotDung < GioiHan`), kiểm tra giá trị đơn tối thiểu (`TongTien >= GiaTriToiThieu`) và áp dụng trần giảm tối đa (`GiamToiDa`).

#### C. Ràng buộc ngầm (Hidden Constraints)
* **Số dư khả dụng không âm:** `SoDuVi >= TongTienSauGiamGia`. Nếu thiếu tiền, hệ thống giữ nguyên giỏ hàng và mở Use Case `<extend> [Số dư ví không đủ]` để nạp bổ sung.
* **Voucher độc quyền:** Mỗi đơn hàng chỉ áp dụng tối đa 1 mã giảm giá có lợi nhất, không cho phép cộng dồn mã.

#### D. Bộ 5 câu hỏi vấn đáp phòng thủ "tử huyệt" & Cách trả lời:

* ❓ **Câu hỏi 2.1:** *"Tại sao trong tài liệu cũ có chỗ viết 'nút +/- sửa số lượng giỏ hàng', nhưng thực tế hệ thống lại khóa số lượng = 1?"*
  * 💡 **Trả lời:** *"Dạ thưa Thầy/Cô, đó là sự nhầm lẫn khi kế thừa khuôn mẫu của các đồ án bán hàng vật lý thông thường. Khi phân tích đúng bản chất nền tảng số như Steam hay Epic Games, mỗi tài khoản người dùng chỉ sở hữu 1 bản quyền duy nhất cho 1 sản phẩm. Việc cho phép tăng số lượng sản phẩm lên 2, 3 cái trên cùng 1 tài khoản là hoàn toàn phi thực tế. Nhóm đã chuẩn hóa: giỏ hàng cố định số lượng = 1 và thay vào đó là tính năng cao cấp hơn: **Kiểm tra và ngăn chặn mua trùng lặp sản phẩm đã sở hữu**."*

* ❓ **Câu hỏi 2.2:** *"Nếu một mã giảm giá chỉ còn 1 lượt dùng cuối cùng, mà có 2 khách hàng cùng áp dụng mã đó vào giỏ và bấm Thanh toán cùng một lúc thì xử lý thế nào?"*
  * 💡 **Trả lời:** *"Việc áp mã vào giỏ hàng chỉ mang tính chất 'tính thử số tiền chiết khấu' (Preview). Lượt sử dụng voucher chỉ thực sự bị trừ tại thời điểm thực thi giao dịch thanh toán trong CSDL. Câu lệnh cập nhật số lượt dùng có điều kiện chặn: `UPDATE MaGiamGia SET SoLuotDung = SoLuotDung + 1 WHERE MaGiamGia = @id AND SoLuotDung < GioiHan`. Ai bấm thanh toán trước người đó được chấp nhận; người bấm sau thì lệnh update trả về 0 dòng bị ảnh hưởng, hệ thống tự động thông báo 'Mã giảm giá vừa hết lượt sử dụng' và tính lại đơn hàng theo giá gốc mà không làm gián đoạn việc mua."*

* ❓ **Câu hỏi 2.3:** *"Khách hàng đang thanh toán mà ví không đủ tiền thì giỏ hàng có bị xóa không?"*
  * 💡 **Trả lời:** *"Tuyệt đối không! Hệ thống giữ nguyên trạng thái giỏ hàng và các sản phẩm đã chọn, thông báo số tiền còn thiếu và mở cửa sổ `WalletControl` để khách hàng nạp bù số tiền thiếu. Sau khi nạp xong, khách quay lại giỏ hàng và bấm Thanh toán ngay lập tức."*

* ❓ **Câu hỏi 2.4:** *"Tiền khách nạp vào ví có bị khấu trừ thuế hay phí gì không? Có rút ra tiền mặt được không?"*
  * 💡 **Trả lời:** *"DPMS triển khai mô hình ví điểm nội bộ khép kín (như Steam Wallet): Tiền nạp vào ví được quy đổi 1:1 sang đơn vị VNĐ để tiêu dùng trực tiếp trên sàn, không hỗ trợ rút ngược lại tiền mặt để đảm bảo mô hình kế toán đơn giản và tuân thủ quy định thanh toán số."*

* ❓ **Câu hỏi 2.5:** *"Khi khách hủy đơn hàng thì mã voucher đã dùng có được hoàn trả lại lượt dùng không?"*
  * 💡 **Trả lời:** *"Có. Trong thủ tục hủy đơn hàng và hoàn tiền của Admin tại Nhóm 5, hệ thống kiểm tra nếu đơn hàng có sử dụng voucher (`MaGiamGiaId IS NOT NULL`), hệ thống sẽ thực hiện hoàn lại 1 lượt dùng: `UPDATE MaGiamGia SET SoLuotDung = SoLuotDung - 1` để đảm bảo quyền lợi cho khách."*

---

### NHÓM 3: QUẢN TRỊ KHO TÀI NGUYÊN SỐ & HỒ SƠ (PRODUCT & REPOSITORY ADMIN)
> **Thành viên:** Phạm Trung Hiếu  
> **3 Màn hình:** UI 10 (`ProductManageControl`), UI 11 (`CategoryManageControl`), UI 8 (`ProfileControl`)

#### A. Bài toán nghiệp vụ & Điểm khác biệt
* Đóng vai trò "thủ kho" quản lý danh mục hàng hóa số và tệp nhị phân cài đặt.
* Giải quyết thách thức lưu trữ các tệp phần mềm/game dung lượng lớn bằng kiến trúc **Hybrid Storage**: Không làm phình to CSDL mà vẫn quản lý tập trung và an toàn.

#### B. Killer Features & Điểm nhấn kỹ thuật
1. **Tự động sinh mã băm SHA-256 khi Upload:** Khi Admin chọn file từ đĩa, hệ thống đọc luồng byte, tính chuỗi băm 64 ký tự Hex `MaHashSHA256` và kích thước byte, lưu vào bảng `TepSanPham` (`ProductManageControl.cs:L261`).
2. **Cơ chế Soft Delete (Bảo toàn quyền sở hữu của khách):** Tuyệt đối không xóa vật lý sản phẩm đã bán.
3. **Quản lý Avatar & Hồ sơ cá nhân:** Cập nhật thông tin tài khoản, đổi mật khẩu có kiểm tra đối chiếu mật khẩu cũ.

#### C. Ràng buộc ngầm (Hidden Constraints)
* **Bảo vệ toàn vẹn dữ liệu đơn hàng:** Nếu sản phẩm đã tồn tại trong `ChiTietDonHang` hoặc `SoHuuSanPham`, nút Xóa bị chặn, buộc Admin dùng chức năng Đổi trạng thái (`TrangThai = 0`).
* **Ràng buộc Danh mục có chứa sản phẩm:** Không cho phép xóa danh mục nếu đang có ít nhất một sản phẩm liên kết (`COUNT > 0`).

#### D. Bộ 5 câu hỏi vấn đáp phòng thủ "tử huyệt" & Cách trả lời:

* ❓ **Câu hỏi 3.1:** *"Tại sao không lưu trực tiếp file cài đặt (`VARBINARY(MAX)` / BLOB) vào bảng `TepSanPham` trong SQL Server mà lại lưu ra thư mục `Storage/Products/` rồi lưu đường dẫn vào DB?"*
  * 💡 **Trả lời:** *"Dạ thưa Thầy/Cô, đây là kiến trúc Hybrid Storage chuẩn mực trong công nghiệp. Tệp cài đặt phần mềm và game số có dung lượng rất lớn (từ hàng trăm MB đến hàng chục GB). Nếu lưu trực tiếp BLOB vào SQL Server sẽ làm kích thước file `.mdf` phình to dữ dội, làm cạn kiệt bộ nhớ RAM đệm (Buffer Pool) của database server, khiến các câu truy vấn thông thường bị chậm nghiêm trọng, đồng thời việc sao lưu (Backup) và phục hồi (Restore) CSDL sẽ mất hàng giờ đồng hồ. Lưu file vật lý riêng trên đĩa và chỉ lưu đường dẫn cùng mã băm SHA-256 trong DB giúp CSDL cực kỳ nhẹ và tối ưu tốc độ."*

* ❓ **Câu hỏi 3.2:** *"Nếu Admin tải lên một file cập nhật (Patch) mới cho game thì mã SHA-256 cũ xử lý ra sao?"*
  * 💡 **Trả lời:** *"Hệ thống sẽ chạy lại hàm `SecurityHelper.ComputeFileSha256` trên file mới, sau đó cập nhật đè (UPDATE) các trường `DuongDanTep`, `KichThuocByte`, `MaHashSHA256`, `PhienBan` và `NgayTaiLen` trong bảng `TepSanPham`. Khách hàng tải về sau này sẽ nhận được bản cập nhật mới nhất kèm mã xác thực mới."*

* ❓ **Câu hỏi 3.3:** *"Nếu người quản trị lỡ tay xóa một sản phẩm đã có 1.000 khách hàng mua thì CSDL sẽ xử lý thế nào?"*
  * 💡 **Trả lời:** *"CSDL thiết lập ràng buộc khóa ngoại `ON DELETE NO ACTION` giữa `SanPham` và các bảng `ChiTietDonHang`, `SoHuuSanPham`. Đồng thời ở tầng nghiệp vụ BLL, hệ thống kiểm tra nếu sản phẩm đã phát sinh giao dịch thì chặn lệnh xóa và hiển thị cảnh báo: 'Sản phẩm đã có người mua, bạn chỉ có thể chọn Ngừng kinh doanh (Ẩn sản phẩm)'. Điều này đảm bảo 1.000 khách hàng cũ vẫn giữ vĩnh viễn quyền sở hữu game trong Thư viện của họ."*

* ❓ **Câu hỏi 3.4:** *"Khi Admin tạo danh mục mới, làm sao chặn việc tạo trùng tên danh mục (ví dụ đã có 'Game Hành Động' lại tạo thêm 'game hành động')?"*
  * 💡 **Trả lời:** *"Trước khi `INSERT`, BLL gọi hàm kiểm tra `EXISTS (SELECT 1 FROM DanhMuc WHERE LOWER(TenDanhMuc) = LOWER(@ten))`. Nếu trùng tên, hệ thống ném ra ngoại lệ và báo lỗi ngay trên giao diện."*

* ❓ **Câu hỏi 3.5:** *"Khi đổi mật khẩu tài khoản ở màn hình Profile, cơ chế bảo mật hoạt động như thế nào?"*
  * 💡 **Trả lời:** *"Người dùng phải nhập đủ: Mật khẩu cũ, Mật khẩu mới và Xác nhận mật khẩu mới. Hệ thống dùng hàm `SecurityHelper.VerifyPassword(matKhauCu, matKhauHashTrongDB)` để xác thực danh tính. Chỉ khi mật khẩu cũ khớp và 2 ô mật khẩu mới giống nhau thì mới thực hiện băm SHA-256 mật khẩu mới và lưu vào CSDL."*

---

### NHÓM 4: BẢO MẬT, QUẢN TRỊ TÀI KHOẢN & PHÂN TÍCH KPI (SECURITY & DASHBOARD)
> **Thành viên:** Trần Quốc Thuận  
> **3 Màn hình:** UI 1 (`LoginControl` / `RegisterControl`), UI 12 (`UserManageControl`), UI 9 (`DashboardControl`)

#### A. Bài toán nghiệp vụ & Điểm khác biệt
* Bảo vệ cánh cổng đăng nhập hệ thống, kiểm soát phân quyền RBAC (Role-Based Access Control) cho 2 vai trò: `QuanTriVien` và `KhachHang`.
* Cung cấp trung tâm điều hành Dashboard đo lường các chỉ số tài chính và sức khỏe kinh doanh của sàn số.

#### B. Killer Features & Điểm nhấn kỹ thuật
1. **Mã hóa mật khẩu 1 chiều chuẩn SHA-256:** Tuyệt đối không lưu mật khẩu dạng bản rõ (Plain text) trong CSDL.
2. **Quy tắc Chống tự khóa tài khoản Admin (Self-Locking Guard):** Quản trị viên đang đăng nhập không thể tự bấm Khóa tài khoản của chính mình.
3. **Điều chỉnh số dư ví thủ công (Manual Balance Adjustment):** Hỗ trợ CSKH nạp tiền bù hoặc hoàn tiền cho khách khi giải quyết khiếu nại.
4. **Dashboard tổng hợp KPI Real-time bằng Native SQL:** Thống kê Doanh thu, Đơn hàng, Khách hàng, Sản phẩm mở bán và Top 5 sản phẩm bán chạy nhất bằng các hàm tổng hợp `SUM`, `COUNT`, `GROUP BY`.

#### C. Ràng buộc ngầm (Hidden Constraints)
* **Tính duy nhất của định danh:** `TenDangNhap` và `Email` là duy nhất trên toàn hệ thống (`UNIQUE`).
* **Thu hồi phiên khi bị khóa:** Nếu tài khoản bị Admin chuyển sang `TrangThai = 0` (Bị khóa), phiên làm việc của người dùng đó sẽ bị chặn ngay lập tức.

#### D. Bộ 5 câu hỏi vấn đáp phòng thủ "tử huyệt" & Cách trả lời:

* ❓ **Câu hỏi 4.1:** *"Mật khẩu băm SHA-256 có dịch ngược lại (Decrypt) ra mật khẩu ban đầu được không? Nếu không dịch ngược được thì làm sao kiểm tra đăng nhập?"*
  * 💡 **Trả lời:** *"SHA-256 là hàm băm mật mã học một chiều (One-way Cryptographic Hash Function), về mặt toán học không thể giải mã ngược lại chuỗi gốc ban đầu. Khi người dùng đăng nhập, hệ thống lấy mật khẩu người dùng vừa nhập, chạy qua hàm băm SHA-256 và so sánh chuỗi Hash vừa sinh ra với chuỗi Hash đang lưu trong CSDL. Nếu 2 chuỗi hash trùng khớp 100% thì đăng nhập thành công."*

* ❓ **Câu hỏi 4.2:** *"Doanh thu hiển thị trên Dashboard được tính như thế nào? Có bị tính trùng với tiền khách nạp vào ví không?"*
  * 💡 **Trả lời:** *"Dạ hoàn toàn không bị tính trùng. Doanh thu của sàn DPMS được tính bằng câu lệnh `SELECT SUM(TongTien) FROM DonHang WHERE TrangThai = N'DaThanhToan'`. Tiền khách nạp vào ví chỉ làm tăng số dư `SoDuVi` trong bảng `NguoiDung`, đó là tiền tạm giữ, chưa phát sinh việc chuyển giao bản quyền số nên chưa được tính là doanh thu. Doanh thu chỉ được ghi nhận khi phát sinh đơn hàng thực tế."*

* ❓ **Câu hỏi 4.3:** *"Nếu một Quản trị viên nổi giận và bấm Khóa tài khoản của chính mình thì hệ thống xử lý ra sao?"*
  * 💡 **Trả lời:** *"Hệ thống đã cài đặt cơ chế bảo vệ **Admin Self-Locking Guard** tại `UserManageControl`. Khi Admin bấm nút Khóa, hệ thống kiểm tra nếu `SelectedUserId == UserSession.CurrentUserId` thì chặn thao tác và hiển thị cảnh báo: 'Bạn không thể tự khóa tài khoản quản trị của chính mình'."*

* ❓ **Câu hỏi 4.4:** *"Top 5 sản phẩm bán chạy trên Dashboard được tính theo tiêu chí nào?"*
  * 💡 **Trả lời:** *"Hệ thống dùng câu lệnh `SELECT TOP 5 sp.TenSanPham, COUNT(ct.MaSanPham) AS SoLuongBan, SUM(ct.GiaBan) AS DoanhThu FROM ChiTietDonHang ct JOIN DonHang dh ON ct.MaDonHang = dh.MaDonHang JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham WHERE dh.TrangThai = N'DaThanhToan' GROUP BY sp.TenSanPham ORDER BY SoLuongBan DESC`."*

* ❓ **Câu hỏi 4.5:** *"Khi Admin tạo mới một tài khoản Khách hàng trong trang Quản lý người dùng, mật khẩu mặc định được cấp là gì?"*
  * 💡 **Trả lời:** *"Hệ thống cấp mật khẩu mặc định ban đầu là `123456` (được băm SHA-256 trước khi lưu) và yêu cầu người dùng phải đổi mật khẩu ở lần đăng nhập đầu tiên tại màn hình Profile."*

---

### NHÓM 5: ĐƠN HÀNG, CẤP BẢN QUYỀN & THƯ VIỆN SỐ (ORDERS & DIGITAL LIBRARY)
> **Thành viên:** Phạm Văn Đam  
> **3 Màn hình:** UI 13 (`OrderManageControl`), UI 7 (`OrderHistoryControl`), UI 6 (`LibraryControl`)

#### A. Bài toán nghiệp vụ & Điểm khác biệt
* Chịu trách nhiệm về khâu bàn giao giá trị cốt lõi: Cấp phát bản quyền số, sinh License Key duy nhất, quản lý Thư viện cá nhân vĩnh viễn và tải tệp kèm chứng chỉ số xác thực.

#### B. Killer Features & Điểm nhấn kỹ thuật
1. **Instant Fulfillment & Sinh License Key tự động:** Ngay khi thanh toán thành công, hệ thống sinh một mã khóa bản quyền ngẫu nhiên duy nhất (GUID dạng `DPMS-XXXX-YYYY-ZZZZ`) và chèn vào bảng `SoHuuSanPham`.
2. **Kích hoạt bù thủ công (Manual Fulfillment Fallback):** Admin có nút "Kích hoạt thủ công" tại `OrderManageControl` để cấp bù game vào Thư viện cho khách khi giao dịch gặp sự cố mạng hoặc tranh chấp.
3. **Tải tệp kèm xác thực SHA-256 & Đóng gói chứng chỉ số:**
   * Tải tệp bất đồng bộ có `ProgressBar` và đối soát mã băm (`LibraryControl.cs:L220`).
   * Tự động sinh file `LICENSE_AND_KEY.txt` chứa thông tin người mua, ngày giờ cấp phép, License Key và mã băm SHA-256 xác thực bản quyền (`DonHangService.cs:L168-171`).
4. **Theo dõi lượt tải (`SoLanTai`):** Tự động tăng biến đếm `SoLanTai = SoLanTai + 1` sau mỗi lần tải thành công.

#### C. Ràng buộc ngầm (Hidden Constraints)
* **Gỡ cài đặt Client không xóa quyền Server:** Nút `Gỡ bỏ` (`btnUninstall`) chỉ xóa file cài đặt trên máy người dùng, bản ghi trong `SoHuuSanPham` được bảo toàn vĩnh viễn.
* **Hoàn tiền đồng bộ:** Hủy đơn hàng sẽ thu hồi bản ghi `SoHuuSanPham` và hoàn tiền ví tương ứng.

#### D. Bộ 5 câu hỏi vấn đáp phòng thủ "tử huyệt" & Cách trả lời:

* ❓ **Câu hỏi 5.1:** *"Tại sao hệ thống thanh toán tự động 1-Click mà trong màn hình Quản lý đơn hàng của Admin lại có nút 'Kích hoạt thủ công'?"*
  * 💡 **Trả lời:** *"Trong thực tế vận hành các hệ thống thương mại điện tử, luôn có tỷ lệ nhỏ phát sinh ngoại lệ kỹ thuật (Edge Cases): ví dụ khách hàng bị trừ tiền ví nhưng đường truyền mạng socket bị ngắt đột ngột khiến tiến trình cấp game vào Thư viện bị treo ở trạng thái `DangXuLy`. Nút 'Kích hoạt thủ công' chính là **kịch bản ngoại lệ `<extend> [Giao dịch lỗi/Khiếu nại]`** cho phép Admin kiểm tra sao kê ví và cấp game cho khách bằng một cú click chuột ngay trên phần mềm mà không cần phải can thiệp thủ công bằng câu lệnh SQL nguy hiểm vào CSDL sản xuất."*

* ❓ **Câu hỏi 5.2:** *"Ý nghĩa thực tế của file `LICENSE_AND_KEY.txt` được tải về kèm bộ cài đặt là gì?"*
  * 💡 **Trả lời:** *"Đây là Giấy chứng nhận sở hữu số (Digital Certificate of Ownership) tương tự như chứng chỉ bản quyền của Microsoft, Steam hay Humble Bundle. Nó ghi nhận danh tính người mua, mã đơn hàng, ngày giờ cấp quyền, License Key để kích hoạt phần mềm và mã SHA-256 để người dùng tự kiểm tra tính toàn vẹn của tệp cài đặt."*

* ❓ **Câu hỏi 5.3:** *"Nếu khách hàng bấm nút 'Gỡ khỏi thư viện' (`btnUninstall`), hệ thống có xóa dòng dữ liệu trong bảng `SoHuuSanPham` không?"*
  * 💡 **Trả lời:** *"Tuyệt đối **KHÔNG**! Nút 'Gỡ bỏ' ở phía client chỉ làm nhiệm vụ xóa tệp cài đặt vật lý trên máy tính của người dùng nhằm giải phóng dung lượng ổ cứng. Quyền sở hữu trong bảng `SoHuuSanPham` trên máy chủ vẫn được lưu trữ vĩnh viễn. Khách hàng có thể mở lại Thư viện và bấm nút 'Tải xuống' bất kỳ lúc nào mà không phải trả thêm tiền."*

* ❓ **Câu hỏi 5.4:** *"Tiến trình tải file với thanh ProgressBar hoạt động như thế nào trong C# WinForms để giao diện không bị đơ (Not Responding)?"*
  * 💡 **Trả lời:** *"Hệ thống sử dụng cơ chế lập trình bất đồng bộ `async/await` kết hợp với `IProgress<int>` hoặc Action callback. Quá trình đọc/ghi luồng file nhị phân được thực thi trên luồng nền (Background Worker / Task), liên tục đẩy phần trăm hoàn thành về UI Thread để cập nhật giá trị `prgDownload.Value` và nhãn thông báo mà không làm đơ giao diện đồ họa."*

* ❓ **Câu hỏi 5.5:** *"Khi Admin bấm 'Hủy đơn hàng', quy trình hoàn tiền và thu hồi bản quyền diễn ra như thế nào?"*
  * 💡 **Trả lời:** *"Hệ thống thực thi một Transaction khép kín: (1) Cập nhật trạng thái `DonHang.TrangThai = N'DaHuy'`, (2) Cộng lại số tiền tương ứng vào ví khách `NguoiDung.SoDuVi = SoDuVi + @TongTien`, (3) Ghi nhận một giao dịch ví hoàn tiền trong `GiaoDichVi`, (4) Xóa hoặc vô hiệu hóa bản ghi bản quyền trong `SoHuuSanPham`. Khách hàng mở lại Thư viện sẽ thấy game đó biến mất và số dư ví tăng trở lại."*

---

## 5. TỔNG KẾT BẢNG SO SÁNH MA TRẬN 5 NHÓM ĐỒ ÁN

| Nhóm | Thành viên | Màn hình | Thực thể CSDL chính | Điểm nhấn kỹ thuật số | Ràng buộc nghiệp vụ ngầm |
| :---: | :---: | :---: | :---: | :---: | :---: |
| **1** | **Lê Nguyễn Minh Trí** | UI 2, UI 3, UI 14 | `SanPham`, `DanhGia` | Verified Purchase Reviews, Auto Recalc Rating | 1 review/user/SP; Chỉ người đã sở hữu mới được đánh giá |
| **2** | **Nguyễn Quốc Trung** | UI 4, UI 5, UI 15 | `GioHang`, `MaGiamGia`, `GiaoDichVi` | 1-Click Wallet Checkout, 3-Layer Voucher Validation | Quantity = 1; Chặn mua trùng SP đã có trong Library |
| **3** | **Phạm Trung Hiếu** | UI 10, UI 11, UI 8 | `SanPham`, `DanhMuc`, `TepSanPham` | Hybrid Storage, Tự động tính hash SHA-256 Upload | Soft Delete bảo vệ quyền khách; Ràng buộc danh mục có SP |
| **4** | **Trần Quốc Thuận** | UI 1, UI 12, UI 9 | `NguoiDung`, `VaiTro` | Mã hóa 1 chiều SHA-256, Native SQL KPI Aggregation | Chống Admin tự khóa mình; Tách biệt tiền ví và doanh thu |
| **5** | **Phạm Văn Đam** | UI 13, UI 7, UI 6 | `DonHang`, `ChiTietDonHang`, `SoHuuSanPham` | Auto License Key, Tải file đối soát SHA-256, Fallback Admin | Uninstall client không mất quyền server; Hoàn tiền đa bảng |

---

## 6. RÀ SOÁT & KHAI THÁC CHUYÊN SÂU LOGIC NGHIỆP VỤ NGƯỜI DÙNG THỰC TẾ

### 6.1. Nhóm 1 (Trí) - Logic Lọc đa tiêu chí & Xử lý Sản phẩm Miễn phí
* **Logic Lọc đa tiêu chí (Multi-facet Filtering Logic):**
  * *Hành vi người dùng:* Khách hàng không bao giờ tìm kiếm đơn lẻ. Họ vừa chọn Danh mục *"Game Hành Động"*, vừa gõ từ khóa *"Cyber"*, vừa lọc giá *"Dưới 500k"*, vừa yêu cầu sắp xếp theo *"Đánh giá cao nhất"*.
  * *Xử lý logic BLL/DAL:* Câu lệnh SQL sử dụng cơ chế tham số hóa an toàn chống SQL Injection:
    ```sql
    SELECT sp.*, dm.TenDanhMuc 
    FROM SanPham sp 
    JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
    WHERE sp.TrangThai = 1
      AND (@catId = 0 OR sp.MaDanhMuc = @catId)
      AND (@keyword = '' OR sp.TenSanPham LIKE N'%' + @keyword + N'%' OR sp.NhaPhatHanh LIKE N'%' + @keyword + N'%')
      AND (@maxPrice = 0 OR sp.GiaBan <= @maxPrice)
    ORDER BY sp.DiemTrungBinh DESC;
    ```
* **Logic Sản phẩm Miễn phí (`GiaBan = 0`):**
  * Khi `GiaBan = 0`, thẻ sản phẩm hiển thị nhãn `MIỄN PHÍ`. Khi khách bấm "Nhận ngay", hệ thống vẫn sinh một đơn hàng 0đ trong `DonHang` để lưu vết lịch sử giao dịch và cấp quyền vào `SoHuuSanPham` mà không trừ tiền ví.

### 6.2. Nhóm 2 (Trung) - Logic Giỏ hàng Xuyên phiên & Ma trận Chiết khấu Voucher có trần
* **Logic Giỏ hàng Xuyên phiên (Persistent Database Cart):**
  * Giỏ hàng không lưu trên RAM hay biến tạm; được lưu trực tiếp vào bảng `GioHang` gắn với `MaNguoiDung`. Khách hàng đăng nhập trên máy tính trường học, tắt app, về nhà mở máy cá nhân giỏ hàng vẫn còn nguyên vẹn.
* **Logic Tính chiết khấu Voucher có trần:**
  * Giả sử giỏ hàng có: Game A ($200.000$đ) + Game B ($400.000$đ) = $600.000$đ.
  * Voucher giảm 20%, đơn tối thiểu $300.000$đ, giảm tối đa $80.000$đ:
    $$600.000 \times 20\% = 120.000\text{đ} > 80.000\text{đ} \implies \text{Tiền giảm chốt: } \mathbf{80.000}\text{đ}$$
    $$\text{Khách phải thanh toán: } 600.000 - 80.000 = \mathbf{520.000}\text{đ}$$
* **Logic Nghẽn số dư khi Checkout:**
  * Nếu ví chỉ có $500.000$đ $\to$ Thiếu $20.000$đ. Hệ thống giữ nguyên giỏ hàng, mở popup nạp tiền ví hiển thị thông báo rõ ràng số tiền cần nạp bổ sung tối thiểu $20.000$đ.

### 6.3. Nhóm 3 (Hiếu) - Logic Ngừng kinh doanh (Deprecation) vs Cập nhật Bản vá (Patch)
* **Logic Ngừng kinh doanh (Soft Delete):**
  * Tựa game hết hạn hợp đồng bản quyền $\to$ Admin chuyển `TrangThai = 0`.
  * Khách mới: Không thể tìm thấy hoặc mua tựa game này trên Store.
  * Khách cũ đã mua trước đó: Vẫn truy cập `LibraryControl` bình thường, vẫn tải file cài đặt được 100%.
* **Logic Cập nhật Bản vá (Patch Update):**
  * Admin chọn file mới `Game_v1.1.zip` $\to$ Hệ thống băm lại SHA-256 mới, cập nhật phiên bản mới vào `TepSanPham` mà không xóa sản phẩm, không làm gián đoạn quyền sở hữu của khách cũ.

### 6.4. Nhóm 4 (Thuận) - Logic Thu hồi Phiên tức thì & Tách biệt Kế toán Doanh thu
* **Logic Thu hồi phiên tức thì (Session Eviction):**
  * Khi Admin bấm Khóa tài khoản (`TrangThai = 0`), ở lần bấm tiếp theo của người dùng (Thêm giỏ, Thanh toán, Đổi pass), BLL kiểm tra DB phát hiện bị khóa $\to$ Hủy ngay `UserSession`, đẩy về trạng thái Khách vãng lai kèm cảnh báo vi phạm.
* **Logic Kế toán Tiền ví vs Doanh thu:**
  * Tiền nạp ví là "Tiền gửi tạm giữ của khách", không được hạch toán là doanh thu.
  * Doanh thu chỉ được ghi nhận khi phát sinh giao dịch mua hàng: `SELECT SUM(TongTien) FROM DonHang WHERE TrangThai = N'DaThanhToan'`.

### 6.5. Nhóm 5 (Đam) - Logic Bàn giao Tức thì & Kích hoạt Bù Thủ công (Fallback)
* **Logic Bàn giao tức thì 1-Click:**
  * Thực thi Transaction khép kín: Trừ ví $\to$ Tạo DonHang $\to$ Tạo ChiTietDonHang $\to$ Sinh GUID License Key $\to$ Tạo SoHuuSanPham $\to$ Xóa Giỏ.
* **Logic Kích hoạt Bù thủ công (Manual Fulfillment Fallback) giải quyết khiếu nại trong 15s:**
  * Khách bị trừ tiền nhưng rớt mạng socket, đơn kẹt ở `DangXuLy`.
  * Khách khiếu nại $\to$ Admin mở `OrderManageControl`, kiểm tra sao kê ví $\to$ Bấm **"Kích hoạt thủ công"**.
  * Hệ thống đổi đơn sang `DaThanhToan`, sinh License Key và cấp game vào Thư viện ngay lập tức.

---

## 7. BẢNG MA TRẬN RỦI RO NGHIỆP VỤ NẾU THIẾU CÁC LOGIC TRÊN

| Phân hệ | Nhiệm vụ logic sống còn | Rủi ro nếu không xử lý logic này |
| :--- | :--- | :--- |
| **Nhóm 1 (Trí)** | Verified Reviews & Auto Recalc Rating | Bị spam review ảo, làm sai lệch điểm đánh giá uy tín của sản phẩm |
| **Nhóm 2 (Trung)** | Quantity = 1 & Chặn mua trùng lặp trong thư viện | Khách mua đè 2 bản quyền cho 1 tài khoản, phát sinh khiếu nại đòi tiền |
| **Nhóm 3 (Hiếu)** | Hybrid Storage & Soft Delete bảo vệ quyền khách | CSDL phình to làm sập SQL Server; xóa game làm mất quyền của khách cũ |
| **Nhóm 4 (Thuận)** | Băm SHA-256 mật khẩu & Tách biệt tiền ví với doanh thu | Lộ mật khẩu khi CSDL bị lộ; hạch toán sai lệch doanh thu kế toán sàn |
| **Nhóm 5 (Đam)** | Cấp License Key tức thì & Kích hoạt bù thủ công | Khách trả tiền không nhận được hàng; không có cơ chế xử lý khiếu nại CSKH |

---

## 8. HƯỚNG DẪN SỬ DỤNG TÀI LIỆU TRONG THƯ MỤC `Docs/00_Business_Analysis/`

1. **Khi viết Chương 1 Báo cáo (Bối cảnh & Hiện trạng):** Trích dẫn toàn bộ nội dung từ [`01_PROBLEM_STATEMENT.md`](./01_PROBLEM_STATEMENT.md).
2. **Khi viết Chương 2 & 3 Báo cáo (Yêu cầu chức năng SRS):** Trích dẫn các quy tắc nghiệp vụ BR1 - BR5 từ [`02_DETAILED_BUSINESS_RULES.md`](./02_DETAILED_BUSINESS_RULES.md).
3. **Khi vẽ Sơ đồ kiến trúc & Tích hợp quy trình:** Sử dụng các biểu đồ tuần tự Sequence Diagram trong [`03_CROSS_MODULE_INTEGRATION.md`](./03_CROSS_MODULE_INTEGRATION.md).
4. **Khi chuẩn bị bảo vệ & Ôn luyện vấn đáp trước hội đồng:** Học kỹ tài liệu này ([`04_IN_DEPTH_BUSINESS_SPEC_AND_DEFENSE_QA.md`](./04_IN_DEPTH_BUSINESS_SPEC_AND_DEFENSE_QA.md)), đặc biệt là **Mục 4 (Bộ 25 câu hỏi phản biện)**, **Mục 6 (Logic người dùng chuyên sâu)** và **Mục 9 (Các cơ chế kiểm toán nâng cao & Câu hỏi 26-31)**.

---

## 9. KIỂM TOÁN NGHIỆP VỤ CHUYÊN SÂU & 8 CƠ CHẾ CỐT LÕI TIỀM ẨN (AUDITED CORE MECHANISMS)

Qua quá trình rà soát và kiểm toán toàn diện mã nguồn (`qlspdigital/`) và CSDL (`DPMS_Database_Master.sql`), hệ thống DPMS sở hữu 8 cơ chế nghiệp vụ chuyên sâu mang tính sống còn mà sinh viên cần nắm vững để trả lời xuất sắc trước hội đồng chấm đồ án:

### 9.1. Kỹ thuật Snapshotting Giá lịch sử (Historical Price Immutability)
* **Vị trí CSDL & Mã nguồn:** Bảng `ChiTietDonHang` lưu trường `DonGia DECIMAL(18,2)` tách biệt với `SanPham.GiaBan`; thực thi tại `DonHangRepository.cs:L240-248`.
* **Bản chất nghiệp vụ:**
  * Khi khách hàng mua sản phẩm với giá $450.000$đ, hệ thống "chụp ảnh nhanh" (Snapshot) mức giá này vào `ChiTietDonHang.DonGia`.
  * Sau này, nếu Admin điều chỉnh giá game thành $600.000$đ hoặc tạo sự kiện giảm giá xuống $200.000$đ trong bảng `SanPham`, **toàn bộ hóa đơn cũ, lịch sử mua hàng cá nhân và báo cáo doanh thu tài chính quá khứ vẫn giữ nguyên $450.000$đ**.
  * Điều này đảm bảo nguyên tắc bất biến (Immutability) của chứng từ tài chính kế toán theo quy định thương mại điện tử.

### 9.2. Kiến trúc Tài trợ Voucher 2 Cấp (Platform vs Studio Subsidies)
* **Vị trí CSDL & Mã nguồn:** Bảng `MaGiamGia` với trường `MaNhaPhatHanh INT NULL`; logic kiểm tra tại `MaGiamGiaRepository.cs:L150-185`.
* **Bản chất nghiệp vụ:**
  * **Cấp 1 - Voucher Toàn sàn (`MaNhaPhatHanh IS NULL`):** Do nền tảng DPMS tự bỏ chi phí khuyến mại để thu hút người dùng mới (Ví dụ: `WELCOME2026`, `SUMMERSALE`). Áp dụng cho mọi danh mục sản phẩm.
  * **Cấp 2 - Voucher Nhà phát hành (`MaNhaPhatHanh IS NOT NULL`):** Do chính Studio/Nhà phát triển game tài trợ (Ví dụ: mã `PIXELSTUDIO10` do *Pixel Game Studio* tài trợ chiết khấu 10%). Tiền giảm trừ được trích từ phần doanh thu chia sẻ của nhà phát hành đó.
  * **Quy trình Thu thập Trước - Áp dụng Sau (Claim-then-Apply):** Khách hàng phải vào UI 5 (`VoucherControl`) bấm "Thu thập vào ví" để sinh bản ghi trong `MaGiamGiaNguoiDung`. Khi thanh toán tại UI 4 (`CartControl`), hệ thống xác thực `DaThuThap = 1` và `DaSuDung = 0`. Điều này ngăn chặn việc người dùng đoán mò mã code công khai khi chưa được cấp quyền nhận.

### 9.3. Đồng bộ Giao diện Phản ứng theo Sự kiện (Event-Driven Reactive UI Bus)
* **Vị trí Mã nguồn:** Các sự kiện tĩnh C# `event Action` phân tán tại BLL và Shared:
  * `UserSession.SessionChanged`: Đồng bộ trạng thái đăng nhập/đăng xuất, hoán đổi danh sách menu Admin/User trên `MainForm`.
  * `UserSession.BalanceChanged`: Tự động cập nhật số dư hiển thị tại Header (`lblBalance`) ngay khi khách nạp tiền hoặc thanh toán mà không cần tải lại form.
  * `GioHangService.CartUpdated`: Tự động nhảy Badge số lượng món trên nút menu "Giỏ hàng" khi bấm mua sản phẩm từ bất kỳ màn hình nào.
  * `MaGiamGiaService.VoucherUpdated`: Đồng bộ lại danh sách voucher còn hiệu lực và kho voucher cá nhân.
  * `SanPhamAdminService.ProductCatalogChanged`: Khi Admin thêm/sửa/đổi trạng thái game, thông báo ngay cho `StoreViewControl` làm mới danh mục.
  * `SanPhamAdminService.CategoryCatalogChanged`: Khi danh mục thay đổi, cập nhật ngay các ComboBox bộ lọc trên giao diện.
  * `SanPhamService.ReviewUpdated`: Khi có đánh giá mới, tự động tính lại điểm sao trung bình `DiemTrungBinh` của sản phẩm.
  * `DonHangService.LibraryUpdated`: Khi đơn hàng chuyển sang `Hoàn thành`, tự động thêm game vào `LibraryControl` và đổi nhãn nút mua trên Store sang "Đã sở hữu".
* **Ý nghĩa:** Tránh hoàn toàn việc WinForms bị "đơ" (lag) hoặc phải `Timer.Tick` quét CSDL liên tục (anti-pattern Polling).

### 9.4. Sổ cái Kế toán Độc lập & Truy vết Gian lận (Financial Audit Ledger)
* **Vị trí CSDL & Mã nguồn:** Bảng `ThanhToan` độc lập với bảng `DonHang`; thực thi tại `DonHangRepository.cs:L268-276` và `ViTaiKhoanRepository.cs`.
* **Bản chất nghiệp vụ:**
  * Bảng `DonHang` chỉ phản ánh giao dịch mua bán giỏ hàng số.
  * Bảng `ThanhToan` đóng vai trò là **Sổ cái tài chính (Audit Log)** ghi lại mọi sự dịch chuyển tiền tệ:
    1. `LoaiGiaoDich = N'Nạp tiền ví'`: Khách nạp tiền từ cổng mô phỏng vào ví (`MaDonHang = NULL`).
    2. `LoaiGiaoDich = N'Thanh toán đơn hàng'`: Khách chi trả cho một đơn hàng cụ thể (`MaDonHang = ID`).
    3. `LoaiGiaoDich = N'Hoàn tiền'`: Hệ thống hoàn lại tiền ví khi Admin hủy đơn giải quyết khiếu nại.
  * Giúp kiểm toán viên đối soát: `Tổng số dư thực tế trong NguoiDung.SoDuVi = Tổng Nạp - Tổng Chi + Tổng Hoàn`. Bất kỳ sự sai lệch nào cũng sẽ lập tức bị phát hiện.

### 9.5. Rào chắn Giao dịch Toàn vẹn ACID 8 Bước (SqlTransaction Safeguard)
* **Vị trí Mã nguồn:** `DonHangRepository.cs:L168-325` (`ThucHienThanhToanDonHang`).
* **Quy trình 8 bước nguyên tử khép kín:**
  1. *Bước 1 - Khóa & Kiểm tra:* Đọc số dư ví `NguoiDung.SoDuVi` trong transaction, đảm bảo đủ chi trả cho `TongThanhToan`.
  2. *Bước 2 - Khấu trừ số dư:* Cập nhật giảm `SoDuVi` của khách hàng.
  3. *Bước 3 - Định danh đơn hàng:* Sinh mã nghiệp vụ tiêu chuẩn quốc tế: `ORD-yyyyMMddHHmmss-XXXX`.
  4. *Bước 4 - Ghi nhận hóa đơn:* Chèn bản ghi vào bảng `DonHang` với trạng thái `Hoàn thành` và lấy `SCOPE_IDENTITY()`.
  5. *Bước 5 - Chi tiết hóa & Cấp quyền:* Lặp qua từng sản phẩm trong giỏ hàng để chèn vào `ChiTietDonHang` và cấp vào Thư viện số `SoHuuSanPham`.
  6. *Bước 6 - Ghi sổ cái tài chính:* Chèn bản ghi giao dịch `N'Thanh toán đơn hàng'` vào bảng `ThanhToan`.
  7. *Bước 7 - Tiêu hủy voucher:* Đánh dấu `DaSuDung = 1` trong `MaGiamGiaNguoiDung` và giảm `SoLuongConLai` trong `MaGiamGia`.
  8. *Bước 8 - Dọn dẹp giỏ hàng:* Xóa sạch các mục trong `GioHang` của người dùng.
  9. *Chốt hạ:* `trans.Commit()`.
* **Cơ chế phòng vệ:** Nếu phát sinh lỗi ở bất kỳ bước nào (ví dụ đứt kết nối mạng ở bước 5), lệnh `trans.Rollback()` lập tức hoàn nguyên toàn bộ: số dư ví không mất 1 xu, giỏ hàng giữ nguyên, không để lại dữ liệu rác mồ côi (Zero Orphan Records).

### 9.6. Cấu hình Trải nghiệm Người dùng & Phòng vệ Thao tác (Defensive UX & Settings)
* **Vị trí CSDL & Mã nguồn:** Bảng `CaiDatNguoiDung`; thực thi tại `HoSoRepository.cs:L155-202` và UI 11 (`ProfileControl`).
* **Các thuộc tính kiểm soát:**
  * `XacNhanThanhToan (BIT)`: Bật/tắt hộp thoại popup xác nhận trước khi thanh toán 1-Click. Giúp khách hàng tránh bị trừ tiền ngoài ý muốn do click chuột nhầm.
  * `TuDongCapNhat (BIT)`: Tùy chọn tự động đối soát phiên bản ứng dụng và bản vá tệp kỹ thuật số.
  * `GhiNhoDangNhap (BIT)`: Duy trì trạng thái thông tin tài khoản trên thiết bị cá nhân.
  * `ThongBaoEmail (BIT)`: Đăng ký nhận thông báo hóa đơn điện tử và mã kích hoạt qua hòm thư.

### 9.7. Quy tắc Phòng vệ Tối cao Chống Tự khóa Admin (Anti-Lockout Rule)
* **Vị trí Mã nguồn:** `NguoiDungRepository.cs:L64-70`.
* **Logic:** Khi Admin thực hiện thao tác khóa tài khoản tại UI 10 (`UserManageControl`), hệ thống kiểm tra:
  ```csharp
  if (UserSession.CurrentUserId == maNguoiDung && !trangThai)
  {
      thongBao = "Bạn không thể tự khóa tài khoản của chính mình!";
      return false;
  }
  ```
* **Ý nghĩa thực tế:** Ngăn chặn tuyệt đối tình huống Admin duy nhất vô tình bấm khóa chính tài khoản của mình, dẫn tới việc hệ thống bị mất quyền quản trị vĩnh viễn (System Lockout).

### 9.8. Đóng gói Luồng Nén PK-Zip Tương thích Tuyệt đối (Resilient Stream Packaging)
* **Vị trí Mã nguồn:** `DonHangService.cs:L78-180` (`TaiXuongTepAsync`).
* **Cơ chế chịu lỗi (Fault-tolerant Packaging):**
  * Sử dụng thư viện chuẩn `.NET` `System.IO.Compression.ZipArchive` với `CompressionLevel.Optimal`, định dạng chuẩn PK zip tương thích 100% với Windows Explorer, 7-Zip và WinRAR.
  * Ngay cả khi tệp thực thi vật lý của sản phẩm mẫu chưa có sẵn trên ổ cứng server, hệ thống **không báo lỗi sập ứng dụng (Crash 404)** mà tự động sinh tệp mô phỏng kèm theo chứng chỉ số độc bản `LICENSE_AND_KEY.txt` chứa đầy đủ: Tên sản phẩm, Mã đơn hàng, Mã khách hàng, Thời điểm cấp quyền và Mã băm SHA-256 xác thực.

---

### 9.9. BỘ 6 CÂU HỎI PHẢN BIỆN NÂNG CAO VỀ CƠ CHẾ KIẾN TRÚC

#### Câu 26: "Nếu Admin sửa giá sản phẩm trong quản trị thì các đơn hàng cũ của khách hàng có bị thay đổi số tiền không? Tại sao?"
* **Trả lời mẫu:**
  > "Dạ thưa Thầy/Cô, các đơn hàng cũ hoàn toàn không bị thay đổi số tiền! Hệ thống của nhóm em áp dụng kỹ thuật **Snapshotting giá lịch sử**: Bảng `SanPham` chỉ giữ giá bán hiện tại (`GiaBan`), còn khi khách đặt mua, mức giá tại đúng thời điểm đó được chụp lại và lưu cố định vào trường `DonGia` của bảng `ChiTietDonHang`. Mọi hóa đơn cũ, lịch sử đơn hàng và doanh thu quá khứ đều được tính dựa trên `ChiTietDonHang.DonGia`, đảm bảo tính bất biến của dữ liệu kế toán."

#### Câu 27: "Tại sao nhóm lại thiết kế bảng `ThanhToan` riêng biệt mà không dùng luôn bảng `DonHang` để xem doanh thu?"
* **Trả lời mẫu:**
  > "Dạ thưa Thầy/Cô, việc tách riêng bảng `ThanhToan` là quy chuẩn thiết kế hệ thống thanh toán số. Bảng `DonHang` chỉ đại diện cho giao dịch mua sản phẩm. Trong khi đó, hệ thống sử dụng ví tài khoản mô phỏng, phát sinh các dòng tiền không gắn với đơn hàng như giao dịch `Nạp tiền ví` (tăng số dư) hoặc `Hoàn tiền` (khi Admin hủy đơn). Bảng `ThanhToan` đóng vai trò là Sổ cái tài chính (Financial Ledger) để đối soát dòng tiền vào-ra của ví người dùng, đảm bảo cân bằng kế toán và phục vụ truy vết gian lận số dư."

#### Câu 28: "Hệ thống làm thế nào để đồng bộ số dư ví và giỏ hàng tức thì trên giao diện mà không cần tắt mở lại ứng dụng hay dùng Timer quét DB liên tục?"
* **Trả lời mẫu:**
  > "Dạ thưa Thầy/Cô, nhóm em xây dựng cơ chế **Event-Driven Reactive Bus** bằng các `event Action` tĩnh trong C#. Cụ thể, khi có giao dịch nạp tiền hoặc thanh toán, BLL phát sự kiện `UserSession.BalanceChanged` và `GioHangService.CartUpdated`. Lớp giao diện `MainForm` đã đăng ký lắng nghe các sự kiện này từ lúc khởi tạo (`SubscribeEvents`), do đó số dư ví và Badge giỏ hàng trên Header được cập nhật tức thì theo thời gian thực mà không tiêu tốn tài nguyên hệ thống như kỹ thuật Polling liên tục."

#### Câu 29: "Trong phương thức thanh toán giỏ hàng, nếu trừ tiền ví thành công nhưng khi cấp sản phẩm vào Thư viện lại bị mất kết nối CSDL thì hệ thống xử lý thế nào?"
* **Trả lời mẫu:**
  > "Dạ thưa Thầy/Cô, toàn bộ quy trình thanh toán 8 bước tại `DonHangRepository.ThucHienThanhToanDonHang` được đặt trong một khối giao dịch **ACID `SqlTransaction`**. Nếu việc thêm bản ghi vào `SoHuuSanPham` gặp sự cố, luồng xử lý sẽ nhảy vào khối `catch` và kích hoạt lệnh `trans.Rollback()`. Lệnh này hoàn nguyên toàn bộ các thao tác trước đó: số tiền ví vừa trừ sẽ được phục hồi nguyên vẹn, đơn hàng không được ghi nhận và giỏ hàng không bị xóa. Hệ thống đảm bảo tính toàn vẹn dữ liệu tuyệt đối (All or Nothing), khách hàng không bao giờ bị trừ tiền oan."

#### Câu 30: "Hệ thống có cơ chế gì để tránh việc Admin vô tình tự khóa tài khoản của chính mình rồi làm tê liệt quyền quản trị hệ thống?"
* **Trả lời mẫu:**
  > "Dạ thưa Thầy/Cô, tại hàm `CapNhatTrangThaiKhoa` trong `NguoiDungRepository`, nhóm em đã cài đặt quy tắc **Anti-Lockout Rule**: Hệ thống so sánh mã người dùng được yêu cầu khóa với mã tài khoản đang đăng nhập trong `UserSession.CurrentUserId`. Nếu trùng khớp và trạng thái yêu cầu là Khóa, hệ thống lập tức từ chối và trả về thông báo lỗi: *'Bạn không thể tự khóa tài khoản của chính mình!'*. Nhờ đó ngăn chặn triệt để nguy cơ Admin duy nhất tự khóa chính mình khiến hệ thống mất người điều hành."

#### Câu 31: "Tại sao người dùng không thể nhập trực tiếp một mã voucher ngẫu nhiên vào ô giảm giá nếu họ chưa vào trang Kho voucher để nhận?"
* **Trả lời mẫu:**
  > "Dạ thưa Thầy/Cô, nhóm em thiết kế mô hình voucher theo cơ chế **Claim-then-Apply (Thu thập trước, áp dụng sau)**. Khi khách áp dụng voucher, hệ thống kiểm tra bảng `MaGiamGiaNguoiDung` xem tài khoản đó đã lưu mã này chưa (`DaThuThap = 1`). Cơ chế này giúp sàn kiểm soát số lượng phát hành thực tế (`SoLuongConLai`), ngăn chặn tình trạng khách hàng dùng các công cụ bot cào quét hoặc tự đoán mò các mã ưu đãi độc quyền dành riêng cho từng sự kiện hay nhóm khách hàng nhất định."


