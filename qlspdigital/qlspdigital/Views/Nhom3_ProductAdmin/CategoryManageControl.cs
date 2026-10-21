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
            LoadCategories();
        }



        public void LoadCategories()
        {
            dgvCategories.Rows.Clear();
            List<DanhMucDTO> list = _danhMucService.LayTatCaDanhMuc();
            foreach (DanhMucDTO dm in list)
            {
                dgvCategories.Rows.Add(dm.MaDanhMuc, dm.TenDanhMuc, dm.MoTa, dm.SoLuongSanPham, dm.TrangThai ? "Hoạt động" : "Khóa");
            }
        }

        private void DgvCategories_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvCategories.SelectedRows[0];
            if (row == null || row.Index < 0) return;

            object? val = (dgvCategories.Columns.Contains("MaDanhMuc") ? row.Cells["MaDanhMuc"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;
            int id = Convert.ToInt32(val);
            List<DanhMucDTO> list = _danhMucService.LayTatCaDanhMuc();
            DanhMucDTO? dm = list.Find(x => x.MaDanhMuc == id);
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
            if (txtName.Text.Trim() == "")
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
