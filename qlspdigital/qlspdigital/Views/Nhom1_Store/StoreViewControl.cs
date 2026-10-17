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

        private FlowLayoutPanel flowProducts = null!;
        private TextBox txtSearch = null!;
        private ComboBox cboCategory = null!;
        private Button btnSearch = null!;
        private Button btnRefresh = null!;
        private Label lblTotalCount = null!;

        public StoreViewControl()
        {
            _sanPhamService = new SanPhamService();
            _danhMucService = new DanhMucService();
            _gioHangService = new GioHangService();

            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadCategories();
            LoadProducts();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "StoreViewControl";
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);

            // 1. THANH CÔNG CỤ LỌC & TÌM KIẾM
            Panel panelFilter = new Panel();
            panelFilter.SuspendLayout();
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Height = 60;
            panelFilter.BackColor = Color.FromArgb(22, 32, 45);
            panelFilter.Padding = new Padding(15, 12, 15, 12);
            panelFilter.Paint += PanelFilter_Paint;

            Label lblSearchTitle = new Label();
            lblSearchTitle.Text = "🔍 Tìm kiếm:";
            lblSearchTitle.ForeColor = Color.FromArgb(199, 213, 224);
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Location = new Point(15, 19);
            lblSearchTitle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblSearchTitle.UseMnemonic = false;

            txtSearch = new TextBox();
            txtSearch.Location = new Point(130, 15);
            txtSearch.Width = 190;
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.BackColor = Color.FromArgb(18, 25, 35);
            txtSearch.ForeColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.PlaceholderText = "Nhập tên sản phẩm...";
            txtSearch.KeyDown += TxtSearch_KeyDown;

            Label lblCategoryTitle = new Label();
            lblCategoryTitle.Text = "📁 Thể loại:";
            lblCategoryTitle.ForeColor = Color.FromArgb(199, 213, 224);
            lblCategoryTitle.AutoSize = true;
            lblCategoryTitle.Location = new Point(335, 19);
            lblCategoryTitle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblCategoryTitle.UseMnemonic = false;

            cboCategory = new ComboBox();
            cboCategory.Location = new Point(445, 15);
            cboCategory.Width = 185;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Font = new Font("Segoe UI", 9.5f);
            cboCategory.BackColor = Color.FromArgb(18, 25, 35);
            cboCategory.ForeColor = Color.White;
            cboCategory.FlatStyle = FlatStyle.Flat;
            cboCategory.SelectedIndexChanged += CboCategory_SelectedIndexChanged;

            btnSearch = new Button();
            btnSearch.Text = "Lọc dữ liệu";
            btnSearch.Location = new Point(640, 15);
            btnSearch.Width = 110;
            btnSearch.Height = 30;
            btnSearch.BackColor = Color.FromArgb(49, 98, 130);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.UseMnemonic = false;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;

            btnRefresh = new Button();
            btnRefresh.Text = "Làm mới";
            btnRefresh.Location = new Point(760, 15);
            btnRefresh.Width = 95;
            btnRefresh.Height = 30;
            btnRefresh.BackColor = Color.FromArgb(60, 75, 90);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.UseMnemonic = false;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            lblTotalCount = new Label();
            lblTotalCount.Text = "Hiển thị: 0 sản phẩm";
            lblTotalCount.ForeColor = Color.FromArgb(102, 192, 244);
            lblTotalCount.AutoSize = true;
            lblTotalCount.Location = new Point(870, 19);
            lblTotalCount.Font = new Font("Segoe UI", 9.5f, FontStyle.Italic);
            lblTotalCount.UseMnemonic = false;

            panelFilter.Controls.Add(lblTotalCount);
            panelFilter.Controls.Add(btnRefresh);
            panelFilter.Controls.Add(btnSearch);
            panelFilter.Controls.Add(cboCategory);
            panelFilter.Controls.Add(lblCategoryTitle);
            panelFilter.Controls.Add(txtSearch);
            panelFilter.Controls.Add(lblSearchTitle);
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();

            // 2. VÙNG LƯỚI THẺ SẢN PHẨM
            flowProducts = new FlowLayoutPanel();
            flowProducts.Dock = DockStyle.Fill;
            flowProducts.AutoScroll = true;
            flowProducts.BackColor = Color.FromArgb(23, 26, 33);
            flowProducts.Padding = new Padding(20);

            this.Controls.Add(flowProducts);
            this.Controls.Add(panelFilter);

            this.ResumeLayout(false);
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
