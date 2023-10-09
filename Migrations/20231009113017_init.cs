using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySinhVien.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeTai",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaDeTai = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TenDeTai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    KinhPhi = table.Column<decimal>(type: "money", nullable: true),
                    NoiThucTap = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Filter = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeTai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiangVien",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    MaGV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TenGV = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Luong = table.Column<decimal>(type: "money", nullable: true),
                    MaKhoa = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Filter = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "HuongDan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaDeTai = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    MaGV = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    MaSV = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    KetQua = table.Column<double>(type: "float", nullable: true),
                    Filter = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuongDan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaKhoa = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TenKhoa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SDT = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Filter = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaSV = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TenSV = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    MaKhoa = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Filter = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeTai");

            migrationBuilder.DropTable(
                name: "GiangVien");

            migrationBuilder.DropTable(
                name: "HuongDan");

            migrationBuilder.DropTable(
                name: "Khoa");

            migrationBuilder.DropTable(
                name: "SinhVien");
        }
    }
}
