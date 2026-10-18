using System.Drawing;
using System.Windows.Forms;

namespace qlspdigital.Views.Nhom5_Orders
{
    partial class LibraryControl
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dgvLibrary = new System.Windows.Forms.DataGridView();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.prgDownload = new System.Windows.Forms.ProgressBar();
            this.lblProgressText = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblDownloadCount = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblSelectedTitle = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibrary)).BeginInit();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.lblHeader);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(29, 33);
            this.panelTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1131, 60);
            this.panelTop.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(971, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(160, 60);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Làm mới kho";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(564, 28);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "KHO THƯ VIỆN SẢN PHẨM ĐÃ SỞ HỮU (MY LIBRARY)";
            this.lblHeader.UseMnemonic = false;
            // 
            // dgvLibrary
            // 
            this.dgvLibrary.AllowUserToAddRows = false;
            this.dgvLibrary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLibrary.BackgroundColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.dgvLibrary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLibrary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLibrary.ColumnHeadersHeight = 29;
            this.dgvLibrary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLibrary.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLibrary.EnableHeadersVisualStyles = false;
            this.dgvLibrary.GridColor = System.Drawing.Color.FromArgb(42, 71, 94);
            this.dgvLibrary.Location = new System.Drawing.Point(29, 87);
            this.dgvLibrary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvLibrary.Name = "dgvLibrary";
            this.dgvLibrary.ReadOnly = true;
            this.dgvLibrary.RowHeadersVisible = false;
            this.dgvLibrary.RowHeadersWidth = 51;
            this.dgvLibrary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLibrary.Size = new System.Drawing.Size(663, 827);
            this.dgvLibrary.TabIndex = 1;
            this.dgvLibrary.SelectionChanged += new System.EventHandler(this.DgvLibrary_SelectionChanged);
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelActions.Controls.Add(this.btnUninstall);
            this.panelActions.Controls.Add(this.prgDownload);
            this.panelActions.Controls.Add(this.lblProgressText);
            this.panelActions.Controls.Add(this.btnDownload);
            this.panelActions.Controls.Add(this.lblDownloadCount);
            this.panelActions.Controls.Add(this.lblSize);
            this.panelActions.Controls.Add(this.lblVersion);
            this.panelActions.Controls.Add(this.lblSelectedTitle);
            this.panelActions.Location = new System.Drawing.Point(714, 87);
            this.panelActions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(23, 27, 23, 27);
            this.panelActions.Size = new System.Drawing.Size(446, 827);
            this.panelActions.TabIndex = 0;
            // 
            // btnUninstall
            // 
            this.btnUninstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUninstall.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            this.btnUninstall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUninstall.FlatAppearance.BorderSize = 0;
            this.btnUninstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUninstall.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUninstall.ForeColor = System.Drawing.Color.White;
            this.btnUninstall.Location = new System.Drawing.Point(23, 733);
            this.btnUninstall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(400, 51);
            this.btnUninstall.TabIndex = 0;
            this.btnUninstall.Text = "Gỡ khỏi Thư viện cá nhân";
            this.btnUninstall.UseMnemonic = false;
            this.btnUninstall.UseVisualStyleBackColor = false;
            this.btnUninstall.Click += new System.EventHandler(this.BtnUninstall_Click);
            // 
            // prgDownload
            // 
            this.prgDownload.Location = new System.Drawing.Point(23, 373);
            this.prgDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.prgDownload.Name = "prgDownload";
            this.prgDownload.Size = new System.Drawing.Size(400, 29);
            this.prgDownload.TabIndex = 1;
            // 
            // lblProgressText
            // 
            this.lblProgressText.AutoSize = true;
            this.lblProgressText.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblProgressText.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblProgressText.Location = new System.Drawing.Point(23, 340);
            this.lblProgressText.Name = "lblProgressText";
            this.lblProgressText.Size = new System.Drawing.Size(120, 17);
            this.lblProgressText.TabIndex = 2;
            this.lblProgressText.Text = "Sẵn sàng tải xuống";
            this.lblProgressText.UseMnemonic = false;
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownload.FlatAppearance.BorderSize = 0;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(23, 260);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(400, 60);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "TẢI XUỐNG VỀ MÁY";
            this.btnDownload.UseMnemonic = false;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // lblDownloadCount
            // 
            this.lblDownloadCount.AutoSize = true;
            this.lblDownloadCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDownloadCount.ForeColor = System.Drawing.Color.FromArgb(164, 208, 7);
            this.lblDownloadCount.Location = new System.Drawing.Point(23, 200);
            this.lblDownloadCount.Name = "lblDownloadCount";
            this.lblDownloadCount.Size = new System.Drawing.Size(142, 19);
            this.lblDownloadCount.TabIndex = 4;
            this.lblDownloadCount.Text = "Số lần đã tải: 0 lượt";
            this.lblDownloadCount.UseMnemonic = false;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSize.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.lblSize.Location = new System.Drawing.Point(23, 160);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(126, 19);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "Dung lượng tệp: ---";
            this.lblSize.UseMnemonic = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(199, 213, 224);
            this.lblVersion.Location = new System.Drawing.Point(23, 120);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(91, 19);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "Phiên bản: ---";
            this.lblVersion.UseMnemonic = false;
            // 
            // lblSelectedTitle
            // 
            this.lblSelectedTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectedTitle.ForeColor = System.Drawing.Color.White;
            this.lblSelectedTitle.Location = new System.Drawing.Point(23, 27);
            this.lblSelectedTitle.Name = "lblSelectedTitle";
            this.lblSelectedTitle.Size = new System.Drawing.Size(400, 87);
            this.lblSelectedTitle.TabIndex = 7;
            this.lblSelectedTitle.Text = "Chọn sản phẩm trong thư viện";
            this.lblSelectedTitle.UseMnemonic = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "MaSoHuu";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Tên sản phẩm";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "TenSanPham";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Thể loại";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "TenDanhMuc";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Phiên bản";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "PhienBan";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Lượt tải";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "SoLanTai";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // LibraryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.dgvLibrary);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LibraryControl";
            this.Padding = new System.Windows.Forms.Padding(29, 33, 29, 33);
            this.Size = new System.Drawing.Size(1189, 933);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibrary)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.panelActions.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DataGridView dgvLibrary;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.ProgressBar prgDownload;
        private System.Windows.Forms.Label lblProgressText;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblDownloadCount;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblSelectedTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}
