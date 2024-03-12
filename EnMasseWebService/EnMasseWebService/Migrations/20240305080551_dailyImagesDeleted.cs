using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnMasseWebService.Migrations
{
    public partial class dailyImagesDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyImages",
                columns: table => new
                {
                    DailyImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyImages", x => x.DailyImageId);
                    table.ForeignKey(
                        name: "FK_DailyImages_Dailies_DailyId",
                        column: x => x.DailyId,
                        principalTable: "Dailies",
                        principalColumn: "DailyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyImages_DailyId",
                table: "DailyImages",
                column: "DailyId");
        }
    }
}
