using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom5_Orders;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom5_Orders
{
    // UI 7: Xem lịch sử đơn hàng cá nhân của khách hàng
    // Thành viên phụ trách: Phạm Văn Đam (Nhóm 5)
    public partial class OrderHistoryControl : UserControl
    {
        private readonly DonHangService _orderService;

        public OrderHistoryControl()
        {
            _orderService = new DonHangService();

            InitializeComponent();
            LoadHistory();
        }

        public void LoadHistory()
        {
            dgvOrders.Rows.Clear();
            dgvDetails.Rows.Clear();

            if (!UserSession.IsLoggedIn) return;

            List<DonHangDTO> list = _orderService.LayLichSuDonHang(UserSession.CurrentUserId);
            foreach (DonHangDTO o in list)
            {
                dgvOrders.Rows.Add(o.MaDonHang, o.MaDonHangCode, FormatHelper.FormatCurrency(o.TongThanhToan), o.TrangThaiDonHang, FormatHelper.FormatDateTime(o.NgayTao));
            }
        }

        private void DgvOrders_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0) { dgvDetails.Rows.Clear(); return; }

            DataGridViewRow row = dgvOrders.SelectedRows[0];
            if (row == null || row.Index < 0) return;

            object? val = row.Cells.Count > 0 ? row.Cells[0].Value : null;
            if (val == null) return;

            int orderId = Convert.ToInt32(val);
            dgvDetails.Rows.Clear();

            // Truyền CurrentUserId để chống IDOR (chỉ xem chi tiết đơn của chính mình)
            List<ChiTietDonHangDTO> details = _orderService.LayChiTietDonHang(orderId, UserSession.CurrentUserId);
            foreach (ChiTietDonHangDTO item in details)
            {
                dgvDetails.Rows.Add(item.TenSanPham, item.SoLuong, FormatHelper.FormatCurrency(item.DonGia));
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e) => LoadHistory();
    }
}
