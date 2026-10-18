using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom1_Store;
using qlspdigital.BLL.Nhom2_Cart;
using qlspdigital.BLL.Nhom3_ProductAdmin;
using qlspdigital.Controls;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom1_Store
{
    // UI 2: Trang chủ Cửa hàng số - Tìm kiếm, lọc theo danh mục & lưới sản phẩm
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public partial class StoreViewControl : UserControl
    {
        private readonly SanPhamService _sanPhamService;
        private readonly DanhMucService _danhMucService;
        private readonly GioHangService _gioHangService;

        public event Action<int>? OnViewProductDetail;

        public StoreViewControl()
        {
            _sanPhamService = new SanPhamService();
            _danhMucService = new DanhMucService();
            _gioHangService = new GioHangService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadCategories();
            LoadProducts();

            // Lắng nghe sự kiện cập nhật đánh giá hoặc danh mục / sản phẩm theo thời gian thực
            DanhGiaService.ReviewUpdated += (id) =>
            {
                if (!this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try { this.BeginInvoke(new Action(LoadProducts)); } catch { }
                }
            };
            qlspdigital.BLL.Nhom3_ProductAdmin.SanPhamAdminService.ProductCatalogChanged += () =>
            {
                if (!this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try { this.BeginInvoke(new Action(LoadProducts)); } catch { }
                }
            };
            qlspdigital.BLL.Nhom3_ProductAdmin.DanhMucService.CategoryCatalogChanged += () =>
            {
                if (!this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            LoadCategories();
                            LoadProducts();
                        }));
                    }
                    catch { }
                }
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelFilter == null || txtSearch == null || cboCategory == null || btnSearch == null || btnRefresh == null || lblTotalCount == null || lblSearchTitle == null || lblCategoryTitle == null) return;
            int pnlW = panelFilter.ClientSize.Width;
            if (pnlW < 300) return;

            // Đặt kết quả đếm sang sát mép phải với lề thoáng đãng
            lblTotalCount.Location = new Point(pnlW - lblTotalCount.Width - 25, 22);

            // Bố cục tuần tự theo toạ độ tương đối (Geometric relative flow)
            lblSearchTitle.Location = new Point(20, 22);
            txtSearch.Location = new Point(lblSearchTitle.Right + 12, 15);

            int btnSearchW = 105;
            int btnRefreshW = 95;
            int rightBoundary = lblTotalCount.Left - 25;
            int nonInputSpace = 20 + lblSearchTitle.Width + 12 + btnSearchW + 22 + lblCategoryTitle.Width + 12 + btnRefreshW + 14;
            int availableForInputs = Math.Max(320, rightBoundary - nonInputSpace);

            int searchW = Math.Clamp((int)(availableForInputs * 0.58), 220, 480);
            int catW = Math.Clamp((int)(availableForInputs * 0.42), 170, 260);

            txtSearch.Size = new Size(searchW, 34);

            // Nút "Tìm kiếm" đặt ngay cạnh ô input tìm kiếm
            btnSearch.Location = new Point(txtSearch.Right + 10, 15);
            btnSearch.Size = new Size(btnSearchW, 34);

            // Thể loại đặt kế tiếp nút tìm kiếm
            lblCategoryTitle.Location = new Point(btnSearch.Right + 22, 22);
            cboCategory.Location = new Point(lblCategoryTitle.Right + 12, 15);
            cboCategory.Size = new Size(catW, 34);

            // Nút làm mới sau danh mục
            btnRefresh.Location = new Point(cboCategory.Right + 14, 15);
            btnRefresh.Size = new Size(btnRefreshW, 34);
        }

        private void LoadCategories()
        {
            try
            {
                var list = _danhMucService.LayTatCaDanhMuc();
                cboCategory.Items.Clear();
                cboCategory.Items.Add(new ComboBoxItem { Text = "--- Tất cả thể loại ---", Value = 0 });

                foreach (var dm in list)
                {
                    cboCategory.Items.Add(new ComboBoxItem { Text = dm.TenDanhMuc, Value = dm.MaDanhMuc });
                }

                if (cboCategory.Items.Count > 0)
                {
                    cboCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadProducts()
        {
            flowProducts.Controls.Clear();
            string tuKhoa = txtSearch.Text.Trim();
            int? maDanhMuc = null;

            if (cboCategory.SelectedItem is ComboBoxItem selectedItem && (int)selectedItem.Value > 0)
            {
                maDanhMuc = (int)selectedItem.Value;
            }

            try
            {
                var products = _sanPhamService.LayDanhSachSanPham(tuKhoa, maDanhMuc);
                lblTotalCount.Text = $"Hiển thị: {products.Count} sản phẩm";

                if (products.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        Text = "Không tìm thấy sản phẩm kỹ thuật số nào phù hợp!",
                        ForeColor = Color.Gray,
                        Font = new Font("Segoe UI", 12, FontStyle.Italic),
                        AutoSize = true,
                        Padding = new Padding(20)
                    };
                    flowProducts.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var sp in products)
                {
                    ProductCardControl card = new ProductCardControl();
                    card.BindData(sp);

                    // Xử lý sự kiện click Chi tiết
                    card.OnDetailClicked += (id) =>
                    {
                        OnViewProductDetail?.Invoke(id);
                    };

                    // Xử lý sự kiện Thêm vào giỏ
                    card.OnAddToCartClicked += (id) =>
                    {
                        if (!UserSession.IsLoggedIn)
                        {
                            MessageBox.Show("Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (_gioHangService.ThemVaoGioHang(UserSession.CurrentUserId, id, 1, out string msg))
                        {
                            MessageBox.Show(msg, "Giỏ hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    };

                    flowProducts.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PanelFilter_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                using var p = new Pen(Color.FromArgb(38, 54, 75), 1);
                e.Graphics.DrawLine(p, 0, pnl.Height - 1, pnl.Width, pnl.Height - 1);
            }
        }

        private void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadProducts();
        }

        private void CboCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadProducts();
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            LoadProducts();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            LoadProducts();
        }

        private class ComboBoxItem
        {
            public string Text { get; set; } = string.Empty;
            public object Value { get; set; } = 0;
            public override string ToString() => Text;
        }
    }
}
