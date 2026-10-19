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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panelTop = new Panel();
            btnRefresh = new Button();
            lblHeader = new Label();
            dgvLibrary = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            panelActions = new Panel();
            btnUninstall = new Button();
            prgDownload = new ProgressBar();
            lblProgressText = new Label();
            btnDownload = new Button();
            lblDownloadCount = new Label();
            lblSize = new Label();
            lblVersion = new Label();
            lblSelectedTitle = new Label();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLibrary).BeginInit();
            panelActions.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(lblHeader);
            panelTop.Location = new Point(25, 20);
            panelTop.Margin = new Padding(3, 5, 3, 5);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(990, 48);
            panelTop.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(49, 98, 130);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(860, 8);
            btnRefresh.Margin = new Padding(3, 5, 3, 5);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(130, 32);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Làm mới kho";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Dock = DockStyle.None;
            lblHeader.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Location = new Point(0, 7);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(657, 35);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "KHO THƯ VIỆN SẢN PHẨM ĐÃ SỞ HỮU (MY LIBRARY)";
            lblHeader.UseMnemonic = false;
            // 
            // dgvLibrary
            // 
            dgvLibrary.AllowUserToAddRows = false;
            dgvLibrary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLibrary.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvLibrary.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvLibrary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvLibrary.ColumnHeadersHeight = 29;
            dgvLibrary.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvLibrary.DefaultCellStyle = dataGridViewCellStyle2;
            dgvLibrary.EnableHeadersVisualStyles = false;
            dgvLibrary.GridColor = Color.FromArgb(42, 71, 94);
            dgvLibrary.Location = new Point(29, 127);
            dgvLibrary.Margin = new Padding(3, 5, 3, 5);
            dgvLibrary.Name = "dgvLibrary";
            dgvLibrary.ReadOnly = true;
            dgvLibrary.RowHeadersVisible = false;
            dgvLibrary.RowHeadersWidth = 51;
            dgvLibrary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLibrary.Size = new Size(686, 773);
            dgvLibrary.TabIndex = 1;
            dgvLibrary.SelectionChanged += DgvLibrary_SelectionChanged;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "ID";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Tên sản phẩm";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Thể loại";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Phiên bản";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Lượt tải";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // panelActions
            // 
            panelActions.BackColor = Color.FromArgb(27, 40, 56);
            panelActions.Controls.Add(btnUninstall);
            panelActions.Controls.Add(prgDownload);
            panelActions.Controls.Add(lblProgressText);
            panelActions.Controls.Add(btnDownload);
            panelActions.Controls.Add(lblDownloadCount);
            panelActions.Controls.Add(lblSize);
            panelActions.Controls.Add(lblVersion);
            panelActions.Controls.Add(lblSelectedTitle);
            panelActions.Location = new Point(731, 127);
            panelActions.Margin = new Padding(3, 5, 3, 5);
            panelActions.Name = "panelActions";
            panelActions.Padding = new Padding(23, 27, 23, 27);
            panelActions.Size = new Size(429, 773);
            panelActions.TabIndex = 0;
            // 
            // btnUninstall
            // 
            btnUninstall.BackColor = Color.FromArgb(180, 40, 40);
            btnUninstall.Cursor = Cursors.Hand;
            btnUninstall.FlatAppearance.BorderSize = 0;
            btnUninstall.FlatStyle = FlatStyle.Flat;
            btnUninstall.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnUninstall.ForeColor = Color.White;
            btnUninstall.Location = new Point(20, 298);
            btnUninstall.Margin = new Padding(3, 5, 3, 5);
            btnUninstall.Name = "btnUninstall";
            btnUninstall.Size = new Size(315, 38);
            btnUninstall.TabIndex = 0;
            btnUninstall.Text = "Gỡ khỏi Thư viện cá nhân";
            btnUninstall.UseMnemonic = false;
            btnUninstall.UseVisualStyleBackColor = false;
            btnUninstall.Click += BtnUninstall_Click;
            // 
            // prgDownload
            // 
            prgDownload.Location = new Point(23, 480);
            prgDownload.Margin = new Padding(3, 5, 3, 5);
            prgDownload.Name = "prgDownload";
            prgDownload.Size = new Size(383, 33);
            prgDownload.TabIndex = 1;
            // 
            // lblProgressText
            // 
            lblProgressText.AutoSize = true;
            lblProgressText.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblProgressText.ForeColor = Color.FromArgb(102, 192, 244);
            lblProgressText.Location = new Point(23, 447);
            lblProgressText.Name = "lblProgressText";
            lblProgressText.Size = new Size(142, 21);
            lblProgressText.TabIndex = 2;
            lblProgressText.Text = "Sẵn sàng tải xuống";
            lblProgressText.UseMnemonic = false;
            // 
            // btnDownload
            // 
            btnDownload.BackColor = Color.FromArgb(90, 160, 45);
            btnDownload.Cursor = Cursors.Hand;
            btnDownload.FlatAppearance.BorderSize = 0;
            btnDownload.FlatStyle = FlatStyle.Flat;
            btnDownload.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDownload.ForeColor = Color.White;
            btnDownload.Location = new Point(20, 180);
            btnDownload.Margin = new Padding(3, 5, 3, 5);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(315, 42);
            btnDownload.TabIndex = 3;
            btnDownload.Text = "TẢI XUỐNG VỀ MÁY";
            btnDownload.UseMnemonic = false;
            btnDownload.UseVisualStyleBackColor = false;
            btnDownload.Click += BtnDownload_Click;
            // 
            // lblDownloadCount
            // 
            lblDownloadCount.AutoSize = true;
            lblDownloadCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDownloadCount.ForeColor = Color.FromArgb(164, 208, 7);
            lblDownloadCount.Location = new Point(23, 280);
            lblDownloadCount.Name = "lblDownloadCount";
            lblDownloadCount.Size = new Size(169, 23);
            lblDownloadCount.TabIndex = 4;
            lblDownloadCount.Text = "Số lần đã tải: 0 lượt";
            lblDownloadCount.UseMnemonic = false;
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Font = new Font("Segoe UI", 10F);
            lblSize.ForeColor = Color.FromArgb(199, 213, 224);
            lblSize.Location = new Point(23, 227);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(161, 23);
            lblSize.TabIndex = 5;
            lblSize.Text = "Dung lượng tệp: ---";
            lblSize.UseMnemonic = false;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 10F);
            lblVersion.ForeColor = Color.FromArgb(199, 213, 224);
            lblVersion.Location = new Point(23, 173);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(117, 23);
            lblVersion.TabIndex = 6;
            lblVersion.Text = "Phiên bản: ---";
            lblVersion.UseMnemonic = false;
            // 
            // lblSelectedTitle
            // 
            lblSelectedTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSelectedTitle.ForeColor = Color.White;
            lblSelectedTitle.Location = new Point(23, 33);
            lblSelectedTitle.Name = "lblSelectedTitle";
            lblSelectedTitle.Size = new Size(383, 120);
            lblSelectedTitle.TabIndex = 7;
            lblSelectedTitle.Text = "Chọn sản phẩm trong thư viện";
            lblSelectedTitle.UseMnemonic = false;
            // 
            // LibraryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(23, 26, 33);
            Controls.Add(panelActions);
            Controls.Add(dgvLibrary);
            Controls.Add(panelTop);
            Margin = new Padding(3, 5, 3, 5);
            Name = "LibraryControl";
            Padding = new Padding(20);
            Size = new Size(1040, 700);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLibrary).EndInit();
            panelActions.ResumeLayout(false);
            panelActions.PerformLayout();
            ResumeLayout(false);
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
