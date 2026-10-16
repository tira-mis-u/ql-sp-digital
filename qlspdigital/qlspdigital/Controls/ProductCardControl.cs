using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Controls
{
    // UserControl đại diện cho một thẻ sản phẩm trên lưới Store (Steam Card style)
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public class ProductCardControl : UserControl
    {
        public int MaSanPham { get; private set; }
        public event Action<int>? OnDetailClicked;
        public event Action<int>? OnAddToCartClicked;

        private Label lblTitle = null!;
        private Label lblCategory = null!;
        private Label lblRating = null!;
        private Label lblPrice = null!;
        private Button btnDetail = null!;
        private Button btnAddToCart = null!;
        private Panel panelHeader = null!;

        public ProductCardControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(245, 305);
            this.BackColor = Color.FromArgb(30, 45, 62);
            this.Margin = new Padding(10);
            this.Padding = new Padding(10);

            // Giả lập ảnh bìa game / phần mềm bằng Panel gradient
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(23, 36, 50)
            };

            Label lblThumb = new Label
            {
                Text = "🎮 DIGITAL\nPRODUCT",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                UseMnemonic = false
            };
            panelHeader.Controls.Add(lblThumb);

            lblTitle = new Label
            {
                Text = "Tên sản phẩm",
                Dock = DockStyle.Top,
                Height = 42,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoEllipsis = true,
                Padding = new Padding(0, 4, 0, 0),
                UseMnemonic = false
            };

            lblCategory = new Label
            {
                Text = "Thể loại: Game",
                Dock = DockStyle.Top,
                Height = 22,
                ForeColor = Color.FromArgb(161, 181, 196),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Regular),
                AutoEllipsis = true,
                UseMnemonic = false
            };

            lblRating = new Label
            {
                Text = "⭐ 5.0 (0 đánh giá)",
                Dock = DockStyle.Top,
                Height = 22,
                ForeColor = Color.FromArgb(243, 156, 18),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                UseMnemonic = false
            };

            lblPrice = new Label
            {
                Text = "150.000 ₫",
                Dock = DockStyle.Top,
                Height = 32,
                ForeColor = Color.FromArgb(164, 208, 7),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Padding = new Padding(0, 4, 0, 0),
                UseMnemonic = false
            };

            Panel panelButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 36
            };

            btnDetail = new Button
            {
                Text = "Chi tiết",
                Width = 95,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnDetail.FlatAppearance.BorderSize = 0;
            btnDetail.Click += (s, e) => OnDetailClicked?.Invoke(MaSanPham);

            btnAddToCart = new Button
            {
                Text = "+ Thêm giỏ",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnAddToCart.FlatAppearance.BorderSize = 0;
            btnAddToCart.Click += (s, e) => OnAddToCartClicked?.Invoke(MaSanPham);

            panelButtons.Controls.Add(btnAddToCart);
            panelButtons.Controls.Add(btnDetail);

            this.Controls.Add(panelButtons);
            this.Controls.Add(lblPrice);
            this.Controls.Add(lblRating);
            this.Controls.Add(lblCategory);
            this.Controls.Add(lblTitle);
            this.Controls.Add(panelHeader);

            // Hover effect
            this.MouseEnter += (s, e) => this.BackColor = Color.FromArgb(42, 71, 94);
            this.MouseLeave += (s, e) => this.BackColor = Color.FromArgb(30, 45, 62);
        }

        public void BindData(SanPhamDTO sp)
        {
            MaSanPham = sp.MaSanPham;
            lblTitle.Text = sp.TenSanPham;
            lblCategory.Text = $"Thể loại: {sp.TenDanhMuc}";
            lblRating.Text = $"⭐ {sp.DiemDanhGiaTrungBinh:0.0} ({sp.SoLuongDanhGia} đánh giá)";
            lblPrice.Text = FormatHelper.FormatCurrency(sp.GiaBan);
        }
    }
}
