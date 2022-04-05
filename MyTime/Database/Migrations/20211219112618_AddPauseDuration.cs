using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Migrations
{
    public partial class AddPauseDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "PauseDuration",
                table: "WorkTimes",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PauseDuration",
                table: "WorkTimes");
        }
    }
}
