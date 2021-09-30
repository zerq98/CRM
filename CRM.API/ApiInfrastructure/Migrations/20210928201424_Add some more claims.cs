using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiInfrastructure.Migrations
{
    public partial class Addsomemoreclaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[] { 9, "Przeglądanie produktów" });

            migrationBuilder.InsertData(
                table: "ApplicationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[] { 10, "Modyfikacja produktów" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
