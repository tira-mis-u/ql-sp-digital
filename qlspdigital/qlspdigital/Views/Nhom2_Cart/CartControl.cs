using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom2_Cart;
using qlspdigital.DTOs;
using qlspdigital.Shared;
using qlspdigital.Views.Main;

namespace qlspdigital.Views.Nhom2_Cart
{
    // UI 4: Giỏ hàng mua sắm, áp dụng Voucher và Thanh toán 1-Click bằng Ví
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public partial class CartControl : UserControl
    {
        private readonly GioHangService _gioHangService;
        private readonly MaGiamGiaService _voucherService;
        private readonly ViTaiKhoanService _viService;
        private readonly ThanhToanService _thanhToanService;


        private decimal _tamTinh = 0;
        private decimal _tienGiam = 0;
        private int? _maGiamGiaId = null;
        private string _appliedVoucherCode = string.Empty;
        private bool _isCheckingOut = false; // Anti double-click guard

        public CartControl()
        {
            _gioHangService = new GioHangService();
            _voucherService = new MaGiamGiaService();
            _viService = new ViTaiKhoanService();
            _thanhToanService = new ThanhToanService();

            InitializeComponent();
            LoadCart();
        }


        public void LoadCart()
        {
            dgvCart.Rows.Clear();
            _tamTinh = 0;
            _tienGiam = 0;
            _maGiamGiaId = null;
            _appliedVoucherCode = string.Empty;
            txtVoucher.Clear();
            if (btnApplyVoucher != null)
            {
                btnApplyVoucher.Text = "Áp dụng";
                btnApplyVoucher.BackColor = Color.FromArgb(49, 98, 130);
            }

            if (!UserSession.IsLoggedIn)
            {
                lblSubtotal.Text = "Tạm tính: 0 ₫ (Chưa đăng nhập)";
                lblTotal.Text = "Tổng: 0 ₫";
                lblWalletBalance.Text = "Số dư ví: 0 ₫";
                return;
            }

            List<GioHangItemDTO> items = _gioHangService.LayGioHang(UserSession.CurrentUserId);
            foreach (GioHangItemDTO item in items)
            {
                dgvCart.Rows.Add(item.MaGioHang, item.TenSanPham, FormatHelper.FormatCurrency(item.DonGia), item.SoLuong, FormatHelper.FormatCurrency(item.ThanhTien));
                _tamTinh += item.ThanhTien;
            }

            UpdateTotals();
            UpdateWalletDisplay();
        }

        private void UpdateWalletDisplay()
        {
            if (UserSession.IsLoggedIn)
            {
                decimal bal = _viService.LaySoDuVi(UserSession.CurrentUserId);
                UserSession.CurrentBalance = bal;
                lblWalletBalance.Text = $"Số dư ví: {FormatHelper.FormatCurrency(bal)}";
            }
            else
            {
                lblWalletBalance.Text = "Số dư ví: 0 ₫";
            }
        }

        private void UpdateTotals()
        {
            lblSubtotal.Text = $"Tạm tính: {FormatHelper.FormatCurrency(_tamTinh)}";
            if (_tienGiam > 0)
            {
                lblDiscount.Text = !string.IsNullOrEmpty(_appliedVoucherCode)
                    ? $"Giảm giá ({_appliedVoucherCode}): -{FormatHelper.FormatCurrency(_tienGiam)}"
                    : $"Giảm giá: -{FormatHelper.FormatCurrency(_tienGiam)}";
            }
            else
            {
                lblDiscount.Text = "Giảm giá: 0 ₫";
            }
            decimal phaiTra = Math.Max(0, _tamTinh - _tienGiam);
            lblTotal.Text = $"Tổng thanh toán: {FormatHelper.FormatCurrency(phaiTra)}";
        }

        private void TxtVoucher_TextChanged(object? sender, EventArgs e)
        {
            if (btnApplyVoucher == null) return;
            string currentText = txtVoucher.Text.Trim();
            if (_appliedVoucherCode != "" && currentText.ToUpper() != _appliedVoucherCode.ToUpper())
            {
                btnApplyVoucher.Text = "Áp dụng";
                btnApplyVoucher.BackColor = Color.FromArgb(49, 98, 130);
            }
            else if (_appliedVoucherCode != "" && currentText.ToUpper() == _appliedVoucherCode.ToUpper())
            {
                btnApplyVoucher.Text = "Đã áp dụng";
                btnApplyVoucher.BackColor = Color.FromArgb(90, 160, 45);
            }
        }

        private void BtnApplyVoucher_Click(object? sender, EventArgs e)
        {
            string code = txtVoucher.Text.Trim();
            if (code == "")
            {
                MessageBox.Show("Vui lòng nhập mã ưu đãi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (code.Length > 50)
            {
                MessageBox.Show("Mã ưu đãi không hợp lệ (vượt quá 50 ký tự)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để áp dụng voucher!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvCart.Rows.Count == 0 || _tamTinh <= 0)
            {
                MessageBox.Show("Giỏ hàng của bạn đang trống! Vui lòng chọn sản phẩm vào giỏ trước khi áp dụng voucher.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_maGiamGiaId.HasValue && _appliedVoucherCode.ToUpper() == code.ToUpper())
            {
                MessageBox.Show($"Mã ưu đãi '{code}' đã được áp dụng thành công cho giỏ hàng hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_voucherService.KiemTraVaApDung(code, UserSession.CurrentUserId, _tamTinh, out MaGiamGiaDTO? voucher, out decimal tienGiam, out string msg))
            {
                _maGiamGiaId = voucher?.MaGiamGiaId;
                _appliedVoucherCode = code;
                _tienGiam = tienGiam;
                btnApplyVoucher.Text = "Đã áp dụng";
                btnApplyVoucher.BackColor = Color.FromArgb(90, 160, 45);
                UpdateTotals();
                MessageBox.Show(msg, "Áp dụng Voucher thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _maGiamGiaId = null;
                _appliedVoucherCode = string.Empty;
                _tienGiam = 0;
                btnApplyVoucher.Text = "Áp dụng";
                btnApplyVoucher.BackColor = Color.FromArgb(49, 98, 130);
                UpdateTotals();
                MessageBox.Show(msg, "Lỗi Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnOpenVouchers_Click(object? sender, EventArgs e)
        {
            MainForm?.NavigateToVouchers();
        }

        public void SetInitialVoucherCode(string code)
        {
            if (txtVoucher != null && code.Trim() != "")
            {
                txtVoucher.Text = code.Trim().ToUpper();
                if (UserSession.IsLoggedIn && _tamTinh > 0)
                {
                    BtnApplyVoucher_Click(btnApplyVoucher, EventArgs.Empty);
                }
            }
        }

        private void BtnRemoveItem_Click(object? sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món hàng trong giỏ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgvCart.SelectedRows[0];
            if (row == null || row.Index < 0) return;
            object? val = (dgvCart.Columns.Contains("MaGioHang") ? row.Cells["MaGioHang"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;
            int maGioHang = Convert.ToInt32(val);
            if (_gioHangService.XoaKhoiGioHang(maGioHang)) LoadCart();
        }

        private void BtnCheckout_Click(object? sender, EventArgs e)
        {
            // Anti double-click: ngăn gửi 2 giao dịch liên tiếp
            if (_isCheckingOut) return;

            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để tiến hành thanh toán!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvCart.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng chọn sản phẩm vào giỏ trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal tongThanhToan = Math.Max(0, _tamTinh - _tienGiam);
            if (UserSession.CurrentBalance < tongThanhToan)
            {
                DialogResult dr = MessageBox.Show(
                    $"Số dư ví ({FormatHelper.FormatCurrency(UserSession.CurrentBalance)}) không đủ thanh toán {FormatHelper.FormatCurrency(tongThanhToan)}!\nBạn có muốn chuyển sang màn hình nạp thêm tiền ví?",
                    "Số dư không đủ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    MainForm?.NavigateToWallet();
                }
                return;
            }

            // Khóa nút trong suốt quá trình giao dịch
            _isCheckingOut = true;
            if (btnCheckout != null) btnCheckout.Enabled = false;
            try
            {
                // Thực hiện giao dịch thanh toán
                if (_thanhToanService.ThanhToanGioHang(UserSession.CurrentUserId, _tienGiam, _maGiamGiaId, out string orderCode, out string msg))
                {
                    MessageBox.Show($"{msg}\nMã hóa đơn: {orderCode}\nCảm ơn bạn đã mua sắm trên DPMS!", "Thanh toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCart();
                    MainForm?.UpdateUserInterfaceSession(); // Cập nhật số dư ví trên thanh header ngay lập tức
                    MainForm?.NavigateToLibrary(); // Tự động mở kho Thư viện game vừa mua

                }
                else
                {
                    MessageBox.Show(msg, "Thanh toán thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                // Luôn mở khóa nút dù giao dịch thành công hay thất bại
                _isCheckingOut = false;
                if (btnCheckout != null && !btnCheckout.IsDisposed) btnCheckout.Enabled = true;
            }
        }

        private void BtnClearCart_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn dọn sạch giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _gioHangService.LayGioHang(UserSession.CurrentUserId).ForEach(x => _gioHangService.XoaKhoiGioHang(x.MaGioHang));
                LoadCart();
            }
        }

        private void BtnDeposit_Click(object? sender, EventArgs e)
        {
            MainForm?.NavigateToWallet();
        }
    }
}
