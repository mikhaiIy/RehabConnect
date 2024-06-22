using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RehabConnect.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ubahApprovalStatus1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovalStatus",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 1,
                column: "DateIssued",
                value: new DateTime(2024, 6, 23, 1, 44, 44, 109, DateTimeKind.Local).AddTicks(2022));

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportID",
                keyValue: 1,
                column: "DateReport",
                value: new DateTime(2024, 6, 23, 1, 44, 44, 109, DateTimeKind.Local).AddTicks(1958));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: 1,
                column: "ApprovalStatus",
                value: "Accept");
        }
    }
}
