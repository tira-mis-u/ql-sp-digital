namespace qlspdigital.Shared
{
    // Lớp lưu trữ phiên làm việc tĩnh của người dùng đang đăng nhập
    // Cho phép mọi UserControl truy cập nhanh thông tin tài khoản hiện hành
    // Phụ trách: Trần Quốc Thuận (Nhóm 4)
    public static class UserSession
    {
        public static int CurrentUserId { get; set; } = 0;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentFullName { get; set; } = "Khách (Chưa đăng nhập)";
        public static string CurrentEmail { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = "KhachHang"; // "QuanTriVien" hoặc "KhachHang"
        public static decimal CurrentBalance { get; set; } = 0;

        public static bool IsLoggedIn => CurrentUserId > 0;
        public static bool IsAdmin => IsLoggedIn && CurrentRole == "QuanTriVien";

        // Khởi tạo phiên làm việc khi đăng nhập thành công
        public static void Login(int userId, string username, string fullName, string email, string role, decimal balance)
        {
            CurrentUserId = userId;
            CurrentUsername = username;
            CurrentFullName = fullName;
            CurrentEmail = email;
            CurrentRole = role;
            CurrentBalance = balance;
        }

        // Cập nhật số dư ví trong phiên
        public static void UpdateBalance(decimal newBalance)
        {
            CurrentBalance = newBalance;
        }

        // Cập nhật thông tin cá nhân trong phiên
        public static void UpdateProfile(string fullName, string email)
        {
            CurrentFullName = fullName;
            CurrentEmail = email;
        }

        // Đăng xuất khỏi hệ thống
        public static void Logout()
        {
            CurrentUserId = 0;
            CurrentUsername = string.Empty;
            CurrentFullName = "Khách (Chưa đăng nhập)";
            CurrentEmail = string.Empty;
            CurrentRole = "KhachHang";
            CurrentBalance = 0;
        }
    }
}
