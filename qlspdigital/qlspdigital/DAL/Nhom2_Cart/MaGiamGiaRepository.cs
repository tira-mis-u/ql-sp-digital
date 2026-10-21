using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.DAL.Nhom2_Cart
{
    // Repository quản lý mã giảm giá, voucher hệ thống và kho voucher cá nhân
    // Thành viên phụ trách: Nguyễn Quốc Trung (Nhóm 2)
    public class MaGiamGiaRepository
    {
        // Lấy toàn bộ danh sách voucher mở phát hành kèm trạng thái đối với người dùng
        public List<MaGiamGiaDTO> LayDanhSachVoucher(int maNguoiDung)
        {
            List<MaGiamGiaDTO> list = new List<MaGiamGiaDTO>();

            string query = @"
                SELECT mgg.MaGiamGiaId, mgg.MaCode, mgg.TieuDe, mgg.MoTa, mgg.LoaiGiamGia,
                       mgg.GiaTriGiam, mgg.GiamToiDa, mgg.DonToiThieu, mgg.SoLuongPhatHanh,
                       mgg.SoLuongConLai, mgg.MaNhaPhatHanh, mgg.NgayBatDau, mgg.NgayKetThuc, mgg.KichHoat,
                       CASE WHEN mnd.MaNguoiDungVoucherId IS NOT NULL THEN 1 ELSE 0 END AS DaThuThap,
                       ISNULL(mnd.DaSuDung, 0) AS DaSuDung
                FROM MaGiamGia mgg
                LEFT JOIN MaGiamGiaNguoiDung mnd ON mgg.MaGiamGiaId = mnd.MaGiamGiaId AND mnd.MaNguoiDung = @MaNguoiDung
                WHERE mgg.KichHoat = 1 AND mgg.NgayKetThuc >= GETDATE()
                ORDER BY mgg.NgayKetThuc ASC";

            SqlParameter[] parameters = { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MaGiamGiaDTO
                {
                    MaGiamGiaId = Convert.ToInt32(row["MaGiamGiaId"]),
                    MaCode = row["MaCode"].ToString() ?? string.Empty,
                    TieuDe = row["TieuDe"].ToString() ?? string.Empty,
                    MoTa = row["MoTa"] != DBNull.Value ? row["MoTa"].ToString() : null,
                    LoaiGiamGia = row["LoaiGiamGia"].ToString() ?? "PhanTram",
                    GiaTriGiam = Convert.ToDecimal(row["GiaTriGiam"]),
                    GiamToiDa = row["GiamToiDa"] != DBNull.Value ? Convert.ToDecimal(row["GiamToiDa"]) : null,
                    DonToiThieu = Convert.ToDecimal(row["DonToiThieu"]),
                    SoLuongPhatHanh = Convert.ToInt32(row["SoLuongPhatHanh"]),
                    SoLuongConLai = Convert.ToInt32(row["SoLuongConLai"]),
                    MaNhaPhatHanh = row["MaNhaPhatHanh"] != DBNull.Value ? Convert.ToInt32(row["MaNhaPhatHanh"]) : null,
                    NgayBatDau = Convert.ToDateTime(row["NgayBatDau"]),
                    NgayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]),
                    KichHoat = Convert.ToBoolean(row["KichHoat"]),
                    DaThuThap = Convert.ToInt32(row["DaThuThap"]) == 1,
                    DaSuDung = Convert.ToBoolean(row["DaSuDung"])
                });
            }

            return list;
        }

        // Thu thập voucher vào kho cá nhân bằng mã code (Create)
        public bool ThuThapVoucher(int maNguoiDung, string maCode, out string thongBao)
        {
            if (string.IsNullOrEmpty(maCode) || maCode.Trim() == "")
            {
                thongBao = "Vui lòng nhập mã voucher!";
                return false;
            }

            string queryFind = @"
                SELECT MaGiamGiaId, SoLuongConLai, NgayKetThuc, KichHoat
                FROM MaGiamGia
                WHERE MaCode = @MaCode";

            SqlParameter[] pFind = { new SqlParameter("@MaCode", maCode.Trim().ToUpper()) };
            DataTable dt = DatabaseHelper.ExecuteQuery(queryFind, pFind);

            if (dt.Rows.Count == 0)
            {
                thongBao = "Mã voucher không tồn tại trong hệ thống!";
                return false;
            }

            DataRow row = dt.Rows[0];
            int maGiamGiaId = Convert.ToInt32(row["MaGiamGiaId"]);
            int soLuongConLai = Convert.ToInt32(row["SoLuongConLai"]);
            DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);
            bool kichHoat = Convert.ToBoolean(row["KichHoat"]);

            if (!kichHoat || ngayKetThuc < DateTime.Now)
            {
                thongBao = "Mã voucher này đã hết hạn hoặc tạm ngưng áp dụng!";
                return false;
            }

            if (soLuongConLai <= 0)
            {
                thongBao = "Mã voucher này đã hết lượt thu thập!";
                return false;
            }

            // Kiểm tra người dùng đã thu thập chưa
            string queryCheck = "SELECT 1 FROM MaGiamGiaNguoiDung WHERE MaNguoiDung = @MaNguoiDung AND MaGiamGiaId = @MaGiamGiaId";
            SqlParameter[] pCheck =
            {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaGiamGiaId", maGiamGiaId)
            };

            object? chkExist = DatabaseHelper.ExecuteScalar(queryCheck, pCheck);
            if (chkExist != null && chkExist != DBNull.Value)
            {
                thongBao = "Bạn đã lưu voucher này trong kho rồi!";
                return false;
            }

            // Thêm vào kho và giảm 1 lượt
            string queryInsert = @"
                INSERT INTO MaGiamGiaNguoiDung (MaNguoiDung, MaGiamGiaId, DaSuDung, NgayNhan)
                VALUES (@MaNguoiDung, @MaGiamGiaId, 0, GETDATE());
                UPDATE MaGiamGia SET SoLuongConLai = SoLuongConLai - 1 WHERE MaGiamGiaId = @MaGiamGiaId;";

            SqlParameter[] pInsert =
            {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaGiamGiaId", maGiamGiaId)
            };

            int affected = DatabaseHelper.ExecuteNonQuery(queryInsert, pInsert);
            if (affected > 0)
            {
                thongBao = "Thu thập mã voucher thành công!";
                return true;
            }

            thongBao = "Không thể lưu voucher, vui lòng thử lại.";
            return false;
        }

        // Kiểm tra và tính số tiền giảm giá khi áp dụng mã voucher cho giỏ hàng
        public bool KiemTraVaApDung(string maCode, int maNguoiDung, decimal tamTinh, out MaGiamGiaDTO? voucher, out decimal tienGiam, out string thongBao)
        {
            voucher = null;
            tienGiam = 0;

            if (string.IsNullOrEmpty(maCode) || maCode.Trim() == "")
            {
                thongBao = "Vui lòng nhập mã giảm giá!";
                return false;
            }

            string query = @"
                SELECT mgg.*, 
                       CASE WHEN mnd.MaNguoiDungVoucherId IS NOT NULL THEN 1 ELSE 0 END AS DaThuThap,
                       ISNULL(mnd.DaSuDung, 0) AS DaSuDung
                FROM MaGiamGia mgg
                LEFT JOIN MaGiamGiaNguoiDung mnd ON mgg.MaGiamGiaId = mnd.MaGiamGiaId AND mnd.MaNguoiDung = @MaNguoiDung
                WHERE mgg.MaCode = @MaCode AND mgg.KichHoat = 1";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaCode", maCode.Trim().ToUpper()),
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt.Rows.Count == 0)
            {
                thongBao = "Mã voucher không hợp lệ!";
                return false;
            }

            DataRow row = dt.Rows[0];
            bool daSuDung = Convert.ToBoolean(row["DaSuDung"]);
            if (daSuDung)
            {
                thongBao = "Mã ưu đãi này bạn đã sử dụng rồi, không thể áp dụng lại!";
                return false;
            }

            bool daThuThap = Convert.ToInt32(row["DaThuThap"]) == 1;
            if (maNguoiDung > 0 && !daThuThap)
            {
                thongBao = $"Mã ưu đãi '{maCode.Trim().ToUpper()}' chưa được lưu vào Ví của bạn!\nVui lòng vào mục 'Kho mã ưu đãi' bấm 'Thu thập vào ví' trước khi áp dụng.";
                return false;
            }

            int soLuongConLai = Convert.ToInt32(row["SoLuongConLai"]);
            if (soLuongConLai <= 0)
            {
                thongBao = "Mã ưu đãi này đã hết lượt sử dụng trên hệ thống!";
                return false;
            }

            DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);
            if (ngayKetThuc < DateTime.Now)
            {
                thongBao = "Mã giảm giá đã quá hạn sử dụng!";
                return false;
            }

            decimal donToiThieu = Convert.ToDecimal(row["DonToiThieu"]);
            if (tamTinh < donToiThieu)
            {
                thongBao = $"Đơn hàng chưa đạt mức tối thiểu {FormatHelper.FormatCurrency(donToiThieu)} để áp dụng mã!";
                return false;
            }

            voucher = new MaGiamGiaDTO
            {
                MaGiamGiaId = Convert.ToInt32(row["MaGiamGiaId"]),
                MaCode = row["MaCode"].ToString() ?? string.Empty,
                TieuDe = row["TieuDe"].ToString() ?? string.Empty,
                LoaiGiamGia = row["LoaiGiamGia"].ToString() ?? "PhanTram",
                GiaTriGiam = Convert.ToDecimal(row["GiaTriGiam"]),
                GiamToiDa = row["GiamToiDa"] != DBNull.Value ? Convert.ToDecimal(row["GiamToiDa"]) : null,
                DonToiThieu = donToiThieu
            };

            // Tính số tiền giảm
            if (voucher.LoaiGiamGia == "PhanTram")
            {
                decimal rawGiam = (tamTinh * voucher.GiaTriGiam) / 100m;
                if (voucher.GiamToiDa.HasValue && rawGiam > voucher.GiamToiDa.Value)
                {
                    tienGiam = voucher.GiamToiDa.Value;
                }
                else
                {
                    tienGiam = rawGiam;
                }
            }
            else
            {
                tienGiam = Math.Min(voucher.GiaTriGiam, tamTinh);
            }

            thongBao = $"Áp dụng mã thành công! Bạn được giảm {FormatHelper.FormatCurrency(tienGiam)}.";
            return true;
        }

        // Xóa voucher khỏi kho cá nhân (Delete)
        public bool XoaVoucherCaNhan(int maGiamGiaId, int maNguoiDung)
        {
            string query = "DELETE FROM MaGiamGiaNguoiDung WHERE MaGiamGiaId = @MaGiamGiaId AND MaNguoiDung = @MaNguoiDung";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaGiamGiaId", maGiamGiaId),
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
