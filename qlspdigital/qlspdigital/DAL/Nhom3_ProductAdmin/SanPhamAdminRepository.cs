using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom3_ProductAdmin
{
    // Repository CRUD quản trị thông tin sản phẩm và liên kết tệp số
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public class SanPhamAdminRepository
    {
        // Lấy toàn bộ danh sách sản phẩm phục vụ màn hình quản trị Admin (Read)
        public List<SanPhamDTO> LayDanhSachAdmin(string? tuKhoa = null, int? maDanhMuc = null)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            string query = @"
                SELECT sp.MaSanPham, sp.TenSanPham, sp.MoTa, sp.GiaBan, sp.HinhAnh,
                       sp.DungLuongMB, sp.PhienBan, sp.MaDanhMuc, sp.MaNhaPhatHanh, sp.DangKinhDoanh, sp.NgayTao,
                       dm.TenDanhMuc, ISNULL(nph.TenNhaPhatHanh, N'Đang cập nhật') AS TenNhaPhatHanh
                FROM SanPham sp
                INNER JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                LEFT JOIN NhaPhatHanh nph ON sp.MaNhaPhatHanh = nph.MaNhaPhatHanh
                WHERE 1 = 1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query += " AND sp.TenSanPham LIKE @TuKhoa";
                string safeKey = tuKhoa.Trim().Replace("[", "[[]");
                parameters.Add(new SqlParameter("@TuKhoa", $"%{safeKey}%"));
            }

            if (maDanhMuc.HasValue && maDanhMuc.Value > 0)
            {
                query += " AND sp.MaDanhMuc = @MaDanhMuc";
                parameters.Add(new SqlParameter("@MaDanhMuc", maDanhMuc.Value));
            }

            query += " ORDER BY sp.MaSanPham DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SanPhamDTO
                {
                    MaSanPham = Convert.ToInt32(row["MaSanPham"]),
                    TenSanPham = row["TenSanPham"].ToString() ?? string.Empty,
                    MoTa = row["MoTa"] != DBNull.Value ? row["MoTa"].ToString() : null,
                    GiaBan = Convert.ToDecimal(row["GiaBan"]),
                    HinhAnh = row["HinhAnh"] != DBNull.Value ? row["HinhAnh"].ToString() : null,
                    DungLuongMB = Convert.ToDecimal(row["DungLuongMB"]),
                    PhienBan = row["PhienBan"] != DBNull.Value ? row["PhienBan"].ToString() ?? "1.0.0" : "1.0.0",
                    MaDanhMuc = Convert.ToInt32(row["MaDanhMuc"]),
                    MaNhaPhatHanh = row["MaNhaPhatHanh"] != DBNull.Value ? Convert.ToInt32(row["MaNhaPhatHanh"]) : null,
                    DangKinhDoanh = Convert.ToBoolean(row["DangKinhDoanh"]),
                    NgayTao = Convert.ToDateTime(row["NgayTao"]),
                    TenDanhMuc = row["TenDanhMuc"].ToString() ?? string.Empty,
                    TenNhaPhatHanh = row["TenNhaPhatHanh"].ToString() ?? string.Empty
                });
            }

            return list;
        }

        // Thêm sản phẩm mới (Create)
        public bool ThemSanPham(SanPhamDTO sp, out int maSanPhamMoi)
        {
            maSanPhamMoi = 0;
            string query = @"
                INSERT INTO SanPham (TenSanPham, MoTa, GiaBan, HinhAnh, DungLuongMB, PhienBan, MaDanhMuc, MaNhaPhatHanh, DangKinhDoanh, NgayTao)
                VALUES (@TenSanPham, @MoTa, @GiaBan, @HinhAnh, @DungLuongMB, @PhienBan, @MaDanhMuc, @MaNhaPhatHanh, @DangKinhDoanh, GETDATE());
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters =
            {
                new SqlParameter("@TenSanPham", sp.TenSanPham),
                new SqlParameter("@MoTa", (object?)sp.MoTa ?? DBNull.Value),
                new SqlParameter("@GiaBan", sp.GiaBan),
                new SqlParameter("@HinhAnh", (object?)sp.HinhAnh ?? DBNull.Value),
                new SqlParameter("@DungLuongMB", sp.DungLuongMB),
                new SqlParameter("@PhienBan", string.IsNullOrWhiteSpace(sp.PhienBan) ? "1.0.0" : sp.PhienBan),
                new SqlParameter("@MaDanhMuc", sp.MaDanhMuc),
                new SqlParameter("@MaNhaPhatHanh", sp.MaNhaPhatHanh.HasValue ? (object)sp.MaNhaPhatHanh.Value : DBNull.Value),
                new SqlParameter("@DangKinhDoanh", sp.DangKinhDoanh)
            };

            object? result = DatabaseHelper.ExecuteScalar(query, parameters);
            if (result != null && result != DBNull.Value)
            {
                maSanPhamMoi = Convert.ToInt32(result);
                return true;
            }
            return false;
        }

        // Chỉnh sửa thông tin sản phẩm (Update)
        public bool CapNhatSanPham(SanPhamDTO sp)
        {
            string query = @"
                UPDATE SanPham
                SET TenSanPham = @TenSanPham,
                    MoTa = @MoTa,
                    GiaBan = @GiaBan,
                    HinhAnh = @HinhAnh,
                    DungLuongMB = @DungLuongMB,
                    PhienBan = @PhienBan,
                    MaDanhMuc = @MaDanhMuc,
                    MaNhaPhatHanh = @MaNhaPhatHanh,
                    DangKinhDoanh = @DangKinhDoanh,
                    NgayCapNhat = GETDATE()
                WHERE MaSanPham = @MaSanPham";

            SqlParameter[] parameters =
            {
                new SqlParameter("@TenSanPham", sp.TenSanPham),
                new SqlParameter("@MoTa", (object?)sp.MoTa ?? DBNull.Value),
                new SqlParameter("@GiaBan", sp.GiaBan),
                new SqlParameter("@HinhAnh", (object?)sp.HinhAnh ?? DBNull.Value),
                new SqlParameter("@DungLuongMB", sp.DungLuongMB),
                new SqlParameter("@PhienBan", string.IsNullOrWhiteSpace(sp.PhienBan) ? "1.0.0" : sp.PhienBan),
                new SqlParameter("@MaDanhMuc", sp.MaDanhMuc),
                new SqlParameter("@MaNhaPhatHanh", sp.MaNhaPhatHanh.HasValue ? (object)sp.MaNhaPhatHanh.Value : DBNull.Value),
                new SqlParameter("@DangKinhDoanh", sp.DangKinhDoanh),
                new SqlParameter("@MaSanPham", sp.MaSanPham)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa sản phẩm khỏi CSDL (Delete)
        public bool XoaSanPham(int maSanPham)
        {
            string query = "DELETE FROM SanPham WHERE MaSanPham = @MaSanPham";
            SqlParameter[] parameters = { new SqlParameter("@MaSanPham", maSanPham) };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
