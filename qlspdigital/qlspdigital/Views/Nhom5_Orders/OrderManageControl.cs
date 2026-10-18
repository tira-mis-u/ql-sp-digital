using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom5_Orders;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom5_Orders
{
    // UI 13: Quản lý và duyệt đơn hàng toàn hệ thống (Admin)
    // Thành viên phụ trách: Phạm Văn Đam (Nhóm 5)
    public partial class OrderManageControl : UserControl
    {
        private readonly DonHangService _orderService;

        public OrderManageControl()
        {
            _orderService = new DonHangService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadOrders();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelFilter == null || dgvOrders == null || panelDetails == null || panelBottomActions == null || lblStatus == null || cboStatus == null || lblSearch == null || txtSearch == null || btnRefresh == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            panelFilter.Location = new Point(20, 45);
            panelFilter.Size = new Size(totalW - 40, 50);

            // Bố cục tuần tự tương đối - không bao giờ đè nhãn
            lblStatus.Location = new Point(15, 15);
            cboStatus.Location = new Point(lblStatus.Right + 12, 10);
            lblSearch.Location = new Point(cboStatus.Right + 24, 15);
            txtSearch.Location = new Point(lblSearch.Right + 12, 10);

            int btnSearchW = 100;
            int btnRefreshW = 100;
            int filterW = panelFilter.ClientSize.Width;

            int searchW = Math.Clamp(filterW - txtSearch.Left - btnSearchW - btnRefreshW - 40, 180, 520);
            txtSearch.Size = new Size(searchW, 30);

            btnSearch.Location = new Point(txtSearch.Right + 10, 9);
            btnSearch.Size = new Size(btnSearchW, 32);

            btnRefresh.Location = new Point(btnSearch.Right + 10, 9);
            btnRefresh.Size = new Size(btnRefreshW, 32);

            int rightW = Math.Clamp((int)(totalW * 0.36), 300, 420);
            int leftW = Math.Max(260, totalW - rightW - 55); // margin 20 left + 15 gap + 20 right
            int bottomH = 48;
            int contentH = Math.Max(180, totalH - 105 - bottomH - 18);

            dgvOrders.Location = new Point(20, 105);
            dgvOrders.Size = new Size(leftW, contentH);

            panelDetails.Location = new Point(20 + leftW + 15, 105);
            panelDetails.Size = new Size(rightW, contentH);

            panelBottomActions.Location = new Point(20, 105 + contentH + 8);
            panelBottomActions.Size = new Size(totalW - 40, bottomH);
        }

        public void LoadOrders()
        {
            dgvOrders.Rows.Clear();
            dgvOrderDetails.Rows.Clear();

            string? status = cboStatus.SelectedItem?.ToString();
            string keyword = txtSearch.Text.Trim();

            var list = _orderService.LayTatCaDonHang(status, keyword);
            foreach (var o in list)
            {
                dgvOrders.Rows.Add(
                    o.MaDonHang,
                    o.MaDonHangCode,
                    o.TenKhachHang,
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
                dgvOrderDetails.Rows.Clear();
                return;
            }

            var row = dgvOrders.SelectedRows[0];
            if (row == null || row.Index < 0) return;

            var val = (dgvOrders.Columns.Contains("MaDonHang") ? row.Cells["MaDonHang"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;

            int orderId = Convert.ToInt32(val);
            dgvOrderDetails.Rows.Clear();

            var details = _orderService.LayChiTietDonHang(orderId);
            foreach (var item in details)
            {
                dgvOrderDetails.Rows.Add(item.TenSanPham, item.SoLuong, FormatHelper.FormatCurrency(item.DonGia));
            }
        }

        private void ChangeOrderStatus(string status)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvOrders.SelectedRows[0];
            if (row == null || row.Index < 0) return;

            var val = (dgvOrders.Columns.Contains("MaDonHang") ? row.Cells["MaDonHang"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;

            int orderId = Convert.ToInt32(val);
            string code = (dgvOrders.Columns.Contains("MaDonHangCode") ? row.Cells["MaDonHangCode"].Value : null)?.ToString()
                ?? (row.Cells.Count > 1 ? row.Cells[1].Value?.ToString() : string.Empty) ?? string.Empty;

            if (MessageBox.Show($"Bạn có chắc muốn chuyển đơn [{code}] sang '{status}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ok = status == "Hoàn thành" ? _orderService.DuyetDonHang(orderId, out string msg) : _orderService.HuyDonHang(orderId, out msg);
                if (ok)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOrders();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void CboStatus_SelectedIndexChanged(object? sender, EventArgs e) => LoadOrders();
        private void TxtSearch_TextChanged(object? sender, EventArgs e) => LoadOrders();
        private void BtnSearch_Click(object? sender, EventArgs e) => LoadOrders();
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cboStatus.SelectedIndex = 0;
            LoadOrders();
        }
        private void BtnApprove_Click(object? sender, EventArgs e) => ChangeOrderStatus("Hoàn thành");
        private void BtnCancelOrder_Click(object? sender, EventArgs e) => ChangeOrderStatus("Đã hủy");
    }
}
