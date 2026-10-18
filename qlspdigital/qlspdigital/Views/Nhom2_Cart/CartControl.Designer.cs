namespace qlspdigital.Views.Nhom2_Cart
{
    partial class CartControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.colMaGioHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBottomActions = new System.Windows.Forms.Panel();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnDeposit = new System.Windows.Forms.Button();
            this.lblWalletBalance = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.panelVoucher = new System.Windows.Forms.Panel();
            this.btnOpenVouchers = new System.Windows.Forms.Button();
            this.btnApplyVoucher = new System.Windows.Forms.Button();
            this.txtVoucher = new System.Windows.Forms.TextBox();
            this.lblVoucherTitle = new System.Windows.Forms.Label();
            this.lblSumTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.panelBottomActions.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panelVoucher.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(25, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(342, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "GIỎ HÀNG SẢN PHẨM SỐ CỦA BẠN";
            this.lblHeader.UseMnemonic = false;
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCart.ColumnHeadersHeight = 32;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaGioHang,
            this.colTenSanPham,
            this.colDonGia,
            this.colSoLuong,
            this.colThanhTien});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCart.EnableHeadersVisualStyles = false;
            this.dgvCart.GridColor = System.Drawing.Color.FromArgb(42, 71, 94);
            this.dgvCart.Location = new System.Drawing.Point(25, 55);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(640, 550);
            this.dgvCart.TabIndex = 1;
            // 
            // colMaGioHang
            // 
            this.colMaGioHang.HeaderText = "ID";
            this.colMaGioHang.Name = "MaGioHang";
            this.colMaGioHang.ReadOnly = true;
            this.colMaGioHang.Visible = false;
            // 
            // colTenSanPham
            // 
            this.colTenSanPham.FillWeight = 46F;
            this.colTenSanPham.HeaderText = "Tên sản phẩm";
            this.colTenSanPham.MinimumWidth = 150;
            this.colTenSanPham.Name = "TenSanPham";
            this.colTenSanPham.ReadOnly = true;
            // 
            // colDonGia
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDonGia.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDonGia.FillWeight = 22F;
            this.colDonGia.HeaderText = "Đơn giá";
            this.colDonGia.MinimumWidth = 95;
            this.colDonGia.Name = "DonGia";
            this.colDonGia.ReadOnly = true;
            // 
            // colSoLuong
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle4;
            this.colSoLuong.FillWeight = 10F;
            this.colSoLuong.HeaderText = "SL";
            this.colSoLuong.MinimumWidth = 45;
            this.colSoLuong.Name = "SoLuong";
            this.colSoLuong.ReadOnly = true;
            // 
            // colThanhTien
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colThanhTien.DefaultCellStyle = dataGridViewCellStyle5;
            this.colThanhTien.FillWeight = 22F;
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.MinimumWidth = 110;
            this.colThanhTien.Name = "ThanhTien";
            this.colThanhTien.ReadOnly = true;
            // 
            // panelBottomActions
            // 
            this.panelBottomActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.panelBottomActions.Controls.Add(this.btnClearCart);
            this.panelBottomActions.Controls.Add(this.btnRemoveItem);
            this.panelBottomActions.Location = new System.Drawing.Point(25, 618);
            this.panelBottomActions.Name = "panelBottomActions";
            this.panelBottomActions.Size = new System.Drawing.Size(640, 50);
            this.panelBottomActions.TabIndex = 2;
            // 
            // btnClearCart
            // 
            this.btnClearCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(75)))), ((int)(((byte)(90)))));
            this.btnClearCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearCart.FlatAppearance.BorderSize = 0;
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClearCart.ForeColor = System.Drawing.Color.White;
            this.btnClearCart.Location = new System.Drawing.Point(195, 5);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(150, 38);
            this.btnClearCart.TabIndex = 1;
            this.btnClearCart.Text = "Xóa toàn bộ giỏ";
            this.btnClearCart.UseMnemonic = false;
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new System.EventHandler(this.BtnClearCart_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnRemoveItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveItem.FlatAppearance.BorderSize = 0;
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRemoveItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveItem.Location = new System.Drawing.Point(0, 5);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(185, 38);
            this.btnRemoveItem.TabIndex = 0;
            this.btnRemoveItem.Text = "Xóa món đã chọn";
            this.btnRemoveItem.UseMnemonic = false;
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.BtnRemoveItem_Click);
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.panelSummary.Controls.Add(this.btnCheckout);
            this.panelSummary.Controls.Add(this.btnDeposit);
            this.panelSummary.Controls.Add(this.lblWalletBalance);
            this.panelSummary.Controls.Add(this.lblTotal);
            this.panelSummary.Controls.Add(this.lblDiscount);
            this.panelSummary.Controls.Add(this.lblSubtotal);
            this.panelSummary.Controls.Add(this.panelVoucher);
            this.panelSummary.Controls.Add(this.lblSumTitle);
            this.panelSummary.Location = new System.Drawing.Point(685, 55);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Padding = new System.Windows.Forms.Padding(20);
            this.panelSummary.Size = new System.Drawing.Size(330, 606);
            this.panelSummary.TabIndex = 3;
            // 
            // btnCheckout
            // 
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(160)))), ((int)(((byte)(45)))));
            this.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(20, 528);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(290, 58);
            this.btnCheckout.TabIndex = 7;
            this.btnCheckout.Text = "THANH TOÁN 1-CLICK\r\n(Trừ số dư Ví)";
            this.btnCheckout.UseMnemonic = false;
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.BtnCheckout_Click);
            // 
            // btnDeposit
            // 
            this.btnDeposit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(71)))), ((int)(((byte)(94)))));
            this.btnDeposit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeposit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDeposit.FlatAppearance.BorderSize = 0;
            this.btnDeposit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeposit.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDeposit.ForeColor = System.Drawing.Color.White;
            this.btnDeposit.Location = new System.Drawing.Point(20, 283);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.Size = new System.Drawing.Size(290, 36);
            this.btnDeposit.TabIndex = 6;
            this.btnDeposit.Text = "Nạp thêm tiền ví";
            this.btnDeposit.UseMnemonic = false;
            this.btnDeposit.UseVisualStyleBackColor = false;
            this.btnDeposit.Click += new System.EventHandler(this.BtnDeposit_Click);
            // 
            // lblWalletBalance
            // 
            this.lblWalletBalance.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWalletBalance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWalletBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.lblWalletBalance.Location = new System.Drawing.Point(20, 253);
            this.lblWalletBalance.Name = "lblWalletBalance";
            this.lblWalletBalance.Size = new System.Drawing.Size(290, 30);
            this.lblWalletBalance.TabIndex = 5;
            this.lblWalletBalance.Text = "Số dư ví: 0 ₫";
            this.lblWalletBalance.UseMnemonic = false;
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(208)))), ((int)(((byte)(7)))));
            this.lblTotal.Location = new System.Drawing.Point(20, 208);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTotal.Size = new System.Drawing.Size(290, 45);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Tổng thanh toán: 0 ₫";
            this.lblTotal.UseMnemonic = false;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.lblDiscount.Location = new System.Drawing.Point(20, 178);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(290, 30);
            this.lblDiscount.TabIndex = 3;
            this.lblDiscount.Text = "Giảm giá: 0 ₫";
            this.lblDiscount.UseMnemonic = false;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.lblSubtotal.Location = new System.Drawing.Point(20, 148);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(290, 30);
            this.lblSubtotal.TabIndex = 2;
            this.lblSubtotal.Text = "Tạm tính: 0 ₫";
            this.lblSubtotal.UseMnemonic = false;
            // 
            // panelVoucher
            // 
            this.panelVoucher.Controls.Add(this.btnOpenVouchers);
            this.panelVoucher.Controls.Add(this.btnApplyVoucher);
            this.panelVoucher.Controls.Add(this.txtVoucher);
            this.panelVoucher.Controls.Add(this.lblVoucherTitle);
            this.panelVoucher.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelVoucher.Location = new System.Drawing.Point(20, 50);
            this.panelVoucher.Name = "panelVoucher";
            this.panelVoucher.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panelVoucher.Size = new System.Drawing.Size(290, 98);
            this.panelVoucher.TabIndex = 1;
            // 
            // btnOpenVouchers
            // 
            this.btnOpenVouchers.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenVouchers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenVouchers.FlatAppearance.BorderSize = 0;
            this.btnOpenVouchers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenVouchers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.btnOpenVouchers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.btnOpenVouchers.Location = new System.Drawing.Point(0, 64);
            this.btnOpenVouchers.Name = "btnOpenVouchers";
            this.btnOpenVouchers.Size = new System.Drawing.Size(240, 24);
            this.btnOpenVouchers.TabIndex = 3;
            this.btnOpenVouchers.Text = "Xem ví Voucher giảm giá";
            this.btnOpenVouchers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenVouchers.UseMnemonic = false;
            this.btnOpenVouchers.UseVisualStyleBackColor = false;
            this.btnOpenVouchers.Click += new System.EventHandler(this.BtnOpenVouchers_Click);
            // 
            // btnApplyVoucher
            // 
            this.btnApplyVoucher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(98)))), ((int)(((byte)(130)))));
            this.btnApplyVoucher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApplyVoucher.FlatAppearance.BorderSize = 0;
            this.btnApplyVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyVoucher.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnApplyVoucher.ForeColor = System.Drawing.Color.White;
            this.btnApplyVoucher.Location = new System.Drawing.Point(180, 26);
            this.btnApplyVoucher.Name = "btnApplyVoucher";
            this.btnApplyVoucher.Size = new System.Drawing.Size(120, 32);
            this.btnApplyVoucher.TabIndex = 2;
            this.btnApplyVoucher.Text = "Áp dụng";
            this.btnApplyVoucher.UseMnemonic = false;
            this.btnApplyVoucher.UseVisualStyleBackColor = false;
            this.btnApplyVoucher.Click += new System.EventHandler(this.BtnApplyVoucher_Click);
            // 
            // txtVoucher
            // 
            this.txtVoucher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.txtVoucher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucher.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVoucher.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVoucher.ForeColor = System.Drawing.Color.White;
            this.txtVoucher.Location = new System.Drawing.Point(0, 27);
            this.txtVoucher.Name = "txtVoucher";
            this.txtVoucher.PlaceholderText = "Nhập mã...";
            this.txtVoucher.Size = new System.Drawing.Size(190, 25);
            this.txtVoucher.TabIndex = 1;
            this.txtVoucher.TextChanged += new System.EventHandler(this.TxtVoucher_TextChanged);
            // 
            // lblVoucherTitle
            // 
            this.lblVoucherTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVoucherTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVoucherTitle.ForeColor = System.Drawing.Color.White;
            this.lblVoucherTitle.Location = new System.Drawing.Point(0, 10);
            this.lblVoucherTitle.Name = "lblVoucherTitle";
            this.lblVoucherTitle.Size = new System.Drawing.Size(290, 22);
            this.lblVoucherTitle.TabIndex = 0;
            this.lblVoucherTitle.Text = "Mã ưu đãi (Voucher):";
            this.lblVoucherTitle.UseMnemonic = false;
            // 
            // lblSumTitle
            // 
            this.lblSumTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSumTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblSumTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.lblSumTitle.Location = new System.Drawing.Point(20, 20);
            this.lblSumTitle.Name = "lblSumTitle";
            this.lblSumTitle.Size = new System.Drawing.Size(290, 30);
            this.lblSumTitle.TabIndex = 0;
            this.lblSumTitle.Text = "TỔNG KẾT ĐƠN HÀNG";
            this.lblSumTitle.UseMnemonic = false;
            // 
            // CartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.panelBottomActions);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.lblHeader);
            this.Name = "CartControl";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Size = new System.Drawing.Size(1040, 700);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.panelBottomActions.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelVoucher.ResumeLayout(false);
            this.panelVoucher.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaGioHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.Panel panelBottomActions;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblSumTitle;
        private System.Windows.Forms.Panel panelVoucher;
        private System.Windows.Forms.Label lblVoucherTitle;
        private System.Windows.Forms.TextBox txtVoucher;
        private System.Windows.Forms.Button btnApplyVoucher;
        private System.Windows.Forms.Button btnOpenVouchers;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblWalletBalance;
        private System.Windows.Forms.Button btnDeposit;
        private System.Windows.Forms.Button btnCheckout;
    }
}
