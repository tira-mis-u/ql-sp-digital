using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom5_Orders;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom5_Orders
{
    // UI 6: Kho thư viện trò chơi & sản phẩm số đã sở hữu (Steam Library style)
    // Thành viên phụ trách: Phạm Văn Đam (Nhóm 5)
    public partial class LibraryControl : UserControl
    {
        private readonly ThuVienService _libraryService;

        private DataGridView dgvLibrary = null!;
        private Panel panelActions = null!;
        private Label lblSelectedTitle = null!;
        private Label lblVersion = null!;
        private Label lblSize = null!;
        private Label lblDownloadCount = null!;
        private ProgressBar prgDownload = null!;
        private Label lblProgressText = null!;
        private Button btnDownload = null!;
        private Button btnUninstall = null!;
        private Panel panelTop;
        private Label lblHeader;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private Button btnRefresh = null!;

        public LibraryControl()
        {
            _libraryService = new ThuVienService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadLibrary();
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panelTop = new Panel();
            btnRefresh = new Button();
            lblHeader = new Label();
            dgvLibrary = new DataGridView();
            panelActions = new Panel();
            btnUninstall = new Button();
            prgDownload = new ProgressBar();
            lblProgressText = new Label();
            btnDownload = new Button();
            lblDownloadCount = new Label();
            lblSize = new Label();
            lblVersion = new Label();
            lblSelectedTitle = new Label();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLibrary).BeginInit();
            panelActions.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(lblHeader);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(29, 33);
            panelTop.Margin = new Padding(3, 4, 3, 4);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1131, 60);
            panelTop.TabIndex = 2;
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
            btnRefresh.Location = new Point(971, 0);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(160, 60);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Làm mới kho";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Dock = DockStyle.Left;
            lblHeader.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Location = new Point(0, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(699, 35);
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
            dgvLibrary.Location = new Point(29, 87);
            dgvLibrary.Margin = new Padding(3, 4, 3, 4);
            dgvLibrary.Name = "dgvLibrary";
            dgvLibrary.ReadOnly = true;
            dgvLibrary.RowHeadersVisible = false;
            dgvLibrary.RowHeadersWidth = 51;
            dgvLibrary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLibrary.Size = new Size(663, 827);
            dgvLibrary.TabIndex = 1;
            dgvLibrary.SelectionChanged += DgvLibrary_SelectionChanged;
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
            panelActions.Location = new Point(714, 87);
            panelActions.Margin = new Padding(3, 4, 3, 4);
            panelActions.Name = "panelActions";
            panelActions.Padding = new Padding(23, 27, 23, 27);
            panelActions.Size = new Size(446, 827);
            panelActions.TabIndex = 0;
            // 
            // btnUninstall
            // 
            btnUninstall.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUninstall.BackColor = Color.FromArgb(180, 40, 40);
            btnUninstall.Cursor = Cursors.Hand;
            btnUninstall.FlatAppearance.BorderSize = 0;
            btnUninstall.FlatStyle = FlatStyle.Flat;
            btnUninstall.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnUninstall.ForeColor = Color.White;
            btnUninstall.Location = new Point(23, 733);
            btnUninstall.Margin = new Padding(3, 4, 3, 4);
            btnUninstall.Name = "btnUninstall";
            btnUninstall.Size = new Size(400, 51);
            btnUninstall.TabIndex = 0;
            btnUninstall.Text = "Gỡ khỏi Thư viện cá nhân";
            btnUninstall.UseMnemonic = false;
            btnUninstall.UseVisualStyleBackColor = false;
            btnUninstall.Click += BtnUninstall_Click;
            // 
            // prgDownload
            // 
            prgDownload.Location = new Point(23, 373);
            prgDownload.Margin = new Padding(3, 4, 3, 4);
            prgDownload.Name = "prgDownload";
            prgDownload.Size = new Size(400, 29);
            prgDownload.TabIndex = 1;
            // 
            // lblProgressText
            // 
            lblProgressText.AutoSize = true;
            lblProgressText.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblProgressText.ForeColor = Color.FromArgb(102, 192, 244);
            lblProgressText.Location = new Point(23, 340);
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
            btnDownload.Location = new Point(23, 260);
            btnDownload.Margin = new Padding(3, 4, 3, 4);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(400, 60);
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
            lblDownloadCount.Location = new Point(23, 200);
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
            lblSize.Location = new Point(23, 160);
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
            lblVersion.Location = new Point(23, 120);
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
            lblSelectedTitle.Location = new Point(23, 27);
            lblSelectedTitle.Name = "lblSelectedTitle";
            lblSelectedTitle.Size = new Size(400, 87);
            lblSelectedTitle.TabIndex = 7;
            lblSelectedTitle.Text = "Chọn sản phẩm trong thư viện";
            lblSelectedTitle.UseMnemonic = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "ID";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "MaSoHuu";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Tên sản phẩm";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "TenSanPham";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Thể loại";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "TenDanhMuc";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Phiên bản";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "PhienBan";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Lượt tải";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "SoLanTai";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // LibraryControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(23, 26, 33);
            Controls.Add(panelActions);
            Controls.Add(dgvLibrary);
            Controls.Add(panelTop);
            Margin = new Padding(3, 4, 3, 4);
            Name = "LibraryControl";
            Padding = new Padding(29, 33, 29, 33);
            Size = new Size(1189, 933);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLibrary).EndInit();
            panelActions.ResumeLayout(false);
            panelActions.PerformLayout();
            ResumeLayout(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (dgvLibrary == null || panelActions == null || lblSelectedTitle == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            int actionW = Math.Clamp((int)(totalW * 0.35), 320, 420);
            int leftW = Math.Max(260, totalW - actionW - 65); // margin 25 left + 15 gap + 25 right
            int contentH = Math.Max(180, totalH - 100 - 25);

            dgvLibrary.Location = new Point(25, 100);
            dgvLibrary.Size = new Size(leftW, contentH);

            panelActions.Location = new Point(25 + leftW + 15, 100);
            panelActions.Size = new Size(actionW, contentH);

            int btnW = actionW - 40;
            lblSelectedTitle.Width = btnW;
            btnDownload.Width = btnW;
            prgDownload.Width = btnW;
            btnUninstall.Width = btnW;
        }

        public void LoadLibrary()
        {
            dgvLibrary.Rows.Clear();
            prgDownload.Value = 0;
            lblProgressText.Text = "Sẵn sàng tải xuống";

            if (!UserSession.IsLoggedIn)
            {
                lblSelectedTitle.Text = "Vui lòng đăng nhập để xem thư viện";
                return;
            }

            var list = _libraryService.LayKhoThuVien(UserSession.CurrentUserId);
            foreach (var item in list)
            {
                dgvLibrary.Rows.Add(
                    item.MaSoHuu,
                    item.TenSanPham,
                    item.TenDanhMuc,
                    item.PhienBan,
                    $"{item.SoLanTai} lần"
                );
            }

            if (list.Count == 0)
            {
                lblSelectedTitle.Text = "Thư viện trống (Hãy mua game tại Store!)";
            }
        }

        private int GetSelectedSoHuuId()
        {
            if (dgvLibrary.SelectedRows.Count == 0) return 0;
            var row = dgvLibrary.SelectedRows[0];
            if (row == null || row.Index < 0) return 0;
            var val = (dgvLibrary.Columns.Contains("MaSoHuu") ? row.Cells["MaSoHuu"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            return val != null ? Convert.ToInt32(val) : 0;
        }

        private string GetSelectedProductName()
        {
            if (dgvLibrary.SelectedRows.Count == 0) return string.Empty;
            var row = dgvLibrary.SelectedRows[0];
            if (row == null || row.Index < 0) return string.Empty;
            var val = (dgvLibrary.Columns.Contains("TenSanPham") ? row.Cells["TenSanPham"].Value : null) ?? (row.Cells.Count > 1 ? row.Cells[1].Value : null);
            return val?.ToString() ?? string.Empty;
        }

        private void DgvLibrary_SelectionChanged(object? sender, EventArgs e)
        {
            int soHuuId = GetSelectedSoHuuId();
            if (soHuuId == 0) return;

            var list = _libraryService.LayKhoThuVien(UserSession.CurrentUserId);
            var item = list.Find(x => x.MaSoHuu == soHuuId);

            if (item != null)
            {
                lblSelectedTitle.Text = item.TenSanPham;
                lblVersion.Text = $"Phiên bản: {item.PhienBan}";
                lblSize.Text = $"Dung lượng: {FormatHelper.FormatFileSize(item.DungLuongMB)}";
                lblDownloadCount.Text = $"Số lần đã tải: {item.SoLanTai} lượt";
            }
        }

        private async void BtnDownload_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn) return;

            int soHuuId = GetSelectedSoHuuId();
            if (soHuuId == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần tải!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var list = _libraryService.LayKhoThuVien(UserSession.CurrentUserId);
            var item = list.Find(x => x.MaSoHuu == soHuuId);

            if (item == null) return;

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = $"Chọn thư mục lưu tệp cho [{item.TenSanPham}]:";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    btnDownload.Enabled = false;
                    lblProgressText.Text = "Đang kết nối máy chủ CDN và tải tệp...";

                    bool success = await _libraryService.TaiXuongTepAsync(item, fbd.SelectedPath, (p) =>
                    {
                        prgDownload.Value = p;
                        lblProgressText.Text = $"Tiến trình tải: {p}%...";
                    });

                    btnDownload.Enabled = true;

                    if (success)
                    {
                        lblProgressText.Text = "Tải xuống hoàn tất! Đã kiểm tra SHA-256 an toàn.";
                        MessageBox.Show($"Tải tệp thành công về thư mục:\n{fbd.SelectedPath}", "Hoàn tất tải xuống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadLibrary();
                    }
                    else
                    {
                        lblProgressText.Text = "Tải xuống thất bại!";
                        MessageBox.Show("Không thể ghi tệp vào thư mục đích.", "Lỗi tải", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnUninstall_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn) return;

            int soHuuId = GetSelectedSoHuuId();
            if (soHuuId == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần gỡ khỏi Thư viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = GetSelectedProductName();

            if (MessageBox.Show($"Bạn có chắc chắn muốn gỡ [{name}] khỏi Thư viện của mình?", "Xác nhận gỡ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_libraryService.GoKhoiThuVien(soHuuId, UserSession.CurrentUserId, out string msg))
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLibrary();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi gỡ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadLibrary();
        }
    }
}
