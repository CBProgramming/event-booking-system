using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Catering.Migrations
{
    public partial class foodInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "thamco.venues");

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "thamco.venues",
                columns: table => new
                {
                    MenuId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CostPerHead = table.Column<double>(nullable: false),
                    Starter = table.Column<string>(nullable: true),
                    Main = table.Column<string>(nullable: true),
                    Dessert = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "FoodBookings",
                schema: "thamco.venues",
                columns: table => new
                {
                    MenuId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBookings", x => new { x.MenuId, x.EventId });
                    table.ForeignKey(
                        name: "FK_FoodBookings_Menus_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "thamco.venues",
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "thamco.venues",
                table: "Menus",
                columns: new[] { "MenuId", "CostPerHead", "Dessert", "Main", "Name", "Starter" },
                values: new object[] { 1, 10.5, "Forest fruit gateaux", "Ham hock and seasonal vegetables", "The Banquet Menu", "Butternut Squash Soup" });

            migrationBuilder.InsertData(
                schema: "thamco.venues",
                table: "Menus",
                columns: new[] { "MenuId", "CostPerHead", "Dessert", "Main", "Name", "Starter" },
                values: new object[] { 2, 15.25, "New York Cheesecake", "The Megaburger", "The Budget Bonanza", "Salt and Pepper Chips" });

            migrationBuilder.InsertData(
                schema: "thamco.venues",
                table: "Menus",
                columns: new[] { "MenuId", "CostPerHead", "Dessert", "Main", "Name", "Starter" },
                values: new object[] { 3, 20.0, "Jelly", "Mashed potato and beans", "The Overpiced Special", "Dry toast" });

            migrationBuilder.InsertData(
                schema: "thamco.venues",
                table: "FoodBookings",
                columns: new[] { "MenuId", "EventId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 2, 2 },
                    { 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodBookings",
                schema: "thamco.venues");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "thamco.venues");
        }
    }
}
