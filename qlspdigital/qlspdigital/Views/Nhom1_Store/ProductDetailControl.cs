using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom1_Store;
using qlspdigital.BLL.Nhom2_Cart;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom1_Store
{
    // UI 3: Màn hình Xem chi tiết sản phẩm kỹ thuật số
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public class ProductDetailControl : UserControl
    {
        private readonly SanPhamService _sanPhamService;
        private readonly GioHangService _gioHangService;

        public event Action? OnBackToStore;
        public event Action<int>? OnViewReviews;

        private int _maSanPham;
        private SanPhamDTO? _currentProduct;

        private Label lblTitle = null!;
        private Label lblCategory = null!;
        private Label lblPublisher = null!;
        private Label lblVersion = null!;
        private Label lblSize = null!;
        private Label lblRating = null!;
        private Label lblPrice = null!;
        private RichTextBox rtfDescription = null!;
        private Button btnBack = null!;
        private Button btnAddToCart = null!;
        private Button btnReviews = null!;

        public ProductDetailControl()
        {
            _sanPhamService = new SanPhamService();
            _gioHangService = new GioHangService();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);

            // Nút quay lại
            btnBack = new Button
            {
                Text = "← Quay lại Cửa hàng",
                Size = new Size(200, 36),
                Location = new Point(25, 18),
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => OnBackToStore?.Invoke();

            // Khung trên: Tiêu đề + Giá + Nút bấm hành động
            Panel panelTop = new Panel
            {
                Location = new Point(25, 65),
                Size = new Size(1000, 175),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            lblTitle = new Label
            {
                Text = "Tên sản phẩm kỹ thuật số",
                Location = new Point(20, 12),
                Size = new Size(680, 45),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                UseMnemonic = false
            };

            lblCategory = new Label
            {
                Text = "Thể loại: Game",
                Location = new Point(22, 58),
                AutoSize = true,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                UseMnemonic = false
            };

            lblPublisher = new Label
            {
                Text = "Nhà phát hành: Studio",
                Location = new Point(22, 85),
                AutoSize = true,
                ForeColor = Color.FromArgb(199, 213, 224),
                Font = new Font("Segoe UI", 10),
                UseMnemonic = false
            };

            lblVersion = new Label
            {
                Text = "Phiên bản: 1.0.0",
                Location = new Point(22, 112),
                AutoSize = true,
                ForeColor = Color.FromArgb(199, 213, 224),
                Font = new Font("Segoe UI", 9.5f),
                UseMnemonic = false
            };

            lblSize = new Label
            {
                Text = "Dung lượng: 0 MB",
                Location = new Point(260, 112),
                AutoSize = true,
                ForeColor = Color.FromArgb(199, 213, 224),
                Font = new Font("Segoe UI", 9.5f),
                UseMnemonic = false
            };

            lblRating = new Label
            {
                Text = "⭐ 5.0 / 5.0 (0 đánh giá)",
                Location = new Point(22, 138),
                AutoSize = true,
                ForeColor = Color.FromArgb(243, 156, 18),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                UseMnemonic = false
            };

            // Khung mua hàng bên phải
            Panel panelBuy = new Panel
            {
                Dock = DockStyle.Right,
                Width = 280,
                BackColor = Color.FromArgb(18, 25, 35),
                Padding = new Padding(15)
            };

            lblPrice = new Label
            {
                Text = "150.000 ₫",
                Dock = DockStyle.Top,
                Height = 35,
                ForeColor = Color.FromArgb(164, 208, 7),
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                UseMnemonic = false
            };

            btnAddToCart = new Button
            {
                Text = "🛒 Thêm vào Giỏ hàng",
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnAddToCart.FlatAppearance.BorderSize = 0;
            btnAddToCart.Click += BtnAddToCart_Click;

            btnReviews = new Button
            {
                Text = "💬 Đánh giá & Nhận xét",
                Dock = DockStyle.Bottom,
                Height = 35,
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnReviews.FlatAppearance.BorderSize = 0;
            btnReviews.Click += (s, e) => OnViewReviews?.Invoke(_maSanPham);

            panelBuy.Controls.Add(btnReviews);
            panelBuy.Controls.Add(btnAddToCart);
            panelBuy.Controls.Add(lblPrice);

            panelTop.Controls.Add(panelBuy);
            panelTop.Controls.Add(lblRating);
            panelTop.Controls.Add(lblSize);
            panelTop.Controls.Add(lblVersion);
            panelTop.Controls.Add(lblPublisher);
            panelTop.Controls.Add(lblCategory);
            panelTop.Controls.Add(lblTitle);

            // Khung dưới: Mô tả chi tiết sản phẩm
            Panel panelBottom = new Panel
            {
                Location = new Point(25, 255),
                Size = new Size(1000, 470),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            Label lblDescTitle = new Label
            {
                Text = "📖 GIỚI THIỆU & ĐẶC TẢ TÍNH NĂNG SẢN PHẨM",
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                UseMnemonic = false
            };

            rtfDescription = new RichTextBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.FromArgb(220, 220, 220),
                Font = new Font("Segoe UI", 10.5f),
                BorderStyle = BorderStyle.None,
                ReadOnly = true
            };

            panelBottom.Controls.Add(rtfDescription);
            panelBottom.Controls.Add(lblDescTitle);

            this.Controls.Add(panelBottom);
            this.Controls.Add(panelTop);
            this.Controls.Add(btnBack);
        }

        public void LoadProductDetail(int maSanPham)
        {
            _maSanPham = maSanPham;
            _currentProduct = _sanPhamService.LayChiTietSanPham(maSanPham);

            if (_currentProduct == null)
            {
                MessageBox.Show("Không tìm thấy thông tin sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblTitle.Text = _currentProduct.TenSanPham;
            lblCategory.Text = $"Thể loại: {_currentProduct.TenDanhMuc}";
            lblPublisher.Text = $"Nhà phát hành: {_currentProduct.TenNhaPhatHanh}";
            lblVersion.Text = $"Phiên bản: {_currentProduct.PhienBan}";
            lblSize.Text = $"Dung lượng: {FormatHelper.FormatFileSize(_currentProduct.DungLuongMB)}";
            lblRating.Text = $"⭐ {_currentProduct.DiemDanhGiaTrungBinh:0.0} / 5.0 ({_currentProduct.SoLuongDanhGia} lượt nhận xét)";
            lblPrice.Text = FormatHelper.FormatCurrency(_currentProduct.GiaBan);
            rtfDescription.Text = string.IsNullOrWhiteSpace(_currentProduct.MoTa)
                ? "Sản phẩm kỹ thuật số chính hãng được phân phối bản quyền trên hệ thống DPMS. Tương thích đa nền tảng, hỗ trợ cập nhật trọn đời."
                : _currentProduct.MoTa;
        }

        private void BtnAddToCart_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_gioHangService.ThemVaoGioHang(UserSession.CurrentUserId, _maSanPham, 1, out string msg))
            {
                MessageBox.Show(msg, "Giỏ hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
