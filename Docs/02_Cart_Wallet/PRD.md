# PRD: PHÂN HỆ GIỎ HÀNG, VÍ TÀI KHOẢN & MÃ GIẢM GIÁ (CART, STEAM WALLET & VOUCHERS)
**Nhiệm vụ:** STT 2  
**Thành viên phụ trách:** Nguyễn Quốc Trung (Nhóm 2)  
**Màn hình phụ trách:**  
- **UI 4:** Giỏ hàng & Thanh toán 1-Click ([`../../qlspdigital/qlspdigital/Views/Nhom2_Cart/CartControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom2_Cart/CartControl.cs))
- **UI 5:** Quản lý Ví tài khoản & Nạp tiền mô phỏng ([`../../qlspdigital/qlspdigital/Views/Nhom2_Cart/WalletControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom2_Cart/WalletControl.cs))
- **UI 15:** Quản lý Mã giảm giá & Ưu đãi ([`../../qlspdigital/qlspdigital/Views/Nhom2_Cart/VoucherControl.cs`](../../qlspdigital/qlspdigital/Views/Nhom2_Cart/VoucherControl.cs))
**Mã nguồn nghiệp vụ & truy vấn:**
- BLL Service: [`../../qlspdigital/qlspdigital/BLL/Nhom2_Cart/GioHangService.cs`](../../qlspdigital/qlspdigital/BLL/Nhom2_Cart/GioHangService.cs)
- DAL Repository: [`../../qlspdigital/qlspdigital/DAL/Nhom2_Cart/GioHangRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom2_Cart/GioHangRepository.cs), [`../../qlspdigital/qlspdigital/DAL/Nhom2_Cart/ViTaiKhoanRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom2_Cart/ViTaiKhoanRepository.cs), [`../../qlspdigital/qlspdigital/DAL/Nhom2_Cart/MaGiamGiaRepository.cs`](../../qlspdigital/qlspdigital/DAL/Nhom2_Cart/MaGiamGiaRepository.cs)

---

## 1. Mục tiêu phân hệ
Áp dụng cơ chế **Số dư ví tài khoản (Steam Wallet)** tương tự nền tảng Steam kết hợp với **Hệ thống Mã giảm giá (Vouchers / Coupons)**: Khách hàng quản lý giỏ hàng, áp dụng các mã ưu đãi giảm trừ trực tiếp vào hóa đơn và thanh toán 1-Click bằng số dư ví. Khi số dư ví không đủ, khách hàng truy cập màn hình Ví tài khoản để nạp thêm tiền mô phỏng (chọn các gói mệnh giá nhanh 50k, 100k, 200k, 500k, 1.000k). Màn hình Kho Voucher giúp khách hàng quản lý các ưu đãi độc quyền, thu thập mã khuyến mãi mới và dọn dẹp voucher hết hạn.

Mô hình này giúp loại bỏ hoàn toàn màn hình Checkout rườm rà, phản ánh đúng thực tế của sản phẩm số (không cần địa chỉ nhận hàng, không cần phí ship) và giúp kịch bản demo bài tập lớn có logic xác thực số dư thật 100%.

---

## 2. Người dùng mục tiêu & Vai trò
- **Khách hàng đã đăng nhập:**
  - Quản lý giỏ hàng: kiểm tra danh sách game, sửa số lượng, xóa món.
  - Áp dụng mã giảm giá (Voucher) vào giỏ hàng để được khấu trừ tiền thanh toán.
  - Theo dõi số dư ví hiện tại ngay trên giỏ hàng và Header.
  - Bấm thanh toán ngay bằng số dư ví (1-Click Pay).
  - Nạp tiền vào ví bằng các gói mệnh giá nhanh.
  - Quản lý kho voucher: tra cứu ưu đãi khả dụng, nhập mã quà tặng/khuyến mãi mới, xóa mã hết hạn.

---

## 3. Đặc tả yêu cầu chức năng (Functional Requirements)

