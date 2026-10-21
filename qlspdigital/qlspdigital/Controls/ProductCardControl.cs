using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Controls
{
    // UserControl đại diện cho một thẻ sản phẩm trên lưới Store (Steam Card style)
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public partial class ProductCardControl : UserControl
    {
        public int MaSanPham { get; private set; }

        public ProductCardControl()
        {
            InitializeComponent();
        }

        public void BindData(SanPhamDTO sp)
        {
            MaSanPham = sp.MaSanPham;
            lblTitle.Text = sp.TenSanPham;
            lblCategory.Text = $"Thể loại: {sp.TenDanhMuc}";
            lblRating.Text = $"⭐ {sp.DiemDanhGiaTrungBinh:0.0} ({sp.SoLuongDanhGia} đánh giá)";
            lblPrice.Text = FormatHelper.FormatCurrency(sp.GiaBan, allowFreeText: true);
        }

        private void BtnDetail_Click(object? sender, EventArgs e)
        {
            MainForm?.NavigateToProductDetail(MaSanPham);
        }

        private void BtnAddToCart_Click(object? sender, EventArgs e)
        {
            // Gọi phương thức thêm giỏ hàng trực tiếp từ StoreViewControl
            StoreView?.XuLyThemVaoGio(MaSanPham);
        }
    }
}
