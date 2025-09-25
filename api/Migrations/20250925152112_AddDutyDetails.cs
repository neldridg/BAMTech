using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stargate.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDutyDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AstronautDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "CareerStartDate",
                value: new DateTime(2025, 9, 25, 10, 21, 12, 731, DateTimeKind.Local).AddTicks(7343));

            migrationBuilder.UpdateData(
                table: "AstronautDuty",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DutyStartDate", "Rank" },
                values: new object[] { new DateTime(2025, 9, 25, 10, 21, 12, 731, DateTimeKind.Local).AddTicks(7395), "2LT" });

            migrationBuilder.InsertData(
                table: "AstronautDuty",
                columns: new[] { "Id", "DutyEndDate", "DutyStartDate", "DutyTitle", "PersonId", "Rank" },
                values: new object[] { 2, new DateTime(2030, 9, 25, 10, 21, 12, 731, DateTimeKind.Local).AddTicks(7401), new DateTime(2025, 9, 24, 10, 21, 12, 731, DateTimeKind.Local).AddTicks(7398), "Commander2", 1, "1LT" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AstronautDuty",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AstronautDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "CareerStartDate",
                value: new DateTime(2025, 9, 25, 10, 16, 21, 91, DateTimeKind.Local).AddTicks(981));

            migrationBuilder.UpdateData(
                table: "AstronautDuty",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DutyStartDate", "Rank" },
                values: new object[] { new DateTime(2025, 9, 25, 10, 16, 21, 91, DateTimeKind.Local).AddTicks(1036), "1LT" });
        }
    }
}
