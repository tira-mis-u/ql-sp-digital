# HỆ THỐNG ĐẶC TẢ PHÂN TÍCH BÀI TOÁN & NGHIỆP VỤ HỆ THỐNG DPMS
**Thư mục chuyên biệt:** `Docs/00_Business_Analysis/`  
**Dự án:** Digital Product Management System (DPMS) - Nền tảng phân phối sản phẩm số Windows Forms C#  
**Mục đích:** Cung cấp tài liệu nghiệp vụ tổng quan và chuyên sâu, tách biệt hoàn toàn với mã nguồn lập trình, phục vụ nghiên cứu, báo cáo học phần và bảo vệ đồ án trước hội đồng.

---

## 📌 CẤU TRÚC VÀ MỤC LỤC TÀI LIỆU TRONG THƯ MỤC NÀY

Thư mục này được tổ chức độc lập thành 4 tài liệu đặc tả nghiệp vụ cốt lõi:

| Thứ tự | Tên tài liệu | Nội dung trọng tâm | Đường dẫn xem chi tiết |
|:---:|---|---|---|
| **01** | **Phát biểu & Mô tả bài toán** | - Bối cảnh chuyển dịch số & mô hình phân phối Steam-like.<br>- Bản chất sản phẩm số: License sở hữu, không logistics, không tồn kho vật lý.<br>- Thách thức lưu trữ tệp số Hybrid Storage & kiểm tra toàn vẹn hash SHA-256.<br>- Xác định đúng 3 tác nhân hệ thống (Guest, Customer, Admin). | [`./01_PROBLEM_STATEMENT.md`](./01_PROBLEM_STATEMENT.md) |
| **02** | **Đặc tả nghiệp vụ chi tiết 5 nhóm** | - Chi tiết toàn bộ nghiệp vụ của 5 nhóm chức năng (15 màn hình `UserControl`).<br>- Quy trình xử lý cụ thể từng màn hình, luật nghiệp vụ (Business Rules).<br>- Ràng buộc dữ liệu, bẫy bắt lỗi và điều kiện biên của từng phân hệ. | [`./02_DETAILED_BUSINESS_RULES.md`](./02_DETAILED_BUSINESS_RULES.md) |
| **03** | **Tích hợp quy trình xuyên suốt** | - Sơ đồ tuần tự (Sequence Diagram) luồng Mua sắm & Cấp quyền sở hữu End-to-End.<br>- Luồng quản trị vòng đời sản phẩm số từ lúc tạo danh mục đến lúc khách tải game.<br>- 3 cơ chế tự phục hồi và xử lý ngoại lệ an toàn (Manual Override, Self-Lock Guard, Soft Delete). | [`./03_CROSS_MODULE_INTEGRATION.md`](./03_CROSS_MODULE_INTEGRATION.md) |
| **04** | **Khai thác chuyên sâu & Vấn đáp phòng thủ** | - Chu chuyển giá trị số, dòng tiền kế toán & vòng đời sở hữu số (Ownership Lifecycle).<br>- Khai thác sâu các ràng buộc CSDL ngầm, kiến trúc Hybrid Storage kép.<br>- **8 cơ chế nghiệp vụ kiểm toán chuyên sâu** (Snapshot giá, Voucher 2 cấp, Reactive Bus, Sổ cái...).<br>- **Bộ 31 câu hỏi vấn đáp phòng thủ "tử huyệt"** của giảng viên/hội đồng và lời giải trình đanh thép. | [`./04_IN_DEPTH_BUSINESS_SPEC_AND_DEFENSE_QA.md`](./04_IN_DEPTH_BUSINESS_SPEC_AND_DEFENSE_QA.md) |

---

## 🗺️ BẢN ĐỒ ÁNH XẠ GIỮA 5 NHÓM VÀ TÀI LIỆU KỸ THUẬT CHI TIẾT

Nếu cần tham khảo sâu hơn về thiết kế giao diện Wireframe hoặc cấu trúc mã nguồn 3 tầng (DTO, BLL Service, DAL Repository, SQL Script), bạn có thể chuyển hướng nhanh sang các thư mục thiết kế kỹ thuật của từng nhóm:

