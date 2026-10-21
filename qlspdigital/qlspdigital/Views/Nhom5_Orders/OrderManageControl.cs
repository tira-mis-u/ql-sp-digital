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
            LoadOrders();
        }

        public void LoadOrders()
        {
            dgvOrders.Rows.Clear();
            dgvOrderDetails.Rows.Clear();

            string? status = cboStatus.SelectedItem?.ToString();
            string keyword = txtSearch.Text.Trim();

            List<DonHangDTO> list = _orderService.LayTatCaDonHang(status, keyword);
            foreach (DonHangDTO o in list)
            {
                dgvOrders.Rows.Add(o.MaDonHang, o.MaDonHangCode, o.TenKhachHang, FormatHelper.FormatCurrency(o.TongThanhToan), o.TrangThaiDonHang, FormatHelper.FormatDateTime(o.NgayTao));
            }

            if (dgvOrders.Rows.Count == 0)
            {
                btnApprove.Enabled = false;
                btnCancelOrder.Enabled = false;
            }
            else
            {
                DgvOrders_SelectionChanged(null, EventArgs.Empty);
            }
        }

        private bool _canApprove = false;
        private bool _canCancel = false;
        private readonly ToolTip _actionToolTip = new ToolTip();

        private void SetButtonVisualState(Button btn, bool canAction, Color activeColor, string tooltipText, string disabledReason, ref bool actionFlag)
        {
            actionFlag = canAction;
            if (canAction)
            {
                btn.BackColor = activeColor;
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
                _actionToolTip.SetToolTip(btn, tooltipText);
            }
            else
            {
                // Giảm opacity: Màu nền xám trầm mờ (42, 54, 66) và chữ xám nhạt (105, 120, 135)
                btn.BackColor = Color.FromArgb(42, 54, 66);
                btn.ForeColor = Color.FromArgb(105, 120, 135);
                btn.Cursor = Cursors.No; // Con trỏ disabled 🚫
                _actionToolTip.SetToolTip(btn, disabledReason);
            }
        }

        private void DgvOrders_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                dgvOrderDetails.Rows.Clear();
                SetButtonVisualState(btnApprove, false, Color.FromArgb(90, 160, 45), "", "Vui lòng chọn đơn hàng cần xử lý", ref _canApprove);
                SetButtonVisualState(btnCancelOrder, false, Color.FromArgb(180, 40, 40), "", "Vui lòng chọn đơn hàng cần xử lý", ref _canCancel);
                return;
            }

            DataGridViewRow row = dgvOrders.SelectedRows[0];
            if (row == null || row.Index < 0)
            {
                SetButtonVisualState(btnApprove, false, Color.FromArgb(90, 160, 45), "", "Vui lòng chọn đơn hàng cần xử lý", ref _canApprove);
                SetButtonVisualState(btnCancelOrder, false, Color.FromArgb(180, 40, 40), "", "Vui lòng chọn đơn hàng cần xử lý", ref _canCancel);
                return;
            }

            object? val = (dgvOrders.Columns.Contains("MaDonHang") ? row.Cells["MaDonHang"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;

            int orderId = Convert.ToInt32(val);
            string orderStatus = (dgvOrders.Columns.Contains("TrangThai") ? row.Cells["TrangThai"].Value : null)?.ToString()
                ?? (row.Cells.Count > 4 ? row.Cells[4].Value?.ToString() : string.Empty) ?? string.Empty;

            // Kiểm soát trạng thái nút hành động:
            // - Đơn đã Hoàn thành: Giảm opacity, đổi màu xám mờ và đổi cursor No (vì khách đã có hàng trong Thư viện, không cần duyệt lại)
            // - Đơn đã Hủy: Giảm opacity, đổi màu xám mờ và đổi cursor No (không hủy tiếp)
            bool isCompleted = orderStatus == "Hoàn thành";
            bool isCancelled = orderStatus == "Đã hủy";

            SetButtonVisualState(btnApprove, !isCompleted, Color.FromArgb(90, 160, 45),
                "Kích hoạt thủ công đơn hàng và cấp bản quyền vào Thư viện của khách",
                "Đơn hàng này đã Hoàn thành và sản phẩm đã có sẵn trong Thư viện của khách hàng", ref _canApprove);

            SetButtonVisualState(btnCancelOrder, !isCancelled, Color.FromArgb(180, 40, 40),
                "Hủy đơn hàng này", "Đơn hàng này đã ở trạng thái Đã hủy", ref _canCancel);

            dgvOrderDetails.Rows.Clear();

            List<ChiTietDonHangDTO> details = _orderService.LayChiTietDonHang(orderId);
            foreach (ChiTietDonHangDTO item in details)
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

            DataGridViewRow row = dgvOrders.SelectedRows[0];
            if (row == null || row.Index < 0) return;

            object? val = (dgvOrders.Columns.Contains("MaDonHang") ? row.Cells["MaDonHang"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;

            int orderId = Convert.ToInt32(val);
            string code = (dgvOrders.Columns.Contains("MaDonHangCode") ? row.Cells["MaDonHangCode"].Value : null)?.ToString()
                ?? (row.Cells.Count > 1 ? row.Cells[1].Value?.ToString() : string.Empty) ?? string.Empty;

            string confirmPrompt = status == "Hoàn thành"
                ? $"Bạn có chắc muốn kích hoạt thủ công đơn hàng [{code}] sang 'Hoàn thành' và cấp sản phẩm vào Thư viện của khách?"
                : $"Bạn có chắc muốn chuyển đơn hàng [{code}] sang trạng thái '{status}'?";

            if (MessageBox.Show(confirmPrompt, "Xác nhận quản trị", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string msg;
                bool ok = status == "Hoàn thành" ? _orderService.DuyetDonHang(orderId, out msg) : _orderService.HuyDonHang(orderId, out msg);
                if (ok)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOrders();
                }
                else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void BtnApprove_Click(object? sender, EventArgs e)
        {
            if (!_canApprove)
            {
                MessageBox.Show("Đơn hàng này đã Hoàn thành và sản phẩm đã có sẵn trong Thư viện của khách hàng, không thể kích hoạt lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ChangeOrderStatus("Hoàn thành");
        }

        private void BtnCancelOrder_Click(object? sender, EventArgs e)
        {
            if (!_canCancel)
            {
                MessageBox.Show("Đơn hàng này đã ở trạng thái Đã hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ChangeOrderStatus("Đã hủy");
        }
    }
}
