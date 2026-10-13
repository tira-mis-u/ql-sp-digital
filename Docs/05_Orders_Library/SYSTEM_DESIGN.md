# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 5
**Màn hình:** UI 13 (`OrderManageControl`), UI 7 (`OrderHistoryControl`), UI 6 (`LibraryControl`)  
**Mô hình áp dụng:** Kiến trúc 3 tầng (Presentation - BLL - DAL - SQL Server)

---

## 1. Kiến trúc tổng thể phân hệ

```text
+-------------------------------------------------------------+
|                     PRESENTATION LAYER                      |
|  - OrderManageControl.cs      - OrderHistoryControl.cs      |
|  - LibraryControl.cs          - ProgressBar Download Timer  |
+------------------------------+------------------------------+
                               | Quản lý đơn & Kho sở hữu
+------------------------------v------------------------------+
|                    BUSINESS LOGIC (BLL)                     |
|  - OrderAdminService.cs       - LibraryService.cs           |
|  - Kiểm tra trạng thái đơn, cấp key, mô phỏng tải tệp       |
+------------------------------+------------------------------+
                               | Gọi ADO.NET
+------------------------------v------------------------------+
|                    DATA ACCESS LAYER (DAL)                  |
|  - OrderRepository.cs         - LibraryRepository.cs        |
|  - ADO.NET: SqlDataReader, SqlCommand ExecuteNonQuery      |
+------------------------------+------------------------------+
                               | Truy vấn CSDL
+------------------------------v------------------------------+
|                     MICROSOFT SQL SERVER                    |
|  - Bảng: Orders, OrderItems, UserProducts, Products         |
+-------------------------------------------------------------+
```

---

## 2. Mô hình dữ liệu (Models & DTOs)

### 2.1. `OrderAdminDTO.cs`
```csharp
using System;

namespace DPMS.Shared.DTOs
{
    public class OrderAdminDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // "Pending", "Completed", "Cancelled"
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
```

### 2.2. `UserLibraryItemDTO.cs`
```csharp
using System;

namespace DPMS.Shared.DTOs
{
    public class UserLibraryItemDTO
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public DateTime PurchasedAt { get; set; }
        public string FileVersion { get; set; } = "v1.0.0";
        public string FileSize { get; set; } = "1.5 GB";
        public string LicenseKey { get; set; } = "DPMS-KEY-XXXX";
    }
}
```

---

## 3. Các câu lệnh SQL phục vụ CRUD phân hệ

### 3.1. [CRUD 1] Quản lý & Lịch sử Đơn hàng (`Orders`, `OrderItems`)
```sql
-- 1. Xem danh sách tất cả đơn hàng (Admin Read)
SELECT o.OrderId, o.UserId, u.Username, o.TotalAmount, o.Status, o.PaymentMethod, o.CreatedAt
FROM Orders o
INNER JOIN Users u ON o.UserId = u.UserId
WHERE (@Status IS NULL OR o.Status = @Status)
  AND (@Keyword IS NULL OR u.Username LIKE '%' + @Keyword + '%' OR CAST(o.OrderId AS VARCHAR) LIKE '%' + @Keyword + '%')
ORDER BY o.OrderId DESC;

-- 2. Xem chi tiết các sản phẩm trong đơn hàng
SELECT oi.OrderItemId, oi.ProductId, p.Title, oi.Quantity, oi.UnitPrice, (oi.Quantity * oi.UnitPrice) AS LineTotal
FROM OrderItems oi
INNER JOIN Products p ON oi.ProductId = p.ProductId
WHERE oi.OrderId = @OrderId;

-- 3. Xem lịch sử đơn hàng của 1 khách hàng cụ thể (Customer Read)
SELECT OrderId, TotalAmount, Status, PaymentMethod, CreatedAt
FROM Orders
WHERE UserId = @CurrentUserId
ORDER BY OrderId DESC;

-- 4. Duyệt hoặc Hủy đơn hàng (Update)
UPDATE Orders
SET Status = @NewStatus -- 'Completed' hoặc 'Cancelled'
WHERE OrderId = @OrderId;
```

### 3.2. [CRUD 2] Thư viện sở hữu sản phẩm số (`UserProducts`)
```sql
-- 1. Lấy danh sách sản phẩm trong kho của người dùng (Read)
SELECT 
    p.ProductId, 
    p.Title, 
    p.ImageUrl, 
    c.CategoryName, 
    up.PurchasedAt
FROM UserProducts up
INNER JOIN Products p ON up.ProductId = p.ProductId
INNER JOIN Categories c ON p.CategoryId = c.CategoryId
WHERE up.UserId = @CurrentUserId
ORDER BY up.PurchasedAt DESC;

-- 2. Gỡ bỏ sản phẩm khỏi thư viện cá nhân (Delete)
DELETE FROM UserProducts
WHERE UserId = @CurrentUserId AND ProductId = @ProductId;
```

---

## 4. Kịch bản xử lý trong C# WinForms

### 4.1. Duyệt đơn hàng (Admin)
```csharp
private void btnApproveOrder_Click(object sender, EventArgs e)
{
    if (dgvAdminOrders.CurrentRow == null) return;

    int orderId = Convert.ToInt32(dgvAdminOrders.CurrentRow.Cells["colOrderId"].Value);
    bool success = _orderService.UpdateOrderStatus(orderId, "Completed");

    if (success)
    {
        MessageBox.Show($"Đã duyệt thành công đơn hàng #{orderId}!", "Thông báo");
        LoadAdminOrders();
    }
}
```

### 4.2. Mô phỏng tải file (Download Simulation)
```csharp
private async void btnDownloadFile_Click(object sender, EventArgs e)
{
    btnDownloadFile.Enabled = false;
    lblStatus.Text = "Đang tải xuống tệp cài đặt...";
    prgDownload.Value = 0;
    prgDownload.Visible = true;

    // Giả lập tiến trình tải file 100% bằng Task.Delay
    for (int i = 0; i <= 100; i += 10)
    {
        prgDownload.Value = i;
        await System.Threading.Tasks.Task.Delay(150); // Mô phỏng mạng tải
    }

    lblStatus.Text = "Trạng thái: Đã tải hoàn tất!";
    btnDownloadFile.Enabled = true;
    MessageBox.Show("Tải xuống thành công! Tệp sản phẩm kỹ thuật số đã sẵn sàng sử dụng trên máy tính của bạn.",
                    "Thông báo hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
```
