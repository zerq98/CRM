using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiInfrastructure.Migrations
{
    public partial class Changeclaimname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Panel administracji");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Przeglądanie danych firmy");
        }
    }
}
