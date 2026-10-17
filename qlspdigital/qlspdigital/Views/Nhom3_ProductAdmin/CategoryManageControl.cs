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

        private DataGridView dgvCategories = null!;
        private Panel panelForm = null!;
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
            AdjustLayout();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadCategories();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1040, 700);
            this.BackColor = Color.FromArgb(23, 26, 33);
            this.Padding = new Padding(25);

            // Tiêu đề
            Label lblHeader = new Label();
            lblHeader.Text = "📁 QUẢN LÝ THỂ LOẠI / DANH MỤC HÀNG HÓA";
            lblHeader.Location = new Point(25, 15);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 15, FontStyle.Bold);

            // 1. BẢNG DANH MỤC (READ)
            dgvCategories = new DataGridView();
            dgvCategories.Location = new Point(25, 60);
            dgvCategories.Size = new Size(490, 615);
            dgvCategories.BackgroundColor = Color.FromArgb(27, 40, 56);
            dgvCategories.ForeColor = Color.White;
            dgvCategories.GridColor = Color.FromArgb(42, 71, 94);
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.DefaultCellStyle.BackColor = Color.FromArgb(18, 25, 35);
            dgvCategories.DefaultCellStyle.ForeColor = Color.White;
            dgvCategories.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
            dgvCategories.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCategories.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvCategories.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 45, 62);
            dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(102, 192, 244);
            dgvCategories.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 45, 62);
            dgvCategories.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(102, 192, 244);
            dgvCategories.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvCategories.EnableHeadersVisualStyles = false;

            dgvCategories.Columns.Add("MaDanhMuc", "Mã");
            dgvCategories.Columns.Add("TenDanhMuc", "Tên thể loại");
            dgvCategories.Columns.Add("MoTa", "Mô tả");
            dgvCategories.Columns.Add("SoLuongSanPham", "Số SP");
            dgvCategories.Columns.Add("TrangThai", "Hiệu lực");

            dgvCategories.Columns["MaDanhMuc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCategories.Columns["MaDanhMuc"].FillWeight = 10;
            dgvCategories.Columns["MaDanhMuc"].MinimumWidth = 45;
            dgvCategories.Columns["MaDanhMuc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCategories.Columns["TenDanhMuc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCategories.Columns["TenDanhMuc"].FillWeight = 35;
            dgvCategories.Columns["TenDanhMuc"].MinimumWidth = 110;

            dgvCategories.Columns["MoTa"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCategories.Columns["MoTa"].FillWeight = 35;
            dgvCategories.Columns["MoTa"].MinimumWidth = 120;

            dgvCategories.Columns["SoLuongSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCategories.Columns["SoLuongSanPham"].FillWeight = 10;
            dgvCategories.Columns["SoLuongSanPham"].MinimumWidth = 60;
            dgvCategories.Columns["SoLuongSanPham"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCategories.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCategories.Columns["TrangThai"].FillWeight = 10;
            dgvCategories.Columns["TrangThai"].MinimumWidth = 75;
            dgvCategories.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCategories.SelectionChanged += DgvCategories_SelectionChanged;

            // 2. KHUNG FORM NHẬP LIỆU BÊN PHẢI (CREATE - UPDATE - DELETE)
            panelForm = new Panel();
            panelForm.Location = new Point(555, 60);
            panelForm.Size = new Size(465, 615);
            panelForm.BackColor = Color.FromArgb(27, 40, 56);
            panelForm.Padding = new Padding(20);

            Label lblFormTitle = new Label();
            lblFormTitle.Text = "THÔNG TIN CHI TIẾT DANH MỤC";
            lblFormTitle.Location = new Point(20, 15);
            lblFormTitle.AutoSize = true;
            lblFormTitle.ForeColor = Color.FromArgb(102, 192, 244);
            lblFormTitle.Font = new Font("Segoe UI", 11.5f, FontStyle.Bold);
            lblFormTitle.UseMnemonic = false;

            Label lblId = new Label();
            lblId.Text = "Mã danh mục:";
            lblId.Location = new Point(20, 60);
            lblId.AutoSize = true;
            lblId.ForeColor = Color.White;
            lblId.Font = new Font("Segoe UI", 9.5f);
            lblId.UseMnemonic = false;

            txtId = new TextBox();
            txtId.Location = new Point(145, 57);
            txtId.Width = 295;
            txtId.Font = new Font("Segoe UI", 10f);
            txtId.BackColor = Color.FromArgb(18, 25, 35);
            txtId.ForeColor = Color.White;
            txtId.BorderStyle = BorderStyle.FixedSingle;
            txtId.ReadOnly = true;

            Label lblName = new Label();
            lblName.Text = "Tên thể loại (*):";
            lblName.Location = new Point(20, 105);
            lblName.AutoSize = true;
            lblName.ForeColor = Color.White;
            lblName.Font = new Font("Segoe UI", 9.5f);
            lblName.UseMnemonic = false;

            txtName = new TextBox();
            txtName.Location = new Point(145, 102);
            txtName.Width = 295;
            txtName.Font = new Font("Segoe UI", 10f);
            txtName.BackColor = Color.FromArgb(18, 25, 35);
            txtName.ForeColor = Color.White;
            txtName.BorderStyle = BorderStyle.FixedSingle;

            Label lblDesc = new Label();
            lblDesc.Text = "Mô tả diễn giải thể loại:";
            lblDesc.Location = new Point(20, 148);
            lblDesc.AutoSize = true;
            lblDesc.ForeColor = Color.White;
            lblDesc.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblDesc.UseMnemonic = false;

            txtDescription = new TextBox();
            txtDescription.Location = new Point(20, 175);
            txtDescription.Size = new Size(420, 130);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Font = new Font("Segoe UI", 10f);
            txtDescription.BackColor = Color.FromArgb(18, 25, 35);
            txtDescription.ForeColor = Color.White;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;

            chkActive = new CheckBox();
            chkActive.Text = "Kích hoạt hiển thị trên Store";
            chkActive.Location = new Point(20, 318);
            chkActive.AutoSize = true;
            chkActive.ForeColor = Color.FromArgb(164, 208, 7);
            chkActive.Checked = true;
            chkActive.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            chkActive.UseMnemonic = false;

            btnNew = new Button();
            btnNew.Text = "+ Thêm mới";
            btnNew.Location = new Point(20, 365);
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
            btnSave.Text = "💾 Lưu";
            btnSave.Location = new Point(165, 365);
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
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.Location = new Point(320, 365);
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

        private void BtnNew_Click(object? sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
