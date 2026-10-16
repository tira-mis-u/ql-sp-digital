using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom3_ProductAdmin;
using qlspdigital.DTOs;

namespace qlspdigital.Views.Nhom3_ProductAdmin
{
    // UI 11: Quản trị danh mục / thể loại sản phẩm số
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public class CategoryManageControl : UserControl
    {
        private readonly DanhMucService _danhMucService;

        private DataGridView dgvCategories = null!;
        private TextBox txtId = null!;
        private TextBox txtName = null!;
        private TextBox txtDescription = null!;
        private CheckBox chkActive = null!;

        private Button btnNew = null!;
        private Button btnSave = null!;
        private Button btnDelete = null!;

        public CategoryManageControl()
        {
            _danhMucService = new DanhMucService();

            InitializeComponent();
            LoadCategories();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);

            // Tiêu đề
            Label lblHeader = new Label
            {
                Text = "📁 QUẢN LÝ THỂ LOẠI / DANH MỤC HÀNG HÓA",
                Location = new Point(25, 15),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15, FontStyle.Bold)
            };

            // 1. BẢNG DANH MỤC (READ)
            dgvCategories = new DataGridView
            {
                Location = new Point(25, 60),
                Size = new Size(580, 520),
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
            dgvCategories.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvCategories.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvCategories.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvCategories.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvCategories.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvCategories.EnableHeadersVisualStyles = false;

            dgvCategories.Columns.Add("MaDanhMuc", "Mã");
            dgvCategories.Columns.Add("TenDanhMuc", "Tên thể loại");
            dgvCategories.Columns.Add("MoTa", "Mô tả");
            dgvCategories.Columns.Add("SoLuongSanPham", "Số SP");
            dgvCategories.Columns.Add("TrangThai", "Hiệu lực");

            dgvCategories.Columns["MaDanhMuc"].Width = 60;
            dgvCategories.Columns["TenDanhMuc"].Width = 160;
            dgvCategories.Columns["SoLuongSanPham"].Width = 80;
            dgvCategories.Columns["TrangThai"].Width = 90;

            dgvCategories.SelectionChanged += DgvCategories_SelectionChanged;

            // 2. KHUNG FORM NHẬP LIỆU BÊN PHẢI (CREATE - UPDATE - DELETE)
            Panel panelForm = new Panel
            {
                Location = new Point(630, 60),
                Size = new Size(400, 360),
                BackColor = Color.FromArgb(27, 40, 56),
                Padding = new Padding(20)
            };

            Label lblFormTitle = new Label
            {
                Text = "THÔNG TIN DANH MỤC",
                Location = new Point(20, 15),
                AutoSize = true,
                ForeColor = Color.FromArgb(102, 192, 244),
                Font = new Font("Segoe UI", 11.5f, FontStyle.Bold)
            };

            Label lblId = new Label { Text = "Mã danh mục:", Location = new Point(20, 55), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txtId = new TextBox { Location = new Point(135, 52), Width = 230, Font = new Font("Segoe UI", 9.5f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White, ReadOnly = true };

            Label lblName = new Label { Text = "Tên thể loại (*):", Location = new Point(20, 95), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txtName = new TextBox { Location = new Point(135, 92), Width = 230, Font = new Font("Segoe UI", 9.5f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White };

            Label lblDesc = new Label { Text = "Mô tả diễn giải:", Location = new Point(20, 135), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f) };
            txtDescription = new TextBox { Location = new Point(135, 132), Size = new Size(230, 60), Multiline = true, Font = new Font("Segoe UI", 9.5f), BackColor = Color.FromArgb(18, 25, 35), ForeColor = Color.White };

            chkActive = new CheckBox { Text = "Kích hoạt hiển thị trên Store", Location = new Point(135, 205), AutoSize = true, ForeColor = Color.FromArgb(164, 208, 7), Checked = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };

            btnNew = new Button { Text = "+ Thêm mới", Location = new Point(20, 260), Size = new Size(110, 32), BackColor = Color.FromArgb(60, 75, 90), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.Click += (s, e) => ResetForm();

            btnSave = new Button { Text = "💾 Lưu", Location = new Point(140, 260), Size = new Size(110, 32), BackColor = Color.FromArgb(92, 126, 16), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), Cursor = Cursors.Hand };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnDelete = new Button { Text = "🗑️ Xóa", Location = new Point(260, 260), Size = new Size(105, 32), BackColor = Color.FromArgb(180, 40, 40), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), Cursor = Cursors.Hand };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;

            panelForm.Controls.Add(btnDelete);
            panelForm.Controls.Add(btnSave);
            panelForm.Controls.Add(btnNew);
            panelForm.Controls.Add(chkActive);
            panelForm.Controls.Add(txtDescription);
            panelForm.Controls.Add(lblDesc);
            panelForm.Controls.Add(txtName);
            panelForm.Controls.Add(lblName);
            panelForm.Controls.Add(txtId);
            panelForm.Controls.Add(lblId);
            panelForm.Controls.Add(lblFormTitle);

            this.Controls.Add(panelForm);
            this.Controls.Add(dgvCategories);
            this.Controls.Add(lblHeader);
        }

        public void LoadCategories()
        {
            dgvCategories.Rows.Clear();
            var list = _danhMucService.LayTatCaDanhMuc();
            foreach (var dm in list)
            {
                dgvCategories.Rows.Add(dm.MaDanhMuc, dm.TenDanhMuc, dm.MoTa, dm.SoLuongSanPham, dm.TrangThai ? "Hoạt động" : "Khóa");
            }
        }

        private void DgvCategories_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvCategories.SelectedRows[0].Cells["MaDanhMuc"].Value);
            var list = _danhMucService.LayTatCaDanhMuc();
            var dm = list.Find(x => x.MaDanhMuc == id);
            if (dm != null)
            {
                txtId.Text = dm.MaDanhMuc.ToString();
                txtName.Text = dm.TenDanhMuc;
                txtDescription.Text = dm.MoTa ?? string.Empty;
                chkActive.Checked = dm.TrangThai;
            }
        }

        private void ResetForm()
        {
            txtId.Clear();
            txtName.Clear();
            txtDescription.Clear();
            chkActive.Checked = true;
            txtName.Focus();
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int.TryParse(txtId.Text, out int id);
            DanhMucDTO dto = new DanhMucDTO
            {
                MaDanhMuc = id,
                TenDanhMuc = txtName.Text.Trim(),
                MoTa = txtDescription.Text.Trim(),
                TrangThai = chkActive.Checked
            };

            if (_danhMucService.LuuDanhMuc(dto, out string msg))
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategories();
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
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa danh mục [{txtName.Text}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_danhMucService.XoaDanhMuc(id, out string msg))
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                    LoadCategories();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
