using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom1_Store;
using qlspdigital.BLL.Nhom2_Cart;
using qlspdigital.DTOs;
using qlspdigital.Shared;
using qlspdigital.Views.Main;

namespace qlspdigital.Views.Nhom1_Store
{
    // UI 3: Màn hình Xem chi tiết sản phẩm kỹ thuật số
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public partial class ProductDetailControl : UserControl
    {
        private readonly SanPhamService _sanPhamService;
        private readonly GioHangService _gioHangService;
        private readonly DanhGiaService _danhGiaService;


        private int _maSanPham;
        private SanPhamDTO? _currentProduct;

        public ProductDetailControl()
        {
            _sanPhamService = new SanPhamService();
            _gioHangService = new GioHangService();
            _danhGiaService = new DanhGiaService();

            InitializeComponent();
        }

        public void LoadProductDetail(int maSanPham)
        {
            if (maSanPham <= 0) return;
            _maSanPham = maSanPham;
            _currentProduct = _sanPhamService.LayChiTietSanPham(maSanPham);

            if (_currentProduct == null) return;

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

            rtfDescription.Text = (string.IsNullOrEmpty(_currentProduct.MoTa)
                ? "Sản phẩm kỹ thuật số chính hãng được phân phối bản quyền trên hệ thống DPMS. Tương thích đa nền tảng, hỗ trợ cập nhật trọn đời."
                : _currentProduct.MoTa) + specs;

            LoadCommunityReviews(maSanPham);
            CapNhatTrangThaiNutMua();
        }

        private void CapNhatTrangThaiNutMua()
        {
            if (btnAddToCart == null) return;

            if (!UserSession.IsLoggedIn)
            {
                btnAddToCart.Text = "Thêm vào giỏ hàng";
                btnAddToCart.Enabled = true;
                btnAddToCart.BackColor = Color.FromArgb(90, 160, 45);
                return;
            }

            if (new qlspdigital.DAL.Nhom5_Orders.ThuVienRepository().DaSoHuu(UserSession.CurrentUserId, _maSanPham))
            {
                btnAddToCart.Text = "✓ Đã sở hữu trong Thư viện";
                btnAddToCart.Enabled = false;
                btnAddToCart.BackColor = Color.FromArgb(60, 65, 75);
            }
            else if (_gioHangService.DaCoTrongGioHang(UserSession.CurrentUserId, _maSanPham))
            {
                btnAddToCart.Text = "✓ Đã có trong Giỏ hàng";
                btnAddToCart.Enabled = false;
                btnAddToCart.BackColor = Color.FromArgb(49, 98, 130);
            }
            else
            {
                btnAddToCart.Text = "Thêm vào giỏ hàng";
                btnAddToCart.Enabled = true;
                btnAddToCart.BackColor = Color.FromArgb(90, 160, 45);
            }
        }

        private void LoadCommunityReviews(int maSanPham)
        {
            if (dgvReviewsDetail == null) return;
            dgvReviewsDetail.Rows.Clear();
            System.Collections.Generic.List<DanhGiaDTO> reviews = _danhGiaService.LayDanhGiaTheoSanPham(maSanPham);
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
                foreach (DanhGiaDTO r in reviews)
                {
                    dgvReviewsDetail.Rows.Add($"{r.HoTenNguoiDung}  ✓ (Đã mua)", $"⭐ {r.SoSao}/5", r.BinhLuan, FormatHelper.FormatDateTime(r.NgayTao));
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
                // Cập nhật trạng thái nút và badge số món trong giỏ hàng trên menu thanh bên
                CapNhatTrangThaiNutMua();
                MainForm?.UpdateCartBadge();
            }
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CapNhatTrangThaiNutMua();
            }
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            MainForm?.NavigateToStore();
        }

        private void BtnReviews_Click(object? sender, EventArgs e)
        {
            MainForm?.NavigateToReviews(_maSanPham);
        }
    }
}
