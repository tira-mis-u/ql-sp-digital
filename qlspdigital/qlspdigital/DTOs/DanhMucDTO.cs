using System;

namespace qlspdigital.DTOs
{
    // DTO chứa thông tin danh mục thể loại sản phẩm
    // Phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public class DanhMucDTO
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public bool TrangThai { get; set; } = true;
        public int SoLuongSanPham { get; set; } = 0; // Đếm số sản phẩm trực thuộc (phục vụ thống kê)
    }

    // DTO thông tin nhà phát hành / Studio
    public class NhaPhatHanhDTO
    {
        public int MaNhaPhatHanh { get; set; }
        public string TenNhaPhatHanh { get; set; } = string.Empty;
        public string? QuocGia { get; set; }
        public string? Website { get; set; }
        public string? MoTa { get; set; }
    }
}
