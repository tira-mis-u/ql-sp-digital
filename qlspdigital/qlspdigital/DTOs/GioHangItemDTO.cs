using System;

namespace qlspdigital.DTOs
{
    // DTO đại diện cho một sản phẩm trong giỏ hàng
    // Phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class GioHangItemDTO
    {
        public int MaGioHang { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public decimal DonGia { get; set; } = 0;
        public int SoLuong { get; set; } = 1;
        public string? HinhAnh { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;
        public DateTime NgayThem { get; set; } = DateTime.Now;
    }

    // DTO chứa thông tin mã giảm giá / khuyến mãi
    // Phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class MaGiamGiaDTO
    {
        public int MaGiamGiaId { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public string TieuDe { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public string LoaiGiamGia { get; set; } = "PhanTram"; // "PhanTram" hoặc "TienMat"
        public decimal GiaTriGiam { get; set; } = 0;
        public decimal? GiamToiDa { get; set; }
        public decimal DonToiThieu { get; set; } = 0;
        public int SoLuongPhatHanh { get; set; } = 100;
        public int SoLuongConLai { get; set; } = 100;
        public int? MaNhaPhatHanh { get; set; }
        public DateTime NgayBatDau { get; set; } = DateTime.Now;
        public DateTime NgayKetThuc { get; set; } = DateTime.Now.AddDays(30);
        public bool KichHoat { get; set; } = true;

        // Trạng thái đối với người dùng hiện tại
        public bool DaThuThap { get; set; } = false;
        public bool DaSuDung { get; set; } = false;
    }
}
