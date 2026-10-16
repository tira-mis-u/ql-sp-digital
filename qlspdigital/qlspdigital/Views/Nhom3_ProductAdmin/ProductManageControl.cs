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
    public class ProductManageControl : UserControl
    {
        private readonly SanPhamAdminService _spAdminService;
        private readonly DanhMucService _danhMucService;

        private DataGridView dgvProducts = null!;
        private TextBox txtSearch = null!;
        private TextBox txtId = null!;
        private TextBox txtName = null!;
        private TextBox txtPrice = null!;
        private TextBox txtSize = null!;
        private TextBox txtVersion = null!;
        private TextBox txtFilePath = null!;
        private ComboBox cboCategory = null!;
        private CheckBox chkActive = null!;
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
            LoadCategories();
            LoadProducts();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(20);

            // Tiêu đề
            Label lblHeader = new Label
            {
                Text = "📦 QUẢN TRỊ KHO HÀNG HÓA & TỆP KỸ THUẬT SỐ",
                Location = new Point(20, 10),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            // 1. THANH TÌM KIẾM SẢN PHẨM
            Panel panelSearch = new Panel
            {
                Location = new Point(20, 45),
                Size = new Size(620, 40),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(10, 6, 10, 6)
            };

            Label lblSearch = new Label
            {
                Text = "Tìm kiếm:",
                ForeColor = Color.White,
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
            };

            txtSearch = new TextBox
            {
                Location = new Point(85, 7),
                Width = 400,
                Font = new Font("Segoe UI", 9.5f),
                BackColor = Color.FromArgb(18, 25, 35),
                ForeColor = Color.White
            };
            txtSearch.TextChanged += (s, e) => LoadProducts();

            Button btnSearch = new Button
            {
                Text = "Tìm",
                Location = new Point(500, 5),
                Size = new Size(80, 28),
                BackColor = Color.FromArgb(49, 98, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += (s, e) => LoadProducts();

            panelSearch.Controls.Add(btnSearch);
            panelSearch.Controls.Add(txtSearch);
            panelSearch.Controls.Add(lblSearch);

            // 2. BẢNG DANH SÁCH SẢN PHẨM (READ)
            dgvProducts = new DataGridView
            {
                Location = new Point(20, 95),
                Size = new Size(620, 550),
                BackgroundColor = Color.FromArgb(27, 40, 56),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(42, 71, 94),
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvProducts.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvProducts.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvProducts.EnableHeadersVisualStyles = false;

            dgvProducts.Columns.Add("MaSanPham", "Mã");
            dgvProducts.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvProducts.Columns.Add("GiaBan", "Giá bán");
            dgvProducts.Columns.Add("TenDanhMuc", "Danh mục");
            dgvProducts.Columns.Add("DungLuong", "Dung lượng");
            dgvProducts.Columns.Add("TrangThai", "Mở bán");

            dgvProducts.Columns["MaSanPham"].Width = 50;
            dgvProducts.Columns["TenSanPham"].FillWeight = 40;
            dgvProducts.Columns["GiaBan"].Width = 100;
            dgvProducts.Columns["TenDanhMuc"].Width = 110;
            dgvProducts.Columns["DungLuong"].Width = 85;
            dgvProducts.Columns["TrangThai"].Width = 70;

            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

            // 3. FORM THÔNG TIN NHẬP LIỆU BÊN PHẢI (CREATE - UPDATE - DELETE)
            Panel panelForm = new Panel
            {
                Location = new Point(660, 45),
                Size = new Size(400, 600),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            Label lblFormHeader = new Label
            {
                Text = "THÔNG TIN CHI TIẾT SẢN PHẨM",
                Location = new Point(20, 15),
                AutoSize = true,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            int y = 50;
            AddFormField(panelForm, "Mã SP:", out txtId, 20, ref y, true);
            AddFormField(panelForm, "Tên sản phẩm (*):", out txtName, 20, ref y);
            AddFormField(panelForm, "Đơn giá (VNĐ):", out txtPrice, 20, ref y);
            AddFormField(panelForm, "Dung lượng (MB):", out txtSize, 20, ref y);
            AddFormField(panelForm, "Phiên bản:", out txtVersion, 20, ref y);

            // Danh mục ComboBox
            Label lblCat = new Label { Text = "Danh mục (*):", Location = new Point(20, y), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9f) };
            cboCategory = new ComboBox { Location = new Point(135, y - 3), Width = 230, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White };
            panelForm.Controls.Add(lblCat);
            panelForm.Controls.Add(cboCategory);
            y += 35;

            // Checkbox Đang kinh doanh
            chkActive = new CheckBox { Text = "Đang mở bán trên Store", Location = new Point(135, y), AutoSize = true, ForeColor = Color.FromArgb(164, 208, 7), Checked = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
            panelForm.Controls.Add(chkActive);
            y += 30;

            // Tệp đính kèm
            Label lblFile = new Label { Text = "Tệp cài đặt:", Location = new Point(20, y), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9f) };
            txtFilePath = new TextBox { Location = new Point(135, y - 3), Width = 150, Font = new Font("Segoe UI", 9f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White, ReadOnly = true };
            btnBrowseFile = new Button { Text = "Chọn...", Location = new Point(290, y - 4), Size = new Size(75, 26), BackColor = Color.FromArgb(49, 98, 130), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8.5f), Cursor = Cursors.Hand };
            btnBrowseFile.FlatAppearance.BorderSize = 0;
            btnBrowseFile.Click += BtnBrowseFile_Click;
            panelForm.Controls.Add(lblFile);
            panelForm.Controls.Add(txtFilePath);
            panelForm.Controls.Add(btnBrowseFile);
            y += 35;

            // Mô tả
            Label lblDesc = new Label { Text = "Mô tả sản phẩm:", Location = new Point(20, y), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9f) };
            txtDescription = new TextBox { Location = new Point(135, y), Size = new Size(230, 75), Multiline = true, ScrollBars = ScrollBars.Vertical, Font = new Font("Segoe UI", 9f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White };
            panelForm.Controls.Add(lblDesc);
            panelForm.Controls.Add(txtDescription);
            y += 90;

            // Các nút bấm Thao tác CRUD
            btnNew = new Button { Text = "+ Thêm mới", Location = new Point(20, y), Size = new Size(110, 32), BackColor = Color.FromArgb(60, 75, 90), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.Click += (s, e) => ResetForm();

            btnSave = new Button { Text = "💾 Lưu", Location = new Point(140, y), Size = new Size(110, 32), BackColor = Color.FromArgb(92, 126, 16), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), Cursor = Cursors.Hand };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnDelete = new Button { Text = "🗑️ Xóa", Location = new Point(260, y), Size = new Size(105, 32), BackColor = Color.FromArgb(180, 40, 40), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), Cursor = Cursors.Hand };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;

            panelForm.Controls.Add(btnDelete);
            panelForm.Controls.Add(btnSave);
            panelForm.Controls.Add(btnNew);
            panelForm.Controls.Add(lblFormHeader);

            this.Controls.Add(panelForm);
            this.Controls.Add(dgvProducts);
            this.Controls.Add(panelSearch);
            this.Controls.Add(lblHeader);
        }

        private void AddFormField(Panel panel, string label, out TextBox txt, int x, ref int y, bool readOnly = false)
        {
            Label lbl = new Label { Text = label, Location = new Point(x, y), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9f) };
            txt = new TextBox { Location = new Point(x + 115, y - 3), Width = 230, Font = new Font("Segoe UI", 9.5f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White, ReadOnly = readOnly };
            panel.Controls.Add(lbl);
            panel.Controls.Add(txt);
            y += 35;
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

            int id = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["MaSanPham"].Value);
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

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(txtSize.Text.Trim(), out decimal size);

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

        private class CategoryItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public override string ToString() => Name;
        }
    }
}
