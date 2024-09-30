using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations
{
    /// <inheritdoc />
    public partial class Upd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_CosmeticsId",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "CosmeticsId",
                table: "CategoryProduct",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProduct_CosmeticsId",
                table: "CategoryProduct",
                newName: "IX_CategoryProduct_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "CategoryProduct",
                newName: "CosmeticsId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                newName: "IX_CategoryProduct_CosmeticsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_CosmeticsId",
                table: "CategoryProduct",
                column: "CosmeticsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
