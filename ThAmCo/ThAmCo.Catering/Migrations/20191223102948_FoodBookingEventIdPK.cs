using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Catering.Migrations
{
    public partial class FoodBookingEventIdPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodBookings",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                schema: "thamco.menus",
                table: "FoodBookings",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodBookings",
                schema: "thamco.menus",
                table: "FoodBookings",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodBookings_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodBookings",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.DropIndex(
                name: "IX_FoodBookings_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                schema: "thamco.menus",
                table: "FoodBookings",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodBookings",
                schema: "thamco.menus",
                table: "FoodBookings",
                columns: new[] { "MenuId", "EventId" });
        }
    }
}
