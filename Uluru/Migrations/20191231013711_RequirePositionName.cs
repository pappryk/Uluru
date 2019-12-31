using Microsoft.EntityFrameworkCore.Migrations;

namespace Uluru.Migrations
{
    public partial class RequirePositionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Position_WorkingGroup_WorkingGroupId",
                table: "Position");

            migrationBuilder.AlterColumn<int>(
                name: "WorkingGroupId",
                table: "Position",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Position",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Position_WorkingGroup_WorkingGroupId",
                table: "Position",
                column: "WorkingGroupId",
                principalTable: "WorkingGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Position_WorkingGroup_WorkingGroupId",
                table: "Position");

            migrationBuilder.AlterColumn<int>(
                name: "WorkingGroupId",
                table: "Position",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Position",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Position_WorkingGroup_WorkingGroupId",
                table: "Position",
                column: "WorkingGroupId",
                principalTable: "WorkingGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
