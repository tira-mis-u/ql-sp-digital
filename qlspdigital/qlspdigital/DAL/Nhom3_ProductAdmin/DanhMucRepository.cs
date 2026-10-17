using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom3_ProductAdmin
{
    // Repository CRUD thể loại / danh mục hàng hóa
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public class DanhMucRepository
    {
        // Lấy toàn bộ danh sách danh mục (Read)
        public List<DanhMucDTO> LayTatCaDanhMuc()
        {
            List<DanhMucDTO> list = new List<DanhMucDTO>();

            string query = @"
                SELECT dm.MaDanhMuc, dm.TenDanhMuc, dm.MoTa, dm.TrangThai,
                       COUNT(sp.MaSanPham) AS SoLuongSanPham
                FROM DanhMuc dm
                LEFT JOIN SanPham sp ON dm.MaDanhMuc = sp.MaDanhMuc
                GROUP BY dm.MaDanhMuc, dm.TenDanhMuc, dm.MoTa, dm.TrangThai
                ORDER BY dm.MaDanhMuc ASC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DanhMucDTO
                {
                    MaDanhMuc = Convert.ToInt32(row["MaDanhMuc"]),
                    TenDanhMuc = row["TenDanhMuc"].ToString() ?? string.Empty,
                    MoTa = row["MoTa"] != DBNull.Value ? row["MoTa"].ToString() : null,
                    TrangThai = Convert.ToBoolean(row["TrangThai"]),
                    SoLuongSanPham = Convert.ToInt32(row["SoLuongSanPham"])
                });
            }

            return list;
        }

        // Thêm danh mục mới (Create)
        public bool ThemDanhMuc(DanhMucDTO dm)
        {
            string query = @"
                INSERT INTO DanhMuc (TenDanhMuc, MoTa, TrangThai)
                VALUES (@TenDanhMuc, @MoTa, @TrangThai)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDanhMuc", dm.TenDanhMuc.Trim()),
                new SqlParameter("@MoTa", (object?)dm.MoTa ?? DBNull.Value),
                new SqlParameter("@TrangThai", dm.TrangThai)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Cập nhật thông tin danh mục (Update)
        public bool CapNhatDanhMuc(DanhMucDTO dm)
        {
            string query = @"
                UPDATE DanhMuc
                SET TenDanhMuc = @TenDanhMuc, MoTa = @MoTa, TrangThai = @TrangThai
                WHERE MaDanhMuc = @MaDanhMuc";

            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDanhMuc", dm.TenDanhMuc.Trim()),
                new SqlParameter("@MoTa", (object?)dm.MoTa ?? DBNull.Value),
                new SqlParameter("@TrangThai", dm.TrangThai),
                new SqlParameter("@MaDanhMuc", dm.MaDanhMuc)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa danh mục (Delete) - Chặn xóa nếu danh mục đang chứa sản phẩm
        public bool XoaDanhMuc(int maDanhMuc, out string thongBao)
        {
            string queryCheck = "SELECT COUNT(1) FROM SanPham WHERE MaDanhMuc = @MaDanhMuc";
            SqlParameter[] pCheck = { new SqlParameter("@MaDanhMuc", maDanhMuc) };
            object? resCount = DatabaseHelper.ExecuteScalar(queryCheck, pCheck);
            int count = (resCount != null && resCount != DBNull.Value) ? Convert.ToInt32(resCount) : 0;

            if (count > 0)
            {
                thongBao = $"Không thể xóa danh mục này vì đang có {count} sản phẩm trực thuộc!";
                return false;
            }

            string queryDelete = "DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc";
            SqlParameter[] pDelete = { new SqlParameter("@MaDanhMuc", maDanhMuc) };

            int result = DatabaseHelper.ExecuteNonQuery(queryDelete, pDelete);
            if (result > 0)
            {
                thongBao = "Xóa danh mục thành công!";
                return true;
            }

            thongBao = "Xóa danh mục thất bại.";
            return false;
        }
    }
}
