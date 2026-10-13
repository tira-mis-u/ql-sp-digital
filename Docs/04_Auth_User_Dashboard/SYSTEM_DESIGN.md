# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 4
**Màn hình:** UI 1 (`LoginControl`, `RegisterControl`), UI 12 (`UserManageControl`), UI 9 (`DashboardControl`)  
**Mô hình áp dụng:** Kiến trúc 3 tầng (Presentation - BLL - DAL - SQL Server)

---

## 1. Kiến trúc tổng thể phân hệ

```text
+-------------------------------------------------------------+
|                     PRESENTATION LAYER                      |
|  - LoginControl.cs            - RegisterControl.cs          |
|  - UserManageControl.cs       - DashboardControl.cs         |
+------------------------------+------------------------------+
                               | Xác thực & Quản trị User & Thống kê
+------------------------------v------------------------------+
|                    BUSINESS LOGIC (BLL)                     |
|  - AuthService.cs             - UserManageService.cs        |
|  - DashboardService.cs        - Session Manager             |
+------------------------------+------------------------------+
                               | Gọi ADO.NET
+------------------------------v------------------------------+
|                    DATA ACCESS LAYER (DAL)                  |
|  - AuthRepository.cs          - UserRepository.cs           |
|  - DashboardRepository.cs     - SqlDataReader / Execute     |
+------------------------------+------------------------------+
                               | Truy vấn SQL
+------------------------------v------------------------------+
|                     MICROSOFT SQL SERVER                    |
|  - Bảng: Users, Orders, OrderItems, Products                |
+-------------------------------------------------------------+
```

---

## 2. Quản lý phiên làm việc tĩnh (`UserSession.cs`)

```csharp
namespace DPMS.Shared
{
    public static class UserSession
    {
        public static int CurrentUserId { get; set; } = 0;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentFullName { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = string.Empty; // "Admin" hoặc "Customer"
        public static bool IsLoggedIn => CurrentUserId > 0;
        public static bool IsAdmin => CurrentRole.Equals("Admin", System.StringComparison.OrdinalIgnoreCase);

        public static void Clear()
        {
            CurrentUserId = 0;
            CurrentUsername = string.Empty;
            CurrentFullName = string.Empty;
            CurrentRole = string.Empty;
        }
    }
}
```

---

## 3. Các câu lệnh SQL phục vụ CRUD phân hệ

### 3.1. [CRUD 1] Xác thực & Đăng ký (`Auth`)
```sql
-- 1. Đăng nhập: Kiểm tra tài khoản và mật khẩu
SELECT UserId, Username, FullName, Role, IsActive
FROM Users
WHERE Username = @Username AND PasswordHash = @PasswordHash;

-- 2. Đăng ký tài khoản mới (Create)
INSERT INTO Users (Username, PasswordHash, FullName, Email, PhoneNumber, Role, IsActive, CreatedAt)
VALUES (@Username, @PasswordHash, @FullName, @Email, @PhoneNumber, 'Customer', 1, GETDATE());

-- 3. Kiểm tra trùng lặp Username hoặc Email trước khi đăng ký
SELECT COUNT(1) FROM Users WHERE Username = @Username OR Email = @Email;
```

### 3.2. [CRUD 2] Quản lý Người dùng (`Users`)
```sql
-- 1. Xem danh sách người dùng (Read)
SELECT UserId, Username, FullName, Email, Role, IsActive, CreatedAt
FROM Users
WHERE (@Role IS NULL OR Role = @Role)
  AND (@Keyword IS NULL OR Username LIKE '%' + @Keyword + '%' OR FullName LIKE '%' + @Keyword + '%')
ORDER BY UserId DESC;

-- 2. Khóa tài khoản (Update)
UPDATE Users
SET IsActive = 0
WHERE UserId = @UserId;

-- 3. Mở khóa tài khoản (Update)
UPDATE Users
SET IsActive = 1
WHERE UserId = @UserId;
```

### 3.3. [Dashboard] Các câu lệnh thống kê tổng hợp (Aggregation)
```sql
-- 1. Tổng doanh thu từ các đơn hoàn thành
SELECT ISNULL(SUM(TotalAmount), 0) AS TotalRevenue
FROM Orders
WHERE Status = 'Completed';

-- 2. Tổng số đơn hàng đã đặt
SELECT COUNT(1) AS TotalOrders
FROM Orders;

-- 3. Tổng số khách hàng đã đăng ký
SELECT COUNT(1) AS TotalCustomers
FROM Users
WHERE Role = 'Customer';

-- 4. Top 5 sản phẩm bán chạy nhất
SELECT TOP 5 
    p.Title,
    SUM(oi.Quantity) AS TotalSold
FROM OrderItems oi
INNER JOIN Products p ON oi.ProductId = p.ProductId
GROUP BY p.ProductId, p.Title
ORDER BY TotalSold DESC;
```

---

## 4. Kịch bản xử lý trong C# WinForms

### 4.1. Xử lý Đăng nhập & Phân quyền giao diện
```csharp
private void btnLogin_Click(object sender, EventArgs e)
{
    string user = txtUsername.Text.Trim();
    string pass = txtPassword.Text.Trim();

    var result = _authService.Login(user, pass);
    if (result.Success)
    {
        // Lưu thông tin vào Session
        UserSession.CurrentUserId = result.User.UserId;
        UserSession.CurrentUsername = result.User.Username;
        UserSession.CurrentFullName = result.User.FullName;
        UserSession.CurrentRole = result.User.Role;

        MessageBox.Show($"Chào mừng {result.User.FullName} đã quay trở lại!", "Thành công");
        MainForm.Instance.ApplyUserPermissions(); // Ẩn hiện menu Admin/User
        MainForm.Instance.SwitchView(new StoreViewControl());
    }
    else
    {
        MessageBox.Show(result.ErrorMessage, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
```
