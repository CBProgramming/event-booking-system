using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class EventTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "thamco.events",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "thamco.events",
                table: "Events");
        }
    }
}
