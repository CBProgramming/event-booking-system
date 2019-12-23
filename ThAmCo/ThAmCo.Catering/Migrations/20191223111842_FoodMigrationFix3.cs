using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Catering.Migrations
{
    public partial class FoodMigrationFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "thamco.menus",
                table: "FoodBookings",
                columns: new[] { "EventId", "MenuId", "MenuNumber" },
                values: new object[] { 5, null, 2 });

            migrationBuilder.InsertData(
                schema: "thamco.menus",
                table: "FoodBookings",
                columns: new[] { "EventId", "MenuId", "MenuNumber" },
                values: new object[] { 6, null, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "thamco.menus",
                table: "FoodBookings",
                keyColumn: "EventId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "thamco.menus",
                table: "FoodBookings",
                keyColumn: "EventId",
                keyValue: 6);
        }
    }
}
