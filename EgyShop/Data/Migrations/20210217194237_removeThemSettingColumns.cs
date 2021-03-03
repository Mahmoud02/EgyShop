using Microsoft.EntityFrameworkCore.Migrations;

namespace EgyShop.Data.Migrations
{
    public partial class removeThemSettingColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NavabarIconColor",
                table: "StoreThemeSettings");

            migrationBuilder.DropColumn(
                name: "NavabrIconToggl",
                table: "StoreThemeSettings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NavabarIconColor",
                table: "StoreThemeSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NavabrIconToggl",
                table: "StoreThemeSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
