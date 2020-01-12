using Microsoft.EntityFrameworkCore.Migrations;

namespace Uluru.Migrations
{
    public partial class AddUserToWorkEntryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "WorkEntry",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkEntry_UserId",
                table: "WorkEntry",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkEntry_User_UserId",
                table: "WorkEntry",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkEntry_User_UserId",
                table: "WorkEntry");

            migrationBuilder.DropIndex(
                name: "IX_WorkEntry_UserId",
                table: "WorkEntry");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkEntry");
        }
    }
}
