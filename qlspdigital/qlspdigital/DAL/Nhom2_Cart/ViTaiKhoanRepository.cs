using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom2_Cart
{
    // Repository quản lý ví tài khoản cá nhân & nạp tiền mô phỏng (Steam Wallet)
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class ViTaiKhoanRepository
    {
        // Lấy số dư ví hiện tại của người dùng (Read)
        public decimal LaySoDuVi(int maNguoiDung)
        {
            string query = "SELECT SoDuVi FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
            SqlParameter[] parameters = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            object? result = DatabaseHelper.ExecuteScalar(query, parameters);

            if (result != null && result != DBNull.Value)
            {
                return Convert.ToDecimal(result);
            }
            return 0;
        }

        // Nạp tiền vào ví và ghi nhật ký giao dịch (Update + Create)
        public bool NapTienVi(int maNguoiDung, decimal soTien)
        {
            if (soTien <= 0) return false;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Cộng tiền vào số dư ví của NguoiDung
                        string queryUpdate = "UPDATE NguoiDung SET SoDuVi = SoDuVi + @SoTien WHERE MaNguoiDung = @MaNguoiDung";
                        using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn, trans))
                        {
                            cmdUpdate.Parameters.AddWithValue("@SoTien", soTien);
                            cmdUpdate.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        // 2. Ghi nhật ký vào bảng ThanhToan
                        string queryInsertLog = @"
                            INSERT INTO ThanhToan (MaNguoiDung, MaDonHang, SoTien, LoaiGiaoDich, PhuongThuc, TrangThai, NgayTao)
                            VALUES (@MaNguoiDung, NULL, @SoTien, N'Nạp tiền ví', N'Ví Steam Simulation', N'Thành công', GETDATE())";

                        using (SqlCommand cmdLog = new SqlCommand(queryInsertLog, conn, trans))
                        {
                            cmdLog.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                            cmdLog.Parameters.AddWithValue("@SoTien", soTien);
                            cmdLog.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        // Lấy lịch sử biến động số dư và giao dịch nạp / thanh toán
        public List<ThanhToanDTO> LayLichSuGiaoDich(int maNguoiDung)
        {
            List<ThanhToanDTO> list = new List<ThanhToanDTO>();

            string query = @"
                SELECT MaThanhToan, MaNguoiDung, MaDonHang, SoTien, LoaiGiaoDich, PhuongThuc, TrangThai, NgayTao
                FROM ThanhToan
                WHERE MaNguoiDung = @MaNguoiDung
                ORDER BY NgayTao DESC";

            SqlParameter[] parameters = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ThanhToanDTO
                {
                    MaThanhToan = Convert.ToInt32(row["MaThanhToan"]),
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    MaDonHang = row["MaDonHang"] != DBNull.Value ? Convert.ToInt32(row["MaDonHang"]) : null,
                    SoTien = Convert.ToDecimal(row["SoTien"]),
                    LoaiGiaoDich = row["LoaiGiaoDich"].ToString() ?? string.Empty,
                    PhuongThuc = row["PhuongThuc"].ToString() ?? string.Empty,
                    TrangThai = row["TrangThai"].ToString() ?? string.Empty,
                    NgayTao = Convert.ToDateTime(row["NgayTao"])
                });
            }

            return list;
        }
    }
}
