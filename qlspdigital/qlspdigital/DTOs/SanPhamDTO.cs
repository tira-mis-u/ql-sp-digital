using System;

namespace qlspdigital.DTOs
{
    // DTO chứa thông tin sản phẩm số (Game, phần mềm, đồ họa, 3D...)
    // Phụ trách: Lê Nguyễn Minh Trí (Nhóm 1) & Phạm Trung Hiếu (Nhóm 3)
    public class SanPhamDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public decimal GiaBan { get; set; } = 0;
        public string? HinhAnh { get; set; }
        public decimal DungLuongMB { get; set; } = 0;
        public string PhienBan { get; set; } = "1.0.0";
        public int MaDanhMuc { get; set; }
        public int? MaNhaPhatHanh { get; set; }
        public bool DangKinhDoanh { get; set; } = true;
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }

        // Các thuộc tính phụ trợ hiển thị trên UI
        public string TenDanhMuc { get; set; } = string.Empty;
        public string TenNhaPhatHanh { get; set; } = string.Empty;
        public double DiemDanhGiaTrungBinh { get; set; } = 5.0;
        public int SoLuongDanhGia { get; set; } = 0;
    }

    // DTO thông tin tệp tải về của sản phẩm số
    // Phụ trách: Phạm Trung Hiếu (Nhóm 3) & Phạm Văn Đam (Nhóm 5)
    public class TepSanPhamDTO
    {
        public int MaTep { get; set; }
        public int MaSanPham { get; set; }
        public string TenTep { get; set; } = string.Empty;
        public string DuongDanTep { get; set; } = string.Empty;
        public string DinhDangTep { get; set; } = ".zip";
        public long KichThuocByte { get; set; } = 0;
        public string? MaHashSHA256 { get; set; }
        public string PhienBan { get; set; } = "1.0.0";
        public DateTime NgayTaiLen { get; set; } = DateTime.Now;
    }
}
