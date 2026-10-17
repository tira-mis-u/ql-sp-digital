using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 9: Bảng chỉ số thống kê & Báo cáo tổng hợp kinh doanh (Admin Dashboard)
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public partial class DashboardControl : UserControl
    {
        private readonly DashboardService _dashboardService;

        private Label lblTotalRevenue = null!;
        private Label lblTotalOrders = null!;
        private Label lblTotalCustomers = null!;
        private Label lblTotalProducts = null!;
        private DataGridView dgvTopProducts = null!;
        private Button btnRefresh = null!;

        public DashboardControl()
        {
            _dashboardService = new DashboardService();

            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadDashboardData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(25);

            // Tiêu đề + Nút làm mới
            Panel panelTopBar = new Panel();
            panelTopBar.Dock = DockStyle.Top;
            panelTopBar.Height = 42;

            Label lblHeader = new Label();
            lblHeader.Text = "📊 BÁO CÁO THỐNG KÊ KINH DOANH (ADMIN DASHBOARD)";
            lblHeader.Dock = DockStyle.Left;
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblHeader.UseMnemonic = false;

            btnRefresh = new Button();
            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.Width = 130;
            btnRefresh.BackColor = Color.FromArgb(49, 98, 130);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.UseMnemonic = false;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            panelTopBar.Controls.Add(btnRefresh);
            panelTopBar.Controls.Add(lblHeader);

            // 1. VÙNG 4 METRIC CARDS LỚN (CO GIÃN TỶ LỆ 25% ĐỀU NHAU)
            TableLayoutPanel panelCards = new TableLayoutPanel();
            panelCards.Location = new Point(25, 90);
            panelCards.Size = new Size(950, 115);
            panelCards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelCards.ColumnCount = 4;
            panelCards.RowCount = 1;
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            panelCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            // Card 1
            Panel card1 = new Panel();
            card1.Dock = DockStyle.Fill;
            card1.Margin = new Padding(0, 0, 8, 0);
            card1.BackColor = Color.FromArgb(27, 40, 56);
            card1.Padding = new Padding(15);

            Label lblTitle1 = new Label();
            lblTitle1.Text = "TỔNG DOANH THU";
            lblTitle1.Dock = DockStyle.Top;
            lblTitle1.Height = 22;
            lblTitle1.ForeColor = Color.FromArgb(161, 181, 196);
            lblTitle1.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblTitle1.UseMnemonic = false;

            lblTotalRevenue = new Label();
            lblTotalRevenue.Text = "0 ₫";
            lblTotalRevenue.Dock = DockStyle.Fill;
            lblTotalRevenue.ForeColor = Color.FromArgb(164, 208, 7);
            lblTotalRevenue.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblTotalRevenue.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalRevenue.UseMnemonic = false;

            card1.Controls.Add(lblTotalRevenue);
            card1.Controls.Add(lblTitle1);

            // Card 2
            Panel card2 = new Panel();
            card2.Dock = DockStyle.Fill;
            card2.Margin = new Padding(4, 0, 4, 0);
            card2.BackColor = Color.FromArgb(27, 40, 56);
            card2.Padding = new Padding(15);

            Label lblTitle2 = new Label();
            lblTitle2.Text = "ĐƠN HÀNG THÀNH CÔNG";
            lblTitle2.Dock = DockStyle.Top;
            lblTitle2.Height = 22;
            lblTitle2.ForeColor = Color.FromArgb(161, 181, 196);
            lblTitle2.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblTitle2.UseMnemonic = false;

            lblTotalOrders = new Label();
            lblTotalOrders.Text = "0 đơn";
            lblTotalOrders.Dock = DockStyle.Fill;
            lblTotalOrders.ForeColor = Color.FromArgb(102, 192, 244);
            lblTotalOrders.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblTotalOrders.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalOrders.UseMnemonic = false;

            card2.Controls.Add(lblTotalOrders);
            card2.Controls.Add(lblTitle2);

            // Card 3
            Panel card3 = new Panel();
            card3.Dock = DockStyle.Fill;
            card3.Margin = new Padding(4, 0, 4, 0);
            card3.BackColor = Color.FromArgb(27, 40, 56);
            card3.Padding = new Padding(15);

            Label lblTitle3 = new Label();
            lblTitle3.Text = "KHÁCH HÀNG ĐĂNG KÝ";
            lblTitle3.Dock = DockStyle.Top;
            lblTitle3.Height = 22;
            lblTitle3.ForeColor = Color.FromArgb(161, 181, 196);
            lblTitle3.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblTitle3.UseMnemonic = false;

            lblTotalCustomers = new Label();
            lblTotalCustomers.Text = "0 khách";
            lblTotalCustomers.Dock = DockStyle.Fill;
            lblTotalCustomers.ForeColor = Color.FromArgb(243, 156, 18);
            lblTotalCustomers.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblTotalCustomers.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalCustomers.UseMnemonic = false;

            card3.Controls.Add(lblTotalCustomers);
            card3.Controls.Add(lblTitle3);

            // Card 4
            Panel card4 = new Panel();
            card4.Dock = DockStyle.Fill;
            card4.Margin = new Padding(8, 0, 0, 0);
            card4.BackColor = Color.FromArgb(27, 40, 56);
            card4.Padding = new Padding(15);

            Label lblTitle4 = new Label();
            lblTitle4.Text = "SẢN PHẨM MỞ BÁN";
            lblTitle4.Dock = DockStyle.Top;
            lblTitle4.Height = 22;
            lblTitle4.ForeColor = Color.FromArgb(161, 181, 196);
            lblTitle4.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblTitle4.UseMnemonic = false;

            lblTotalProducts = new Label();
            lblTotalProducts.Text = "0 sản phẩm";
            lblTotalProducts.Dock = DockStyle.Fill;
            lblTotalProducts.ForeColor = Color.FromArgb(155, 89, 182);
            lblTotalProducts.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblTotalProducts.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalProducts.UseMnemonic = false;

            card4.Controls.Add(lblTotalProducts);
            card4.Controls.Add(lblTitle4);

            panelCards.Controls.Add(card1, 0, 0);
            panelCards.Controls.Add(card2, 1, 0);
            panelCards.Controls.Add(card3, 2, 0);
            panelCards.Controls.Add(card4, 3, 0);

            // 2. BẢNG TOP 5 SẢN PHẨM BÁN CHẠY NHẤT
            Label lblTableTitle = new Label();
            lblTableTitle.Text = "🔥 TOP 5 SẢN PHẨM BÁN CHẠY NHẤT HỆ THỐNG";
            lblTableTitle.Location = new Point(25, 218);
            lblTableTitle.AutoSize = true;
            lblTableTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblTableTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTableTitle.UseMnemonic = false;

            dgvTopProducts = new DataGridView();
            dgvTopProducts.Location = new Point(25, 250);
            dgvTopProducts.Size = new Size(950, 420);
            dgvTopProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTopProducts.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvTopProducts.ForeColor = Color.White;
            dgvTopProducts.GridColor = Color.FromArgb(42, 71, 94);
            dgvTopProducts.BorderStyle = BorderStyle.None;
            dgvTopProducts.RowHeadersVisible = false;
            dgvTopProducts.AllowUserToAddRows = false;
            dgvTopProducts.ReadOnly = true;
            dgvTopProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopProducts.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvTopProducts.DefaultCellStyle.ForeColor = Color.White;
            dgvTopProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvTopProducts.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 45, 62);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(102, 192, 244);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTopProducts.EnableHeadersVisualStyles = false;

            dgvTopProducts.Columns.Add("XepHang", "Xếp hạng");
            dgvTopProducts.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvTopProducts.Columns.Add("SoLuongDaBan", "Số lượng đã bán");
            dgvTopProducts.Columns.Add("DoanhThu", "Doanh thu tích lũy");

            dgvTopProducts.Columns["XepHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTopProducts.Columns["XepHang"].FillWeight = 12;
            dgvTopProducts.Columns["XepHang"].MinimumWidth = 75;
            dgvTopProducts.Columns["XepHang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTopProducts.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTopProducts.Columns["TenSanPham"].FillWeight = 48;
            dgvTopProducts.Columns["TenSanPham"].MinimumWidth = 180;

            dgvTopProducts.Columns["SoLuongDaBan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTopProducts.Columns["SoLuongDaBan"].FillWeight = 20;
            dgvTopProducts.Columns["SoLuongDaBan"].MinimumWidth = 130;
            dgvTopProducts.Columns["SoLuongDaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTopProducts.Columns["DoanhThu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTopProducts.Columns["DoanhThu"].FillWeight = 20;
            dgvTopProducts.Columns["DoanhThu"].MinimumWidth = 150;
            dgvTopProducts.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.Controls.Add(dgvTopProducts);
            this.Controls.Add(lblTableTitle);
            this.Controls.Add(panelCards);
            this.Controls.Add(panelTopBar);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadDashboardData();
        }

        public void LoadDashboardData()
        {
            try
            {
                var stats = _dashboardService.LayChiSoTongHop();
                lblTotalRevenue.Text = FormatHelper.FormatCurrency(stats.TongDoanhThu);
                lblTotalOrders.Text = $"{stats.TongDonHang:N0} đơn";
                lblTotalCustomers.Text = $"{stats.TongKhachHang:N0} người";
                lblTotalProducts.Text = $"{stats.TongSanPham:N0} món";

                dgvTopProducts.Rows.Clear();
                var topList = _dashboardService.LayTop5BanChay();
                int rank = 1;
                foreach (var p in topList)
                {
                    string medal = rank switch
                    {
                        1 => "🥇 Top 1",
                        2 => "🥈 Top 2",
                        3 => "🥉 Top 3",
                        _ => $"#{rank}"
                    };

                    dgvTopProducts.Rows.Add(
                        medal,
                        p.TenSanPham,
                        $"{p.SoLuongDaBan:N0} lượt",
                        FormatHelper.FormatCurrency(p.DoanhThu)
                    );
                    rank++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải số liệu Dashboard: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
