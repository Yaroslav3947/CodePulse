using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostCategory_BlogPosts_BLogPostsId",
                table: "BlogPostCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostCategory_Categories_BLogPostsId1",
                table: "BlogPostCategory");

            migrationBuilder.RenameColumn(
                name: "BLogPostsId",
                table: "BlogPostCategory",
                newName: "BlogPostsId");

            migrationBuilder.RenameColumn(
                name: "BLogPostsId1",
                table: "BlogPostCategory",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostCategory_BLogPostsId1",
                table: "BlogPostCategory",
                newName: "IX_BlogPostCategory_CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostCategory_BlogPosts_BlogPostsId",
                table: "BlogPostCategory",
                column: "BlogPostsId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostCategory_Categories_CategoriesId",
                table: "BlogPostCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostCategory_BlogPosts_BlogPostsId",
                table: "BlogPostCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostCategory_Categories_CategoriesId",
                table: "BlogPostCategory");

            migrationBuilder.RenameColumn(
                name: "BlogPostsId",
                table: "BlogPostCategory",
                newName: "BLogPostsId");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "BlogPostCategory",
                newName: "BLogPostsId1");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostCategory_CategoriesId",
                table: "BlogPostCategory",
                newName: "IX_BlogPostCategory_BLogPostsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostCategory_BlogPosts_BLogPostsId",
                table: "BlogPostCategory",
                column: "BLogPostsId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostCategory_Categories_BLogPostsId1",
                table: "BlogPostCategory",
                column: "BLogPostsId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
