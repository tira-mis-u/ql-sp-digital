namespace qlspdigital.Views.Nhom1_Store
{
    partial class ProductReviewControl
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblAverageScore = new System.Windows.Forms.Label();
            this.cboProduct = new System.Windows.Forms.ComboBox();
            this.lblSelect = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblCommentTitle = new System.Windows.Forms.Label();
            this.cboStar = new System.Windows.Forms.ComboBox();
            this.lblStarTitle = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.dgvReviews = new System.Windows.Forms.DataGridView();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoSao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBinhLuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop.SuspendLayout();
            this.panelForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.panelTop.Controls.Add(this.lblAverageScore);
            this.panelTop.Controls.Add(this.cboProduct);
            this.panelTop.Controls.Add(this.lblSelect);
            this.panelTop.Location = new System.Drawing.Point(25, 15);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.panelTop.Size = new System.Drawing.Size(990, 56);
            this.panelTop.TabIndex = 0;
            // 
            // lblAverageScore
            // 
            this.lblAverageScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAverageScore.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblAverageScore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.lblAverageScore.Location = new System.Drawing.Point(620, 11);
            this.lblAverageScore.Name = "lblAverageScore";
            this.lblAverageScore.Size = new System.Drawing.Size(350, 34);
            this.lblAverageScore.TabIndex = 2;
            this.lblAverageScore.Text = "Điểm TB: ⭐ 5.0 / 5.0";
            this.lblAverageScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAverageScore.UseMnemonic = false;
            // 
            // cboProduct
            // 
            this.cboProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.cboProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboProduct.ForeColor = System.Drawing.Color.White;
            this.cboProduct.Location = new System.Drawing.Point(275, 12);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.Size = new System.Drawing.Size(280, 25);
            this.cboProduct.TabIndex = 1;
            this.cboProduct.SelectedIndexChanged += new System.EventHandler(this.CboProduct_SelectedIndexChanged);
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSelect.ForeColor = System.Drawing.Color.White;
            this.lblSelect.Location = new System.Drawing.Point(15, 15);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(206, 19);
            this.lblSelect.TabIndex = 0;
            this.lblSelect.Text = "Chọn sản phẩm xem đánh giá:";
            this.lblSelect.UseMnemonic = false;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.panelForm.Controls.Add(this.btnDelete);
            this.panelForm.Controls.Add(this.btnSubmit);
            this.panelForm.Controls.Add(this.txtComment);
            this.panelForm.Controls.Add(this.lblCommentTitle);
            this.panelForm.Controls.Add(this.cboStar);
            this.panelForm.Controls.Add(this.lblStarTitle);
            this.panelForm.Controls.Add(this.lblFormTitle);
            this.panelForm.Location = new System.Drawing.Point(25, 80);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(15);
            this.panelForm.Size = new System.Drawing.Size(990, 195);
            this.panelForm.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(800, 132);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(160, 38);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Xóa nhận xét";
            this.btnDelete.UseMnemonic = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(160)))), ((int)(((byte)(45)))));
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(800, 88);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(160, 38);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "Gửi nhận xét";
            this.btnSubmit.UseMnemonic = false;
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // txtComment
            // 
            this.txtComment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtComment.ForeColor = System.Drawing.Color.White;
            this.txtComment.Location = new System.Drawing.Point(165, 88);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(560, 80);
            this.txtComment.TabIndex = 4;
            // 
            // lblCommentTitle
            // 
            this.lblCommentTitle.AutoSize = true;
            this.lblCommentTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCommentTitle.ForeColor = System.Drawing.Color.White;
            this.lblCommentTitle.Location = new System.Drawing.Point(15, 92);
            this.lblCommentTitle.Name = "lblCommentTitle";
            this.lblCommentTitle.Size = new System.Drawing.Size(117, 17);
            this.lblCommentTitle.TabIndex = 3;
            this.lblCommentTitle.Text = "Nội dung nhận xét:";
            this.lblCommentTitle.UseMnemonic = false;
            // 
            // cboStar
            // 
            this.cboStar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.cboStar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboStar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.cboStar.Items.AddRange(new object[] {
            "⭐⭐⭐⭐⭐ (5/5) - Rất tích cực / Cực tốt",
            "⭐⭐⭐⭐ (4/5) - Hài lòng",
            "⭐⭐⭐ (3/5) - Bình thường",
            "⭐⭐ (2/5) - Kém",
            "⭐ (1/5) - Rất tệ / Không khuyến nghị"});
            this.cboStar.Location = new System.Drawing.Point(165, 45);
            this.cboStar.Name = "cboStar";
            this.cboStar.Size = new System.Drawing.Size(310, 23);
            this.cboStar.TabIndex = 2;
            // 
            // lblStarTitle
            // 
            this.lblStarTitle.AutoSize = true;
            this.lblStarTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStarTitle.ForeColor = System.Drawing.Color.White;
            this.lblStarTitle.Location = new System.Drawing.Point(15, 48);
            this.lblStarTitle.Name = "lblStarTitle";
            this.lblStarTitle.Size = new System.Drawing.Size(107, 17);
            this.lblStarTitle.TabIndex = 1;
            this.lblStarTitle.Text = "Mức độ hài lòng:";
            this.lblStarTitle.UseMnemonic = false;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.lblFormTitle.Location = new System.Drawing.Point(15, 14);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(306, 19);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "VIẾT HOẶC CHỈNH SỬA ĐÁNH GIÁ CỦA BẠN";
            this.lblFormTitle.UseMnemonic = false;
            // 
            // dgvReviews
            // 
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReviews.BackgroundColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.dgvReviews.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReviews.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReviews.ColumnHeadersHeight = 32;
            this.dgvReviews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHoTen,
            this.colSoSao,
            this.colBinhLuan,
            this.colNgayTao});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReviews.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReviews.EnableHeadersVisualStyles = false;
            this.dgvReviews.GridColor = System.Drawing.Color.FromArgb(42, 71, 94);
            this.dgvReviews.Location = new System.Drawing.Point(25, 285);
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.RowHeadersVisible = false;
            this.dgvReviews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReviews.Size = new System.Drawing.Size(990, 390);
            this.dgvReviews.TabIndex = 2;
            // 
            // colHoTen
            // 
            this.colHoTen.FillWeight = 25F;
            this.colHoTen.HeaderText = "Người đánh giá";
            this.colHoTen.MinimumWidth = 120;
            this.colHoTen.Name = "HoTen";
            this.colHoTen.ReadOnly = true;
            // 
            // colSoSao
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSoSao.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSoSao.FillWeight = 12F;
            this.colSoSao.HeaderText = "Số sao";
            this.colSoSao.MinimumWidth = 70;
            this.colSoSao.Name = "SoSao";
            this.colSoSao.ReadOnly = true;
            // 
            // colBinhLuan
            // 
            this.colBinhLuan.FillWeight = 45F;
            this.colBinhLuan.HeaderText = "Nhận xét";
            this.colBinhLuan.MinimumWidth = 150;
            this.colBinhLuan.Name = "BinhLuan";
            this.colBinhLuan.ReadOnly = true;
            // 
            // colNgayTao
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNgayTao.DefaultCellStyle = dataGridViewCellStyle4;
            this.colNgayTao.FillWeight = 18F;
            this.colNgayTao.HeaderText = "Thời gian";
            this.colNgayTao.MinimumWidth = 110;
            this.colNgayTao.Name = "NgayTao";
            this.colNgayTao.ReadOnly = true;
            // 
            // ProductReviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.dgvReviews);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelTop);
            this.Name = "ProductReviewControl";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Size = new System.Drawing.Size(1040, 700);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.AdjustLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.ComboBox cboProduct;
        private System.Windows.Forms.Label lblAverageScore;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblStarTitle;
        private System.Windows.Forms.ComboBox cboStar;
        private System.Windows.Forms.Label lblCommentTitle;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvReviews;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoSao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBinhLuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTao;

        #region Windows Form Designer - Layout & UI Helpers
