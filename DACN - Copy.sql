-- ==========================================
-- 1. Tạo cơ sở dữ liệu
-- ==========================================
CREATE DATABASE QLKhoHang;
GO
USE QLKhoHang;
GO

-- ==========================================
-- 2. Stored Procedure / Function tạo mã tự động
-- ==========================================
CREATE OR ALTER PROCEDURE dbo.Sp_TaoMaTuDong
(
    @prefix NVARCHAR(10),
    @tableName NVARCHAR(50),
    @columnName NVARCHAR(50),
    @newCode NVARCHAR(20) OUTPUT
)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX), @maxCode NVARCHAR(20);
    DECLARE @num INT;

    SET @sql = N'SELECT @maxCodeOUT = MAX(' + QUOTENAME(@columnName) + ') FROM ' + QUOTENAME(@tableName);
    EXEC sp_executesql @sql, N'@maxCodeOUT NVARCHAR(20) OUTPUT', @maxCodeOUT=@maxCode OUTPUT;

    IF @maxCode IS NULL
        SET @num = 1;
    ELSE
        SET @num = CAST(RIGHT(@maxCode, LEN(@maxCode) - LEN(@prefix)) AS INT) + 1;

    SET @newCode = @prefix + RIGHT('000' + CAST(@num AS NVARCHAR(10)), 3);
END;
GO

CREATE FUNCTION dbo.fn_TaoMaTuDong
(
    @Prefix NVARCHAR(10),
    @TableName NVARCHAR(50),
    @ColumnName NVARCHAR(50)
)
RETURNS NVARCHAR(10)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX), @MaxCode NVARCHAR(10), @Num INT, @NewCode NVARCHAR(10);

    SET @SQL = N'SELECT @MaxCodeOUT = MAX(' + QUOTENAME(@ColumnName) + ') FROM ' + QUOTENAME(@TableName);
    EXEC sp_executesql @SQL, N'@MaxCodeOUT NVARCHAR(10) OUTPUT', @MaxCodeOUT=@MaxCode OUTPUT;

    IF @MaxCode IS NULL
        SET @Num = 1;
    ELSE
        SET @Num = TRY_CAST(SUBSTRING(@MaxCode, LEN(@Prefix) + 1, 3) AS INT) + 1;

    SET @NewCode = @Prefix + RIGHT('000' + CAST(@Num AS NVARCHAR(3)), 3);
    RETURN @NewCode;
END;
GO

-- ==========================================
-- 3. Tạo các bảng dữ liệu chính
-- ==========================================
CREATE TABLE Kho (
    MaKho CHAR(10) PRIMARY KEY,
    TenKho NVARCHAR(100) NOT NULL,
    DiaChiKho NVARCHAR(255)
);

CREATE TABLE LoaiHang (
    MaLoai CHAR(10) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL
);

CREATE TABLE HangHoa (
    MaHang CHAR(10) PRIMARY KEY,
    TenHang NVARCHAR(100) NOT NULL,
    DonViTinh NVARCHAR(20),
    SoLuongTon INT DEFAULT 0,
    GiaNhap DECIMAL(18,2),
    GiaXuat DECIMAL(18,2),
    MaLoai CHAR(10) FOREIGN KEY REFERENCES LoaiHang(MaLoai),
    MaKho CHAR(10) FOREIGN KEY REFERENCES Kho(MaKho)
);

CREATE TABLE NhanVien (
    MaNV CHAR(10) PRIMARY KEY,
    TenNV NVARCHAR(100),
    SDT NVARCHAR(20),
    DiaChi NVARCHAR(255)
);

CREATE TABLE KhachHang (
    MaKH CHAR(10) PRIMARY KEY,
    TenKH NVARCHAR(100),
    SDT NVARCHAR(20),
    DiaChi NVARCHAR(255)
);

CREATE TABLE NhaCungCap (
    MaNCC CHAR(10) PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    SDT NVARCHAR(20),
    DiaChi NVARCHAR(255)
);

CREATE TABLE PhieuNhap (
    MaPN CHAR(10) PRIMARY KEY,
    NgayNhap DATE DEFAULT GETDATE(),
    MaNV CHAR(10) FOREIGN KEY REFERENCES NhanVien(MaNV),
    MaNCC CHAR(10) FOREIGN KEY REFERENCES NhaCungCap(MaNCC)
);

CREATE TABLE CT_PhieuNhap (
    MaPN CHAR(10) FOREIGN KEY REFERENCES PhieuNhap(MaPN),
    MaHang CHAR(10) FOREIGN KEY REFERENCES HangHoa(MaHang),
    SoLuong INT,
    DonGiaNhap DECIMAL(18,2),
    PRIMARY KEY (MaPN, MaHang)
);

