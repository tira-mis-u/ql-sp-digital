using System;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom4_SystemAdmin
{
    // Repository quản lý xác thực đăng nhập & đăng ký tài khoản
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class AuthRepository
    {
        // Đăng nhập tài khoản (Read)
        public bool DangNhap(string tenDangNhap, string matKhau, out NguoiDungDTO? user, out string thongBao)
        {
            user = null;
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                thongBao = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!";
                return false;
            }

            string query = @"
                SELECT MaNguoiDung, TenDangNhap, 
                       MatKhau,
                       HoTen, Email, SoDienThoai, VaiTro, SoDuVi, TrangThai, NgayTao
                FROM NguoiDung
                WHERE TenDangNhap = @TenDangNhap";

            SqlParameter[] parameters = { new SqlParameter("@TenDangNhap", tenDangNhap.Trim()) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            {
                thongBao = "Tên đăng nhập không tồn tại trong hệ thống!";
                return false;
            }

            DataRow row = dt.Rows[0];
            bool trangThai = Convert.ToBoolean(row["TrangThai"]);
            if (!trangThai)
            {
                thongBao = "Tài khoản của bạn đã bị khóa! Vui lòng liên hệ Quản trị viên để được mở khóa.";
                return false;
            }

            string storedPass = row["MatKhau"]?.ToString() ?? string.Empty;
            if (matKhau != storedPass)
            {
                thongBao = "Mật khẩu không chính xác, vui lòng thử lại!";
                return false;
            }

            user = new NguoiDungDTO
            {
                MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                TenDangNhap = row["TenDangNhap"].ToString() ?? string.Empty,
                MatKhau = storedPass,
                HoTen = row["HoTen"].ToString() ?? string.Empty,
                Email = row["Email"].ToString() ?? string.Empty,
                SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : null,
                VaiTro = row["VaiTro"].ToString() ?? "KhachHang",
                SoDuVi = Convert.ToDecimal(row["SoDuVi"]),
                TrangThai = trangThai,
                NgayTao = Convert.ToDateTime(row["NgayTao"])
            };

            thongBao = "Đăng nhập thành công!";
            return true;
        }

        // Đăng ký tài khoản người dùng mới (Create)
        public bool DangKy(string tenDangNhap, string matKhau, string hoTen, string email, string? soDienThoai, out string thongBao)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau) ||
                string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email))
            {
                thongBao = "Vui lòng điền đầy đủ các thông tin bắt buộc (*)!";
                return false;
            }

            // Kiểm tra trùng username hoặc email
            string queryCheck = "SELECT TenDangNhap, Email FROM NguoiDung WHERE TenDangNhap = @TenDangNhap OR Email = @Email";
            SqlParameter[] pCheck =
            {
                new SqlParameter("@TenDangNhap", tenDangNhap.Trim()),
                new SqlParameter("@Email", email.Trim())
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(queryCheck, pCheck);
            if (dt.Rows.Count > 0)
            {
                string existingUser = dt.Rows[0]["TenDangNhap"].ToString() ?? string.Empty;
                if (existingUser.ToLower() == tenDangNhap.Trim().ToLower())
                {
                    thongBao = "Tên đăng nhập này đã được sử dụng! Vui lòng chọn tên khác.";
                    return false;
                }
                else
                {
                    thongBao = "Địa chỉ Email này đã được đăng ký tài khoản khác!";
                    return false;
                }
            }

            string queryInsert = @"
                INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, VaiTro, SoDuVi, TrangThai, NgayTao)
                VALUES (@TenDangNhap, @MatKhau, @HoTen, @Email, @SoDienThoai, 'KhachHang', 0, 1, GETDATE())";

            SqlParameter[] pInsert =
            {
                new SqlParameter("@TenDangNhap", tenDangNhap.Trim()),
                new SqlParameter("@MatKhau", matKhau),
                new SqlParameter("@HoTen", hoTen.Trim()),
                new SqlParameter("@Email", email.Trim()),
                new SqlParameter("@SoDienThoai", (object?)soDienThoai?.Trim() ?? DBNull.Value)
            };

            int affected = DatabaseHelper.ExecuteNonQuery(queryInsert, pInsert);
            if (affected > 0)
            {
                thongBao = "Đăng ký tài khoản thành công! Bạn có thể đăng nhập ngay.";
                return true;
            }

            thongBao = "Đăng ký thất bại, vui lòng thử lại.";
            return false;
        }
    }
}
