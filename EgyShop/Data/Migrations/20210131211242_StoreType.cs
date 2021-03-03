using Microsoft.EntityFrameworkCore.Migrations;

namespace EgyShop.Data.Migrations
{
    public partial class StoreType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreTypeId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StoreType",
                columns: table => new
                {
                    StoreTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreType", x => x.StoreTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreTypeId",
                table: "Stores",
                column: "StoreTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_StoreType_StoreTypeId",
                table: "Stores",
                column: "StoreTypeId",
                principalTable: "StoreType",
                principalColumn: "StoreTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_StoreType_StoreTypeId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "StoreType");

            migrationBuilder.DropIndex(
                name: "IX_Stores_StoreTypeId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreTypeId",
                table: "Stores");
        }
    }
}
