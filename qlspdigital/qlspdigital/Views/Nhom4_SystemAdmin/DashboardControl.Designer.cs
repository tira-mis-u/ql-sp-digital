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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTopBar = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelCards = new System.Windows.Forms.TableLayoutPanel();
            this.card4 = new System.Windows.Forms.Panel();
            this.lblTotalProducts = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();
            this.card3 = new System.Windows.Forms.Panel();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.card2 = new System.Windows.Forms.Panel();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.card1 = new System.Windows.Forms.Panel();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.dgvTopProducts = new System.Windows.Forms.DataGridView();
            this.colXepHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongDaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoanhThu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTopBar.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.card4.SuspendLayout();
            this.card3.SuspendLayout();
            this.card2.SuspendLayout();
            this.card1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTopBar
            // 
            this.panelTopBar.Controls.Add(this.btnRefresh);
            this.panelTopBar.Controls.Add(this.lblHeader);
            this.panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopBar.Location = new System.Drawing.Point(25, 25);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Size = new System.Drawing.Size(990, 42);
            this.panelTopBar.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(98)))), ((int)(((byte)(130)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(860, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 42);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseMnemonic = false;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(534, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "BÁO CÁO THỐNG KÊ KINH DOANH (ADMIN DASHBOARD)";
            this.lblHeader.UseMnemonic = false;
            // 
            // panelCards
            // 
            this.panelCards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCards.ColumnCount = 4;
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.Controls.Add(this.card4, 3, 0);
            this.panelCards.Controls.Add(this.card3, 2, 0);
            this.panelCards.Controls.Add(this.card2, 1, 0);
            this.panelCards.Controls.Add(this.card1, 0, 0);
            this.panelCards.Location = new System.Drawing.Point(25, 90);
            this.panelCards.Name = "panelCards";
            this.panelCards.RowCount = 1;
            this.panelCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelCards.Size = new System.Drawing.Size(950, 115);
            this.panelCards.TabIndex = 1;
            // 
            // card4
            // 
            this.card4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.card4.Controls.Add(this.lblTotalProducts);
            this.card4.Controls.Add(this.lblTitle4);
            this.card4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card4.Location = new System.Drawing.Point(719, 0);
            this.card4.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.card4.Name = "card4";
            this.card4.Padding = new System.Windows.Forms.Padding(15);
            this.card4.Size = new System.Drawing.Size(231, 115);
            this.card4.TabIndex = 3;
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalProducts.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.lblTotalProducts.Location = new System.Drawing.Point(15, 37);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(201, 63);
            this.lblTotalProducts.TabIndex = 1;
            this.lblTotalProducts.Text = "0 sản phẩm";
            this.lblTotalProducts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalProducts.UseMnemonic = false;
            // 
            // lblTitle4
            // 
            this.lblTitle4.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle4.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(181)))), ((int)(((byte)(196)))));
            this.lblTitle4.Location = new System.Drawing.Point(15, 15);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Size = new System.Drawing.Size(201, 22);
            this.lblTitle4.TabIndex = 0;
            this.lblTitle4.Text = "SẢN PHẨM MỞ BÁN";
            this.lblTitle4.UseMnemonic = false;
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.card3.Controls.Add(this.lblTotalCustomers);
            this.card3.Controls.Add(this.lblTitle3);
            this.card3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card3.Location = new System.Drawing.Point(478, 0);
            this.card3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.card3.Name = "card3";
            this.card3.Padding = new System.Windows.Forms.Padding(15);
            this.card3.Size = new System.Drawing.Size(229, 115);
            this.card3.TabIndex = 2;
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCustomers.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.lblTotalCustomers.Location = new System.Drawing.Point(15, 37);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(199, 63);
            this.lblTotalCustomers.TabIndex = 1;
            this.lblTotalCustomers.Text = "0 khách";
            this.lblTotalCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalCustomers.UseMnemonic = false;
            // 
            // lblTitle3
            // 
            this.lblTitle3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle3.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(181)))), ((int)(((byte)(196)))));
            this.lblTitle3.Location = new System.Drawing.Point(15, 15);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(199, 22);
            this.lblTitle3.TabIndex = 0;
            this.lblTitle3.Text = "KHÁCH HÀNG ĐĂNG KÝ";
            this.lblTitle3.UseMnemonic = false;
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.card2.Controls.Add(this.lblTotalOrders);
            this.card2.Controls.Add(this.lblTitle2);
            this.card2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card2.Location = new System.Drawing.Point(241, 0);
            this.card2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.card2.Name = "card2";
            this.card2.Padding = new System.Windows.Forms.Padding(15);
            this.card2.Size = new System.Drawing.Size(229, 115);
            this.card2.TabIndex = 1;
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalOrders.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.lblTotalOrders.Location = new System.Drawing.Point(15, 37);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(199, 63);
            this.lblTotalOrders.TabIndex = 1;
            this.lblTotalOrders.Text = "0 đơn";
            this.lblTotalOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalOrders.UseMnemonic = false;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle2.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(181)))), ((int)(((byte)(196)))));
            this.lblTitle2.Location = new System.Drawing.Point(15, 15);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(199, 22);
            this.lblTitle2.TabIndex = 0;
            this.lblTitle2.Text = "ĐƠN HÀNG THÀNH CÔNG";
            this.lblTitle2.UseMnemonic = false;
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.card1.Controls.Add(this.lblTotalRevenue);
            this.card1.Controls.Add(this.lblTitle1);
            this.card1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card1.Location = new System.Drawing.Point(0, 0);
            this.card1.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.card1.Name = "card1";
            this.card1.Padding = new System.Windows.Forms.Padding(15);
            this.card1.Size = new System.Drawing.Size(229, 115);
            this.card1.TabIndex = 0;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(208)))), ((int)(((byte)(7)))));
            this.lblTotalRevenue.Location = new System.Drawing.Point(15, 37);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(199, 63);
            this.lblTotalRevenue.TabIndex = 1;
            this.lblTotalRevenue.Text = "0 ₫";
            this.lblTotalRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalRevenue.UseMnemonic = false;
            // 
            // lblTitle1
            // 
            this.lblTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle1.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(181)))), ((int)(((byte)(196)))));
            this.lblTitle1.Location = new System.Drawing.Point(15, 15);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(199, 22);
            this.lblTitle1.TabIndex = 0;
            this.lblTitle1.Text = "TỔNG DOANH THU";
            this.lblTitle1.UseMnemonic = false;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.AutoSize = true;
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.lblTableTitle.Location = new System.Drawing.Point(25, 218);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(378, 21);
            this.lblTableTitle.TabIndex = 2;
            this.lblTableTitle.Text = "TOP 5 SẢN PHẨM BÁN CHẠY NHẤT HỆ THỐNG";
            this.lblTableTitle.UseMnemonic = false;
            // 
            // dgvTopProducts
            // 
            this.dgvTopProducts.AllowUserToAddRows = false;
            this.dgvTopProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopProducts.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.dgvTopProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(45)))), ((int)(((byte)(62)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(45)))), ((int)(((byte)(62)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTopProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTopProducts.ColumnHeadersHeight = 32;
            this.dgvTopProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colXepHang,
            this.colTenSanPham,
            this.colSoLuongDaBan,
            this.colDoanhThu});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(98)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTopProducts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTopProducts.EnableHeadersVisualStyles = false;
            this.dgvTopProducts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(71)))), ((int)(((byte)(94)))));
            this.dgvTopProducts.Location = new System.Drawing.Point(25, 250);
            this.dgvTopProducts.Name = "dgvTopProducts";
            this.dgvTopProducts.ReadOnly = true;
            this.dgvTopProducts.RowHeadersVisible = false;
            this.dgvTopProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopProducts.Size = new System.Drawing.Size(950, 420);
            this.dgvTopProducts.TabIndex = 3;
            // 
            // colXepHang
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colXepHang.DefaultCellStyle = dataGridViewCellStyle3;
            this.colXepHang.FillWeight = 16F;
            this.colXepHang.HeaderText = "Xếp hạng";
            this.colXepHang.MinimumWidth = 6;
            this.colXepHang.Name = "colXepHang";
            this.colXepHang.ReadOnly = true;
            // 
            // colTenSanPham
            // 
            this.colTenSanPham.FillWeight = 44F;
            this.colTenSanPham.HeaderText = "Tên sản phẩm";
            this.colTenSanPham.MinimumWidth = 6;
            this.colTenSanPham.Name = "colTenSanPham";
            this.colTenSanPham.ReadOnly = true;
            // 
            // colSoLuongDaBan
            // 
            this.colSoLuongDaBan.FillWeight = 20F;
            this.colSoLuongDaBan.HeaderText = "Số lượng đã bán";
            this.colSoLuongDaBan.MinimumWidth = 6;
            this.colSoLuongDaBan.Name = "colSoLuongDaBan";
            this.colSoLuongDaBan.ReadOnly = true;
            // 
            // colDoanhThu
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDoanhThu.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDoanhThu.FillWeight = 20F;
            this.colDoanhThu.HeaderText = "Doanh thu tích lũy";
            this.colDoanhThu.MinimumWidth = 6;
            this.colDoanhThu.Name = "colDoanhThu";
            this.colDoanhThu.ReadOnly = true;
            // 
            // DashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.dgvTopProducts);
            this.Controls.Add(this.lblTableTitle);
            this.Controls.Add(this.panelCards);
            this.Controls.Add(this.panelTopBar);
            this.Name = "DashboardControl";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Size = new System.Drawing.Size(1040, 700);
            this.panelTopBar.ResumeLayout(false);
            this.panelTopBar.PerformLayout();
            this.panelCards.ResumeLayout(false);
            this.card4.ResumeLayout(false);
            this.card3.ResumeLayout(false);
            this.card2.ResumeLayout(false);
            this.card1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
