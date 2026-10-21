using System.Drawing;
using System.Windows.Forms;

namespace qlspdigital.Views.Nhom5_Orders
{
    partial class OrderManageControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.colMaDonHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaDonHangCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongThanhToan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.colDetailTenSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBottomActions = new System.Windows.Forms.Panel();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnCancelOrder = new System.Windows.Forms.Button();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.panelBottomActions.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(437, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "QUẢN LÝ VÀ THEO DÕI ĐƠN HÀNG TOÀN SÀN";

            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelFilter.Controls.Add(this.btnRefresh);
            this.panelFilter.Controls.Add(this.btnSearch);
            this.panelFilter.Controls.Add(this.txtSearch);
            this.panelFilter.Controls.Add(this.lblSearch);
            this.panelFilter.Controls.Add(this.cboStatus);
            this.panelFilter.Controls.Add(this.lblStatus);
            this.panelFilter.Location = new System.Drawing.Point(20, 45);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelFilter.Size = new System.Drawing.Size(1000, 50);
            this.panelFilter.TabIndex = 1;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(15, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(75, 17);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Trạng thái:";
            this.lblStatus.UseMnemonic = false;

            // 
            // cboStatus
            // 
            this.cboStatus.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStatus.ForeColor = System.Drawing.Color.White;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Tất cả",
            "Hoàn thành",
            "Chờ xử lý",
            "Đã hủy"});
            this.cboStatus.Location = new System.Drawing.Point(105, 10);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(150, 25);
            this.cboStatus.TabIndex = 1;
            this.cboStatus.SelectedIndex = 0;
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.CboStatus_SelectedIndexChanged);

            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(280, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(70, 17);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Tìm kiếm:";
            this.lblSearch.UseMnemonic = false;

            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(365, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Mã đơn hàng, tên khách...";
            this.txtSearch.Size = new System.Drawing.Size(320, 25);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);

            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(695, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 32);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseMnemonic = false;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(60, 75, 90);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(805, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseMnemonic = false;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);

            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.BackgroundColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.dgvOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            this.dgvOrders.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.dgvOrders.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            this.dgvOrders.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.dgvOrders.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.dgvOrders.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvOrders.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvOrders.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.dgvOrders.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvOrders.EnableHeadersVisualStyles = false;
            this.dgvOrders.GridColor = System.Drawing.Color.FromArgb(42, 71, 94);
            this.dgvOrders.Location = new System.Drawing.Point(20, 95);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(630, 535);
            this.dgvOrders.TabIndex = 2;
            this.dgvOrders.SelectionChanged += new System.EventHandler(this.DgvOrders_SelectionChanged);

            System.Windows.Forms.DataGridViewCellStyle dgvOrdersTotalCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dgvOrdersTotalCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            System.Windows.Forms.DataGridViewCellStyle dgvOrdersCenterCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dgvOrdersCenterCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            // 
            // colMaDonHang
            // 
            this.colMaDonHang.HeaderText = "ID";
            this.colMaDonHang.Name = "MaDonHang";
            this.colMaDonHang.ReadOnly = true;
            this.colMaDonHang.Visible = false;
            // 
            // colMaDonHangCode
            // 
            this.colMaDonHangCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMaDonHangCode.FillWeight = 20F;
            this.colMaDonHangCode.HeaderText = "Mã đơn";
            this.colMaDonHangCode.MinimumWidth = 110;
            this.colMaDonHangCode.Name = "MaDonHangCode";
            this.colMaDonHangCode.ReadOnly = true;
            // 
            // colTenKhachHang
            // 
            this.colTenKhachHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenKhachHang.FillWeight = 32F;
            this.colTenKhachHang.HeaderText = "Khách hàng";
            this.colTenKhachHang.MinimumWidth = 130;
            this.colTenKhachHang.Name = "TenKhachHang";
            this.colTenKhachHang.ReadOnly = true;
            // 
            // colTongThanhToan
            // 
            this.colTongThanhToan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTongThanhToan.DefaultCellStyle = dgvOrdersTotalCellStyle;
            this.colTongThanhToan.FillWeight = 18F;
            this.colTongThanhToan.HeaderText = "Tổng tiền";
            this.colTongThanhToan.MinimumWidth = 100;
            this.colTongThanhToan.Name = "TongThanhToan";
            this.colTongThanhToan.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTrangThai.DefaultCellStyle = dgvOrdersCenterCellStyle;
            this.colTrangThai.FillWeight = 14F;
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 85;
            this.colTrangThai.Name = "TrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // colNgayTao
            // 
            this.colNgayTao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNgayTao.DefaultCellStyle = dgvOrdersCenterCellStyle;
            this.colNgayTao.FillWeight = 16F;
            this.colNgayTao.HeaderText = "Ngày đặt";
            this.colNgayTao.MinimumWidth = 110;
            this.colNgayTao.Name = "NgayTao";
            this.colNgayTao.ReadOnly = true;

            this.dgvOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colMaDonHang,
                this.colMaDonHangCode,
                this.colTenKhachHang,
                this.colTongThanhToan,
                this.colTrangThai,
                this.colNgayTao
            });

            // 
            // panelDetails
            // 
            this.panelDetails.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelDetails.Controls.Add(this.dgvOrderDetails);
            this.panelDetails.Controls.Add(this.lblDetailTitle);
            this.panelDetails.Location = new System.Drawing.Point(670, 95);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Padding = new System.Windows.Forms.Padding(15);
            this.panelDetails.Size = new System.Drawing.Size(350, 535);
            this.panelDetails.TabIndex = 3;

            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblDetailTitle.Location = new System.Drawing.Point(15, 15);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(320, 25);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "CHI TIẾT MẶT HÀNG TRONG ĐƠN";
            this.lblDetailTitle.UseMnemonic = false;

            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.AllowUserToAddRows = false;
            this.dgvOrderDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderDetails.BackgroundColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.dgvOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderDetails.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            this.dgvOrderDetails.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvOrderDetails.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.dgvOrderDetails.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            this.dgvOrderDetails.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.dgvOrderDetails.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.dgvOrderDetails.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvOrderDetails.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvOrderDetails.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.dgvOrderDetails.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetails.EnableHeadersVisualStyles = false;
            this.dgvOrderDetails.GridColor = System.Drawing.Color.FromArgb(42, 71, 94);
            this.dgvOrderDetails.Location = new System.Drawing.Point(15, 40);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.ReadOnly = true;
            this.dgvOrderDetails.RowHeadersVisible = false;
            this.dgvOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderDetails.Size = new System.Drawing.Size(320, 480);
            this.dgvOrderDetails.TabIndex = 1;

            System.Windows.Forms.DataGridViewCellStyle dgvDetailsCenterCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dgvDetailsCenterCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            System.Windows.Forms.DataGridViewCellStyle dgvDetailsPriceCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dgvDetailsPriceCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            // 
            // colDetailTenSanPham
            // 
            this.colDetailTenSanPham.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDetailTenSanPham.FillWeight = 55F;
            this.colDetailTenSanPham.HeaderText = "Sản phẩm";
            this.colDetailTenSanPham.MinimumWidth = 100;
            this.colDetailTenSanPham.Name = "TenSanPham";
            this.colDetailTenSanPham.ReadOnly = true;
            // 
            // colDetailSoLuong
            // 
            this.colDetailSoLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDetailSoLuong.DefaultCellStyle = dgvDetailsCenterCellStyle;
            this.colDetailSoLuong.FillWeight = 15F;
            this.colDetailSoLuong.HeaderText = "SL";
            this.colDetailSoLuong.MinimumWidth = 40;
            this.colDetailSoLuong.Name = "SoLuong";
            this.colDetailSoLuong.ReadOnly = true;
            // 
            // colDetailDonGia
            // 
            this.colDetailDonGia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDetailDonGia.DefaultCellStyle = dgvDetailsPriceCellStyle;
            this.colDetailDonGia.FillWeight = 30F;
            this.colDetailDonGia.HeaderText = "Đơn giá";
            this.colDetailDonGia.MinimumWidth = 85;
            this.colDetailDonGia.Name = "DonGia";
            this.colDetailDonGia.ReadOnly = true;

            this.dgvOrderDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colDetailTenSanPham,
                this.colDetailSoLuong,
                this.colDetailDonGia
            });

            // 
            // panelBottomActions
            // 
            this.panelBottomActions.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.panelBottomActions.Controls.Add(this.btnCancelOrder);
            this.panelBottomActions.Controls.Add(this.btnApprove);
            this.panelBottomActions.Location = new System.Drawing.Point(20, 642);
            this.panelBottomActions.Name = "panelBottomActions";
            this.panelBottomActions.Size = new System.Drawing.Size(1000, 50);
            this.panelBottomActions.TabIndex = 4;

            // 
            // btnApprove
            // 
            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnApprove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApprove.FlatAppearance.BorderSize = 0;
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnApprove.ForeColor = System.Drawing.Color.White;
            this.btnApprove.Location = new System.Drawing.Point(0, 4);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(260, 36);
            this.btnApprove.TabIndex = 0;
            this.btnApprove.Text = "Kích hoạt thủ công (Cấp Thư viện)";
            this.btnApprove.UseMnemonic = false;
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.BtnApprove_Click);

            // 
            // btnCancelOrder
            // 
            this.btnCancelOrder.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            this.btnCancelOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelOrder.FlatAppearance.BorderSize = 0;
            this.btnCancelOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelOrder.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancelOrder.ForeColor = System.Drawing.Color.White;
            this.btnCancelOrder.Location = new System.Drawing.Point(275, 4);
            this.btnCancelOrder.Name = "btnCancelOrder";
            this.btnCancelOrder.Size = new System.Drawing.Size(150, 36);
            this.btnCancelOrder.TabIndex = 1;
            this.btnCancelOrder.Text = "Hủy đơn hàng";
            this.btnCancelOrder.UseMnemonic = false;
            this.btnCancelOrder.UseVisualStyleBackColor = false;
            this.btnCancelOrder.Click += new System.EventHandler(this.BtnCancelOrder_Click);

            // 
            // OrderManageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.Controls.Add(this.panelBottomActions);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.lblHeader);
            this.Name = "OrderManageControl";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(1040, 700);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.panelDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.panelBottomActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.AdjustLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaDonHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaDonHangCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongThanhToan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTao;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailTenSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailDonGia;
        private System.Windows.Forms.Panel panelBottomActions;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnCancelOrder;

        #region Windows Form Designer - Layout & UI Helpers

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (panelFilter == null || dgvOrders == null || panelDetails == null || panelBottomActions == null || lblStatus == null || cboStatus == null || lblSearch == null || txtSearch == null || btnRefresh == null) return;
            int totalW = System.Math.Max(600, this.ClientSize.Width);
            int totalH = System.Math.Max(380, this.ClientSize.Height);

            panelFilter.Location = new System.Drawing.Point(20, 45);
            panelFilter.Size = new System.Drawing.Size(totalW - 40, 50);

            // Bố cục tuần tự tương đối - không bao giờ đè nhãn
            lblStatus.Location = new System.Drawing.Point(15, 15);
            cboStatus.Location = new System.Drawing.Point(lblStatus.Right + 12, 10);
            lblSearch.Location = new System.Drawing.Point(cboStatus.Right + 24, 15);
            txtSearch.Location = new System.Drawing.Point(lblSearch.Right + 12, 10);

            int btnSearchW = 100;
            int btnRefreshW = 100;
            int filterW = panelFilter.ClientSize.Width;

            int searchW = System.Math.Clamp(filterW - txtSearch.Left - btnSearchW - btnRefreshW - 40, 180, 520);
            txtSearch.Size = new System.Drawing.Size(searchW, 30);

            btnSearch.Location = new System.Drawing.Point(txtSearch.Right + 10, 9);
            btnSearch.Size = new System.Drawing.Size(btnSearchW, 32);

            btnRefresh.Location = new System.Drawing.Point(btnSearch.Right + 10, 9);
            btnRefresh.Size = new System.Drawing.Size(btnRefreshW, 32);

            int rightW = System.Math.Clamp((int)(totalW * 0.36), 300, 420);
            int leftW = System.Math.Max(260, totalW - rightW - 55); // margin 20 left + 15 gap + 20 right
            int bottomH = 48;
            int contentH = System.Math.Max(180, totalH - 105 - bottomH - 18);

            dgvOrders.Location = new System.Drawing.Point(20, 105);
            dgvOrders.Size = new System.Drawing.Size(leftW, contentH);

            panelDetails.Location = new System.Drawing.Point(20 + leftW + 15, 105);
            panelDetails.Size = new System.Drawing.Size(rightW, contentH);

            panelBottomActions.Location = new System.Drawing.Point(20, 105 + contentH + 8);
            panelBottomActions.Size = new System.Drawing.Size(totalW - 40, bottomH);
        }

        #endregion
    }
}
