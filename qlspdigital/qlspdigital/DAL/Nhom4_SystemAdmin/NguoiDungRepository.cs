using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom4_SystemAdmin
{
    // Repository quản lý danh sách người dùng và phân quyền / khóa tài khoản
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class NguoiDungRepository
    {
        // Lấy danh sách toàn bộ người dùng kèm bộ lọc vai trò và từ khóa (Read)
        public List<NguoiDungDTO> LayDanhSachNguoiDung(string? vaiTro = null, string? tuKhoa = null)
        {
            List<NguoiDungDTO> list = new List<NguoiDungDTO>();

            string query = @"
                SELECT MaNguoiDung, TenDangNhap, HoTen, Email, SoDienThoai, VaiTro, SoDuVi, TrangThai, NgayTao
                FROM NguoiDung
                WHERE 1 = 1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(vaiTro) && !vaiTro.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
            {
                query += " AND VaiTro = @VaiTro";
                parameters.Add(new SqlParameter("@VaiTro", vaiTro.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query += " AND (TenDangNhap LIKE @TuKhoa OR HoTen LIKE @TuKhoa OR Email LIKE @TuKhoa)";
                string safeKey = tuKhoa.Trim().Replace("[", "[[]");
                parameters.Add(new SqlParameter("@TuKhoa", $"%{safeKey}%"));
            }

            query += " ORDER BY MaNguoiDung DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new NguoiDungDTO
                {
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    TenDangNhap = row["TenDangNhap"].ToString() ?? string.Empty,
                    HoTen = row["HoTen"].ToString() ?? string.Empty,
                    Email = row["Email"].ToString() ?? string.Empty,
                    SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : null,
                    VaiTro = row["VaiTro"].ToString() ?? "KhachHang",
                    SoDuVi = Convert.ToDecimal(row["SoDuVi"]),
                    TrangThai = Convert.ToBoolean(row["TrangThai"]),
                    NgayTao = Convert.ToDateTime(row["NgayTao"])
                });
            }

            return list;
        }

        // Khóa hoặc Mở khóa tài khoản người dùng (Update)
        public bool CapNhatTrangThaiKhoa(int maNguoiDung, bool trangThai, out string thongBao)
        {
            // Kiểm tra quy tắc an toàn: Không cho phép Admin tự khóa tài khoản của chính mình
            if (UserSession.CurrentUserId == maNguoiDung && !trangThai)
            {
                thongBao = "Bạn không thể tự khóa tài khoản của chính mình!";
                return false;
            }

            string query = "UPDATE NguoiDung SET TrangThai = @TrangThai WHERE MaNguoiDung = @MaNguoiDung";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            int affected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (affected > 0)
            {
                thongBao = trangThai ? "Đã mở khóa tài khoản thành công!" : "Đã khóa tài khoản thành công!";
                return true;
            }

            thongBao = "Thao tác cập nhật trạng thái thất bại.";
            return false;
        }
    }

    // Repository tổng hợp các chỉ số KPI thống kê và top bán chạy cho Dashboard
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class DashboardRepository
    {
        // Lấy 4 chỉ số tổng hợp tổng quan toàn sàn (SUM và COUNT)
        public DashboardStatsDTO LayChiSoTongHop()
        {
            DashboardStatsDTO stats = new DashboardStatsDTO();

            // 1. Tổng doanh thu (SUM các đơn hàng đã Hoàn thành)
            string qDoanhThu = "SELECT ISNULL(SUM(TongThanhToan), 0) FROM DonHang WHERE TrangThaiDonHang = N'Hoàn thành'";
            object? oDoanhThu = DatabaseHelper.ExecuteScalar(qDoanhThu);
            stats.TongDoanhThu = (oDoanhThu != null && oDoanhThu != DBNull.Value) ? Convert.ToDecimal(oDoanhThu) : 0m;

            // 2. Tổng số đơn hàng thành công
            string qDonHang = "SELECT COUNT(1) FROM DonHang WHERE TrangThaiDonHang = N'Hoàn thành'";
            object? oDonHang = DatabaseHelper.ExecuteScalar(qDonHang);
            stats.TongDonHang = (oDonHang != null && oDonHang != DBNull.Value) ? Convert.ToInt32(oDonHang) : 0;

            // 3. Tổng số khách hàng
            string qKhachHang = "SELECT COUNT(1) FROM NguoiDung WHERE VaiTro = 'KhachHang'";
            object? oKhachHang = DatabaseHelper.ExecuteScalar(qKhachHang);
            stats.TongKhachHang = (oKhachHang != null && oKhachHang != DBNull.Value) ? Convert.ToInt32(oKhachHang) : 0;

            // 4. Tổng số sản phẩm đang mở bán
            string qSanPham = "SELECT COUNT(1) FROM SanPham WHERE DangKinhDoanh = 1";
            object? oSanPham = DatabaseHelper.ExecuteScalar(qSanPham);
            stats.TongSanPham = (oSanPham != null && oSanPham != DBNull.Value) ? Convert.ToInt32(oSanPham) : 0;

            return stats;
        }

        // Lấy Top 5 sản phẩm bán chạy nhất hệ thống
        public List<TopSanPhamDTO> LayTop5BanChay()
        {
            List<TopSanPhamDTO> list = new List<TopSanPhamDTO>();

            string query = @"
                SELECT TOP 5 sp.MaSanPham, sp.TenSanPham,
                       SUM(ct.SoLuong) AS SoLuongDaBan,
                       SUM(ct.SoLuong * ct.DonGia) AS DoanhThu
                FROM ChiTietDonHang ct
                INNER JOIN DonHang dh ON ct.MaDonHang = dh.MaDonHang
                INNER JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
                WHERE dh.TrangThaiDonHang = N'Hoàn thành'
                GROUP BY sp.MaSanPham, sp.TenSanPham
                ORDER BY SoLuongDaBan DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TopSanPhamDTO
                {
                    MaSanPham = Convert.ToInt32(row["MaSanPham"]),
                    TenSanPham = row["TenSanPham"].ToString() ?? string.Empty,
                    SoLuongDaBan = Convert.ToInt32(row["SoLuongDaBan"]),
                    DoanhThu = Convert.ToDecimal(row["DoanhThu"])
                });
            }

            return list;
        }
    }
}
