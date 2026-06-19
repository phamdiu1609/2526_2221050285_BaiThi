using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiThietBis",
                columns: table => new
                {
                    LoaiThietBiId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenLoai = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThietBis", x => x.LoaiThietBiId);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCaps",
                columns: table => new
                {
                    NhaCungCapId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenNhaCungCap = table.Column<string>(type: "TEXT", nullable: false),
                    DiaChi = table.Column<string>(type: "TEXT", nullable: true),
                    SoDienThoai = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCaps", x => x.NhaCungCapId);
                });

            migrationBuilder.CreateTable(
                name: "PhieuXuats",
                columns: table => new
                {
                    PhieuXuatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NgayXuat = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TongTien = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuXuats", x => x.PhieuXuatId);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhaps",
                columns: table => new
                {
                    PhieuNhapId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NgayNhap = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NhaCungCapId = table.Column<int>(type: "INTEGER", nullable: false),
                    TongTien = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhaps", x => x.PhieuNhapId);
                    table.ForeignKey(
                        name: "FK_PhieuNhaps_NhaCungCaps_NhaCungCapId",
                        column: x => x.NhaCungCapId,
                        principalTable: "NhaCungCaps",
                        principalColumn: "NhaCungCapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThietBis",
                columns: table => new
                {
                    ThietBiId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenThietBi = table.Column<string>(type: "TEXT", nullable: false),
                    LoaiThietBiId = table.Column<int>(type: "INTEGER", nullable: false),
                    NhaCungCapId = table.Column<int>(type: "INTEGER", nullable: false),
                    GiaBan = table.Column<decimal>(type: "TEXT", nullable: false),
                    SoLuongTon = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThietBis", x => x.ThietBiId);
                    table.ForeignKey(
                        name: "FK_ThietBis_LoaiThietBis_LoaiThietBiId",
                        column: x => x.LoaiThietBiId,
                        principalTable: "LoaiThietBis",
                        principalColumn: "LoaiThietBiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThietBis_NhaCungCaps_NhaCungCapId",
                        column: x => x.NhaCungCapId,
                        principalTable: "NhaCungCaps",
                        principalColumn: "NhaCungCapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietNhaps",
                columns: table => new
                {
                    ChiTietNhapId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhieuNhapId = table.Column<int>(type: "INTEGER", nullable: false),
                    ThietBiId = table.Column<int>(type: "INTEGER", nullable: false),
                    DonGiaNhap = table.Column<decimal>(type: "TEXT", nullable: false),
                    SoLuong = table.Column<int>(type: "INTEGER", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietNhaps", x => x.ChiTietNhapId);
                    table.ForeignKey(
                        name: "FK_ChiTietNhaps_PhieuNhaps_PhieuNhapId",
                        column: x => x.PhieuNhapId,
                        principalTable: "PhieuNhaps",
                        principalColumn: "PhieuNhapId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietNhaps_ThietBis_ThietBiId",
                        column: x => x.ThietBiId,
                        principalTable: "ThietBis",
                        principalColumn: "ThietBiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietXuats",
                columns: table => new
                {
                    ChiTietXuatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhieuXuatId = table.Column<int>(type: "INTEGER", nullable: false),
                    ThietBiId = table.Column<int>(type: "INTEGER", nullable: false),
                    DonGiaXuat = table.Column<decimal>(type: "TEXT", nullable: false),
                    SoLuong = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietXuats", x => x.ChiTietXuatId);
                    table.ForeignKey(
                        name: "FK_ChiTietXuats_PhieuXuats_PhieuXuatId",
                        column: x => x.PhieuXuatId,
                        principalTable: "PhieuXuats",
                        principalColumn: "PhieuXuatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietXuats_ThietBis_ThietBiId",
                        column: x => x.ThietBiId,
                        principalTable: "ThietBis",
                        principalColumn: "ThietBiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietNhaps_PhieuNhapId",
                table: "ChiTietNhaps",
                column: "PhieuNhapId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietNhaps_ThietBiId",
                table: "ChiTietNhaps",
                column: "ThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietXuats_PhieuXuatId",
                table: "ChiTietXuats",
                column: "PhieuXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietXuats_ThietBiId",
                table: "ChiTietXuats",
                column: "ThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhaps_NhaCungCapId",
                table: "PhieuNhaps",
                column: "NhaCungCapId");

            migrationBuilder.CreateIndex(
                name: "IX_ThietBis_LoaiThietBiId",
                table: "ThietBis",
                column: "LoaiThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_ThietBis_NhaCungCapId",
                table: "ThietBis",
                column: "NhaCungCapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietNhaps");

            migrationBuilder.DropTable(
                name: "ChiTietXuats");

            migrationBuilder.DropTable(
                name: "PhieuNhaps");

            migrationBuilder.DropTable(
                name: "PhieuXuats");

            migrationBuilder.DropTable(
                name: "ThietBis");

            migrationBuilder.DropTable(
                name: "LoaiThietBis");

            migrationBuilder.DropTable(
                name: "NhaCungCaps");
        }
    }
}
