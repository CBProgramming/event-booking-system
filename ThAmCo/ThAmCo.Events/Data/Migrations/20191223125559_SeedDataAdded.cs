using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class SeedDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "Party");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Type",
                value: "Wedding");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: "Party");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "Type",
                value: "Party");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "Type",
                value: "Wedding");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "Type",
                value: "Party");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: null);

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Type",
                value: null);

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: null);

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "Type",
                value: null);

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "Type",
                value: null);

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "Type",
                value: null);
        }
    }
}
