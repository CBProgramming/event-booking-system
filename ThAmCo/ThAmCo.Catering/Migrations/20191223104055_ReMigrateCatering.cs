using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Catering.Migrations
{
    public partial class ReMigrateCatering : Migration
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CostPerHead = table.Column<double>(nullable: false),
                    Starter = table.Column<string>(nullable: true),
                    Main = table.Column<string>(nullable: true),
                    Dessert = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodBookings",
                schema: "thamco.menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBookings", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_FoodBookings_Menus_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "thamco.menus",
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "thamco.menus",
                table: "Menus",
                columns: new[] { "Id", "CostPerHead", "Dessert", "Main", "Name", "Starter" },
                values: new object[] { 1, 10.5, "Forest fruit gateaux", "Ham hock and seasonal vegetables", "The Banquet Menu", "Butternut Squash Soup" });

            migrationBuilder.InsertData(
                schema: "thamco.menus",
                table: "Menus",
                columns: new[] { "Id", "CostPerHead", "Dessert", "Main", "Name", "Starter" },
                values: new object[] { 2, 15.25, "New York Cheesecake", "The Megaburger", "The Budget Bonanza", "Salt and Pepper Chips" });

            migrationBuilder.InsertData(
                schema: "thamco.menus",
                table: "Menus",
                columns: new[] { "Id", "CostPerHead", "Dessert", "Main", "Name", "Starter" },
                values: new object[] { 3, 20.0, "Jelly", "Mashed potato and beans", "The Overpiced Special", "Dry toast" });

            migrationBuilder.InsertData(
                schema: "thamco.menus",
                table: "FoodBookings",
                columns: new[] { "EventId", "MenuId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 2, 2 },
                    { 3, 3 }
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
