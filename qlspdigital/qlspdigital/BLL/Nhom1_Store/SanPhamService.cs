using System.Collections.Generic;
using qlspdigital.DAL.Nhom1_Store;
using qlspdigital.DAL.Nhom5_Orders;
using qlspdigital.DTOs;

namespace qlspdigital.BLL.Nhom1_Store
{
    // Service xử lý nghiệp vụ Cửa hàng & Chi tiết sản phẩm
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public class SanPhamService
    {
        private readonly SanPhamRepository _sanPhamRepo;

        public SanPhamService()
        {
            _sanPhamRepo = new SanPhamRepository();
        }

        public List<SanPhamDTO> LayDanhSachSanPham(string? tuKhoa = null, int? maDanhMuc = null)
        {
            return _sanPhamRepo.LayDanhSachSanPham(tuKhoa, maDanhMuc);
        }

        public SanPhamDTO? LayChiTietSanPham(int maSanPham)
        {
            if (maSanPham <= 0) return null;
            return _sanPhamRepo.LayChiTietSanPham(maSanPham);
        }
    }

    // Service xử lý nghiệp vụ Đánh giá & Nhận xét sản phẩm
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public class DanhGiaService
    {
        private readonly DanhGiaRepository _danhGiaRepo;
        private readonly ThuVienRepository _thuVienRepo;

        public DanhGiaService()
        {
            _danhGiaRepo = new DanhGiaRepository();
            _thuVienRepo = new ThuVienRepository();
        }

        // Kiểm tra xem người dùng đã thực sự mua và sở hữu sản phẩm này trong Thư viện chưa (Verified Purchase)
        public bool DaMuaSanPham(int maNguoiDung, int maSanPham)
        {
            if (maNguoiDung <= 0 || maSanPham <= 0) return false;
            return _thuVienRepo.DaSoHuu(maNguoiDung, maSanPham);
        }

        public List<DanhGiaDTO> LayDanhGiaTheoSanPham(int maSanPham)
        {
            if (maSanPham <= 0) return new List<DanhGiaDTO>();
            return _danhGiaRepo.LayDanhGiaTheoSanPham(maSanPham);
        }

        public DanhGiaDTO? LayDanhGiaCuaUser(int maNguoiDung, int maSanPham)
        {
            if (maNguoiDung <= 0 || maSanPham <= 0) return null;
            return _danhGiaRepo.LayDanhGiaCuaUser(maNguoiDung, maSanPham);
        }

        public bool LuuDanhGia(int maNguoiDung, int maSanPham, int soSao, string binhLuan, out string thongBao)
        {
            if (maNguoiDung <= 0)
            {
                thongBao = "Vui lòng đăng nhập để gửi đánh giá sản phẩm!";
                return false;
            }

            if (soSao < 1 || soSao > 5)
            {
                thongBao = "Số sao đánh giá phải từ 1 đến 5 sao!";
                return false;
            }

            if (string.IsNullOrEmpty(binhLuan) || binhLuan.Trim() == "")
            {
                thongBao = "Vui lòng nhập nội dung nhận xét của bạn!";
                return false;
            }

            // Quy tắc Verified Purchase: Chỉ tài khoản đã sở hữu sản phẩm trong Thư viện mới được phép đánh giá
            if (!_thuVienRepo.DaSoHuu(maNguoiDung, maSanPham))
            {
                thongBao = "Bạn chưa sở hữu sản phẩm này trong Thư viện! Chỉ những khách hàng đã thực sự mua sản phẩm mới có quyền gửi nhận xét & đánh giá (Verified Purchase).";
                return false;
            }

            var existing = _danhGiaRepo.LayDanhGiaCuaUser(maNguoiDung, maSanPham);
            if (existing != null)
            {
                // Cập nhật đánh giá cũ
                existing.SoSao = soSao;
                existing.BinhLuan = binhLuan.Trim();
                bool ok = _danhGiaRepo.CapNhatDanhGia(existing);
                thongBao = ok ? "Đã cập nhật đánh giá thành công!" : "Cập nhật đánh giá thất bại.";
                return ok;
            }
            else
            {
                // Tạo mới đánh giá
                var dto = new DanhGiaDTO
                {
                    MaNguoiDung = maNguoiDung,
                    MaSanPham = maSanPham,
                    SoSao = soSao,
                    BinhLuan = binhLuan.Trim()
                };
                bool ok = _danhGiaRepo.ThemDanhGia(dto);
                thongBao = ok ? "Cảm ơn bạn đã gửi đánh giá sản phẩm!" : "Gửi đánh giá thất bại.";
                return ok;
            }
        }

        public bool XoaDanhGia(int maDanhGia, int maNguoiDung, out string thongBao)
        {
            bool ok = _danhGiaRepo.XoaDanhGia(maDanhGia, maNguoiDung);
            thongBao = ok ? "Đã xóa đánh giá của bạn!" : "Xóa đánh giá thất bại.";
            return ok;
        }
    }
}
