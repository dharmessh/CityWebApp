using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityAPI.Migrations
{
    public partial class SeedDAtabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityDescription", "CityName" },
                values: new object[] { 1, "India's Capital", "New Delhi" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityDescription", "CityName" },
                values: new object[] { 2, "India's Finance Hub", "Mumbai" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityDescription", "CityName" },
                values: new object[] { 3, "India's Largest Exporter", "Jamnagar" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "CityId", "PlaceDescription", "PlaceName" },
                values: new object[] { 1, 1, "President, Lok Sabha and Rajya Sabha", "Parliament House" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "CityId", "PlaceDescription", "PlaceName" },
                values: new object[] { 2, 1, "It is UNESCO World Heritage Site.", "Qutub Minar" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "CityId", "PlaceDescription", "PlaceName" },
                values: new object[] { 3, 2, "Five Star Hotel in Mumbai", "Hotel Taj Mahel" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "CityId", "PlaceDescription", "PlaceName" },
                values: new object[] { 4, 3, "World's Largest Oil Refinary", "Reliance Industries Limited" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "CityId", "PlaceDescription", "PlaceName" },
                values: new object[] { 5, 3, "Formerly Known as Essar Oil and Essar Power Limited", "Nyara Energy" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
