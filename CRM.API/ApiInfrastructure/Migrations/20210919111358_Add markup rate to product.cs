using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiInfrastructure.Migrations
{
    public partial class Addmarkupratetoproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MarkupRate",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkupRate",
                table: "Products");
        }
    }
}