CREATE TABLE PhieuXuat (
    MaPX CHAR(10) PRIMARY KEY,
    NgayXuat DATE DEFAULT GETDATE(),
    MaNV CHAR(10) FOREIGN KEY REFERENCES NhanVien(MaNV),
    MaKH CHAR(10) FOREIGN KEY REFERENCES KhachHang(MaKH)
);

CREATE TABLE CT_PhieuXuat (
    MaPX CHAR(10) FOREIGN KEY REFERENCES PhieuXuat(MaPX),
    MaHang CHAR(10) FOREIGN KEY REFERENCES HangHoa(MaHang),
    SoLuong INT,
    DonGiaXuat DECIMAL(18,2),
    PRIMARY KEY (MaPX, MaHang)
);

-- ==========================================
-- 4. Tạo các bảng báo cáo
-- ==========================================
CREATE TABLE BaoCaoNhapHang (
    MaBCN CHAR(10) PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaHH CHAR(10) NOT NULL,
    TongSoLuongNhap INT NOT NULL,
    TongGiaTriNhap DECIMAL(18,2) NOT NULL,
    GhiChu NVARCHAR(200) NULL,
    FOREIGN KEY (MaHH) REFERENCES HangHoa(MaHang)
);

CREATE TABLE BaoCaoXuatHang (
    MaBCX CHAR(10) PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaHH CHAR(10) NOT NULL,
    TongSoLuongXuat INT NOT NULL,
    TongGiaTriXuat DECIMAL(18,2) NOT NULL,
    GhiChu NVARCHAR(200) NULL,
    FOREIGN KEY (MaHH) REFERENCES HangHoa(MaHang)
);

CREATE TABLE BaoCaoTonKho (
    MaBCT CHAR(10) PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaHH CHAR(10) NOT NULL,
    SoLuongTon INT NOT NULL,
    GiaTriTon DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (MaHH) REFERENCES HangHoa(MaHang)
);

CREATE TABLE KyBaoCao (
    MaKy CHAR(10) PRIMARY KEY,
    TuNgay DATE NOT NULL,
    DenNgay DATE NOT NULL,
    MoTa NVARCHAR(100)
);

-- ==========================================
-- 5. Trigger tự động sinh mã khi insert
-- ==========================================
-- Kho, LoaiHang, HangHoa, NhanVien, KhachHang, NhaCungCap, PhieuNhap, PhieuXuat
-- Tất cả đều dùng kiểu INSTEAD OF INSERT
-- (Bạn đã viết đầy đủ, có thể dùng lại mà không cần chỉnh sửa)

-- ==========================================
-- 6. Trigger cập nhật tồn kho
-- ==========================================
CREATE TRIGGER trg_CapNhatTonKhiNhap
ON CT_PhieuNhap
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE HH
    SET HH.SoLuongTon = HH.SoLuongTon + PN.SoLuong
    FROM HangHoa HH
    INNER JOIN inserted PN ON HH.MaHang = PN.MaHang;
END;
GO

