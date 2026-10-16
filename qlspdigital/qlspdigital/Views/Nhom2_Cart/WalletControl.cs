using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom2_Cart;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom2_Cart
{
    // UI 5: Ví tài khoản cá nhân & Nạp tiền mô phỏng (Steam Wallet Simulation)
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class WalletControl : UserControl
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
            LoadWalletData();

            UserSession.SessionChanged += LoadWalletData;
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
                Text = "💰 VÍ TÀI KHOẢN DPMS (STEAM WALLET SIMULATION)",
                Location = new Point(25, 15),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                UseMnemonic = false
            };

            // 1. KHUNG THẺ SỐ DƯ VÍ
            Panel panelCard = new Panel
            {
                Location = new Point(25, 55),
                Size = new Size(950, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            Label lblCardTitle = new Label
            {
                Text = "SỐ DƯ KHẢ DỤNG HIỆN TẠI:",
                Location = new Point(20, 15),
                AutoSize = true,
                ForeColor = Color.FromArgb(161, 181, 196),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                UseMnemonic = false
            };

            lblBalance = new Label
            {
                Text = "0 ₫",
                Location = new Point(20, 40),
                AutoSize = true,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                UseMnemonic = false
            };

            Label lblNote = new Label
            {
                Text = "⚡ Số dư ví dùng để thanh toán 1-Click tức thì mọi trò chơi và tài nguyên số trên hệ thống.",
                Dock = DockStyle.Right,
                Width = 400,
                ForeColor = Color.FromArgb(199, 213, 224),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Italic),
                TextAlign = ContentAlignment.MiddleRight,
                UseMnemonic = false
            };

            panelCard.Controls.Add(lblNote);
            panelCard.Controls.Add(lblBalance);
            panelCard.Controls.Add(lblCardTitle);

            // 2. KHUNG NẠP TIỀN NHANH THEO MỆNH GIÁ CỐ ĐỊNH
            Panel panelDeposit = new Panel
            {
                Location = new Point(25, 170),
                Size = new Size(950, 110),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            Label lblDepositTitle = new Label
            {
                Text = "💳 NẠP TIỀN VÀO VÍ (CHỌN MỆNH GIÁ MÔ PHỎNG HOẶC NHẬP TÙY Ý)",
                Dock = DockStyle.Top,
                Height = 25,
                ForeColor = Color.FromArgb(164, 208, 7),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                UseMnemonic = false
            };

            btnTopUp50k = CreateTopUpButton("+ 50.000 ₫", 20, 48, 50000);
            btnTopUp100k = CreateTopUpButton("+ 100.000 ₫", 155, 48, 100000);
            btnTopUp200k = CreateTopUpButton("+ 200.000 ₫", 290, 48, 200000);
            btnTopUp500k = CreateTopUpButton("+ 500.000 ₫", 425, 48, 500000);

            // Nhập số tiền tùy ý
            txtCustomAmount = new TextBox
            {
                Location = new Point(560, 48),
                Size = new Size(165, 36),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                PlaceholderText = "Nhập số tiền..."
            };

            btnCustomTopUp = new Button
            {
                Text = "Nạp số này",
                Location = new Point(735, 48),
                Size = new Size(135, 36),
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnCustomTopUp.FlatAppearance.BorderSize = 0;
            btnCustomTopUp.Click += (s, e) =>
            {
                if (decimal.TryParse(txtCustomAmount.Text.Trim(), out decimal val) && val > 0)
                {
                    NapTien(val);
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số tiền hợp lệ (> 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            panelDeposit.Controls.Add(btnCustomTopUp);
            panelDeposit.Controls.Add(txtCustomAmount);
            panelDeposit.Controls.Add(btnTopUp500k);
            panelDeposit.Controls.Add(btnTopUp200k);
            panelDeposit.Controls.Add(btnTopUp100k);
            panelDeposit.Controls.Add(btnTopUp50k);
            panelDeposit.Controls.Add(lblDepositTitle);

            // 3. BẢNG LỊCH SỬ GIAO DỊCH BIẾN ĐỘNG SỐ DƯ
            Label lblHistoryTitle = new Label
            {
                Text = "📜 LỊCH SỬ BIẾN ĐỘNG SỐ DƯ & GIAO DỊCH",
                Location = new Point(25, 295),
                AutoSize = true,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                UseMnemonic = false
            };

            dgvTransactions = new DataGridView
            {
                Location = new Point(25, 325),
                Size = new Size(950, 400),
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
        }

        private Button CreateTopUpButton(string text, int x, int y, decimal amount)
        {
            Button btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(125, 36),
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => NapTien(amount);
            return btn;
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
