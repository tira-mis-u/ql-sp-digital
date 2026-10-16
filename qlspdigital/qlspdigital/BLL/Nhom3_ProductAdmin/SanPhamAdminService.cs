using System;
using System.Collections.Generic;
using qlspdigital.DAL.Nhom3_ProductAdmin;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.BLL.Nhom3_ProductAdmin
{
    // Service quản lý sản phẩm số và tệp số cho Admin
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public class SanPhamAdminService
    {
        private readonly SanPhamAdminRepository _repo;
        private readonly TepSanPhamRepository _tepRepo;

        public SanPhamAdminService()
        {
            _repo = new SanPhamAdminRepository();
            _tepRepo = new TepSanPhamRepository();
        }

        public List<SanPhamDTO> LayDanhSachSanPham(string? tuKhoa = null, int? maDanhMuc = null)
        {
            return _repo.LayDanhSachAdmin(tuKhoa, maDanhMuc);
        }

        public bool LuuSanPham(SanPhamDTO sp, TepSanPhamDTO? tep, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(sp.TenSanPham))
            {
                thongBao = "Tên sản phẩm không được để trống!";
                return false;
            }

            if (sp.GiaBan < 0)
            {
                thongBao = "Giá bán không được là số âm!";
                return false;
            }

            if (sp.MaDanhMuc <= 0)
            {
                thongBao = "Vui lòng chọn danh mục hợp lệ cho sản phẩm!";
                return false;
            }

            if (sp.MaSanPham > 0)
            {
                // Cập nhật sản phẩm cũ
                bool ok = _repo.CapNhatSanPham(sp);
                if (ok && tep != null)
                {
                    tep.MaSanPham = sp.MaSanPham;
                    _tepRepo.LuuThongTinTep(tep);
                }
                thongBao = ok ? "Cập nhật sản phẩm thành công!" : "Cập nhật sản phẩm thất bại.";
                return ok;
            }
            else
            {
                // Thêm sản phẩm mới
                bool ok = _repo.ThemSanPham(sp, out int maSanPhamMoi);
                if (ok)
                {
                    if (tep != null)
                    {
                        tep.MaSanPham = maSanPhamMoi;
                        _tepRepo.LuuThongTinTep(tep);
                    }
                    thongBao = "Thêm sản phẩm mới thành công!";
                    return true;
                }
                thongBao = "Thêm sản phẩm mới thất bại.";
                return false;
            }
        }

        public bool XoaSanPham(int maSanPham, out string thongBao)
        {
            try
            {
                bool ok = _repo.XoaSanPham(maSanPham);
                thongBao = ok ? "Đã xóa sản phẩm thành công!" : "Xóa sản phẩm thất bại.";
                return ok;
            }
            catch (Exception ex)
            {
                thongBao = "Không thể xóa sản phẩm vì đã có đơn hàng phát sinh liên quan: " + ex.Message;
                return false;
            }
        }

        public TepSanPhamDTO? LayTepTheoSanPham(int maSanPham)
        {
            return _tepRepo.LayTepTheoSanPham(maSanPham);
        }
    }

    // Service quản lý thể loại / danh mục hàng hóa
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public class DanhMucService
    {
        private readonly DanhMucRepository _repo;

        public DanhMucService()
        {
            _repo = new DanhMucRepository();
        }

        public List<DanhMucDTO> LayTatCaDanhMuc()
        {
            return _repo.LayTatCaDanhMuc();
        }

        public bool LuuDanhMuc(DanhMucDTO dm, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(dm.TenDanhMuc))
            {
                thongBao = "Tên danh mục không được để trống!";
                return false;
            }

            if (dm.MaDanhMuc > 0)
            {
                bool ok = _repo.CapNhatDanhMuc(dm);
                thongBao = ok ? "Cập nhật danh mục thành công!" : "Cập nhật thất bại.";
                return ok;
            }
            else
            {
                bool ok = _repo.ThemDanhMuc(dm);
                thongBao = ok ? "Thêm danh mục mới thành công!" : "Thêm danh mục thất bại.";
                return ok;
            }
        }

        public bool XoaDanhMuc(int maDanhMuc, out string thongBao)
        {
            return _repo.XoaDanhMuc(maDanhMuc, out thongBao);
        }
    }

    // Service quản lý hồ sơ cá nhân
    // Thành viên phụ trách: Phạm Trung Hiếu (Nhóm 3)
    public class HoSoService
    {
        private readonly HoSoRepository _repo;

        public HoSoService()
        {
            _repo = new HoSoRepository();
        }

        public NguoiDungDTO? LayHoSo(int maNguoiDung)
        {
            return _repo.LayThongTinHoSo(maNguoiDung);
        }

        public bool CapNhatHoSo(int maNguoiDung, string hoTen, string email, string? soDienThoai, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                thongBao = "Họ tên không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                thongBao = "Địa chỉ email không hợp lệ!";
                return false;
            }

            return _repo.CapNhatHoSo(maNguoiDung, hoTen, email, soDienThoai, out thongBao);
        }

        public bool DoiMatKhau(int maNguoiDung, string matKhauCu, string matKhauMoi, string xacNhanMatKhau, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(matKhauCu) || string.IsNullOrWhiteSpace(matKhauMoi))
            {
                thongBao = "Vui lòng nhập đầy đủ mật khẩu cũ và mật khẩu mới!";
                return false;
            }

            if (matKhauMoi.Length < 6)
            {
                thongBao = "Mật khẩu mới phải có tối thiểu 6 ký tự!";
                return false;
            }

            if (!matKhauMoi.Equals(xacNhanMatKhau))
            {
                thongBao = "Mật khẩu xác nhận không trùng khớp với mật khẩu mới!";
                return false;
            }

            return _repo.DoiMatKhau(maNguoiDung, matKhauCu, matKhauMoi, out thongBao);
        }
    }
}
