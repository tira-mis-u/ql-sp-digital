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
    public class OrderManageControl : UserControl
    {
        private readonly DonHangService _orderService;

        private DataGridView dgvOrders = null!;
        private DataGridView dgvOrderDetails = null!;
        private ComboBox cboStatus = null!;
        private TextBox txtSearch = null!;
        private Button btnApprove = null!;
        private Button btnCancelOrder = null!;
        private Button btnRefresh = null!;

        public OrderManageControl()
        {
            _orderService = new DonHangService();

            InitializeComponent();
            LoadOrders();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(20);

            // Tiêu đề
            Label lblHeader = new Label
            {
                Text = "📑 QUẢN LÝ VÀ THEO DÕI ĐƠN HÀNG TOÀN SÀN",
                Location = new Point(20, 10),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            // 1. THANH LỌC VÀ TÌM KIẾM
            Panel panelFilter = new Panel
            {
                Location = new Point(20, 45),
                Size = new Size(1020, 42),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(10, 6, 10, 6)
            };

            Label lblStatus = new Label { Text = "Trạng thái:", Location = new Point(10, 11), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
            cboStatus = new ComboBox { Location = new Point(85, 8), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White };
            cboStatus.Items.AddRange(new string[] { "Tất cả", "Hoàn thành", "Chờ xử lý", "Đã hủy" });
            cboStatus.SelectedIndex = 0;
            cboStatus.SelectedIndexChanged += (s, e) => LoadOrders();

            Label lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(255, 11), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
            txtSearch = new TextBox { Location = new Point(325, 8), Width = 280, Font = new Font("Segoe UI", 9.5f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White, PlaceholderText = "Mã đơn hàng, tên khách..." };
            txtSearch.TextChanged += (s, e) => LoadOrders();

            btnRefresh = new Button
            {
                Text = "Làm mới",
                Location = new Point(620, 6),
                Size = new Size(90, 28),
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); cboStatus.SelectedIndex = 0; LoadOrders(); };

            panelFilter.Controls.Add(btnRefresh);
            panelFilter.Controls.Add(txtSearch);
            panelFilter.Controls.Add(lblSearch);
            panelFilter.Controls.Add(cboStatus);
            panelFilter.Controls.Add(lblStatus);

            // 2. BẢNG DANH SÁCH ĐƠN HÀNG (READ)
            dgvOrders = new DataGridView
            {
                Location = new Point(20, 95),
                Size = new Size(650, 480),
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
            dgvOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
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

            dgvOrders.Columns["MaDonHang"].Width = 45;
            dgvOrders.Columns["MaDonHangCode"].Width = 140;
            dgvOrders.Columns["TenKhachHang"].FillWeight = 30;
            dgvOrders.Columns["TongThanhToan"].Width = 100;
            dgvOrders.Columns["TrangThai"].Width = 95;
            dgvOrders.Columns["NgayTao"].Width = 120;

            dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;

            // 3. BẢNG CHI TIẾT CÁC MÓN TRONG ĐƠN HÀNG
            Panel panelDetails = new Panel
            {
                Location = new Point(685, 95),
                Size = new Size(355, 480),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(15)
            };

            Label lblDetailTitle = new Label
            {
                Text = "CHI TIẾT MẶT HÀNG TRONG ĐƠN",
                Dock = DockStyle.Top,
                Height = 25,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold)
            };

            dgvOrderDetails = new DataGridView
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
            dgvOrderDetails.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvOrderDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvOrderDetails.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvOrderDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvOrderDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(164, 208, 7);
            dgvOrderDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvOrderDetails.EnableHeadersVisualStyles = false;

            dgvOrderDetails.Columns.Add("TenSanPham", "Sản phẩm");
            dgvOrderDetails.Columns.Add("SoLuong", "SL");
            dgvOrderDetails.Columns.Add("DonGia", "Đơn giá");
            dgvOrderDetails.Columns["SoLuong"].Width = 45;
            dgvOrderDetails.Columns["DonGia"].Width = 90;

            panelDetails.Controls.Add(dgvOrderDetails);
            panelDetails.Controls.Add(lblDetailTitle);

            // 4. CÁC NÚT DUYỆT ĐƠN / HỦY ĐƠN (UPDATE)
            btnApprove = new Button
            {
                Text = "✅ Duyệt đơn (Chuyển sang Hoàn thành & Cấp Thư viện)",
                Location = new Point(20, 590),
                Size = new Size(380, 36),
                BackColor = Color.FromArgb(92, 126, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += (s, e) => ChangeOrderStatus("Hoàn thành");

            btnCancelOrder = new Button
            {
                Text = "❌ Hủy đơn hàng",
                Location = new Point(415, 590),
                Size = new Size(160, 36),
                BackColor = Color.FromArgb(180, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancelOrder.FlatAppearance.BorderSize = 0;
            btnCancelOrder.Click += (s, e) => ChangeOrderStatus("Đã hủy");

            this.Controls.Add(btnCancelOrder);
            this.Controls.Add(btnApprove);
            this.Controls.Add(panelDetails);
            this.Controls.Add(dgvOrders);
            this.Controls.Add(panelFilter);
            this.Controls.Add(lblHeader);
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
    }
}
