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
    public class CartControl : UserControl
    {
        private readonly GioHangService _gioHangService;
        private readonly MaGiamGiaService _voucherService;
        private readonly ThanhToanService _thanhToanService;

        public event Action? OnNavigateToLibrary;
        public event Action? OnNavigateToWallet;

        private DataGridView dgvCart = null!;
        private TextBox txtVoucher = null!;
        private Button btnApplyVoucher = null!;
        private Label lblSubtotal = null!;
        private Label lblDiscount = null!;
        private Label lblTotal = null!;
        private Label lblWalletBalance = null!;
        private Button btnCheckout = null!;
        private Button btnClearCart = null!;
        private Button btnDeposit = null!;

        private decimal _tamTinh = 0;
        private decimal _tienGiam = 0;
        private int? _maGiamGiaId = null;

        public CartControl()
        {
            _gioHangService = new GioHangService();
            _voucherService = new MaGiamGiaService();
            _thanhToanService = new ThanhToanService();

            InitializeComponent();
            LoadCart();

            // Lắng nghe biến động số dư ví
            UserSession.BalanceChanged += (bal) =>
            {
                lblWalletBalance.Text = $"Số dư ví: {FormatHelper.FormatCurrency(bal)}";
            };
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);

            // Tiêu đề
            Label lblHeader = new Label
            {
                Text = "🛒 GIỎ HÀNG SẢN PHẨM SỐ CỦA BẠN",
                Location = new Point(25, 15),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15, FontStyle.Bold)
            };

            // 1. BẢNG DANH SÁCH GIỎ HÀNG
            dgvCart = new DataGridView
            {
                Location = new Point(25, 55),
                Size = new Size(650, 480),
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
            dgvCart.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
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
            dgvCart.Columns["TenSanPham"].FillWeight = 45;
            dgvCart.Columns["DonGia"].FillWeight = 20;
            dgvCart.Columns["SoLuong"].FillWeight = 10;
            dgvCart.Columns["ThanhTien"].FillWeight = 25;

            // Nút xóa món khỏi giỏ dưới bảng
            Button btnRemoveItem = new Button
            {
                Text = "🗑️ Xóa món đã chọn",
                Location = new Point(25, 545),
                Size = new Size(160, 32),
                BackColor = Color.FromArgb(180, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRemoveItem.FlatAppearance.BorderSize = 0;
            btnRemoveItem.Click += BtnRemoveItem_Click;

            btnClearCart = new Button
            {
                Text = "Xóa toàn bộ giỏ",
                Location = new Point(195, 545),
                Size = new Size(140, 32),
                BackColor = Color.FromArgb(60, 75, 90),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.Click += (s, e) =>
            {
                if (!UserSession.IsLoggedIn) return;
                if (MessageBox.Show("Bạn có chắc chắn muốn dọn sạch giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _gioHangService.LayGioHang(UserSession.CurrentUserId).ForEach(x => _gioHangService.XoaKhoiGioHang(x.MaGioHang));
                    LoadCart();
                }
            };

            // 2. KHUNG THANH TOÁN BÊN PHẢI (ORDER SUMMARY)
            Panel panelSummary = new Panel
            {
                Location = new Point(695, 55),
                Size = new Size(330, 480),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            Label lblSumTitle = new Label
            {
                Text = "TỔNG KẾT ĐƠN HÀNG",
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 11.5f, FontStyle.Bold)
            };

            // Khung voucher
            Panel panelVoucher = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                Padding = new Padding(0, 10, 0, 0)
            };

            Label lblVoucherTitle = new Label
            {
                Text = "Mã ưu đãi (Voucher):",
                Dock = DockStyle.Top,
                Height = 22,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f)
            };

            txtVoucher = new TextBox
            {
                Location = new Point(0, 30),
                Width = 190,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                CharacterCasing = CharacterCasing.Upper
            };

            btnApplyVoucher = new Button
            {
                Text = "Áp dụng",
                Location = new Point(200, 28),
                Width = 90,
                Height = 28,
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnApplyVoucher.FlatAppearance.BorderSize = 0;
            btnApplyVoucher.Click += BtnApplyVoucher_Click;

            panelVoucher.Controls.Add(btnApplyVoucher);
            panelVoucher.Controls.Add(txtVoucher);
            panelVoucher.Controls.Add(lblVoucherTitle);

            // Các dòng số tiền
            lblSubtotal = new Label
            {
                Text = "Tạm tính: 0 ₫",
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = Color.FromArgb(199, 213, 224),
                Font = new Font("Segoe UI", 10)
            };

            lblDiscount = new Label
            {
                Text = "Giảm giá: 0 ₫",
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = Color.FromArgb(243, 156, 18),
                Font = new Font("Segoe UI", 10)
            };

            lblTotal = new Label
            {
                Text = "Tổng thanh toán: 0 ₫",
                Dock = DockStyle.Top,
                Height = 45,
                ForeColor = Color.FromArgb(164, 208, 7),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Padding = new Padding(0, 10, 0, 0)
            };

            lblWalletBalance = new Label
            {
                Text = "Số dư ví: 0 ₫",
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            btnDeposit = new Button
            {
                Text = "💰 Nạp thêm tiền ví",
                Dock = DockStyle.Top,
                Height = 32,
                BackColor = Color.FromArgb(42, 71, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnDeposit.FlatAppearance.BorderSize = 0;
            btnDeposit.Click += (s, e) => OnNavigateToWallet?.Invoke();

            btnCheckout = new Button
            {
                Text = "⚡ THANH TOÁN 1-CLICK\n(Trừ số dư Ví)",
                Dock = DockStyle.Bottom,
                Height = 55,
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
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

            this.Controls.Add(btnClearCart);
            this.Controls.Add(btnRemoveItem);
            this.Controls.Add(panelSummary);
            this.Controls.Add(dgvCart);
            this.Controls.Add(lblHeader);
        }

        public void LoadCart()
        {
            dgvCart.Rows.Clear();
            _tamTinh = 0;
            _tienGiam = 0;
            _maGiamGiaId = null;

            if (!UserSession.IsLoggedIn)
            {
                lblSubtotal.Text = "Tạm tính: 0 ₫";
                lblTotal.Text = "Tổng thanh toán: 0 ₫";
                lblWalletBalance.Text = "Vui lòng đăng nhập";
                return;
            }

            lblWalletBalance.Text = $"Số dư ví: {FormatHelper.FormatCurrency(UserSession.CurrentBalance)}";

            var items = _gioHangService.LayGioHang(UserSession.CurrentUserId);
            foreach (var item in items)
            {
                _tamTinh += item.ThanhTien;
                dgvCart.Rows.Add(item.MaGioHang, item.TenSanPham, FormatHelper.FormatCurrency(item.DonGia), item.SoLuong, FormatHelper.FormatCurrency(item.ThanhTien));
            }

            UpdateTotalLabels();
        }

        private void UpdateTotalLabels()
        {
            decimal tongThanhToan = Math.Max(0, _tamTinh - _tienGiam);
            lblSubtotal.Text = $"Tạm tính: {FormatHelper.FormatCurrency(_tamTinh)}";
            lblDiscount.Text = $"Giảm giá: -{FormatHelper.FormatCurrency(_tienGiam)}";
            lblTotal.Text = $"Tổng thanh toán: {FormatHelper.FormatCurrency(tongThanhToan)}";
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
                UpdateTotalLabels();
                MessageBox.Show(msg, "Áp dụng Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _maGiamGiaId = null;
                _tienGiam = 0;
                UpdateTotalLabels();
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
    }
}
