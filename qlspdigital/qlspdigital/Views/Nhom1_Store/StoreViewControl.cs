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
    public class StoreViewControl : UserControl
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
            LoadCategories();
            LoadProducts();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;

            // 1. THANH CÔNG CỤ LỌC & TÌM KIẾM
            Panel panelFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(22, 32, 45),
                Padding = new Padding(15, 12, 15, 12)
            };
            panelFilter.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(38, 54, 75), 1);
                e.Graphics.DrawLine(p, 0, panelFilter.Height - 1, panelFilter.Width, panelFilter.Height - 1);
            };

            Label lblSearchTitle = new Label
            {
                Text = "🔍 Tìm kiếm:",
                ForeColor = Color.FromArgb(199, 213, 224),
                AutoSize = true,
                Location = new Point(15, 19),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                UseMnemonic = false
            };

            txtSearch = new TextBox
            {
                Location = new Point(130, 15),
                Width = 190,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Nhập tên sản phẩm..."
            };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) LoadProducts(); };

            Label lblCategoryTitle = new Label
            {
                Text = "📁 Thể loại:",
                ForeColor = Color.FromArgb(199, 213, 224),
                AutoSize = true,
                Location = new Point(335, 19),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                UseMnemonic = false
            };

            cboCategory = new ComboBox
            {
                Location = new Point(445, 15),
                Width = 185,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cboCategory.SelectedIndexChanged += (s, e) => LoadProducts();

            btnSearch = new Button
            {
                Text = "Lọc dữ liệu",
                Location = new Point(640, 15),
                Width = 110,
                Height = 30,
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += (s, e) => LoadProducts();

            btnRefresh = new Button
            {
                Text = "Làm mới",
                Location = new Point(760, 15),
                Width = 95,
                Height = 30,
                BackColor = Color.FromArgb(60, 75, 90),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) =>
            {
                txtSearch.Clear();
                if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
                LoadProducts();
            };

            lblTotalCount = new Label
            {
                Text = "Hiển thị: 0 sản phẩm",
                ForeColor = Color.FromArgb(102, 192, 244),
                AutoSize = true,
                Location = new Point(870, 19),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Italic),
                UseMnemonic = false
            };

            panelFilter.Controls.Add(lblTotalCount);
            panelFilter.Controls.Add(btnRefresh);
            panelFilter.Controls.Add(btnSearch);
            panelFilter.Controls.Add(cboCategory);
            panelFilter.Controls.Add(lblCategoryTitle);
            panelFilter.Controls.Add(txtSearch);
            panelFilter.Controls.Add(lblSearchTitle);

            // 2. VÙNG LƯỚI THẺ SẢN PHẨM
            flowProducts = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(23, 26, 33),
                Padding = new Padding(20)
            };

            this.Controls.Add(flowProducts);
            this.Controls.Add(panelFilter);
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

        private class ComboBoxItem
        {
            public string Text { get; set; } = string.Empty;
            public object Value { get; set; } = 0;
            public override string ToString() => Text;
        }
    }
}
