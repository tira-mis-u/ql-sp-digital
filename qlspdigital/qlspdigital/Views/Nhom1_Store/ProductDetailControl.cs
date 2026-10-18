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
        private readonly DanhGiaService _danhGiaService;

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
        private Panel panelBottom = null!;
        private Panel panelDesc = null!;
        private Panel panelReviews = null!;
        private Label lblDescTitle = null!;
        private Label lblReviewsTitle = null!;
        private Button btnWriteReview = null!;
        private DataGridView dgvReviewsDetail = null!;
        private Label lblNoReviews = null!;
        private Panel panelBuy = null!;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private Panel panelReviewsHeader = null!;

        public ProductDetailControl()
        {
            _sanPhamService = new SanPhamService();
            _gioHangService = new GioHangService();
            _danhGiaService = new DanhGiaService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;

            // Lắng nghe sự kiện cập nhật đánh giá theo thời gian thực
            DanhGiaService.ReviewUpdated += (spId) =>
            {
                if ((spId == _maSanPham || spId == 0) && _maSanPham > 0 && !this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try
                    {
                        this.BeginInvoke(new Action(() => LoadProductDetail(_maSanPham)));
                    }
                    catch { }
                }
            };
            UserSession.SessionChanged += () =>
            {
                if (_maSanPham > 0 && !this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try
                    {
                        this.BeginInvoke(new Action(() => LoadProductDetail(_maSanPham)));
                    }
                    catch { }
                }
            };
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            btnBack = new Button();
            panelTop = new Panel();
            panelBuy = new Panel();
            btnReviews = new Button();
            btnAddToCart = new Button();
            lblPrice = new Label();
            lblRating = new Label();
            lblSize = new Label();
            lblVersion = new Label();
            lblPublisher = new Label();
            lblCategory = new Label();
            lblTitle = new Label();
            panelBottom = new Panel();
            panelReviews = new Panel();
            lblNoReviews = new Label();
            dgvReviewsDetail = new DataGridView();
            panelReviewsHeader = new Panel();
            lblReviewsTitle = new Label();
            btnWriteReview = new Button();
            panelDesc = new Panel();
            rtfDescription = new RichTextBox();
            lblDescTitle = new Label();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            panelTop.SuspendLayout();
            panelBuy.SuspendLayout();
            panelBottom.SuspendLayout();
            panelReviews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReviewsDetail).BeginInit();
            panelReviewsHeader.SuspendLayout();
            panelDesc.SuspendLayout();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(49, 98, 130);
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(29, 24);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(229, 48);
            btnBack.TabIndex = 2;
            btnBack.Text = "Quay lại Cửa hàng";
            btnBack.UseMnemonic = false;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += BtnBack_Click;
            // 
            // panelTop
            // 
            panelTop.Anchor = AnchorStyles.None;
            panelTop.BackColor = Color.FromArgb(27, 40, 56);
            panelTop.Controls.Add(panelBuy);
            panelTop.Controls.Add(lblRating);
            panelTop.Controls.Add(lblSize);
            panelTop.Controls.Add(lblVersion);
            panelTop.Controls.Add(lblPublisher);
            panelTop.Controls.Add(lblCategory);
            panelTop.Controls.Add(lblTitle);
            panelTop.Location = new Point(25, 52);
            panelTop.Margin = new Padding(3, 4, 3, 4);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(20, 16, 20, 16);
            panelTop.Size = new Size(1143, 205);
            panelTop.TabIndex = 1;
            // 
            // panelBuy
            // 
            panelBuy.BackColor = Color.FromArgb(18, 25, 35);
            panelBuy.Controls.Add(btnReviews);
            panelBuy.Controls.Add(btnAddToCart);
            panelBuy.Controls.Add(lblPrice);
            panelBuy.Dock = DockStyle.Right;
            panelBuy.Location = new Point(813, 16);
            panelBuy.Margin = new Padding(3, 4, 3, 4);
            panelBuy.Name = "panelBuy";
            panelBuy.Padding = new Padding(16, 12, 16, 12);
            panelBuy.Size = new Size(310, 173);
            panelBuy.TabIndex = 0;
            // 
            // btnReviews
            // 
            btnReviews.BackColor = Color.FromArgb(49, 98, 130);
            btnReviews.Cursor = Cursors.Hand;
            btnReviews.Dock = DockStyle.Bottom;
            btnReviews.FlatAppearance.BorderSize = 0;
            btnReviews.FlatStyle = FlatStyle.Flat;
            btnReviews.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReviews.ForeColor = Color.White;
            btnReviews.Location = new Point(16, 123);
            btnReviews.Margin = new Padding(3, 4, 3, 4);
            btnReviews.Name = "btnReviews";
            btnReviews.Size = new Size(278, 38);
            btnReviews.TabIndex = 0;
            btnReviews.Text = "Đánh giá & Nhận xét";
            btnReviews.UseMnemonic = false;
            btnReviews.UseVisualStyleBackColor = false;
            btnReviews.Click += BtnReviews_Click;
            // 
            // btnAddToCart
            // 
            btnAddToCart.BackColor = Color.FromArgb(90, 160, 45);
            btnAddToCart.Cursor = Cursors.Hand;
            btnAddToCart.Dock = DockStyle.Top;
            btnAddToCart.FlatAppearance.BorderSize = 0;
            btnAddToCart.FlatStyle = FlatStyle.Flat;
            btnAddToCart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddToCart.ForeColor = Color.White;
            btnAddToCart.Location = new Point(16, 52);
            btnAddToCart.Margin = new Padding(3, 4, 3, 4);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(278, 48);
            btnAddToCart.TabIndex = 1;
            btnAddToCart.Text = "Thêm vào Giỏ hàng";
            btnAddToCart.UseMnemonic = false;
            btnAddToCart.UseVisualStyleBackColor = false;
            btnAddToCart.Click += BtnAddToCart_Click;
            // 
            // lblPrice
            // 
            lblPrice.Dock = DockStyle.Top;
            lblPrice.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblPrice.ForeColor = Color.FromArgb(164, 208, 7);
            lblPrice.Location = new Point(16, 12);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(278, 40);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "150.000 ₫";
            lblPrice.TextAlign = ContentAlignment.MiddleCenter;
            lblPrice.UseMnemonic = false;
            // 
            // lblRating
            // 
            lblRating.AutoSize = true;
            lblRating.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRating.ForeColor = Color.FromArgb(243, 156, 18);
            lblRating.Location = new Point(22, 150);
            lblRating.Name = "lblRating";
            lblRating.Size = new Size(201, 23);
            lblRating.TabIndex = 1;
            lblRating.Text = "⭐ 5.0 / 5.0 (0 đánh giá)";
            lblRating.UseMnemonic = false;
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Font = new Font("Segoe UI", 9.5F);
            lblSize.ForeColor = Color.FromArgb(199, 213, 224);
            lblSize.Location = new Point(260, 116);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(136, 21);
            lblSize.TabIndex = 2;
            lblSize.Text = "Dung lượng: 0 MB";
            lblSize.UseMnemonic = false;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 9.5F);
            lblVersion.ForeColor = Color.FromArgb(199, 213, 224);
            lblVersion.Location = new Point(22, 116);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(119, 21);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "Phiên bản: 1.0.0";
            lblVersion.UseMnemonic = false;
            // 
            // lblPublisher
            // 
            lblPublisher.AutoSize = true;
            lblPublisher.Font = new Font("Segoe UI", 10F);
            lblPublisher.ForeColor = Color.FromArgb(199, 213, 224);
            lblPublisher.Location = new Point(22, 84);
            lblPublisher.Name = "lblPublisher";
            lblPublisher.Size = new Size(183, 23);
            lblPublisher.TabIndex = 4;
            lblPublisher.Text = "Nhà phát hành: Studio";
            lblPublisher.UseMnemonic = false;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCategory.ForeColor = Color.FromArgb(102, 192, 244);
            lblCategory.Location = new Point(22, 54);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(129, 23);
            lblCategory.TabIndex = 5;
            lblCategory.Text = "Thể loại: Game";
            lblCategory.UseMnemonic = false;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(770, 36);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "Tên sản phẩm kỹ thuật số";
            lblTitle.UseMnemonic = false;
            // 
            // panelBottom
            // 
            panelBottom.Anchor = AnchorStyles.None;
            panelBottom.BackColor = Color.FromArgb(23, 26, 33);
            panelBottom.Controls.Add(panelReviews);
            panelBottom.Controls.Add(panelDesc);
            panelBottom.Location = new Point(29, 323);
            panelBottom.Margin = new Padding(3, 4, 3, 4);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1143, 560);
            panelBottom.TabIndex = 0;
            // 
            // panelReviews
            // 
            panelReviews.BackColor = Color.FromArgb(27, 40, 56);
            panelReviews.Controls.Add(lblNoReviews);
            panelReviews.Controls.Add(dgvReviewsDetail);
            panelReviews.Controls.Add(panelReviewsHeader);
            panelReviews.Location = new Point(0, 0);
            panelReviews.Margin = new Padding(3, 4, 3, 4);
            panelReviews.Name = "panelReviews";
            panelReviews.Padding = new Padding(17, 20, 17, 20);
            panelReviews.Size = new Size(229, 133);
            panelReviews.TabIndex = 0;
            // 
            // lblNoReviews
            // 
            lblNoReviews.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblNoReviews.ForeColor = Color.FromArgb(161, 181, 196);
            lblNoReviews.Location = new Point(0, 0);
            lblNoReviews.Name = "lblNoReviews";
            lblNoReviews.Size = new Size(114, 31);
            lblNoReviews.TabIndex = 0;
            lblNoReviews.Text = "Chưa có nhận xét nào từ cộng đồng.\nHãy là người đầu tiên đánh giá sản phẩm này!";
            lblNoReviews.TextAlign = ContentAlignment.MiddleCenter;
            lblNoReviews.Visible = false;
            // 
            // dgvReviewsDetail
            // 
            dgvReviewsDetail.AllowUserToAddRows = false;
            dgvReviewsDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReviewsDetail.BackgroundColor = Color.FromArgb(18, 25, 35);
            dgvReviewsDetail.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvReviewsDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvReviewsDetail.ColumnHeadersHeight = 29;
            dgvReviewsDetail.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvReviewsDetail.DefaultCellStyle = dataGridViewCellStyle2;
            dgvReviewsDetail.EnableHeadersVisualStyles = false;
            dgvReviewsDetail.GridColor = Color.FromArgb(42, 71, 94);
            dgvReviewsDetail.Location = new Point(0, 0);
            dgvReviewsDetail.Margin = new Padding(3, 4, 3, 4);
            dgvReviewsDetail.Name = "dgvReviewsDetail";
            dgvReviewsDetail.ReadOnly = true;
            dgvReviewsDetail.RowHeadersVisible = false;
            dgvReviewsDetail.RowHeadersWidth = 51;
            dgvReviewsDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReviewsDetail.Size = new Size(274, 200);
            dgvReviewsDetail.TabIndex = 1;
            // 
            // panelReviewsHeader
            // 
            panelReviewsHeader.Controls.Add(lblReviewsTitle);
            panelReviewsHeader.Controls.Add(btnWriteReview);
            panelReviewsHeader.Dock = DockStyle.Top;
            panelReviewsHeader.Location = new Point(17, 20);
            panelReviewsHeader.Margin = new Padding(3, 4, 3, 4);
            panelReviewsHeader.Name = "panelReviewsHeader";
            panelReviewsHeader.Size = new Size(195, 43);
            panelReviewsHeader.TabIndex = 2;
            // 
            // lblReviewsTitle
            // 
            lblReviewsTitle.AutoSize = true;
            lblReviewsTitle.Dock = DockStyle.Left;
            lblReviewsTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblReviewsTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblReviewsTitle.Location = new Point(0, 0);
            lblReviewsTitle.Name = "lblReviewsTitle";
            lblReviewsTitle.Size = new Size(249, 25);
            lblReviewsTitle.TabIndex = 0;
            lblReviewsTitle.Text = "ĐÁNH GIÁ TỪ CỘNG ĐỒNG";
            lblReviewsTitle.UseMnemonic = false;
            // 
            // btnWriteReview
            // 
            btnWriteReview.BackColor = Color.FromArgb(90, 160, 45);
            btnWriteReview.Cursor = Cursors.Hand;
            btnWriteReview.Dock = DockStyle.Right;
            btnWriteReview.FlatAppearance.BorderSize = 0;
            btnWriteReview.FlatStyle = FlatStyle.Flat;
            btnWriteReview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnWriteReview.ForeColor = Color.White;
            btnWriteReview.Location = new Point(46, 0);
            btnWriteReview.Margin = new Padding(3, 4, 3, 4);
            btnWriteReview.Name = "btnWriteReview";
            btnWriteReview.Size = new Size(149, 43);
            btnWriteReview.TabIndex = 1;
            btnWriteReview.Text = "+ Viết nhận xét";
            btnWriteReview.UseMnemonic = false;
            btnWriteReview.UseVisualStyleBackColor = false;
            btnWriteReview.Click += BtnReviews_Click;
            // 
            // panelDesc
            // 
            panelDesc.BackColor = Color.FromArgb(27, 40, 56);
            panelDesc.Controls.Add(rtfDescription);
            panelDesc.Controls.Add(lblDescTitle);
            panelDesc.Location = new Point(0, 0);
            panelDesc.Margin = new Padding(3, 4, 3, 4);
            panelDesc.Name = "panelDesc";
            panelDesc.Padding = new Padding(17, 20, 17, 20);
            panelDesc.Size = new Size(229, 133);
            panelDesc.TabIndex = 1;
            // 
            // rtfDescription
            // 
            rtfDescription.BackColor = Color.FromArgb(18, 25, 35);
            rtfDescription.BorderStyle = BorderStyle.None;
            rtfDescription.Dock = DockStyle.Fill;
            rtfDescription.Font = new Font("Segoe UI", 10F);
            rtfDescription.ForeColor = Color.FromArgb(220, 220, 220);
            rtfDescription.Location = new Point(17, 57);
            rtfDescription.Margin = new Padding(3, 4, 3, 4);
            rtfDescription.Name = "rtfDescription";
            rtfDescription.ReadOnly = true;
            rtfDescription.Size = new Size(195, 56);
            rtfDescription.TabIndex = 0;
            rtfDescription.Text = "";
            // 
            // lblDescTitle
            // 
            lblDescTitle.Dock = DockStyle.Top;
            lblDescTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblDescTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblDescTitle.Location = new Point(17, 20);
            lblDescTitle.Name = "lblDescTitle";
            lblDescTitle.Size = new Size(195, 37);
            lblDescTitle.TabIndex = 1;
            lblDescTitle.Text = "GIỚI THIỆU & ĐẶC TẢ TÍNH NĂNG";
            lblDescTitle.UseMnemonic = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Người đánh giá";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Số sao";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Nhận xét";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Ngày đăng";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // ProductDetailControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(23, 26, 33);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Controls.Add(btnBack);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ProductDetailControl";
            Padding = new Padding(29, 33, 29, 33);
            Size = new Size(1189, 933);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelBuy.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            panelReviews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReviewsDetail).EndInit();
            panelReviewsHeader.ResumeLayout(false);
            panelReviewsHeader.PerformLayout();
            panelDesc.ResumeLayout(false);
            ResumeLayout(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelTop == null || panelBottom == null || panelDesc == null || panelReviews == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(400, this.ClientSize.Height);
            int fullW = totalW - 50;

            if (lblTitle != null)
            {
                lblTitle.Width = Math.Max(200, panelTop.ClientSize.Width - 340);
            }

            int topH = 205;
            panelTop.Location = new Point(25, 52);
            panelTop.Size = new Size(fullW, topH);

            int bottomY = panelTop.Bottom + 12;
            int bottomH = Math.Max(260, totalH - bottomY - 15);
            panelBottom.Location = new Point(25, bottomY);
            panelBottom.Size = new Size(fullW, bottomH);

            int pnlW = panelBottom.ClientSize.Width;
            int descW = Math.Max(260, (int)(pnlW * 0.48));
            int revW = Math.Max(260, pnlW - descW - 15);

            panelDesc.Location = new Point(0, 0);
            panelDesc.Size = new Size(descW, bottomH);

            panelReviews.Location = new Point(descW + 15, 0);
            panelReviews.Size = new Size(revW, bottomH);

            if (dgvReviewsDetail != null)
            {
                int dgvH = Math.Max(120, panelReviews.ClientSize.Height - 65);
                dgvReviewsDetail.Location = new Point(15, 50);
                dgvReviewsDetail.Size = new Size(panelReviews.ClientSize.Width - 30, dgvH);
                if (lblNoReviews != null)
                {
                    lblNoReviews.Location = new Point(15, 100);
                    lblNoReviews.Size = new Size(panelReviews.ClientSize.Width - 30, 90);
                }
            }
        }

        public void LoadProductDetail(int maSanPham)
        {
            if (maSanPham <= 0) return;
            _maSanPham = maSanPham;
            _currentProduct = _sanPhamService.LayChiTietSanPham(maSanPham);

            if (_currentProduct == null)
            {
                return;
            }

            lblTitle.Text = _currentProduct.TenSanPham;
            lblCategory.Text = $"Thể loại: {_currentProduct.TenDanhMuc}";
            lblPublisher.Text = $"Nhà phát hành: {_currentProduct.TenNhaPhatHanh}";
            lblVersion.Text = $"Phiên bản: {_currentProduct.PhienBan}";
            lblSize.Text = $"Dung lượng: {FormatHelper.FormatFileSize(_currentProduct.DungLuongMB)}";
            lblRating.Text = $"⭐ {_currentProduct.DiemDanhGiaTrungBinh:0.0} / 5.0 ({_currentProduct.SoLuongDanhGia} lượt nhận xét)";
            lblPrice.Text = FormatHelper.FormatCurrency(_currentProduct.GiaBan, allowFreeText: true);

            string specs = $"\n\n═════════════════════════════════════════════════════════════════\n" +
                           $"THÔNG TIN BẢN QUYỀN & THÔNG SỐ KỸ THUẬT (DPMS CERTIFIED):\n" +
                           $"• Nền tảng tương thích: Windows 10 / Windows 11 (64-bit)\n" +
                           $"• Hình thức phân phối: Bản quyền số chính hãng (DRM-Free)\n" +
                           $"• Dung lượng tải tệp: {FormatHelper.FormatFileSize(_currentProduct.DungLuongMB)}\n" +
                           $"• Phiên bản phát hành: {_currentProduct.PhienBan}\n" +
                           $"• Nhà phát triển / Studio: {_currentProduct.TenNhaPhatHanh}\n" +
                           $"• Chính sách bảo hành: Cấp lại tệp tải xuống & hỗ trợ kích hoạt trọn đời";

            rtfDescription.Text = (string.IsNullOrWhiteSpace(_currentProduct.MoTa)
                ? "Sản phẩm kỹ thuật số chính hãng được phân phối bản quyền trên hệ thống DPMS. Tương thích đa nền tảng, hỗ trợ cập nhật trọn đời."
                : _currentProduct.MoTa) + specs;

            LoadCommunityReviews(maSanPham);
        }

        private void LoadCommunityReviews(int maSanPham)
        {
            if (dgvReviewsDetail == null) return;
            dgvReviewsDetail.Rows.Clear();
            var reviews = _danhGiaService.LayDanhGiaTheoSanPham(maSanPham);
            if (reviews.Count == 0)
            {
                lblNoReviews.Visible = true;
                dgvReviewsDetail.Visible = false;
                lblReviewsTitle.Text = "ĐÁNH GIÁ TỪ CỘNG ĐỒNG (0)";
            }
            else
            {
                lblNoReviews.Visible = false;
                dgvReviewsDetail.Visible = true;
                lblReviewsTitle.Text = $"ĐÁNH GIÁ TỪ CỘNG ĐỒNG ({reviews.Count})";
                foreach (var r in reviews)
                {
                    dgvReviewsDetail.Rows.Add(r.HoTenNguoiDung, $"⭐ {r.SoSao}/5", r.BinhLuan, FormatHelper.FormatDateTime(r.NgayTao));
                }
            }
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
