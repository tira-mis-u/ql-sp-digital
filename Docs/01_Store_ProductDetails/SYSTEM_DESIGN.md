# THIẾT KẾ HỆ THỐNG (SYSTEM DESIGN) - NHÓM 1
**Màn hình:** UI 2 (`StoreViewControl`) & UI 3 (`ProductDetailControl`)  
**Mô hình áp dụng:** Kiến trúc 3 tầng (Presentation - BLL - DAL - SQL Server)

---

## 1. Kiến trúc tổng thể phân hệ

```text
+-------------------------------------------------------------+
|                     PRESENTATION LAYER                      |
|  - StoreViewControl.cs        - ProductDetailControl.cs     |
|  - ProductCardControl.cs      - Event Handlers (Click/Load) |
+------------------------------+------------------------------+
                               | Gọi nghiệp vụ (DTO)
+------------------------------v------------------------------+
|                    BUSINESS LOGIC (BLL)                     |
|  - ProductService.cs          - FavoriteService.cs          |
|  - Kiểm tra điều kiện dữ liệu, xử lý logic sắp xếp          |
+------------------------------+------------------------------+
                               | Truy xuất dữ liệu (Entity)
+------------------------------v------------------------------+
|                    DATA ACCESS LAYER (DAL)                  |
|  - ProductRepository.cs       - FavoriteRepository.cs       |
|  - ADO.NET: SqlConnection, SqlCommand, SqlDataReader       |
+------------------------------+------------------------------+
                               | Lệnh SQL
+------------------------------v------------------------------+
|                     MICROSOFT SQL SERVER                    |
|  - Bảng: Products, Categories, Favorites, Publishers        |
+-------------------------------------------------------------+
```

---

## 2. Mô hình dữ liệu (Models & DTOs)

### 2.1. `ProductDTO.cs`
```csharp
namespace DPMS.Shared.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
        public bool IsFavorite { get; set; }
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
    }
}
```

---

## 3. Các câu lệnh SQL phục vụ CRUD phân hệ

### 3.1. [CRUD 1] Đọc & Lọc danh sách sản phẩm (`SELECT`)
```sql
-- Lấy danh sách sản phẩm kèm tên danh mục, hỗ trợ tìm kiếm và lọc danh mục
SELECT 
    p.ProductId, 
    p.Title, 
    p.Description, 
    p.Price, 
    p.ImageUrl, 
    p.CategoryId, 
    c.CategoryName,
    pub.PublisherName
FROM Products p
INNER JOIN Categories c ON p.CategoryId = c.CategoryId
LEFT JOIN Publishers pub ON p.PublisherId = pub.PublisherId
WHERE p.IsActive = 1
  AND (@CategoryId = 0 OR p.CategoryId = @CategoryId)
  AND (@Keyword IS NULL OR p.Title LIKE '%' + @Keyword + '%')
ORDER BY p.CreatedAt DESC;
```

### 3.2. [CRUD 2] Quản lý Danh sách yêu thích (`Favorites`)
```sql
-- 1. Kiểm tra xem người dùng đã thích sản phẩm chưa (Read)
SELECT COUNT(1) FROM Favorites 
WHERE UserId = @UserId AND ProductId = @ProductId;

-- 2. Thêm vào danh sách yêu thích (Create)
INSERT INTO Favorites (UserId, ProductId, CreatedAt)
VALUES (@UserId, @ProductId, GETDATE());

-- 3. Xóa khỏi danh sách yêu thích (Delete)
DELETE FROM Favorites 
WHERE UserId = @UserId AND ProductId = @ProductId;

-- 4. Lấy danh sách ID các sản phẩm yêu thích của 1 người dùng (Read)
SELECT ProductId FROM Favorites 
WHERE UserId = @UserId;
```

---

## 4. Kịch bản xử lý sự kiện trong C# WinForms

### 4.1. Sự kiện nạp danh mục và danh sách sản phẩm khi mở màn hình
```csharp
// Trong StoreViewControl.cs
private readonly ProductService _productService = new ProductService();

private void StoreViewControl_Load(object sender, EventArgs e)
{
    LoadCategories();
    LoadProducts();
}

private void LoadProducts()
{
    string keyword = txtSearch.Text.Trim();
    int categoryId = (int)(cboCategory.SelectedValue ?? 0);

    flowProductList.Controls.Clear();
    var products = _productService.GetFilteredProducts(keyword, categoryId);

    foreach (var product in products)
    {
        var card = new ProductCardControl(product);
        card.OnViewDetailClicked += (s, prodId) => 
        {
            // Chuyển sang màn hình ProductDetailControl
            var detailView = new ProductDetailControl(prodId);
            MainForm.Instance.SwitchView(detailView);
        };
        flowProductList.Controls.Add(card);
    }
}
```

### 4.2. Xử lý nút Thích / Bỏ thích (Favorites)
```csharp
private void btnFavorite_Click(object sender, EventArgs e)
{
    int currentUserId = UserSession.CurrentUserId;
    bool isNowFav = _favoriteService.ToggleFavorite(currentUserId, _product.ProductId);

    if (isNowFav)
    {
        btnFavorite.Text = "❤️";
        MessageBox.Show("Đã thêm vào danh sách yêu thích!");
    }
    else
    {
        btnFavorite.Text = "🤍";
        MessageBox.Show("Đã xóa khỏi danh sách yêu thích!");
    }
}
```
