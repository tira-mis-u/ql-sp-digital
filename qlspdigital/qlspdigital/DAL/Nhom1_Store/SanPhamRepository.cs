using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom1_Store
{
    // Repository truy vấn dữ liệu sản phẩm phục vụ Cửa hàng & Chi tiết sản phẩm
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public class SanPhamRepository
    {
        // Lấy danh sách sản phẩm mở bán theo bộ lọc từ khóa và danh mục
        public List<SanPhamDTO> LayDanhSachSanPham(string? tuKhoa = null, int? maDanhMuc = null)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            string query = @"
                SELECT sp.MaSanPham, sp.TenSanPham, sp.MoTa, sp.GiaBan, sp.HinhAnh,
                       sp.DungLuongMB, sp.PhienBan, sp.MaDanhMuc, sp.MaNhaPhatHanh, sp.DangKinhDoanh, sp.NgayTao,
                       dm.TenDanhMuc, ISNULL(nph.TenNhaPhatHanh, N'Đang cập nhật') AS TenNhaPhatHanh,
                       ISNULL(AVG(CAST(dg.SoSao AS FLOAT)), 5.0) AS DiemDanhGiaTrungBinh,
                       COUNT(dg.MaDanhGia) AS SoLuongDanhGia
                FROM SanPham sp
                INNER JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                LEFT JOIN NhaPhatHanh nph ON sp.MaNhaPhatHanh = nph.MaNhaPhatHanh
                LEFT JOIN DanhGia dg ON sp.MaSanPham = dg.MaSanPham
                WHERE sp.DangKinhDoanh = 1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query += " AND sp.TenSanPham LIKE @TuKhoa";
                parameters.Add(new SqlParameter("@TuKhoa", $"%{tuKhoa.Trim()}%"));
            }

            if (maDanhMuc.HasValue && maDanhMuc.Value > 0)
            {
                query += " AND sp.MaDanhMuc = @MaDanhMuc";
                parameters.Add(new SqlParameter("@MaDanhMuc", maDanhMuc.Value));
            }

            query += @"
                GROUP BY sp.MaSanPham, sp.TenSanPham, sp.MoTa, sp.GiaBan, sp.HinhAnh,
                         sp.DungLuongMB, sp.PhienBan, sp.MaDanhMuc, sp.MaNhaPhatHanh, sp.DangKinhDoanh, sp.NgayTao,
                         dm.TenDanhMuc, nph.TenNhaPhatHanh
                ORDER BY sp.NgayTao DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRowToDto(row));
            }

            return list;
        }

        // Lấy chi tiết 1 sản phẩm theo mã
        public SanPhamDTO? LayChiTietSanPham(int maSanPham)
        {
            string query = @"
                SELECT sp.MaSanPham, sp.TenSanPham, sp.MoTa, sp.GiaBan, sp.HinhAnh,
                       sp.DungLuongMB, sp.PhienBan, sp.MaDanhMuc, sp.MaNhaPhatHanh, sp.DangKinhDoanh, sp.NgayTao,
                       dm.TenDanhMuc, ISNULL(nph.TenNhaPhatHanh, N'Đang cập nhật') AS TenNhaPhatHanh,
                       ISNULL(AVG(CAST(dg.SoSao AS FLOAT)), 5.0) AS DiemDanhGiaTrungBinh,
                       COUNT(dg.MaDanhGia) AS SoLuongDanhGia
                FROM SanPham sp
                INNER JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                LEFT JOIN NhaPhatHanh nph ON sp.MaNhaPhatHanh = nph.MaNhaPhatHanh
                LEFT JOIN DanhGia dg ON sp.MaSanPham = dg.MaSanPham
                WHERE sp.MaSanPham = @MaSanPham
                GROUP BY sp.MaSanPham, sp.TenSanPham, sp.MoTa, sp.GiaBan, sp.HinhAnh,
                         sp.DungLuongMB, sp.PhienBan, sp.MaDanhMuc, sp.MaNhaPhatHanh, sp.DangKinhDoanh, sp.NgayTao,
                         dm.TenDanhMuc, nph.TenNhaPhatHanh";

            SqlParameter[] parameters = { new SqlParameter("@MaSanPham", maSanPham) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                return MapRowToDto(dt.Rows[0]);
            }

            return null;
        }

        private SanPhamDTO MapRowToDto(DataRow row)
        {
            return new SanPhamDTO
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
                TenNhaPhatHanh = row["TenNhaPhatHanh"].ToString() ?? string.Empty,
                DiemDanhGiaTrungBinh = Math.Round(Convert.ToDouble(row["DiemDanhGiaTrungBinh"]), 1),
                SoLuongDanhGia = Convert.ToInt32(row["SoLuongDanhGia"])
            };
        }
    }
}
