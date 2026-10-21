using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom1_Store;
using qlspdigital.BLL.Nhom2_Cart;
using qlspdigital.BLL.Nhom3_ProductAdmin;
using qlspdigital.Controls;
using qlspdigital.DTOs;
using qlspdigital.Shared;
using qlspdigital.Views.Main;

namespace qlspdigital.Views.Nhom1_Store
{
    // UI 2: Trang chủ Cửa hàng số - Tìm kiếm, lọc theo danh mục & lưới sản phẩm
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public partial class StoreViewControl : UserControl
    {
        private readonly SanPhamService _sanPhamService;
        private readonly DanhMucService _danhMucService;
        private readonly GioHangService _gioHangService;


        public StoreViewControl()
        {
            _sanPhamService = new SanPhamService();
            _danhMucService = new DanhMucService();
            _gioHangService = new GioHangService();

            InitializeComponent();
            LoadCategories();
            LoadProducts();
        }


        private void LoadCategories()
        {
            try
            {
                List<DanhMucDTO> list = _danhMucService.LayTatCaDanhMuc();
                cboCategory.Items.Clear();
                cboCategory.Items.Add(new ComboBoxItem { Text = "--- Tất cả thể loại ---", Value = 0 });

                foreach (DanhMucDTO dm in list)
                {
                    cboCategory.Items.Add(new ComboBoxItem { Text = dm.TenDanhMuc, Value = dm.MaDanhMuc });
                }

                if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadProducts()
        {
            flowProducts.Controls.Clear();
            string tuKhoa = txtSearch.Text.Trim();
            int? maDanhMuc = null;

            if (cboCategory.SelectedItem is ComboBoxItem selectedItem && (int)selectedItem.Value > 0)
            {
                maDanhMuc = (int)selectedItem.Value;
            }

            try
            {
                List<SanPhamDTO> products = _sanPhamService.LayDanhSachSanPham(tuKhoa, maDanhMuc);
                lblTotalCount.Text = $"Hiển thị: {products.Count} sản phẩm";

                if (products.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        Text = "Không tìm thấy sản phẩm kỹ thuật số nào phù hợp!",
                        ForeColor = Color.Gray,
                        Font = new Font("Segoe UI", 12, FontStyle.Italic),
                        AutoSize = true,
                        Padding = new Padding(20)
                    };
                    flowProducts.Controls.Add(lblEmpty);
                    return;
                }

                foreach (SanPhamDTO sp in products)
                {
                    ProductCardControl card = new ProductCardControl();
                    card.BindData(sp);

                    // Chuyển sang trang chi tiết sản phẩm
                    card.MainForm = MainForm;
                    card.StoreView = this;

                    flowProducts.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void XuLyThemVaoGio(int maSanPham)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_gioHangService.ThemVaoGioHang(UserSession.CurrentUserId, maSanPham, 1, out string msg))
            {
                MessageBox.Show(msg, "Giỏ hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainForm?.UpdateCartBadge();
            }
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadProducts();
        }

        private void CboCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadProducts();
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            LoadProducts();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            LoadProducts();
        }

        private class ComboBoxItem
        {
            public string Text { get; set; } = string.Empty;
            public object Value { get; set; } = 0;
            public override string ToString() => Text;
        }
    }
}
