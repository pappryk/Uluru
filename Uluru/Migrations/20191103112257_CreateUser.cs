using Microsoft.EntityFrameworkCore.Migrations;

namespace Uluru.Migrations
{
    public partial class CreateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blah",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Blah",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
