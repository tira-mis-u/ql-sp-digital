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

        private DataGridView dgvOrders = null!;
        private DataGridView dgvOrderDetails = null!;
        private Panel panelFilter = null!;
        private Panel panelDetails = null!;
        private Panel panelBottomActions = null!;
        private ComboBox cboStatus = null!;
        private TextBox txtSearch = null!;
        private Button btnApprove = null!;
        private Button btnCancelOrder = null!;
        private Button btnRefresh = null!;

        public OrderManageControl()
        {
            _orderService = new DonHangService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadOrders();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(20);

            // Tiêu đề
            Label lblHeader = new Label();
            lblHeader.Text = "📑 QUẢN LÝ VÀ THEO DÕI ĐƠN HÀNG TOÀN SÀN";
            lblHeader.Location = new Point(20, 10);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            // 1. THANH LỌC VÀ TÌM KIẾM
            panelFilter = new Panel();
            panelFilter.Location = new Point(20, 45);
            panelFilter.Size = new Size(1000, 42);
            panelFilter.BackColor = Color.FromArgb(27, 40, 56);
            panelFilter.Padding = new Padding(10, 6, 10, 6);

            Label lblStatus = new Label();
            lblStatus.Text = "Trạng thái:";
            lblStatus.Location = new Point(10, 11);
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.White;
            lblStatus.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblStatus.UseMnemonic = false;

            cboStatus = new ComboBox();
            cboStatus.Location = new Point(110, 7);
            cboStatus.Width = 145;
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Font = new Font("Segoe UI", 9.5f);
            cboStatus.BackColor = Color.FromArgb(18, 25, 35);
            cboStatus.ForeColor = Color.White;
            cboStatus.FlatStyle = FlatStyle.Flat;
            cboStatus.Items.AddRange(new string[] { "Tất cả", "Hoàn thành", "Chờ xử lý", "Đã hủy" });
            cboStatus.SelectedIndex = 0;
            cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;

            Label lblSearch = new Label();
            lblSearch.Text = "Tìm kiếm:";
            lblSearch.Location = new Point(275, 11);
            lblSearch.AutoSize = true;
            lblSearch.ForeColor = Color.White;
            lblSearch.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblSearch.UseMnemonic = false;

            txtSearch = new TextBox();
            txtSearch.Location = new Point(365, 7);
            txtSearch.Width = 250;
            txtSearch.Font = new Font("Segoe UI", 9.5f);
            txtSearch.BackColor = Color.FromArgb(18, 25, 35);
            txtSearch.ForeColor = Color.White;
            txtSearch.PlaceholderText = "Mã đơn hàng, tên khách...";
            txtSearch.TextChanged += TxtSearch_TextChanged;

            btnRefresh = new Button();
            btnRefresh.Text = "Làm mới";
            btnRefresh.Location = new Point(630, 7);
            btnRefresh.Size = new Size(95, 28);
            btnRefresh.BackColor = Color.FromArgb(49, 98, 130);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.UseMnemonic = false;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            panelFilter.Controls.Add(btnRefresh);
            panelFilter.Controls.Add(txtSearch);
            panelFilter.Controls.Add(lblSearch);
            panelFilter.Controls.Add(cboStatus);
            panelFilter.Controls.Add(lblStatus);

            // 2. BẢNG DANH SÁCH ĐƠN HÀNG (READ)
            dgvOrders = new DataGridView();
            dgvOrders.Location = new Point(20, 95);
            dgvOrders.Size = new Size(630, 535);
            dgvOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvOrders.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvOrders.ForeColor = Color.White;
            dgvOrders.GridColor = Color.FromArgb(42, 71, 94);
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvOrders.DefaultCellStyle.ForeColor = Color.White;
            dgvOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvOrders.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvOrders.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvOrders.EnableHeadersVisualStyles = false;

            dgvOrders.Columns.Add("MaDonHang", "ID");
            dgvOrders.Columns.Add("MaDonHangCode", "Mã đơn");
            dgvOrders.Columns.Add("TenKhachHang", "Khách hàng");
            dgvOrders.Columns.Add("TongThanhToan", "Tổng tiền");
            dgvOrders.Columns.Add("TrangThai", "Trạng thái");
            dgvOrders.Columns.Add("NgayTao", "Ngày đặt");

            dgvOrders.Columns["MaDonHang"].Visible = false;

            dgvOrders.Columns["MaDonHangCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrders.Columns["MaDonHangCode"].Width = 150;

            dgvOrders.Columns["TenKhachHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOrders.Columns["TenKhachHang"].FillWeight = 100;

            dgvOrders.Columns["TongThanhToan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrders.Columns["TongThanhToan"].Width = 110;
            dgvOrders.Columns["TongThanhToan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvOrders.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrders.Columns["TrangThai"].Width = 95;
            dgvOrders.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvOrders.Columns["NgayTao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrders.Columns["NgayTao"].Width = 135;
            dgvOrders.Columns["NgayTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;

            // 3. BẢNG CHI TIẾT CÁC MÓN TRONG ĐƠN HÀNG
            panelDetails = new Panel();
            panelDetails.Location = new Point(670, 95);
            panelDetails.Size = new Size(350, 535);
            panelDetails.BackColor = Color.FromArgb(27, 40, 56);
            panelDetails.Padding = new Padding(15);

            Label lblDetailTitle = new Label();
            lblDetailTitle.Text = "CHI TIẾT MẶT HÀNG TRONG ĐƠN";
            lblDetailTitle.Dock = DockStyle.Top;
            lblDetailTitle.Height = 25;
            lblDetailTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblDetailTitle.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblDetailTitle.UseMnemonic = false;

            dgvOrderDetails = new DataGridView();
            dgvOrderDetails.Dock = DockStyle.Fill;
            dgvOrderDetails.BackgroundColor = Color.FromArgb(18, 25, 35);
            dgvOrderDetails.ForeColor = Color.White;
            dgvOrderDetails.GridColor = Color.FromArgb(42, 71, 94);
            dgvOrderDetails.BorderStyle = BorderStyle.None;
            dgvOrderDetails.RowHeadersVisible = false;
            dgvOrderDetails.AllowUserToAddRows = false;
            dgvOrderDetails.ReadOnly = true;
            dgvOrderDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrderDetails.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvOrderDetails.DefaultCellStyle.ForeColor = Color.White;
            dgvOrderDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvOrderDetails.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvOrderDetails.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvOrderDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvOrderDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(164, 208, 7);
            dgvOrderDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvOrderDetails.EnableHeadersVisualStyles = false;

            dgvOrderDetails.Columns.Add("TenSanPham", "Sản phẩm");
            dgvOrderDetails.Columns.Add("SoLuong", "SL");
            dgvOrderDetails.Columns.Add("DonGia", "Đơn giá");

            dgvOrderDetails.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOrderDetails.Columns["TenSanPham"].FillWeight = 100;

            dgvOrderDetails.Columns["SoLuong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrderDetails.Columns["SoLuong"].Width = 45;
            dgvOrderDetails.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvOrderDetails.Columns["DonGia"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrderDetails.Columns["DonGia"].Width = 100;
            dgvOrderDetails.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            panelDetails.Controls.Add(dgvOrderDetails);
            panelDetails.Controls.Add(lblDetailTitle);

            // 4. CÁC NÚT DUYỆT ĐƠN / HỦY ĐƠN (UPDATE)
            panelBottomActions = new Panel();
            panelBottomActions.Location = new Point(20, 642);
            panelBottomActions.Size = new Size(1000, 50);
            panelBottomActions.BackColor = Color.FromArgb(23, 26, 33);

            btnApprove = new Button();
            btnApprove.Text = "✅ Duyệt đơn (Hoàn thành & Cấp Thư viện)";
            btnApprove.Location = new Point(0, 5);
            btnApprove.Size = new Size(380, 38);
            btnApprove.BackColor = Color.FromArgb(92, 126, 16);
            btnApprove.ForeColor = Color.White;
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnApprove.Cursor = Cursors.Hand;
            btnApprove.UseMnemonic = false;
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += BtnApprove_Click;

            btnCancelOrder = new Button();
            btnCancelOrder.Text = "❌ Hủy đơn hàng";
            btnCancelOrder.Location = new Point(395, 5);
            btnCancelOrder.Size = new Size(175, 38);
            btnCancelOrder.BackColor = Color.FromArgb(180, 40, 40);
            btnCancelOrder.ForeColor = Color.White;
            btnCancelOrder.FlatStyle = FlatStyle.Flat;
            btnCancelOrder.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnCancelOrder.Cursor = Cursors.Hand;
            btnCancelOrder.UseMnemonic = false;
            btnCancelOrder.FlatAppearance.BorderSize = 0;
            btnCancelOrder.Click += BtnCancelOrder_Click;

            panelBottomActions.Controls.Add(btnCancelOrder);
            panelBottomActions.Controls.Add(btnApprove);

            this.Controls.Add(panelBottomActions);
            this.Controls.Add(panelDetails);
            this.Controls.Add(dgvOrders);
            this.Controls.Add(panelFilter);
            this.Controls.Add(lblHeader);

            panelBottomActions.BringToFront();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelFilter == null || dgvOrders == null || panelDetails == null || panelBottomActions == null) return;
            int totalW = Math.Max(1040, this.ClientSize.Width);
            int totalH = Math.Max(700, this.ClientSize.Height);

            panelFilter.Location = new Point(20, 45);
            panelFilter.Size = new Size(totalW - 40, 42);

            int leftW = Math.Max(400, (int)((totalW - 55) * 0.60));
            int rightW = totalW - 55 - leftW;
            int contentH = Math.Max(250, totalH - 165);

            dgvOrders.Location = new Point(20, 95);
            dgvOrders.Size = new Size(leftW, contentH);

            panelDetails.Location = new Point(20 + leftW + 15, 95);
            panelDetails.Size = new Size(rightW, contentH);

            panelBottomActions.Location = new Point(20, 95 + contentH + 12);
            panelBottomActions.Size = new Size(totalW - 40, 50);
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
            if (dgvOrders.SelectedRows.Count == 0) return;

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["MaDonHang"].Value);
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

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["MaDonHang"].Value);
            string code = dgvOrders.SelectedRows[0].Cells["MaDonHangCode"].Value?.ToString() ?? string.Empty;

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
