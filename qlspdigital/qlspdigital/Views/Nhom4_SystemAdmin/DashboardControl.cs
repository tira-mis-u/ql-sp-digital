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

        public DashboardControl()
        {
            _dashboardService = new DashboardService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadDashboardData();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelTopBar == null || panelCards == null || dgvTopProducts == null || lblTableTitle == null || btnRefresh == null || lblHeader == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            int fullW = totalW - 50;
            panelTopBar.Location = new Point(25, 20);
            panelTopBar.Size = new Size(fullW, 48);
            lblHeader.Location = new Point(0, (panelTopBar.Height - lblHeader.Height) / 2);
            btnRefresh.Location = new Point(panelTopBar.Width - 110, (panelTopBar.Height - 32) / 2);
            btnRefresh.Size = new Size(110, 32);

            panelCards.Location = new Point(25, 75);
            panelCards.Size = new Size(fullW, 115);

            lblTableTitle.Location = new Point(25, 205);

            int dgvY = 240;
            int dgvH = Math.Max(160, totalH - dgvY - 25);
            dgvTopProducts.Location = new Point(25, dgvY);
            dgvTopProducts.Size = new Size(fullW, dgvH);
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
