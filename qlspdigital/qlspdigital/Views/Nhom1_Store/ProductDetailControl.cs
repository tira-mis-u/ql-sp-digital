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
    public partial class ProductDetailControl : UserControl
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
        private Panel panelTop = null!;

        public ProductDetailControl()
        {
            _sanPhamService = new SanPhamService();
            _gioHangService = new GioHangService();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(25);

            // Nút quay lại
            btnBack = new Button();
            btnBack.Text = "Quay lại Cửa hàng";
            btnBack.Size = new Size(200, 36);
            btnBack.Location = new Point(25, 18);
            btnBack.BackColor = Color.FromArgb(49, 98, 130);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnBack.Cursor = Cursors.Hand;
            btnBack.UseMnemonic = false;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;

            // Khung trên: Thông tin chính sản phẩm
            panelTop = new Panel();
            panelTop.Location = new Point(25, 55);
            panelTop.Size = new Size(1000, 175);
            panelTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelTop.BackColor = Color.FromArgb(27, 40, 56);
            panelTop.Padding = new Padding(20);

            lblTitle = new Label();
            lblTitle.Text = "Tên sản phẩm kỹ thuật số";
            lblTitle.Location = new Point(20, 12);
            lblTitle.Size = new Size(680, 45);
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.UseMnemonic = false;

            lblCategory = new Label();
            lblCategory.Text = "Thể loại: Game";
            lblCategory.Location = new Point(22, 58);
            lblCategory.AutoSize = true;
            lblCategory.ForeColor = Color.FromArgb(102, 192, 244);
            lblCategory.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblCategory.UseMnemonic = false;

            lblPublisher = new Label();
            lblPublisher.Text = "Nhà phát hành: Studio";
            lblPublisher.Location = new Point(22, 85);
            lblPublisher.AutoSize = true;
            lblPublisher.ForeColor = Color.FromArgb(199, 213, 224);
            lblPublisher.Font = new Font("Segoe UI", 10);
            lblPublisher.UseMnemonic = false;

            lblVersion = new Label();
            lblVersion.Text = "Phiên bản: 1.0.0";
            lblVersion.Location = new Point(22, 112);
            lblVersion.AutoSize = true;
            lblVersion.ForeColor = Color.FromArgb(199, 213, 224);
            lblVersion.Font = new Font("Segoe UI", 9.5f);
            lblVersion.UseMnemonic = false;

            lblSize = new Label();
            lblSize.Text = "Dung lượng: 0 MB";
            lblSize.Location = new Point(260, 112);
            lblSize.AutoSize = true;
            lblSize.ForeColor = Color.FromArgb(199, 213, 224);
            lblSize.Font = new Font("Segoe UI", 9.5f);
            lblSize.UseMnemonic = false;

            lblRating = new Label();
            lblRating.Text = "⭐ 5.0 / 5.0 (0 đánh giá)";
            lblRating.Location = new Point(22, 138);
            lblRating.AutoSize = true;
            lblRating.ForeColor = Color.FromArgb(243, 156, 18);
            lblRating.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblRating.UseMnemonic = false;

            // Khung mua hàng bên phải
            Panel panelBuy = new Panel();
            panelBuy.Dock = DockStyle.Right;
            panelBuy.Width = 280;
            panelBuy.BackColor = Color.FromArgb(18, 25, 35);
            panelBuy.Padding = new Padding(15);

            lblPrice = new Label();
            lblPrice.Text = "150.000 ₫";
            lblPrice.Dock = DockStyle.Top;
            lblPrice.Height = 35;
            lblPrice.ForeColor = Color.FromArgb(164, 208, 7);
            lblPrice.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblPrice.TextAlign = ContentAlignment.MiddleCenter;
            lblPrice.UseMnemonic = false;

            btnAddToCart = new Button();
            btnAddToCart.Text = "Thêm vào Giỏ hàng";
            btnAddToCart.Dock = DockStyle.Top;
            btnAddToCart.Height = 40;
            btnAddToCart.BackColor = Color.FromArgb(90, 160, 45);
            btnAddToCart.ForeColor = Color.White;
            btnAddToCart.FlatStyle = FlatStyle.Flat;
            btnAddToCart.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAddToCart.Cursor = Cursors.Hand;
            btnAddToCart.UseMnemonic = false;
            btnAddToCart.FlatAppearance.BorderSize = 0;
            btnAddToCart.Click += BtnAddToCart_Click;

            btnReviews = new Button();
            btnReviews.Text = "Đánh giá & Nhận xét";
            btnReviews.Dock = DockStyle.Bottom;
            btnReviews.Height = 38;
            btnReviews.BackColor = Color.FromArgb(49, 98, 130);
            btnReviews.ForeColor = Color.White;
            btnReviews.FlatStyle = FlatStyle.Flat;
            btnReviews.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnReviews.Cursor = Cursors.Hand;
            btnReviews.UseMnemonic = false;
            btnReviews.FlatAppearance.BorderSize = 0;
            btnReviews.Click += BtnReviews_Click;

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
            Panel panelBottom = new Panel();
            panelBottom.Location = new Point(25, 245);
            panelBottom.Size = new Size(1000, 420);
            panelBottom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBottom.BackColor = Color.FromArgb(27, 40, 56);
            panelBottom.Padding = new Padding(20);

            Label lblDescTitle = new Label();
            lblDescTitle.Text = "GIỚI THIỆU & ĐẶC TẢ TÍNH NĂNG SẢN PHẨM";
            lblDescTitle.Dock = DockStyle.Top;
            lblDescTitle.Height = 30;
            lblDescTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblDescTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblDescTitle.UseMnemonic = false;

            rtfDescription = new RichTextBox();
            rtfDescription.Dock = DockStyle.Fill;
            rtfDescription.BackColor = Color.FromArgb(18, 25, 35);
            rtfDescription.ForeColor = Color.FromArgb(220, 220, 220);
            rtfDescription.Font = new Font("Segoe UI", 10.5f);
            rtfDescription.BorderStyle = BorderStyle.None;
            rtfDescription.ReadOnly = true;

            panelBottom.Controls.Add(rtfDescription);
            panelBottom.Controls.Add(lblDescTitle);

            this.Controls.Add(panelBottom);
            this.Controls.Add(panelTop);
            this.Controls.Add(btnBack);

            this.ResumeLayout(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (lblTitle != null && panelTop != null)
            {
                lblTitle.Width = Math.Max(200, panelTop.ClientSize.Width - 310);
            }
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

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            OnBackToStore?.Invoke();
        }

        private void BtnReviews_Click(object? sender, EventArgs e)
        {
            OnViewReviews?.Invoke(_maSanPham);
        }
    }
}
