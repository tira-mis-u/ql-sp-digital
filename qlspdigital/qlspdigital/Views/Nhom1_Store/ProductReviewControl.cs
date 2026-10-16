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
    public class ProductReviewControl : UserControl
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

        public ProductReviewControl()
        {
            _sanPhamService = new SanPhamService();
            _danhGiaService = new DanhGiaService();

            InitializeComponent();
            LoadProductList();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);

            // Nút quay lại
            btnBack = new Button
            {
                Text = "← Quay lại Cửa hàng",
                Size = new Size(160, 32),
                Location = new Point(25, 15),
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => OnBackToStore?.Invoke();

            // Khung chọn sản phẩm
            Panel panelTop = new Panel
            {
                Location = new Point(25, 55),
                Size = new Size(950, 50),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(15, 10, 15, 10)
            };

            Label lblSelect = new Label
            {
                Text = "Chọn sản phẩm xem đánh giá:",
                ForeColor = Color.White,
                Location = new Point(15, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            cboProduct = new ComboBox
            {
                Location = new Point(230, 12),
                Width = 350,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White
            };
            cboProduct.SelectedIndexChanged += (s, e) =>
            {
                if (cboProduct.SelectedItem is ProductComboItem item)
                {
                    _maSanPhamHienTai = item.Id;
                    LoadReviews();
                }
            };

            lblAverageScore = new Label
            {
                Text = "Điểm TB: ⭐ 5.0 / 5.0",
                Dock = DockStyle.Right,
                ForeColor = Color.FromArgb(243, 156, 18),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                Width = 250
            };

            panelTop.Controls.Add(lblAverageScore);
            panelTop.Controls.Add(cboProduct);
            panelTop.Controls.Add(lblSelect);

            // Khung viết đánh giá (Form CRUD 1-5 sao)
            Panel panelForm = new Panel
            {
                Location = new Point(25, 115),
                Size = new Size(950, 160),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(15)
            };

            Label lblFormTitle = new Label
            {
                Text = "✍️ VIẾT HOẶC CHỈNH SỬA ĐÁNH GIÁ CỦA BẠN",
                Location = new Point(15, 12),
                AutoSize = true,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold)
            };

            Label lblStarTitle = new Label
            {
                Text = "Mức độ hài lòng:",
                Location = new Point(15, 45),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f)
            };

            cboStar = new ComboBox
            {
                Location = new Point(140, 42),
                Width = 220,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.FromArgb(243, 156, 18)
            };
            cboStar.Items.Add("⭐⭐⭐⭐⭐ (5/5) - Rất tích cực / Cực tốt");
            cboStar.Items.Add("⭐⭐⭐⭐ (4/5) - Hài lòng");
            cboStar.Items.Add("⭐⭐⭐ (3/5) - Bình thường");
            cboStar.Items.Add("⭐⭐ (2/5) - Kém");
            cboStar.Items.Add("⭐ (1/5) - Rất tệ / Không khuyến nghị");
            cboStar.SelectedIndex = 0;

            Label lblCommentTitle = new Label
            {
                Text = "Nội dung nhận xét:",
                Location = new Point(15, 75),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f)
            };

            txtComment = new TextBox
            {
                Location = new Point(140, 75),
                Size = new Size(620, 65),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f)
            };

            btnSubmit = new Button
            {
                Text = "Gửi nhận xét",
                Location = new Point(780, 75),
                Size = new Size(140, 32),
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += BtnSubmit_Click;

            btnDelete = new Button
            {
                Text = "Xóa nhận xét",
                Location = new Point(780, 112),
                Size = new Size(140, 28),
                BackColor = Color.FromArgb(180, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
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
            dgvReviews = new DataGridView
            {
                Location = new Point(25, 290),
                Size = new Size(950, 350),
                BackgroundColor = Color.FromArgb(27, 40, 56),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(42, 71, 94),
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvReviews.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvReviews.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
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
            dgvReviews.Columns["HoTen"].Width = 160;
            dgvReviews.Columns["SoSao"].Width = 90;
            dgvReviews.Columns["NgayTao"].Width = 140;

            this.Controls.Add(dgvReviews);
            this.Controls.Add(panelForm);
            this.Controls.Add(panelTop);
            this.Controls.Add(btnBack);
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

        private class ProductComboItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public override string ToString() => Name;
        }
    }
}
