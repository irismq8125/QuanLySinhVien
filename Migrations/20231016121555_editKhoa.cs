using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySinhVien.Migrations
{
    /// <inheritdoc />
    public partial class editKhoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Khoa",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Khoa");
        }
    }
}
