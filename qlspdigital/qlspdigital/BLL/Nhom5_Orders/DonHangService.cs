using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using qlspdigital.DAL.Nhom5_Orders;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.BLL.Nhom5_Orders
{
    // Service xử lý nghiệp vụ Quản lý đơn hàng (Admin) & Lịch sử mua hàng (Khách)
    // Thành viên phụ trách: Phạm Văn Đam (Nhóm 5)
    public class DonHangService
    {
        private readonly DonHangRepository _repo;

        public DonHangService()
        {
            _repo = new DonHangRepository();
        }

        public List<DonHangDTO> LayTatCaDonHang(string? trangThai = null, string? tuKhoa = null)
        {
            return _repo.LayTatCaDonHang(trangThai, tuKhoa);
        }

        public List<DonHangDTO> LayLichSuDonHang(int maNguoiDung)
        {
            if (maNguoiDung <= 0) return new List<DonHangDTO>();
            return _repo.LayLichSuDonHang(maNguoiDung);
        }

        public List<ChiTietDonHangDTO> LayChiTietDonHang(int maDonHang)
        {
            if (maDonHang <= 0) return new List<ChiTietDonHangDTO>();
            return _repo.LayChiTietDonHang(maDonHang);
        }

        public bool DuyetDonHang(int maDonHang, out string thongBao)
        {
            return _repo.CapNhatTrangThaiDonHang(maDonHang, "Hoàn thành", out thongBao);
        }

        public bool HuyDonHang(int maDonHang, out string thongBao)
        {
            return _repo.CapNhatTrangThaiDonHang(maDonHang, "Đã hủy", out thongBao);
        }
    }

    // Service xử lý nghiệp vụ Thư viện số & Tải tệp sở hữu
    // Thành viên phụ trách: Phạm Văn Đam (Nhóm 5)
    public class ThuVienService
    {
        private readonly ThuVienRepository _repo;

        public ThuVienService()
        {
            _repo = new ThuVienRepository();
        }

        public List<ThuVienItemDTO> LayKhoThuVien(int maNguoiDung)
        {
            if (maNguoiDung <= 0) return new List<ThuVienItemDTO>();
            return _repo.LayKhoThuVien(maNguoiDung);
        }

        // Mô phỏng quá trình tải xuống tệp kỹ thuật số có ProgressBar và tăng SoLanTai
        public async Task<bool> TaiXuongTepAsync(ThuVienItemDTO item, string thuMucLuu, Action<int> onProgress)
        {
            // Mô phỏng thanh tiến trình tải từ 0% đến 100%
            for (int p = 0; p <= 100; p += 10)
            {
                onProgress(p);
                await Task.Delay(100);
            }

            // Tạo file zip/txt mô phỏng trong thư mục khách hàng chọn
            try
            {
                if (!Directory.Exists(thuMucLuu))
                {
                    Directory.CreateDirectory(thuMucLuu);
                }

                string safeName = string.Join("_", item.TenSanPham.Split(Path.GetInvalidFileNameChars()));
                string targetFile = Path.Combine(thuMucLuu, $"{safeName}_v{item.PhienBan}.zip");

                if (!File.Exists(targetFile))
                {
                    File.WriteAllText(targetFile, $"DPMS Digital Product License File\nProduct: {item.TenSanPham}\nVersion: {item.PhienBan}\nDownloaded on: {DateTime.Now}");
                }

                // Cập nhật lượt tải trong CSDL
                _repo.CapNhatLanTai(item.MaSoHuu);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GoKhoiThuVien(int maSoHuu, int maNguoiDung, out string thongBao)
        {
            bool ok = _repo.GoKhoiThuVien(maSoHuu, maNguoiDung);
            thongBao = ok ? "Đã gỡ sản phẩm khỏi thư viện cá nhân của bạn." : "Gỡ sản phẩm thất bại.";
            return ok;
        }
    }
}
