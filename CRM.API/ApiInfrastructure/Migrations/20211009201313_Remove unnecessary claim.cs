using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiInfrastructure.Migrations
{
    public partial class Removeunnecessaryclaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Przeglądanie produktów");

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Modyfikacja produktów");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Przeglądanie szans sprzedaży");

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Przeglądanie produktów");

            migrationBuilder.InsertData(
                table: "ApplicationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[] { 10, "Modyfikacja produktów" });
        }
    }
}
