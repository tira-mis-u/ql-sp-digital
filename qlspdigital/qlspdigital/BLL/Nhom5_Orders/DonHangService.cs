using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
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

        // IDOR-safe: yêu cầu thêm requestorId (0 = Admin, bỏ qua kiểm tra sở hữu)
        public List<ChiTietDonHangDTO> LayChiTietDonHang(int maDonHang, int requestorId = 0)
        {
            if (maDonHang <= 0) return new List<ChiTietDonHangDTO>();
            return _repo.LayChiTietDonHang(maDonHang, requestorId);
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

        // Tải xuống tệp kỹ thuật số nén ZIP chuẩn (PK zip) tương thích 100% với Windows Explorer, 7-Zip, WinRAR
        public async Task<bool> TaiXuongTepAsync(ThuVienItemDTO item, string thuMucLuu)
        {
            await Task.Delay(120);

            try
            {
                if (!Directory.Exists(thuMucLuu))
                {
                    Directory.CreateDirectory(thuMucLuu);
                }

                string safeName = string.Join("_", item.TenSanPham.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)).Trim().Replace(' ', '_');
                string targetFile = Path.Combine(thuMucLuu, $"{safeName}_v{item.PhienBan}.zip");

                // Tìm tệp gốc đã upload của sản phẩm nếu có
                string? resolvedSourcePath = null;
                if (item.DuongDanTep != null && item.DuongDanTep.Trim() != "")
                {
                    if (Path.IsPathRooted(item.DuongDanTep) && File.Exists(item.DuongDanTep))
                    {
                        resolvedSourcePath = item.DuongDanTep;
                    }
                    else
                    {
                        string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, item.DuongDanTep.Replace('/', Path.DirectorySeparatorChar));
                        if (File.Exists(localPath))
                        {
                            resolvedSourcePath = localPath;
                        }
                    }
                }

                await Task.Delay(150);

                // Xóa tệp zip cũ nếu đã tồn tại để tạo tệp nén mới toàn vẹn
                if (File.Exists(targetFile))
                {
                    File.Delete(targetFile);
                }

                await Task.Delay(150);

                // Tạo file .zip chuẩn nén (PK zip specification) tương thích 100% với Windows Explorer, WinRAR, 7-Zip
                using (FileStream fs = new FileStream(targetFile, FileMode.Create, FileAccess.Write, FileShare.None))
                using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Create, false, Encoding.UTF8))
                {
                    // 1. Đóng gói tệp phần mềm / dữ liệu gốc (ví dụ: .exe, .iso, .zip)
                    if (!string.IsNullOrEmpty(resolvedSourcePath) && File.Exists(resolvedSourcePath))
                    {
                        string entryFileName = Path.GetFileName(resolvedSourcePath);
                        archive.CreateEntryFromFile(resolvedSourcePath, entryFileName, CompressionLevel.Optimal);
                    }
                    else
                    {
                        // Nếu sản phẩm mẫu chưa có tệp vật lý trên đĩa, tạo tệp thực thi / mô phỏng chuẩn
                        string ext = !string.IsNullOrEmpty(item.DuongDanTep) ? Path.GetExtension(item.DuongDanTep) : ".exe";
                        if (string.IsNullOrEmpty(ext)) ext = ".exe";
                        string entryFileName = $"{safeName}{ext}";

                        ZipArchiveEntry mainEntry = archive.CreateEntry(entryFileName, CompressionLevel.Optimal);
                        using (Stream entryStream = mainEntry.Open())
                        using (StreamWriter writer = new StreamWriter(entryStream, Encoding.UTF8))
                        {
                            writer.WriteLine($"# DPMS Digital Product Distribution Package");
                            writer.WriteLine($"# Product: {item.TenSanPham}");
                            writer.WriteLine($"# Version: {item.PhienBan}");
                            writer.WriteLine($"# Order Code: #{item.MaDonHang}");
                            writer.WriteLine($"# Category: {item.TenDanhMuc}");
                            writer.WriteLine($"# Licensed to User ID: {item.MaNguoiDung}");
                            writer.WriteLine($"# Built with: Visual Studio C# WinForms .NET 10.0");
                        }
                    }

                    // 2. Đính kèm giấy phép số LICENSE_AND_KEY.txt
                    ZipArchiveEntry licEntry = archive.CreateEntry("LICENSE_AND_KEY.txt", CompressionLevel.Optimal);
                    using (Stream licStream = licEntry.Open())
                    using (StreamWriter writer = new StreamWriter(licStream, Encoding.UTF8))
                    {
                        writer.WriteLine("==================================================================");
                        writer.WriteLine("          DPMS DIGITAL PRODUCT LICENSE & VERIFICATION CERTIFICATE ");
                        writer.WriteLine("==================================================================");
                        writer.WriteLine($"Sản phẩm           : {item.TenSanPham}");
                        writer.WriteLine($"Phiên bản          : {item.PhienBan}");
                        writer.WriteLine($"Mã đơn hàng        : #{item.MaDonHang}");
                        writer.WriteLine($"Mã người dùng      : {item.MaNguoiDung}");
                        writer.WriteLine($"Thời điểm cấp quyền: {item.NgaySoHuu:dd/MM/yyyy HH:mm:ss}");
                        writer.WriteLine($"Thời điểm tải tệp  : {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                        if (!string.IsNullOrEmpty(item.MaHashSHA256))
                        {
                            writer.WriteLine($"Mã SHA-256 xác thực: {item.MaHashSHA256}");
                        }
                        writer.WriteLine("==================================================================");
                        writer.WriteLine("Bản quyền kỹ thuật số được chứng nhận bởi DPMS Platform.");
                        writer.WriteLine("Chúc bạn có trải nghiệm tuyệt vời với sản phẩm!");
                    }
                }

                await Task.Delay(150);

                // Cập nhật lượt tải trong CSDL
                _repo.CapNhatLanTai(item.MaSoHuu);

                await Task.Delay(100);
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
