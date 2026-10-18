namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    partial class DashboardControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panelTopBar = new Panel();
            btnRefresh = new Button();
            lblHeader = new Label();
            panelCards = new TableLayoutPanel();
            card4 = new Panel();
            lblTotalProducts = new Label();
            lblTitle4 = new Label();
            card3 = new Panel();
            lblTotalCustomers = new Label();
            lblTitle3 = new Label();
            card2 = new Panel();
            lblTotalOrders = new Label();
            lblTitle2 = new Label();
            card1 = new Panel();
            lblTotalRevenue = new Label();
            lblTitle1 = new Label();
            lblTableTitle = new Label();
            dgvTopProducts = new DataGridView();
            colXepHang = new DataGridViewTextBoxColumn();
            colTenSanPham = new DataGridViewTextBoxColumn();
            colSoLuongDaBan = new DataGridViewTextBoxColumn();
            colDoanhThu = new DataGridViewTextBoxColumn();
            panelTopBar.SuspendLayout();
            panelCards.SuspendLayout();
            card4.SuspendLayout();
            card3.SuspendLayout();
            card2.SuspendLayout();
            card1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).BeginInit();
            SuspendLayout();
            // 
            // panelTopBar
            // 
            panelTopBar.Controls.Add(btnRefresh);
            panelTopBar.Controls.Add(lblHeader);
            panelTopBar.Dock = DockStyle.Top;
            panelTopBar.Location = new Point(23, 27);
            panelTopBar.Margin = new Padding(3, 5, 3, 5);
            panelTopBar.Name = "panelTopBar";
            panelTopBar.Size = new Size(1143, 75);
            panelTopBar.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(49, 98, 130);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(973, 0);
            btnRefresh.Margin = new Padding(3, 5, 3, 5);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(170, 75);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseMnemonic = false;
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Dock = DockStyle.Left;
            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Location = new Point(0, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(682, 32);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "BÁO CÁO THỐNG KÊ KINH DOANH (ADMIN DASHBOARD)";
            lblHeader.UseMnemonic = false;
            // 
            // panelCards
            // 
            panelCards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelCards.ColumnCount = 4;
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panelCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panelCards.Controls.Add(card4, 3, 0);
            panelCards.Controls.Add(card3, 2, 0);
            panelCards.Controls.Add(card2, 1, 0);
            panelCards.Controls.Add(card1, 0, 0);
            panelCards.Location = new Point(33, 160);
            panelCards.Margin = new Padding(3, 5, 3, 5);
            panelCards.Name = "panelCards";
            panelCards.RowCount = 1;
            panelCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelCards.Size = new Size(1241, 204);
            panelCards.TabIndex = 1;
            // 
            // card4
            // 
            card4.BackColor = Color.FromArgb(27, 40, 56);
            card4.Controls.Add(lblTotalProducts);
            card4.Controls.Add(lblTitle4);
            card4.Dock = DockStyle.Fill;
            card4.Location = new Point(940, 0);
            card4.Margin = new Padding(10, 0, 0, 0);
            card4.Name = "card4";
            card4.Padding = new Padding(19, 27, 19, 27);
            card4.Size = new Size(301, 204);
            card4.TabIndex = 3;
            // 
            // lblTotalProducts
            // 
            lblTotalProducts.Dock = DockStyle.Fill;
            lblTotalProducts.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTotalProducts.ForeColor = Color.FromArgb(155, 89, 182);
            lblTotalProducts.Location = new Point(19, 66);
            lblTotalProducts.Name = "lblTotalProducts";
            lblTotalProducts.Size = new Size(263, 111);
            lblTotalProducts.TabIndex = 1;
            lblTotalProducts.Text = "0 sản phẩm";
            lblTotalProducts.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalProducts.UseMnemonic = false;
            // 
            // lblTitle4
            // 
            lblTitle4.Dock = DockStyle.Top;
            lblTitle4.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTitle4.ForeColor = Color.FromArgb(161, 181, 196);
            lblTitle4.Location = new Point(19, 27);
            lblTitle4.Name = "lblTitle4";
            lblTitle4.Size = new Size(263, 39);
            lblTitle4.TabIndex = 0;
            lblTitle4.Text = "SẢN PHẨM MỞ BÁN";
            lblTitle4.UseMnemonic = false;
            // 
            // card3
            // 
            card3.BackColor = Color.FromArgb(27, 40, 56);
            card3.Controls.Add(lblTotalCustomers);
            card3.Controls.Add(lblTitle3);
            card3.Dock = DockStyle.Fill;
            card3.Location = new Point(626, 0);
            card3.Margin = new Padding(6, 0, 6, 0);
            card3.Name = "card3";
            card3.Padding = new Padding(19, 27, 19, 27);
            card3.Size = new Size(298, 204);
            card3.TabIndex = 2;
            // 
            // lblTotalCustomers
            // 
            lblTotalCustomers.Dock = DockStyle.Fill;
            lblTotalCustomers.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTotalCustomers.ForeColor = Color.FromArgb(243, 156, 18);
            lblTotalCustomers.Location = new Point(19, 66);
            lblTotalCustomers.Name = "lblTotalCustomers";
            lblTotalCustomers.Size = new Size(260, 111);
            lblTotalCustomers.TabIndex = 1;
            lblTotalCustomers.Text = "0 khách";
            lblTotalCustomers.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalCustomers.UseMnemonic = false;
            // 
            // lblTitle3
            // 
            lblTitle3.Dock = DockStyle.Top;
            lblTitle3.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTitle3.ForeColor = Color.FromArgb(161, 181, 196);
            lblTitle3.Location = new Point(19, 27);
            lblTitle3.Name = "lblTitle3";
            lblTitle3.Size = new Size(260, 39);
            lblTitle3.TabIndex = 0;
            lblTitle3.Text = "KHÁCH HÀNG ĐĂNG KÝ";
            lblTitle3.UseMnemonic = false;
            // 
            // card2
            // 
            card2.BackColor = Color.FromArgb(27, 40, 56);
            card2.Controls.Add(lblTotalOrders);
            card2.Controls.Add(lblTitle2);
            card2.Dock = DockStyle.Fill;
            card2.Location = new Point(316, 0);
            card2.Margin = new Padding(6, 0, 6, 0);
            card2.Name = "card2";
            card2.Padding = new Padding(19, 27, 19, 27);
            card2.Size = new Size(298, 204);
            card2.TabIndex = 1;
            // 
            // lblTotalOrders
            // 
            lblTotalOrders.Dock = DockStyle.Fill;
            lblTotalOrders.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.FromArgb(102, 192, 244);
            lblTotalOrders.Location = new Point(19, 66);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Size = new Size(260, 111);
            lblTotalOrders.TabIndex = 1;
            lblTotalOrders.Text = "0 đơn";
            lblTotalOrders.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalOrders.UseMnemonic = false;
            // 
            // lblTitle2
            // 
            lblTitle2.Dock = DockStyle.Top;
            lblTitle2.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTitle2.ForeColor = Color.FromArgb(161, 181, 196);
            lblTitle2.Location = new Point(19, 27);
            lblTitle2.Name = "lblTitle2";
            lblTitle2.Size = new Size(260, 39);
            lblTitle2.TabIndex = 0;
            lblTitle2.Text = "ĐƠN HÀNG THÀNH CÔNG";
            lblTitle2.UseMnemonic = false;
            // 
            // card1
            // 
            card1.BackColor = Color.FromArgb(27, 40, 56);
            card1.Controls.Add(lblTotalRevenue);
            card1.Controls.Add(lblTitle1);
            card1.Dock = DockStyle.Fill;
            card1.Location = new Point(0, 0);
            card1.Margin = new Padding(0, 0, 10, 0);
            card1.Name = "card1";
            card1.Padding = new Padding(19, 27, 19, 27);
            card1.Size = new Size(300, 204);
            card1.TabIndex = 0;
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.Dock = DockStyle.Fill;
            lblTotalRevenue.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.FromArgb(164, 208, 7);
            lblTotalRevenue.Location = new Point(19, 66);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(262, 111);
            lblTotalRevenue.TabIndex = 1;
            lblTotalRevenue.Text = "0 ₫";
            lblTotalRevenue.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalRevenue.UseMnemonic = false;
            // 
            // lblTitle1
            // 
            lblTitle1.Dock = DockStyle.Top;
            lblTitle1.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTitle1.ForeColor = Color.FromArgb(161, 181, 196);
            lblTitle1.Location = new Point(19, 27);
            lblTitle1.Name = "lblTitle1";
            lblTitle1.Size = new Size(262, 39);
            lblTitle1.TabIndex = 0;
            lblTitle1.Text = "TỔNG DOANH THU";
            lblTitle1.UseMnemonic = false;
            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblTableTitle.Location = new Point(33, 388);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(462, 28);
            lblTableTitle.TabIndex = 2;
            lblTableTitle.Text = "TOP 5 SẢN PHẨM BÁN CHẠY NHẤT HỆ THỐNG";
            lblTableTitle.UseMnemonic = false;
            // 
            // dgvTopProducts
            // 
            dgvTopProducts.AllowUserToAddRows = false;
            dgvTopProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopProducts.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvTopProducts.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvTopProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvTopProducts.ColumnHeadersHeight = 32;
            dgvTopProducts.Columns.AddRange(new DataGridViewColumn[] { colXepHang, colTenSanPham, colSoLuongDaBan, colDoanhThu });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvTopProducts.DefaultCellStyle = dataGridViewCellStyle4;
            dgvTopProducts.EnableHeadersVisualStyles = false;
            dgvTopProducts.GridColor = Color.FromArgb(42, 71, 94);
            dgvTopProducts.Location = new Point(33, 444);
            dgvTopProducts.Margin = new Padding(3, 5, 3, 5);
            dgvTopProducts.Name = "dgvTopProducts";
            dgvTopProducts.ReadOnly = true;
            dgvTopProducts.RowHeadersVisible = false;
            dgvTopProducts.RowHeadersWidth = 51;
            dgvTopProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopProducts.Size = new Size(1241, 747);
            dgvTopProducts.TabIndex = 3;
            // 
            // colXepHang
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colXepHang.DefaultCellStyle = dataGridViewCellStyle2;
            colXepHang.FillWeight = 16F;
            colXepHang.HeaderText = "Xếp hạng";
            colXepHang.MinimumWidth = 6;
            colXepHang.Name = "colXepHang";
            colXepHang.ReadOnly = true;
            // 
            // colTenSanPham
            // 
            colTenSanPham.FillWeight = 44F;
            colTenSanPham.HeaderText = "Tên sản phẩm";
            colTenSanPham.MinimumWidth = 6;
            colTenSanPham.Name = "colTenSanPham";
            colTenSanPham.ReadOnly = true;
            // 
            // colSoLuongDaBan
            // 
            colSoLuongDaBan.FillWeight = 20F;
            colSoLuongDaBan.HeaderText = "Số lượng đã bán";
            colSoLuongDaBan.MinimumWidth = 6;
            colSoLuongDaBan.Name = "colSoLuongDaBan";
            colSoLuongDaBan.ReadOnly = true;
            // 
            // colDoanhThu
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDoanhThu.DefaultCellStyle = dataGridViewCellStyle3;
            colDoanhThu.FillWeight = 20F;
            colDoanhThu.HeaderText = "Doanh thu tích lũy";
            colDoanhThu.MinimumWidth = 6;
            colDoanhThu.Name = "colDoanhThu";
            colDoanhThu.ReadOnly = true;
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(23, 26, 33);
            Controls.Add(dgvTopProducts);
            Controls.Add(lblTableTitle);
            Controls.Add(panelCards);
            Controls.Add(panelTopBar);
            Margin = new Padding(3, 5, 3, 5);
            Name = "DashboardControl";
            Padding = new Padding(23, 27, 23, 27);
            Size = new Size(1189, 933);
            panelTopBar.ResumeLayout(false);
            panelTopBar.PerformLayout();
            panelCards.ResumeLayout(false);
            card4.ResumeLayout(false);
            card3.ResumeLayout(false);
            card2.ResumeLayout(false);
            card1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTopBar;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel panelCards;
        private System.Windows.Forms.Panel card1;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Panel card2;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Panel card3;
        private System.Windows.Forms.Label lblTitle3;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Panel card4;
        private System.Windows.Forms.Label lblTitle4;
        private System.Windows.Forms.Label lblTotalProducts;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvTopProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXepHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongDaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoanhThu;
    }
}
