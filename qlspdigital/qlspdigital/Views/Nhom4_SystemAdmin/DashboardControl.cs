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
                Font = new Font("Segoe UI", 15, FontStyle.Bold)
            };

            btnRefresh = new Button
            {
                Text = "🔄 Làm mới dữ liệu",
                Dock = DockStyle.Right,
                Width = 160,
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => LoadDashboardData();

            panelTopBar.Controls.Add(btnRefresh);
            panelTopBar.Controls.Add(lblHeader);

            // 1. VÙNG 4 METRIC CARDS LỚN
            Panel panelCards = new Panel
            {
                Location = new Point(25, 65),
                Size = new Size(950, 115)
            };

            int cardWidth = 225;
            int gap = 16;

            Panel card1 = CreateMetricCard("TỔNG DOANH THU", "0 ₫", Color.FromArgb(164, 208, 7), out lblTotalRevenue, 0, cardWidth);
            Panel card2 = CreateMetricCard("ĐƠN HÀNG THÀNH CÔNG", "0 đơn", Color.FromArgb(102, 192, 244), out lblTotalOrders, (cardWidth + gap), cardWidth);
            Panel card3 = CreateMetricCard("KHÁCH HÀNG ĐĂNG KÝ", "0 khách", Color.FromArgb(243, 156, 18), out lblTotalCustomers, (cardWidth + gap) * 2, cardWidth);
            Panel card4 = CreateMetricCard("SẢN PHẨM MỞ BÁN", "0 sản phẩm", Color.FromArgb(155, 89, 182), out lblTotalProducts, (cardWidth + gap) * 3, cardWidth);

            panelCards.Controls.Add(card4);
            panelCards.Controls.Add(card3);
            panelCards.Controls.Add(card2);
            panelCards.Controls.Add(card1);

            // 2. BẢNG TOP 5 SẢN PHẨM BÁN CHẠY NHẤT
            Label lblTableTitle = new Label
            {
                Text = "🔥 TOP 5 SẢN PHẨM BÁN CHẠY NHẤT HỆ THỐNG",
                Location = new Point(25, 200),
                AutoSize = true,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            dgvTopProducts = new DataGridView
            {
                Location = new Point(25, 235),
                Size = new Size(950, 390),
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

            dgvTopProducts.Columns["XepHang"].Width = 100;
            dgvTopProducts.Columns["TenSanPham"].FillWeight = 50;
            dgvTopProducts.Columns["SoLuongDaBan"].Width = 150;
            dgvTopProducts.Columns["DoanhThu"].Width = 200;

            this.Controls.Add(dgvTopProducts);
            this.Controls.Add(lblTableTitle);
            this.Controls.Add(panelCards);
            this.Controls.Add(panelTopBar);
        }

        private Panel CreateMetricCard(string title, string initialValue, Color accentColor, out Label lblValue, int x, int width)
        {
            Panel card = new Panel
            {
                Location = new Point(x, 0),
                Size = new Size(width, 110),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(15)
            };

            Label lblTitle = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 22,
                ForeColor = Color.FromArgb(161, 181, 196),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            lblValue = new Label
            {
                Text = initialValue,
                Dock = DockStyle.Fill,
                ForeColor = accentColor,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
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
