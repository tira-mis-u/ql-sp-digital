using System.Drawing;
using System.Windows.Forms;

namespace qlspdigital.Views.Nhom2_Cart
{
    partial class WalletControl
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelCard = new System.Windows.Forms.Panel();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.panelDeposit = new System.Windows.Forms.Panel();
            this.lblDepositTitle = new System.Windows.Forms.Label();
            this.btnTopUp50k = new System.Windows.Forms.Button();
            this.btnTopUp100k = new System.Windows.Forms.Button();
            this.btnTopUp200k = new System.Windows.Forms.Button();
            this.btnTopUp500k = new System.Windows.Forms.Button();
            this.txtCustomAmount = new System.Windows.Forms.TextBox();
            this.btnCustomTopUp = new System.Windows.Forms.Button();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.colMaThanhToan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiGiaoDich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhuongThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelCard.SuspendLayout();
            this.panelDeposit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.SuspendLayout();

            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(25, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(564, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "VÍ TÀI KHOẢN DPMS (STEAM WALLET SIMULATION)";
            this.lblHeader.UseMnemonic = false;

            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelCard.Controls.Add(this.lblNote);
            this.panelCard.Controls.Add(this.lblBalance);
            this.panelCard.Controls.Add(this.lblCardTitle);
            this.panelCard.Location = new System.Drawing.Point(25, 55);
            this.panelCard.Name = "panelCard";
            this.panelCard.Padding = new System.Windows.Forms.Padding(20);
            this.panelCard.Size = new System.Drawing.Size(990, 100);
            this.panelCard.TabIndex = 1;

            // 
            // lblCardTitle
            // 
            this.lblCardTitle.AutoSize = true;
            this.lblCardTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCardTitle.ForeColor = System.Drawing.Color.FromArgb(161, 181, 196);
            this.lblCardTitle.Location = new System.Drawing.Point(20, 15);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(193, 19);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "SỐ DƯ KHẢ DỤNG HIỆN TẠI:";
            this.lblCardTitle.UseMnemonic = false;

            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblBalance.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblBalance.Location = new System.Drawing.Point(20, 40);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(65, 45);
            this.lblBalance.TabIndex = 1;
            this.lblBalance.Text = "0 ₫";
            this.lblBalance.UseMnemonic = false;

            // 
            // lblNote
            // 
            this.lblNote.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.lblNote.Location = new System.Drawing.Point(530, 20);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(400, 60);
            this.lblNote.TabIndex = 2;
            this.lblNote.Text = "Số dư ví dùng để thanh toán 1-Click tức thì mọi trò chơi và tài nguyên số trên hệ thống.";
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNote.UseMnemonic = false;

            // 
            // panelDeposit
            // 
            this.panelDeposit.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelDeposit.Controls.Add(this.btnCustomTopUp);
            this.panelDeposit.Controls.Add(this.txtCustomAmount);
            this.panelDeposit.Controls.Add(this.btnTopUp500k);
            this.panelDeposit.Controls.Add(this.btnTopUp200k);
            this.panelDeposit.Controls.Add(this.btnTopUp100k);
            this.panelDeposit.Controls.Add(this.btnTopUp50k);
            this.panelDeposit.Controls.Add(this.lblDepositTitle);
            this.panelDeposit.Location = new System.Drawing.Point(25, 170);
            this.panelDeposit.Name = "panelDeposit";
            this.panelDeposit.Padding = new System.Windows.Forms.Padding(25, 18, 25, 18);
            this.panelDeposit.Size = new System.Drawing.Size(990, 125);
            this.panelDeposit.TabIndex = 2;

            // 
            // lblDepositTitle
            // 
            this.lblDepositTitle.AutoSize = true;
            this.lblDepositTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblDepositTitle.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblDepositTitle.Location = new System.Drawing.Point(25, 18);
            this.lblDepositTitle.Name = "lblDepositTitle";
            this.lblDepositTitle.Size = new System.Drawing.Size(512, 19);
            this.lblDepositTitle.TabIndex = 0;
            this.lblDepositTitle.Text = "NẠP TIỀN VÀO VÍ (CHỌN MỆNH GIÁ MÔ PHỎNG HOẶC NHẬP TÙY Ý)";
            this.lblDepositTitle.UseMnemonic = false;

            // 
            // btnTopUp50k
            // 
            this.btnTopUp50k.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnTopUp50k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTopUp50k.FlatAppearance.BorderSize = 0;
            this.btnTopUp50k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopUp50k.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTopUp50k.ForeColor = System.Drawing.Color.White;
            this.btnTopUp50k.Location = new System.Drawing.Point(25, 62);
            this.btnTopUp50k.Name = "btnTopUp50k";
            this.btnTopUp50k.Size = new System.Drawing.Size(130, 40);
            this.btnTopUp50k.TabIndex = 1;
            this.btnTopUp50k.Text = "+ 50.000 đ";
            this.btnTopUp50k.UseVisualStyleBackColor = false;
            this.btnTopUp50k.Click += new System.EventHandler(this.BtnTopUp50k_Click);

            // 
            // btnTopUp100k
            // 
            this.btnTopUp100k.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnTopUp100k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTopUp100k.FlatAppearance.BorderSize = 0;
            this.btnTopUp100k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopUp100k.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTopUp100k.ForeColor = System.Drawing.Color.White;
            this.btnTopUp100k.Location = new System.Drawing.Point(167, 62);
            this.btnTopUp100k.Name = "btnTopUp100k";
            this.btnTopUp100k.Size = new System.Drawing.Size(130, 40);
            this.btnTopUp100k.TabIndex = 2;
            this.btnTopUp100k.Text = "+ 100.000 đ";
            this.btnTopUp100k.UseVisualStyleBackColor = false;
            this.btnTopUp100k.Click += new System.EventHandler(this.BtnTopUp100k_Click);

            // 
            // btnTopUp200k
            // 
            this.btnTopUp200k.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnTopUp200k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTopUp200k.FlatAppearance.BorderSize = 0;
            this.btnTopUp200k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopUp200k.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTopUp200k.ForeColor = System.Drawing.Color.White;
            this.btnTopUp200k.Location = new System.Drawing.Point(309, 62);
            this.btnTopUp200k.Name = "btnTopUp200k";
            this.btnTopUp200k.Size = new System.Drawing.Size(130, 40);
            this.btnTopUp200k.TabIndex = 3;
            this.btnTopUp200k.Text = "+ 200.000 đ";
            this.btnTopUp200k.UseVisualStyleBackColor = false;
            this.btnTopUp200k.Click += new System.EventHandler(this.BtnTopUp200k_Click);

            // 
            // btnTopUp500k
            // 
            this.btnTopUp500k.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnTopUp500k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTopUp500k.FlatAppearance.BorderSize = 0;
            this.btnTopUp500k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopUp500k.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTopUp500k.ForeColor = System.Drawing.Color.White;
            this.btnTopUp500k.Location = new System.Drawing.Point(451, 62);
            this.btnTopUp500k.Name = "btnTopUp500k";
            this.btnTopUp500k.Size = new System.Drawing.Size(130, 40);
            this.btnTopUp500k.TabIndex = 4;
            this.btnTopUp500k.Text = "+ 500.000 đ";
            this.btnTopUp500k.UseVisualStyleBackColor = false;
            this.btnTopUp500k.Click += new System.EventHandler(this.BtnTopUp500k_Click);

            // 
            // txtCustomAmount
            // 
            this.txtCustomAmount.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtCustomAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomAmount.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtCustomAmount.ForeColor = System.Drawing.Color.White;
            this.txtCustomAmount.Location = new System.Drawing.Point(605, 62);
            this.txtCustomAmount.Name = "txtCustomAmount";
            this.txtCustomAmount.PlaceholderText = "Nhập số tiền...";
            this.txtCustomAmount.Size = new System.Drawing.Size(180, 26);
            this.txtCustomAmount.TabIndex = 5;

            // 
            // btnCustomTopUp
            // 
            this.btnCustomTopUp.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnCustomTopUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomTopUp.FlatAppearance.BorderSize = 0;
            this.btnCustomTopUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomTopUp.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCustomTopUp.ForeColor = System.Drawing.Color.White;
            this.btnCustomTopUp.Location = new System.Drawing.Point(797, 62);
            this.btnCustomTopUp.Name = "btnCustomTopUp";
            this.btnCustomTopUp.Size = new System.Drawing.Size(130, 40);
            this.btnCustomTopUp.TabIndex = 6;
            this.btnCustomTopUp.Text = "Nạp số này";
            this.btnCustomTopUp.UseMnemonic = false;
            this.btnCustomTopUp.UseVisualStyleBackColor = false;
            this.btnCustomTopUp.Click += new System.EventHandler(this.BtnCustomTopUp_Click);

            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistoryTitle.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblHistoryTitle.Location = new System.Drawing.Point(25, 305);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(306, 20);
            this.lblHistoryTitle.TabIndex = 3;
            this.lblHistoryTitle.Text = "LỊCH SỬ BIẾN ĐỘNG SỐ DƯ & GIAO DỊCH";
            this.lblHistoryTitle.UseMnemonic = false;

            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransactions.ColumnHeadersHeight = 32;
            this.dgvTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaThanhToan,
            this.colLoaiGiaoDich,
            this.colSoTien,
            this.colPhuongThuc,
            this.colTrangThai,
            this.colNgayTao});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTransactions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTransactions.EnableHeadersVisualStyles = false;
            this.dgvTransactions.GridColor = System.Drawing.Color.FromArgb(42, 71, 94);
            this.dgvTransactions.Location = new System.Drawing.Point(25, 340);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersVisible = false;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new System.Drawing.Size(990, 335);
            this.dgvTransactions.TabIndex = 4;
            // 
            // colMaThanhToan
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colMaThanhToan.DefaultCellStyle = dataGridViewCellStyle3;
            this.colMaThanhToan.FillWeight = 10F;
            this.colMaThanhToan.HeaderText = "Mã GD";
            this.colMaThanhToan.MinimumWidth = 65;
            this.colMaThanhToan.Name = "MaThanhToan";
            this.colMaThanhToan.ReadOnly = true;
            // 
            // colLoaiGiaoDich
            // 
            this.colLoaiGiaoDich.FillWeight = 24F;
            this.colLoaiGiaoDich.HeaderText = "Loại giao dịch";
            this.colLoaiGiaoDich.MinimumWidth = 110;
            this.colLoaiGiaoDich.Name = "LoaiGiaoDich";
            this.colLoaiGiaoDich.ReadOnly = true;
            // 
            // colSoTien
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSoTien.DefaultCellStyle = dataGridViewCellStyle4;
            this.colSoTien.FillWeight = 18F;
            this.colSoTien.HeaderText = "Số tiền";
            this.colSoTien.MinimumWidth = 100;
            this.colSoTien.Name = "SoTien";
            this.colSoTien.ReadOnly = true;
            // 
            // colPhuongThuc
            // 
            this.colPhuongThuc.FillWeight = 20F;
            this.colPhuongThuc.HeaderText = "Phương thức";
            this.colPhuongThuc.MinimumWidth = 100;
            this.colPhuongThuc.Name = "PhuongThuc";
            this.colPhuongThuc.ReadOnly = true;
            // 
            // colTrangThai
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colTrangThai.DefaultCellStyle = dataGridViewCellStyle5;
            this.colTrangThai.FillWeight = 14F;
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 85;
            this.colTrangThai.Name = "TrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // colNgayTao
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNgayTao.DefaultCellStyle = dataGridViewCellStyle6;
            this.colNgayTao.FillWeight = 14F;
            this.colNgayTao.HeaderText = "Thời gian";
            this.colNgayTao.MinimumWidth = 110;
            this.colNgayTao.Name = "NgayTao";
            this.colNgayTao.ReadOnly = true;

