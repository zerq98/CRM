using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiInfrastructure.Migrations
{
    public partial class Addnewopportunitystatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OpportunityStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Oferta" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OpportunityStatuses",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
