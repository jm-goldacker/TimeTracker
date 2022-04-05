using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Migrations
{
    public partial class AddDurationOfWorkTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "WorkTimes",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "WorkTimes",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "WorkTimes",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "WorkTimes",
                newName: "EndTime");
        }
    }
}
