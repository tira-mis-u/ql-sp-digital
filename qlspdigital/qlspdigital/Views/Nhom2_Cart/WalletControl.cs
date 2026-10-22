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
            LoadWalletData();
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
            List<ThanhToanDTO> list = _viService.LayLichSuGiaoDich(UserSession.CurrentUserId);
            foreach (ThanhToanDTO tx in list)
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
            Control?[] topupBtns = { btnTopUp50k, btnTopUp100k, btnTopUp200k, btnTopUp500k, btnCustomTopUp };
            foreach (Control? btn in topupBtns) if (btn != null) btn.Enabled = false;

            try
            {
                if (_viService.NapTien(UserSession.CurrentUserId, soTien, out string msg))
                {
                    MessageBox.Show(msg, "Nạp tiền thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomAmount.Clear();
                    LoadWalletData();
                    MainForm?.UpdateUserInterfaceSession();
                }
                else
                {
                    MessageBox.Show(msg, "Nạp tiền thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                _isTopUpPending = false;
                foreach (Control? btn in topupBtns) if (btn != null && !btn.IsDisposed) btn.Enabled = true;
            }
        }
    }
}
