using System;
using System.Collections.Generic;

namespace qlspdigital.DTOs
{
    // DTO chứa thông tin hóa đơn đơn hàng
    // Phụ trách: Phạm Văn Đam (Nhóm 5) & Nguyễn Quốc Trung (Nhóm 2)
    public class DonHangDTO
    {
        public int MaDonHang { get; set; }
        public string MaDonHangCode { get; set; } = string.Empty;
        public int MaNguoiDung { get; set; }
        public string TenKhachHang { get; set; } = string.Empty;
        public int? MaGiamGiaId { get; set; }
        public string? MaVoucherCode { get; set; }
        public decimal TamTinh { get; set; } = 0;
        public decimal TienGiamGia { get; set; } = 0;
        public decimal TongThanhToan { get; set; } = 0;
        public string TrangThaiDonHang { get; set; } = "Hoàn thành"; // "Hoàn thành", "Chờ xử lý", "Đã hủy"
        public string? GhiChu { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        // Danh sách mặt hàng thuộc đơn hàng
        public List<ChiTietDonHangDTO> DanhSachChiTiet { get; set; } = new List<ChiTietDonHangDTO>();
    }

    // DTO chi tiết sản phẩm trong đơn hàng
    public class ChiTietDonHangDTO
    {
        public int MaChiTietDonHang { get; set; }
        public int MaDonHang { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public int SoLuong { get; set; } = 1;
        public decimal DonGia { get; set; } = 0;
        public decimal ThanhTien => DonGia * SoLuong;
    }

    // DTO ghi nhận nhật ký giao dịch tiền tệ
    public class ThanhToanDTO
    {
        public int MaThanhToan { get; set; }
        public int MaNguoiDung { get; set; }
        public int? MaDonHang { get; set; }
        public decimal SoTien { get; set; } = 0;
        public string LoaiGiaoDich { get; set; } = "Nạp tiền ví"; // "Nạp tiền ví", "Thanh toán đơn hàng", "Hoàn tiền"
        public string PhuongThuc { get; set; } = "Ví Steam Simulation";
        public string TrangThai { get; set; } = "Thành công";
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}