### 3.1. UI 4 - Giỏ hàng & Thanh toán 1-Click (`CartControl`)
1. **Hiển thị giỏ hàng (CRUD Read 1):**
   - Danh sách món trong giỏ: Tên sản phẩm, Thể loại, Đơn giá, Số lượng, Thành tiền, Nút xóa.
   - Thống kê chi tiết:
     + Tạm tính (Tổng tiền gốc các món trong giỏ).
     + Giảm giá Voucher (Số tiền được khấu trừ từ mã ưu đãi).
     + Tổng thanh toán thực tế = `Tạm tính - Giảm giá`.
   - Hiển thị số dư ví hiện tại của người dùng: `Số dư ví của bạn: 500.000 VNĐ`.
2. **Cập nhật số lượng & Xóa món (CRUD Update & Delete 1):**
   - Nút `+` / `-` để tăng/giảm số lượng món hàng trong `GioHang`.
   - Nút "Xóa" từng món khỏi `GioHang`.
   - Nút "Xóa tất cả".
3. **Áp dụng Mã giảm giá (Tích hợp với UI 15):**
   - Ô nhập mã voucher hoặc ComboBox chọn nhanh mã voucher khả dụng từ kho cá nhân.
   - Nút **[ Áp dụng ]**: Kiểm tra điều kiện đơn hàng tối thiểu và hạn dùng -> Tính toán số tiền giảm giá và cập nhật lại Tổng thanh toán.
   - Nút **[ Hủy mã ]**: Bỏ áp dụng voucher, trả về tổng tiền ban đầu.
4. **Thanh toán 1-Click bằng số dư ví (CRUD Create 2 cho BTL):**
   - Nút **[ ⚡ THANH TOÁN BẰNG SỐ DƯ VÍ ]**:
     + **Trường hợp 1 (Số dư >= Tổng thanh toán sau giảm):**
       - Trừ tiền ví tài khoản: `SoDuVi = SoDuVi - Tổng thanh toán`.
       - Tạo đơn hàng `DonHang` (`TrangThaiDonHang = N'Hoàn thành'`, `GhiChu = N'Thanh toán 1-Click bằng Ví'`, `TienGiamGia = @Discount`).
       - Đánh dấu voucher đã sử dụng trong `MaGiamGiaNguoiDung` (`DaSuDung = 1, NgaySuDung = GETDATE()`).
       - Cấp quyền vào kho `SoHuuSanPham` (sở hữu vĩnh viễn).
       - Xóa sạch giỏ hàng `GioHang`.
       - Hiển thị thông báo: *"Thanh toán thành công! Sản phẩm đã được thêm vào Thư viện. Số dư còn lại: X VNĐ"*.
       - Cập nhật lại số dư ví hiển thị trên Header `MainForm`.
     + **Trường hợp 2 (Số dư < Tổng thanh toán):**
       - Báo lỗi: *"Số dư ví không đủ (Bạn còn thiếu Y VNĐ)! Vui lòng nạp thêm tiền vào ví để mua sắm."*
       - Cung cấp nút bấm: *"Đi tới Nạp tiền ngay"* -> Chuyển sang UI 5 (`WalletControl`).

### 3.2. UI 5 - Quản lý Ví tài khoản & Nạp tiền (`WalletControl`)
1. **Hiển thị thông tin ví (CRUD Read 2):**
   - Tên chủ tài khoản, Username, Trạng thái ví.
   - Số dư khả dụng hiện tại (Hiển thị to, nổi bật).
