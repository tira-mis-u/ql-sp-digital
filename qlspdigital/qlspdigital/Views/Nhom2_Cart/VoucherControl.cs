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
    public partial class VoucherControl : UserControl
    {
        private readonly MaGiamGiaService _voucherService;

        public event Action? OnNavigateToCart;
        public event Action<string>? OnNavigateToCartWithVoucher;

        private TextBox txtVoucherCode = null!;
        private Button btnCollect = null!;
        private Button btnDelete = null!;
        private Button btnCollectSelected = null!;
        private Button btnApplySelected = null!;
        private Label lblVoucherGuide = null!;
        private Button btnGoToCart = null!;
        private DataGridView dgvVouchers = null!;
        private Label lblListTitle = null!;
        private Label lblCodeTitle = null!;
        private Panel panelCollect = null!;
        private Panel panelBottomActions = null!;

        public VoucherControl()
        {
            _voucherService = new MaGiamGiaService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadVouchers();

            MaGiamGiaService.VoucherUpdated += () =>
            {
                if (!this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try { this.BeginInvoke(new Action(LoadVouchers)); } catch { }
                }
            };
            UserSession.SessionChanged += () =>
            {
                if (!this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try { this.BeginInvoke(new Action(LoadVouchers)); } catch { }
                }
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
            lblHeader.Text = "KHO MÃ ƯU ĐÃI & VOUCHER GIẢM GIÁ";
            lblHeader.Location = new Point(25, 15);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblHeader.UseMnemonic = false;

            // 1. KHUNG THU THẬP MÃ MỚI
            panelCollect = new Panel();
            panelCollect.Location = new Point(25, 55);
            panelCollect.Size = new Size(950, 85);
            panelCollect.BackColor = Color.FromArgb(27, 40, 56);
            panelCollect.Padding = new Padding(20, 15, 20, 15);

            lblCodeTitle = new Label();
            lblCodeTitle.Text = "Nhập mã khuyến mãi nhận quà (VD: WELCOME2026, SUMMERSALE):";
            lblCodeTitle.Location = new Point(20, 14);
            lblCodeTitle.AutoSize = true;
            lblCodeTitle.ForeColor = Color.FromArgb(199, 213, 224);
            lblCodeTitle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblCodeTitle.UseMnemonic = false;

            txtVoucherCode = new TextBox();
            txtVoucherCode.Location = new Point(20, 42);
            txtVoucherCode.Width = 300;
            txtVoucherCode.Font = new Font("Segoe UI", 11);
            txtVoucherCode.BackColor = Color.FromArgb(18, 25, 35);
            txtVoucherCode.ForeColor = Color.White;
            txtVoucherCode.BorderStyle = BorderStyle.FixedSingle;
            txtVoucherCode.CharacterCasing = CharacterCasing.Upper;
            txtVoucherCode.PlaceholderText = "Nhập mã code...";

            btnCollect = new Button();
            btnCollect.Text = "Thu thập mã";
            btnCollect.Location = new Point(335, 38);
            btnCollect.Size = new Size(160, 34);
            btnCollect.BackColor = Color.FromArgb(90, 160, 45);
            btnCollect.ForeColor = Color.White;
            btnCollect.FlatStyle = FlatStyle.Flat;
            btnCollect.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnCollect.Cursor = Cursors.Hand;
            btnCollect.UseMnemonic = false;
            btnCollect.FlatAppearance.BorderSize = 0;
            btnCollect.Click += BtnCollect_Click;

            btnGoToCart = new Button();
            btnGoToCart.Text = "Đến giỏ hàng để mua sắm ngay";
            btnGoToCart.Location = new Point(510, 38);
            btnGoToCart.Size = new Size(310, 34);
            btnGoToCart.BackColor = Color.FromArgb(49, 98, 130);
            btnGoToCart.ForeColor = Color.White;
            btnGoToCart.FlatStyle = FlatStyle.Flat;
            btnGoToCart.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnGoToCart.Cursor = Cursors.Hand;
            btnGoToCart.UseMnemonic = false;
            btnGoToCart.FlatAppearance.BorderSize = 0;
            btnGoToCart.Click += BtnGoToCart_Click;

            panelCollect.Controls.Add(btnGoToCart);
            panelCollect.Controls.Add(btnCollect);
            panelCollect.Controls.Add(txtVoucherCode);
            panelCollect.Controls.Add(lblCodeTitle);

            // 2. BẢNG DANH SÁCH VOUCHER
            lblListTitle = new Label();
            lblListTitle.Text = "DANH SÁCH MÃ GIẢM GIÁ KHẢ DỤNG TRÊN HỆ THỐNG";
            lblListTitle.Location = new Point(25, 155);
            lblListTitle.AutoSize = true;
            lblListTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblListTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblListTitle.UseMnemonic = false;

            dgvVouchers = new DataGridView();
            dgvVouchers.Location = new Point(25, 185);
            dgvVouchers.Size = new Size(950, 435);
            dgvVouchers.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvVouchers.ForeColor = Color.White;
            dgvVouchers.GridColor = Color.FromArgb(42, 71, 94);
            dgvVouchers.BorderStyle = BorderStyle.None;
            dgvVouchers.RowHeadersVisible = false;
            dgvVouchers.AllowUserToAddRows = false;
            dgvVouchers.ReadOnly = true;
            dgvVouchers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVouchers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVouchers.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvVouchers.DefaultCellStyle.ForeColor = Color.White;
            dgvVouchers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvVouchers.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvVouchers.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvVouchers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvVouchers.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvVouchers.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 45, 62);
            dgvVouchers.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(102, 192, 244);
            dgvVouchers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvVouchers.EnableHeadersVisualStyles = false;
            dgvVouchers.SelectionChanged += DgvVouchers_SelectionChanged;
            dgvVouchers.CellDoubleClick += DgvVouchers_CellDoubleClick;

            dgvVouchers.Columns.Add("MaGiamGiaId", "ID");
            dgvVouchers.Columns.Add("MaCode", "Mã Code");
            dgvVouchers.Columns.Add("TieuDe", "Chương trình ưu đãi");
            dgvVouchers.Columns.Add("GiaTri", "Mức giảm");
            dgvVouchers.Columns.Add("DonToiThieu", "Đơn tối thiểu");
            dgvVouchers.Columns.Add("HanDung", "Hạn sử dụng");
            dgvVouchers.Columns.Add("TrangThai", "Trạng thái kho");

            dgvVouchers.Columns["MaGiamGiaId"].Visible = false;

            dgvVouchers.Columns["MaCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVouchers.Columns["MaCode"].FillWeight = 15;
            dgvVouchers.Columns["MaCode"].MinimumWidth = 110;
            dgvVouchers.Columns["MaCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvVouchers.Columns["TieuDe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVouchers.Columns["TieuDe"].FillWeight = 33;
            dgvVouchers.Columns["TieuDe"].MinimumWidth = 160;

            dgvVouchers.Columns["GiaTri"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVouchers.Columns["GiaTri"].FillWeight = 13;
            dgvVouchers.Columns["GiaTri"].MinimumWidth = 95;
            dgvVouchers.Columns["GiaTri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvVouchers.Columns["DonToiThieu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVouchers.Columns["DonToiThieu"].FillWeight = 13;
            dgvVouchers.Columns["DonToiThieu"].MinimumWidth = 100;
            dgvVouchers.Columns["DonToiThieu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvVouchers.Columns["HanDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVouchers.Columns["HanDung"].FillWeight = 13;
            dgvVouchers.Columns["HanDung"].MinimumWidth = 95;
            dgvVouchers.Columns["HanDung"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvVouchers.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVouchers.Columns["TrangThai"].FillWeight = 13;
            dgvVouchers.Columns["TrangThai"].MinimumWidth = 95;
            dgvVouchers.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Thanh thao tác dưới bảng voucher (chống đè lưới)
            panelBottomActions = new Panel();
            panelBottomActions.Location = new Point(25, 630);
            panelBottomActions.Size = new Size(950, 50);
            panelBottomActions.BackColor = Color.FromArgb(23, 26, 33);

            // Nút thu thập mã đang chọn trên bảng
            btnCollectSelected = new Button();
            btnCollectSelected.Text = "Thu thập mã vào ví";
            btnCollectSelected.Location = new Point(0, 4);
            btnCollectSelected.Size = new Size(200, 40);
            btnCollectSelected.BackColor = Color.FromArgb(49, 98, 130);
            btnCollectSelected.ForeColor = Color.White;
            btnCollectSelected.FlatStyle = FlatStyle.Flat;
            btnCollectSelected.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnCollectSelected.Cursor = Cursors.Hand;
            btnCollectSelected.UseMnemonic = false;
            btnCollectSelected.FlatAppearance.BorderSize = 0;
            btnCollectSelected.Click += BtnCollectSelected_Click;

            // Nút dùng ngay cho mã đã trong ví
            btnApplySelected = new Button();
            btnApplySelected.Text = "Dùng ngay (Áp dụng vào giỏ)";
            btnApplySelected.Location = new Point(210, 4);
            btnApplySelected.Size = new Size(230, 40);
            btnApplySelected.BackColor = Color.FromArgb(90, 160, 45);
            btnApplySelected.ForeColor = Color.White;
            btnApplySelected.FlatStyle = FlatStyle.Flat;
            btnApplySelected.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnApplySelected.Cursor = Cursors.Hand;
            btnApplySelected.UseMnemonic = false;
            btnApplySelected.FlatAppearance.BorderSize = 0;
            btnApplySelected.Click += BtnApplySelected_Click;

            // Nút xóa voucher khỏi kho
            btnDelete = new Button();
            btnDelete.Text = "Xóa khỏi ví";
            btnDelete.Location = new Point(450, 4);
            btnDelete.Size = new Size(140, 40);
            btnDelete.BackColor = Color.FromArgb(180, 40, 40);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.UseMnemonic = false;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;

            lblVoucherGuide = new Label();
            lblVoucherGuide.Text = "Lưu ý: Chỉ mã 'Đã trong ví' mới có thể áp dụng tại Giỏ hàng. Mã 'Chưa lưu' vui lòng nhấn 'Thu thập'!";
            lblVoucherGuide.Location = new Point(600, 8);
            lblVoucherGuide.Size = new Size(350, 36);
            lblVoucherGuide.ForeColor = Color.FromArgb(199, 213, 224);
            lblVoucherGuide.Font = new Font("Segoe UI", 8.5f, FontStyle.Italic);
            lblVoucherGuide.TextAlign = ContentAlignment.MiddleLeft;
            lblVoucherGuide.UseMnemonic = false;

            panelBottomActions.Controls.Add(lblVoucherGuide);
            panelBottomActions.Controls.Add(btnDelete);
            panelBottomActions.Controls.Add(btnApplySelected);
            panelBottomActions.Controls.Add(btnCollectSelected);

            this.Controls.Add(panelBottomActions);
            this.Controls.Add(dgvVouchers);
            this.Controls.Add(lblListTitle);
            this.Controls.Add(panelCollect);
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
            if (panelCollect == null || dgvVouchers == null || panelBottomActions == null || txtVoucherCode == null || btnCollect == null || btnGoToCart == null || lblCodeTitle == null || lblListTitle == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            int fullW = totalW - 50;
            panelCollect.Location = new Point(25, 55);

            // Bố cục khung thu thập mã: nhãn ở trên, ô nhập + nút ở dưới với khoảng cách rộng rãi, không bao giờ che dấu tiếng Việt
            lblCodeTitle.Location = new Point(20, 14);
            int inputY = lblCodeTitle.Bottom + 10;
            int codeW = Math.Clamp((int)(fullW * 0.35), 280, 420);
            txtVoucherCode.Location = new Point(20, inputY);
            txtVoucherCode.Width = codeW;

            btnCollect.Location = new Point(txtVoucherCode.Right + 15, inputY);
            btnCollect.Size = new Size(160, 34);

            btnGoToCart.Location = new Point(btnCollect.Right + 15, inputY);
            btnGoToCart.Size = new Size(310, 34);

            panelCollect.Size = new Size(fullW, txtVoucherCode.Bottom + 16);

            // Bố cục bảng voucher
            lblListTitle.Location = new Point(25, panelCollect.Bottom + 16);
            int dgvY = lblListTitle.Bottom + 10;
            int bottomH = 48;
            int contentH = Math.Max(180, totalH - dgvY - bottomH - 20);

            dgvVouchers.Location = new Point(25, dgvY);
            dgvVouchers.Size = new Size(fullW, contentH);

            panelBottomActions.Location = new Point(25, dgvVouchers.Bottom + 10);
            panelBottomActions.Size = new Size(fullW, bottomH);

            if (btnCollectSelected != null && btnApplySelected != null && btnDelete != null && lblVoucherGuide != null)
            {
                btnCollectSelected.Location = new Point(0, 4);
                btnApplySelected.Location = new Point(btnCollectSelected.Right + 12, 4);
                btnDelete.Location = new Point(btnApplySelected.Right + 12, 4);
                int guideLeft = btnDelete.Right + 15;
                lblVoucherGuide.Location = new Point(guideLeft, 6);
                lblVoucherGuide.Size = new Size(Math.Max(150, fullW - guideLeft), 38);
            }
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

                int newRowIdx = dgvVouchers.Rows.Add(
                    v.MaGiamGiaId,
                    v.MaCode,
                    v.TieuDe,
                    mucGiam,
                    FormatHelper.FormatCurrency(v.DonToiThieu),
                    FormatHelper.FormatDate(v.NgayKetThuc),
                    tt
                );

                if (v.DaSuDung)
                {
                    dgvVouchers.Rows[newRowIdx].DefaultCellStyle.ForeColor = Color.FromArgb(140, 140, 140);
                }
                else if (v.DaThuThap)
                {
                    dgvVouchers.Rows[newRowIdx].DefaultCellStyle.ForeColor = Color.FromArgb(164, 208, 7);
                }
            }

            DgvVouchers_SelectionChanged(null, EventArgs.Empty);
        }

        private void BtnCollect_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để thu thập voucher!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string code = txtVoucherCode.Text.Trim();
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập mã voucher cần thu thập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (code.Length > 50)
            {
                MessageBox.Show("Mã voucher không hợp lệ (vượt quá 50 ký tự)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            var row = dgvVouchers.SelectedRows[0];
            if (row == null || row.Index < 0) return;
            var val = (dgvVouchers.Columns.Contains("MaGiamGiaId") ? row.Cells["MaGiamGiaId"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;
            int voucherId = Convert.ToInt32(val);
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

        private void BtnGoToCart_Click(object? sender, EventArgs e)
        {
            OnNavigateToCart?.Invoke();
        }

        private void DgvVouchers_SelectionChanged(object? sender, EventArgs e)
        {
            if (btnCollectSelected == null || btnApplySelected == null || btnDelete == null) return;

            if (dgvVouchers.SelectedRows.Count == 0)
            {
                btnCollectSelected.Enabled = false;
                btnApplySelected.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            var row = dgvVouchers.SelectedRows[0];
            string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? string.Empty;

            if (tt == "Chưa lưu")
            {
                btnCollectSelected.Enabled = true;
                btnCollectSelected.Text = "Thu thập mã vào ví";
                btnCollectSelected.BackColor = Color.FromArgb(49, 98, 130);

                btnApplySelected.Enabled = false;
                btnApplySelected.Text = "Dùng ngay (Cần thu thập trước)";
                btnApplySelected.BackColor = Color.FromArgb(60, 60, 60);

                btnDelete.Enabled = false;
            }
            else if (tt == "Đã trong ví")
            {
                btnCollectSelected.Enabled = false;
                btnCollectSelected.Text = "Đã có trong ví";
                btnCollectSelected.BackColor = Color.FromArgb(60, 60, 60);

                btnApplySelected.Enabled = true;
                btnApplySelected.Text = "Dùng ngay (Áp dụng vào giỏ)";
                btnApplySelected.BackColor = Color.FromArgb(90, 160, 45);

                btnDelete.Enabled = true;
            }
            else if (tt == "Đã sử dụng")
            {
                btnCollectSelected.Enabled = false;
                btnCollectSelected.Text = "Đã sử dụng";
                btnCollectSelected.BackColor = Color.FromArgb(60, 60, 60);

                btnApplySelected.Enabled = false;
                btnApplySelected.Text = "Đã sử dụng (Hết lượt)";
                btnApplySelected.BackColor = Color.FromArgb(60, 60, 60);

                btnDelete.Enabled = false;
            }
        }

        private void DgvVouchers_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvVouchers.Rows.Count) return;
            var row = dgvVouchers.Rows[e.RowIndex];
            string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? string.Empty;

            if (tt == "Chưa lưu")
            {
                BtnCollectSelected_Click(null, EventArgs.Empty);
            }
            else if (tt == "Đã trong ví")
            {
                BtnApplySelected_Click(null, EventArgs.Empty);
            }
        }

        private void BtnCollectSelected_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để thu thập voucher vào ví!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvVouchers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn voucher cần thu thập trên danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvVouchers.SelectedRows[0];
            string code = row.Cells["MaCode"]?.Value?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(code)) return;

            if (_voucherService.ThuThapVoucher(UserSession.CurrentUserId, code, out string msg))
            {
                MessageBox.Show(msg, "Thu thập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadVouchers();
            }
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnApplySelected_Click(object? sender, EventArgs e)
        {
            if (dgvVouchers.SelectedRows.Count == 0) return;
            var row = dgvVouchers.SelectedRows[0];
            string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? string.Empty;

            if (tt != "Đã trong ví")
            {
                MessageBox.Show("Chỉ có mã ưu đãi 'Đã trong ví' mới có thể áp dụng vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string code = row.Cells["MaCode"]?.Value?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(code)) return;

            OnNavigateToCartWithVoucher?.Invoke(code);
        }
    }
}
