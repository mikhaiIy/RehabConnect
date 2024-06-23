using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RehabConnect.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTherapistColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TherapistID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 1,
                column: "DateIssued",
                value: new DateTime(2024, 6, 23, 15, 16, 37, 991, DateTimeKind.Local).AddTicks(2746));

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportID",
                keyValue: 1,
                column: "DateReport",
                value: new DateTime(2024, 6, 23, 15, 16, 37, 991, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: 1,
                column: "TherapistID",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Students_TherapistID",
                table: "Students",
                column: "TherapistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Therapists_TherapistID",
                table: "Students",
                column: "TherapistID",
                principalTable: "Therapists",
                principalColumn: "TherapistID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Therapists_TherapistID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_TherapistID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TherapistID",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 1,
                column: "DateIssued",
                value: new DateTime(2024, 6, 23, 2, 29, 45, 788, DateTimeKind.Local).AddTicks(2872));

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportID",
                keyValue: 1,
                column: "DateReport",
                value: new DateTime(2024, 6, 23, 2, 29, 45, 788, DateTimeKind.Local).AddTicks(2808));
        }
    }
}
