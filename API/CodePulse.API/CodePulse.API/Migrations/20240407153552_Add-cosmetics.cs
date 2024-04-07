using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations
{
    /// <inheritdoc />
    public partial class Addcosmetics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostCategory");

            migrationBuilder.AddColumn<Guid>(
                name: "BlogPostId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cosmetics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cosmetics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryCosmetic",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CosmeticsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCosmetic", x => new { x.CategoriesId, x.CosmeticsId });
                    table.ForeignKey(
                        name: "FK_CategoryCosmetic_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCosmetic_Cosmetics_CosmeticsId",
                        column: x => x.CosmeticsId,
                        principalTable: "Cosmetics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BlogPostId",
                table: "Categories",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCosmetic_CosmeticsId",
                table: "CategoryCosmetic",
                column: "CosmeticsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_BlogPosts_BlogPostId",
                table: "Categories",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_BlogPosts_BlogPostId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryCosmetic");

            migrationBuilder.DropTable(
                name: "Cosmetics");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BlogPostId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "BlogPostCategory",
                columns: table => new
                {
                    BlogPostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostCategory", x => new { x.BlogPostsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BlogPostCategory_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostCategory_CategoriesId",
                table: "BlogPostCategory",
                column: "CategoriesId");
        }
    }
}
