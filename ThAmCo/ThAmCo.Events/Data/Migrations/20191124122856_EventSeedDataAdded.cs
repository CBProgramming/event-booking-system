using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class EventSeedDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "VenueCapacity", "VenueCost", "VenueDescription", "VenueName", "VenueRef" },
                values: new object[] { 150, 100.0, "", "Crackling Hall", "" });

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "VenueCapacity", "VenueCost", "VenueDescription", "VenueName", "VenueRef" },
                values: new object[] { 150, 100.0, "", "Crackling Hall", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "VenueCapacity", "VenueCost", "VenueDescription", "VenueName", "VenueRef" },
                values: new object[] { 0, 0.0, null, null, null });

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "VenueCapacity", "VenueCost", "VenueDescription", "VenueName", "VenueRef" },
                values: new object[] { 0, 0.0, null, null, null });
        }
    }
}
