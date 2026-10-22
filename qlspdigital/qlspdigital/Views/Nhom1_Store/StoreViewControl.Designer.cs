namespace qlspdigital.Views.Nhom1_Store
{
    partial class StoreViewControl
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
            this.panelFilter = new System.Windows.Forms.Panel();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearchTitle = new System.Windows.Forms.Label();
            this.flowProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.panelFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(45)))));
            this.panelFilter.Controls.Add(this.lblTotalCount);
            this.panelFilter.Controls.Add(this.btnRefresh);
            this.panelFilter.Controls.Add(this.cboCategory);
            this.panelFilter.Controls.Add(this.lblCategoryTitle);
            this.panelFilter.Controls.Add(this.btnSearch);
            this.panelFilter.Controls.Add(this.txtSearch);
            this.panelFilter.Controls.Add(this.lblSearchTitle);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.panelFilter.Size = new System.Drawing.Size(1040, 65);
            this.panelFilter.TabIndex = 0;
            this.panelFilter.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelFilter_Paint);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblTotalCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.lblTotalCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalCount.Location = new System.Drawing.Point(840, 22);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(133, 19);
            this.lblTotalCount.TabIndex = 6;
            this.lblTotalCount.Text = "Hiển thị: 0 sản phẩm";
            this.lblTotalCount.UseMnemonic = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(80)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(890, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(95, 34);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseMnemonic = false;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // cboCategory
            // 
            this.cboCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCategory.ForeColor = System.Drawing.Color.White;
            this.cboCategory.Location = new System.Drawing.Point(675, 15);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(200, 25);
            this.cboCategory.TabIndex = 4;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.CboCategory_SelectedIndexChanged);
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCategoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.lblCategoryTitle.Location = new System.Drawing.Point(600, 22);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(61, 17);
            this.lblCategoryTitle.TabIndex = 3;
            this.lblCategoryTitle.Text = "Thể loại:";
            this.lblCategoryTitle.UseMnemonic = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(98)))), ((int)(((byte)(130)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(475, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(105, 34);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseMnemonic = false;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(145, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Nhập tên sản phẩm...";
            this.txtSearch.Size = new System.Drawing.Size(320, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // lblSearchTitle
            // 
            this.lblSearchTitle.AutoSize = true;
            this.lblSearchTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearchTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.lblSearchTitle.Location = new System.Drawing.Point(20, 22);
            this.lblSearchTitle.Name = "lblSearchTitle";
            this.lblSearchTitle.Size = new System.Drawing.Size(70, 17);
            this.lblSearchTitle.TabIndex = 0;
            this.lblSearchTitle.Text = "Tìm kiếm:";
            this.lblSearchTitle.UseMnemonic = false;
            // 
            // flowProducts
            // 
            this.flowProducts.AutoScroll = true;
            this.flowProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.flowProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowProducts.Location = new System.Drawing.Point(0, 65);
            this.flowProducts.Name = "flowProducts";
            this.flowProducts.Padding = new System.Windows.Forms.Padding(15, 8, 32, 8);
            this.flowProducts.Size = new System.Drawing.Size(1040, 635);
            this.flowProducts.TabIndex = 1;
            this.flowProducts.WrapContents = true;
            // 
            // StoreViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.flowProducts);
            this.Controls.Add(this.panelFilter);
            this.Name = "StoreViewControl";
            this.Size = new System.Drawing.Size(1040, 700);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.ResumeLayout(false);
            this.AdjustLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.FlowLayoutPanel flowProducts;

        #region Windows Form Designer - Layout & UI Helpers
#nullable enable

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public qlspdigital.Views.Main.MainForm? MainForm
        {
            get => _mainForm;
            set
            {
                _mainForm = value;
                // Khi MainForm được gán (sau constructor), cập nhật tất cả card đang hiển thị
                // Điều này sửa lỗi card.MainForm == null khi LoadProducts() chạy trước khi MainForm được set
                if (flowProducts != null)
                {
                    foreach (System.Windows.Forms.Control ctrl in flowProducts.Controls)
                    {
                        if (ctrl is qlspdigital.Controls.ProductCardControl card)
                            card.MainForm = value;
                    }
                }
            }
        }
        private qlspdigital.Views.Main.MainForm? _mainForm;

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (panelFilter == null || txtSearch == null || cboCategory == null || btnSearch == null || btnRefresh == null || lblTotalCount == null || lblSearchTitle == null || lblCategoryTitle == null) return;
            int pnlW = panelFilter.ClientSize.Width;
            if (pnlW < 300) return;

            lblTotalCount.Location = new System.Drawing.Point(pnlW - lblTotalCount.Width - 25, 22);

            lblSearchTitle.Location = new System.Drawing.Point(20, 22);
            txtSearch.Location = new System.Drawing.Point(lblSearchTitle.Right + 12, 15);

            int btnSearchW = 100;
            int btnRefreshW = 90;
            int rightBoundary = lblTotalCount.Left - 20;
            int nonInputSpace = 20 + lblSearchTitle.Width + 12 + btnSearchW + 18 + lblCategoryTitle.Width + 12 + btnRefreshW + 12;
            int availableForInputs = System.Math.Max(260, rightBoundary - nonInputSpace);

            int searchW = System.Math.Clamp((int)(availableForInputs * 0.58), 160, 450);
            int catW = System.Math.Clamp((int)(availableForInputs * 0.42), 120, 220);

            txtSearch.Size = new System.Drawing.Size(searchW, 34);

            btnSearch.Location = new System.Drawing.Point(txtSearch.Right + 10, 15);
            btnSearch.Size = new System.Drawing.Size(btnSearchW, 34);

            lblCategoryTitle.Location = new System.Drawing.Point(btnSearch.Right + 22, 22);
            cboCategory.Location = new System.Drawing.Point(lblCategoryTitle.Right + 12, 15);
            cboCategory.Size = new System.Drawing.Size(catW, 34);

            btnRefresh.Location = new System.Drawing.Point(cboCategory.Right + 14, 15);
            btnRefresh.Size = new System.Drawing.Size(btnRefreshW, 34);
        }

        private void PanelFilter_Paint(object? sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (sender is System.Windows.Forms.Panel pnl)
            {
                using (System.Drawing.Pen p = new System.Drawing.Pen(qlspdigital.Shared.ThemeHelper.GetHeaderBorderColor(), 1))
                {
                    e.Graphics.DrawLine(p, 0, pnl.Height - 1, pnl.Width, pnl.Height - 1);
                }
            }
        }

#nullable restore
        #endregion
    }
}
