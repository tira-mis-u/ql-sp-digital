using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom1_Store
{
    // Repository thao tác bảng DanhGia (CRUD Đánh giá & Nhận xét sản phẩm)
    // Thành viên phụ trách: Lê Nguyễn Minh Trí (Nhóm 1)
    public class DanhGiaRepository
    {
        // Lấy toàn bộ danh sách đánh giá của 1 sản phẩm
        public List<DanhGiaDTO> LayDanhGiaTheoSanPham(int maSanPham)
        {
            List<DanhGiaDTO> list = new List<DanhGiaDTO>();

            string query = @"
                SELECT dg.MaDanhGia, dg.MaNguoiDung, dg.MaSanPham, dg.SoSao, dg.BinhLuan, dg.NgayTao, dg.NgayCapNhat,
                       nd.HoTen AS HoTenNguoiDung, sp.TenSanPham
                FROM DanhGia dg
                INNER JOIN NguoiDung nd ON dg.MaNguoiDung = nd.MaNguoiDung
                INNER JOIN SanPham sp ON dg.MaSanPham = sp.MaSanPham
                WHERE dg.MaSanPham = @MaSanPham
                ORDER BY dg.NgayTao DESC";

            SqlParameter[] parameters = { new SqlParameter("@MaSanPham", maSanPham) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DanhGiaDTO
                {
                    MaDanhGia = Convert.ToInt32(row["MaDanhGia"]),
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    HoTenNguoiDung = row["HoTenNguoiDung"].ToString() ?? string.Empty,
                    MaSanPham = Convert.ToInt32(row["MaSanPham"]),
                    TenSanPham = row["TenSanPham"].ToString() ?? string.Empty,
                    SoSao = Convert.ToInt32(row["SoSao"]),
                    BinhLuan = row["BinhLuan"].ToString() ?? string.Empty,
                    NgayTao = Convert.ToDateTime(row["NgayTao"]),
                    NgayCapNhat = row["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(row["NgayCapNhat"]) : null
                });
            }

            return list;
        }

        // Lấy đánh giá của một người dùng cụ thể trên sản phẩm (để kiểm tra đã đánh giá chưa)
        public DanhGiaDTO? LayDanhGiaCuaUser(int maNguoiDung, int maSanPham)
        {
            string query = @"
                SELECT dg.MaDanhGia, dg.MaNguoiDung, dg.MaSanPham, dg.SoSao, dg.BinhLuan, dg.NgayTao, dg.NgayCapNhat,
                       nd.HoTen AS HoTenNguoiDung, sp.TenSanPham
                FROM DanhGia dg
                INNER JOIN NguoiDung nd ON dg.MaNguoiDung = nd.MaNguoiDung
                INNER JOIN SanPham sp ON dg.MaSanPham = sp.MaSanPham
                WHERE dg.MaNguoiDung = @MaNguoiDung AND dg.MaSanPham = @MaSanPham";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaSanPham", maSanPham)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new DanhGiaDTO
                {
                    MaDanhGia = Convert.ToInt32(row["MaDanhGia"]),
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    HoTenNguoiDung = row["HoTenNguoiDung"].ToString() ?? string.Empty,
                    MaSanPham = Convert.ToInt32(row["MaSanPham"]),
                    TenSanPham = row["TenSanPham"].ToString() ?? string.Empty,
                    SoSao = Convert.ToInt32(row["SoSao"]),
                    BinhLuan = row["BinhLuan"].ToString() ?? string.Empty,
                    NgayTao = Convert.ToDateTime(row["NgayTao"]),
                    NgayCapNhat = row["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(row["NgayCapNhat"]) : null
                };
            }

            return null;
        }

        // Thêm đánh giá mới (Create)
        public bool ThemDanhGia(DanhGiaDTO dto)
        {
            string query = @"
                INSERT INTO DanhGia (MaNguoiDung, MaSanPham, SoSao, BinhLuan, NgayTao)
                VALUES (@MaNguoiDung, @MaSanPham, @SoSao, @BinhLuan, GETDATE())";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNguoiDung", dto.MaNguoiDung),
                new SqlParameter("@MaSanPham", dto.MaSanPham),
                new SqlParameter("@SoSao", dto.SoSao),
                new SqlParameter("@BinhLuan", dto.BinhLuan ?? string.Empty)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Sửa đánh giá đã có (Update)
        public bool CapNhatDanhGia(DanhGiaDTO dto)
        {
            string query = @"
                UPDATE DanhGia
                SET SoSao = @SoSao, BinhLuan = @BinhLuan, NgayCapNhat = GETDATE()
                WHERE MaDanhGia = @MaDanhGia AND MaNguoiDung = @MaNguoiDung";

            SqlParameter[] parameters =
            {
                new SqlParameter("@SoSao", dto.SoSao),
                new SqlParameter("@BinhLuan", dto.BinhLuan ?? string.Empty),
                new SqlParameter("@MaDanhGia", dto.MaDanhGia),
                new SqlParameter("@MaNguoiDung", dto.MaNguoiDung)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa đánh giá (Delete)
        public bool XoaDanhGia(int maDanhGia, int maNguoiDung)
        {
            string query = "DELETE FROM DanhGia WHERE MaDanhGia = @MaDanhGia AND MaNguoiDung = @MaNguoiDung";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaDanhGia", maDanhGia),
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
