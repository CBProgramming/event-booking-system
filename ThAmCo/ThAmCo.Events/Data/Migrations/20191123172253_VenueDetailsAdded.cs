using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class VenueDetailsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VenueCapacity",
                schema: "thamco.events",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "VenueCost",
                schema: "thamco.events",
                table: "Events",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "VenueDescription",
                schema: "thamco.events",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VenueCapacity",
                schema: "thamco.events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "VenueCost",
                schema: "thamco.events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "VenueDescription",
                schema: "thamco.events",
                table: "Events");
        }
    }
}
