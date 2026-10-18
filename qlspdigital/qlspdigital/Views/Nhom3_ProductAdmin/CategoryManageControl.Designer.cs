using System.Drawing;
using System.Windows.Forms;

namespace qlspdigital.Views.Nhom3_ProductAdmin
{
    partial class CategoryManageControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.colMaDanhMuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenDanhMuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelForm = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(25, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(437, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "QUẢN LÝ THỂ LOẠI / DANH MỤC HÀNG HÓA";

            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.BackgroundColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.dgvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCategories.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCategories.ColumnHeadersHeight = 32;
            this.dgvCategories.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaDanhMuc,
            this.colTenDanhMuc,
            this.colMoTa,
            this.colSoLuongSanPham,
            this.colTrangThai});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCategories.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCategories.EnableHeadersVisualStyles = false;
            this.dgvCategories.GridColor = System.Drawing.Color.FromArgb(42, 71, 94);
            this.dgvCategories.Location = new System.Drawing.Point(25, 60);
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(490, 615);
            this.dgvCategories.TabIndex = 1;
            this.dgvCategories.SelectionChanged += new System.EventHandler(this.DgvCategories_SelectionChanged);
            // 
            // colMaDanhMuc
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colMaDanhMuc.DefaultCellStyle = dataGridViewCellStyle3;
            this.colMaDanhMuc.FillWeight = 10F;
            this.colMaDanhMuc.HeaderText = "Mã";
            this.colMaDanhMuc.MinimumWidth = 45;
            this.colMaDanhMuc.Name = "MaDanhMuc";
            this.colMaDanhMuc.ReadOnly = true;
            // 
            // colTenDanhMuc
            // 
            this.colTenDanhMuc.FillWeight = 35F;
            this.colTenDanhMuc.HeaderText = "Tên thể loại";
            this.colTenDanhMuc.MinimumWidth = 110;
            this.colTenDanhMuc.Name = "TenDanhMuc";
            this.colTenDanhMuc.ReadOnly = true;
            // 
            // colMoTa
            // 
            this.colMoTa.FillWeight = 35F;
            this.colMoTa.HeaderText = "Mô tả";
            this.colMoTa.MinimumWidth = 120;
            this.colMoTa.Name = "MoTa";
            this.colMoTa.ReadOnly = true;
            // 
            // colSoLuongSanPham
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSoLuongSanPham.DefaultCellStyle = dataGridViewCellStyle4;
            this.colSoLuongSanPham.FillWeight = 10F;
            this.colSoLuongSanPham.HeaderText = "Số SP";
            this.colSoLuongSanPham.MinimumWidth = 60;
            this.colSoLuongSanPham.Name = "SoLuongSanPham";
            this.colSoLuongSanPham.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.FillWeight = 10F;
            this.colTrangThai.HeaderText = "Hiệu lực";
            this.colTrangThai.MinimumWidth = 75;
            this.colTrangThai.Name = "TrangThai";
            this.colTrangThai.ReadOnly = true;

            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelForm.Controls.Add(this.btnDelete);
            this.panelForm.Controls.Add(this.btnSave);
            this.panelForm.Controls.Add(this.btnNew);
            this.panelForm.Controls.Add(this.chkActive);
            this.panelForm.Controls.Add(this.txtDescription);
            this.panelForm.Controls.Add(this.lblDesc);
            this.panelForm.Controls.Add(this.txtName);
            this.panelForm.Controls.Add(this.lblName);
            this.panelForm.Controls.Add(this.txtId);
            this.panelForm.Controls.Add(this.lblId);
            this.panelForm.Controls.Add(this.lblFormTitle);
            this.panelForm.Location = new System.Drawing.Point(555, 60);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(20);
            this.panelForm.Size = new System.Drawing.Size(465, 615);
            this.panelForm.TabIndex = 2;

            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblFormTitle.Location = new System.Drawing.Point(20, 15);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(262, 21);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "THÔNG TIN CHI TIẾT DANH MỤC";
            this.lblFormTitle.UseMnemonic = false;

            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblId.ForeColor = System.Drawing.Color.White;
            this.lblId.Location = new System.Drawing.Point(20, 60);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(95, 17);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "Mã danh mục:";
            this.lblId.UseMnemonic = false;

            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtId.ForeColor = System.Drawing.Color.White;
            this.txtId.Location = new System.Drawing.Point(145, 57);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(295, 25);
            this.txtId.TabIndex = 2;

            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(20, 105);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(98, 17);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Tên thể loại (*):";
            this.lblName.UseMnemonic = false;

            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(145, 102);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(295, 25);
            this.txtName.TabIndex = 4;

            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDesc.ForeColor = System.Drawing.Color.White;
            this.lblDesc.Location = new System.Drawing.Point(20, 148);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(155, 17);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "Mô tả diễn giải thể loại:";
            this.lblDesc.UseMnemonic = false;

            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.ForeColor = System.Drawing.Color.White;
            this.txtDescription.Location = new System.Drawing.Point(20, 175);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(420, 130);
            this.txtDescription.TabIndex = 6;

            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.chkActive.ForeColor = System.Drawing.Color.FromArgb(164, 208, 7);
            this.chkActive.Location = new System.Drawing.Point(20, 318);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(199, 21);
            this.chkActive.TabIndex = 7;
            this.chkActive.Text = "Kích hoạt hiển thị trên Store";
            this.chkActive.UseMnemonic = false;
            this.chkActive.UseVisualStyleBackColor = true;

            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(60, 75, 90);
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(20, 365);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(130, 38);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "+ Thêm mới";
            this.btnNew.UseMnemonic = false;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);

            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(90, 160, 45);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(165, 365);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 38);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseMnemonic = false;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(320, 365);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 38);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseMnemonic = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // 
            // CategoryManageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.dgvCategories);
            this.Controls.Add(this.lblHeader);
            this.Name = "CategoryManageControl";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Size = new System.Drawing.Size(1040, 700);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaDanhMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenDanhMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}