2. **Nạp tiền mô phỏng vào ví (CRUD Update 2 cho BTL):**
   - Các nút bấm chọn nhanh gói mệnh giá:
     + `[ + 50.000 VNĐ ]`
     + `[ + 100.000 VNĐ ]`
     + `[ + 200.000 VNĐ ]`
     + `[ + 500.000 VNĐ ]`
     + `[ + 1.000.000 VNĐ ]`
   - Ô nhập số tiền tùy chọn (nếu muốn nạp con số khác).
   - Nút **[ 💰 XÁC NHẬN NẠP TIỀN VÀO VÍ ]**:
     + Thực hiện cộng tiền vào tài khoản: `UPDATE NguoiDung SET SoDuVi = SoDuVi + @Amount`.
     + Ghi nhận giao dịch nạp tiền vào bảng `ThanhToan`.
     + Thông báo: *"Nạp tiền thành công! Số dư mới của bạn là: X VNĐ"*.
     + Đồng bộ số dư lên Header của `MainForm`.
3. **Lịch sử biến động số dư (Đơn giản hóa):**
   - Hiển thị danh sách các lần nạp tiền và trừ tiền mua hàng từ bảng `ThanhToan` / `DonHang` (Thời gian, Loại giao dịch, Số tiền).

### 3.3. UI 15 - Quản lý Mã giảm giá & Ưu đãi (`VoucherControl` - Bổ sung cho Nhóm 2)
1. **Phân biệt 3 Trạng thái Voucher rõ ràng (Voucher Lifecycle):**
   - **Trạng thái 1: "Chưa lưu" (Chưa vào ví):** Mã ưu đãi hệ thống phát hành trong bảng `MaGiamGia` mà người dùng chưa thêm vào ví cá nhân. Xuất hiện trong bảng gợi ý ưu đãi để người dùng chủ động chọn **"Thu thập mã vào ví"**. Nếu người dùng bấm thẳng **"Dùng ngay (Áp dụng vào giỏ)"**, hệ thống tự động thực hiện thao tác kép: lưu vào ví và nạp mã vào giỏ hàng ngay lập tức.
   - **Trạng thái 2: "Đã trong ví" (Sẵn sàng sử dụng):** Mã đã được thu thập vào bảng `MaGiamGiaNguoiDung` với `DaSuDung = 0`. Người dùng có thể bấm **"Dùng ngay (Áp dụng vào giỏ)"** để chuyển sang giỏ hàng kích hoạt ưu đãi, hoặc bấm **"Xóa khỏi ví"** nếu không còn nhu cầu.
   - **Trạng thái 3: "Đã sử dụng" (Vô hiệu hóa):** Mã ưu đãi đã được khấu trừ thành công vào một đơn hàng thanh toán (`DaSuDung = 1`). Trạng thái này ngăn chặn người dùng tái sử dụng mã một cách bất hợp lệ trên các đơn hàng tiếp theo.
2. **Thu thập mã Voucher mới (CRUD Create 3):**
   - Ô nhập mã code: Nhập mã khuyến mãi (VD: `CHAOBANMOI`, `SALE10`, `STEAM50K`).
   - Nút **[ Thu thập mã vào ví ]**: Kiểm tra mã có tồn tại trong bảng hệ thống `MaGiamGia`, nếu hợp lệ thì thêm vào kho voucher cá nhân `MaGiamGiaNguoiDung`.
   - Thông báo: *"Thu thập voucher thành công! Bạn nhận được ưu đãi giảm X% cho đơn hàng tiếp theo."*
3. **Thao tác nhanh từ kho Voucher:**
   - Nút **[ Dùng ngay (Áp dụng vào giỏ) ]**: Tự động mở màn hình Giỏ hàng (UI 4) và áp dụng mã ưu đãi cho giỏ hàng hiện tại.
   - Nút **[ Xóa khỏi ví ]**: Xóa mã đã hết hạn hoặc không muốn giữ khỏi `MaGiamGiaNguoiDung`.
4. **Quy chuẩn nhãn nút bấm:**
   - Tuyệt đối không gắn emoji trên các nút hành động: dùng `Thu thập mã vào ví`, `Dùng ngay (Áp dụng vào giỏ)`, `Xóa khỏi ví`, `Xem ví Voucher giảm giá`.

---

