using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom3_ProductAdmin;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom3_ProductAdmin
{
    // UI 11: Quản trị danh mục / thể loại sản phẩm số
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public partial class CategoryManageControl : UserControl
    {
        private readonly DanhMucService _danhMucService;

        public CategoryManageControl()
        {
            _danhMucService = new DanhMucService();

            InitializeComponent();
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadCategories();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelForm == null || dgvCategories == null || txtDescription == null || btnNew == null || chkActive == null || txtId == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            int rightW = Math.Clamp((int)(totalW * 0.40), 380, 520);
            int leftW = Math.Max(260, totalW - rightW - 65); // margin 25 left + 15 gap + 25 right
            int contentH = Math.Max(220, totalH - 85);

            // 1. DataGridView
            dgvCategories.Location = new Point(25, 60);
            dgvCategories.Size = new Size(leftW, contentH);

            // 2. Right Form Panel
            panelForm.Location = new Point(25 + leftW + 15, 60);
            panelForm.Size = new Size(rightW, contentH);

            int formW = panelForm.ClientSize.Width;
            int formH = panelForm.ClientSize.Height;
            int inputW = Math.Max(200, formW - 145 - 25);
            txtId.Width = inputW;
            txtName.Width = inputW;
            txtDescription.Width = Math.Max(200, formW - 40);

            int descH = Math.Clamp(formH - 280, 65, 180);
            txtDescription.Height = descH;
            chkActive.Location = new Point(20, txtDescription.Bottom + 8);
            int btnY = chkActive.Bottom + 12;

            int btnW = Math.Max(90, (formW - 40 - 20) / 3);
            btnNew.Location = new Point(20, btnY);
            btnNew.Size = new Size(btnW, 36);

            btnSave.Location = new Point(20 + btnW + 10, btnY);
            btnSave.Size = new Size(btnW, 36);

            btnDelete.Location = new Point(20 + (btnW + 10) * 2, btnY);
            btnDelete.Size = new Size(btnW, 36);
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

            var row = dgvCategories.SelectedRows[0];
            if (row == null || row.Index < 0) return;

            var val = (dgvCategories.Columns.Contains("MaDanhMuc") ? row.Cells["MaDanhMuc"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;
            int id = Convert.ToInt32(val);
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

            if (txtName.Text.Trim().Length > 100)
            {
                MessageBox.Show("Tên danh mục không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void BtnNew_Click(object? sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
