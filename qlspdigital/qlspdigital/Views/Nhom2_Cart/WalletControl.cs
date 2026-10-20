using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom2_Cart;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom2_Cart
{
    // UI 5: Ví tài khoản cá nhân & Nạp tiền mô phỏng (Steam Wallet Simulation)
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public partial class WalletControl : UserControl
    {
        private readonly ViTaiKhoanService _viService;
        private bool _isTopUpPending = false; // Anti double-click guard

        public WalletControl()
        {
            _viService = new ViTaiKhoanService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadWalletData();

            UserSession.SessionChanged += LoadWalletData;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelCard == null || panelDeposit == null || dgvTransactions == null || lblDepositTitle == null || lblHistoryTitle == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(400, this.ClientSize.Height);
            int fullW = totalW - 50;

            panelCard.Location = new Point(25, 55);
            panelCard.Size = new Size(fullW, 100);

            panelDeposit.Location = new Point(25, 168);

            lblDepositTitle.Location = new Point(25, 18);
            int btnY = lblDepositTitle.Bottom + 14;
            int btnH = 40;
            btnTopUp50k.Height = btnH;
            btnTopUp100k.Height = btnH;
            btnTopUp200k.Height = btnH;
            btnTopUp500k.Height = btnH;
            txtCustomAmount.Height = btnH;
            btnCustomTopUp.Height = btnH;

            int pnlW = panelDeposit.ClientSize.Width;
            if (btnCustomTopUp != null && txtCustomAmount != null && btnTopUp50k != null)
            {
                int gap = 10;
                int customBtnW = 125;
                // Ô nhập số tiền rộng rãi, thoải mái nhập liệu (260px - 360px)
                int customTxtW = Math.Clamp((int)(pnlW * 0.28), 260, 360);
                int fixedPresetSpace = pnlW - 50 - customTxtW - customBtnW - gap * 5;
                int btnW = Math.Clamp(fixedPresetSpace / 4, 85, 125);

                btnTopUp50k.Location = new Point(25, btnY);
                btnTopUp50k.Width = btnW;

                btnTopUp100k.Location = new Point(btnTopUp50k.Right + gap, btnY);
                btnTopUp100k.Width = btnW;

                btnTopUp200k.Location = new Point(btnTopUp100k.Right + gap, btnY);
                btnTopUp200k.Width = btnW;

                btnTopUp500k.Location = new Point(btnTopUp200k.Right + gap, btnY);
                btnTopUp500k.Width = btnW;

                txtCustomAmount.Location = new Point(btnTopUp500k.Right + gap + 6, btnY);
                txtCustomAmount.Width = customTxtW;

                btnCustomTopUp.Location = new Point(txtCustomAmount.Right + gap, btnY);
                btnCustomTopUp.Width = customBtnW;
            }

            panelDeposit.Size = new Size(fullW, txtCustomAmount.Bottom + 16);

            lblHistoryTitle.Location = new Point(25, panelDeposit.Bottom + 16);
            int dgvY = lblHistoryTitle.Bottom + 10;
            int contentH = Math.Max(150, totalH - dgvY - 25);

            dgvTransactions.Location = new Point(25, dgvY);
            dgvTransactions.Size = new Size(fullW, contentH);
        }

        private void BtnTopUp50k_Click(object? sender, EventArgs e) => NapTien(50000);
        private void BtnTopUp100k_Click(object? sender, EventArgs e) => NapTien(100000);
        private void BtnTopUp200k_Click(object? sender, EventArgs e) => NapTien(200000);
        private void BtnTopUp500k_Click(object? sender, EventArgs e) => NapTien(500000);

        private void BtnCustomTopUp_Click(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(txtCustomAmount.Text.Trim(), out decimal val) || val <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ (> 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (val < 10000)
            {
                MessageBox.Show("Số tiền nạp tối thiểu là 10.000 ₫!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (val > 50000000)
            {
                MessageBox.Show("Số tiền nạp tối đa mỗi lần là 50.000.000 ₫!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NapTien(val);
        }

        public void LoadWalletData()
        {
            if (!UserSession.IsLoggedIn)
            {
                lblBalance.Text = "Chưa đăng nhập";
                dgvTransactions.Rows.Clear();
                return;
            }

            decimal balance = _viService.LaySoDuVi(UserSession.CurrentUserId);
            UserSession.CurrentBalance = balance;
            lblBalance.Text = FormatHelper.FormatCurrency(balance);

            dgvTransactions.Rows.Clear();
            var list = _viService.LayLichSuGiaoDich(UserSession.CurrentUserId);
            foreach (var tx in list)
            {
                string tienStr = tx.LoaiGiaoDich.Contains("Nạp")
                    ? $"+{FormatHelper.FormatCurrency(tx.SoTien)}"
                    : $"-{FormatHelper.FormatCurrency(tx.SoTien)}";

                dgvTransactions.Rows.Add(tx.MaThanhToan, tx.LoaiGiaoDich, tienStr, tx.PhuongThuc, tx.TrangThai, FormatHelper.FormatDateTime(tx.NgayTao));
            }
        }

        private void NapTien(decimal soTien)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để nạp tiền vào ví!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Anti double-click: chặn nạp tiền 2 lần cùng lúc
            if (_isTopUpPending) return;
            _isTopUpPending = true;

            // Vô hiệu hóa tất cả nút nạp trong suốt giao dịch
            Control[] topupBtns = { btnTopUp50k, btnTopUp100k, btnTopUp200k, btnTopUp500k, btnCustomTopUp };
            foreach (var btn in topupBtns) if (btn != null) btn.Enabled = false;

            try
            {
                if (_viService.NapTien(UserSession.CurrentUserId, soTien, out string msg))
                {
                    MessageBox.Show(msg, "Nạp tiền thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomAmount.Clear();
                    LoadWalletData();
                }
                else
                {
                    MessageBox.Show(msg, "Nạp tiền thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                _isTopUpPending = false;
                foreach (var btn in topupBtns) if (btn != null && !btn.IsDisposed) btn.Enabled = true;
            }
        }
    }
}
