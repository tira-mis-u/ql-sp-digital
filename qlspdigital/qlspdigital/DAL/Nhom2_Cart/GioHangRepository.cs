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
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    HinhAnh = row["HinhAnh"] != DBNull.Value ? row["HinhAnh"].ToString() : null,
                    NgayThem = Convert.ToDateTime(row["NgayThem"])
                });
            }

            return list;
        }

        // Thêm sản phẩm vào giỏ hàng (Create / Update nếu đã tồn tại)
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
                // Đã có trong giỏ hàng, cập nhật tăng số lượng
                int maGioHang = Convert.ToInt32(dt.Rows[0]["MaGioHang"]);
                int currentQty = Convert.ToInt32(dt.Rows[0]["SoLuong"]);
                return CapNhatSoLuong(maGioHang, currentQty + soLuong);
            }
            else
            {
                // Chưa có, thêm mới dòng hàng
                string queryInsert = @"
                    INSERT INTO GioHang (MaNguoiDung, MaSanPham, SoLuong, NgayThem)
                    VALUES (@MaNguoiDung, @MaSanPham, @SoLuong, GETDATE())";

                SqlParameter[] pInsert =
                {
                    new SqlParameter("@MaNguoiDung", maNguoiDung),
                    new SqlParameter("@MaSanPham", maSanPham),
                    new SqlParameter("@SoLuong", soLuong)
                };

                return DatabaseHelper.ExecuteNonQuery(queryInsert, pInsert) > 0;
            }
        }

        // Cập nhật số lượng món trong giỏ (Update)
        public bool CapNhatSoLuong(int maGioHang, int soLuong)
        {
            if (soLuong <= 0)
            {
                return XoaKhoiGioHang(maGioHang);
            }

            string query = "UPDATE GioHang SET SoLuong = @SoLuong WHERE MaGioHang = @MaGioHang";
            SqlParameter[] parameters =
            {
                new SqlParameter("@SoLuong", soLuong),
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
