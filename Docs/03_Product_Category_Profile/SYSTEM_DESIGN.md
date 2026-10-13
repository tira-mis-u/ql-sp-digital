# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 3
**Màn hình:** UI 10 (`ProductManageControl`), UI 11 (`CategoryManageControl`), UI 8 (`ProfileControl`)  
**Mô hình áp dụng:** Kiến trúc 3 tầng (Presentation - BLL - DAL - SQL Server)

---

## 1. Kiến trúc tổng thể phân hệ

```text
+-------------------------------------------------------------+
|                     PRESENTATION LAYER                      |
|  - ProductManageControl.cs    - CategoryManageControl.cs    |
|  - ProfileControl.cs          - CellClick Events            |
+------------------------------+------------------------------+
                               | Gọi nghiệp vụ CRUD
+------------------------------v------------------------------+
|                    BUSINESS LOGIC (BLL)                     |
|  - ProductManageService.cs    - CategoryService.cs          |
|  - UserService.cs             - Validation logic            |
+------------------------------+------------------------------+
                               | Gọi truy xuất dữ liệu
+------------------------------v------------------------------+
|                    DATA ACCESS LAYER (DAL)                  |
|  - ProductRepository.cs       - CategoryRepository.cs       |
|  - UserRepository.cs          - ADO.NET SqlCommand         |
+------------------------------+------------------------------+
                               | Thao tác trực tiếp
+------------------------------v------------------------------+
|                     MICROSOFT SQL SERVER                    |
|  - Bảng: Products, Categories, Users                        |
+-------------------------------------------------------------+
```

---

## 2. Mô hình dữ liệu (Models & DTOs)

### 2.1. `ProductManageDTO.cs`
```csharp
namespace DPMS.Shared.DTOs
{
    public class ProductManageDTO
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
```

### 2.2. `CategoryDTO.cs`
```csharp
namespace DPMS.Shared.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
    }
}
```

---

## 3. Các câu lệnh SQL phục vụ CRUD phân hệ

### 3.1. [CRUD 1] Thao tác bảng Sản phẩm (`Products`)
```sql
-- 1. Xem danh sách sản phẩm quản trị (Read)
SELECT p.ProductId, p.Title, p.CategoryId, c.CategoryName, p.Price, p.ImageUrl, p.IsActive, p.CreatedAt
FROM Products p
INNER JOIN Categories c ON p.CategoryId = c.CategoryId
WHERE (@Keyword IS NULL OR p.Title LIKE '%' + @Keyword + '%')
ORDER BY p.ProductId DESC;

-- 2. Thêm mới sản phẩm (Create)
INSERT INTO Products (Title, Description, Price, ImageUrl, CategoryId, IsActive, CreatedAt)
VALUES (@Title, @Description, @Price, @ImageUrl, @CategoryId, 1, GETDATE());

-- 3. Cập nhật thông tin sản phẩm (Update)
UPDATE Products
SET Title = @Title,
    Description = @Description,
    Price = @Price,
    ImageUrl = @ImageUrl,
    CategoryId = @CategoryId,
    IsActive = @IsActive
WHERE ProductId = @ProductId;

-- 4. Xóa mềm hoặc ngưng kinh doanh (Delete)
UPDATE Products
SET IsActive = 0
WHERE ProductId = @ProductId;
```

### 3.2. [CRUD 2] Thao tác bảng Danh mục (`Categories`)
```sql
-- 1. Xem danh sách danh mục và đếm số sản phẩm (Read)
SELECT c.CategoryId, c.CategoryName, c.Description, COUNT(p.ProductId) AS ProductCount
FROM Categories c
LEFT JOIN Products p ON c.CategoryId = p.CategoryId
GROUP BY c.CategoryId, c.CategoryName, c.Description;

-- 2. Thêm mới danh mục (Create)
INSERT INTO Categories (CategoryName, Description)
VALUES (@CategoryName, @Description);

-- 3. Sửa danh mục (Update)
UPDATE Categories
SET CategoryName = @CategoryName, Description = @Description
WHERE CategoryId = @CategoryId;

-- 4. Xóa danh mục (Delete - kiểm tra nếu còn sản phẩm thì chặn)
DELETE FROM Categories
WHERE CategoryId = @CategoryId
  AND NOT EXISTS (SELECT 1 FROM Products WHERE CategoryId = @CategoryId);
```

### 3.3. [CRUD 3] Hồ sơ người dùng & Đổi mật khẩu (`Users`)
```sql
-- 1. Xem hồ sơ cá nhân (Read)
SELECT UserId, Username, FullName, Email, PhoneNumber, Role, CreatedAt
FROM Users
WHERE UserId = @UserId;

-- 2. Cập nhật thông tin họ tên, email, sđt (Update)
UPDATE Users
SET FullName = @FullName, Email = @Email, PhoneNumber = @PhoneNumber
WHERE UserId = @UserId;

-- 3. Đổi mật khẩu (Update)
UPDATE Users
SET PasswordHash = @NewPasswordHash
WHERE UserId = @UserId AND PasswordHash = @OldPasswordHash;
```

---

## 4. Kịch bản xử lý sự kiện trong C# WinForms

### 4.1. Sự kiện đổ dữ liệu từ DataGridView lên Form nhập liệu
```csharp
private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex >= 0)
    {
        DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
        txtProductId.Text = row.Cells["colProductId"].Value.ToString();
        txtTitle.Text = row.Cells["colTitle"].Value.ToString();
        txtPrice.Text = row.Cells["colPrice"].Value.ToString();
        cboCategory.SelectedValue = Convert.ToInt32(row.Cells["colCategoryId"].Value);
        chkIsActive.Checked = Convert.ToBoolean(row.Cells["colIsActive"].Value);
    }
}
```

### 4.2. Thêm mới sản phẩm
```csharp
private void btnAddProduct_Click(object sender, EventArgs e)
{
    var dto = new ProductManageDTO
    {
        Title = txtTitle.Text.Trim(),
        Price = decimal.Parse(txtPrice.Text.Trim()),
        CategoryId = (int)cboCategory.SelectedValue,
        Description = txtDescription.Text.Trim(),
        ImageUrl = txtImageUrl.Text.Trim()
    };

    bool success = _productManageService.CreateProduct(dto);
    if (success)
    {
        MessageBox.Show("Thêm sản phẩm thành công!");
        LoadProductGrid();
        ClearInputs();
    }
}
```
