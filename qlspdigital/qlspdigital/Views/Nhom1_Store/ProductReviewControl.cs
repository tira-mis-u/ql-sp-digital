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

        public ProductReviewControl()
        {
            _sanPhamService = new SanPhamService();
            _danhGiaService = new DanhGiaService();

            InitializeComponent();
            LoadProductList();
        }


        private void LoadProductList()
        {
            try
            {
                List<SanPhamDTO> list = _sanPhamService.LayDanhSachSanPham();
                cboProduct.Items.Clear();
                foreach (SanPhamDTO sp in list)
                {
                    cboProduct.Items.Add(new ProductComboItem { Id = sp.MaSanPham, Name = sp.TenSanPham });
                }

                if (cboProduct.Items.Count > 0) cboProduct.SelectedIndex = 0;
            }
            catch { }
        }

        public void SetSelectedProduct(int maSanPham)
        {
            _maSanPhamHienTai = maSanPham;
            bool found = false;
            for (int i = 0; i < cboProduct.Items.Count; i++)
            {
                if (cboProduct.Items[i] is ProductComboItem item && item.Id == maSanPham)
                {
                    if (cboProduct.SelectedIndex != i) cboProduct.SelectedIndex = i;
                    found = true;
                    break;
                }
            }

            if (!found && cboProduct.Items.Count > 0)
            {
                if (cboProduct.SelectedIndex != 0) cboProduct.SelectedIndex = 0;
                if (cboProduct.SelectedItem is ProductComboItem firstItem) _maSanPhamHienTai = firstItem.Id;
            }

            LoadReviews();
        }

        private void LoadReviews()
        {
            dgvReviews.Rows.Clear();
            List<DanhGiaDTO> reviews = _danhGiaService.LayDanhGiaTheoSanPham(_maSanPhamHienTai);

            double totalScore = 0;
            foreach (DanhGiaDTO r in reviews)
            {
                totalScore += r.SoSao;
                dgvReviews.Rows.Add($"{r.HoTenNguoiDung}  ✓ (Đã mua)", $"⭐ {r.SoSao}/5", r.BinhLuan, FormatHelper.FormatDateTime(r.NgayTao));
            }

            if (reviews.Count > 0)
            {
                double avg = Math.Round(totalScore / reviews.Count, 1);
                lblAverageScore.Text = $"Điểm TB: ⭐ {avg:0.0} / 5.0 ({reviews.Count} đánh giá từ người mua)";
            }
            else
            {
                lblAverageScore.Text = "Chưa có đánh giá nào";
            }

            // Nếu người dùng đã đăng nhập, nạp đánh giá cũ của họ lên form nếu có và hiển thị trạng thái quyền sở hữu
            if (UserSession.IsLoggedIn)
            {
                bool daMua = _danhGiaService.DaMuaSanPham(UserSession.CurrentUserId, _maSanPhamHienTai);
                DanhGiaDTO? myReview = _danhGiaService.LayDanhGiaCuaUser(UserSession.CurrentUserId, _maSanPhamHienTai);

                if (daMua)
                {
                    lblFormTitle.Text = "VIẾT HOẶC CHỈNH SỬA ĐÁNH GIÁ (BẠN ĐÃ SỞ HỮU SẢN PHẨM)";
                    lblFormTitle.ForeColor = Color.FromArgb(102, 192, 244);
                }
                else
                {
                    lblFormTitle.Text = "VIẾT ĐÁNH GIÁ (BẠN CHƯA MUA SẢN PHẨM NÀY)";
                    lblFormTitle.ForeColor = Color.FromArgb(243, 156, 18);
                }

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
            else
            {
                lblFormTitle.Text = "VIẾT ĐÁNH GIÁ (VUI LÒNG ĐĂNG NHẬP ĐỂ ĐÁNH GIÁ)";
                lblFormTitle.ForeColor = Color.FromArgb(199, 213, 224);
                txtComment.Clear();
                btnSubmit.Text = "Gửi nhận xét";
            }
        }

        private void BtnSubmit_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để gửi đánh giá!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra người dùng đã sở hữu sản phẩm trong Thư viện chưa
            if (!_danhGiaService.DaMuaSanPham(UserSession.CurrentUserId, _maSanPhamHienTai))
            {
                MessageBox.Show("Bạn cần mua và sở hữu sản phẩm này để có thể gửi đánh giá. Vui lòng mua sản phẩm trước khi chia sẻ nhận xét nhé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int star = Math.Clamp(cboStar.SelectedIndex >= 0 ? 5 - cboStar.SelectedIndex : 5, 1, 5);
            string comment = txtComment.Text.Trim();

            if (comment == "")
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

            DanhGiaDTO? myReview = _danhGiaService.LayDanhGiaCuaUser(UserSession.CurrentUserId, _maSanPhamHienTai);
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
