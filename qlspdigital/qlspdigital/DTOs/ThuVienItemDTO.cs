using System;

namespace qlspdigital.DTOs
{
    // DTO đại diện cho sản phẩm đã sở hữu trong Thư viện cá nhân (My Library)
    // Phụ trách: Phạm Văn Đam (Nhóm 5)
    public class ThuVienItemDTO
    {
        public int MaSoHuu { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string? HinhAnh { get; set; }
        public decimal DungLuongMB { get; set; } = 0;
        public string PhienBan { get; set; } = "1.0.0";
        public string TenDanhMuc { get; set; } = string.Empty;
        public int MaDonHang { get; set; }
        public DateTime NgaySoHuu { get; set; } = DateTime.Now;
        public int SoLanTai { get; set; } = 0;
        public DateTime? LanTaiCuoi { get; set; }
        public string DuongDanTep { get; set; } = string.Empty;
    }

    // DTO đánh giá sao và nhận xét sản phẩm
    // Phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public class DanhGiaDTO
    {
        public int MaDanhGia { get; set; }
        public int MaNguoiDung { get; set; }
        public string HoTenNguoiDung { get; set; } = string.Empty;
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public int SoSao { get; set; } = 5; // 1 đến 5 sao
        public string BinhLuan { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }
    }

    // DTO lưu trữ chỉ số tổng hợp cho Dashboard quản trị viên
    // Phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class DashboardStatsDTO
    {
        public decimal TongDoanhThu { get; set; } = 0;
        public int TongDonHang { get; set; } = 0;
        public int TongKhachHang { get; set; } = 0;
        public int TongSanPham { get; set; } = 0;
    }

    // DTO sản phẩm bán chạy nhất trong Dashboard
    public class TopSanPhamDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public int SoLuongDaBan { get; set; } = 0;
        public decimal DoanhThu { get; set; } = 0;
    }
}
