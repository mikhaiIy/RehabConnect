using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RehabConnect.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addStartTimeEndTimeinSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Schedules",
                newName: "StartTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Schedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Schedules",
                newName: "Time");
        }
    }
}
