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

        private int _maSanPhamHienTai = 1;
        private ComboBox cboProduct = null!;
        private ComboBox cboStar = null!;
        private TextBox txtComment = null!;
        private Button btnSubmit = null!;
        private Button btnDelete = null!;
        private DataGridView dgvReviews = null!;
        private Label lblAverageScore = null!;
        private Label lblSelect = null!;
        private Label lblStarTitle = null!;
        private Label lblCommentTitle = null!;
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

            // Khung chọn sản phẩm
            panelTop = new Panel();
            panelTop.Location = new Point(25, 15);
            panelTop.Size = new Size(950, 50);
            panelTop.BackColor = Color.FromArgb(27, 40, 56);
            panelTop.Padding = new Padding(15, 10, 15, 10);

            lblSelect = new Label();
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
            cboProduct.Font = new Font("Segoe UI", 10f);
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
            panelForm.Location = new Point(25, 75);
            panelForm.Size = new Size(950, 190);
            panelForm.BackColor = Color.FromArgb(27, 40, 56);
            panelForm.Padding = new Padding(15);

            Label lblFormTitle = new Label();
            lblFormTitle.Text = "VIẾT HOẶC CHỈNH SỬA ĐÁNH GIÁ CỦA BẠN";
            lblFormTitle.Location = new Point(15, 14);
            lblFormTitle.AutoSize = true;
            lblFormTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblFormTitle.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblFormTitle.UseMnemonic = false;

            lblStarTitle = new Label();
            lblStarTitle.Text = "Mức độ hài lòng:";
            lblStarTitle.Location = new Point(15, 48);
            lblStarTitle.AutoSize = true;
            lblStarTitle.ForeColor = Color.White;
            lblStarTitle.Font = new Font("Segoe UI", 9.5f);
            lblStarTitle.UseMnemonic = false;

            cboStar = new ComboBox();
            cboStar.Location = new Point(165, 45);
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

            lblCommentTitle = new Label();
            lblCommentTitle.Text = "Nội dung nhận xét:";
            lblCommentTitle.Location = new Point(15, 92);
            lblCommentTitle.AutoSize = true;
            lblCommentTitle.ForeColor = Color.White;
            lblCommentTitle.Font = new Font("Segoe UI", 9.5f);
            lblCommentTitle.UseMnemonic = false;

            txtComment = new TextBox();
            txtComment.Location = new Point(165, 88);
            txtComment.Size = new Size(560, 80);
            txtComment.Multiline = true;
            txtComment.ScrollBars = ScrollBars.Vertical;
            txtComment.BackColor = Color.FromArgb(18, 25, 35);
            txtComment.ForeColor = Color.White;
            txtComment.BorderStyle = BorderStyle.FixedSingle;
            txtComment.Font = new Font("Segoe UI", 10f);

            btnSubmit = new Button();
            btnSubmit.Text = "Gửi nhận xét";
            btnSubmit.Location = new Point(750, 88);
            btnSubmit.Size = new Size(160, 38);
            btnSubmit.BackColor = Color.FromArgb(90, 160, 45);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.UseMnemonic = false;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += BtnSubmit_Click;

            btnDelete = new Button();
            btnDelete.Text = "Xóa nhận xét";
            btnDelete.Location = new Point(750, 132);
            btnDelete.Size = new Size(160, 38);
            btnDelete.BackColor = Color.FromArgb(180, 40, 40);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
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
            dgvReviews.Location = new Point(25, 320);
            dgvReviews.Size = new Size(950, 410);
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
            dgvReviews.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 45, 62);
            dgvReviews.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(102, 192, 244);
            dgvReviews.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvReviews.EnableHeadersVisualStyles = false;

            // Thiết lập cột
            dgvReviews.Columns.Add("HoTen", "Người đánh giá");
            dgvReviews.Columns.Add("SoSao", "Số sao");
            dgvReviews.Columns.Add("BinhLuan", "Nhận xét");
            dgvReviews.Columns.Add("NgayTao", "Thời gian");

            dgvReviews.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReviews.Columns["HoTen"].FillWeight = 25;
            dgvReviews.Columns["HoTen"].MinimumWidth = 120;

            dgvReviews.Columns["SoSao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReviews.Columns["SoSao"].FillWeight = 12;
            dgvReviews.Columns["SoSao"].MinimumWidth = 70;
            dgvReviews.Columns["SoSao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvReviews.Columns["BinhLuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReviews.Columns["BinhLuan"].FillWeight = 45;
            dgvReviews.Columns["BinhLuan"].MinimumWidth = 150;

            dgvReviews.Columns["NgayTao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReviews.Columns["NgayTao"].FillWeight = 18;
            dgvReviews.Columns["NgayTao"].MinimumWidth = 110;
            dgvReviews.Columns["NgayTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Controls.Add(dgvReviews);
            this.Controls.Add(panelForm);
            this.Controls.Add(panelTop);

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
            if (panelTop == null || panelForm == null || dgvReviews == null || txtComment == null || btnSubmit == null || lblSelect == null || cboProduct == null || lblStarTitle == null || cboStar == null || lblCommentTitle == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            int fullW = totalW - 50;
            panelTop.Location = new Point(25, 15);
            panelTop.Size = new Size(fullW, 50);

            // Bố cục khung chọn sản phẩm theo Relative Flow (tránh hoàn toàn đè chữ nhãn)
            lblSelect.Location = new Point(15, 15);
            cboProduct.Location = new Point(lblSelect.Right + 16, 12);
            int maxCboW = Math.Max(180, panelTop.ClientSize.Width - cboProduct.Left - lblAverageScore.Width - 25);
            cboProduct.Width = Math.Clamp(maxCboW, 200, 420);

            // Bố cục Form nhận xét & đánh giá
            panelForm.Location = new Point(25, 75);
            int formW = panelForm.ClientSize.Width;

            int starY = 48;
            lblStarTitle.Location = new Point(15, starY + 4);
            cboStar.Location = new Point(lblStarTitle.Right + 16, starY);
            cboStar.Width = 320;

            int commentY = cboStar.Bottom + 16;
            lblCommentTitle.Location = new Point(15, commentY + 6);
            int commentX = Math.Max(lblStarTitle.Right, lblCommentTitle.Right) + 16;

            int btnW = 160;
            int btnX = formW - btnW - 20;
            btnSubmit.Location = new Point(btnX, commentY);
            btnSubmit.Size = new Size(btnW, 36);
            btnDelete.Location = new Point(btnX, commentY + 44);
            btnDelete.Size = new Size(btnW, 36);

            int txtW = Math.Max(200, btnX - commentX - 20);
            txtComment.Location = new Point(commentX, commentY);
            txtComment.Size = new Size(txtW, 80);

            int formH = Math.Max(195, txtComment.Bottom + 18);
            panelForm.Size = new Size(fullW, formH);

            int dgvY = panelForm.Bottom + 15;
            int reviewH = Math.Max(180, totalH - dgvY - 25);
            dgvReviews.Location = new Point(25, dgvY);
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

            int star = Math.Clamp(cboStar.SelectedIndex >= 0 ? 5 - cboStar.SelectedIndex : 5, 1, 5);
            string comment = txtComment.Text.Trim();

            if (string.IsNullOrWhiteSpace(comment))
            {
                MessageBox.Show("Vui lòng nhập nội dung nhận xét của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comment.Length > 2000)
            {
                MessageBox.Show("Nội dung nhận xét không được vượt quá 2000 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
