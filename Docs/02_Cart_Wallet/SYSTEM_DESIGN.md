# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 2
**Màn hình:** UI 4 (`CartControl`) & UI 5 (`WalletControl`)  
**Mô hình áp dụng:** Kiến trúc 3 tầng (Presentation - BLL - DAL - SQL Server)

---

## 1. Kiến trúc tổng thể phân hệ

```text
+-------------------------------------------------------------+
|                     PRESENTATION LAYER                      |
|  - CartControl.cs             - WalletControl.cs            |
|  - btnPayWithWallet_Click     - btnConfirmDeposit_Click     |
+------------------------------+------------------------------+
                               | Gọi dịch vụ giỏ hàng & Ví tiền
+------------------------------v------------------------------+
|                    BUSINESS LOGIC (BLL)                     |
|  - CartService.cs             - WalletService.cs            |
|  - Kiểm tra số dư >= Tổng tiền, xử lý logic nạp/trừ tiền    |
+------------------------------+------------------------------+
                               | Gọi truy xuất SQL
+------------------------------v------------------------------+
|                    DATA ACCESS LAYER (DAL)                  |
|  - CartRepository.cs          - WalletRepository.cs         |
|  - SqlTransaction, SqlCommand, ADO.NET                      |
+------------------------------+------------------------------+
                               | Thao tác CSDL
+------------------------------v------------------------------+
|                     MICROSOFT SQL SERVER                    |
|  - Bảng: CartItems, Users (cột Balance), Orders,             |
|          OrderItems, UserProducts                           |
+-------------------------------------------------------------+
```

---

## 2. Mô hình dữ liệu (Models & DTOs)

### 2.1. `CartItemDTO.cs`
```csharp
namespace DPMS.Shared.DTOs
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
```

### 2.2. `WalletInfoDTO.cs`
```csharp
namespace DPMS.Shared.DTOs
{
    public class WalletInfoDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public decimal Balance { get; set; }
    }
}
```

---

## 3. Các câu lệnh SQL phục vụ CRUD phân hệ

### 3.1. [CRUD 1] Thao tác Giỏ hàng (`CartItems`)
```sql
-- 1. Xem danh sách món trong giỏ (Read)
SELECT 
    ci.CartItemId,
    ci.ProductId,
    p.Title,
    p.Price,
    ci.Quantity,
    (p.Price * ci.Quantity) AS TotalPrice
FROM CartItems ci
INNER JOIN Products p ON ci.ProductId = p.ProductId
WHERE ci.UserId = @UserId;

-- 2. Thêm món vào giỏ (Create)
IF EXISTS (SELECT 1 FROM CartItems WHERE UserId = @UserId AND ProductId = @ProductId)
    UPDATE CartItems SET Quantity = Quantity + 1 WHERE UserId = @UserId AND ProductId = @ProductId;
ELSE
    INSERT INTO CartItems (UserId, ProductId, Quantity, CreatedAt)
    VALUES (@UserId, @ProductId, 1, GETDATE());

-- 3. Cập nhật số lượng (Update)
UPDATE CartItems
SET Quantity = @Quantity
WHERE CartItemId = @CartItemId;

-- 4. Xóa món khỏi giỏ (Delete)
DELETE FROM CartItems
WHERE CartItemId = @CartItemId;

-- 5. Xóa sạch giỏ hàng (Delete All)
DELETE FROM CartItems
WHERE UserId = @UserId;
```

### 3.2. [CRUD 2] Ví tài khoản & Nạp tiền (`Users.Balance`)
```sql
-- 1. Xem số dư ví hiện tại của người dùng (Read)
SELECT Balance FROM Users WHERE UserId = @UserId;

-- 2. Nạp tiền mô phỏng vào ví (Update)
UPDATE Users
SET Balance = Balance + @Amount
WHERE UserId = @UserId;
```

