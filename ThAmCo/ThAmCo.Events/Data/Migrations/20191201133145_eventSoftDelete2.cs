using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class eventSoftDelete2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: false);
        }
    }
}
