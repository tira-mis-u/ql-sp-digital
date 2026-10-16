using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 9: Bảng chỉ số thống kê & Báo cáo tổng hợp kinh doanh (Admin Dashboard)
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class DashboardControl : UserControl
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
            LoadDashboardData();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);

            // Tiêu đề + Nút làm mới
            Panel panelTopBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45
            };

            Label lblHeader = new Label
            {
                Text = "📊 BÁO CÁO THỐNG KÊ KINH DOANH (ADMIN DASHBOARD)",
                Dock = DockStyle.Left,
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                UseMnemonic = false
            };

            btnRefresh = new Button
            {
                Text = "🔄 Làm mới",
                Dock = DockStyle.Right,
                Width = 130,
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => LoadDashboardData();

            panelTopBar.Controls.Add(btnRefresh);
            panelTopBar.Controls.Add(lblHeader);

            // 1. VÙNG 4 METRIC CARDS LỚN (CO GIÃN TỶ LỆ 25% ĐỀU NHAU)
            TableLayoutPanel panelCards = new TableLayoutPanel
            {
                Location = new Point(25, 65),
                Size = new Size(950, 115),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ColumnCount = 4,
                RowCount = 1
            };
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            panelCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            Panel card1 = CreateMetricCard("TỔNG DOANH THU", "0 ₫", Color.FromArgb(164, 208, 7), out lblTotalRevenue, new Padding(0, 0, 8, 0));
            Panel card2 = CreateMetricCard("ĐƠN HÀNG THÀNH CÔNG", "0 đơn", Color.FromArgb(102, 192, 244), out lblTotalOrders, new Padding(4, 0, 4, 0));
            Panel card3 = CreateMetricCard("KHÁCH HÀNG ĐĂNG KÝ", "0 khách", Color.FromArgb(243, 156, 18), out lblTotalCustomers, new Padding(4, 0, 4, 0));
            Panel card4 = CreateMetricCard("SẢN PHẨM MỞ BÁN", "0 sản phẩm", Color.FromArgb(155, 89, 182), out lblTotalProducts, new Padding(8, 0, 0, 0));

            panelCards.Controls.Add(card1, 0, 0);
            panelCards.Controls.Add(card2, 1, 0);
            panelCards.Controls.Add(card3, 2, 0);
            panelCards.Controls.Add(card4, 3, 0);

            // 2. BẢNG TOP 5 SẢN PHẨM BÁN CHẠY NHẤT
            Label lblTableTitle = new Label
            {
                Text = "🔥 TOP 5 SẢN PHẨM BÁN CHẠY NHẤT HỆ THỐNG",
                Location = new Point(25, 200),
                AutoSize = true,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                UseMnemonic = false
            };

            dgvTopProducts = new DataGridView
            {
                Location = new Point(25, 235),
                Size = new Size(950, 500),
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
            dgvTopProducts.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvTopProducts.DefaultCellStyle.ForeColor = Color.White;
            dgvTopProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvTopProducts.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTopProducts.EnableHeadersVisualStyles = false;

            dgvTopProducts.Columns.Add("XepHang", "Xếp hạng");
            dgvTopProducts.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvTopProducts.Columns.Add("SoLuongDaBan", "Số lượng đã bán");
            dgvTopProducts.Columns.Add("DoanhThu", "Doanh thu tích lũy");

            dgvTopProducts.Columns["XepHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTopProducts.Columns["XepHang"].Width = 80;
            dgvTopProducts.Columns["XepHang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTopProducts.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTopProducts.Columns["TenSanPham"].FillWeight = 100;

            dgvTopProducts.Columns["SoLuongDaBan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTopProducts.Columns["SoLuongDaBan"].Width = 135;
            dgvTopProducts.Columns["SoLuongDaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTopProducts.Columns["DoanhThu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTopProducts.Columns["DoanhThu"].Width = 160;
            dgvTopProducts.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.Controls.Add(dgvTopProducts);
            this.Controls.Add(lblTableTitle);
            this.Controls.Add(panelCards);
            this.Controls.Add(panelTopBar);
        }

        private Panel CreateMetricCard(string title, string initialValue, Color accentColor, out Label lblValue, Padding margin)
        {
            Panel card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = margin,
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(15)
            };

            Label lblTitle = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 22,
                ForeColor = Color.FromArgb(161, 181, 196),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                UseMnemonic = false
            };

            lblValue = new Label
            {
                Text = initialValue,
                Dock = DockStyle.Fill,
                ForeColor = accentColor,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };

            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            return card;
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