            // 
            // WalletControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.Controls.Add(this.dgvTransactions);
            this.Controls.Add(this.lblHistoryTitle);
            this.Controls.Add(this.panelDeposit);
            this.Controls.Add(this.panelCard);
            this.Controls.Add(this.lblHeader);
            this.Name = "WalletControl";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Size = new System.Drawing.Size(1040, 700);
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            this.panelDeposit.ResumeLayout(false);
            this.panelDeposit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.AdjustLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Panel panelDeposit;
        private System.Windows.Forms.Label lblDepositTitle;
        private System.Windows.Forms.Button btnTopUp50k;
        private System.Windows.Forms.Button btnTopUp100k;
        private System.Windows.Forms.Button btnTopUp200k;
        private System.Windows.Forms.Button btnTopUp500k;
        private System.Windows.Forms.TextBox txtCustomAmount;
        private System.Windows.Forms.Button btnCustomTopUp;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaThanhToan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiGiaoDich;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhuongThuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTao;

        #region Windows Form Designer - Layout & UI Helpers
#nullable enable

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public qlspdigital.Views.Main.MainForm? MainForm { get; set; }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (panelCard == null || panelDeposit == null || dgvTransactions == null || lblDepositTitle == null || lblHistoryTitle == null) return;
            int totalW = System.Math.Max(600, this.ClientSize.Width);
            int totalH = System.Math.Max(400, this.ClientSize.Height);
            int fullW = totalW - 50;

