using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Catering.Migrations
{
    public partial class FoodMigrationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodBookings_Menus_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "thamco.menus",
                table: "Menus",
                newName: "MenuId");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                schema: "thamco.menus",
                table: "FoodBookings",
                newName: "MenuNumber");

            migrationBuilder.RenameIndex(
                name: "IX_FoodBookings_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings",
                newName: "IX_FoodBookings_MenuNumber");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodBookings_Menus_MenuNumber",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                schema: "thamco.menus",
                table: "Menus",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MenuNumber",
                schema: "thamco.menus",
                table: "FoodBookings",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodBookings_MenuNumber",
                schema: "thamco.menus",
                table: "FoodBookings",
                newName: "IX_FoodBookings_MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodBookings_Menus_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings",
                column: "MenuId",
                principalSchema: "thamco.menus",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
