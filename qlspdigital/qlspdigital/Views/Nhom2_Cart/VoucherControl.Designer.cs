using System.Drawing;
using System.Windows.Forms;

namespace qlspdigital.Views.Nhom2_Cart
{
    partial class VoucherControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelCollect = new System.Windows.Forms.Panel();
            this.lblCodeTitle = new System.Windows.Forms.Label();
            this.txtVoucherCode = new System.Windows.Forms.TextBox();
            this.btnCollect = new System.Windows.Forms.Button();
            this.btnGoToCart = new System.Windows.Forms.Button();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.dgvVouchers = new System.Windows.Forms.DataGridView();
            this.colMaGiamGiaId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTieuDe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaTri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonToiThieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHanDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBottomActions = new System.Windows.Forms.Panel();
            this.btnCollectSelected = new System.Windows.Forms.Button();
            this.btnApplySelected = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblVoucherGuide = new System.Windows.Forms.Label();
            this.panelCollect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVouchers)).BeginInit();
            this.panelBottomActions.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(25, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(437, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "KHO MÃ ƯU ĐÃI & VOUCHER GIẢM GIÁ";
            this.lblHeader.UseMnemonic = false;

            // 
            // panelCollect
            // 
            this.panelCollect.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelCollect.Controls.Add(this.btnGoToCart);
            this.panelCollect.Controls.Add(this.btnCollect);
            this.panelCollect.Controls.Add(this.txtVoucherCode);
            this.panelCollect.Controls.Add(this.lblCodeTitle);
            this.panelCollect.Location = new System.Drawing.Point(25, 55);
            this.panelCollect.Name = "panelCollect";
            this.panelCollect.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelCollect.Size = new System.Drawing.Size(950, 85);
            this.panelCollect.TabIndex = 1;

            // 
            // lblCodeTitle
            // 
            this.lblCodeTitle.AutoSize = true;
            this.lblCodeTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCodeTitle.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.lblCodeTitle.Location = new System.Drawing.Point(20, 14);
            this.lblCodeTitle.Name = "lblCodeTitle";
            this.lblCodeTitle.Size = new System.Drawing.Size(480, 17);
            this.lblCodeTitle.TabIndex = 0;
            this.lblCodeTitle.Text = "Nhập mã khuyến mãi nhận quà (VD: WELCOME2026, SUMMERSALE):";
            this.lblCodeTitle.UseMnemonic = false;

            // 
            // txtVoucherCode
            // 
            this.txtVoucherCode.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtVoucherCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucherCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVoucherCode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtVoucherCode.ForeColor = System.Drawing.Color.White;
            this.txtVoucherCode.Location = new System.Drawing.Point(20, 42);
            this.txtVoucherCode.Name = "txtVoucherCode";
            this.txtVoucherCode.PlaceholderText = "Nhập mã code...";
            this.txtVoucherCode.Size = new System.Drawing.Size(300, 27);
            this.txtVoucherCode.TabIndex = 1;

            // 
            // btnCollect
            // 
            this.btnCollect.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnCollect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCollect.FlatAppearance.BorderSize = 0;
            this.btnCollect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCollect.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCollect.ForeColor = System.Drawing.Color.White;
            this.btnCollect.Location = new System.Drawing.Point(335, 38);
            this.btnCollect.Name = "btnCollect";
            this.btnCollect.Size = new System.Drawing.Size(160, 34);
            this.btnCollect.TabIndex = 2;
            this.btnCollect.Text = "Thu thập mã";
            this.btnCollect.UseMnemonic = false;
            this.btnCollect.UseVisualStyleBackColor = false;
            this.btnCollect.Click += new System.EventHandler(this.BtnCollect_Click);

            // 
            // btnGoToCart
            // 
            this.btnGoToCart.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnGoToCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoToCart.FlatAppearance.BorderSize = 0;
            this.btnGoToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToCart.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGoToCart.ForeColor = System.Drawing.Color.White;
            this.btnGoToCart.Location = new System.Drawing.Point(510, 38);
            this.btnGoToCart.Name = "btnGoToCart";
            this.btnGoToCart.Size = new System.Drawing.Size(310, 34);
            this.btnGoToCart.TabIndex = 3;
            this.btnGoToCart.Text = "Đến giỏ hàng để mua sắm ngay";
            this.btnGoToCart.UseMnemonic = false;
            this.btnGoToCart.UseVisualStyleBackColor = false;
            this.btnGoToCart.Click += new System.EventHandler(this.BtnGoToCart_Click);

            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblListTitle.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblListTitle.Location = new System.Drawing.Point(25, 155);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(437, 20);
            this.lblListTitle.TabIndex = 2;
            this.lblListTitle.Text = "DANH SÁCH MÃ GIẢM GIÁ KHẢ DỤNG TRÊN HỆ THỐNG";
            this.lblListTitle.UseMnemonic = false;

            // 
            // dgvVouchers
            // 
            this.dgvVouchers.AllowUserToAddRows = false;
            this.dgvVouchers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVouchers.BackgroundColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.dgvVouchers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVouchers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVouchers.ColumnHeadersHeight = 32;
            this.dgvVouchers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaGiamGiaId,
            this.colMaCode,
            this.colTieuDe,
            this.colGiaTri,
            this.colDonToiThieu,
            this.colHanDung,
            this.colTrangThai});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVouchers.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVouchers.EnableHeadersVisualStyles = false;
            this.dgvVouchers.GridColor = System.Drawing.Color.FromArgb(42, 71, 94);
            this.dgvVouchers.Location = new System.Drawing.Point(25, 185);
            this.dgvVouchers.Name = "dgvVouchers";
            this.dgvVouchers.ReadOnly = true;
            this.dgvVouchers.RowHeadersVisible = false;
            this.dgvVouchers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVouchers.Size = new System.Drawing.Size(950, 435);
            this.dgvVouchers.TabIndex = 3;
            this.dgvVouchers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVouchers_CellDoubleClick);
            this.dgvVouchers.SelectionChanged += new System.EventHandler(this.DgvVouchers_SelectionChanged);
            // 
            // colMaGiamGiaId
            // 
            this.colMaGiamGiaId.HeaderText = "ID";
            this.colMaGiamGiaId.Name = "MaGiamGiaId";
            this.colMaGiamGiaId.ReadOnly = true;
            this.colMaGiamGiaId.Visible = false;
            // 
            // colMaCode
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colMaCode.DefaultCellStyle = dataGridViewCellStyle3;
            this.colMaCode.FillWeight = 15F;
            this.colMaCode.HeaderText = "Mã Code";
            this.colMaCode.MinimumWidth = 110;
            this.colMaCode.Name = "MaCode";
            this.colMaCode.ReadOnly = true;
            // 
            // colTieuDe
            // 
            this.colTieuDe.FillWeight = 33F;
            this.colTieuDe.HeaderText = "Chương trình ưu đãi";
            this.colTieuDe.MinimumWidth = 160;
            this.colTieuDe.Name = "TieuDe";
            this.colTieuDe.ReadOnly = true;
            // 
            // colGiaTri
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colGiaTri.DefaultCellStyle = dataGridViewCellStyle4;
            this.colGiaTri.FillWeight = 13F;
            this.colGiaTri.HeaderText = "Mức giảm";
            this.colGiaTri.MinimumWidth = 95;
            this.colGiaTri.Name = "GiaTri";
            this.colGiaTri.ReadOnly = true;
            // 
            // colDonToiThieu
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDonToiThieu.DefaultCellStyle = dataGridViewCellStyle5;
            this.colDonToiThieu.FillWeight = 13F;
            this.colDonToiThieu.HeaderText = "Đơn tối thiểu";
            this.colDonToiThieu.MinimumWidth = 100;
            this.colDonToiThieu.Name = "DonToiThieu";
            this.colDonToiThieu.ReadOnly = true;
            // 
            // colHanDung
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colHanDung.DefaultCellStyle = dataGridViewCellStyle6;
            this.colHanDung.FillWeight = 13F;
            this.colHanDung.HeaderText = "Hạn sử dụng";
            this.colHanDung.MinimumWidth = 95;
            this.colHanDung.Name = "HanDung";
            this.colHanDung.ReadOnly = true;
            // 
            // colTrangThai
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colTrangThai.DefaultCellStyle = dataGridViewCellStyle7;
            this.colTrangThai.FillWeight = 13F;
            this.colTrangThai.HeaderText = "Trạng thái kho";
            this.colTrangThai.MinimumWidth = 95;
            this.colTrangThai.Name = "TrangThai";
            this.colTrangThai.ReadOnly = true;

            // 
            // panelBottomActions
            // 
            this.panelBottomActions.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.panelBottomActions.Controls.Add(this.lblVoucherGuide);
            this.panelBottomActions.Controls.Add(this.btnDelete);
            this.panelBottomActions.Controls.Add(this.btnApplySelected);
            this.panelBottomActions.Controls.Add(this.btnCollectSelected);
            this.panelBottomActions.Location = new System.Drawing.Point(25, 630);
            this.panelBottomActions.Name = "panelBottomActions";
            this.panelBottomActions.Size = new System.Drawing.Size(950, 50);
            this.panelBottomActions.TabIndex = 4;

            // 
            // btnCollectSelected
            // 
            this.btnCollectSelected.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnCollectSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCollectSelected.FlatAppearance.BorderSize = 0;
            this.btnCollectSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCollectSelected.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCollectSelected.ForeColor = System.Drawing.Color.White;
            this.btnCollectSelected.Location = new System.Drawing.Point(0, 4);
            this.btnCollectSelected.Name = "btnCollectSelected";
            this.btnCollectSelected.Size = new System.Drawing.Size(200, 40);
            this.btnCollectSelected.TabIndex = 0;
            this.btnCollectSelected.Text = "Thu thập mã vào ví";
            this.btnCollectSelected.UseMnemonic = false;
            this.btnCollectSelected.UseVisualStyleBackColor = false;
            this.btnCollectSelected.Click += new System.EventHandler(this.BtnCollectSelected_Click);

            // 
            // btnApplySelected
            // 
            this.btnApplySelected.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnApplySelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApplySelected.FlatAppearance.BorderSize = 0;
            this.btnApplySelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplySelected.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnApplySelected.ForeColor = System.Drawing.Color.White;
            this.btnApplySelected.Location = new System.Drawing.Point(210, 4);
            this.btnApplySelected.Name = "btnApplySelected";
            this.btnApplySelected.Size = new System.Drawing.Size(230, 40);
            this.btnApplySelected.TabIndex = 1;
            this.btnApplySelected.Text = "Dùng ngay (Áp dụng vào giỏ)";
            this.btnApplySelected.UseMnemonic = false;
            this.btnApplySelected.UseVisualStyleBackColor = false;
            this.btnApplySelected.Click += new System.EventHandler(this.BtnApplySelected_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(450, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 40);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa khỏi ví";
            this.btnDelete.UseMnemonic = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // 
            // lblVoucherGuide
            // 
            this.lblVoucherGuide.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblVoucherGuide.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.lblVoucherGuide.Location = new System.Drawing.Point(600, 8);
            this.lblVoucherGuide.Name = "lblVoucherGuide";
            this.lblVoucherGuide.Size = new System.Drawing.Size(350, 36);
            this.lblVoucherGuide.TabIndex = 3;
            this.lblVoucherGuide.Text = "Lưu ý: Chỉ mã 'Đã trong ví' mới có thể áp dụng tại Giỏ hàng. Mã 'Chưa lưu' vui lòng nhấn 'Thu thập'!";
            this.lblVoucherGuide.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVoucherGuide.UseMnemonic = false;

            // 
            // VoucherControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.Controls.Add(this.panelBottomActions);
            this.Controls.Add(this.dgvVouchers);
            this.Controls.Add(this.lblListTitle);
            this.Controls.Add(this.panelCollect);
            this.Controls.Add(this.lblHeader);
            this.Name = "VoucherControl";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Size = new System.Drawing.Size(1040, 700);
            this.panelCollect.ResumeLayout(false);
            this.panelCollect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVouchers)).EndInit();
            this.panelBottomActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelCollect;
        private System.Windows.Forms.Label lblCodeTitle;
        private System.Windows.Forms.TextBox txtVoucherCode;
        private System.Windows.Forms.Button btnCollect;
        private System.Windows.Forms.Button btnGoToCart;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.DataGridView dgvVouchers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaGiamGiaId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTieuDe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaTri;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonToiThieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHanDung;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.Panel panelBottomActions;
        private System.Windows.Forms.Button btnCollectSelected;
        private System.Windows.Forms.Button btnApplySelected;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblVoucherGuide;
    }
}
