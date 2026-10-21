using System;

namespace qlspdigital.DTOs
{
    // DTO chứa thông tin người dùng / tài khoản
    // Phụ trách: Trần Quốc Thuận (Nhóm 4) & Phạm Trung Hiếu (Nhóm 3)
    public class NguoiDungDTO
    {
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string VaiTro { get; set; } = "KhachHang"; // "QuanTriVien" hoặc "KhachHang"
        public decimal SoDuVi { get; set; } = 0;
        public bool TrangThai { get; set; } = true; // true: Hoạt động, false: Khóa
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}
