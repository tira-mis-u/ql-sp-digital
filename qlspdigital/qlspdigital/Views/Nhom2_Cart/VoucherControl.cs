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

            if (dgvVouchers.SelectedRows.Count == 0 || !_canDeleteSelected)
            {
                MessageBox.Show("Vui lòng chọn voucher đã có trong ví cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private bool _canCollectSelected = false;
        private bool _canApplySelected = false;
        private bool _canDeleteSelected = false;

        private void SetBtnState(Button btn, bool enabled, Color activeColor, string text, ref bool flag)
        {
            if (btn == null) return;
            flag = enabled;
            btn.Text = text;
            if (enabled)
            {
                btn.BackColor = activeColor;
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
            }
            else
            {
                btn.BackColor = Color.FromArgb(42, 54, 66);
                btn.ForeColor = Color.FromArgb(105, 120, 135);
                btn.Cursor = Cursors.No;
            }
        }

        private void DgvVouchers_SelectionChanged(object? sender, EventArgs e)
        {
            if (btnCollectSelected == null || btnApplySelected == null || btnDelete == null) return;

            if (dgvVouchers.SelectedRows.Count == 0)
            {
                SetBtnState(btnCollectSelected, false, Color.FromArgb(49, 98, 130), "Thu thập mã vào ví", ref _canCollectSelected);
                SetBtnState(btnApplySelected, false, Color.FromArgb(90, 160, 45), "Dùng ngay (Cần thu thập trước)", ref _canApplySelected);
                SetBtnState(btnDelete, false, Color.FromArgb(180, 40, 40), "Xóa khỏi ví", ref _canDeleteSelected);
                return;
            }

            var row = dgvVouchers.SelectedRows[0];
            string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? string.Empty;

            if (tt == "Chưa lưu")
            {
                SetBtnState(btnCollectSelected, true, Color.FromArgb(49, 98, 130), "Thu thập mã vào ví", ref _canCollectSelected);
                SetBtnState(btnApplySelected, false, Color.FromArgb(90, 160, 45), "Dùng ngay (Cần thu thập trước)", ref _canApplySelected);
                SetBtnState(btnDelete, false, Color.FromArgb(180, 40, 40), "Xóa khỏi ví", ref _canDeleteSelected);
            }
            else if (tt == "Đã trong ví")
            {
                SetBtnState(btnCollectSelected, false, Color.FromArgb(49, 98, 130), "Đã có trong ví", ref _canCollectSelected);
                SetBtnState(btnApplySelected, true, Color.FromArgb(90, 160, 45), "Dùng ngay (Áp dụng vào giỏ)", ref _canApplySelected);
                SetBtnState(btnDelete, true, Color.FromArgb(180, 40, 40), "Xóa khỏi ví", ref _canDeleteSelected);
            }
            else if (tt == "Đã sử dụng")
            {
                SetBtnState(btnCollectSelected, false, Color.FromArgb(49, 98, 130), "Đã sử dụng", ref _canCollectSelected);
                SetBtnState(btnApplySelected, false, Color.FromArgb(90, 160, 45), "Đã sử dụng (Hết lượt)", ref _canApplySelected);
                SetBtnState(btnDelete, false, Color.FromArgb(180, 40, 40), "Xóa khỏi ví", ref _canDeleteSelected);
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

            if (dgvVouchers.SelectedRows.Count == 0 || !_canCollectSelected)
            {
                MessageBox.Show("Mã ưu đãi này đã có trong ví hoặc đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dgvVouchers.SelectedRows.Count == 0 || !_canApplySelected)
            {
                MessageBox.Show("Chỉ có mã ưu đãi 'Đã trong ví' mới có thể áp dụng vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
