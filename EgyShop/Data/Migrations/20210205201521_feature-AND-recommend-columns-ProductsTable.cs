using Microsoft.EntityFrameworkCore.Migrations;

namespace EgyShop.Data.Migrations
{
    public partial class featureANDrecommendcolumnsProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FeatureProduct",
                table: "CategoryProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendProduct",
                table: "CategoryProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureProduct",
                table: "CategoryProducts");

            migrationBuilder.DropColumn(
                name: "RecommendProduct",
                table: "CategoryProducts");
        }
    }
}
