using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace qlspdigital.Shared
{
    // Lớp tiện ích quản lý kết nối và thao tác với SQL Server (ADO.NET)
    // Dùng chung cho toàn bộ 5 phân hệ
    public static class DatabaseHelper
    {
        // Chuỗi kết nối mặc định đến SQL Server (ưu tiên localhost\SQLEXPRESS01 theo cấu hình máy của bạn)
        // Hỗ trợ TrustServerCertificate để tránh lỗi SSL chứng chỉ khi chạy local
        public static string ConnectionString { get; set; } =
            @"Server=localhost\SQLEXPRESS01;Database=DPMS_Db;Integrated Security=True;TrustServerCertificate=True;";

        // Danh sách các chuỗi kết nối dự phòng để tự động thử nếu chuỗi chính chưa mở được
        private static readonly string[] FallbackConnectionStrings = new[]
        {
            @"Server=localhost\SQLEXPRESS01;Database=DPMS_Db;Integrated Security=True;TrustServerCertificate=True;",
            @"Server=.;Database=DPMS_Db;Integrated Security=True;TrustServerCertificate=True;",
            @"Server=localhost\SQLEXPRESS;Database=DPMS_Db;Integrated Security=True;TrustServerCertificate=True;",
            @"Server=localhost;Database=DPMS_Db;Integrated Security=True;TrustServerCertificate=True;",
            @"Server=(localdb)\MSSQLLocalDB;Database=DPMS_Db;Integrated Security=True;TrustServerCertificate=True;"
        };

        // Tạo và mở đối tượng SqlConnection.
        // Tự động ưu tiên localhost\SQLEXPRESS01, nếu lỗi sẽ tự động dò các server local khác (., SQLEXPRESS...).
        public static SqlConnection GetConnection()
        {
            // 1. Thử chuỗi kết nối chính hiện hành
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch
            {
                // 2. Nếu thất bại, duyệt qua danh sách các chuỗi kết nối dự phòng
                foreach (string candidate in FallbackConnectionStrings)
                {
                    if (string.Equals(candidate, ConnectionString, StringComparison.OrdinalIgnoreCase)) // So sánh từng ký tự, không phân biệt chữ hoa chữ thường
                        continue;

                    try
                    {
                        SqlConnection candidateConn = new SqlConnection(candidate);
                        candidateConn.Open();
                        ConnectionString = candidate; // Ghi nhớ lại chuỗi kết nối thành công để các lần sau gọi trực tiếp
                        return candidateConn;
                    }
                    catch
                    {
                        // Thử tiếp ứng viên kế tiếp
                    }
                }

                throw new Exception("Không thể kết nối đến cơ sở dữ liệu DPMS_Db trên SQL Server (đã thử: localhost\\SQLEXPRESS01, ., localhost\\SQLEXPRESS).\n" +
                                    "Vui lòng đảm bảo dịch vụ SQL Server (SQLEXPRESS01) đang được Start và bạn đã chạy script 'DPMS_Database_Master.sql' để tạo database DPMS_Db.");
            }
        }

        // Thực thi truy vấn SELECT và trả về DataTable
        public static DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // Thực thi lệnh INSERT, UPDATE, DELETE và trả về số dòng bị ảnh hưởng
        public static int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Thực thi lệnh trả về 1 giá trị duy nhất (COUNT, SUM, MAX, IDENTITY...)
        public static object? ExecuteScalar(string query, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteScalar();
                }
            }
        }

        // Kiểm tra nhanh kết nối cơ sở dữ liệu
        public static bool TestConnection(out string message)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    message = "Kết nối SQL Server thành công!";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