CREATE TRIGGER trg_CapNhatTonKhiXuat
ON CT_PhieuXuat
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM HangHoa HH
        JOIN inserted PX ON HH.MaHang = PX.MaHang
        WHERE HH.SoLuongTon < PX.SoLuong
    )
    BEGIN
        RAISERROR('Số lượng xuất vượt quá tồn kho!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    UPDATE HH
    SET HH.SoLuongTon = HH.SoLuongTon - PX.SoLuong
    FROM HangHoa HH
    INNER JOIN inserted PX ON HH.MaHang = PX.MaHang;
END;
GO

-- ==========================================
-- 7. Trigger cập nhật báo cáo
-- ==========================================
CREATE TRIGGER trg_BaoCaoNhapHang
ON CT_PhieuNhap
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    MERGE BaoCaoNhapHang AS target
    USING (
        SELECT i.MaHang, CAST(GETDATE() AS DATE) AS NgayLap,
               SUM(i.SoLuong) AS TongSoLuongNhap,
               SUM(i.SoLuong * i.DonGiaNhap) AS TongGiaTriNhap
        FROM inserted i
        GROUP BY i.MaHang
    ) AS src
    ON target.MaHH = src.MaHang AND target.NgayLap = src.NgayLap
    WHEN MATCHED THEN
        UPDATE SET target.TongSoLuongNhap = target.TongSoLuongNhap + src.TongSoLuongNhap,
                   target.TongGiaTriNhap = target.TongGiaTriNhap + src.TongGiaTriNhap
    WHEN NOT MATCHED THEN
        INSERT (MaBCN, NgayLap, MaHH, TongSoLuongNhap, TongGiaTriNhap)
        VALUES (dbo.fn_TaoMaTuDong('BCN', 'BaoCaoNhapHang', 'MaBCN'),
                src.NgayLap, src.MaHang, src.TongSoLuongNhap, src.TongGiaTriNhap);
END;
GO

CREATE TRIGGER trg_BaoCaoXuatHang
ON CT_PhieuXuat
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    MERGE BaoCaoXuatHang AS target
    USING (
        SELECT i.MaHang, CAST(GETDATE() AS DATE) AS NgayLap,
               SUM(i.SoLuong) AS TongSoLuongXuat,
               SUM(i.SoLuong * i.DonGiaXuat) AS TongGiaTriXuat
        FROM inserted i
        GROUP BY i.MaHang
    ) AS src
    ON target.MaHH = src.MaHang AND target.NgayLap = src.NgayLap
    WHEN MATCHED THEN
        UPDATE SET target.TongSoLuongXuat = target.TongSoLuongXuat + src.TongSoLuongXuat,
                   target.TongGiaTriXuat = target.TongGiaTriXuat + src.TongGiaTriXuat
    WHEN NOT MATCHED THEN
        INSERT (MaBCX, NgayLap, MaHH, TongSoLuongXuat, TongGiaTriXuat)
        VALUES (dbo.fn_TaoMaTuDong('BCX', 'BaoCaoXuatHang', 'MaBCX'),
                src.NgayLap, src.MaHang, src.TongSoLuongXuat, src.TongGiaTriXuat);
END;
GO

CREATE TRIGGER trg_BaoCaoTonKho
ON HangHoa
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO BaoCaoTonKho (MaBCT, NgayLap, MaHH, SoLuongTon, GiaTriTon)
    SELECT dbo.fn_TaoMaTuDong('BCT', 'BaoCaoTonKho', 'MaBCT'),
           CAST(GETDATE() AS DATE),
           i.MaHang, i.SoLuongTon, i.SoLuongTon * i.GiaXuat
    FROM inserted i;
END;
GO
-- ==========================================
-- 1. Thêm phiếu nhập mới (tăng tồn kho)
-- ==========================================
-- Phiếu nhập 1
INSERT INTO PhieuNhap (NgayNhap, MaNV, MaNCC)
VALUES (GETDATE(), 'NV001', 'NCC001');

INSERT INTO CT_PhieuNhap (MaPN, MaHang, SoLuong, DonGiaNhap)
VALUES 
('PN001', 'HH001', 10, 12000000),   -- Tivi Samsung
('PN001', 'HH002', 50, 80000);      -- Mì Gói Hảo Hảo

-- Phiếu nhập 2
INSERT INTO PhieuNhap (NgayNhap, MaNV, MaNCC)
VALUES (GETDATE(), 'NV002', 'NCC002');

INSERT INTO CT_PhieuNhap (MaPN, MaHang, SoLuong, DonGiaNhap)
VALUES 
('PN002', 'HH003', 20, 500000),     -- Nồi cơm điện
('PN002', 'HH004', 30, 45000);      -- Nước suối

-- ==========================================
-- 2. Thêm phiếu xuất (giảm tồn kho)
-- ==========================================
-- Phiếu xuất 1
INSERT INTO PhieuXuat (NgayXuat, MaNV, MaKH)
VALUES (GETDATE(), 'NV001', 'KH001');

INSERT INTO CT_PhieuXuat (MaPX, MaHang, SoLuong, DonGiaXuat)
VALUES 
('PX001', 'HH001', 5, 13000000),    -- Tivi Samsung
('PX001', 'HH002', 20, 90000);      -- Mì Gói Hảo Hảo

-- Phiếu xuất 2
INSERT INTO PhieuXuat (NgayXuat, MaNV, MaKH)
VALUES (GETDATE(), 'NV002', 'KH002');

INSERT INTO CT_PhieuXuat (MaPX, MaHang, SoLuong, DonGiaXuat)
VALUES 
('PX002', 'HH003', 10, 550000),     -- Nồi cơm điện
('PX002', 'HH004', 15, 60000);      -- Nước suối
-- Thêm phiếu nhập PN004, PN005
INSERT INTO PhieuNhap (MaNV, MaNCC, NgayNhap)
VALUES 
('NV001', 'NCC001', '2025-10-10'),
('NV002', 'NCC002', '2025-10-11');

-- Thêm chi tiết nhập hàng
INSERT INTO CT_PhieuNhap (MaPN, MaHang, SoLuong, DonGiaNhap)
VALUES
('PN004', 'HH001', 10, 12000000),   -- Tivi Samsung
('PN004', 'HH002', 50, 75000),      -- Mì Gói Hảo Hảo
('PN005', 'HH003', 20, 450000),     -- Nồi cơm điện
('PN005', 'HH004', 100, 50000);     -- Nước suối Lavie

-- ==========================================
-- 3. Kiểm tra tồn kho hiện tại
-- ==========================================
SELECT MaHang, TenHang, SoLuongTon
FROM HangHoa;

-- ==========================================
-- 4. Kiểm tra báo cáo nhập
-- ==========================================
SELECT * FROM BaoCaoNhapHang ORDER BY NgayLap, MaHH;

-- ==========================================
-- 5. Kiểm tra báo cáo xuất
-- ==========================================
SELECT * FROM BaoCaoXuatHang ORDER BY NgayLap, MaHH;

-- ==========================================
-- 6. Kiểm tra báo cáo tồn kho
-- ==========================================
SELECT * FROM BaoCaoTonKho ORDER BY NgayLap, MaHH;
IF OBJECT_ID('trg_BaoCaoNhapHang', 'TR') IS NOT NULL
    DROP TRIGGER trg_BaoCaoNhapHang;
GO

IF OBJECT_ID('trg_BaoCaoNhapHang', 'TR') IS NOT NULL
    DROP TRIGGER trg_BaoCaoNhapHang;
GO

CREATE TRIGGER trg_BaoCaoNhapHang
ON CT_PhieuNhap
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaxSo INT;
    SELECT @MaxSo = ISNULL(MAX(CAST(SUBSTRING(MaBCN, 4, 3) AS INT)), 0) 
    FROM BaoCaoNhapHang;

    -- Lặp từng hàng trong inserted
    INSERT INTO BaoCaoNhapHang (MaBCN, NgayLap, MaHH, TongSoLuongNhap, TongGiaTriNhap)
    SELECT 
        'BCN' + RIGHT('000' + CAST(ROW_NUMBER() OVER (ORDER BY i.MaHang) + @MaxSo AS VARCHAR(3)), 3),
        CAST(GETDATE() AS DATE),
        i.MaHang,
        SUM(i.SoLuong),
        SUM(i.SoLuong * i.DonGiaNhap)
    FROM inserted i
    GROUP BY i.MaHang;
END;
GO
IF OBJECT_ID('trg_BaoCaoXuatHang', 'TR') IS NOT NULL
    DROP TRIGGER trg_BaoCaoXuatHang;
GO

CREATE TRIGGER trg_BaoCaoXuatHang
ON CT_PhieuXuat
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaxSo INT;
    SELECT @MaxSo = ISNULL(MAX(CAST(SUBSTRING(MaBCX, 4, 3) AS INT)), 0) 
    FROM BaoCaoXuatHang;

    INSERT INTO BaoCaoXuatHang (MaBCX, NgayLap, MaHH, TongSoLuongXuat, TongGiaTriXuat)
    SELECT 
        'BCX' + RIGHT('000' + CAST(ROW_NUMBER() OVER (ORDER BY i.MaHang) + @MaxSo AS VARCHAR(3)), 3),
        CAST(GETDATE() AS DATE),
        i.MaHang,
        SUM(i.SoLuong),
        SUM(i.SoLuong * i.DonGiaXuat)
    FROM inserted i
    GROUP BY i.MaHang;
END;
GO

IF OBJECT_ID('trg_BaoCaoTonKho', 'TR') IS NOT NULL
    DROP TRIGGER trg_BaoCaoTonKho;
GO
CREATE OR ALTER TRIGGER trg_BaoCaoTonKho
ON HangHoa
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaxSo INT;
    SELECT @MaxSo = ISNULL(MAX(CAST(SUBSTRING(MaBCT, 4, 3) AS INT)), 0)
    FROM BaoCaoTonKho;

    INSERT INTO BaoCaoTonKho (MaBCT, NgayLap, MaHH, SoLuongTon, GiaTriTon)
    SELECT 
        'BCT' + RIGHT('000' + CAST(ROW_NUMBER() OVER (ORDER BY i.MaHang) + @MaxSo AS VARCHAR(3)), 3),
        CAST(GETDATE() AS DATE),
        i.MaHang,
        i.SoLuongTon,
        i.SoLuongTon * i.GiaXuat
    FROM inserted i;
END;
SELECT name, type_desc 
FROM sys.triggers 
WHERE parent_id = OBJECT_ID('Kho');
SELECT * FROM Kho;
