using System;
using System.Globalization;

namespace qlspdigital.Shared
{
    // Lớp định dạng hiển thị tiền tệ, ngày giờ, kích thước tệp
    public static class FormatHelper
    {
        private static readonly CultureInfo VietNamCulture = new CultureInfo("vi-VN");

        // Định dạng tiền tệ VNĐ (VD: 150000 -> "150.000 ₫", nếu amount <= 0: "0 ₫", trừ khi allowFreeText = true thì "Miễn phí")
        public static string FormatCurrency(decimal amount, bool allowFreeText = false)
        {
            if (amount <= 0 && allowFreeText) return "Miễn phí";
            if (amount <= 0) return "0 ₫";
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
