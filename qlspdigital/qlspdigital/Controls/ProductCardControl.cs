using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Controls
{
    // UserControl đại diện cho một thẻ sản phẩm trên lưới Store (Steam Card style)
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public partial class ProductCardControl : UserControl
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
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "ProductCardControl";
            this.Size = new Size(245, 305);
            this.BackColor = Color.FromArgb(30, 45, 62);
            this.Margin = new Padding(10);
            this.Padding = new Padding(10);

            // Giả lập ảnh bìa game / phần mềm bằng Panel gradient
            panelHeader = new Panel();
            panelHeader.SuspendLayout();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 100;
            panelHeader.BackColor = Color.FromArgb(23, 36, 50);

            Label lblThumb = new Label();
            lblThumb.Text = "🎮 DIGITAL\nPRODUCT";
            lblThumb.Dock = DockStyle.Fill;
            lblThumb.TextAlign = ContentAlignment.MiddleCenter;
            lblThumb.ForeColor = Color.FromArgb(102, 192, 244);
            lblThumb.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblThumb.UseMnemonic = false;
            panelHeader.Controls.Add(lblThumb);
            panelHeader.ResumeLayout(false);

            lblTitle = new Label();
            lblTitle.Text = "Tên sản phẩm";
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 42;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTitle.AutoEllipsis = true;
            lblTitle.Padding = new Padding(0, 4, 0, 0);
            lblTitle.UseMnemonic = false;

            lblCategory = new Label();
            lblCategory.Text = "Thể loại: Game";
            lblCategory.Dock = DockStyle.Top;
            lblCategory.Height = 22;
            lblCategory.ForeColor = Color.FromArgb(161, 181, 196);
            lblCategory.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            lblCategory.AutoEllipsis = true;
            lblCategory.UseMnemonic = false;

            lblRating = new Label();
            lblRating.Text = "⭐ 5.0 (0 đánh giá)";
            lblRating.Dock = DockStyle.Top;
            lblRating.Height = 22;
            lblRating.ForeColor = Color.FromArgb(243, 156, 18);
            lblRating.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblRating.UseMnemonic = false;

            lblPrice = new Label();
            lblPrice.Text = "150.000 ₫";
            lblPrice.Dock = DockStyle.Top;
            lblPrice.Height = 32;
            lblPrice.ForeColor = Color.FromArgb(164, 208, 7);
            lblPrice.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblPrice.Padding = new Padding(0, 4, 0, 0);
            lblPrice.UseMnemonic = false;

            Panel panelButtons = new Panel();
            panelButtons.SuspendLayout();
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Height = 36;

            btnDetail = new Button();
            btnDetail.Text = "Chi tiết";
            btnDetail.Width = 95;
            btnDetail.Dock = DockStyle.Left;
            btnDetail.BackColor = Color.FromArgb(49, 98, 130);
            btnDetail.ForeColor = Color.White;
            btnDetail.FlatStyle = FlatStyle.Flat;
            btnDetail.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnDetail.Cursor = Cursors.Hand;
            btnDetail.UseMnemonic = false;
            btnDetail.FlatAppearance.BorderSize = 0;
            btnDetail.Click += BtnDetail_Click;

            btnAddToCart = new Button();
            btnAddToCart.Text = "+ Thêm giỏ";
            btnAddToCart.Dock = DockStyle.Fill;
            btnAddToCart.BackColor = Color.FromArgb(92, 126, 16);
            btnAddToCart.ForeColor = Color.White;
            btnAddToCart.FlatStyle = FlatStyle.Flat;
            btnAddToCart.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnAddToCart.Cursor = Cursors.Hand;
            btnAddToCart.UseMnemonic = false;
            btnAddToCart.FlatAppearance.BorderSize = 0;
            btnAddToCart.Click += BtnAddToCart_Click;

            panelButtons.Controls.Add(btnAddToCart);
            panelButtons.Controls.Add(btnDetail);
            panelButtons.ResumeLayout(false);

            this.Controls.Add(panelButtons);
            this.Controls.Add(lblPrice);
            this.Controls.Add(lblRating);
            this.Controls.Add(lblCategory);
            this.Controls.Add(lblTitle);
            this.Controls.Add(panelHeader);

            // Hover effect
            this.MouseEnter += ProductCardControl_MouseEnter;
            this.MouseLeave += ProductCardControl_MouseLeave;

            this.ResumeLayout(false);
        }

        public void BindData(SanPhamDTO sp)
        {
            MaSanPham = sp.MaSanPham;
            lblTitle.Text = sp.TenSanPham;
            lblCategory.Text = $"Thể loại: {sp.TenDanhMuc}";
            lblRating.Text = $"⭐ {sp.DiemDanhGiaTrungBinh:0.0} ({sp.SoLuongDanhGia} đánh giá)";
            lblPrice.Text = FormatHelper.FormatCurrency(sp.GiaBan);
        }

        private void BtnDetail_Click(object? sender, EventArgs e)
        {
            OnDetailClicked?.Invoke(MaSanPham);
        }

        private void BtnAddToCart_Click(object? sender, EventArgs e)
        {
            OnAddToCartClicked?.Invoke(MaSanPham);
        }

        private void ProductCardControl_MouseEnter(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(42, 71, 94);
        }

        private void ProductCardControl_MouseLeave(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(30, 45, 62);
        }
    }
}
