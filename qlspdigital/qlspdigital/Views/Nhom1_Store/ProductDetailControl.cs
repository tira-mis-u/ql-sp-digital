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

            if (btnBack != null)
            {
                btnBack.Location = new Point(25, 14);
                btnBack.Size = new Size(180, 36);
            }

            if (lblTitle != null)
            {
                lblTitle.Width = Math.Max(200, panelTop.ClientSize.Width - 340);
            }

            int topH = 205;
            int topY = (btnBack != null) ? (btnBack.Bottom + 12) : 62;
            panelTop.Location = new Point(25, topY);
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

            if (panelReviewsHeader != null)
            {
                panelReviewsHeader.Height = 36;
            }

            if (dgvReviewsDetail != null)
            {
                int headerBottom = (panelReviewsHeader != null) ? panelReviewsHeader.Bottom : 56;
                int gridTop = headerBottom + 10;
                int dgvH = Math.Max(120, panelReviews.ClientSize.Height - gridTop - 15);
                dgvReviewsDetail.Location = new Point(17, gridTop);
                dgvReviewsDetail.Size = new Size(panelReviews.ClientSize.Width - 34, dgvH);
                if (lblNoReviews != null)
                {
                    lblNoReviews.Location = new Point(17, gridTop + 30);
                    lblNoReviews.Size = new Size(panelReviews.ClientSize.Width - 34, 90);
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
