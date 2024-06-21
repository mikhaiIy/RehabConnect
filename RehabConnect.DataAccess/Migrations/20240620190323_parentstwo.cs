using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RehabConnect.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class parentstwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 1,
                column: "DateIssued",
                value: new DateTime(2024, 6, 21, 3, 3, 22, 153, DateTimeKind.Local).AddTicks(5142));

            migrationBuilder.InsertData(
                table: "ParentDetails",
                columns: new[] { "ParentID", "FatherAddress", "FatherCity", "FatherCountry", "FatherEmail", "FatherIC", "FatherName", "FatherOccupation", "FatherPhoneNum", "FatherPostcode", "FatherRace", "FatherWorkAddress", "HouseholdIncome", "MotherAddress", "MotherCity", "MotherCountry", "MotherEmail", "MotherIC", "MotherName", "MotherOccupation", "MotherPhoneNum", "MotherPostcode", "MotherRace", "MotherWorkAddress", "UserId" },
                values: new object[] { 2, "123 Main St", "City", "Country", "Homelander.doe@example.com", "A1234567", "Homelander", "Occupation", "123456789", "12345", "Race", null, "50000", "123 Main St", "City", "Country", "Deep.doe@example.com", "B7654321", "Deep", "Occupation", "987654321", "12345", "Race", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParentDetails",
                keyColumn: "ParentID",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 1,
                column: "DateIssued",
                value: new DateTime(2024, 6, 21, 1, 11, 59, 337, DateTimeKind.Local).AddTicks(2256));
        }
    }
}
