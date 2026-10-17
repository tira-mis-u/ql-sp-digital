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

        private Label lblBalance = null!;
        private TextBox txtCustomAmount = null!;
        private Button btnTopUp50k = null!;
        private Button btnTopUp100k = null!;
        private Button btnTopUp200k = null!;
        private Button btnTopUp500k = null!;
        private Button btnCustomTopUp = null!;
        private DataGridView dgvTransactions = null!;

        public WalletControl()
        {
            _viService = new ViTaiKhoanService();

            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadWalletData();

            UserSession.SessionChanged += LoadWalletData;
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
            lblHeader.Text = "💰 VÍ TÀI KHOẢN DPMS (STEAM WALLET SIMULATION)";
            lblHeader.Location = new Point(25, 15);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblHeader.UseMnemonic = false;

            // 1. KHUNG THẺ SỐ DƯ VÍ
            Panel panelCard = new Panel();
            panelCard.Location = new Point(25, 55);
            panelCard.Size = new Size(950, 100);
            panelCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelCard.BackColor = Color.FromArgb(27, 40, 56);
            panelCard.Padding = new Padding(20);

            Label lblCardTitle = new Label();
            lblCardTitle.Text = "SỐ DƯ KHẢ DỤNG HIỆN TẠI:";
            lblCardTitle.Location = new Point(20, 15);
            lblCardTitle.AutoSize = true;
            lblCardTitle.ForeColor = Color.FromArgb(161, 181, 196);
            lblCardTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblCardTitle.UseMnemonic = false;

            lblBalance = new Label();
            lblBalance.Text = "0 ₫";
            lblBalance.Location = new Point(20, 40);
            lblBalance.AutoSize = true;
            lblBalance.ForeColor = Color.FromArgb(102, 192, 244);
            lblBalance.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblBalance.UseMnemonic = false;

            Label lblNote = new Label();
            lblNote.Text = "⚡ Số dư ví dùng để thanh toán 1-Click tức thì mọi trò chơi và tài nguyên số trên hệ thống.";
            lblNote.Dock = DockStyle.Right;
            lblNote.Width = 400;
            lblNote.ForeColor = Color.FromArgb(199, 213, 224);
            lblNote.Font = new Font("Segoe UI", 9.5f, FontStyle.Italic);
            lblNote.TextAlign = ContentAlignment.MiddleRight;
            lblNote.UseMnemonic = false;

            panelCard.Controls.Add(lblNote);
            panelCard.Controls.Add(lblBalance);
            panelCard.Controls.Add(lblCardTitle);

            // 2. KHUNG NẠP TIỀN NHANH THEO MỆNH GIÁ CỐ ĐỊNH
            Panel panelDeposit = new Panel();
            panelDeposit.Location = new Point(25, 170);
            panelDeposit.Size = new Size(950, 110);
            panelDeposit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelDeposit.BackColor = Color.FromArgb(27, 40, 56);
            panelDeposit.Padding = new Padding(20);

            Label lblDepositTitle = new Label();
            lblDepositTitle.Text = "💳 NẠP TIỀN VÀO VÍ (CHỌN MỆNH GIÁ MÔ PHỎNG HOẶC NHẬP TÙY Ý)";
            lblDepositTitle.Dock = DockStyle.Top;
            lblDepositTitle.Height = 25;
            lblDepositTitle.ForeColor = Color.FromArgb(164, 208, 7);
            lblDepositTitle.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblDepositTitle.UseMnemonic = false;

            btnTopUp50k = new Button();
            btnTopUp50k.Text = "+ 50.000 ₫";
            btnTopUp50k.Location = new Point(20, 48);
            btnTopUp50k.Size = new Size(125, 36);
            btnTopUp50k.BackColor = Color.FromArgb(92, 126, 16);
            btnTopUp50k.ForeColor = Color.White;
            btnTopUp50k.FlatStyle = FlatStyle.Flat;
            btnTopUp50k.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnTopUp50k.Cursor = Cursors.Hand;
            btnTopUp50k.FlatAppearance.BorderSize = 0;
            btnTopUp50k.Click += BtnTopUp50k_Click;

            btnTopUp100k = new Button();
            btnTopUp100k.Text = "+ 100.000 ₫";
            btnTopUp100k.Location = new Point(155, 48);
            btnTopUp100k.Size = new Size(125, 36);
            btnTopUp100k.BackColor = Color.FromArgb(92, 126, 16);
            btnTopUp100k.ForeColor = Color.White;
            btnTopUp100k.FlatStyle = FlatStyle.Flat;
            btnTopUp100k.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnTopUp100k.Cursor = Cursors.Hand;
            btnTopUp100k.FlatAppearance.BorderSize = 0;
            btnTopUp100k.Click += BtnTopUp100k_Click;

            btnTopUp200k = new Button();
            btnTopUp200k.Text = "+ 200.000 ₫";
            btnTopUp200k.Location = new Point(290, 48);
            btnTopUp200k.Size = new Size(125, 36);
            btnTopUp200k.BackColor = Color.FromArgb(92, 126, 16);
            btnTopUp200k.ForeColor = Color.White;
            btnTopUp200k.FlatStyle = FlatStyle.Flat;
            btnTopUp200k.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnTopUp200k.Cursor = Cursors.Hand;
            btnTopUp200k.FlatAppearance.BorderSize = 0;
            btnTopUp200k.Click += BtnTopUp200k_Click;

            btnTopUp500k = new Button();
            btnTopUp500k.Text = "+ 500.000 ₫";
            btnTopUp500k.Location = new Point(425, 48);
            btnTopUp500k.Size = new Size(125, 36);
            btnTopUp500k.BackColor = Color.FromArgb(92, 126, 16);
            btnTopUp500k.ForeColor = Color.White;
            btnTopUp500k.FlatStyle = FlatStyle.Flat;
            btnTopUp500k.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnTopUp500k.Cursor = Cursors.Hand;
            btnTopUp500k.FlatAppearance.BorderSize = 0;
            btnTopUp500k.Click += BtnTopUp500k_Click;

            // Nhập số tiền tùy ý
            txtCustomAmount = new TextBox();
            txtCustomAmount.Location = new Point(560, 48);
            txtCustomAmount.Size = new Size(165, 36);
            txtCustomAmount.Font = new Font("Segoe UI", 11);
            txtCustomAmount.BackColor = Color.FromArgb(18, 25, 35);
            txtCustomAmount.ForeColor = Color.White;
            txtCustomAmount.PlaceholderText = "Nhập số tiền...";

            btnCustomTopUp = new Button();
            btnCustomTopUp.Text = "Nạp số này";
            btnCustomTopUp.Location = new Point(735, 48);
            btnCustomTopUp.Size = new Size(135, 36);
            btnCustomTopUp.BackColor = Color.FromArgb(49, 98, 130);
            btnCustomTopUp.ForeColor = Color.White;
            btnCustomTopUp.FlatStyle = FlatStyle.Flat;
            btnCustomTopUp.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnCustomTopUp.Cursor = Cursors.Hand;
            btnCustomTopUp.UseMnemonic = false;
            btnCustomTopUp.FlatAppearance.BorderSize = 0;
            btnCustomTopUp.Click += BtnCustomTopUp_Click;

            panelDeposit.Controls.Add(btnCustomTopUp);
            panelDeposit.Controls.Add(txtCustomAmount);
            panelDeposit.Controls.Add(btnTopUp500k);
            panelDeposit.Controls.Add(btnTopUp200k);
            panelDeposit.Controls.Add(btnTopUp100k);
            panelDeposit.Controls.Add(btnTopUp50k);
            panelDeposit.Controls.Add(lblDepositTitle);

            // 3. BẢNG LỊCH SỬ GIAO DỊCH BIẾN ĐỘNG SỐ DƯ
            Label lblHistoryTitle = new Label();
            lblHistoryTitle.Text = "📜 LỊCH SỬ BIẾN ĐỘNG SỐ DƯ & GIAO DỊCH";
            lblHistoryTitle.Location = new Point(25, 295);
            lblHistoryTitle.AutoSize = true;
            lblHistoryTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblHistoryTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblHistoryTitle.UseMnemonic = false;

            dgvTransactions = new DataGridView();
            dgvTransactions.Location = new Point(25, 325);
            dgvTransactions.Size = new Size(950, 400);
            dgvTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTransactions.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvTransactions.ForeColor = Color.White;
            dgvTransactions.GridColor = Color.FromArgb(42, 71, 94);
            dgvTransactions.BorderStyle = BorderStyle.None;
            dgvTransactions.RowHeadersVisible = false;
            dgvTransactions.AllowUserToAddRows = false;
            dgvTransactions.ReadOnly = true;
            dgvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransactions.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvTransactions.DefaultCellStyle.ForeColor = Color.White;
            dgvTransactions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvTransactions.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTransactions.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvTransactions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvTransactions.EnableHeadersVisualStyles = false;

            dgvTransactions.Columns.Add("MaThanhToan", "Mã GD");
            dgvTransactions.Columns.Add("LoaiGiaoDich", "Loại giao dịch");
            dgvTransactions.Columns.Add("SoTien", "Số tiền");
            dgvTransactions.Columns.Add("PhuongThuc", "Phương thức");
            dgvTransactions.Columns.Add("TrangThai", "Trạng thái");
            dgvTransactions.Columns.Add("NgayTao", "Thời gian");

            dgvTransactions.Columns["MaThanhToan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTransactions.Columns["MaThanhToan"].Width = 75;
            dgvTransactions.Columns["MaThanhToan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTransactions.Columns["LoaiGiaoDich"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTransactions.Columns["LoaiGiaoDich"].FillWeight = 45;

            dgvTransactions.Columns["SoTien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTransactions.Columns["SoTien"].Width = 135;
            dgvTransactions.Columns["SoTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvTransactions.Columns["PhuongThuc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTransactions.Columns["PhuongThuc"].FillWeight = 55;

            dgvTransactions.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTransactions.Columns["TrangThai"].Width = 110;
            dgvTransactions.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTransactions.Columns["NgayTao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTransactions.Columns["NgayTao"].Width = 150;
            dgvTransactions.Columns["NgayTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Controls.Add(dgvTransactions);
            this.Controls.Add(lblHistoryTitle);
            this.Controls.Add(panelDeposit);
            this.Controls.Add(panelCard);
            this.Controls.Add(lblHeader);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnTopUp50k_Click(object? sender, EventArgs e) => NapTien(50000);
        private void BtnTopUp100k_Click(object? sender, EventArgs e) => NapTien(100000);
        private void BtnTopUp200k_Click(object? sender, EventArgs e) => NapTien(200000);
        private void BtnTopUp500k_Click(object? sender, EventArgs e) => NapTien(500000);

        private void BtnCustomTopUp_Click(object? sender, EventArgs e)
        {
            if (decimal.TryParse(txtCustomAmount.Text.Trim(), out decimal val) && val > 0)
            {
                NapTien(val);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ (> 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
    }
}
