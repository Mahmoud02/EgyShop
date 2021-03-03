using Microsoft.EntityFrameworkCore.Migrations;

namespace EgyShop.Data.Migrations
{
    public partial class AddThemeSettngTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreThemeSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavabarBackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NabarTextColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavabarIconColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavabrIconToggl = table.Column<bool>(type: "bit", nullable: false),
                    StoreDescriotionTextColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreDesctiptionToggle = table.Column<bool>(type: "bit", nullable: false),
                    FeatureProductsToggle = table.Column<bool>(type: "bit", nullable: false),
                    RecoomendProductsToggle = table.Column<bool>(type: "bit", nullable: false),
                    CarsoulToggle = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfProductsPerRow = table.Column<int>(type: "int", nullable: false),
                    ShowProductImage = table.Column<bool>(type: "bit", nullable: false),
                    ShowProductName = table.Column<bool>(type: "bit", nullable: false),
                    ShowProductPrice = table.Column<bool>(type: "bit", nullable: false),
                    ShowProductDescription = table.Column<bool>(type: "bit", nullable: false),
                    ShowProductCategory = table.Column<bool>(type: "bit", nullable: false),
                    FooterBackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterTextColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterIconColo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToggleFooterIcon = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreThemeSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreThemeSettings_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreThemeSettings_StoreId",
                table: "StoreThemeSettings",
                column: "StoreId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreThemeSettings");
        }
    }
}
