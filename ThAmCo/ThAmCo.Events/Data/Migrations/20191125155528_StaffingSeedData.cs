using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class StaffingSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffing",
                columns: new[] { "StaffId", "EventId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 5, 1 });
        }
    }
}
