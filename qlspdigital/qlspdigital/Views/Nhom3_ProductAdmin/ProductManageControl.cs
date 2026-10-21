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
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            cboCategory.Items.Clear();
            List<DanhMucDTO> list = _danhMucService.LayTatCaDanhMuc();
            foreach (DanhMucDTO dm in list)
            {
                cboCategory.Items.Add(new CategoryItem { Id = dm.MaDanhMuc, Name = dm.TenDanhMuc });
            }
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
        }

        public void LoadProducts()
        {
            dgvProducts.Rows.Clear();
            string keyword = txtSearch.Text.Trim();
            List<SanPhamDTO> list = _spAdminService.LayDanhSachSanPham(keyword);

            foreach (SanPhamDTO sp in list)
            {
                dgvProducts.Rows.Add(sp.MaSanPham, sp.TenSanPham, FormatHelper.FormatCurrency(sp.GiaBan), sp.TenDanhMuc, $"{sp.DungLuongMB:0.#} MB", sp.DangKinhDoanh ? "Đang bán" : "Ngưng");
            }
        }

        private void DgvProducts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvProducts.SelectedRows[0];
            if (row == null || row.Index < 0) return;

            object? val = (dgvProducts.Columns.Contains("MaSanPham") ? row.Cells["MaSanPham"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;
            int id = Convert.ToInt32(val);
            List<SanPhamDTO> list = _spAdminService.LayDanhSachSanPham();
            SanPhamDTO? sp = list.Find(x => x.MaSanPham == id);

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

                TepSanPhamDTO? tep = _spAdminService.LayTepTheoSanPham(sp.MaSanPham);
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
            if (txtName.Text.Trim() == "")
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
                PhienBan = txtVersion.Text.Trim() == "" ? "1.0.0" : txtVersion.Text.Trim(),
                MaDanhMuc = catId,
                DangKinhDoanh = chkActive.Checked,
                MoTa = txtDescription.Text.Trim()
            };

            TepSanPhamDTO? tep = null;
            if (txtFilePath.Text.Trim() != "" && !txtFilePath.Text.Contains("Chưa có file"))
            {
                string fileName = txtFilePath.Text.Trim();
                string storageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage", "Products");
                if (!Directory.Exists(storageDir))
                {
                    Directory.CreateDirectory(storageDir);
                }
                string destPath = Path.Combine(storageDir, fileName);

                long fileBytes = 0;

                if (!string.IsNullOrEmpty(_selectedSourceFilePath) && File.Exists(_selectedSourceFilePath))
                {
                    try
                    {
                        File.Copy(_selectedSourceFilePath, destPath, true);
                        fileBytes = new FileInfo(destPath).Length;
                    }
                    catch { }
                }
                else if (File.Exists(destPath))
                {
                    fileBytes = new FileInfo(destPath).Length;
                }

                tep = new TepSanPhamDTO
                {
                    TenTep = fileName,
                    DuongDanTep = $"Storage/Products/{fileName}",
                    DinhDangTep = Path.GetExtension(fileName),
                    KichThuocByte = fileBytes,
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
