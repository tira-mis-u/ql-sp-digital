-- ============================================================================
-- DỰ ÁN: HỆ THỐNG QUẢN LÝ CỬA HÀNG SẢN PHẨM SỐ (DPMS - DIGITAL PRODUCT MANAGEMENT SYSTEM)
-- MÔN HỌC: BÀI TẬP LỚN LẬP TRÌNH TRỰC QUAN C# (WINFORMS + SQL SERVER)
-- KỊCH BẢN T-SQL TỔNG QUÁT: TẠO DATABASE, BẢNG, RÀNG BUỘC & DỮ LIỆU MẪU
-- QUY CHUẨN THIẾT KẾ: 100% PASCALCASE TIẾNG VIỆT CHUẨN CHUYÊN NGÀNH CNTT
-- Hướng dẫn: Mở SQL Server Management Studio (SSMS), dán toàn bộ script và nhấn F5.
-- ============================================================================

-- 1. KHỞI TẠO CƠ SỞ DỮ LIỆU
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'DPMS_Db')
BEGIN
    CREATE DATABASE DPMS_Db;
END
GO

USE DPMS_Db;
GO

-- 2. XÓA CÁC BẢNG CŨ NẾU ĐÃ TỒN TẠI (THEO THỨ TỰ RÀNG BUỘC KHÓA NGOẠI)
IF OBJECT_ID('DanhGia', 'U') IS NOT NULL DROP TABLE DanhGia;
IF OBJECT_ID('SoHuuSanPham', 'U') IS NOT NULL DROP TABLE SoHuuSanPham;
IF OBJECT_ID('ThanhToan', 'U') IS NOT NULL DROP TABLE ThanhToan;
IF OBJECT_ID('ChiTietDonHang', 'U') IS NOT NULL DROP TABLE ChiTietDonHang;
IF OBJECT_ID('DonHang', 'U') IS NOT NULL DROP TABLE DonHang;
IF OBJECT_ID('MaGiamGiaNguoiDung', 'U') IS NOT NULL DROP TABLE MaGiamGiaNguoiDung;
IF OBJECT_ID('MaGiamGia', 'U') IS NOT NULL DROP TABLE MaGiamGia;
IF OBJECT_ID('GioHang', 'U') IS NOT NULL DROP TABLE GioHang;
IF OBJECT_ID('TepSanPham', 'U') IS NOT NULL DROP TABLE TepSanPham;
IF OBJECT_ID('SanPham', 'U') IS NOT NULL DROP TABLE SanPham;
IF OBJECT_ID('NhaPhatHanh', 'U') IS NOT NULL DROP TABLE NhaPhatHanh;
IF OBJECT_ID('DanhMuc', 'U') IS NOT NULL DROP TABLE DanhMuc;
IF OBJECT_ID('NguoiDung', 'U') IS NOT NULL DROP TABLE NguoiDung;
GO

-- ============================================================================
-- 3. TẠO CÁC BẢNG DỮ LIỆU CHÍNH & RÀNG BUỘC TOÀN VẸN
-- ============================================================================

-- BẢNG 1: NguoiDung (Tài khoản người dùng, phân quyền và số dư ví tài khoản)
CREATE TABLE NguoiDung (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhauHash NVARCHAR(256) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    SoDienThoai NVARCHAR(20) NULL,
    VaiTro NVARCHAR(20) NOT NULL DEFAULT N'KhachHang' CHECK (VaiTro IN (N'QuanTriVien', N'KhachHang')),
    SoDuVi DECIMAL(18,2) NOT NULL DEFAULT 0.00 CHECK (SoDuVi >= 0),
    TrangThai BIT NOT NULL DEFAULT 1, -- 1: Đang hoạt động, 0: Bị khóa
    NgayTao DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- BẢNG 2: DanhMuc (Danh mục phân loại sản phẩm số)
CREATE TABLE DanhMuc (
    MaDanhMuc INT IDENTITY(1,1) PRIMARY KEY,
    TenDanhMuc NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255) NULL,
    TrangThai BIT NOT NULL DEFAULT 1
);
GO

