using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom5_Orders;
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
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadHistory();

            ThuVienService.LibraryUpdated += () =>
            {
                if (!this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try { this.BeginInvoke(new Action(LoadHistory)); } catch { }
                }
            };
            UserSession.SessionChanged += () =>
            {
                if (!this.IsDisposed && !this.Disposing && this.IsHandleCreated)
                {
                    try { this.BeginInvoke(new Action(LoadHistory)); } catch { }
                }
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (dgvOrders == null || panelDetail == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            int rightW = Math.Clamp((int)(totalW * 0.38), 300, 440);
            int leftW = Math.Max(260, totalW - rightW - 65);
            int contentH = Math.Max(180, totalH - 100 - 25);

            dgvOrders.Location = new Point(25, 100);
            dgvOrders.Size = new Size(leftW, contentH);

            panelDetail.Location = new Point(25 + leftW + 15, 100);
            panelDetail.Size = new Size(rightW, contentH);
        }

        public void LoadHistory()
        {
            dgvOrders.Rows.Clear();
            dgvDetails.Rows.Clear();

            if (!UserSession.IsLoggedIn) return;

            var list = _orderService.LayLichSuDonHang(UserSession.CurrentUserId);
            foreach (var o in list)
            {
                dgvOrders.Rows.Add(
                    o.MaDonHang,
                    o.MaDonHangCode,
                    FormatHelper.FormatCurrency(o.TongThanhToan),
                    o.TrangThaiDonHang,
                    FormatHelper.FormatDateTime(o.NgayTao)
                );
            }
        }

        private void DgvOrders_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                dgvDetails.Rows.Clear();
                return;
            }

            var row = dgvOrders.SelectedRows[0];
            if (row == null || row.Index < 0) return;

            var val = (dgvOrders.Columns.Contains("MaDonHang") ? row.Cells["MaDonHang"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;

            int orderId = Convert.ToInt32(val);
            dgvDetails.Rows.Clear();

            var details = _orderService.LayChiTietDonHang(orderId);
            foreach (var item in details)
            {
                dgvDetails.Rows.Add(item.TenSanPham, item.SoLuong, FormatHelper.FormatCurrency(item.DonGia));
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadHistory();
        }
    }
}
