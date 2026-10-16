using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace qlspdigital.Shared
{
    // Lớp tiện ích mã hóa bảo mật, băm mật khẩu SHA-256
    // Phụ trách: Trần Quốc Thuận (Nhóm 4)
    public static class SecurityHelper
    {
        // Băm chuỗi văn bản thuần thành chuỗi băm SHA-256 viết thường (64 ký tự hex)
        // Khớp với dữ liệu mẫu trong DPMS_Database_Master.sql (VD: '123456' -> 8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92)
        public static string ComputeSha256Hash(string rawData)
        {
            if (string.IsNullOrEmpty(rawData)) return string.Empty;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // So khớp mật khẩu nhập vào với mã băm trong CSDL
        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashOfInput = ComputeSha256Hash(inputPassword);
            return string.Equals(hashOfInput, storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }

    // Lớp định dạng hiển thị tiền tệ, ngày giờ, kích thước tệp
    public static class FormatHelper
    {
        private static readonly CultureInfo VietNamCulture = new CultureInfo("vi-VN");

        // Định dạng tiền tệ VNĐ (VD: 150000 -> "150.000 ₫" hoặc "Miễn phí")
        public static string FormatCurrency(decimal amount)
        {
            if (amount <= 0) return "Miễn phí";
            return string.Format(VietNamCulture, "{0:N0} ₫", amount);
        }

        // Định dạng ngày giờ chuẩn Việt Nam (dd/MM/yyyy HH:mm)
        public static string FormatDateTime(DateTime? dateTime)
        {
            if (!dateTime.HasValue) return "---";
            return dateTime.Value.ToString("dd/MM/yyyy HH:mm");
        }

        // Định dạng ngày (dd/MM/yyyy)
        public static string FormatDate(DateTime? dateTime)
        {
            if (!dateTime.HasValue) return "---";
            return dateTime.Value.ToString("dd/MM/yyyy");
        }

        // Định dạng dung lượng tệp (MB hoặc GB)
        public static string FormatFileSize(decimal dungLuongMB)
        {
            if (dungLuongMB >= 1024)
            {
                return $"{dungLuongMB / 1024m:0.##} GB";
            }
            return $"{dungLuongMB:0.##} MB";
        }
    }
}
