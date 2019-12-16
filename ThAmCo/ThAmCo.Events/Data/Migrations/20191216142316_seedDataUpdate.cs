using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class seedDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "VenueDescription",
                value: "Once the residence of Lord and Lady Crackling, this lavish dwelling remains a prime example of 18th century fine living.");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "VenueDescription",
                value: "Once the residence of Lord and Lady Crackling, this lavish dwelling remains a prime example of 18th century fine living.");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "VenueDescription",
                value: "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "VenueDescription",
                value: "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "VenueDescription",
                value: "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "VenueDescription",
                value: "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "VenueDescription",
                value: "");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "VenueDescription",
                value: "");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "VenueDescription",
                value: "");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "VenueDescription",
                value: "");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "VenueDescription",
                value: "");

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "VenueDescription",
                value: "");
        }
    }
}
