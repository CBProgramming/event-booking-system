using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class extraTestDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValues: new object[] { 3, 1 });

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
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "Surname" },
                values: new object[,]
                {
                    { 4, "Gareth@example.com", "Gareth", "Garethson" },
                    { 20, "Val@example.com", "Val", "Valson" },
                    { 19, "Peter@example.com", "Peter", "Peterson" },
                    { 18, "Kat@example.com", "Kat", "Katson" },
                    { 17, "Phil@example.com", "Phil", "Philson" },
                    { 15, "Aidan@example.com", "Aidan", "Aidanson" },
                    { 14, "Laura@example.com", "Laura", "Laurason" },
                    { 13, "Camille@example.com", "Camille", "Camilleson" },
                    { 16, "Layla@example.com", "Layla", "Laylason" },
                    { 11, "Sarah@example.com", "Sarah", "Sarahson" },
                    { 10, "Alan@example.com", "Alan", "Alanson" },
                    { 9, "Diane@example.com", "Diane", "Dianeson" },
                    { 8, "Charlotte@example.com", "Charlotte", "Charlotteson" },
                    { 7, "Adam@example.com", "Adam", "Adamson" },
                    { 6, "Mark@example.com", "Mark", "Markson" },
                    { 5, "Alice@example.com", "Alice", "Aliceson" },
                    { 12, "Luke@example.com", "Luke", "Lukeson" }
                });

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Events",
                columns: new[] { "Id", "Date", "Duration", "IsActive", "Title", "TypeId", "VenueCapacity", "VenueCost", "VenueDescription", "VenueName", "VenueRef" },
                values: new object[,]
                {
                    { 3, new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), true, "Billie's Birthday Bonanza", "PTY", 150, 100.0, "", "Tinder Manor", "" },
                    { 4, new DateTime(2019, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), true, "Stevie's Stag", "PYT", 150, 100.0, "", "Tinder Manor", "" },
                    { 5, new DateTime(2019, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), true, "Cheryl and Fleur get hitched", "WED", 150, 100.0, "", "Tinder Manor", "" },
                    { 6, new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), true, "George's Divorce Party", "PTY", 150, 100.0, "", "Tinder Manor", "" }
                });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Guests",
                columns: new[] { "CustomerId", "EventId", "Attended" },
                values: new object[,]
                {
                    { 2, 2, false },
                    { 3, 1, false }
                });

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Staff",
                keyColumn: "Id",
                keyValue: 5,
                column: "Email",
                value: "tom@example.com");

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Guests",
                columns: new[] { "CustomerId", "EventId", "Attended" },
                values: new object[,]
                {
                    { 4, 1, false },
                    { 20, 4, false },
                    { 14, 3, false },
                    { 13, 3, false },
                    { 12, 3, false },
                    { 11, 3, false },
                    { 10, 3, false },
                    { 19, 1, false },
                    { 18, 1, false },
                    { 17, 1, false },
                    { 16, 1, false },
                    { 15, 1, false },
                    { 14, 1, false },
                    { 12, 1, false },
                    { 13, 1, false },
                    { 6, 2, false },
                    { 10, 1, false },
                    { 9, 2, false },
                    { 9, 1, false },
                    { 8, 2, false },
                    { 8, 1, false },
                    { 7, 2, false },
                    { 7, 1, false },
                    { 11, 1, false },
                    { 6, 1, false },
                    { 5, 2, false },
                    { 5, 1, false },
                    { 4, 2, false }
                });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffing",
                columns: new[] { "StaffId", "EventId" },
                values: new object[,]
                {
                    { 4, 4 },
                    { 2, 3 },
                    { 3, 3 },
                    { 5, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 13, 3 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 14, 3 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 18, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 19, 1 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Guests",
                keyColumns: new[] { "CustomerId", "EventId" },
                keyValues: new object[] { 20, 4 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Staffing",
                keyColumns: new[] { "StaffId", "EventId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2018, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "thamco.events",
                table: "Staff",
                keyColumn: "Id",
                keyValue: 5,
                column: "Email",
                value: "top@example.com");

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffing",
                columns: new[] { "StaffId", "EventId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 4, 2 },
                    { 5, 2 }
                });
        }
    }
}
