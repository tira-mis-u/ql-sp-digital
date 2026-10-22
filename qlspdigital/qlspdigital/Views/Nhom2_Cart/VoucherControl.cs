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

        public VoucherControl()
        {
            _voucherService = new MaGiamGiaService();

            InitializeComponent();
            LoadVouchers();
        }

        public void LoadVouchers()
        {
            dgvVouchers.Rows.Clear();
            int userId = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 0;
            List<MaGiamGiaDTO> list = _voucherService.LayDanhSachVoucher(userId);

            foreach (MaGiamGiaDTO v in list)
            {
                string mucGiam = v.LoaiGiamGia == "PhanTram"
                    ? $"Giảm {v.GiaTriGiam:0.#}%"
                    : $"Giảm {FormatHelper.FormatCurrency(v.GiaTriGiam)}";

                string tt = "Chưa lưu";
                if (v.DaSuDung) tt = "Đã sử dụng";
                else if (v.DaThuThap) tt = "Đã trong ví";

                int newRowIdx = dgvVouchers.Rows.Add(v.MaGiamGiaId, v.MaCode, v.TieuDe, mucGiam, FormatHelper.FormatCurrency(v.DonToiThieu), FormatHelper.FormatDate(v.NgayKetThuc), tt);


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
            if (code == "")
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

            DataGridViewRow row = dgvVouchers.SelectedRows[0];
            if (row == null || row.Index < 0) return;
            object? val = (dgvVouchers.Columns.Contains("MaGiamGiaId") ? row.Cells["MaGiamGiaId"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
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
            MainForm?.NavigateToCart();
        }

        private bool _canCollectSelected = false;
        private bool _canApplySelected = false;
        private bool _canDeleteSelected = false;

        private void SetBtnState(Button btn, bool enabled, Color activeColor, string text)
        {
            if (btn == null) return;
            btn.Text = text;
            if (enabled)
            {
                btn.BackColor = ThemeHelper.GetButtonColor(activeColor);
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
            }
            else
            {
                btn.BackColor = ThemeHelper.GetDisabledButtonColor();
                btn.ForeColor = ThemeHelper.GetDisabledButtonTextColor();
                btn.Cursor = Cursors.No;
            }
        }

        private void DgvVouchers_SelectionChanged(object? sender, EventArgs e)
        {
            if (btnCollectSelected == null || btnApplySelected == null || btnDelete == null) return;

            if (dgvVouchers.SelectedRows.Count == 0)
            {
                _canCollectSelected = false;
                _canApplySelected = false;
                _canDeleteSelected = false;
                SetBtnState(btnCollectSelected, false, Color.FromArgb(49, 98, 130), "Thu thập mã vào ví");
                SetBtnState(btnApplySelected, false, Color.FromArgb(90, 160, 45), "Dùng ngay (Cần thu thập trước)");
                SetBtnState(btnDelete, false, Color.FromArgb(180, 40, 40), "Xóa khỏi ví");
                return;
            }

            DataGridViewRow row = dgvVouchers.SelectedRows[0];
            string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? string.Empty;

            if (tt == "Chưa lưu")
            {
                _canCollectSelected = true;
                _canApplySelected = false;
                _canDeleteSelected = false;
                SetBtnState(btnCollectSelected, true, Color.FromArgb(49, 98, 130), "Thu thập mã vào ví");
                SetBtnState(btnApplySelected, false, Color.FromArgb(90, 160, 45), "Dùng ngay (Cần thu thập trước)");
                SetBtnState(btnDelete, false, Color.FromArgb(180, 40, 40), "Xóa khỏi ví");
            }
            else if (tt == "Đã trong ví")
            {
                _canCollectSelected = false;
                _canApplySelected = true;
                _canDeleteSelected = true;
                SetBtnState(btnCollectSelected, false, Color.FromArgb(49, 98, 130), "Đã có trong ví");
                SetBtnState(btnApplySelected, true, Color.FromArgb(90, 160, 45), "Dùng ngay (Áp dụng vào giỏ)");
                SetBtnState(btnDelete, true, Color.FromArgb(180, 40, 40), "Xóa khỏi ví");
            }
            else if (tt == "Đã sử dụng")
            {
                _canCollectSelected = false;
                _canApplySelected = false;
                _canDeleteSelected = false;
                SetBtnState(btnCollectSelected, false, Color.FromArgb(49, 98, 130), "Đã sử dụng");
                SetBtnState(btnApplySelected, false, Color.FromArgb(90, 160, 45), "Đã sử dụng (Hết lượt)");
                SetBtnState(btnDelete, false, Color.FromArgb(180, 40, 40), "Xóa khỏi ví");
            }
        }

        private void DgvVouchers_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvVouchers.Rows.Count) return;
            DataGridViewRow row = dgvVouchers.Rows[e.RowIndex];
            string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? string.Empty;

            if (tt == "Chưa lưu") BtnCollectSelected_Click(null, EventArgs.Empty);
            else if (tt == "Đã trong ví") BtnApplySelected_Click(null, EventArgs.Empty);
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

            DataGridViewRow row = dgvVouchers.SelectedRows[0];
            string code = row.Cells["MaCode"]?.Value?.ToString() ?? string.Empty;
            if (code == "") return;

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
            DataGridViewRow row = dgvVouchers.SelectedRows[0];
            string tt = row.Cells["TrangThai"]?.Value?.ToString() ?? string.Empty;

            if (tt != "Đã trong ví")
            {
                MessageBox.Show("Chỉ có mã ưu đãi 'Đã trong ví' mới có thể áp dụng vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string code = row.Cells["MaCode"]?.Value?.ToString() ?? string.Empty;
            if (code == "") return;

            MainForm?.NavigateToCart(code);
        }
    }
}
