using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiInfrastructure.Migrations
{
    public partial class AddLeadtoopportunityheader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "SellOpportunityHeaders");

            migrationBuilder.AddColumn<int>(
                name: "LeadId",
                table: "SellOpportunityHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SellOpportunityHeaders_LeadId",
                table: "SellOpportunityHeaders",
                column: "LeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellOpportunityHeaders_Leads_LeadId",
                table: "SellOpportunityHeaders",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellOpportunityHeaders_Leads_LeadId",
                table: "SellOpportunityHeaders");

            migrationBuilder.DropIndex(
                name: "IX_SellOpportunityHeaders_LeadId",
                table: "SellOpportunityHeaders");

            migrationBuilder.DropColumn(
                name: "LeadId",
                table: "SellOpportunityHeaders");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "SellOpportunityHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
