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

        private DataGridView dgvOrders = null!;
        private DataGridView dgvDetails = null!;
        private Panel panelTop = null!;
        private Panel panelDetail = null!;
        private Button btnRefresh = null!;

        public OrderHistoryControl()
        {
            _orderService = new DonHangService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadHistory();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(25);

            // Tiêu đề
            panelTop = new Panel();
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 45;

            Label lblHeader = new Label();
            lblHeader.Text = "📜 LỊCH SỬ GIAO DỊCH & HÓA ĐƠN CÁ NHÂN";
            lblHeader.Dock = DockStyle.Left;
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblHeader.UseMnemonic = false;

            btnRefresh = new Button();
            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.Width = 120;
            btnRefresh.BackColor = Color.FromArgb(49, 98, 130);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.UseMnemonic = false;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(lblHeader);

            // Bảng danh sách đơn hàng đã mua
            dgvOrders = new DataGridView();
            dgvOrders.Location = new Point(25, 65);
            dgvOrders.Size = new Size(580, 615);
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
            dgvOrders.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvOrders.EnableHeadersVisualStyles = false;

            dgvOrders.Columns.Add("MaDonHang", "ID");
            dgvOrders.Columns.Add("MaDonHangCode", "Mã đơn hàng");
            dgvOrders.Columns.Add("TongThanhToan", "Tổng tiền");
            dgvOrders.Columns.Add("TrangThai", "Trạng thái");
            dgvOrders.Columns.Add("NgayTao", "Thời gian");

            dgvOrders.Columns["MaDonHang"].Visible = false;

            dgvOrders.Columns["MaDonHangCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOrders.Columns["MaDonHangCode"].FillWeight = 100;

            dgvOrders.Columns["TongThanhToan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrders.Columns["TongThanhToan"].Width = 115;
            dgvOrders.Columns["TongThanhToan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvOrders.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrders.Columns["TrangThai"].Width = 100;
            dgvOrders.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvOrders.Columns["NgayTao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrders.Columns["NgayTao"].Width = 140;
            dgvOrders.Columns["NgayTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;

            // Bảng chi tiết sản phẩm thuộc đơn hàng
            panelDetail = new Panel();
            panelDetail.Location = new Point(625, 65);
            panelDetail.Size = new Size(390, 615);
            panelDetail.BackColor = Color.FromArgb(27, 40, 56);
            panelDetail.Padding = new Padding(15);

            Label lblDetailTitle = new Label();
            lblDetailTitle.Text = "CÁC MÓN HÀNG TRONG HÓA ĐƠN";
            lblDetailTitle.Dock = DockStyle.Top;
            lblDetailTitle.Height = 30;
            lblDetailTitle.ForeColor = Color.FromArgb(164, 208, 7);
            lblDetailTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblDetailTitle.UseMnemonic = false;

            dgvDetails = new DataGridView();
            dgvDetails.Dock = DockStyle.Fill;
            dgvDetails.BackgroundColor = Color.FromArgb(18, 25, 35);
            dgvDetails.ForeColor = Color.White;
            dgvDetails.GridColor = Color.FromArgb(42, 71, 94);
            dgvDetails.BorderStyle = BorderStyle.None;
            dgvDetails.RowHeadersVisible = false;
            dgvDetails.AllowUserToAddRows = false;
            dgvDetails.ReadOnly = true;
            dgvDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetails.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvDetails.DefaultCellStyle.ForeColor = Color.White;
            dgvDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvDetails.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvDetails.EnableHeadersVisualStyles = false;

            dgvDetails.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvDetails.Columns.Add("SoLuong", "SL");
            dgvDetails.Columns.Add("DonGia", "Đơn giá");

            dgvDetails.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetails.Columns["TenSanPham"].FillWeight = 100;

            dgvDetails.Columns["SoLuong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvDetails.Columns["SoLuong"].Width = 45;
            dgvDetails.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetails.Columns["DonGia"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvDetails.Columns["DonGia"].Width = 105;
            dgvDetails.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            panelDetail.Controls.Add(dgvDetails);
            panelDetail.Controls.Add(lblDetailTitle);

            this.Controls.Add(panelDetail);
            this.Controls.Add(dgvOrders);
            this.Controls.Add(panelTop);

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
            if (dgvOrders == null || panelDetail == null) return;
            int totalW = Math.Max(1040, this.ClientSize.Width);
            int totalH = Math.Max(700, this.ClientSize.Height);

            int leftW = Math.Max(400, (int)((totalW - 65) * 0.58));
            int rightW = totalW - 65 - leftW;
            int contentH = Math.Max(250, totalH - 85);

            dgvOrders.Location = new Point(25, 65);
            dgvOrders.Size = new Size(leftW, contentH);

            panelDetail.Location = new Point(25 + leftW + 15, 65);
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
            if (dgvOrders.SelectedRows.Count == 0) return;

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["MaDonHang"].Value);
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
