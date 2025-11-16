using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quản_lý_kho_hàng.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaKH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "Kho",
                columns: table => new
                {
                    MaKho = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenKho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChiKho = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kho", x => x.MaKho);
                });

            migrationBuilder.CreateTable(
                name: "LoaiHangs",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHangs", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCaps",
                columns: table => new
                {
                    MaNCC = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenNCC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCaps", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenNV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaNV);
                });

            migrationBuilder.CreateTable(
                name: "HangHoas",
                columns: table => new
                {
                    MaHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    GiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiaXuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaLoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaKho = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoas", x => x.MaHang);
                    table.ForeignKey(
                        name: "FK_HangHoas_Kho_MaKho",
                        column: x => x.MaKho,
                        principalTable: "Kho",
                        principalColumn: "MaKho",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HangHoas_LoaiHangs_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "LoaiHangs",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhaps",
                columns: table => new
                {
                    MaPN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaNCC = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhaps", x => x.MaPN);
                    table.ForeignKey(
                        name: "FK_PhieuNhaps_NhaCungCaps_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCaps",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuNhaps_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuXuats",
                columns: table => new
                {
                    MaPX = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayXuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaKH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuXuats", x => x.MaPX);
                    table.ForeignKey(
                        name: "FK_PhieuXuats_KhachHangs_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuXuats_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaoCaoNhapHang",
                columns: table => new
                {
                    MaBCN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaHH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TongSoLuongNhap = table.Column<int>(type: "int", nullable: false),
                    TongGiaTriNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaoNhapHang", x => x.MaBCN);
                    table.ForeignKey(
                        name: "FK_BaoCaoNhapHang_HangHoas_MaHH",
                        column: x => x.MaHH,
                        principalTable: "HangHoas",
                        principalColumn: "MaHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaoCaoTonKho",
                columns: table => new
                {
                    MaBCT = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaHH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    GiaTriTon = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaoTonKho", x => x.MaBCT);
                    table.ForeignKey(
                        name: "FK_BaoCaoTonKho_HangHoas_MaHH",
                        column: x => x.MaHH,
                        principalTable: "HangHoas",
                        principalColumn: "MaHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaoCaoXuatHang",
                columns: table => new
                {
                    MaBCX = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaHH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TongSoLuongXuat = table.Column<int>(type: "int", nullable: false),
                    TongGiaTriXuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaoXuatHang", x => x.MaBCX);
                    table.ForeignKey(
                        name: "FK_BaoCaoXuatHang_HangHoas_MaHH",
                        column: x => x.MaHH,
                        principalTable: "HangHoas",
                        principalColumn: "MaHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CT_PhieuNhaps",
                columns: table => new
                {
                    MaPN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_PhieuNhaps", x => new { x.MaPN, x.MaHang });
                    table.ForeignKey(
                        name: "FK_CT_PhieuNhaps_HangHoas_MaHang",
                        column: x => x.MaHang,
                        principalTable: "HangHoas",
                        principalColumn: "MaHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_PhieuNhaps_PhieuNhaps_MaPN",
                        column: x => x.MaPN,
                        principalTable: "PhieuNhaps",
                        principalColumn: "MaPN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CT_PhieuXuats",
                columns: table => new
                {
                    MaPX = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGiaXuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_PhieuXuats", x => new { x.MaPX, x.MaHang });
                    table.ForeignKey(
                        name: "FK_CT_PhieuXuats_HangHoas_MaHang",
                        column: x => x.MaHang,
                        principalTable: "HangHoas",
                        principalColumn: "MaHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_PhieuXuats_PhieuXuats_MaPX",
                        column: x => x.MaPX,
                        principalTable: "PhieuXuats",
                        principalColumn: "MaPX",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoNhapHang_MaHH",
                table: "BaoCaoNhapHang",
                column: "MaHH");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoTonKho_MaHH",
                table: "BaoCaoTonKho",
                column: "MaHH");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoXuatHang_MaHH",
                table: "BaoCaoXuatHang",
                column: "MaHH");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PhieuNhaps_MaHang",
                table: "CT_PhieuNhaps",
                column: "MaHang");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PhieuXuats_MaHang",
                table: "CT_PhieuXuats",
                column: "MaHang");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoas_MaKho",
                table: "HangHoas",
                column: "MaKho");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoas_MaLoai",
                table: "HangHoas",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhaps_MaNCC",
                table: "PhieuNhaps",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhaps_MaNV",
                table: "PhieuNhaps",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuats_MaKH",
                table: "PhieuXuats",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuats_MaNV",
                table: "PhieuXuats",
                column: "MaNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaoCaoNhapHang");

            migrationBuilder.DropTable(
                name: "BaoCaoTonKho");

            migrationBuilder.DropTable(
                name: "BaoCaoXuatHang");

            migrationBuilder.DropTable(
                name: "CT_PhieuNhaps");

            migrationBuilder.DropTable(
                name: "CT_PhieuXuats");

            migrationBuilder.DropTable(
                name: "PhieuNhaps");

            migrationBuilder.DropTable(
                name: "HangHoas");

            migrationBuilder.DropTable(
                name: "PhieuXuats");

            migrationBuilder.DropTable(
                name: "NhaCungCaps");

            migrationBuilder.DropTable(
                name: "Kho");

            migrationBuilder.DropTable(
                name: "LoaiHangs");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "NhanViens");
        }
    }
}
