# HỆ THỐNG ĐẶC TẢ LUỒNG GỌI HÀM & BẢN ĐỒ NÚT BẤM (BUTTON & FUNCTION CALL FLOW)
**Dự án:** Digital Product Management System (DPMS) - Nền tảng phân phối sản phẩm số Windows Forms C#  
**Thư mục tài liệu:** `Docs/06_Function_And_Button_Call_Flow/`  
**Mục tiêu:** Cung cấp tài liệu tra cứu toàn diện về mọi nút bấm trên 15 màn hình `UserControl` và khung sườn `MainForm`. Giải thích chi tiết nút bấm hoạt động thế nào, gọi hàm BLL/DAL nào, truy vấn SQL nào và hàm/sự kiện nào kích hoạt nó.

---

## 🏛️ KIẾN TRÚC PHÂN TẦNG VÀ DÒNG CHU CHUYỂN DỮ LIỆU (CALL HIERARCHY)

Mọi tương tác bấm nút của người dùng trên DPMS đều tuân thủ kiến trúc 3 tầng chuẩn mực (3-Tier Layered Architecture) kết hợp Bus sự kiện phản ứng (Event-Driven Reactive Bus):

```text
[ USER CLICK NÚT BẤM ]
         |
         v
+-----------------------------------------------------------------------------------+
| 1. TẦNG GIAO DIỆN (UI LAYER - Windows Forms UserControls)                         |
|    - Hàm bắt sự kiện: Btn*_Click(object? sender, EventArgs e)                     |
|    - Nhiệm vụ: Xác thực hợp lệ đầu vào (Input Validation), kiểm tra UserSession,  |
|      hiển thị Loading/Dialog xác nhận.                                            |
+-----------------------------------------------------------------------------------+
         |
         | Gọi phương thức nghiệp vụ (Truyền DTO / Nguyên thủy)
         v
+-----------------------------------------------------------------------------------+
| 2. TẦNG NGHIỆP VỤ (BLL LAYER - Business Logic Layer Services)                     |
|    - Lớp Service: SanPhamService, GioHangService, AuthService, DonHangService...  |
|    - Nhiệm vụ: Kiểm tra quyền (RBAC), kiểm tra điều kiện logic (Ví đủ tiền?       |
|      Đã sở hữu trong Thư viện? Voucher còn hạn?), tính toán chiết khấu.           |
+-----------------------------------------------------------------------------------+
         |
         | Gọi phương thức truy xuất dữ liệu
         v
+-----------------------------------------------------------------------------------+
| 3. TẦNG DỮ LIỆU (DAL LAYER - Data Access Layer Repositories)                      |
|    - Lớp Repository: SanPhamRepository, DonHangRepository, ViTaiKhoanRepository...|
|    - Nhiệm vụ: Mở SqlConnection, bắt đầu SqlTransaction (ACID), tham số hóa      |
|      SqlParameter chống SQL Injection, ánh xạ DataRow -> DTO.                     |
+-----------------------------------------------------------------------------------+
         |
         | Thực thi T-SQL / Stored Transaction
         v
+-----------------------------------------------------------------------------------+
| 4. HỆ QUẢN TRỊ CSDL (SQL SERVER - DPMS_Db)                                        |
|    - Bảng dữ liệu: SanPham, DonHang, ChiTietDonHang, ThanhToan, SoHuuSanPham...    |
+-----------------------------------------------------------------------------------+
         |
         | Trả về kết quả (Success / Error / Output Message)
         v
+-----------------------------------------------------------------------------------+
| 5. CẬP NHẬT GIAO DIỆN TƯỜNG MINH (EXPLICIT UI REFRESH)                            |
|    - Sau mỗi hành động: gọi trực tiếp mainForm.UpdateUserInterfaceSession()       |
|    - Không dùng static event (SessionChanged/BalanceChanged/CartUpdated đã xóa)   |
+-----------------------------------------------------------------------------------+
```

---

## 📂 DANH MỤC TÀI LIỆU CHI TIẾT TRONG THƯ MỤC NÀY