### 3.3. [Thanh toán 1-Click bằng Ví] Transaction SQL an toàn
```sql
BEGIN TRANSACTION;
BEGIN TRY
    -- Bước 1: Kiểm tra số dư ví hiện tại
    DECLARE @CurrentBalance DECIMAL(18, 2);
    SELECT @CurrentBalance = Balance FROM Users WHERE UserId = @UserId;

    IF @CurrentBalance < @TotalAmount
    BEGIN
        -- Không đủ tiền: Hủy bỏ ngay lập tức
        ROLLBACK TRANSACTION;
        SELECT 'INSUFFICIENT_FUNDS' AS Result, @CurrentBalance AS Balance;
        RETURN;
    END

    -- Bước 2: Trừ tiền trực tiếp trong ví tài khoản
    UPDATE Users 
    SET Balance = Balance - @TotalAmount 
    WHERE UserId = @UserId;

    -- Bước 3: Tạo đơn hàng mới ghi nhận phương thức thanh toán là WALLET
    DECLARE @NewOrderId INT;
    INSERT INTO Orders (UserId, TotalAmount, Status, PaymentMethod, CreatedAt)
    VALUES (@UserId, @TotalAmount, 'Completed', 'WALLET', GETDATE());
    SET @NewOrderId = SCOPE_IDENTITY();

    -- Bước 4: Lưu chi tiết đơn hàng
    INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice)
    SELECT @NewOrderId, ci.ProductId, ci.Quantity, p.Price
    FROM CartItems ci
    INNER JOIN Products p ON ci.ProductId = p.ProductId
    WHERE ci.UserId = @UserId;

    -- Bước 5: Cấp game/phần mềm vào kho sở hữu thư viện (UserProducts)
    INSERT INTO UserProducts (UserId, ProductId, PurchasedAt)
    SELECT @UserId, ProductId, GETDATE()
    FROM CartItems
    WHERE UserId = @UserId
      AND ProductId NOT IN (SELECT ProductId FROM UserProducts WHERE UserId = @UserId);

    -- Bước 6: Làm sạch giỏ hàng
    DELETE FROM CartItems WHERE UserId = @UserId;

    COMMIT TRANSACTION;
    SELECT 'SUCCESS' AS Result, @NewOrderId AS OrderId, (@CurrentBalance - @TotalAmount) AS NewBalance;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH;
```

---

## 4. Kịch bản xử lý trong C# WinForms

### 4.1. Sự kiện nạp tiền nhanh vào ví (UI 5)
```csharp
private void btnConfirmDeposit_Click(object sender, EventArgs e)
{
    if (decimal.TryParse(txtCustomAmount.Text, out decimal amount) && amount > 0)
    {
        bool success = _walletService.Deposit(UserSession.CurrentUserId, amount);
        if (success)
        {
            MessageBox.Show($"Nạp thành công {amount:N0} VNĐ vào ví tài khoản!", "Thành công");
            LoadWalletInfo(); // Cập nhật lại số dư trên màn hình ví
            MainForm.Instance.UpdateHeaderBalance(); // Cập nhật số dư trên Header
        }
    }
    else
    {
        MessageBox.Show("Vui lòng chọn hoặc nhập số tiền hợp lệ!", "Cảnh báo");
    }
}
```

### 4.2. Sự kiện bấm nút Thanh toán 1-Click bằng Ví (UI 4)
```csharp
private void btnPayWithWallet_Click(object sender, EventArgs e)
{
    int userId = UserSession.CurrentUserId;
    decimal currentBalance = _walletService.GetBalance(userId);

    // Kiểm tra sơ bộ phía Client
    if (currentBalance < _totalCartAmount)
    {
        var confirm = MessageBox.Show(
            $"Số dư ví ({currentBalance:N0} đ) không đủ để thanh toán ({_totalCartAmount:N0} đ)!\nBạn có muốn nạp thêm tiền ngay không?",
            "Số dư không đủ",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirm == DialogResult.Yes)
        {
            MainForm.Instance.SwitchView(new Views.Cart.WalletControl());
        }
        return;
    }

    // Thực thi trừ tiền ví và tạo đơn hàng
    var result = _cartService.CheckoutWithWallet(userId, _totalCartAmount);
    if (result.Success)
    {
        MessageBox.Show(
            $"Thanh toán thành công!\nSản phẩm đã có trong Thư viện của bạn.\nSố dư ví còn lại: {result.NewBalance:N0} VNĐ",
            "Thành công",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        MainForm.Instance.UpdateHeaderBalance();
        MainForm.Instance.SwitchView(new Views.Customer.LibraryControl());
    }
    else
    {
        MessageBox.Show(result.ErrorMessage, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```
