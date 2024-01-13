using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnMasseWebService.Migrations
{
    public partial class cafeDuzeltme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cafes",
                newName: "CafeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CafeId",
                table: "Cafes",
                newName: "Id");
        }
    }
}
