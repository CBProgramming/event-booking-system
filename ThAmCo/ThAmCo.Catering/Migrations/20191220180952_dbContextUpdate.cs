using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Catering.Migrations
{
    public partial class dbContextUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "thamco.menus");

            migrationBuilder.RenameTable(
                name: "Menus",
                schema: "thamco.venues",
                newName: "Menus",
                newSchema: "thamco.menus");

            migrationBuilder.RenameTable(
                name: "FoodBookings",
                schema: "thamco.venues",
                newName: "FoodBookings",
                newSchema: "thamco.menus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "thamco.venues");

            migrationBuilder.RenameTable(
                name: "Menus",
                schema: "thamco.menus",
                newName: "Menus",
                newSchema: "thamco.venues");

            migrationBuilder.RenameTable(
                name: "FoodBookings",
                schema: "thamco.menus",
                newName: "FoodBookings",
                newSchema: "thamco.venues");
        }
    }
}
