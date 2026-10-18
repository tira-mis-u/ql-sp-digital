using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom5_Orders
{
    // Repository quản lý đơn hàng toàn sàn và lịch sử mua sắm
    // Thành viên phụ trách: Phạm Văn Đam (Nhóm 5)
    public class DonHangRepository
    {
        // Lấy toàn bộ đơn hàng toàn sàn cho Admin duyệt và lọc theo trạng thái (UI 13)
        public List<DonHangDTO> LayTatCaDonHang(string? trangThai = null, string? tuKhoa = null)
        {
            List<DonHangDTO> list = new List<DonHangDTO>();

            string query = @"
                SELECT dh.MaDonHang, dh.MaDonHangCode, dh.MaNguoiDung, dh.MaGiamGiaId,
                       dh.TamTinh, dh.TienGiamGia, dh.TongThanhToan, dh.TrangThaiDonHang,
                       dh.GhiChu, dh.NgayTao, nd.HoTen AS TenKhachHang, mgg.MaCode AS MaVoucherCode
                FROM DonHang dh
                INNER JOIN NguoiDung nd ON dh.MaNguoiDung = nd.MaNguoiDung
                LEFT JOIN MaGiamGia mgg ON dh.MaGiamGiaId = mgg.MaGiamGiaId
                WHERE 1 = 1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(trangThai) && !trangThai.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
            {
                query += " AND dh.TrangThaiDonHang = @TrangThai";
                parameters.Add(new SqlParameter("@TrangThai", trangThai.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query += " AND (dh.MaDonHangCode LIKE @TuKhoa OR nd.HoTen LIKE @TuKhoa)";
                string safeKey = tuKhoa.Trim().Replace("[", "[[]");
                parameters.Add(new SqlParameter("@TuKhoa", $"%{safeKey}%"));
            }

            query += " ORDER BY dh.NgayTao DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRowToDto(row));
            }

            return list;
        }

        // Lấy lịch sử mua hàng của cá nhân khách hàng (UI 7)
        public List<DonHangDTO> LayLichSuDonHang(int maNguoiDung)
        {
            List<DonHangDTO> list = new List<DonHangDTO>();

            string query = @"
                SELECT dh.MaDonHang, dh.MaDonHangCode, dh.MaNguoiDung, dh.MaGiamGiaId,
                       dh.TamTinh, dh.TienGiamGia, dh.TongThanhToan, dh.TrangThaiDonHang,
                       dh.GhiChu, dh.NgayTao, nd.HoTen AS TenKhachHang, mgg.MaCode AS MaVoucherCode
                FROM DonHang dh
                INNER JOIN NguoiDung nd ON dh.MaNguoiDung = nd.MaNguoiDung
                LEFT JOIN MaGiamGia mgg ON dh.MaGiamGiaId = mgg.MaGiamGiaId
                WHERE dh.MaNguoiDung = @MaNguoiDung
                ORDER BY dh.NgayTao DESC";

            SqlParameter[] parameters = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRowToDto(row));
            }

            return list;
        }

        // Lấy chi tiết các sản phẩm trong 1 đơn hàng
        public List<ChiTietDonHangDTO> LayChiTietDonHang(int maDonHang)
        {
            List<ChiTietDonHangDTO> list = new List<ChiTietDonHangDTO>();

            string query = @"
                SELECT ct.MaChiTietDonHang, ct.MaDonHang, ct.MaSanPham, ct.SoLuong, ct.DonGia,
                       sp.TenSanPham
                FROM ChiTietDonHang ct
                INNER JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
                WHERE ct.MaDonHang = @MaDonHang";

            SqlParameter[] parameters = { new SqlParameter("@MaDonHang", maDonHang) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ChiTietDonHangDTO
                {
                    MaChiTietDonHang = Convert.ToInt32(row["MaChiTietDonHang"]),
                    MaDonHang = Convert.ToInt32(row["MaDonHang"]),
                    MaSanPham = Convert.ToInt32(row["MaSanPham"]),
                    TenSanPham = row["TenSanPham"].ToString() ?? string.Empty,
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    DonGia = Convert.ToDecimal(row["DonGia"])
                });
            }

            return list;
        }

        // Admin cập nhật trạng thái đơn hàng (Duyệt / Hủy)
        // Nếu duyệt sang 'Hoàn thành', hệ thống tự động cấp quyền sở hữu vào Thư viện
        public bool CapNhatTrangThaiDonHang(int maDonHang, string trangThaiMoi, out string thongBao)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Cập nhật trạng thái đơn
                        string queryUpdate = "UPDATE DonHang SET TrangThaiDonHang = @TrangThai WHERE MaDonHang = @MaDonHang";
                        using (SqlCommand cmd = new SqlCommand(queryUpdate, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                            cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Nếu chuyển sang 'Hoàn thành', cấp game vào Thư viện SoHuuSanPham
                        if (trangThaiMoi.Equals("Hoàn thành", StringComparison.OrdinalIgnoreCase))
                        {
                            string queryGrant = @"
                                INSERT INTO SoHuuSanPham (MaNguoiDung, MaSanPham, MaDonHang, NgaySoHuu, SoLanTai)
                                SELECT dh.MaNguoiDung, ct.MaSanPham, dh.MaDonHang, GETDATE(), 0
                                FROM DonHang dh
                                INNER JOIN ChiTietDonHang ct ON dh.MaDonHang = ct.MaDonHang
                                WHERE dh.MaDonHang = @MaDonHang
                                  AND NOT EXISTS (
                                      SELECT 1 FROM SoHuuSanPham sh
                                      WHERE sh.MaNguoiDung = dh.MaNguoiDung AND sh.MaSanPham = ct.MaSanPham
                                  )";

                            using (SqlCommand cmdGrant = new SqlCommand(queryGrant, conn, trans))
                            {
                                cmdGrant.Parameters.AddWithValue("@MaDonHang", maDonHang);
                                cmdGrant.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        thongBao = trangThaiMoi == "Hoàn thành"
                            ? $"Đã kích hoạt đơn hàng sang trạng thái 'Hoàn thành' và cấp sản phẩm vào Thư viện thành công!"
                            : $"Đã cập nhật đơn hàng sang trạng thái '{trangThaiMoi}' thành công!";
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        thongBao = "Lỗi khi cập nhật trạng thái: " + ex.Message;
                        return false;
                    }
                }
            }
        }

        // Xử lý giao dịch mua hàng toàn vẹn ACID (Thanh toán ví, tạo đơn, cấp thư viện, xóa giỏ)
        public bool ThucHienThanhToanDonHang(int maNguoiDung, List<GioHangItemDTO> items, decimal tamTinh, decimal tienGiam, int? maGiamGiaId, out string maDonHangCode, out string thongBao)
        {
            maDonHangCode = string.Empty;
            decimal tongThanhToan = Math.Max(0, tamTinh - tienGiam);

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Kiểm tra số dư ví hiện tại
                        string qCheckBal = "SELECT SoDuVi FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
                        decimal soDuHienTai = 0;
                        using (SqlCommand cmdBal = new SqlCommand(qCheckBal, conn, trans))
                        {
                            cmdBal.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                            object? res = cmdBal.ExecuteScalar();
                            if (res != null) soDuHienTai = Convert.ToDecimal(res);
                        }

                        if (soDuHienTai < tongThanhToan)
                        {
                            trans.Rollback();
                            thongBao = $"Số dư ví không đủ ({FormatHelper.FormatCurrency(soDuHienTai)}). Cần thanh toán {FormatHelper.FormatCurrency(tongThanhToan)}!";
                            return false;
                        }

                        // 2. Trừ tiền ví
                        string qDeduct = "UPDATE NguoiDung SET SoDuVi = SoDuVi - @SoTien WHERE MaNguoiDung = @MaNguoiDung";
                        using (SqlCommand cmdDeduct = new SqlCommand(qDeduct, conn, trans))
                        {
                            cmdDeduct.Parameters.AddWithValue("@SoTien", tongThanhToan);
                            cmdDeduct.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                            cmdDeduct.ExecuteNonQuery();
                        }

                        // 3. Tạo mã đơn hàng duy nhất: ORD-yyyyMMddHHmmss-XXXX
                        string orderCode = $"ORD-{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..4].ToUpper()}";
                        maDonHangCode = orderCode;

                        // 4. Tạo bản ghi DonHang
                        string qInsertOrder = @"
                            INSERT INTO DonHang (MaDonHangCode, MaNguoiDung, MaGiamGiaId, TamTinh, TienGiamGia, TongThanhToan, TrangThaiDonHang, GhiChu, NgayTao)
                            VALUES (@MaDonHangCode, @MaNguoiDung, @MaGiamGiaId, @TamTinh, @TienGiamGia, @TongThanhToan, N'Hoàn thành', N'Thanh toán qua Ví Steam Simulation', GETDATE());
                            SELECT SCOPE_IDENTITY();";

                        int newOrderId = 0;
                        using (SqlCommand cmdOrder = new SqlCommand(qInsertOrder, conn, trans))
                        {
                            cmdOrder.Parameters.AddWithValue("@MaDonHangCode", orderCode);
                            cmdOrder.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                            cmdOrder.Parameters.AddWithValue("@MaGiamGiaId", maGiamGiaId.HasValue ? (object)maGiamGiaId.Value : DBNull.Value);
                            cmdOrder.Parameters.AddWithValue("@TamTinh", tamTinh);
                            cmdOrder.Parameters.AddWithValue("@TienGiamGia", tienGiam);
                            cmdOrder.Parameters.AddWithValue("@TongThanhToan", tongThanhToan);

                            object? resId = cmdOrder.ExecuteScalar();
                            if (resId == null || resId == DBNull.Value)
                            {
                                trans.Rollback();
                                thongBao = "Không thể khởi tạo mã định danh đơn hàng mới.";
                                return false;
                            }
                            newOrderId = Convert.ToInt32(resId);
                        }

                        // 5. Thêm chi tiết đơn hàng & cấp vào SoHuuSanPham
                        foreach (var item in items)
                        {
                            // ChiTietDonHang
                            string qDetail = @"
                                INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuong, DonGia)
                                VALUES (@MaDonHang, @MaSanPham, @SoLuong, @DonGia)";
                            using (SqlCommand cmdDetail = new SqlCommand(qDetail, conn, trans))
                            {
                                cmdDetail.Parameters.AddWithValue("@MaDonHang", newOrderId);
                                cmdDetail.Parameters.AddWithValue("@MaSanPham", item.MaSanPham);
                                cmdDetail.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                                cmdDetail.Parameters.AddWithValue("@DonGia", item.DonGia);
                                cmdDetail.ExecuteNonQuery();
                            }

                            // SoHuuSanPham (Thư viện)
                            string qLibrary = @"
                                IF NOT EXISTS (SELECT 1 FROM SoHuuSanPham WHERE MaNguoiDung = @MaNguoiDung AND MaSanPham = @MaSanPham)
                                BEGIN
                                    INSERT INTO SoHuuSanPham (MaNguoiDung, MaSanPham, MaDonHang, NgaySoHuu, SoLanTai)
                                    VALUES (@MaNguoiDung, @MaSanPham, @MaDonHang, GETDATE(), 0)
                                END";
                            using (SqlCommand cmdLib = new SqlCommand(qLibrary, conn, trans))
                            {
                                cmdLib.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                                cmdLib.Parameters.AddWithValue("@MaSanPham", item.MaSanPham);
                                cmdLib.Parameters.AddWithValue("@MaDonHang", newOrderId);
                                cmdLib.ExecuteNonQuery();
                            }
                        }

                        // 6. Ghi nhật ký bảng ThanhToan
                        string qPayLog = @"
                            INSERT INTO ThanhToan (MaNguoiDung, MaDonHang, SoTien, LoaiGiaoDich, PhuongThuc, TrangThai, NgayTao)
                            VALUES (@MaNguoiDung, @MaDonHang, @SoTien, N'Thanh toán đơn hàng', N'Ví Steam Simulation', N'Thành công', GETDATE())";
                        using (SqlCommand cmdPay = new SqlCommand(qPayLog, conn, trans))
                        {
                            cmdPay.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                            cmdPay.Parameters.AddWithValue("@MaDonHang", newOrderId);
                            cmdPay.Parameters.AddWithValue("@SoTien", tongThanhToan);
                            cmdPay.ExecuteNonQuery();
                        }

                        // 7. Đánh dấu voucher đã dùng và trừ số lượng phát hành còn lại
                        if (maGiamGiaId.HasValue)
                        {
                            string qVoucher = @"
                                IF EXISTS (SELECT 1 FROM MaGiamGiaNguoiDung WHERE MaNguoiDung = @MaNguoiDung AND MaGiamGiaId = @MaGiamGiaId)
                                BEGIN
                                    UPDATE MaGiamGiaNguoiDung
                                    SET DaSuDung = 1, NgaySuDung = GETDATE()
                                    WHERE MaNguoiDung = @MaNguoiDung AND MaGiamGiaId = @MaGiamGiaId;
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO MaGiamGiaNguoiDung (MaNguoiDung, MaGiamGiaId, DaSuDung, NgayNhan, NgaySuDung)
                                    VALUES (@MaNguoiDung, @MaGiamGiaId, 1, GETDATE(), GETDATE());
                                END
                                UPDATE MaGiamGia
                                SET SoLuongConLai = CASE WHEN SoLuongConLai > 0 THEN SoLuongConLai - 1 ELSE 0 END
                                WHERE MaGiamGiaId = @MaGiamGiaId;";
                            using (SqlCommand cmdVoucher = new SqlCommand(qVoucher, conn, trans))
                            {
                                cmdVoucher.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                                cmdVoucher.Parameters.AddWithValue("@MaGiamGiaId", maGiamGiaId.Value);
                                cmdVoucher.ExecuteNonQuery();
                            }
                        }

                        // 8. Xóa sạch giỏ hàng của khách
                        string qClearCart = "DELETE FROM GioHang WHERE MaNguoiDung = @MaNguoiDung";
                        using (SqlCommand cmdClear = new SqlCommand(qClearCart, conn, trans))
                        {
                            cmdClear.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                            cmdClear.ExecuteNonQuery();
                        }

                        trans.Commit();
                        thongBao = "Thanh toán thành công! Sản phẩm đã được thêm vào Thư viện của bạn.";
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        thongBao = "Giao dịch thanh toán thất bại: " + ex.Message;
                        return false;
                    }
                }
            }
        }

        private DonHangDTO MapRowToDto(DataRow row)
        {
            return new DonHangDTO
            {
                MaDonHang = Convert.ToInt32(row["MaDonHang"]),
                MaDonHangCode = row["MaDonHangCode"].ToString() ?? string.Empty,
                MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                TenKhachHang = row["TenKhachHang"].ToString() ?? string.Empty,
                MaGiamGiaId = row["MaGiamGiaId"] != DBNull.Value ? Convert.ToInt32(row["MaGiamGiaId"]) : null,
                MaVoucherCode = row["MaVoucherCode"] != DBNull.Value ? row["MaVoucherCode"].ToString() : null,
                TamTinh = Convert.ToDecimal(row["TamTinh"]),
                TienGiamGia = Convert.ToDecimal(row["TienGiamGia"]),
                TongThanhToan = Convert.ToDecimal(row["TongThanhToan"]),
                TrangThaiDonHang = row["TrangThaiDonHang"].ToString() ?? "Hoàn thành",
                GhiChu = row["GhiChu"] != DBNull.Value ? row["GhiChu"].ToString() : null,
                NgayTao = Convert.ToDateTime(row["NgayTao"])
            };
        }
    }

    // Repository quản lý kho game và sản phẩm đã sở hữu (Thư viện)
    // Thành viên phụ trách: Phạm Văn Đam (Nhóm 5)
    public class ThuVienRepository
    {
        // Lấy toàn bộ sản phẩm trong Thư viện của người dùng (Read)
        public List<ThuVienItemDTO> LayKhoThuVien(int maNguoiDung)
        {
            List<ThuVienItemDTO> list = new List<ThuVienItemDTO>();

            string query = @"
                SELECT sh.MaSoHuu, sh.MaNguoiDung, sh.MaSanPham, sh.MaDonHang, sh.NgaySoHuu, sh.SoLanTai, sh.LanTaiCuoi,
                       sp.TenSanPham, sp.HinhAnh, sp.DungLuongMB, sp.PhienBan, dm.TenDanhMuc,
                       ISNULL(tp.DuongDanTep, '') AS DuongDanTep, tp.MaHashSHA256
                FROM SoHuuSanPham sh
                INNER JOIN SanPham sp ON sh.MaSanPham = sp.MaSanPham
                INNER JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                LEFT JOIN TepSanPham tp ON sp.MaSanPham = tp.MaSanPham
                WHERE sh.MaNguoiDung = @MaNguoiDung
                ORDER BY sh.NgaySoHuu DESC";

            SqlParameter[] parameters = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ThuVienItemDTO
                {
                    MaSoHuu = Convert.ToInt32(row["MaSoHuu"]),
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    MaSanPham = Convert.ToInt32(row["MaSanPham"]),
                    TenSanPham = row["TenSanPham"].ToString() ?? string.Empty,
                    HinhAnh = row["HinhAnh"] != DBNull.Value ? row["HinhAnh"].ToString() : null,
                    DungLuongMB = Convert.ToDecimal(row["DungLuongMB"]),
                    PhienBan = row["PhienBan"].ToString() ?? "1.0.0",
                    TenDanhMuc = row["TenDanhMuc"].ToString() ?? string.Empty,
                    MaDonHang = Convert.ToInt32(row["MaDonHang"]),
                    NgaySoHuu = Convert.ToDateTime(row["NgaySoHuu"]),
                    SoLanTai = Convert.ToInt32(row["SoLanTai"]),
                    LanTaiCuoi = row["LanTaiCuoi"] != DBNull.Value ? Convert.ToDateTime(row["LanTaiCuoi"]) : null,
                    DuongDanTep = row["DuongDanTep"].ToString() ?? string.Empty,
                    MaHashSHA256 = row["MaHashSHA256"] != DBNull.Value ? row["MaHashSHA256"].ToString() : null
                });
            }

            return list;
        }

        // Cập nhật số lần tải tệp và thời điểm tải gần nhất (Update)
        public bool CapNhatLanTai(int maSoHuu)
        {
            string query = @"
                UPDATE SoHuuSanPham
                SET SoLanTai = SoLanTai + 1, LanTaiCuoi = GETDATE()
                WHERE MaSoHuu = @MaSoHuu";

            SqlParameter[] parameters = { new SqlParameter("@MaSoHuu", maSoHuu) };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Gỡ sản phẩm khỏi thư viện cá nhân (Delete)
        public bool GoKhoiThuVien(int maSoHuu, int maNguoiDung)
        {
            string query = "DELETE FROM SoHuuSanPham WHERE MaSoHuu = @MaSoHuu AND MaNguoiDung = @MaNguoiDung";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaSoHuu", maSoHuu),
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Kiểm tra người dùng đã sở hữu sản phẩm này trong Thư viện chưa
        public bool DaSoHuu(int maNguoiDung, int maSanPham)
        {
            string query = "SELECT COUNT(1) FROM SoHuuSanPham WHERE MaNguoiDung = @MaNguoiDung AND MaSanPham = @MaSanPham";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaSanPham", maSanPham)
            };

            object? res = DatabaseHelper.ExecuteScalar(query, parameters);
            return res != null && res != DBNull.Value && Convert.ToInt32(res) > 0;
        }
    }
}
