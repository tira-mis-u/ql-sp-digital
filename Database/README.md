# CƠ SỞ DỮ LIỆU DPMS (MICROSOFT SQL SERVER)

> [!WARNING]
> **LƯU Ý QUAN TRỌNG:**  
> **Script SQL trong thư mục này hiện đang trong quá trình chạy TEST và nghiệm thử nghiệm nội bộ, CHƯA CHỐT thành CSDL chính thức của nhóm.**  
> Các thành viên cần chạy thử trên máy cá nhân, kiểm tra tính tương thích với code WinForms / ADO.NET của mình trước khi cả nhóm thống nhất phiên bản Release cuối cùng.

---

## 1. Mục đích của thư mục `Database/`
- Chứa toàn bộ kịch bản T-SQL tổng quát (`DPMS_Database_Master.sql`) để dựng database, tạo bảng, khóa chính, khóa ngoại, ràng buộc toàn vẹn và nạp dữ liệu mẫu (Seed Data) mô phỏng nền tảng Steam.
- Chuẩn hóa 100% theo **PascalCase Tiếng Việt Chuẩn Chuyên Ngành CNTT**.
- Phục vụ môi trường kiểm thử cho cả 5 nhóm (15 UI chức năng).

---

## 2. Danh sách 13 Bảng dữ liệu đang được thử nghiệm

| STT | Tên bảng (PascalCase) | Mục đích nghiệp vụ trong hệ thống | Phân hệ / Nhóm sử dụng chính |
|:---:|---|---|---|
| 1 | `NguoiDung` | Tài khoản, mật khẩu hash SHA-256, vai trò (`QuanTriVien`/`KhachHang`), số dư ví `SoDuVi`. | Toàn hệ thống (Nhóm 4 Auth, Nhóm 2 Ví, Nhóm 3 Profile) |
| 2 | `DanhMuc` | Thể loại sản phẩm (Game, Phần mềm, Đồ họa 3D...). | Nhóm 1 (Lọc Store), Nhóm 3 (Quản trị CRUD) |
| 3 | `NhaPhatHanh` | Nhà sáng tạo, Studio phát hành sản phẩm số. | Nhóm 1 (Chi tiết SP), Nhóm 3 (Gán Studio) |
| 4 | `SanPham` | Hàng hóa số: Tên, Mô tả, Giá bán, Ảnh bìa, Dung lượng, Phiên bản, Trạng thái. | Nhóm 1 (Storefront), Nhóm 3 (Admin Inventory) |
| 5 | `TepSanPham` | Metadata tệp số vật lý: Tên tệp, Đường dẫn tương đối, Định dạng, Byte, Mã hash SHA-256. | Nhóm 3 (Admin Upload), Nhóm 5 (Khách hàng Download) |
| 6 | `GioHang` | Giỏ hàng tạm thời của khách hàng. | Nhóm 2 (Giỏ hàng & Checkout 1-Click) |
| 7 | `MaGiamGia` | Hệ thống mã ưu đãi toàn sàn (Mã code, % giảm, tiền giảm, hạn dùng, đơn tối thiểu). | Nhóm 2 (Voucher & Chiết khấu hóa đơn) |
| 8 | `MaGiamGiaNguoiDung`| Kho Voucher cá nhân của từng khách hàng (Thu thập, đánh dấu đã dùng). | Nhóm 2 (UI 15 Kho Voucher cá nhân) |
| 9 | `DonHang` | Hóa đơn mua hàng: Mã code, Tạm tính, Tiền giảm, Tổng thanh toán, Trạng thái. | Nhóm 2 (Tạo đơn), Nhóm 5 (Quản trị & Lịch sử) |
| 10 | `ChiTietDonHang` | Danh sách sản phẩm mua trong từng đơn hàng, chốt giá tại thời điểm thanh toán. | Nhóm 2 (Ghi chi tiết), Nhóm 5 (Tra cứu chi tiết) |
| 11 | `ThanhToan` | Nhật ký giao dịch: Nạp tiền ví, Trừ tiền mua hàng (Mô hình Ví Steam). | Nhóm 2 (Biến động số dư ví), Nhóm 4 (Báo cáo) |
| 12 | `SoHuuSanPham` | Kho Thư viện sản phẩm đã sở hữu sau khi mua hàng thành công (My Library). | Nhóm 5 (Thư viện số & Tải file), Nhóm 2 (Cấp quyền) |
| 13 | `DanhGia` | Đánh giá sao (1-5★) và bình luận nhận xét của người dùng về sản phẩm. | Nhóm 1 (Đánh giá cộng đồng UI 14 & UI 3) |

---

## 3. Quy trình chạy thử nghiệm (Testing Workflow)

1. Mở công cụ **SQL Server Management Studio (SSMS)**.
2. Kết nối tới SQL Server cục bộ trên máy của bạn (VD: `localhost` hoặc `.\SQLEXPRESS`).
3. Mở file [DPMS_Database_Master.sql](DPMS_Database_Master.sql).
4. Nhấn phím `F5` hoặc bấm nút **Execute** để tạo CSDL `DPMS_Db` và nạp toàn bộ bảng cùng dữ liệu mẫu.
5. Kiểm tra kết quả:
   - Đảm bảo 13 bảng được tạo thành công không có lỗi syntax.
   - Thử các câu lệnh truy vấn mẫu trong thư mục `Docs/` để đánh giá tính tiện dụng khi viết code C# ADO.NET.

---

## 4. Kênh tiếp nhận phản hồi & Đóng góp chỉnh sửa
- Nếu thành viên nào trong 5 nhóm thấy cần:
  - Thêm cột / đổi kiểu dữ liệu cho thuận tiện viết form C#.
  - Thêm ràng buộc hoặc sửa đổi quan hệ bảng.
  - Tối ưu hóa câu lệnh query cho bài tập lớn.
- **Hành động:** Báo lên nhóm trao đổi hoặc ghi chú vào issue để cùng thống nhất điều chỉnh script `DPMS_Database_Master.sql` trước ngày nộp bài tập lớn!