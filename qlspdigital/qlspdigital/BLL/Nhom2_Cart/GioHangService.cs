using System.Collections.Generic;
using qlspdigital.DAL.Nhom2_Cart;
using qlspdigital.DAL.Nhom5_Orders;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.BLL.Nhom2_Cart
{
    // Service xử lý nghiệp vụ giỏ hàng mua sắm
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class GioHangService
    {
        private readonly GioHangRepository _gioHangRepo;

        public GioHangService()
        {
            _gioHangRepo = new GioHangRepository();
        }

        public List<GioHangItemDTO> LayGioHang(int maNguoiDung)
        {
            if (maNguoiDung <= 0) return new List<GioHangItemDTO>();
            return _gioHangRepo.LayGioHang(maNguoiDung);
        }

        public int LayTongSoLuongItem(int maNguoiDung)
        {
            if (maNguoiDung <= 0) return 0;
            List<GioHangItemDTO> list = _gioHangRepo.LayGioHang(maNguoiDung);
            int sum = 0;
            foreach (GioHangItemDTO item in list) sum += item.SoLuong;
            return sum;
        }

        public bool DaCoTrongGioHang(int maNguoiDung, int maSanPham)
        {
            if (maNguoiDung <= 0 || maSanPham <= 0) return false;
            return _gioHangRepo.DaCoTrongGioHang(maNguoiDung, maSanPham);
        }

        public bool ThemVaoGioHang(int maNguoiDung, int maSanPham, int soLuong, out string thongBao)
        {
            if (maNguoiDung <= 0)
            {
                thongBao = "Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng!";
                return false;
            }

            if (maSanPham <= 0 || soLuong <= 0)
            {
                thongBao = "Thông tin sản phẩm hoặc số lượng không hợp lệ!";
                return false;
            }

            if (new ThuVienRepository().DaSoHuu(maNguoiDung, maSanPham))
            {
                thongBao = "Bạn đã sở hữu sản phẩm này trong Thư viện rồi! Không cần mua lại.";
                return false;
            }

            if (_gioHangRepo.DaCoTrongGioHang(maNguoiDung, maSanPham))
            {
                thongBao = "Sản phẩm này đã có trong giỏ hàng của bạn rồi! Vì là sản phẩm kỹ thuật số, mỗi tài khoản chỉ mua 1 bản quyền.";
                return false;
            }

            bool ok = _gioHangRepo.ThemVaoGioHang(maNguoiDung, maSanPham, 1);
            thongBao = ok ? "Đã thêm sản phẩm vào giỏ hàng thành công!" : "Không thể thêm vào giỏ hàng.";
            return ok;
        }

        public bool CapNhatSoLuong(int maGioHang, int soLuong)
        {
            bool ok = _gioHangRepo.CapNhatSoLuong(maGioHang, soLuong);
            return ok;
        }

        public bool XoaKhoiGioHang(int maGioHang)
        {
            bool ok = _gioHangRepo.XoaKhoiGioHang(maGioHang);
            return ok;
        }

        public decimal TinhTongTien(List<GioHangItemDTO> items)
        {
            decimal total = 0;
            foreach (GioHangItemDTO item in items)
            {
                total += item.ThanhTien;
            }
            return total;
        }
    }

    // Service xử lý nghiệp vụ Ví tài khoản Steam simulation
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class ViTaiKhoanService
    {
        private readonly ViTaiKhoanRepository _viRepo;

        public ViTaiKhoanService()
        {
            _viRepo = new ViTaiKhoanRepository();
        }

        public decimal LaySoDuVi(int maNguoiDung)
        {
            if (maNguoiDung <= 0) return 0;
            return _viRepo.LaySoDuVi(maNguoiDung);
        }

        public bool NapTien(int maNguoiDung, decimal soTien, out string thongBao)
        {
            if (maNguoiDung <= 0)
            {
                thongBao = "Vui lòng đăng nhập để nạp tiền vào ví!";
                return false;
            }

            if (soTien < 10000)
            {
                thongBao = "Mệnh giá nạp tối thiểu là 10.000 ₫!";
                return false;
            }

            if (soTien > 50000000)
            {
                thongBao = "Mệnh giá nạp tối đa mỗi lần là 50.000.000 ₫!";
                return false;
            }

            try
            {
                bool ok = _viRepo.NapTienVi(maNguoiDung, soTien);
                if (ok)
                {
                    decimal balanceMoi = _viRepo.LaySoDuVi(maNguoiDung);
                    UserSession.UpdateBalance(balanceMoi);
                    thongBao = $"Nạp thành công {FormatHelper.FormatCurrency(soTien)} vào ví! Số dư hiện tại: {FormatHelper.FormatCurrency(balanceMoi)}.";
                    return true;
                }

                thongBao = "Nạp tiền thất bại, vui lòng thử lại.";
                return false;
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi nạp tiền: " + ex.Message;
                return false;
            }
        }

        public List<ThanhToanDTO> LayLichSuGiaoDich(int maNguoiDung)
        {
            if (maNguoiDung <= 0) return new List<ThanhToanDTO>();
            return _viRepo.LayLichSuGiaoDich(maNguoiDung);
        }
    }

    // Service xử lý nghiệp vụ mã giảm giá voucher
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class MaGiamGiaService
    {
        private readonly MaGiamGiaRepository _voucherRepo;

        public MaGiamGiaService()
        {
            _voucherRepo = new MaGiamGiaRepository();
        }

        public List<MaGiamGiaDTO> LayDanhSachVoucher(int maNguoiDung)
        {
            return _voucherRepo.LayDanhSachVoucher(maNguoiDung);
        }

        public bool ThuThapVoucher(int maNguoiDung, string maCode, out string thongBao)
        {
            if (maNguoiDung <= 0)
            {
                thongBao = "Vui lòng đăng nhập để thu thập voucher ưu đãi!";
                return false;
            }

            if (string.IsNullOrEmpty(maCode) || maCode.Trim() == "")
            {
                thongBao = "Vui lòng nhập mã voucher cần thu thập!";
                return false;
            }

            bool ok = _voucherRepo.ThuThapVoucher(maNguoiDung, maCode, out thongBao);
            return ok;
        }

        public bool KiemTraVaApDung(string maCode, int maNguoiDung, decimal tamTinh, out MaGiamGiaDTO? voucher, out decimal tienGiam, out string thongBao)
        {
            return _voucherRepo.KiemTraVaApDung(maCode, maNguoiDung, tamTinh, out voucher, out tienGiam, out thongBao);
        }

        public bool XoaVoucherCaNhan(int maGiamGiaId, int maNguoiDung)
        {
            bool ok = _voucherRepo.XoaVoucherCaNhan(maGiamGiaId, maNguoiDung);
            return ok;
        }
    }

    // Service xử lý Thanh toán 1-Click giỏ hàng bằng ví
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2) & Phạm Văn Đam (Nhóm 5)
    public class ThanhToanService
    {
        private readonly DonHangRepository _donHangRepo;
        private readonly GioHangRepository _gioHangRepo;
        private readonly ViTaiKhoanRepository _viRepo;

        public ThanhToanService()
        {
            _donHangRepo = new DonHangRepository();
            _gioHangRepo = new GioHangRepository();
            _viRepo = new ViTaiKhoanRepository();
        }

        public bool ThanhToanGioHang(int maNguoiDung, decimal tienGiam, int? maGiamGiaId, out string maDonHangCode, out string thongBao)
        {
            maDonHangCode = string.Empty;

            if (maNguoiDung <= 0)
            {
                thongBao = "Vui lòng đăng nhập để thực hiện thanh toán!";
                return false;
            }

            List<GioHangItemDTO> items = _gioHangRepo.LayGioHang(maNguoiDung);
            if (items == null || items.Count == 0)
            {
                thongBao = "Giỏ hàng của bạn đang trống! Vui lòng chọn sản phẩm trước khi thanh toán.";
                return false;
            }

            decimal tamTinh = 0;
            foreach (GioHangItemDTO item in items) tamTinh += item.ThanhTien;

            bool ok = _donHangRepo.ThucHienThanhToanDonHang(maNguoiDung, items, tamTinh, tienGiam, maGiamGiaId, out maDonHangCode, out thongBao);
            if (ok)
            {
                // Đồng bộ số dư mới sau khi trừ ví
                decimal balanceMoi = _viRepo.LaySoDuVi(maNguoiDung);
                UserSession.UpdateBalance(balanceMoi);
            }

            return ok;
        }
    }
}