-- BẢNG 3: NhaPhatHanh (Nhà phát hành / Studio sáng tạo nội dung số)
CREATE TABLE NhaPhatHanh (
    MaNhaPhatHanh INT IDENTITY(1,1) PRIMARY KEY,
    TenNhaPhatHanh NVARCHAR(100) NOT NULL UNIQUE,
    QuocGia NVARCHAR(50) NULL,
    Website NVARCHAR(255) NULL,
    MoTa NVARCHAR(500) NULL
);
GO

-- BẢNG 4: SanPham (Thông tin sản phẩm kỹ thuật số)
CREATE TABLE SanPham (
    MaSanPham INT IDENTITY(1,1) PRIMARY KEY,
    TenSanPham NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    GiaBan DECIMAL(18,2) NOT NULL DEFAULT 0.00 CHECK (GiaBan >= 0),
    HinhAnh NVARCHAR(500) NULL, -- Đường dẫn ảnh tương đối: Assets/Images/... hoặc URL
    DungLuongMB DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    PhienBan NVARCHAR(50) NOT NULL DEFAULT '1.0.0',
    MaDanhMuc INT NOT NULL,
    MaNhaPhatHanh INT NULL,
    DangKinhDoanh BIT NOT NULL DEFAULT 1,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL,
    CONSTRAINT FK_SanPham_DanhMuc FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc),
    CONSTRAINT FK_SanPham_NhaPhatHanh FOREIGN KEY (MaNhaPhatHanh) REFERENCES NhaPhatHanh(MaNhaPhatHanh)
);
GO

-- BẢNG 5: TepSanPham (Siêu dữ liệu & đường dẫn tương đối lưu tệp kỹ thuật số vật lý)
CREATE TABLE TepSanPham (
    MaTep INT IDENTITY(1,1) PRIMARY KEY,
    MaSanPham INT NOT NULL,
    TenTep NVARCHAR(255) NOT NULL,
    DuongDanTep NVARCHAR(500) NOT NULL, -- Đường dẫn tương đối: Storage/Products/{MaSanPham}/setup.zip
    DinhDangTep NVARCHAR(50) NOT NULL, -- .zip, .exe, .rar, .iso, .pdf
    KichThuocByte BIGINT NOT NULL DEFAULT 0,
    MaHashSHA256 NVARCHAR(64) NULL, -- Kiểm tra toàn vẹn file chống lỗi khi tải
    PhienBan NVARCHAR(50) NOT NULL DEFAULT '1.0.0',
    NgayTaiLen DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_TepSanPham_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham) ON DELETE CASCADE
);
GO

-- BẢNG 6: GioHang (Chi tiết giỏ hàng tạm thời của khách hàng)
CREATE TABLE GioHang (
    MaGioHang INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    MaSanPham INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 1 CHECK (SoLuong > 0),
    NgayThem DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_GioHang_NguoiDung_SanPham UNIQUE (MaNguoiDung, MaSanPham),
    CONSTRAINT FK_GioHang_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE,
    CONSTRAINT FK_GioHang_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham) ON DELETE CASCADE
);
GO

-- BẢNG 7: MaGiamGia (Kho mã ưu đãi hệ thống hoặc do Studio tài trợ)
CREATE TABLE MaGiamGia (
    MaGiamGiaId INT IDENTITY(1,1) PRIMARY KEY,
    MaCode NVARCHAR(50) NOT NULL UNIQUE,
    TieuDe NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255) NULL,
    LoaiGiamGia NVARCHAR(20) NOT NULL DEFAULT N'PhanTram' CHECK (LoaiGiamGia IN (N'PhanTram', N'TienMat')),
    GiaTriGiam DECIMAL(18,2) NOT NULL CHECK (GiaTriGiam > 0),
    GiamToiDa DECIMAL(18,2) NULL, -- Áp dụng khi giảm theo phần trăm
    DonToiThieu DECIMAL(18,2) NOT NULL DEFAULT 0.00 CHECK (DonToiThieu >= 0),
    SoLuongPhatHanh INT NOT NULL DEFAULT 100,
    SoLuongConLai INT NOT NULL DEFAULT 100,
    MaNhaPhatHanh INT NULL, -- NULL: Voucher toàn hệ thống; NOT NULL: Voucher do Studio tài trợ
    NgayBatDau DATETIME NOT NULL DEFAULT GETDATE(),
    NgayKetThuc DATETIME NOT NULL,
    KichHoat BIT NOT NULL DEFAULT 1,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_MaGiamGia_NhaPhatHanh FOREIGN KEY (MaNhaPhatHanh) REFERENCES NhaPhatHanh(MaNhaPhatHanh)
);
GO