| Thứ tự | Tên tài liệu | Đối tượng phân tích | Nội dung trọng tâm |
|:---:|---|---|---|
| **01** | [`01_MAINFORM_AND_NAVIGATION_FLOW.md`](./01_MAINFORM_AND_NAVIGATION_FLOW.md) | Khung sườn ứng dụng `MainForm` | 14 nút Menu điều hướng, Nút Đăng nhập/Đăng xuất Header, Nút đổi Theme Tối/Sáng, Nhãn số dư ví, Bộ lọc quyền RBAC Guards (`EnsureLoggedIn`, `EnsureAdmin`). |
| **02** | [`02_NHOM1_STORE_AND_REVIEWS_FLOW.md`](./02_NHOM1_STORE_AND_REVIEWS_FLOW.md) | **Nhóm 1:** Lê Nguyễn Minh Trí<br>(UI 2, UI 3, UI 14, Thẻ Card) | Nút Tìm kiếm, Lọc danh mục, Thêm giỏ hàng từ thẻ game, Xem chi tiết, Gửi đánh giá sao, Xóa nhận xét và tự động tính lại `DiemTrungBinh`. |
| **03** | [`03_NHOM2_CART_WALLET_VOUCHERS_FLOW.md`](./03_NHOM2_CART_WALLET_VOUCHERS_FLOW.md) | **Nhóm 2:** Nguyễn Quốc Trung<br>(UI 4, UI 5, UI 15) | Nút 1-Click Thanh toán ví, Áp dụng Voucher, Nạp tiền ví các mệnh giá (50k-500k, tùy chọn), Thu thập Voucher cá nhân và Xóa giỏ hàng. |
| **04** | [`04_NHOM3_PRODUCT_CATEGORY_PROFILE_FLOW.md`](./04_NHOM3_PRODUCT_CATEGORY_PROFILE_FLOW.md) | **Nhóm 3:** Phạm Trung Hiếu<br>(UI 10, UI 11, UI 8) | Nút Chọn tệp số, Lưu/Sửa sản phẩm, Ngừng kinh doanh (Soft Delete), Quản lý danh mục, Đổi mật khẩu & Cài đặt người dùng. |
| **05** | [`05_NHOM4_AUTH_USER_DASHBOARD_FLOW.md`](./05_NHOM4_AUTH_USER_DASHBOARD_FLOW.md) | **Nhóm 4:** Trần Quốc Thuận<br>(UI 1, UI 1.1, UI 12, UI 9) | Nút Đăng nhập/Đăng ký, Khóa/Mở khóa tài khoản (Quy tắc Anti-Lockout Admin), Làm mới số liệu báo cáo KPI Dashboard & Top 5 bán chạy. |
| **06** | [`06_NHOM5_ORDERS_AND_LIBRARY_FLOW.md`](./06_NHOM5_ORDERS_AND_LIBRARY_FLOW.md) | **Nhóm 5:** Phạm Văn Đam<br>(UI 13, UI 7, UI 6) | Nút Kích hoạt đơn thủ công (Manual override), Hủy đơn hoàn tiền ví, Tải tệp nén PK-Zip bất đồng bộ và Sinh `LICENSE_AND_KEY.txt`. |
| **07** | [`07_TRACEABILITY_BUTTON_CALL_MATRIX.md`](./07_TRACEABILITY_BUTTON_CALL_MATRIX.md) | Toàn bộ 15 màn hình & MainForm | **Bảng Ma trận Truy vết Tổng thể (Master Traceability Matrix):** Liệt kê 100% nút bấm, hàm xử lý UI, Caller gọi nó, Hàm BLL, Hàm DAL, SQL executed và Event bắn ra. |

---

## 🎯 CÁCH SỬ DỤNG TÀI LIỆU KHI LÀM BÁO CÁO & BẢO VỆ ĐỒ ÁN

1. **Khi viết Chương 3 Báo cáo (Thiết kế chi tiết chức năng):**
   * Sao chép sơ đồ luồng dữ liệu của từng nút bấm từ tài liệu tương ứng của nhóm mình.
   * Trích dẫn chính xác tên hàm BLL/DAL và câu lệnh SQL mà nút bấm đó phụ trách.
2. **Khi giảng viên chỉ tay vào màn hình hỏi:** *"Nút này bấm vào thì code chạy vào đâu? Hàm nào xử lý? CSDL thay đổi ra sao?"*:
   * Mở ngay file [`07_TRACEABILITY_BUTTON_CALL_MATRIX.md`](./07_TRACEABILITY_BUTTON_CALL_MATRIX.md) hoặc file riêng của nhóm mình để đọc vanh vách:
     * Tên hàm UI Event: `Btn..._Click`
     * Hàm BLL: `...Service....`
     * Hàm DAL: `...Repository....`
     * Lệnh SQL: `UPDATE / INSERT / TRANSACTION...`
     * Sự kiện phát ra: `...Updated?.Invoke()`
