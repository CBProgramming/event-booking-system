using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Catering.Migrations
{
    public partial class AddMigrationnewCateringProjectThAmCoCatering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "thamco.menus");

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "thamco.menus",
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
                schema: "thamco.menus",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuNumber = table.Column<int>(nullable: false),
                    MenuId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBookings", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_FoodBookings_Menus_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "thamco.menus",
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "thamco.menus",
                table: "FoodBookings",
                columns: new[] { "EventId", "MenuId", "MenuNumber" },
                values: new object[,]
                {
                    { 1, null, 1 },
                    { 2, null, 2 },
                    { 3, null, 3 },
                    { 4, null, 1 },
                    { 5, null, 2 },
                    { 6, null, 3 }
                });

            migrationBuilder.InsertData(
                schema: "thamco.menus",
                table: "Menus",
                columns: new[] { "MenuId", "CostPerHead", "Dessert", "Main", "Name", "Starter" },
                values: new object[,]
                {
                    { 1, 10.5, "Forest fruit gateaux", "Ham hock and seasonal vegetables", "The Banquet Menu", "Butternut Squash Soup" },
                    { 2, 15.25, "New York Cheesecake", "The Megaburger", "The Budget Bonanza", "Salt and Pepper Chips" },
                    { 3, 20.0, "Jelly", "Mashed potato and beans", "The Overpiced Special", "Dry toast" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodBookings_MenuId",
                schema: "thamco.menus",
                table: "FoodBookings",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodBookings",
                schema: "thamco.menus");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "thamco.menus");
        }
    }
}
