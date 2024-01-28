using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnMasseWebService.Migrations
{
    public partial class fixForTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoNumber",
                table: "Dailies");

            migrationBuilder.DropColumn(
                name: "VideoNumber",
                table: "Dailies");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Dailies",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Dailies");

            migrationBuilder.AddColumn<int>(
                name: "PhotoNumber",
                table: "Dailies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoNumber",
                table: "Dailies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
