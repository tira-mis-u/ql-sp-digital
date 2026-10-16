using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom5_Orders;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom5_Orders
{
    // UI 7: Xem lịch sử đơn hàng cá nhân của khách hàng
    // Thành viên phụ trách: Phạm Văn Đam (Nhóm 5)
    public class OrderHistoryControl : UserControl
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
            LoadHistory();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);

            // Tiêu đề
            panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45
            };

            Label lblHeader = new Label
            {
                Text = "📜 LỊCH SỬ GIAO DỊCH & HÓA ĐƠN CÁ NHÂN",
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
                Width = 120,
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => LoadHistory();

            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(lblHeader);

            // Bảng danh sách đơn hàng đã mua
            dgvOrders = new DataGridView
            {
                Location = new Point(25, 65),
                Size = new Size(580, 615),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
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
            panelDetail = new Panel
            {
                Location = new Point(625, 65),
                Size = new Size(390, 615),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(15)
            };

            Label lblDetailTitle = new Label
            {
                Text = "CÁC MÓN HÀNG TRONG HÓA ĐƠN",
                Dock = DockStyle.Top,
                Height = 30,
                ForeColor = Color.FromArgb(164, 208, 7),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                UseMnemonic = false
            };

            dgvDetails = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(42, 71, 94),
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
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

            this.Resize += (s, e) => AdjustLayout();
            AdjustLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (this.ClientSize.Width < 500 || dgvOrders == null || panelDetail == null) return;
            int totalW = this.ClientSize.Width;
            int totalH = this.ClientSize.Height;

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
    }
}
