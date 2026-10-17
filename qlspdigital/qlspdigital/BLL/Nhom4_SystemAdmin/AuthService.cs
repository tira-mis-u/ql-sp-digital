using System;
using System.Collections.Generic;
using qlspdigital.DAL.Nhom4_SystemAdmin;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.BLL.Nhom4_SystemAdmin
{
    // Service xử lý nghiệp vụ xác thực đăng nhập & đăng ký
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class AuthService
    {
        private readonly AuthRepository _repo;

        public AuthService()
        {
            _repo = new AuthRepository();
        }

        public bool DangNhap(string tenDangNhap, string matKhau, out string thongBao)
        {
            bool ok = _repo.DangNhap(tenDangNhap, matKhau, out NguoiDungDTO? user, out thongBao);
            if (ok && user != null)
            {
                // Khởi tạo phiên làm việc tĩnh
                UserSession.Login(user.MaNguoiDung, user.TenDangNhap, user.HoTen, user.Email, user.VaiTro, user.SoDuVi);
            }
            return ok;
        }

        public bool DangKy(string tenDangNhap, string matKhau, string xacNhanMatKhau, string hoTen, string email, string? soDienThoai, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                thongBao = "Tên đăng nhập không được để trống!";
                return false;
            }

            if (tenDangNhap.Length < 3)
            {
                thongBao = "Tên đăng nhập phải có ít nhất 3 ký tự!";
                return false;
            }

            if (tenDangNhap.Length > 50)
            {
                thongBao = "Tên đăng nhập không được vượt quá 50 ký tự!";
                return false;
            }

            if (tenDangNhap.Any(char.IsWhiteSpace))
            {
                thongBao = "Tên đăng nhập không được chứa khoảng trắng!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(matKhau) || matKhau.Length < 6)
            {
                thongBao = "Mật khẩu phải có độ dài tối thiểu 6 ký tự!";
                return false;
            }

            if (!matKhau.Equals(xacNhanMatKhau))
            {
                thongBao = "Mật khẩu xác nhận không khớp!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                thongBao = "Họ tên không được để trống!";
                return false;
            }

            if (hoTen.Trim().Length > 100)
            {
                thongBao = "Họ tên không được vượt quá 100 ký tự!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || email.Trim().Length > 100)
            {
                thongBao = "Địa chỉ email không hợp lệ (phải chứa '@' và dưới 100 ký tự)!";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(soDienThoai) && soDienThoai.Trim().Length > 20)
            {
                thongBao = "Số điện thoại không được vượt quá 20 ký tự!";
                return false;
            }

            return _repo.DangKy(tenDangNhap, matKhau, hoTen, email, soDienThoai, out thongBao);
        }

        public void DangXuat()
        {
            UserSession.Logout();
        }
    }

    // Service quản lý tài khoản người dùng cho Quản trị viên
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class NguoiDungService
    {
        private readonly NguoiDungRepository _repo;

        public NguoiDungService()
        {
            _repo = new NguoiDungRepository();
        }

        public List<NguoiDungDTO> LayDanhSachNguoiDung(string? vaiTro = null, string? tuKhoa = null)
        {
            return _repo.LayDanhSachNguoiDung(vaiTro, tuKhoa);
        }

        public bool KhoaTaiKhoan(int maNguoiDung, out string thongBao)
        {
            return _repo.CapNhatTrangThaiKhoa(maNguoiDung, false, out thongBao);
        }

        public bool MoKhoaTaiKhoan(int maNguoiDung, out string thongBao)
        {
            return _repo.CapNhatTrangThaiKhoa(maNguoiDung, true, out thongBao);
        }
    }

    // Service thống kê chỉ số Dashboard & Top bán chạy
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public class DashboardService
    {
        private readonly DashboardRepository _repo;

        public DashboardService()
        {
            _repo = new DashboardRepository();
        }

        public DashboardStatsDTO LayChiSoTongHop()
        {
            return _repo.LayChiSoTongHop();
        }

        public List<TopSanPhamDTO> LayTop5BanChay()
        {
            return _repo.LayTop5BanChay();
        }
    }
}
