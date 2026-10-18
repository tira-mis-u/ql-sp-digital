namespace qlspdigital.Controls
{
    partial class ProductCardControl
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblThumb = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(36)))), ((int)(((byte)(50)))));
            this.panelHeader.Controls.Add(this.lblThumb);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(10, 10);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(225, 100);
            this.panelHeader.TabIndex = 0;
            // 
            // lblThumb
            // 
            this.lblThumb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblThumb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblThumb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.lblThumb.Location = new System.Drawing.Point(0, 0);
            this.lblThumb.Name = "lblThumb";
            this.lblThumb.Size = new System.Drawing.Size(225, 100);
            this.lblThumb.TabIndex = 0;
            this.lblThumb.Text = "🎮 DIGITAL\r\nPRODUCT";
            this.lblThumb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblThumb.UseMnemonic = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 110);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(225, 42);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Tên sản phẩm";
            this.lblTitle.UseMnemonic = false;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoEllipsis = true;
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(181)))), ((int)(((byte)(196)))));
            this.lblCategory.Location = new System.Drawing.Point(10, 152);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(225, 22);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Thể loại: Game";
            this.lblCategory.UseMnemonic = false;
            // 
            // lblRating
            // 
            this.lblRating.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRating.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblRating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.lblRating.Location = new System.Drawing.Point(10, 174);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(225, 22);
            this.lblRating.TabIndex = 3;
            this.lblRating.Text = "⭐ 5.0 (0 đánh giá)";
            this.lblRating.UseMnemonic = false;
            // 
            // lblPrice
            // 
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(208)))), ((int)(((byte)(7)))));
            this.lblPrice.Location = new System.Drawing.Point(10, 196);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblPrice.Size = new System.Drawing.Size(225, 32);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "150.000 ₫";
            this.lblPrice.UseMnemonic = false;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnAddToCart);
            this.panelButtons.Controls.Add(this.btnDetail);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(10, 259);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(225, 36);
            this.panelButtons.TabIndex = 5;
            // 
            // btnDetail
            // 
            this.btnDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(98)))), ((int)(((byte)(130)))));
            this.btnDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetail.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDetail.FlatAppearance.BorderSize = 0;
            this.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.btnDetail.ForeColor = System.Drawing.Color.White;
            this.btnDetail.Location = new System.Drawing.Point(0, 0);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(95, 36);
            this.btnDetail.TabIndex = 0;
            this.btnDetail.Text = "Chi tiết";
            this.btnDetail.UseMnemonic = false;
            this.btnDetail.UseVisualStyleBackColor = false;
            this.btnDetail.Click += new System.EventHandler(this.BtnDetail_Click);
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(160)))), ((int)(((byte)(45)))));
            this.btnAddToCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddToCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddToCart.FlatAppearance.BorderSize = 0;
            this.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToCart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(95, 0);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(130, 36);
            this.btnAddToCart.TabIndex = 1;
            this.btnAddToCart.Text = "+ Thêm giỏ";
            this.btnAddToCart.UseMnemonic = false;
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.BtnAddToCart_Click);
            // 
            // ProductCardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(45)))), ((int)(((byte)(62)))));
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panelHeader);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "ProductCardControl";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(245, 305);
            this.MouseEnter += new System.EventHandler(this.ProductCardControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ProductCardControl_MouseLeave);
            this.panelHeader.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblThumb;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnAddToCart;
    }
}
