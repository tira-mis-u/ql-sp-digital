# HỆ THỐNG TÀI LIỆU DỰ ÁN DPMS (DOCUMENTATION INDEX)

Tài liệu được phân cấp theo **5 Nhóm Nhiệm Vụ** tương ứng với 5 thành viên từ [`DEVELOPMENT_PLAN.md`](./DEVELOPMENT_PLAN.md).  
Mỗi thư mục đại diện cho một phân hệ độc lập, bao gồm trọn vẹn:
1. **PRD (Product Requirements Document):** Yêu cầu nghiệp vụ, luồng xử lý, đặc tả tính năng và tiêu chí nghiệm thu.
2. **UI Mockup & Wireframe:** Bản vẽ mô phỏng bố cục trực quan bằng sơ đồ ký tự (ASCII Art) cùng danh sách thuộc tính Controls WinForms để kéo thả trong Visual Studio.
3. **System Design:** Thiết kế kỹ thuật 3 tầng (UI -> BLL -> DAL -> SQL Server), cấu trúc DTO, các sự kiện C# và câu lệnh SQL truy vấn thực tế.

---

## Cây thư mục tài liệu

```text
Docs/
├── DEVELOPMENT_PLAN.md                     # Kế hoạch triển khai tổng thể & kiến trúc khung sườn MainForm
├── README.md                               # (Tài liệu này) Mục lục và hướng dẫn tra cứu
│
├── 01_Store_ProductDetails/                # [NHIỆM VỤ 1] Cửa hàng & Chi tiết sản phẩm
│   ├── PRD.md                              # Đặc tả yêu cầu: Store & Product Details
│   ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 2 (Store) & UI 3 (ProductDetail)
│   └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, DTO, Sự kiện C# & SQL truy vấn
│
├── 02_Cart_Wallet/                         # [NHIỆM VỤ 2] Giỏ hàng & Ví tài khoản (Steam Wallet)
│   ├── PRD.md                              # Đặc tả yêu cầu: Giỏ hàng, Thanh toán 1-Click & Nạp tiền ví
│   ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 4 (Cart) & UI 5 (Wallet/Nạp tiền)
│   └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, DTO, Kiểm tra số dư & Transaction SQL ví
│
├── 03_Product_Category_Profile/            # [NHIỆM VỤ 3] Quản trị Sản phẩm, Danh mục & Hồ sơ
│   ├── PRD.md                              # Đặc tả yêu cầu: CRUD Hàng hóa, Thể loại & User Profile
│   ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 10 (ProductMgmt), UI 11 (CategoryMgmt), UI 8 (Profile)
│   └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, Form nhập liệu & Lệnh CRUD SQL
│
├── 04_Auth_User_Dashboard/                 # [NHIỆM VỤ 4] Xác thực, Phân quyền & Quản trị thống kê
│   ├── PRD.md                              # Đặc tả yêu cầu: Đăng nhập/Đăng ký, Quản lý tài khoản & Thống kê
│   ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 1 (Auth), UI 12 (UserMgmt), UI 9 (Dashboard)
│   └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, Logic phân quyền Session & SQL thống kê SUM/COUNT
│
└── 05_Orders_Library/                      # [NHIỆM VỤ 5] Quản lý Đơn hàng & Kho thư viện số
    ├── PRD.md                              # Đặc tả yêu cầu: Duyệt đơn, Lịch sử mua hàng & Tải file sở hữu
    ├── UI_MOCKUP.md                        # Mô phỏng bố cục UI 13 (OrderAdmin), UI 7 (OrderHistory), UI 6 (MyLibrary)
    └── SYSTEM_DESIGN.md                    # Kiến trúc 3 lớp, Quản lý trạng thái đơn & Quyền sở hữu file
```

---

## Hướng dẫn sử dụng tài liệu cho từng thành viên

1. **Bước 1 - Đọc PRD:** Nắm rõ nghiệp vụ màn hình mình chịu trách nhiệm, các nút bấm cần làm gì và kịch bản trả lời khi giảng viên hỏi vấn đáp.
2. **Bước 2 - Xem UI Mockup:** Mở file `UI_MOCKUP.md`, xem sơ đồ màn hình và mở Visual Studio kéo thả các Controls WinForms theo đúng bảng thuộc tính gợi ý (tên nút, thuộc tính Dock/Anchor).
3. **Bước 3 - Xem System Design:** Viết code theo mẫu kiến trúc 3 lớp đã định nghĩa sẵn: tạo DTO, viết câu lệnh SQL trong DAL, xử lý logic trong BLL và gọi lên WinForms Event.
