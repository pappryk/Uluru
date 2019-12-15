using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uluru.Migrations
{
    public partial class CreateDataBaseStructure_Add_UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "User",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "WorkingAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingAvailability_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingAvailability_UserId",
                table: "WorkingAvailability",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingAvailability");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "User");
        }
    }
}
