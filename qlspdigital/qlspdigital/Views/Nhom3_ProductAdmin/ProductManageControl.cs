using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom3_ProductAdmin;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom3_ProductAdmin
{
    // UI 10: Quản lý sản phẩm kỹ thuật số & Đính kèm tệp số (Admin)
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public partial class ProductManageControl : UserControl
    {
        private readonly SanPhamAdminService _spAdminService;
        private readonly DanhMucService _danhMucService;

        private DataGridView dgvProducts = null!;
        private Panel panelSearch = null!;
        private Panel panelForm = null!;

        private TextBox txtSearch = null!;
        private Button btnSearch = null!;
        private Label lblSearch = null!;

        private TextBox txtId = null!;
        private TextBox txtName = null!;
        private TextBox txtPrice = null!;
        private TextBox txtSize = null!;
        private TextBox txtVersion = null!;
        private ComboBox cboCategory = null!;
        private CheckBox chkActive = null!;
        private TextBox txtFilePath = null!;
        private TextBox txtDescription = null!;

        private Button btnNew = null!;
        private Button btnSave = null!;
        private Button btnDelete = null!;
        private Button btnBrowseFile = null!;

        public ProductManageControl()
        {
            _spAdminService = new SanPhamAdminService();
            _danhMucService = new DanhMucService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadCategories();
            LoadProducts();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(20);

            // Tiêu đề
            Label lblHeader = new Label();
            lblHeader.Text = "QUẢN TRỊ KHO HÀNG HÓA & TỆP KỸ THUẬT SỐ";
            lblHeader.Location = new Point(20, 10);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblHeader.UseMnemonic = false;

            // 1. THANH TÌM KIẾM SẢN PHẨM
            panelSearch = new Panel();
            panelSearch.Location = new Point(20, 45);
            panelSearch.Size = new Size(490, 42);
            panelSearch.BackColor = Color.FromArgb(27, 40, 56);
            panelSearch.Padding = new Padding(10, 6, 10, 6);

            lblSearch = new Label();
            lblSearch.Text = "Tìm kiếm:";
            lblSearch.ForeColor = Color.White;
            lblSearch.Location = new Point(12, 12);
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblSearch.UseMnemonic = false;

            txtSearch = new TextBox();
            txtSearch.Location = new Point(95, 8);
            txtSearch.Width = 285;
            txtSearch.Font = new Font("Segoe UI", 10f);
            txtSearch.BackColor = Color.FromArgb(18, 25, 35);
            txtSearch.ForeColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            btnSearch = new Button();
            btnSearch.Text = "Tìm kiếm";
            btnSearch.Location = new Point(385, 7);
            btnSearch.Size = new Size(95, 30);
            btnSearch.BackColor = Color.FromArgb(49, 98, 130);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.UseMnemonic = false;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;

            panelSearch.Controls.Add(btnSearch);
            panelSearch.Controls.Add(txtSearch);
            panelSearch.Controls.Add(lblSearch);

            // 2. BẢNG DANH SÁCH SẢN PHẨM (READ)
            dgvProducts = new DataGridView();
            dgvProducts.Location = new Point(20, 95);
            dgvProducts.Size = new Size(490, 580);
            dgvProducts.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvProducts.ForeColor = Color.White;
            dgvProducts.GridColor = Color.FromArgb(42, 71, 94);
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvProducts.DefaultCellStyle.ForeColor = Color.White;
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvProducts.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 45, 62);
            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(102, 192, 244);
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvProducts.EnableHeadersVisualStyles = false;

            dgvProducts.Columns.Add("MaSanPham", "Mã");
            dgvProducts.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvProducts.Columns.Add("GiaBan", "Giá bán");
            dgvProducts.Columns.Add("TenDanhMuc", "Danh mục");
            dgvProducts.Columns.Add("DungLuongMB", "Dung lượng");
            dgvProducts.Columns.Add("TrangThai", "Mở bán");

            dgvProducts.Columns["MaSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProducts.Columns["MaSanPham"].FillWeight = 8;
            dgvProducts.Columns["MaSanPham"].MinimumWidth = 45;
            dgvProducts.Columns["MaSanPham"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProducts.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProducts.Columns["TenSanPham"].FillWeight = 36;
            dgvProducts.Columns["TenSanPham"].MinimumWidth = 140;

            dgvProducts.Columns["TenDanhMuc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProducts.Columns["TenDanhMuc"].FillWeight = 18;
            dgvProducts.Columns["TenDanhMuc"].MinimumWidth = 95;

            dgvProducts.Columns["GiaBan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProducts.Columns["GiaBan"].FillWeight = 18;
            dgvProducts.Columns["GiaBan"].MinimumWidth = 95;
            dgvProducts.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvProducts.Columns["DungLuongMB"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProducts.Columns["DungLuongMB"].FillWeight = 10;
            dgvProducts.Columns["DungLuongMB"].MinimumWidth = 80;
            dgvProducts.Columns["DungLuongMB"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProducts.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProducts.Columns["TrangThai"].FillWeight = 10;
            dgvProducts.Columns["TrangThai"].MinimumWidth = 75;
            dgvProducts.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

            // 3. FORM THÔNG TIN NHẬP LIỆU BÊN PHẢI (CREATE - UPDATE - DELETE)
            panelForm = new Panel();
            panelForm.Location = new Point(555, 45);
            panelForm.Size = new Size(465, 630);
            panelForm.BackColor = Color.FromArgb(27, 40, 56);
            panelForm.Padding = new Padding(20);

            Label lblFormHeader = new Label();
            lblFormHeader.Text = "THÔNG TIN CHI TIẾT SẢN PHẨM";
            lblFormHeader.Location = new Point(20, 15);
            lblFormHeader.AutoSize = true;
            lblFormHeader.ForeColor = Color.FromArgb(102, 192, 244);
            lblFormHeader.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblFormHeader.UseMnemonic = false;

            // Fields
            Label lblFieldId = new Label();
            lblFieldId.Text = "Mã SP:";
            lblFieldId.Location = new Point(20, 50);
            lblFieldId.AutoSize = true;
            lblFieldId.ForeColor = Color.White;
            lblFieldId.Font = new Font("Segoe UI", 9f);
            lblFieldId.UseMnemonic = false;

            txtId = new TextBox();
            txtId.Location = new Point(155, 47);
            txtId.Width = 285;
            txtId.Font = new Font("Segoe UI", 10f);
            txtId.BackColor = Color.FromArgb(18, 25, 35);
            txtId.ForeColor = Color.White;
            txtId.BorderStyle = BorderStyle.FixedSingle;
            txtId.ReadOnly = true;

            Label lblFieldName = new Label();
            lblFieldName.Text = "Tên sản phẩm (*):";
            lblFieldName.Location = new Point(20, 85);
            lblFieldName.AutoSize = true;
            lblFieldName.ForeColor = Color.White;
            lblFieldName.Font = new Font("Segoe UI", 9.5f);
            lblFieldName.UseMnemonic = false;

            txtName = new TextBox();
            txtName.Location = new Point(155, 82);
            txtName.Width = 285;
            txtName.Font = new Font("Segoe UI", 10f);
            txtName.BackColor = Color.FromArgb(18, 25, 35);
            txtName.ForeColor = Color.White;
            txtName.BorderStyle = BorderStyle.FixedSingle;

            Label lblFieldPrice = new Label();
            lblFieldPrice.Text = "Đơn giá (VNĐ):";
            lblFieldPrice.Location = new Point(20, 120);
            lblFieldPrice.AutoSize = true;
            lblFieldPrice.ForeColor = Color.White;
            lblFieldPrice.Font = new Font("Segoe UI", 9.5f);
            lblFieldPrice.UseMnemonic = false;

            txtPrice = new TextBox();
            txtPrice.Location = new Point(155, 117);
            txtPrice.Width = 285;
            txtPrice.Font = new Font("Segoe UI", 10f);
            txtPrice.BackColor = Color.FromArgb(18, 25, 35);
            txtPrice.ForeColor = Color.White;
            txtPrice.BorderStyle = BorderStyle.FixedSingle;

            Label lblFieldSize = new Label();
            lblFieldSize.Text = "Dung lượng (MB):";
            lblFieldSize.Location = new Point(20, 155);
            lblFieldSize.AutoSize = true;
            lblFieldSize.ForeColor = Color.White;
            lblFieldSize.Font = new Font("Segoe UI", 9.5f);
            lblFieldSize.UseMnemonic = false;

            txtSize = new TextBox();
            txtSize.Location = new Point(155, 152);
            txtSize.Width = 285;
            txtSize.Font = new Font("Segoe UI", 10f);
            txtSize.BackColor = Color.FromArgb(18, 25, 35);
            txtSize.ForeColor = Color.White;
            txtSize.BorderStyle = BorderStyle.FixedSingle;

            Label lblFieldVersion = new Label();
            lblFieldVersion.Text = "Phiên bản:";
            lblFieldVersion.Location = new Point(20, 190);
            lblFieldVersion.AutoSize = true;
            lblFieldVersion.ForeColor = Color.White;
            lblFieldVersion.Font = new Font("Segoe UI", 9.5f);
            lblFieldVersion.UseMnemonic = false;

            txtVersion = new TextBox();
            txtVersion.Location = new Point(155, 187);
            txtVersion.Width = 285;
            txtVersion.Font = new Font("Segoe UI", 10f);
            txtVersion.BackColor = Color.FromArgb(18, 25, 35);
            txtVersion.ForeColor = Color.White;
            txtVersion.BorderStyle = BorderStyle.FixedSingle;

            // Danh mục ComboBox
            Label lblCat = new Label();
            lblCat.Text = "Danh mục (*):";
            lblCat.Location = new Point(20, 225);
            lblCat.AutoSize = true;
            lblCat.ForeColor = Color.White;
            lblCat.Font = new Font("Segoe UI", 9.5f);
            lblCat.UseMnemonic = false;

            cboCategory = new ComboBox();
            cboCategory.Location = new Point(155, 222);
            cboCategory.Width = 285;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Font = new Font("Segoe UI", 10f);
            cboCategory.BackColor = Color.FromArgb(18, 25, 35);
            cboCategory.ForeColor = Color.White;
            cboCategory.FlatStyle = FlatStyle.Flat;

            // Checkbox Đang kinh doanh
            chkActive = new CheckBox();
            chkActive.Text = "Đang mở bán trên Store";
            chkActive.Location = new Point(155, 260);
            chkActive.AutoSize = true;
            chkActive.ForeColor = Color.FromArgb(164, 208, 7);
            chkActive.Checked = true;
            chkActive.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            chkActive.UseMnemonic = false;

            // Tệp đính kèm
            Label lblFile = new Label();
            lblFile.Text = "Tệp cài đặt:";
            lblFile.Location = new Point(20, 290);
            lblFile.AutoSize = true;
            lblFile.ForeColor = Color.White;
            lblFile.Font = new Font("Segoe UI", 9.5f);
            lblFile.UseMnemonic = false;

            txtFilePath = new TextBox();
            txtFilePath.Location = new Point(155, 287);
            txtFilePath.Width = 195;
            txtFilePath.Font = new Font("Segoe UI", 10f);
            txtFilePath.BackColor = Color.FromArgb(18, 25, 35);
            txtFilePath.ForeColor = Color.White;
            txtFilePath.BorderStyle = BorderStyle.FixedSingle;
            txtFilePath.ReadOnly = true;

            btnBrowseFile = new Button();
            btnBrowseFile.Text = "Chọn...";
            btnBrowseFile.Location = new Point(355, 285);
            btnBrowseFile.Size = new Size(85, 28);
            btnBrowseFile.BackColor = Color.FromArgb(49, 98, 130);
            btnBrowseFile.ForeColor = Color.White;
            btnBrowseFile.FlatStyle = FlatStyle.Flat;
            btnBrowseFile.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnBrowseFile.Cursor = Cursors.Hand;
            btnBrowseFile.UseMnemonic = false;
            btnBrowseFile.FlatAppearance.BorderSize = 0;
            btnBrowseFile.Click += BtnBrowseFile_Click;

            // Mô tả chi tiết - TEXTAREA
            Label lblDesc = new Label();
            lblDesc.Text = "Mô tả chi tiết sản phẩm:";
            lblDesc.Location = new Point(20, 325);
            lblDesc.AutoSize = true;
            lblDesc.ForeColor = Color.White;
            lblDesc.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblDesc.UseMnemonic = false;

            txtDescription = new TextBox();
            txtDescription.Location = new Point(20, 349);
            txtDescription.Size = new Size(420, 115);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Font = new Font("Segoe UI", 10f);
            txtDescription.BackColor = Color.FromArgb(18, 25, 35);
            txtDescription.ForeColor = Color.White;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;

            // Các nút bấm Thao tác CRUD
            btnNew = new Button();
            btnNew.Text = "+ Thêm mới";
            btnNew.Location = new Point(20, 480);
            btnNew.Size = new Size(130, 38);
            btnNew.BackColor = Color.FromArgb(60, 75, 90);
            btnNew.ForeColor = Color.White;
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnNew.Cursor = Cursors.Hand;
            btnNew.UseMnemonic = false;
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.Click += BtnNew_Click;

            btnSave = new Button();
            btnSave.Text = "Lưu";
            btnSave.Location = new Point(165, 480);
            btnSave.Size = new Size(140, 38);
            btnSave.BackColor = Color.FromArgb(90, 160, 45);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnSave.Cursor = Cursors.Hand;
            btnSave.UseMnemonic = false;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnDelete = new Button();
            btnDelete.Text = "Xóa";
            btnDelete.Location = new Point(320, 480);
            btnDelete.Size = new Size(120, 38);
            btnDelete.BackColor = Color.FromArgb(180, 40, 40);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.UseMnemonic = false;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;

            panelForm.Controls.Add(btnDelete);
            panelForm.Controls.Add(btnSave);
            panelForm.Controls.Add(btnNew);
            panelForm.Controls.Add(txtDescription);
            panelForm.Controls.Add(lblDesc);
            panelForm.Controls.Add(btnBrowseFile);
            panelForm.Controls.Add(txtFilePath);
            panelForm.Controls.Add(lblFile);
            panelForm.Controls.Add(chkActive);
            panelForm.Controls.Add(cboCategory);
            panelForm.Controls.Add(lblCat);
            panelForm.Controls.Add(txtVersion);
            panelForm.Controls.Add(lblFieldVersion);
            panelForm.Controls.Add(txtSize);
            panelForm.Controls.Add(lblFieldSize);
            panelForm.Controls.Add(txtPrice);
            panelForm.Controls.Add(lblFieldPrice);
            panelForm.Controls.Add(txtName);
            panelForm.Controls.Add(lblFieldName);
            panelForm.Controls.Add(txtId);
            panelForm.Controls.Add(lblFieldId);
            panelForm.Controls.Add(lblFormHeader);

            this.Controls.Add(panelForm);
            this.Controls.Add(dgvProducts);
            this.Controls.Add(panelSearch);
            this.Controls.Add(lblHeader);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelForm == null || dgvProducts == null || txtDescription == null || btnNew == null || txtId == null) return;
            int totalW = Math.Max(700, this.ClientSize.Width);
            int totalH = Math.Max(400, this.ClientSize.Height);

            int rightW = Math.Clamp((int)(totalW * 0.44), 420, 600);
            int leftW = Math.Max(260, totalW - rightW - 65); // margin 25 left + 15 gap + 25 right
            int contentH = Math.Max(220, totalH - 120);

            // 1. Search Bar
            panelSearch.Location = new Point(25, 55);
            panelSearch.Size = new Size(leftW, 42);
            lblSearch.Location = new Point(12, 12);
            txtSearch.Location = new Point(lblSearch.Right + 12, 8);
            btnSearch.Size = new Size(95, 30);
            btnSearch.Location = new Point(leftW - 105, 6);
            txtSearch.Width = Math.Max(120, btnSearch.Left - txtSearch.Left - 12);

            // 2. DataGridView
            dgvProducts.Location = new Point(25, 105);
            dgvProducts.Size = new Size(leftW, contentH);

            // 3. Form Panel
            panelForm.Location = new Point(25 + leftW + 15, 55);
            panelForm.Size = new Size(rightW, contentH + 50);

            // 4. Form Controls inside panelForm
            int fieldInputW = Math.Max(200, rightW - 175);
            txtId.Width = fieldInputW;
            txtName.Width = fieldInputW;
            txtPrice.Width = fieldInputW;
            txtSize.Width = fieldInputW;
            txtVersion.Width = fieldInputW;
            cboCategory.Width = fieldInputW;
            txtFilePath.Width = Math.Max(120, fieldInputW - 90);
            btnBrowseFile.Location = new Point(155 + txtFilePath.Width + 10, txtFilePath.Top - 1);
            txtDescription.Width = rightW - 40;

            // Dynamically stretch textarea vertically and pin buttons cleanly
            int formH = panelForm.ClientSize.Height;
            int descH = Math.Clamp(formH - txtDescription.Top - 65, 75, 180);
            txtDescription.Height = descH;
            int btnY = txtDescription.Bottom + 12;

            int btnW = Math.Max(90, (rightW - 40 - 20) / 3);
            btnNew.Location = new Point(20, btnY);
            btnNew.Size = new Size(btnW, 38);
            btnSave.Location = new Point(20 + btnW + 10, btnY);
            btnSave.Size = new Size(btnW, 38);
            btnDelete.Location = new Point(20 + (btnW + 10) * 2, btnY);
            btnDelete.Size = new Size(btnW, 38);
        }

        private void LoadCategories()
        {
            cboCategory.Items.Clear();
            var list = _danhMucService.LayTatCaDanhMuc();
            foreach (var dm in list)
            {
                cboCategory.Items.Add(new CategoryItem { Id = dm.MaDanhMuc, Name = dm.TenDanhMuc });
            }
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
        }

        public void LoadProducts()
        {
            dgvProducts.Rows.Clear();
            string keyword = txtSearch.Text.Trim();
            var list = _spAdminService.LayDanhSachSanPham(keyword);

            foreach (var sp in list)
            {
                dgvProducts.Rows.Add(
                    sp.MaSanPham,
                    sp.TenSanPham,
                    FormatHelper.FormatCurrency(sp.GiaBan),
                    sp.TenDanhMuc,
                    $"{sp.DungLuongMB:0.#} MB",
                    sp.DangKinhDoanh ? "Đang bán" : "Ngưng"
                );
            }
        }

        private void DgvProducts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) return;

            var row = dgvProducts.SelectedRows[0];
            if (row == null || row.Index < 0) return;

            var val = (dgvProducts.Columns.Contains("MaSanPham") ? row.Cells["MaSanPham"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;
            int id = Convert.ToInt32(val);
            var list = _spAdminService.LayDanhSachSanPham();
            var sp = list.Find(x => x.MaSanPham == id);

            if (sp != null)
            {
                txtId.Text = sp.MaSanPham.ToString();
                txtName.Text = sp.TenSanPham;
                txtPrice.Text = sp.GiaBan.ToString("0");
                txtSize.Text = sp.DungLuongMB.ToString("0.##");
                txtVersion.Text = sp.PhienBan;
                chkActive.Checked = sp.DangKinhDoanh;
                txtDescription.Text = sp.MoTa ?? string.Empty;

                for (int i = 0; i < cboCategory.Items.Count; i++)
                {
                    if (cboCategory.Items[i] is CategoryItem item && item.Id == sp.MaDanhMuc)
                    {
                        cboCategory.SelectedIndex = i;
                        break;
                    }
                }

                var tep = _spAdminService.LayTepTheoSanPham(sp.MaSanPham);
                txtFilePath.Text = tep != null ? tep.TenTep : "Chưa có file";
            }
        }

        private void ResetForm()
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Text = "0";
            txtSize.Text = "0";
            txtVersion.Text = "1.0.0";
            txtFilePath.Clear();
            txtDescription.Clear();
            chkActive.Checked = true;
            txtName.Focus();
        }

        private void BtnBrowseFile_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Tệp phần mềm / dữ liệu (*.zip;*.exe;*.rar;*.iso)|*.zip;*.exe;*.rar;*.iso|Tất cả tệp (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = ofd.SafeFileName;
                    FileInfo fi = new FileInfo(ofd.FileName);
                    txtSize.Text = Math.Round((decimal)fi.Length / (1024 * 1024), 2).ToString();
                }
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtName.Text.Trim().Length > 200)
            {
                MessageBox.Show("Tên sản phẩm không được vượt quá 200 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0 || price > 1000000000m)
            {
                MessageBox.Show("Giá bán không hợp lệ (phải từ 0 đến 1.000.000.000 ₫)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSize.Text.Trim(), out decimal size) || size < 0 || size > 1000000m)
            {
                MessageBox.Show("Dung lượng không hợp lệ (phải từ 0 đến 1.000.000 MB)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtVersion.Text.Trim().Length > 50)
            {
                MessageBox.Show("Phiên bản không được vượt quá 50 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int catId = 1;
            if (cboCategory.SelectedItem is CategoryItem ci) catId = ci.Id;

            int.TryParse(txtId.Text, out int id);

            SanPhamDTO dto = new SanPhamDTO
            {
                MaSanPham = id,
                TenSanPham = txtName.Text.Trim(),
                GiaBan = price,
                DungLuongMB = size,
                PhienBan = string.IsNullOrWhiteSpace(txtVersion.Text) ? "1.0.0" : txtVersion.Text.Trim(),
                MaDanhMuc = catId,
                DangKinhDoanh = chkActive.Checked,
                MoTa = txtDescription.Text.Trim()
            };

            TepSanPhamDTO? tep = null;
            if (!string.IsNullOrWhiteSpace(txtFilePath.Text) && !txtFilePath.Text.Contains("Chưa có file"))
            {
                tep = new TepSanPhamDTO
                {
                    TenTep = txtFilePath.Text.Trim(),
                    DuongDanTep = $"Storage/Products/{txtFilePath.Text.Trim()}",
                    DinhDangTep = Path.GetExtension(txtFilePath.Text.Trim()),
                    PhienBan = dto.PhienBan
                };
            }

            if (_spAdminService.LuuSanPham(dto, tep, out string msg))
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm [{txtName.Text}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_spAdminService.XoaSanPham(id, out string msg))
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e) => LoadProducts();
        private void BtnSearch_Click(object? sender, EventArgs e) => LoadProducts();
        private void BtnNew_Click(object? sender, EventArgs e) => ResetForm();

        private class CategoryItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public override string ToString() => Name;
        }
    }
}
