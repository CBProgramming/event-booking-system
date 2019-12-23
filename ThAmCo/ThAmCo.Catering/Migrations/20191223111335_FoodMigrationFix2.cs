using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Catering.Migrations
{
    public partial class FoodMigrationFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodBookings_Menus_MenuNumber",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.DropIndex(
                name: "IX_FoodBookings_MenuNumber",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                schema: "thamco.menus",
                table: "FoodBookings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodBookings_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodBookings_Menus_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings",
                column: "MenuId",
                principalSchema: "thamco.menus",
                principalTable: "Menus",
                principalColumn: "MenuId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodBookings_Menus_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.DropIndex(
                name: "IX_FoodBookings_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.DropColumn(
                name: "MenuId",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.CreateIndex(
                name: "IX_FoodBookings_MenuNumber",
                schema: "thamco.menus",
                table: "FoodBookings",
                column: "MenuNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodBookings_Menus_MenuNumber",
                schema: "thamco.menus",
                table: "FoodBookings",
                column: "MenuNumber",
                principalSchema: "thamco.menus",
                principalTable: "Menus",
                principalColumn: "MenuId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