## 4. Quy tắc nghiệp vụ (Business Rules)
1. **Ràng buộc số dư:** Số dư tài khoản không bao giờ được phép âm (`SoDuVi >= 0`).
2. **Điều kiện áp dụng Voucher:**
   - Mỗi đơn hàng chỉ áp dụng tối đa 1 mã giảm giá.
   - Tổng tiền tạm tính phải đạt giá trị tối thiểu (`TamTinh >= DonToiThieu`).
   - Mã voucher phải còn trong hạn sử dụng (`NgayKetThuc >= GETDATE()`) và chưa từng được tài khoản này sử dụng (`DaSuDung = 0`).
   - Voucher chỉ có thể sử dụng một lần duy nhất cho mỗi tài khoản, sau khi đơn hàng thanh toán thành công, voucher lập tức chuyển sang trạng thái "Đã sử dụng" và không thể kích hoạt lại.
3. **Tính toàn vẹn giao dịch (Transaction):** Thao tác trừ tiền ví, đánh dấu voucher đã dùng, tạo đơn hàng và cấp quyền thư viện phải nằm chung trong 1 Transaction SQL. Nếu có lỗi xảy ra, hệ thống tự động Rollback toàn bộ.
4. **Đồng bộ giao diện:** Khi số dư ví thay đổi (sau khi nạp tiền hoặc mua hàng), nhãn hiển thị số dư trên Header của `MainForm` phải được cập nhật ngay lập tức.

---

## 5. Tiêu chí nghiệm thu & Báo cáo BTL (Acceptance Criteria)
- [x] **CRUD 1 (Giỏ hàng):** Xem danh sách giỏ, sửa số lượng (Update), xóa món (Delete).
- [x] **CRUD 2 (Ví tài khoản):** Nạp tiền tăng số dư (Update `SoDuVi`), mua hàng trừ số dư (Update `SoDuVi`) và chặn thành công khi số dư không đủ.
- [x] **CRUD 3 (Kho Voucher - UI 15):** Hiển thị kho voucher (Read), thu thập mã ưu đãi mới (Create), tính toán giảm trừ hóa đơn chính xác khi áp dụng (Update `DaSuDung`), xóa voucher khỏi danh sách (Delete).
- [x] Kịch bản demo bài tập lớn mượt mà, thuyết phục và logic 100%.

---

## 6. Ma trận Đặc tả Nghiệp vụ CRUD Chuyên sâu (CRUD Matrix & Defense Rationale)

| Thực thể | **C** (Create) | **R** (Read) | **U** (Update) | **D** (Delete) |
|---|---|---|---|---|
| **Giỏ hàng** (`GioHang`) | Thêm SP từ Store (`ThemVaoGioHang`) | `dgvCart` hiển thị món hàng (`LayGioHang`) | Cộng dồn số lượng khi thêm trùng (`CapNhatSoLuong`) | `btnRemoveItem` (xóa món lẻ), `btnClearCart` (dọn giỏ) |
| **Voucher** (`MaGiamGiaNguoiDung`) | Nhập mã hoặc bấm `btnCollectSelected` | `dgvVouchers` phân loại 3 trạng thái | Cập nhật `DaSuDung = 1` khi đơn hàng thành công | `btnDelete` ("Xóa khỏi ví") qua `XoaVoucherCaNhan()` |
| **Ví Steam** (`SoDuVi`) | Nạp tiền vào ví (`NapTienVi`) | Hiển thị số dư real-time trên Header & Giỏ | Trừ tiền thanh toán đơn hàng | Không áp dụng (Số dư được bảo toàn lịch sử) |

> **Phòng thủ vấn đáp BTL:** Khẳng định với Hội đồng rằng phân hệ của Nguyễn Quốc Trung có **FULL CRUD** trên cả Giỏ hàng lẫn Kho Voucher cá nhân. Hệ thống áp dụng nguyên tắc **ACID Transaction** khi thanh toán: đồng thời trừ tiền ví, tạo hóa đơn, cấp bản quyền thư viện và xóa giỏ hàng chỉ trong 1 giao dịch an toàn tuyệt đối.

