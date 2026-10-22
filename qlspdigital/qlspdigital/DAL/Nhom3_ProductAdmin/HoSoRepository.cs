using System;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom3_ProductAdmin
{
    // Repository quản lý hồ sơ cá nhân và đổi mật khẩu người dùng
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public class HoSoRepository
    {
        // Lấy thông tin cá nhân của người dùng (Read)
        public NguoiDungDTO? LayThongTinHoSo(int maNguoiDung)
        {
            string query = @"
                SELECT MaNguoiDung, TenDangNhap, 
                       MatKhau,
                       HoTen, Email, SoDienThoai, VaiTro, SoDuVi, TrangThai, NgayTao
                FROM NguoiDung
                WHERE MaNguoiDung = @MaNguoiDung";

            SqlParameter[] parameters = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new NguoiDungDTO
                {
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    TenDangNhap = row["TenDangNhap"].ToString() ?? string.Empty,
                    MatKhau = row["MatKhau"].ToString() ?? string.Empty,
                    HoTen = row["HoTen"].ToString() ?? string.Empty,
                    Email = row["Email"].ToString() ?? string.Empty,
                    SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : null,
                    VaiTro = row["VaiTro"].ToString() ?? "KhachHang",
                    SoDuVi = Convert.ToDecimal(row["SoDuVi"]),
                    TrangThai = Convert.ToBoolean(row["TrangThai"]),
                    NgayTao = Convert.ToDateTime(row["NgayTao"])
                };
            }

            return null;
        }

        // Cập nhật thông tin cá nhân (Update)
        public bool CapNhatHoSo(int maNguoiDung, string hoTen, string email, string? soDienThoai, out string thongBao)
        {
            // Kiểm tra trùng email với tài khoản khác
            string queryCheckEmail = "SELECT COUNT(1) FROM NguoiDung WHERE Email = @Email AND MaNguoiDung <> @MaNguoiDung";
            SqlParameter[] pCheck =
            {
                new SqlParameter("@Email", email.Trim()),
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            object? resCount = DatabaseHelper.ExecuteScalar(queryCheckEmail, pCheck);
            int count = (resCount != null && resCount != DBNull.Value) ? Convert.ToInt32(resCount) : 0;
            if (count > 0)
            {
                thongBao = "Email này đã được sử dụng bởi một tài khoản khác!";
                return false;
            }

            string queryUpdate = @"
                UPDATE NguoiDung
                SET HoTen = @HoTen, Email = @Email, SoDienThoai = @SoDienThoai
                WHERE MaNguoiDung = @MaNguoiDung";

            SqlParameter[] parameters =
            {
                new SqlParameter("@HoTen", hoTen.Trim()),
                new SqlParameter("@Email", email.Trim()),
                new SqlParameter("@SoDienThoai", (object?)soDienThoai?.Trim() ?? DBNull.Value),
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            int result = DatabaseHelper.ExecuteNonQuery(queryUpdate, parameters);
            if (result > 0)
            {
                thongBao = "Cập nhật hồ sơ cá nhân thành công!";
                return true;
            }

            thongBao = "Không thể cập nhật hồ sơ, vui lòng kiểm tra lại.";
            return false;
        }

        // Đổi mật khẩu tài khoản (Update)
        public bool DoiMatKhau(int maNguoiDung, string matKhauCu, string matKhauMoi, out string thongBao)
        {
            // Kiểm tra mật khẩu cũ
            string queryCheckPass = "SELECT MatKhau FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
            SqlParameter[] pCheck = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            object? passObj = DatabaseHelper.ExecuteScalar(queryCheckPass, pCheck);

            if (passObj == null || passObj == DBNull.Value)
            {
                thongBao = "Không tìm thấy thông tin tài khoản!";
                return false;
            }

            string storedPass = passObj.ToString() ?? string.Empty;
            if (matKhauCu != storedPass)
            {
                thongBao = "Mật khẩu hiện tại không chính xác!";
                return false;
            }

            string queryUpdate = "UPDATE NguoiDung SET MatKhau = @NewPass WHERE MaNguoiDung = @MaNguoiDung";
            SqlParameter[] pUpdate =
            {
                new SqlParameter("@NewPass", matKhauMoi),
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            int result = DatabaseHelper.ExecuteNonQuery(queryUpdate, pUpdate);
            if (result > 0)
            {
                thongBao = "Đổi mật khẩu thành công!";
                return true;
            }

            thongBao = "Đổi mật khẩu thất bại, vui lòng thử lại.";
            return false;
        }

        // Đảm bảo bảng CaiDatNguoiDung tồn tại
        private static bool _tableCaiDatChecked = false;
        private static void EnsureCaiDatTable()
        {
            if (_tableCaiDatChecked) return;
            string sql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CaiDatNguoiDung')
                BEGIN
                    CREATE TABLE CaiDatNguoiDung (
                        MaNguoiDung INT PRIMARY KEY,
                        ThongBaoEmail BIT NOT NULL DEFAULT 1,
                        GhiNhoDangNhap BIT NOT NULL DEFAULT 1,
                        TuDongCapNhat BIT NOT NULL DEFAULT 1,
                        XacNhanThanhToan BIT NOT NULL DEFAULT 1,
                        NgayCapNhat DATETIME NOT NULL DEFAULT GETDATE(),
                        CONSTRAINT FK_CaiDat_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE
                    );
                END";
            try
            {
                DatabaseHelper.ExecuteNonQuery(sql);
                _tableCaiDatChecked = true;
            }
            catch { }
        }

        // Lấy cài đặt người dùng từ CSDL
        public (bool ThongBaoEmail, bool GhiNhoDangNhap, bool TuDongCapNhat, bool XacNhanThanhToan) LayCaiDat(int maNguoiDung)
        {
            EnsureCaiDatTable();
            string query = "SELECT ThongBaoEmail, GhiNhoDangNhap, TuDongCapNhat, XacNhanThanhToan FROM CaiDatNguoiDung WHERE MaNguoiDung = @MaNguoiDung";
            SqlParameter[] p = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, p);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                return (
                    Convert.ToBoolean(r["ThongBaoEmail"]),
                    Convert.ToBoolean(r["GhiNhoDangNhap"]),
                    Convert.ToBoolean(r["TuDongCapNhat"]),
                    Convert.ToBoolean(r["XacNhanThanhToan"])
                );
            }
            return (true, true, true, true);
        }

        // Lưu cài đặt người dùng vào CSDL
        public bool LuuCaiDat(int maNguoiDung, bool thongBaoEmail, bool ghiNhoDangNhap, bool tuDongCapNhat, bool xacNhanThanhToan)
        {
            EnsureCaiDatTable();
            string query = @"
                IF EXISTS (SELECT 1 FROM CaiDatNguoiDung WHERE MaNguoiDung = @MaNguoiDung)
                BEGIN
                    UPDATE CaiDatNguoiDung
                    SET ThongBaoEmail = @Email, GhiNhoDangNhap = @Remember, TuDongCapNhat = @Auto,
                        XacNhanThanhToan = @Confirm, NgayCapNhat = GETDATE()
                    WHERE MaNguoiDung = @MaNguoiDung;
                END
                ELSE
                BEGIN
                    INSERT INTO CaiDatNguoiDung (MaNguoiDung, ThongBaoEmail, GhiNhoDangNhap, TuDongCapNhat, XacNhanThanhToan)
                    VALUES (@MaNguoiDung, @Email, @Remember, @Auto, @Confirm);
                END";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@Email", thongBaoEmail),
                new SqlParameter("@Remember", ghiNhoDangNhap),
                new SqlParameter("@Auto", tuDongCapNhat),
                new SqlParameter("@Confirm", xacNhanThanhToan)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }

    // Repository quản lý metadata tệp kỹ thuật số
    public class TepSanPhamRepository
    {
        public TepSanPhamDTO? LayTepTheoSanPham(int maSanPham)
        {
            string query = "SELECT * FROM TepSanPham WHERE MaSanPham = @MaSanPham";
            SqlParameter[] parameters = { new SqlParameter("@MaSanPham", maSanPham) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new TepSanPhamDTO
                {
                    MaTep = Convert.ToInt32(row["MaTep"]),
                    MaSanPham = Convert.ToInt32(row["MaSanPham"]),
                    TenTep = row["TenTep"].ToString() ?? string.Empty,
                    DuongDanTep = row["DuongDanTep"].ToString() ?? string.Empty,
                    DinhDangTep = row["DinhDangTep"].ToString() ?? ".zip",
                    KichThuocByte = Convert.ToInt64(row["KichThuocByte"]),
                    PhienBan = row["PhienBan"].ToString() ?? "1.0.0",
                    NgayTaiLen = Convert.ToDateTime(row["NgayTaiLen"])
                };
            }
            return null;
        }

        public bool LuuThongTinTep(TepSanPhamDTO dto)
        {
            // Xóa tệp cũ nếu đã có và ghi đè
            string query = @"
                IF EXISTS (SELECT 1 FROM TepSanPham WHERE MaSanPham = @MaSanPham)
                BEGIN
                    UPDATE TepSanPham
                    SET TenTep = @TenTep, DuongDanTep = @DuongDanTep, DinhDangTep = @DinhDangTep,
                        KichThuocByte = @KichThuocByte, PhienBan = @PhienBan, NgayTaiLen = GETDATE()
                    WHERE MaSanPham = @MaSanPham
                END
                ELSE
                BEGIN
                    INSERT INTO TepSanPham (MaSanPham, TenTep, DuongDanTep, DinhDangTep, KichThuocByte, PhienBan, NgayTaiLen)
                    VALUES (@MaSanPham, @TenTep, @DuongDanTep, @DinhDangTep, @KichThuocByte, @PhienBan, GETDATE())
                END";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaSanPham", dto.MaSanPham),
                new SqlParameter("@TenTep", dto.TenTep),
                new SqlParameter("@DuongDanTep", dto.DuongDanTep),
                new SqlParameter("@DinhDangTep", dto.DinhDangTep),
                new SqlParameter("@KichThuocByte", dto.KichThuocByte),
                new SqlParameter("@PhienBan", dto.PhienBan)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