```text
Docs/
├── 00_Business_Analysis/           <--- THƯ MỤC NÀY (Phát biểu bài toán & Nghiệp vụ chuyên sâu)
│   ├── README.md                   # Mục lục và hướng dẫn tra cứu
│   ├── 01_PROBLEM_STATEMENT.md     # Phát biểu bài toán, bối cảnh, mô hình sản phẩm số Steam
│   ├── 02_DETAILED_BUSINESS_RULES.md # Nghiệp vụ chi tiết của 5 nhóm chức năng (15 màn hình)
│   ├── 03_CROSS_MODULE_INTEGRATION.md # Quy trình mua hàng xuyên suốt & xử lý ngoại lệ
│   └── 04_IN_DEPTH_BUSINESS_SPEC_AND_DEFENSE_QA.md # Khai thác chuyên sâu & 31 câu hỏi phòng thủ BTL (Kèm 8 cơ chế kiểm toán)
│
├── 01_Store_ProductDetails/        # [NHIỆM VỤ 1 - LÊ NGUYỄN MINH TRÍ]
│   ├── PRD.md                      # Cửa hàng (UI 2), Chi tiết SP (UI 3), Đánh giá 1-5★ (UI 14)
│   ├── UI_MOCKUP.md                # Bản vẽ giao diện ASCII & thuộc tính Controls
│   └── SYSTEM_DESIGN.md            # Kiến trúc 3 tầng, DTO, Sự kiện C# & SQL Reviews
│
├── 02_Cart_Wallet/                 # [NHIỆM VỤ 2 - NGUYỄN QUỐC TRUNG]
│   ├── PRD.md                      # Giỏ hàng (UI 4), Ví tài khoản (UI 5), Voucher ưu đãi (UI 15)
│   ├── UI_MOCKUP.md                # Bản vẽ giao diện ASCII & thuộc tính Controls
│   └── SYSTEM_DESIGN.md            # Kiến trúc 3 tầng, Transaction thanh toán ACID & CSDL ví
│
├── 03_Product_Category_Profile/    # [NHIỆM VỤ 3 - PHẠM TRUNG HIẾU]
│   ├── PRD.md                      # Quản lý SP & Tệp số (UI 10), Danh mục (UI 11), Hồ sơ (UI 8)
│   ├── UI_MOCKUP.md                # Bản vẽ giao diện ASCII & thuộc tính Controls
│   └── SYSTEM_DESIGN.md            # Kiến trúc 3 tầng, Hybrid Storage, Hash SHA-256 & Lệnh CRUD
│
├── 04_Auth_User_Dashboard/         # [NHIỆM VỤ 4 - TRẦN QUỐC THUẬN]
│   ├── PRD.md                      # Đăng nhập/Đăng ký (UI 1), Quản lý User (UI 12), Dashboard (UI 9)
│   ├── UI_MOCKUP.md                # Bản vẽ giao diện ASCII & thuộc tính Controls
│   └── SYSTEM_DESIGN.md            # Kiến trúc 3 tầng, Băm SHA-256, RBAC & SQL Aggregations
│
├── 05_Orders_Library/              # [NHIỆM VỤ 5 - PHẠM VĂN ĐAM]
│   ├── PRD.md                      # Duyệt đơn (UI 13), Lịch sử mua (UI 7), Thư viện số (UI 6)
│   ├── UI_MOCKUP.md                # Bản vẽ giao diện ASCII & thuộc tính Controls
│   └── SYSTEM_DESIGN.md            # Kiến trúc 3 tầng, Cấp quyền tức thì & Đối chiếu SHA-256
│
└── 06_Function_And_Button_Call_Flow/ # [TRA CỨU LUỒNG GỌI HÀM & NÚT BẤM 15 MÀN HÌNH + MAINFORM]
    ├── README.md                   # Kiến trúc Call Hierarchy & Dòng chu chuyển dữ liệu
    ├── 01_MAINFORM_AND_NAVIGATION_FLOW.md # Điều hướng Menu, Header & Phân quyền RBAC Guards
    ├── 02_NHOM1_STORE_AND_REVIEWS_FLOW.md # Chi tiết nút bấm Store, Thẻ Card & Đánh giá sao
    ├── 03_NHOM2_CART_WALLET_VOUCHERS_FLOW.md # Chi tiết nút bấm Giỏ hàng, Nạp ví & Kho Voucher
    ├── 04_NHOM3_PRODUCT_CATEGORY_PROFILE_FLOW.md # Chi tiết nút bấm Quản lý SP, File Hash, DM & Hồ sơ
    ├── 05_NHOM4_AUTH_USER_DASHBOARD_FLOW.md # Chi tiết nút bấm Login, Register, Khóa User & Dashboard
    ├── 06_NHOM5_ORDERS_AND_LIBRARY_FLOW.md # Chi tiết nút bấm Duyệt đơn, Tải PK-Zip & Bản quyền
    └── 07_TRACEABILITY_BUTTON_CALL_MATRIX.md # BẢNG MA TRẬN TRUY VẾT TỔNG THỂ (100% Nút Bấm -> Hàm -> SQL)
```