-- BẢNG 8: MaGiamGiaNguoiDung (Kho voucher cá nhân người dùng đã thu thập)
CREATE TABLE MaGiamGiaNguoiDung (
    MaNguoiDungVoucherId INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    MaGiamGiaId INT NOT NULL,
    DaSuDung BIT NOT NULL DEFAULT 0,
    NgaySuDung DATETIME NULL,
    NgayNhan DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_NguoiDung_MaGiamGia UNIQUE (MaNguoiDung, MaGiamGiaId),
    CONSTRAINT FK_MaGiamGiaNguoiDung_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE,
    CONSTRAINT FK_MaGiamGiaNguoiDung_MaGiamGia FOREIGN KEY (MaGiamGiaId) REFERENCES MaGiamGia(MaGiamGiaId) ON DELETE CASCADE
);
GO

-- BẢNG 9: DonHang (Thông tin hóa đơn mua sắm sản phẩm số)
CREATE TABLE DonHang (
    MaDonHang INT IDENTITY(1,1) PRIMARY KEY,
    MaDonHangCode NVARCHAR(50) NOT NULL UNIQUE,
    MaNguoiDung INT NOT NULL,
    MaGiamGiaId INT NULL,
    TamTinh DECIMAL(18,2) NOT NULL CHECK (TamTinh >= 0),
    TienGiamGia DECIMAL(18,2) NOT NULL DEFAULT 0.00 CHECK (TienGiamGia >= 0),
    TongThanhToan DECIMAL(18,2) NOT NULL CHECK (TongThanhToan >= 0),
    TrangThaiDonHang NVARCHAR(50) NOT NULL DEFAULT N'Hoàn thành' CHECK (TrangThaiDonHang IN (N'Chờ xử lý', N'Hoàn thành', N'Đã hủy')),
    GhiChu NVARCHAR(255) NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_DonHang_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    CONSTRAINT FK_DonHang_MaGiamGia FOREIGN KEY (MaGiamGiaId) REFERENCES MaGiamGia(MaGiamGiaId)
);
GO

-- BẢNG 10: ChiTietDonHang (Chi tiết từng món hàng và lưu đơn giá tại thời điểm mua)
CREATE TABLE ChiTietDonHang (
    MaChiTietDonHang INT IDENTITY(1,1) PRIMARY KEY,
    MaDonHang INT NOT NULL,
    MaSanPham INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 1 CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0),
    CONSTRAINT UQ_ChiTietDonHang UNIQUE (MaDonHang, MaSanPham),
    CONSTRAINT FK_ChiTietDonHang_DonHang FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietDonHang_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);
GO

-- BẢNG 11: ThanhToan (Nhật ký giao dịch nạp tiền hoặc trừ tiền ví)
CREATE TABLE ThanhToan (
    MaThanhToan INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    MaDonHang INT NULL,
    SoTien DECIMAL(18,2) NOT NULL CHECK (SoTien > 0),
    LoaiGiaoDich NVARCHAR(50) NOT NULL CHECK (LoaiGiaoDich IN (N'Nạp tiền ví', N'Thanh toán đơn hàng', N'Hoàn tiền')),
    PhuongThuc NVARCHAR(50) NOT NULL DEFAULT N'Ví Steam Simulation',
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Thành công',
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ThanhToan_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    CONSTRAINT FK_ThanhToan_DonHang FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang)
);
GO

-- BẢNG 12: SoHuuSanPham (Quyền sở hữu vĩnh viễn sản phẩm số trong Thư viện cá nhân)
CREATE TABLE SoHuuSanPham (
    MaSoHuu INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    MaSanPham INT NOT NULL,
    MaDonHang INT NOT NULL,
    NgaySoHuu DATETIME NOT NULL DEFAULT GETDATE(),
    SoLanTai INT NOT NULL DEFAULT 0,
    LanTaiCuoi DATETIME NULL,
    CONSTRAINT UQ_SoHuuSanPham UNIQUE (MaNguoiDung, MaSanPham),
    CONSTRAINT FK_SoHuuSanPham_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    CONSTRAINT FK_SoHuuSanPham_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham),
    CONSTRAINT FK_SoHuuSanPham_DonHang FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang)
);
GO

