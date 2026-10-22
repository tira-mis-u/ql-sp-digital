using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom2_Cart
{
    // Repository quản lý giỏ hàng mua sắm
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class GioHangRepository
    {
        // Lấy toàn bộ sản phẩm trong giỏ của người dùng
        public List<GioHangItemDTO> LayGioHang(int maNguoiDung)
        {
            List<GioHangItemDTO> list = new List<GioHangItemDTO>();

            string query = @"
                UPDATE GioHang SET SoLuong = 1 WHERE MaNguoiDung = @MaNguoiDung AND SoLuong > 1;
                SELECT gh.MaGioHang, gh.MaNguoiDung, gh.MaSanPham, gh.SoLuong, gh.NgayThem,
                       sp.TenSanPham, sp.GiaBan AS DonGia, sp.HinhAnh
                FROM GioHang gh
                INNER JOIN SanPham sp ON gh.MaSanPham = sp.MaSanPham
                WHERE gh.MaNguoiDung = @MaNguoiDung
                ORDER BY gh.NgayThem DESC";

            SqlParameter[] parameters = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new GioHangItemDTO
                {
                    MaGioHang = Convert.ToInt32(row["MaGioHang"]),
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    MaSanPham = Convert.ToInt32(row["MaSanPham"]),
                    TenSanPham = row["TenSanPham"].ToString() ?? string.Empty,
                    DonGia = Convert.ToDecimal(row["DonGia"]),
                    SoLuong = 1, // Sản phẩm kỹ thuật số: mỗi bản quyền chỉ có số lượng là 1
                    HinhAnh = row["HinhAnh"] != DBNull.Value ? row["HinhAnh"].ToString() : null,
                    NgayThem = Convert.ToDateTime(row["NgayThem"])
                });
            }

            return list;
        }

        // Kiểm tra sản phẩm đã có trong giỏ hàng chưa (chặn chọn lặp lại)
        public bool DaCoTrongGioHang(int maNguoiDung, int maSanPham)
        {
            string query = "SELECT COUNT(1) FROM GioHang WHERE MaNguoiDung = @MaNguoiDung AND MaSanPham = @MaSanPham";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaSanPham", maSanPham)
            };

            object? result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        // Thêm sản phẩm vào giỏ hàng (Chặn nếu đã có trong giỏ, số lượng luôn là 1)
        public bool ThemVaoGioHang(int maNguoiDung, int maSanPham, int soLuong = 1)
        {
            string queryCheck = "SELECT MaGioHang, SoLuong FROM GioHang WHERE MaNguoiDung = @MaNguoiDung AND MaSanPham = @MaSanPham";
            SqlParameter[] pCheck =
            {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaSanPham", maSanPham)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(queryCheck, pCheck);
            if (dt.Rows.Count > 0)
            {
                // Sản phẩm số: đã có trong giỏ hàng thì chặn, không cho tăng số lượng
                return false;
            }
            else
            {
                // Chưa có, thêm mới với số lượng luôn là 1
                string queryInsert = @"
                    INSERT INTO GioHang (MaNguoiDung, MaSanPham, SoLuong, NgayThem)
                    VALUES (@MaNguoiDung, @MaSanPham, 1, GETDATE())";

                SqlParameter[] pInsert =
                {
                    new SqlParameter("@MaNguoiDung", maNguoiDung),
                    new SqlParameter("@MaSanPham", maSanPham)
                };

                return DatabaseHelper.ExecuteNonQuery(queryInsert, pInsert) > 0;
            }
        }

        // Cập nhật số lượng món trong giỏ (Update) - với sản phẩm số thì luôn là 1
        public bool CapNhatSoLuong(int maGioHang, int soLuong)
        {
            if (soLuong <= 0)
            {
                return XoaKhoiGioHang(maGioHang);
            }

            string query = "UPDATE GioHang SET SoLuong = 1 WHERE MaGioHang = @MaGioHang";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaGioHang", maGioHang)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa 1 món khỏi giỏ (Delete)
        public bool XoaKhoiGioHang(int maGioHang)
        {
            string query = "DELETE FROM GioHang WHERE MaGioHang = @MaGioHang";
            SqlParameter[] parameters = { new SqlParameter("@MaGioHang", maGioHang) };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa sạch giỏ hàng của người dùng (sau khi thanh toán thành công)
        public bool XoaToanBoGioHang(int maNguoiDung)
        {
            string query = "DELETE FROM GioHang WHERE MaNguoiDung = @MaNguoiDung";
            SqlParameter[] parameters = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) >= 0;
        }
    }
}
