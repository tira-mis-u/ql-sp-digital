using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;
using qlspdigital.DTOs;
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
            LoadDashboardData();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadDashboardData();
        }

        public void LoadDashboardData()
        {
            try
            {
                DashboardStatsDTO stats = _dashboardService.LayChiSoTongHop();
                lblTotalRevenue.Text = FormatHelper.FormatCurrency(stats.TongDoanhThu);
                lblTotalOrders.Text = $"{stats.TongDonHang:N0} đơn";
                lblTotalCustomers.Text = $"{stats.TongKhachHang:N0} người";
                lblTotalProducts.Text = $"{stats.TongSanPham:N0} món";

                dgvTopProducts.Rows.Clear();
                List<TopSanPhamDTO> topList = _dashboardService.LayTop5BanChay();
                int rank = 1;
                foreach (TopSanPhamDTO p in topList)
                {
                    string medal = rank switch
                    {
                        1 => "🥇 Top 1",
                        2 => "🥈 Top 2",
                        3 => "🥉 Top 3",
                        _ => $"#{rank}"
                    };

                    dgvTopProducts.Rows.Add(medal, p.TenSanPham, $"{p.SoLuongDaBan:N0} lượt", FormatHelper.FormatCurrency(p.DoanhThu));
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
