using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiInfrastructure.Migrations
{
    public partial class Addrestofapplicationclaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Dodawanie użytkowników");

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Usuwanie użytkowników");

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Przeglądanie danych firmy");

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Przeglądanie leadów");

            migrationBuilder.InsertData(
                table: "ApplicationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Modyfikacja cudzych leadów" },
                    { 6, "Przeglądanie szans sprzedaży" },
                    { 7, "Modyfikacja cudzych szans sprzedaży" },
                    { 8, "Przeglądanie szans sprzedaży" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "AppAdministrator");

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "CEO");

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "IT Administrator");

            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Trader");
        }
    }
}
