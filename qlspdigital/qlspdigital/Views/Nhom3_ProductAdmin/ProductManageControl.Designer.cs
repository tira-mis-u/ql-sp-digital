using System.Drawing;
using System.Windows.Forms;

namespace qlspdigital.Views.Nhom3_ProductAdmin
{
    partial class ProductManageControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.colMaSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenDanhMuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDungLuongMB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelForm = new System.Windows.Forms.Panel();
            this.lblFormHeader = new System.Windows.Forms.Label();
            this.lblFieldId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblFieldPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblFieldSize = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.lblFieldVersion = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblCat = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(437, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "QUẢN TRỊ KHO HÀNG HÓA & TỆP KỸ THUẬT SỐ";
            this.lblHeader.UseMnemonic = false;

            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.lblSearch);
            this.panelSearch.Location = new System.Drawing.Point(20, 45);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.panelSearch.Size = new System.Drawing.Size(490, 42);
            this.panelSearch.TabIndex = 1;

            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(12, 12);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(70, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm:";
            this.lblSearch.UseMnemonic = false;

            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(95, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(285, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);

            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(385, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseMnemonic = false;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(30, 45, 62);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProducts.ColumnHeadersHeight = 34;
            this.dgvProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSanPham,
            this.colTenSanPham,
            this.colGiaBan,
            this.colTenDanhMuc,
            this.colDungLuongMB,
            this.colTrangThai});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProducts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProducts.EnableHeadersVisualStyles = false;
            this.dgvProducts.GridColor = System.Drawing.Color.FromArgb(42, 71, 94);
            this.dgvProducts.Location = new System.Drawing.Point(20, 95);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(490, 580);
            this.dgvProducts.TabIndex = 2;
            this.dgvProducts.SelectionChanged += new System.EventHandler(this.DgvProducts_SelectionChanged);
            // 
            // colMaSanPham
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colMaSanPham.DefaultCellStyle = dataGridViewCellStyle3;
            this.colMaSanPham.FillWeight = 5F;
            this.colMaSanPham.HeaderText = "Mã";
            this.colMaSanPham.MinimumWidth = 32;
            this.colMaSanPham.Name = "MaSanPham";
            this.colMaSanPham.ReadOnly = true;
            // 
            // colTenSanPham
            // 
            this.colTenSanPham.FillWeight = 24F;
            this.colTenSanPham.HeaderText = "Tên sản phẩm";
            this.colTenSanPham.MinimumWidth = 155;
            this.colTenSanPham.Name = "TenSanPham";
            this.colTenSanPham.ReadOnly = true;
            // 
            // colGiaBan
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colGiaBan.DefaultCellStyle = dataGridViewCellStyle4;
            this.colGiaBan.FillWeight = 14F;
            this.colGiaBan.HeaderText = "Giá bán";
            this.colGiaBan.MinimumWidth = 85;
            this.colGiaBan.Name = "GiaBan";
            this.colGiaBan.ReadOnly = true;
            // 
            // colTenDanhMuc
            // 
            this.colTenDanhMuc.FillWeight = 33F;
            this.colTenDanhMuc.HeaderText = "Danh mục";
            this.colTenDanhMuc.MinimumWidth = 185;
            this.colTenDanhMuc.Name = "TenDanhMuc";
            this.colTenDanhMuc.ReadOnly = true;
            // 
            // colDungLuongMB
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colDungLuongMB.DefaultCellStyle = dataGridViewCellStyle5;
            this.colDungLuongMB.FillWeight = 14F;
            this.colDungLuongMB.HeaderText = "Dung lượng";
            this.colDungLuongMB.MinimumWidth = 92;
            this.colDungLuongMB.Name = "DungLuongMB";
            this.colDungLuongMB.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.FillWeight = 10F;
            this.colTrangThai.HeaderText = "Mở bán";
            this.colTrangThai.MinimumWidth = 70;
            this.colTrangThai.Name = "TrangThai";
            this.colTrangThai.ReadOnly = true;

            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.panelForm.Controls.Add(this.btnDelete);
            this.panelForm.Controls.Add(this.btnSave);
            this.panelForm.Controls.Add(this.btnNew);
            this.panelForm.Controls.Add(this.txtDescription);
            this.panelForm.Controls.Add(this.lblDesc);
            this.panelForm.Controls.Add(this.btnBrowseFile);
            this.panelForm.Controls.Add(this.txtFilePath);
            this.panelForm.Controls.Add(this.lblFile);
            this.panelForm.Controls.Add(this.chkActive);
            this.panelForm.Controls.Add(this.cboCategory);
            this.panelForm.Controls.Add(this.lblCat);
            this.panelForm.Controls.Add(this.txtVersion);
            this.panelForm.Controls.Add(this.lblFieldVersion);
            this.panelForm.Controls.Add(this.txtSize);
            this.panelForm.Controls.Add(this.lblFieldSize);
            this.panelForm.Controls.Add(this.txtPrice);
            this.panelForm.Controls.Add(this.lblFieldPrice);
            this.panelForm.Controls.Add(this.txtName);
            this.panelForm.Controls.Add(this.lblFieldName);
            this.panelForm.Controls.Add(this.txtId);
            this.panelForm.Controls.Add(this.lblFieldId);
            this.panelForm.Controls.Add(this.lblFormHeader);
            this.panelForm.Location = new System.Drawing.Point(555, 45);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(20);
            this.panelForm.Size = new System.Drawing.Size(465, 630);
            this.panelForm.TabIndex = 3;

            // 
            // lblFormHeader
            // 
            this.lblFormHeader.AutoSize = true;
            this.lblFormHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFormHeader.ForeColor = System.Drawing.Color.FromArgb(102, 192, 244);
            this.lblFormHeader.Location = new System.Drawing.Point(20, 15);
            this.lblFormHeader.Name = "lblFormHeader";
            this.lblFormHeader.Size = new System.Drawing.Size(252, 20);
            this.lblFormHeader.TabIndex = 0;
            this.lblFormHeader.Text = "THÔNG TIN CHI TIẾT SẢN PHẨM";
            this.lblFormHeader.UseMnemonic = false;

            // 
            // lblFieldId
            // 
            this.lblFieldId.AutoSize = true;
            this.lblFieldId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFieldId.ForeColor = System.Drawing.Color.White;
            this.lblFieldId.Location = new System.Drawing.Point(20, 50);
            this.lblFieldId.Name = "lblFieldId";
            this.lblFieldId.Size = new System.Drawing.Size(44, 15);
            this.lblFieldId.TabIndex = 1;
            this.lblFieldId.Text = "Mã SP:";
            this.lblFieldId.UseMnemonic = false;

            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtId.ForeColor = System.Drawing.Color.White;
            this.txtId.Location = new System.Drawing.Point(155, 47);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(285, 25);
            this.txtId.TabIndex = 2;

            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFieldName.ForeColor = System.Drawing.Color.White;
            this.lblFieldName.Location = new System.Drawing.Point(20, 85);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(111, 17);
            this.lblFieldName.TabIndex = 3;
            this.lblFieldName.Text = "Tên sản phẩm (*):";
            this.lblFieldName.UseMnemonic = false;

            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(155, 82);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(285, 25);
            this.txtName.TabIndex = 4;

            // 
            // lblFieldPrice
            // 
            this.lblFieldPrice.AutoSize = true;
            this.lblFieldPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFieldPrice.ForeColor = System.Drawing.Color.White;
            this.lblFieldPrice.Location = new System.Drawing.Point(20, 120);
            this.lblFieldPrice.Name = "lblFieldPrice";
            this.lblFieldPrice.Size = new System.Drawing.Size(95, 17);
            this.lblFieldPrice.TabIndex = 5;
            this.lblFieldPrice.Text = "Đơn giá (VNĐ):";
            this.lblFieldPrice.UseMnemonic = false;

            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPrice.ForeColor = System.Drawing.Color.White;
            this.txtPrice.Location = new System.Drawing.Point(155, 117);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(285, 25);
            this.txtPrice.TabIndex = 6;

            // 
            // lblFieldSize
            // 
            this.lblFieldSize.AutoSize = true;
            this.lblFieldSize.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFieldSize.ForeColor = System.Drawing.Color.White;
            this.lblFieldSize.Location = new System.Drawing.Point(20, 155);
            this.lblFieldSize.Name = "lblFieldSize";
            this.lblFieldSize.Size = new System.Drawing.Size(111, 17);
            this.lblFieldSize.TabIndex = 7;
            this.lblFieldSize.Text = "Dung lượng (MB):";
            this.lblFieldSize.UseMnemonic = false;

            // 
            // txtSize
            // 
            this.txtSize.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSize.ForeColor = System.Drawing.Color.White;
            this.txtSize.Location = new System.Drawing.Point(155, 152);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(285, 25);
            this.txtSize.TabIndex = 8;

            // 
            // lblFieldVersion
            // 
            this.lblFieldVersion.AutoSize = true;
            this.lblFieldVersion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFieldVersion.ForeColor = System.Drawing.Color.White;
            this.lblFieldVersion.Location = new System.Drawing.Point(20, 190);
            this.lblFieldVersion.Name = "lblFieldVersion";
            this.lblFieldVersion.Size = new System.Drawing.Size(68, 17);
            this.lblFieldVersion.TabIndex = 9;
            this.lblFieldVersion.Text = "Phiên bản:";
            this.lblFieldVersion.UseMnemonic = false;

            // 
            // txtVersion
            // 
            this.txtVersion.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVersion.ForeColor = System.Drawing.Color.White;
            this.txtVersion.Location = new System.Drawing.Point(155, 187);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(285, 25);
            this.txtVersion.TabIndex = 10;

            // 
            // lblCat
            // 
            this.lblCat.AutoSize = true;
            this.lblCat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCat.ForeColor = System.Drawing.Color.White;
            this.lblCat.Location = new System.Drawing.Point(20, 225);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(89, 17);
            this.lblCat.TabIndex = 11;
            this.lblCat.Text = "Danh mục (*):";
            this.lblCat.UseMnemonic = false;

            // 
            // cboCategory
            // 
            this.cboCategory.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCategory.ForeColor = System.Drawing.Color.White;
            this.cboCategory.Location = new System.Drawing.Point(155, 222);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(285, 25);
            this.cboCategory.TabIndex = 12;

            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.chkActive.ForeColor = System.Drawing.Color.FromArgb(164, 208, 7);
            this.chkActive.Location = new System.Drawing.Point(155, 260);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(176, 21);
            this.chkActive.TabIndex = 13;
            this.chkActive.Text = "Đang mở bán trên Store";
            this.chkActive.UseMnemonic = false;
            this.chkActive.UseVisualStyleBackColor = true;

            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFile.ForeColor = System.Drawing.Color.White;
            this.lblFile.Location = new System.Drawing.Point(20, 290);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(73, 17);
            this.lblFile.TabIndex = 14;
            this.lblFile.Text = "Tệp cài đặt:";
            this.lblFile.UseMnemonic = false;

            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilePath.ForeColor = System.Drawing.Color.White;
            this.txtFilePath.Location = new System.Drawing.Point(155, 287);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(195, 25);
            this.txtFilePath.TabIndex = 15;

            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.BackColor = System.Drawing.Color.FromArgb(49, 98, 130);
            this.btnBrowseFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowseFile.FlatAppearance.BorderSize = 0;
            this.btnBrowseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowseFile.ForeColor = System.Drawing.Color.White;
            this.btnBrowseFile.Location = new System.Drawing.Point(355, 285);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(85, 28);
            this.btnBrowseFile.TabIndex = 16;
            this.btnBrowseFile.Text = "Chọn...";
            this.btnBrowseFile.UseMnemonic = false;
            this.btnBrowseFile.UseVisualStyleBackColor = false;
            this.btnBrowseFile.Click += new System.EventHandler(this.BtnBrowseFile_Click);

            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDesc.ForeColor = System.Drawing.Color.White;
            this.lblDesc.Location = new System.Drawing.Point(20, 325);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(161, 17);
            this.lblDesc.TabIndex = 17;
            this.lblDesc.Text = "Mô tả chi tiết sản phẩm:";
            this.lblDesc.UseMnemonic = false;

            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(18, 25, 35);
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.ForeColor = System.Drawing.Color.White;
            this.txtDescription.Location = new System.Drawing.Point(20, 349);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(420, 115);
            this.txtDescription.TabIndex = 18;

            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(60, 75, 90);
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(20, 480);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(130, 38);
            this.btnNew.TabIndex = 19;
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
            this.btnSave.Location = new System.Drawing.Point(165, 480);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 38);
            this.btnSave.TabIndex = 20;
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
            this.btnDelete.Location = new System.Drawing.Point(320, 480);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 38);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseMnemonic = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // 
            // ProductManageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(23, 26, 33);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.lblHeader);
            this.Name = "ProductManageControl";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(1040, 700);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.AdjustLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenDanhMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDungLuongMB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label lblFormHeader;
        private System.Windows.Forms.Label lblFieldId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblFieldPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblFieldSize;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Label lblFieldVersion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblCat;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;

        #region Windows Form Designer - Layout & UI Helpers

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (panelForm == null || dgvProducts == null || txtDescription == null || btnNew == null || txtId == null) return;
            int totalW = System.Math.Max(700, this.ClientSize.Width);
            int totalH = System.Math.Max(400, this.ClientSize.Height);

            int rightW = System.Math.Clamp((int)(totalW * 0.34), 345, 420);
            int leftW = System.Math.Max(260, totalW - rightW - 65); // margin 25 left + 15 gap + 25 right
            int contentH = System.Math.Max(220, totalH - 120);

            // 1. Search Bar
            panelSearch.Location = new System.Drawing.Point(25, 55);
            panelSearch.Size = new System.Drawing.Size(leftW, 42);
            lblSearch.Location = new System.Drawing.Point(12, 12);
            txtSearch.Location = new System.Drawing.Point(lblSearch.Right + 12, 8);
            btnSearch.Size = new System.Drawing.Size(95, 30);
            btnSearch.Location = new System.Drawing.Point(leftW - 105, 6);
            txtSearch.Width = System.Math.Max(120, btnSearch.Left - txtSearch.Left - 12);

            // 2. DataGridView
            dgvProducts.Location = new System.Drawing.Point(25, 105);
            dgvProducts.Size = new System.Drawing.Size(leftW, contentH);

            // 3. Form Panel
            panelForm.Location = new System.Drawing.Point(25 + leftW + 15, 55);
            panelForm.Size = new System.Drawing.Size(rightW, contentH + 50);

            // 4. Form Controls inside panelForm
            int fieldInputW = System.Math.Max(150, rightW - 175);
            txtId.Width = fieldInputW;
            txtName.Width = fieldInputW;
            txtPrice.Width = fieldInputW;
            txtSize.Width = fieldInputW;
            txtVersion.Width = fieldInputW;
            cboCategory.Width = fieldInputW;

            int browseW = 75;
            btnBrowseFile.Size = new System.Drawing.Size(browseW, txtFilePath.Height + 2);
            txtFilePath.Width = System.Math.Max(60, fieldInputW - browseW - 10);
            btnBrowseFile.Location = new System.Drawing.Point(txtFilePath.Left + txtFilePath.Width + 10, txtFilePath.Top - 1);
            txtDescription.Width = rightW - 40;

            // Dynamically stretch textarea vertically and pin buttons cleanly
            int formH = panelForm.ClientSize.Height;
            int descH = System.Math.Clamp(formH - txtDescription.Top - 65, 75, 180);
            txtDescription.Height = descH;
            int btnY = txtDescription.Bottom + 12;

            int btnW = System.Math.Max(90, (rightW - 40 - 20) / 3);
            btnNew.Location = new System.Drawing.Point(20, btnY);
            btnNew.Size = new System.Drawing.Size(btnW, 38);
            btnSave.Location = new System.Drawing.Point(20 + btnW + 10, btnY);
            btnSave.Size = new System.Drawing.Size(btnW, 38);
            btnDelete.Location = new System.Drawing.Point(20 + (btnW + 10) * 2, btnY);
            btnDelete.Size = new System.Drawing.Size(btnW, 38);
        }

        #endregion
    }
}