            panelCard.Location = new System.Drawing.Point(25, 55);
            panelCard.Size = new System.Drawing.Size(fullW, 100);

            panelDeposit.Location = new System.Drawing.Point(25, 168);

            lblDepositTitle.Location = new System.Drawing.Point(25, 18);
            int btnY = lblDepositTitle.Bottom + 14;
            int btnH = 40;
            btnTopUp50k.Height = btnH;
            btnTopUp100k.Height = btnH;
            btnTopUp200k.Height = btnH;
            btnTopUp500k.Height = btnH;
            txtCustomAmount.Height = btnH;
            btnCustomTopUp.Height = btnH;

            int pnlW = panelDeposit.ClientSize.Width;
            if (btnCustomTopUp != null && txtCustomAmount != null && btnTopUp50k != null)
            {
                int gap = 10;
                int customBtnW = 125;
                // Ô nhập số tiền rộng rãi, thoải mái nhập liệu (260px - 360px)
                int customTxtW = System.Math.Clamp((int)(pnlW * 0.28), 260, 360);
                int fixedPresetSpace = pnlW - 50 - customTxtW - customBtnW - gap * 5;
                int btnW = System.Math.Clamp(fixedPresetSpace / 4, 85, 125);

                btnTopUp50k.Location = new System.Drawing.Point(25, btnY);
                btnTopUp50k.Width = btnW;

                btnTopUp100k.Location = new System.Drawing.Point(btnTopUp50k.Right + gap, btnY);
                btnTopUp100k.Width = btnW;

                btnTopUp200k.Location = new System.Drawing.Point(btnTopUp100k.Right + gap, btnY);
                btnTopUp200k.Width = btnW;

                btnTopUp500k.Location = new System.Drawing.Point(btnTopUp200k.Right + gap, btnY);
                btnTopUp500k.Width = btnW;

                txtCustomAmount.Location = new System.Drawing.Point(btnTopUp500k.Right + gap + 6, btnY);
                txtCustomAmount.Width = customTxtW;

                btnCustomTopUp.Location = new System.Drawing.Point(txtCustomAmount.Right + gap, btnY);
                btnCustomTopUp.Width = customBtnW;
            }

            panelDeposit.Size = new System.Drawing.Size(fullW, txtCustomAmount.Bottom + 16);

            lblHistoryTitle.Location = new System.Drawing.Point(25, panelDeposit.Bottom + 16);
            int dgvY = lblHistoryTitle.Bottom + 10;
            int contentH = System.Math.Max(150, totalH - dgvY - 25);

            dgvTransactions.Location = new System.Drawing.Point(25, dgvY);
            dgvTransactions.Size = new System.Drawing.Size(fullW, contentH);
        }

#nullable restore
        #endregion
    }
}
