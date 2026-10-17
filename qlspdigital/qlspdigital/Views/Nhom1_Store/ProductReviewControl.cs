using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom1_Store;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom1_Store
{
    // UI 14: Đánh giá & Nhận xét sản phẩm (Steam Reviews style)
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public partial class ProductReviewControl : UserControl
    {
        private readonly SanPhamService _sanPhamService;
        private readonly DanhGiaService _danhGiaService;

        public event Action? OnBackToStore;

        private int _maSanPhamHienTai = 1;
        private ComboBox cboProduct = null!;
        private ComboBox cboStar = null!;
        private TextBox txtComment = null!;
        private Button btnSubmit = null!;
        private Button btnDelete = null!;
        private Button btnBack = null!;
        private DataGridView dgvReviews = null!;
        private Label lblAverageScore = null!;
        private Panel panelTop = null!;
        private Panel panelForm = null!;

        public ProductReviewControl()
        {
            _sanPhamService = new SanPhamService();
            _danhGiaService = new DanhGiaService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadProductList();
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
            btnBack.Text = "← Quay lại Cửa hàng";
            btnBack.Size = new Size(195, 36);
            btnBack.Location = new Point(25, 12);
            btnBack.BackColor = Color.FromArgb(49, 98, 130);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnBack.Cursor = Cursors.Hand;
            btnBack.UseMnemonic = false;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;

            // Khung chọn sản phẩm
            panelTop = new Panel();
            panelTop.Location = new Point(25, 55);
            panelTop.Size = new Size(950, 50);
            panelTop.BackColor = Color.FromArgb(27, 40, 56);
            panelTop.Padding = new Padding(15, 10, 15, 10);

            Label lblSelect = new Label();
            lblSelect.Text = "Chọn sản phẩm xem đánh giá:";
            lblSelect.ForeColor = Color.White;
            lblSelect.Location = new Point(15, 15);
            lblSelect.AutoSize = true;
            lblSelect.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblSelect.UseMnemonic = false;

            cboProduct = new ComboBox();
            cboProduct.Location = new Point(275, 12);
            cboProduct.Width = 280;
            cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProduct.Font = new Font("Segoe UI", 9.5f);
            cboProduct.BackColor = Color.FromArgb(18, 25, 35);
            cboProduct.ForeColor = Color.White;
            cboProduct.FlatStyle = FlatStyle.Flat;
            cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

            lblAverageScore = new Label();
            lblAverageScore.Text = "Điểm TB: ⭐ 5.0 / 5.0";
            lblAverageScore.Dock = DockStyle.Right;
            lblAverageScore.ForeColor = Color.FromArgb(243, 156, 18);
            lblAverageScore.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblAverageScore.TextAlign = ContentAlignment.MiddleRight;
            lblAverageScore.Width = 350;
            lblAverageScore.UseMnemonic = false;

            panelTop.Controls.Add(lblAverageScore);
            panelTop.Controls.Add(cboProduct);
            panelTop.Controls.Add(lblSelect);

            // Khung viết đánh giá (Form CRUD 1-5 sao)
            panelForm = new Panel();
            panelForm.Location = new Point(25, 115);
            panelForm.Size = new Size(950, 160);
            panelForm.BackColor = Color.FromArgb(27, 40, 56);
            panelForm.Padding = new Padding(15);

            Label lblFormTitle = new Label();
            lblFormTitle.Text = "✍️ VIẾT HOẶC CHỈNH SỬA ĐÁNH GIÁ CỦA BẠN";
            lblFormTitle.Location = new Point(15, 12);
            lblFormTitle.AutoSize = true;
            lblFormTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblFormTitle.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblFormTitle.UseMnemonic = false;

            Label lblStarTitle = new Label();
            lblStarTitle.Text = "Mức độ hài lòng:";
            lblStarTitle.Location = new Point(15, 45);
            lblStarTitle.AutoSize = true;
            lblStarTitle.ForeColor = Color.White;
            lblStarTitle.Font = new Font("Segoe UI", 9.5f);
            lblStarTitle.UseMnemonic = false;

            cboStar = new ComboBox();
            cboStar.Location = new Point(165, 42);
            cboStar.Width = 310;
            cboStar.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStar.Font = new Font("Segoe UI", 9.5f);
            cboStar.BackColor = Color.FromArgb(18, 25, 35);
            cboStar.ForeColor = Color.FromArgb(243, 156, 18);
            cboStar.FlatStyle = FlatStyle.Flat;
            cboStar.Items.Add("⭐⭐⭐⭐⭐ (5/5) - Rất tích cực / Cực tốt");
            cboStar.Items.Add("⭐⭐⭐⭐ (4/5) - Hài lòng");
            cboStar.Items.Add("⭐⭐⭐ (3/5) - Bình thường");
            cboStar.Items.Add("⭐⭐ (2/5) - Kém");
            cboStar.Items.Add("⭐ (1/5) - Rất tệ / Không khuyến nghị");
            cboStar.SelectedIndex = 0;

            Label lblCommentTitle = new Label();
            lblCommentTitle.Text = "Nội dung nhận xét:";
            lblCommentTitle.Location = new Point(15, 78);
            lblCommentTitle.AutoSize = true;
            lblCommentTitle.ForeColor = Color.White;
            lblCommentTitle.Font = new Font("Segoe UI", 9.5f);
            lblCommentTitle.UseMnemonic = false;

            txtComment = new TextBox();
            txtComment.Location = new Point(165, 75);
            txtComment.Size = new Size(595, 65);
            txtComment.Multiline = true;
            txtComment.ScrollBars = ScrollBars.Vertical;
            txtComment.BackColor = Color.FromArgb(18, 25, 35);
            txtComment.ForeColor = Color.White;
            txtComment.Font = new Font("Segoe UI", 9.5f);

            btnSubmit = new Button();
            btnSubmit.Text = "Gửi nhận xét";
            btnSubmit.Location = new Point(775, 75);
            btnSubmit.Size = new Size(150, 32);
            btnSubmit.BackColor = Color.FromArgb(92, 126, 16);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.UseMnemonic = false;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += BtnSubmit_Click;

            btnDelete = new Button();
            btnDelete.Text = "Xóa nhận xét";
            btnDelete.Location = new Point(775, 112);
            btnDelete.Size = new Size(150, 28);
            btnDelete.BackColor = Color.FromArgb(180, 40, 40);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.UseMnemonic = false;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;

            panelForm.Controls.Add(btnDelete);
            panelForm.Controls.Add(btnSubmit);
            panelForm.Controls.Add(txtComment);
            panelForm.Controls.Add(lblCommentTitle);
            panelForm.Controls.Add(cboStar);
            panelForm.Controls.Add(lblStarTitle);
            panelForm.Controls.Add(lblFormTitle);

            // Bảng danh sách đánh giá của cộng đồng (Read)
            dgvReviews = new DataGridView();
            dgvReviews.Location = new Point(25, 290);
            dgvReviews.Size = new Size(950, 440);
            dgvReviews.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReviews.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvReviews.ForeColor = Color.White;
            dgvReviews.GridColor = Color.FromArgb(42, 71, 94);
            dgvReviews.BorderStyle = BorderStyle.None;
            dgvReviews.RowHeadersVisible = false;
            dgvReviews.AllowUserToAddRows = false;
            dgvReviews.ReadOnly = true;
            dgvReviews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReviews.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvReviews.DefaultCellStyle.ForeColor = Color.White;
            dgvReviews.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvReviews.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvReviews.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvReviews.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvReviews.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvReviews.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvReviews.EnableHeadersVisualStyles = false;

            // Thiết lập cột
            dgvReviews.Columns.Add("HoTen", "Người đánh giá");
            dgvReviews.Columns.Add("SoSao", "Số sao");
            dgvReviews.Columns.Add("BinhLuan", "Nhận xét");
            dgvReviews.Columns.Add("NgayTao", "Thời gian");
            dgvReviews.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvReviews.Columns["HoTen"].Width = 170;

            dgvReviews.Columns["SoSao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvReviews.Columns["SoSao"].Width = 90;
            dgvReviews.Columns["SoSao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvReviews.Columns["BinhLuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReviews.Columns["BinhLuan"].FillWeight = 100;

            dgvReviews.Columns["NgayTao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvReviews.Columns["NgayTao"].Width = 140;
            dgvReviews.Columns["NgayTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Controls.Add(dgvReviews);
            this.Controls.Add(panelForm);
            this.Controls.Add(panelTop);
            this.Controls.Add(btnBack);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelTop == null || panelForm == null || dgvReviews == null || txtComment == null || btnSubmit == null) return;
            int totalW = Math.Max(1040, this.ClientSize.Width);
            int totalH = Math.Max(700, this.ClientSize.Height);

            int fullW = totalW - 50;
            panelTop.Location = new Point(25, 55);
            panelTop.Size = new Size(fullW, 50);

            panelForm.Location = new Point(25, 115);
            panelForm.Size = new Size(fullW, 160);

            int formW = panelForm.ClientSize.Width;
            int btnW = 150;
            btnSubmit.Location = new Point(formW - btnW - 20, 75);
            btnDelete.Location = new Point(formW - btnW - 20, 112);
            txtComment.Size = new Size(Math.Max(250, formW - 165 - btnW - 40), 65);

            int reviewH = Math.Max(250, totalH - 290 - 25);
            dgvReviews.Location = new Point(25, 290);
            dgvReviews.Size = new Size(fullW, reviewH);
        }

        private void LoadProductList()
        {
            try
            {
                var list = _sanPhamService.LayDanhSachSanPham();
                cboProduct.Items.Clear();
                foreach (var sp in list)
                {
                    cboProduct.Items.Add(new ProductComboItem { Id = sp.MaSanPham, Name = sp.TenSanPham });
                }

                if (cboProduct.Items.Count > 0)
                {
                    cboProduct.SelectedIndex = 0;
                }
            }
            catch { }
        }

        public void SetSelectedProduct(int maSanPham)
        {
            _maSanPhamHienTai = maSanPham;
            for (int i = 0; i < cboProduct.Items.Count; i++)
            {
                if (cboProduct.Items[i] is ProductComboItem item && item.Id == maSanPham)
                {
                    cboProduct.SelectedIndex = i;
                    break;
                }
            }
            LoadReviews();
        }

        private void LoadReviews()
        {
            dgvReviews.Rows.Clear();
            var reviews = _danhGiaService.LayDanhGiaTheoSanPham(_maSanPhamHienTai);

            double totalScore = 0;
            foreach (var r in reviews)
            {
                totalScore += r.SoSao;
                dgvReviews.Rows.Add(r.HoTenNguoiDung, $"⭐ {r.SoSao}/5", r.BinhLuan, FormatHelper.FormatDateTime(r.NgayTao));
            }

            if (reviews.Count > 0)
            {
                double avg = Math.Round(totalScore / reviews.Count, 1);
                lblAverageScore.Text = $"Điểm TB: ⭐ {avg:0.0} / 5.0 ({reviews.Count} đánh giá)";
            }
            else
            {
                lblAverageScore.Text = "Chưa có đánh giá nào";
            }

            // Nếu người dùng đã đăng nhập, nạp đánh giá cũ của họ lên form nếu có
            if (UserSession.IsLoggedIn)
            {
                var myReview = _danhGiaService.LayDanhGiaCuaUser(UserSession.CurrentUserId, _maSanPhamHienTai);
                if (myReview != null)
                {
                    txtComment.Text = myReview.BinhLuan;
                    cboStar.SelectedIndex = Math.Clamp(5 - myReview.SoSao, 0, 4);
                    btnSubmit.Text = "Cập nhật đánh giá";
                }
                else
                {
                    txtComment.Clear();
                    cboStar.SelectedIndex = 0;
                    btnSubmit.Text = "Gửi nhận xét";
                }
            }
        }

        private void BtnSubmit_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để gửi đánh giá!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int star = 5 - cboStar.SelectedIndex;
            string comment = txtComment.Text.Trim();

            if (_danhGiaService.LuuDanhGia(UserSession.CurrentUserId, _maSanPhamHienTai, star, comment, out string msg))
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadReviews();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn) return;

            var myReview = _danhGiaService.LayDanhGiaCuaUser(UserSession.CurrentUserId, _maSanPhamHienTai);
            if (myReview == null)
            {
                MessageBox.Show("Bạn chưa có đánh giá nào cho sản phẩm này để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhận xét của mình?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_danhGiaService.XoaDanhGia(myReview.MaDanhGia, UserSession.CurrentUserId, out string msg))
                {
                    MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtComment.Clear();
                    btnSubmit.Text = "Gửi nhận xét";
                    LoadReviews();
                }
            }
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            OnBackToStore?.Invoke();
        }

        private void CboProduct_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboProduct.SelectedItem is ProductComboItem item)
            {
                _maSanPhamHienTai = item.Id;
                LoadReviews();
            }
        }

        private class ProductComboItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public override string ToString() => Name;
        }
    }
}
