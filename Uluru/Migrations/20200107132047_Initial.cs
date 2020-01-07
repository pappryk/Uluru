using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uluru.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkingGroupId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_WorkingGroup_WorkingGroupId",
                        column: x => x.WorkingGroupId,
                        principalTable: "WorkingGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingGroupSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    WorkingGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingGroupSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingGroupSchedule_WorkingGroup_WorkingGroupId",
                        column: x => x.WorkingGroupId,
                        principalTable: "WorkingGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: false),
                    LastName = table.Column<string>(maxLength: 64, nullable: false),
                    HourlyWage = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    UserRole = table.Column<string>(nullable: false),
                    PositionId = table.Column<int>(nullable: true),
                    WorkingGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("AK_User_Email", x => x.Email);
                    table.ForeignKey(
                        name: "FK_User_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_User_WorkingGroup_WorkingGroupId",
                        column: x => x.WorkingGroupId,
                        principalTable: "WorkingGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    WorkingGroupScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingDay_WorkingGroupSchedule_WorkingGroupScheduleId",
                        column: x => x.WorkingGroupScheduleId,
                        principalTable: "WorkingGroupSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "WorkEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    WorkingGroupScheduleId = table.Column<int>(nullable: false),
                    WorkingAvailabilityId = table.Column<int>(nullable: true),
                    PositionId = table.Column<int>(nullable: false),
                    WorkingDayId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkEntry_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkEntry_WorkingAvailability_WorkingAvailabilityId",
                        column: x => x.WorkingAvailabilityId,
                        principalTable: "WorkingAvailability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkEntry_WorkingDay_WorkingDayId",
                        column: x => x.WorkingDayId,
                        principalTable: "WorkingDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkEntry_WorkingGroupSchedule_WorkingGroupScheduleId",
                        column: x => x.WorkingGroupScheduleId,
                        principalTable: "WorkingGroupSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Position_WorkingGroupId",
                table: "Position",
                column: "WorkingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PositionId",
                table: "User",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_WorkingGroupId",
                table: "User",
                column: "WorkingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkEntry_PositionId",
                table: "WorkEntry",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkEntry_WorkingAvailabilityId",
                table: "WorkEntry",
                column: "WorkingAvailabilityId",
                unique: true,
                filter: "[WorkingAvailabilityId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkEntry_WorkingDayId",
                table: "WorkEntry",
                column: "WorkingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkEntry_WorkingGroupScheduleId",
                table: "WorkEntry",
                column: "WorkingGroupScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingAvailability_UserId",
                table: "WorkingAvailability",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDay_WorkingGroupScheduleId",
                table: "WorkingDay",
                column: "WorkingGroupScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingGroupSchedule_WorkingGroupId",
                table: "WorkingGroupSchedule",
                column: "WorkingGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkEntry");

            migrationBuilder.DropTable(
                name: "WorkingAvailability");

            migrationBuilder.DropTable(
                name: "WorkingDay");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WorkingGroupSchedule");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "WorkingGroup");
        }
    }
}