-- BẢNG 13: DanhGia (Đánh giá sao 1-5 và nhận xét phản hồi sản phẩm)
CREATE TABLE DanhGia (
    MaDanhGia INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    MaSanPham INT NOT NULL,
    SoSao INT NOT NULL CHECK (SoSao BETWEEN 1 AND 5),
    BinhLuan NVARCHAR(MAX) NOT NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL,
    CONSTRAINT UQ_DanhGia UNIQUE (MaNguoiDung, MaSanPham),
    CONSTRAINT FK_DanhGia_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE,
    CONSTRAINT FK_DanhGia_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham) ON DELETE CASCADE
);
GO

-- ============================================================================
-- 4. TẠO CÁC CHỈ MỤC (INDEXES) TỐI ƯU HIỆU NĂNG TÌM KIẾM
-- ============================================================================
CREATE NONCLUSTERED INDEX IX_SanPham_DanhMuc ON SanPham(MaDanhMuc);
CREATE NONCLUSTERED INDEX IX_SanPham_NhaPhatHanh ON SanPham(MaNhaPhatHanh);
CREATE NONCLUSTERED INDEX IX_SanPham_GiaBan ON SanPham(GiaBan);
CREATE NONCLUSTERED INDEX IX_DonHang_NguoiDung ON DonHang(MaNguoiDung);
CREATE NONCLUSTERED INDEX IX_DanhGia_SanPham ON DanhGia(MaSanPham);
GO

-- ============================================================================
-- 5. NẠP DỮ LIỆU MẪU BAN ĐẦU (SEED DATA - TIẾNG VIỆT ĐẦY ĐỦ)
-- ============================================================================

