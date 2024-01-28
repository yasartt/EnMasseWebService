using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnMasseWebService.Migrations
{
    public partial class DailyTypeDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dailies_DailyTypes_DailyTypeId",
                table: "Dailies");

            migrationBuilder.DropTable(
                name: "DailyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Dailies_DailyTypeId",
                table: "Dailies");

            migrationBuilder.DropColumn(
                name: "DailyTypeId",
                table: "Dailies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailyTypeId",
                table: "Dailies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DailyTypes",
                columns: table => new
                {
                    DailyTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTypes", x => x.DailyTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dailies_DailyTypeId",
                table: "Dailies",
                column: "DailyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dailies_DailyTypes_DailyTypeId",
                table: "Dailies",
                column: "DailyTypeId",
                principalTable: "DailyTypes",
                principalColumn: "DailyTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
