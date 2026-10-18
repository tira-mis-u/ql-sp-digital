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

        private string? _selectedSourceFilePath = null;

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

            int rightW = Math.Clamp((int)(totalW * 0.34), 345, 420);
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
            int fieldInputW = Math.Max(150, rightW - 175);
            txtId.Width = fieldInputW;
            txtName.Width = fieldInputW;
            txtPrice.Width = fieldInputW;
            txtSize.Width = fieldInputW;
            txtVersion.Width = fieldInputW;
            cboCategory.Width = fieldInputW;

            int browseW = 75;
            btnBrowseFile.Size = new Size(browseW, txtFilePath.Height + 2);
            txtFilePath.Width = Math.Max(60, fieldInputW - browseW - 10);
            btnBrowseFile.Location = new Point(txtFilePath.Left + txtFilePath.Width + 10, txtFilePath.Top - 1);
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
                _selectedSourceFilePath = null;
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
            _selectedSourceFilePath = null;
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
                    _selectedSourceFilePath = ofd.FileName;
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
                string fileName = txtFilePath.Text.Trim();
                string storageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage", "Products");
                if (!Directory.Exists(storageDir))
                {
                    Directory.CreateDirectory(storageDir);
                }
                string destPath = Path.Combine(storageDir, fileName);

                long fileBytes = 0;
                string? sha256 = null;

                if (!string.IsNullOrEmpty(_selectedSourceFilePath) && File.Exists(_selectedSourceFilePath))
                {
                    try
                    {
                        File.Copy(_selectedSourceFilePath, destPath, true);
                        fileBytes = new FileInfo(destPath).Length;
                        sha256 = SecurityHelper.ComputeFileSha256(destPath);
                    }
                    catch { }
                }
                else if (File.Exists(destPath))
                {
                    fileBytes = new FileInfo(destPath).Length;
                    sha256 = SecurityHelper.ComputeFileSha256(destPath);
                }

                tep = new TepSanPhamDTO
                {
                    TenTep = fileName,
                    DuongDanTep = $"Storage/Products/{fileName}",
                    DinhDangTep = Path.GetExtension(fileName),
                    KichThuocByte = fileBytes,
                    MaHashSHA256 = sha256,
                    PhienBan = dto.PhienBan
                };
            }

            if (_spAdminService.LuuSanPham(dto, tep, out string msg))
            {
                _selectedSourceFilePath = null;
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