#nullable enable

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (panelTop == null || panelForm == null || dgvReviews == null || txtComment == null || btnSubmit == null || lblSelect == null || cboProduct == null || lblStarTitle == null || cboStar == null || lblCommentTitle == null) return;
            int totalW = System.Math.Max(600, this.ClientSize.Width);
            int totalH = System.Math.Max(380, this.ClientSize.Height);

            int fullW = totalW - 50;
            panelTop.Location = new System.Drawing.Point(25, 15);
            panelTop.Size = new System.Drawing.Size(fullW, 56);

            // Bố cục khung chọn sản phẩm theo Relative Flow (tránh hoàn toàn đè chữ nhãn)
            lblSelect.Location = new System.Drawing.Point(15, 17);
            cboProduct.Location = new System.Drawing.Point(lblSelect.Right + 16, 14);
            lblAverageScore.Location = new System.Drawing.Point(panelTop.ClientSize.Width - 365, 11);
            lblAverageScore.Size = new System.Drawing.Size(350, 34);
            int maxCboW = System.Math.Max(180, panelTop.ClientSize.Width - cboProduct.Left - lblAverageScore.Width - 25);
            cboProduct.Width = System.Math.Clamp(maxCboW, 200, 420);

            // Bố cục Form nhận xét & đánh giá
            panelForm.Location = new System.Drawing.Point(25, 80);
            int formW = panelForm.ClientSize.Width;

            int starY = 48;
            lblStarTitle.Location = new System.Drawing.Point(15, starY + 4);
            cboStar.Location = new System.Drawing.Point(lblStarTitle.Right + 16, starY);
            cboStar.Width = 320;

            int commentY = cboStar.Bottom + 16;
            lblCommentTitle.Location = new System.Drawing.Point(15, commentY + 6);
            int commentX = System.Math.Max(lblStarTitle.Right, lblCommentTitle.Right) + 16;

            int btnW = 160;
            int btnX = formW - btnW - 20;
            btnSubmit.Location = new System.Drawing.Point(btnX, commentY);
            btnSubmit.Size = new System.Drawing.Size(btnW, 36);
            btnDelete.Location = new System.Drawing.Point(btnX, commentY + 44);
            btnDelete.Size = new System.Drawing.Size(btnW, 36);

            int txtW = System.Math.Max(200, btnX - commentX - 20);
            txtComment.Location = new System.Drawing.Point(commentX, commentY);
            txtComment.Size = new System.Drawing.Size(txtW, 80);

            int formH = System.Math.Max(195, txtComment.Bottom + 18);
            panelForm.Size = new System.Drawing.Size(fullW, formH);

            int dgvY = panelForm.Bottom + 15;
            int reviewH = System.Math.Max(180, totalH - dgvY - 25);
            dgvReviews.Location = new System.Drawing.Point(25, dgvY);
            dgvReviews.Size = new System.Drawing.Size(fullW, reviewH);
        }

#nullable restore
        #endregion
    }
}
