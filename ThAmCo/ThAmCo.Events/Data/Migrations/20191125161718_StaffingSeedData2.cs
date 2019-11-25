using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class StaffingSeedData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffing",
                columns: new[] { "StaffId", "EventId" },
                values: new object[] { 4, 2 });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffing",
                columns: new[] { "StaffId", "EventId" },
                values: new object[] { 5, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffing",
                columns: new[] { "StaffId", "EventId" },
                values: new object[] { 4, 1 });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffing",
                columns: new[] { "StaffId", "EventId" },
                values: new object[] { 5, 1 });
        }
    }
}
