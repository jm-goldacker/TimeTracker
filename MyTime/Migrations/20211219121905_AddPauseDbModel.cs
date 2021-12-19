using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Migrations
{
    public partial class AddPauseDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "WorkTimes");

            migrationBuilder.RenameColumn(
                name: "PauseDuration",
                table: "WorkTimes",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "WorkTimes",
                newName: "End");

            migrationBuilder.CreateTable(
                name: "PauseTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WorkTimeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PauseTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PauseTimes_WorkTimes_WorkTimeId",
                        column: x => x.WorkTimeId,
                        principalTable: "WorkTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PauseTimes_WorkTimeId",
                table: "PauseTimes",
                column: "WorkTimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PauseTimes");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "WorkTimes",
                newName: "PauseDuration");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "WorkTimes",
                newName: "Duration");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "WorkTimes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
