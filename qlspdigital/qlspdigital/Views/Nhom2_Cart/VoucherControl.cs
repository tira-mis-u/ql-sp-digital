using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom2_Cart;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom2_Cart
{
    // UI 15: Kho mã ưu đãi giảm giá & Thu thập Voucher
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class VoucherControl : UserControl
    {
        private readonly MaGiamGiaService _voucherService;

        public event Action? OnNavigateToCart;

        private TextBox txtVoucherCode = null!;
        private Button btnCollect = null!;
        private Button btnDelete = null!;
        private Button btnGoToCart = null!;
        private DataGridView dgvVouchers = null!;
        private Panel panelCollect = null!;
        private Panel panelBottomActions = null!;

        public VoucherControl()
        {
            _voucherService = new MaGiamGiaService();

            InitializeComponent();
            LoadVouchers();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);

            // Tiêu đề
            Label lblHeader = new Label
            {
                Text = "🎁 KHO MÃ ƯU ĐÃI & VOUCHER GIẢM GIÁ",
                Location = new Point(25, 15),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                UseMnemonic = false
            };

            // 1. KHUNG THU THẬP MÃ MỚI
            panelCollect = new Panel
            {
                Location = new Point(25, 55),
                Size = new Size(950, 85),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20, 15, 20, 15)
            };

            Label lblCodeTitle = new Label
            {
                Text = "Nhập mã khuyến mãi nhận quà (VD: WELCOME2026, SUMMERSALE):",
                Location = new Point(20, 15),
                AutoSize = true,
                ForeColor = Color.FromArgb(199, 213, 224),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                UseMnemonic = false
            };

            txtVoucherCode = new TextBox
            {
                Location = new Point(20, 40),
                Width = 250,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                CharacterCasing = CharacterCasing.Upper,
                PlaceholderText = "Nhập mã code..."
            };

            btnCollect = new Button
            {
                Text = "🎁 Thu thập mã",
                Location = new Point(285, 40),
                Size = new Size(160, 32),
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnCollect.FlatAppearance.BorderSize = 0;
            btnCollect.Click += BtnCollect_Click;

            btnGoToCart = new Button
            {
                Text = "🛍️ Đến giỏ hàng để mua sắm ngay",
                Location = new Point(460, 40),
                Size = new Size(310, 32),
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnGoToCart.FlatAppearance.BorderSize = 0;
            btnGoToCart.Click += (s, e) => OnNavigateToCart?.Invoke();

            panelCollect.Controls.Add(btnGoToCart);
            panelCollect.Controls.Add(btnCollect);
            panelCollect.Controls.Add(txtVoucherCode);
            panelCollect.Controls.Add(lblCodeTitle);

            // 2. BẢNG DANH SÁCH VOUCHER
            Label lblListTitle = new Label
            {
                Text = "📋 DANH SÁCH MÃ GIẢM GIÁ KHẢ DỤNG TRÊN HỆ THỐNG",
                Location = new Point(25, 155),
                AutoSize = true,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                UseMnemonic = false
            };

            dgvVouchers = new DataGridView
            {
                Location = new Point(25, 185),
                Size = new Size(950, 435),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
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
            dgvVouchers.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvVouchers.DefaultCellStyle.ForeColor = Color.White;
            dgvVouchers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvVouchers.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvVouchers.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvVouchers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvVouchers.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvVouchers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvVouchers.EnableHeadersVisualStyles = false;

            dgvVouchers.Columns.Add("MaGiamGiaId", "ID");
            dgvVouchers.Columns.Add("MaCode", "Mã Code");
            dgvVouchers.Columns.Add("TieuDe", "Chương trình ưu đãi");
            dgvVouchers.Columns.Add("GiaTri", "Mức giảm");
            dgvVouchers.Columns.Add("DonToiThieu", "Đơn tối thiểu");
            dgvVouchers.Columns.Add("HanDung", "Hạn sử dụng");
            dgvVouchers.Columns.Add("TrangThai", "Trạng thái kho");

            dgvVouchers.Columns["MaGiamGiaId"].Visible = false;

            dgvVouchers.Columns["MaCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvVouchers.Columns["MaCode"].Width = 130;
            dgvVouchers.Columns["MaCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvVouchers.Columns["TieuDe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVouchers.Columns["TieuDe"].FillWeight = 100;

            dgvVouchers.Columns["GiaTri"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvVouchers.Columns["GiaTri"].Width = 110;
            dgvVouchers.Columns["GiaTri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvVouchers.Columns["DonToiThieu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvVouchers.Columns["DonToiThieu"].Width = 115;
            dgvVouchers.Columns["DonToiThieu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvVouchers.Columns["HanDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvVouchers.Columns["HanDung"].Width = 110;
            dgvVouchers.Columns["HanDung"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvVouchers.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvVouchers.Columns["TrangThai"].Width = 110;
            dgvVouchers.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Thanh thao tác dưới bảng voucher (chống đè lưới)
            panelBottomActions = new Panel
            {
                Location = new Point(25, 630),
                Size = new Size(950, 50),
                BackColor = Color.FromArgb(23, 26, 33)
            };

            // Nút xóa voucher khỏi kho
            btnDelete = new Button
            {
                Text = "🗑️ Xóa mã khỏi kho cá nhân",
                Location = new Point(0, 5),
                Size = new Size(245, 38),
                BackColor = Color.FromArgb(180, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;

            panelBottomActions.Controls.Add(btnDelete);

            this.Controls.Add(panelBottomActions);
            this.Controls.Add(dgvVouchers);
            this.Controls.Add(lblListTitle);
            this.Controls.Add(panelCollect);
            this.Controls.Add(lblHeader);

            panelBottomActions.BringToFront();
            AdjustLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (this.ClientSize.Width < 500 || panelCollect == null || dgvVouchers == null || panelBottomActions == null) return;
            int totalW = this.ClientSize.Width;
            int totalH = this.ClientSize.Height;

            int fullW = totalW - 50;
            panelCollect.Location = new Point(25, 55);
            panelCollect.Size = new Size(fullW, 85);

            int bottomH = 50;
            int contentH = Math.Max(250, totalH - 185 - bottomH - 25);

            dgvVouchers.Location = new Point(25, 185);
            dgvVouchers.Size = new Size(fullW, contentH);

            panelBottomActions.Location = new Point(25, 185 + contentH + 10);
            panelBottomActions.Size = new Size(fullW, bottomH);
        }

        public void LoadVouchers()
        {
            dgvVouchers.Rows.Clear();
            int userId = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 0;
            var list = _voucherService.LayDanhSachVoucher(userId);

            foreach (var v in list)
            {
                string mucGiam = v.LoaiGiamGia.Equals("PhanTram", StringComparison.OrdinalIgnoreCase)
                    ? $"Giảm {v.GiaTriGiam:0.#}%"
                    : $"Giảm {FormatHelper.FormatCurrency(v.GiaTriGiam)}";

                string tt = "Chưa lưu";
                if (v.DaSuDung) tt = "Đã sử dụng";
                else if (v.DaThuThap) tt = "Đã trong ví";

                dgvVouchers.Rows.Add(
                    v.MaGiamGiaId,
                    v.MaCode,
                    v.TieuDe,
                    mucGiam,
                    FormatHelper.FormatCurrency(v.DonToiThieu),
                    FormatHelper.FormatDate(v.NgayKetThuc),
                    tt
                );
            }
        }

        private void BtnCollect_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để thu thập voucher!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string code = txtVoucherCode.Text.Trim();
            if (_voucherService.ThuThapVoucher(UserSession.CurrentUserId, code, out string msg))
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVoucherCode.Clear();
                LoadVouchers();
            }
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn) return;

            if (dgvVouchers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn voucher cần xóa khỏi kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int voucherId = Convert.ToInt32(dgvVouchers.SelectedRows[0].Cells["MaGiamGiaId"].Value);
            if (_voucherService.XoaVoucherCaNhan(voucherId, UserSession.CurrentUserId))
            {
                MessageBox.Show("Đã xóa voucher khỏi kho cá nhân của bạn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadVouchers();
            }
            else
            {
                MessageBox.Show("Voucher này chưa được lưu trong kho của bạn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
