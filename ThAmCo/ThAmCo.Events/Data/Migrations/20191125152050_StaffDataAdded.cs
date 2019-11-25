using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class StaffDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staff",
                columns: new[] { "Id", "Email", "FirstAider", "FirstName", "IsActive", "Surname" },
                values: new object[,]
                {
                    { 1, "fred@example.com", true, "Fred", true, "Frederickson" },
                    { 2, "jenny@example.com", false, "Jenny", true, "Jenson" },
                    { 3, "simon@example.com", false, "Simon", true, "Simonson" },
                    { 4, "linda@example.com", false, "Linda", true, "Lindason" },
                    { 5, "top@example.com", false, "Tom", true, "Thompson" },
                    { 6, "rachel@example.com", false, "Rachel", true, "Rachelson" },
                    { 7, "michael@example.com", false, "Mike", true, "Michaelson" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staff",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staff",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staff",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staff",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staff",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staff",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