-- 5.1. Dữ liệu Người dùng (NguoiDung)
-- Mật khẩu mặc định: '123456' (Hash SHA-256: 8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92)
INSERT INTO NguoiDung (TenDangNhap, MatKhauHash, HoTen, Email, SoDienThoai, VaiTro, SoDuVi, TrangThai)
VALUES
(N'admin', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Quản Trị Viên Hệ Thống', N'admin@dpms.com', N'0901234567', N'QuanTriVien', 5000000.00, 1),
(N'hieu_pham', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Phạm Trung Hiếu', N'hieu@gmail.com', N'0912345678', N'KhachHang', 1500000.00, 1),
(N'thuan_tran', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Trần Quốc Thuận', N'thuan@gmail.com', N'0923456789', N'KhachHang', 850000.00, 1),
(N'tri_le', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Lê Nguyễn Minh Trí', N'tri@gmail.com', N'0934567890', N'KhachHang', 200000.00, 1),
(N'dam_pham', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Phạm Văn Đam', N'dam@gmail.com', N'0945678901', N'KhachHang', 600000.00, 1),
(N'trung_nguyen', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Nguyễn Quốc Trung', N'trung@gmail.com', N'0956789012', N'KhachHang', 1200000.00, 1);
GO

-- 5.2. Danh mục sản phẩm (DanhMuc)
INSERT INTO DanhMuc (TenDanhMuc, MoTa)
VALUES
(N'Trò chơi điện tử', N'Các tựa game nhập vai, hành động, chiến thuật số'),
(N'Phần mềm đồ họa & tiện ích', N'Công cụ chỉnh sửa ảnh, video, dựng hình và lập trình'),
(N'Mô hình 3D & Assets', N'Tài nguyên mô hình 3D, rigging và hiệu ứng cho game'),
(N'Âm nhạc & Hiệu ứng âm thanh', N'Các gói âm thanh nền, nhạc phim, hiệu ứng âm thanh game'),
(N'Đồ họa số & Minh họa', N'Banner, poster số, vector và icon thiết kế'),
(N'Tài liệu & Sách điện tử', N'Ebook, hướng dẫn chuyên ngành CNTT và thiết kế');
GO

-- 5.3. Nhà phát hành (NhaPhatHanh)
INSERT INTO NhaPhatHanh (TenNhaPhatHanh, QuocGia, Website, MoTa)
VALUES
(N'Pixel Game Studio', N'Việt Nam', N'https://pixelstudio.vn', N'Studio phát triển game độc lập hàng đầu'),
(N'CyberSoft Technologies', N'Mỹ', N'https://cybersoft.io', N'Công ty phát triển phần mềm chuyên nghiệp'),
(N'PolyCraft 3D', N'Nhật Bản', N'https://polycraft3d.jp', N'Đơn vị thiết kế mô hình 3D phục vụ game AAA'),
(N'AudioWave Productions', N'Đức', N'https://audiowave.de', N'Studio sản xuất âm thanh và nhạc phim'),
(N'CodeMaster Plugins', N'Hàn Quốc', N'https://codemaster.dev', N'Nhà cung cấp tiện ích lập trình viên');
GO

-- 5.4. Sản phẩm kỹ thuật số (SanPham)
INSERT INTO SanPham (TenSanPham, MoTa, GiaBan, HinhAnh, DungLuongMB, PhienBan, MaDanhMuc, MaNhaPhatHanh, DangKinhDoanh)
VALUES
(N'Cyber Adventure RPG', N'Tựa game nhập vai thế giới mở phong cách Cyberpunk với đồ họa tương lai đỉnh cao.', 450000.00, N'Assets/Images/cyber_rpg.jpg', 14848.00, N'1.2.0', 1, 1, 1),
(N'Photo Studio Pro 2026', N'Phần mềm biên tập hình ảnh chuyên nghiệp tích hợp trí tuệ nhân tạo thế hệ mới.', 1200000.00, N'Assets/Images/photo_pro.jpg', 2560.00, N'3.0.1', 2, 2, 1),
(N'Low Poly Sci-Fi Pack 3D', N'Gói hơn 200 mô hình 3D phong cách viễn tưởng low-poly tối ưu cho Unity & Unreal Engine.', 150000.00, N'Assets/Images/scifi_3d.jpg', 850.00, N'1.0.0', 3, 3, 1),
(N'Epic Ambient Sounds FX', N'Bộ sưu tập 500+ hiệu ứng âm thanh môi trường chuẩn phòng thu chất lượng cao 96kHz/24bit.', 80000.00, N'Assets/Images/ambient_fx.jpg', 1200.00, N'2.0.0', 4, 4, 1),
(N'Code Master AI Extension', N'Plugin hỗ trợ tự động gợi ý code thông minh và tối ưu hóa truy vấn SQL Server cho Visual Studio.', 300000.00, N'Assets/Images/codemaster.jpg', 150.00, N'1.5.4', 2, 5, 1),
(N'Fantasy Hero Rigged 3D Model', N'Nhân vật chiến binh hiệp sĩ giả tưởng đầy đủ khung xương hoạt họa (Rigging & Animations).', 520000.00, N'Assets/Images/hero_rig.jpg', 420.00, N'1.1.0', 3, 3, 1),
(N'Pixel Battle Arena 2D', N'Tựa game hành động phong cách pixel art đối kháng trực tuyến hấp dẫn nhiều người chơi.', 190000.00, N'Assets/Images/pixel_arena.jpg', 450.00, N'1.0.5', 1, 1, 1),
(N'Game UI Vector Kit 2026', N'Bộ tài nguyên giao diện game phẳng cao cấp gồm hơn 1000 icons, buttons và HUD bars.', 95000.00, N'Assets/Images/game_ui_kit.jpg', 320.00, N'2.1.0', 5, 1, 1);
GO

-- 5.5. Tệp sản phẩm kỹ thuật số (TepSanPham - Metadata & đường dẫn tương đối)
INSERT INTO TepSanPham (MaSanPham, TenTep, DuongDanTep, DinhDangTep, KichThuocByte, MaHashSHA256, PhienBan)
VALUES
(1, N'Setup_CyberAdventure_v1.2.0.exe', N'Storage/Products/1/Setup_CyberAdventure_v1.2.0.exe', N'.exe', 15569256448, N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', N'1.2.0'),
(2, N'PhotoStudioPro_Installer_x64.zip', N'Storage/Products/2/PhotoStudioPro_Installer_x64.zip', N'.zip', 2684354560, N'a591a6d40bf420404a011733cfb7b190d62c65bf0bcda32b57b277d9ad9f146e', N'3.0.1'),
(3, N'LowPoly_SciFi_Assets_FBX.zip', N'Storage/Products/3/LowPoly_SciFi_Assets_FBX.zip', N'.zip', 891289600, N'5d41402abc4b2a76b9719d911017c592b23a9d363b7e416a7f80db40243454b5', N'1.0.0'),
(4, N'Epic_Ambient_Audio_WAV.rar', N'Storage/Products/4/Epic_Ambient_Audio_WAV.rar', N'.rar', 1258291200, N'098f6bcd4621d373cade4e832627b4f62bf9f0951111626c927f879685a2ce2b', N'2.0.0'),
(5, N'CodeMaster_Extension_vsix.zip', N'Storage/Products/5/CodeMaster_Extension_vsix.zip', N'.zip', 157286400, N'1f3870be274f6c49b3e31a0c6728957f4955b7ca4b2ffb2888cf3e27a6f3b0ba', N'1.5.4'),
(6, N'FantasyHero_Rigged_Blender_FBX.zip', N'Storage/Products/6/FantasyHero_Rigged_Blender_FBX.zip', N'.zip', 440401920, N'7d793037a0760186574b0282f2f435e7090b8f1073860ee6e3a652ff5432095f', N'1.1.0'),
(7, N'PixelBattleArena_Setup.exe', N'Storage/Products/7/PixelBattleArena_Setup.exe', N'.exe', 471859200, N'865c0c0b4ab0e063e5caa3387c1a874136e0d37e38ec2d480e6c278fb1a6a6f4', N'1.0.5'),
(8, N'GameUI_Vector_Kit_SVG_PNG.zip', N'Storage/Products/8/GameUI_Vector_Kit_SVG_PNG.zip', N'.zip', 335544320, N'f073286f059e99c15848aa61730bb394d21e8609ee3631f79f046b0a17409fcf', N'2.1.0');
GO

-- 5.6. Mã giảm giá (MaGiamGia)
-- Hỗ trợ cả Voucher Hệ thống (MaNhaPhatHanh IS NULL) và Voucher do Studio tài trợ (MaNhaPhatHanh = 1)
INSERT INTO MaGiamGia (MaCode, TieuDe, MoTa, LoaiGiamGia, GiaTriGiam, GiamToiDa, DonToiThieu, SoLuongPhatHanh, SoLuongConLai, MaNhaPhatHanh, NgayBatDau, NgayKetThuc, KichHoat)
VALUES
(N'WELCOME2026', N'Ưu đãi thành viên mới 20%', N'Giảm ngay 20% cho đơn hàng đầu tiên từ 100.000đ', N'PhanTram', 20.00, 100000.00, 100000.00, 500, 482, NULL, GETDATE(), DATEADD(month, 6, GETDATE()), 1),
(N'SUMMERSALE', N'Siêu hội mùa hè giảm 50.000đ', N'Giảm trực tiếp 50.000 VNĐ cho giỏ hàng từ 300.000đ', N'TienMat', 50000.00, NULL, 300000.00, 200, 165, NULL, GETDATE(), DATEADD(month, 3, GETDATE()), 1),
(N'PIXELSTUDIO10', N'Tri ân người hâm mộ Pixel Studio', N'Giảm 10% các sản phẩm do Pixel Game Studio phát hành', N'PhanTram', 10.00, 50000.00, 150000.00, 100, 94, 1, GETDATE(), DATEADD(month, 4, GETDATE()), 1),
(N'VIPMEGA100K', N'Voucher Khách hàng thân thiết', N'Giảm ngay 100.000 VNĐ cho đơn hàng từ 500.000đ', N'TienMat', 100000.00, NULL, 500000.00, 50, 42, NULL, GETDATE(), DATEADD(month, 2, GETDATE()), 1);
GO

-- 5.7. Mã giảm giá người dùng đã thu thập (MaGiamGiaNguoiDung)
INSERT INTO MaGiamGiaNguoiDung (MaNguoiDung, MaGiamGiaId, DaSuDung, NgaySuDung)
VALUES
(2, 1, 0, NULL),
(2, 2, 0, NULL),
(3, 1, 1, DATEADD(day, -2, GETDATE())),
(3, 3, 0, NULL),
(4, 2, 0, NULL);
GO

-- 5.8. Giỏ hàng mẫu (GioHang)
INSERT INTO GioHang (MaNguoiDung, MaSanPham, SoLuong)
VALUES
(2, 3, 1),
(2, 4, 1),
(3, 5, 1);
GO

-- 5.9. Đơn hàng mẫu (DonHang) & Chi tiết đơn hàng (ChiTietDonHang)
INSERT INTO DonHang (MaDonHangCode, MaNguoiDung, MaGiamGiaId, TamTinh, TienGiamGia, TongThanhToan, TrangThaiDonHang, GhiChu)
VALUES
(N'ORD-20261014-001', 3, 1, 450000.00, 90000.00, 360000.00, N'Hoàn thành', N'Mua Cyber Adventure RPG bằng ví Steam'),
(N'ORD-20261015-002', 6, NULL, 300000.00, 0.00, 300000.00, N'Hoàn thành', N'Mua Code Master AI Extension');
GO

INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuong, DonGia)
VALUES
(1, 1, 1, 450000.00),
(2, 5, 1, 300000.00);
GO

-- 5.10. Nhật ký thanh toán ví (ThanhToan)
INSERT INTO ThanhToan (MaNguoiDung, MaDonHang, SoTien, LoaiGiaoDich, PhuongThuc, TrangThai)
VALUES
(3, NULL, 500000.00, N'Nạp tiền ví', N'Ví Steam Simulation', N'Thành công'),
(3, 1, 360000.00, N'Thanh toán đơn hàng', N'Ví Steam Simulation', N'Thành công'),
(6, NULL, 500000.00, N'Nạp tiền ví', N'Ví Steam Simulation', N'Thành công'),
(6, 2, 300000.00, N'Thanh toán đơn hàng', N'Ví Steam Simulation', N'Thành công');
GO

-- 5.11. Sở hữu bản quyền trong Thư viện (SoHuuSanPham)
INSERT INTO SoHuuSanPham (MaNguoiDung, MaSanPham, MaDonHang, NgaySoHuu, SoLanTai, LanTaiCuoi)
VALUES
(3, 1, 1, DATEADD(day, -1, GETDATE()), 2, GETDATE()),
(6, 5, 2, GETDATE(), 1, GETDATE());
GO

-- 5.12. Đánh giá & Nhận xét sản phẩm (DanhGia)
INSERT INTO DanhGia (MaNguoiDung, MaSanPham, SoSao, BinhLuan, NgayTao)
VALUES
(3, 1, 5, N'Game Cyber Adventure RPG cốt truyện xuất sắc, đồ họa cực đỉnh và tối ưu mượt mà!', DATEADD(day, -1, GETDATE())),
(6, 5, 4, N'Code Master plugin hỗ trợ sinh code SQL rất nhanh, đáng giá từng đồng!', GETDATE());
GO

-- ============================================================================
-- 6. KIỂM TRA TỔNG QUAN DỮ LIỆU SAU KHI KHỞI TẠO
-- ============================================================================
PRINT N'==> KHỞI TẠO CƠ SỞ DỮ LIỆU DPMS_Db VÀ 13 BẢNG PASCALCASE TIẾNG VIỆT THÀNH CÔNG!';
SELECT N'NguoiDung' AS TenBang, COUNT(1) AS TongBanGhi FROM NguoiDung
UNION ALL
SELECT N'DanhMuc', COUNT(1) FROM DanhMuc
UNION ALL
SELECT N'NhaPhatHanh', COUNT(1) FROM NhaPhatHanh
UNION ALL
SELECT N'SanPham', COUNT(1) FROM SanPham
UNION ALL
SELECT N'TepSanPham', COUNT(1) FROM TepSanPham
UNION ALL
SELECT N'GioHang', COUNT(1) FROM GioHang
UNION ALL
SELECT N'MaGiamGia', COUNT(1) FROM MaGiamGia
UNION ALL
SELECT N'MaGiamGiaNguoiDung', COUNT(1) FROM MaGiamGiaNguoiDung
UNION ALL
SELECT N'DonHang', COUNT(1) FROM DonHang
UNION ALL
SELECT N'ChiTietDonHang', COUNT(1) FROM ChiTietDonHang
UNION ALL
SELECT N'ThanhToan', COUNT(1) FROM ThanhToan
UNION ALL
SELECT N'SoHuuSanPham', COUNT(1) FROM SoHuuSanPham
UNION ALL
SELECT N'DanhGia', COUNT(1) FROM DanhGia;
GO
