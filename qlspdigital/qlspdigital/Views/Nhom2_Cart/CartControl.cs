using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom2_Cart;
using qlspdigital.DTOs;
using qlspdigital.Shared;

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

        public event Action? OnNavigateToLibrary;
        public event Action? OnNavigateToWallet;

        private DataGridView dgvCart = null!;
        private Panel panelSummary = null!;
        private Panel panelBottomActions = null!;
        private TextBox txtVoucher = null!;
        private Button btnApplyVoucher = null!;
        private Label lblSubtotal = null!;
        private Label lblDiscount = null!;
        private Label lblTotal = null!;
        private Label lblWalletBalance = null!;
        private Button btnDeposit = null!;
        private Button btnCheckout = null!;
        private Button btnClearCart = null!;

        private decimal _tamTinh = 0;
        private decimal _tienGiam = 0;
        private int? _maGiamGiaId = null;

        public CartControl()
        {
            _gioHangService = new GioHangService();
            _voucherService = new MaGiamGiaService();
            _viService = new ViTaiKhoanService();
            _thanhToanService = new ThanhToanService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadCart();

            // Lắng nghe biến động số dư ví
            UserSession.BalanceChanged += (bal) =>
            {
                lblWalletBalance.Text = $"Số dư ví: {FormatHelper.FormatCurrency(bal)}";
            };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(25);

            // Tiêu đề
            Label lblHeader = new Label();
            lblHeader.Text = "🛒 GIỎ HÀNG SẢN PHẨM SỐ CỦA BẠN";
            lblHeader.Location = new Point(25, 15);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblHeader.UseMnemonic = false;

            // 1. BẢNG DANH SÁCH GIỎ HÀNG
            dgvCart = new DataGridView();
            dgvCart.Location = new Point(25, 55);
            dgvCart.Size = new Size(640, 550);
            dgvCart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvCart.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvCart.ForeColor = Color.White;
            dgvCart.GridColor = Color.FromArgb(42, 71, 94);
            dgvCart.BorderStyle = BorderStyle.None;
            dgvCart.RowHeadersVisible = false;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.ReadOnly = true;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvCart.DefaultCellStyle.ForeColor = Color.White;
            dgvCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvCart.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCart.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvCart.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvCart.EnableHeadersVisualStyles = false;

            dgvCart.Columns.Add("MaGioHang", "ID");
            dgvCart.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvCart.Columns.Add("DonGia", "Đơn giá");
            dgvCart.Columns.Add("SoLuong", "SL");
            dgvCart.Columns.Add("ThanhTien", "Thành tiền");

            dgvCart.Columns["MaGioHang"].Visible = false;

            dgvCart.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCart.Columns["TenSanPham"].FillWeight = 100;

            dgvCart.Columns["DonGia"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCart.Columns["DonGia"].Width = 110;
            dgvCart.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvCart.Columns["SoLuong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCart.Columns["SoLuong"].Width = 55;
            dgvCart.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCart.Columns["ThanhTien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCart.Columns["ThanhTien"].Width = 120;
            dgvCart.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Thanh thao tác dưới bảng giỏ hàng (chống đè lưới)
            panelBottomActions = new Panel();
            panelBottomActions.Location = new Point(25, 618);
            panelBottomActions.Size = new Size(640, 50);
            panelBottomActions.BackColor = Color.FromArgb(23, 26, 33);

            // Nút xóa món khỏi giỏ dưới bảng
            Button btnRemoveItem = new Button();
            btnRemoveItem.Text = "🗑️ Xóa món đã chọn";
            btnRemoveItem.Location = new Point(0, 6);
            btnRemoveItem.Size = new Size(185, 36);
            btnRemoveItem.BackColor = Color.FromArgb(180, 40, 40);
            btnRemoveItem.ForeColor = Color.White;
            btnRemoveItem.FlatStyle = FlatStyle.Flat;
            btnRemoveItem.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnRemoveItem.Cursor = Cursors.Hand;
            btnRemoveItem.UseMnemonic = false;
            btnRemoveItem.FlatAppearance.BorderSize = 0;
            btnRemoveItem.Click += BtnRemoveItem_Click;

            btnClearCart = new Button();
            btnClearCart.Text = "Xóa toàn bộ giỏ";
            btnClearCart.Location = new Point(195, 6);
            btnClearCart.Size = new Size(140, 36);
            btnClearCart.BackColor = Color.FromArgb(60, 75, 90);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnClearCart.Cursor = Cursors.Hand;
            btnClearCart.UseMnemonic = false;
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.Click += BtnClearCart_Click;

            panelBottomActions.Controls.Add(btnClearCart);
            panelBottomActions.Controls.Add(btnRemoveItem);

            // 2. KHUNG THANH TOÁN BÊN PHẢI (ORDER SUMMARY)
            panelSummary = new Panel();
            panelSummary.Location = new Point(685, 55);
            panelSummary.Size = new Size(330, 606);
            panelSummary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelSummary.BackColor = Color.FromArgb(27, 40, 56);
            panelSummary.Padding = new Padding(20);

            Label lblSumTitle = new Label();
            lblSumTitle.Text = "TỔNG KẾT ĐƠN HÀNG";
            lblSumTitle.Dock = DockStyle.Top;
            lblSumTitle.Height = 30;
            lblSumTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblSumTitle.Font = new Font("Segoe UI", 11.5f, FontStyle.Bold);
            lblSumTitle.UseMnemonic = false;

            // Khung voucher
            Panel panelVoucher = new Panel();
            panelVoucher.Dock = DockStyle.Top;
            panelVoucher.Height = 85;
            panelVoucher.Padding = new Padding(0, 10, 0, 0);

            Label lblVoucherTitle = new Label();
            lblVoucherTitle.Text = "Mã ưu đãi (Voucher):";
            lblVoucherTitle.Dock = DockStyle.Top;
            lblVoucherTitle.Height = 22;
            lblVoucherTitle.ForeColor = Color.White;
            lblVoucherTitle.Font = new Font("Segoe UI", 9.5f);
            lblVoucherTitle.UseMnemonic = false;

            txtVoucher = new TextBox();
            txtVoucher.Location = new Point(0, 28);
            txtVoucher.Width = 185;
            txtVoucher.Font = new Font("Segoe UI", 10);
            txtVoucher.BackColor = Color.FromArgb(18, 25, 35);
            txtVoucher.ForeColor = Color.White;
            txtVoucher.CharacterCasing = CharacterCasing.Upper;
            txtVoucher.PlaceholderText = "Nhập mã...";

            btnApplyVoucher = new Button();
            btnApplyVoucher.Text = "Áp dụng";
            btnApplyVoucher.Location = new Point(195, 28);
            btnApplyVoucher.Width = 90;
            btnApplyVoucher.Height = 28;
            btnApplyVoucher.BackColor = Color.FromArgb(49, 98, 130);
            btnApplyVoucher.ForeColor = Color.White;
            btnApplyVoucher.FlatStyle = FlatStyle.Flat;
            btnApplyVoucher.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnApplyVoucher.Cursor = Cursors.Hand;
            btnApplyVoucher.UseMnemonic = false;
            btnApplyVoucher.FlatAppearance.BorderSize = 0;
            btnApplyVoucher.Click += BtnApplyVoucher_Click;

            panelVoucher.Controls.Add(btnApplyVoucher);
            panelVoucher.Controls.Add(txtVoucher);
            panelVoucher.Controls.Add(lblVoucherTitle);

            // Các dòng số tiền
            lblSubtotal = new Label();
            lblSubtotal.Text = "Tạm tính: 0 ₫";
            lblSubtotal.Dock = DockStyle.Top;
            lblSubtotal.Height = 30;
            lblSubtotal.ForeColor = Color.FromArgb(199, 213, 224);
            lblSubtotal.Font = new Font("Segoe UI", 10);
            lblSubtotal.UseMnemonic = false;

            lblDiscount = new Label();
            lblDiscount.Text = "Giảm giá: 0 ₫";
            lblDiscount.Dock = DockStyle.Top;
            lblDiscount.Height = 30;
            lblDiscount.ForeColor = Color.FromArgb(243, 156, 18);
            lblDiscount.Font = new Font("Segoe UI", 10);
            lblDiscount.UseMnemonic = false;

            lblTotal = new Label();
            lblTotal.Text = "Tổng thanh toán: 0 ₫";
            lblTotal.Dock = DockStyle.Top;
            lblTotal.Height = 45;
            lblTotal.ForeColor = Color.FromArgb(164, 208, 7);
            lblTotal.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTotal.Padding = new Padding(0, 10, 0, 0);
            lblTotal.UseMnemonic = false;

            lblWalletBalance = new Label();
            lblWalletBalance.Text = "Số dư ví: 0 ₫";
            lblWalletBalance.Dock = DockStyle.Top;
            lblWalletBalance.Height = 30;
            lblWalletBalance.ForeColor = Color.FromArgb(102, 192, 244);
            lblWalletBalance.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblWalletBalance.UseMnemonic = false;

            btnDeposit = new Button();
            btnDeposit.Text = "💰 Nạp thêm tiền ví";
            btnDeposit.Dock = DockStyle.Top;
            btnDeposit.Height = 34;
            btnDeposit.BackColor = Color.FromArgb(42, 71, 94);
            btnDeposit.ForeColor = Color.White;
            btnDeposit.FlatStyle = FlatStyle.Flat;
            btnDeposit.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnDeposit.Cursor = Cursors.Hand;
            btnDeposit.UseMnemonic = false;
            btnDeposit.FlatAppearance.BorderSize = 0;
            btnDeposit.Click += BtnDeposit_Click;

            btnCheckout = new Button();
            btnCheckout.Text = "⚡ THANH TOÁN 1-CLICK\n(Trừ số dư Ví)";
            btnCheckout.Dock = DockStyle.Bottom;
            btnCheckout.Height = 55;
            btnCheckout.BackColor = Color.FromArgb(92, 126, 16);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnCheckout.Cursor = Cursors.Hand;
            btnCheckout.UseMnemonic = false;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.Click += BtnCheckout_Click;

            panelSummary.Controls.Add(btnCheckout);
            panelSummary.Controls.Add(btnDeposit);
            panelSummary.Controls.Add(lblWalletBalance);
            panelSummary.Controls.Add(lblTotal);
            panelSummary.Controls.Add(lblDiscount);
            panelSummary.Controls.Add(lblSubtotal);
            panelSummary.Controls.Add(panelVoucher);
            panelSummary.Controls.Add(lblSumTitle);

            this.Controls.Add(panelBottomActions);
            this.Controls.Add(panelSummary);
            this.Controls.Add(dgvCart);
            this.Controls.Add(lblHeader);

            panelBottomActions.BringToFront();

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
            if (dgvCart == null || panelBottomActions == null || panelSummary == null) return;
            int totalW = Math.Max(1040, this.ClientSize.Width);
            int totalH = Math.Max(700, this.ClientSize.Height);

            int summaryW = 340;
            int leftW = totalW - summaryW - 65; // margin 25 left + 15 gap + 25 right
            int contentH = Math.Max(300, totalH - 125);

            // Left grid
            dgvCart.Location = new Point(25, 55);
            dgvCart.Size = new Size(leftW, contentH);

            // Left bottom actions
            panelBottomActions.Location = new Point(25, 55 + contentH + 12);
            panelBottomActions.Size = new Size(leftW, 48);

            // Right Summary panel
            panelSummary.Location = new Point(25 + leftW + 15, 55);
            panelSummary.Size = new Size(summaryW, contentH + 60);
        }

        public void LoadCart()
        {
            dgvCart.Rows.Clear();
            _tamTinh = 0;
            _tienGiam = 0;
            _maGiamGiaId = null;
            txtVoucher.Clear();

            if (!UserSession.IsLoggedIn)
            {
                lblSubtotal.Text = "Tạm tính: 0 ₫ (Chưa đăng nhập)";
                lblTotal.Text = "Tổng: 0 ₫";
                lblWalletBalance.Text = "Số dư ví: 0 ₫";
                return;
            }

            var items = _gioHangService.LayGioHang(UserSession.CurrentUserId);
            foreach (var item in items)
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
            lblDiscount.Text = $"Giảm giá: -{FormatHelper.FormatCurrency(_tienGiam)}";
            decimal phaiTra = Math.Max(0, _tamTinh - _tienGiam);
            lblTotal.Text = $"Tổng thanh toán: {FormatHelper.FormatCurrency(phaiTra)}";
        }

        private void BtnApplyVoucher_Click(object? sender, EventArgs e)
        {
            string code = txtVoucher.Text.Trim();
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập mã ưu đãi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để áp dụng voucher!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_voucherService.KiemTraVaApDung(code, UserSession.CurrentUserId, _tamTinh, out MaGiamGiaDTO? voucher, out decimal tienGiam, out string msg))
            {
                _maGiamGiaId = voucher?.MaGiamGiaId;
                _tienGiam = tienGiam;
                UpdateTotals();
                MessageBox.Show(msg, "Áp dụng Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _maGiamGiaId = null;
                _tienGiam = 0;
                UpdateTotals();
                MessageBox.Show(msg, "Lỗi Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRemoveItem_Click(object? sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món hàng trong giỏ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int maGioHang = Convert.ToInt32(dgvCart.SelectedRows[0].Cells["MaGioHang"].Value);
            if (_gioHangService.XoaKhoiGioHang(maGioHang))
            {
                LoadCart();
            }
        }

        private void BtnCheckout_Click(object? sender, EventArgs e)
        {
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
                    OnNavigateToWallet?.Invoke();
                }
                return;
            }

            // Thực hiện giao dịch thanh toán
            if (_thanhToanService.ThanhToanGioHang(UserSession.CurrentUserId, _tienGiam, _maGiamGiaId, out string orderCode, out string msg))
            {
                MessageBox.Show($"{msg}\nMã hóa đơn: {orderCode}\nCảm ơn bạn đã mua sắm trên DPMS!", "Thanh toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCart();
                OnNavigateToLibrary?.Invoke(); // Tự động mở kho Thư viện game vừa mua
            }
            else
            {
                MessageBox.Show(msg, "Thanh toán thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            OnNavigateToWallet?.Invoke();
        }
    }
}
