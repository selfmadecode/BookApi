using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookApi.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTimeOffset(new DateTime(1650, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Berry", "Griffin Beak Eldritch", "Ships" },
                    { new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"), new DateTimeOffset(new DateTime(1690, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Seabury", "Toxic Reyson", "Maps" },
                    { new Guid("2aadd2df-7caf-45ab-9355-7f6332985a87"), new DateTimeOffset(new DateTime(1723, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Rutherford", "Fearless Cloven", "General debauchery" },
                    { new Guid("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"), new DateTimeOffset(new DateTime(1721, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Atherton", "Crow Ridley", "Rum" },
                    { new Guid("71838f8b-6ab3-4539-9e67-4e77b8ede1c0"), new DateTimeOffset(new DateTime(1969, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Huxford", "The Hawk Morris", "Maps" },
                    { new Guid("119f9ccb-149d-4d3c-ad4f-40100f38e918"), new DateTimeOffset(new DateTime(1972, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Dwennon", "Rigger Quye", "Maps" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("119f9ccb-149d-4d3c-ad4f-40100f38e918"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("2aadd2df-7caf-45ab-9355-7f6332985a87"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("71838f8b-6ab3-4539-9e67-4e77b8ede1c0"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"));
        }
    }
}
