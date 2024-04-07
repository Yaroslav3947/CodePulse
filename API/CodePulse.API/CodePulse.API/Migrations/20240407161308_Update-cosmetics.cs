using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations
{
    /// <inheritdoc />
    public partial class Updatecosmetics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeaturedImageUrl",
                table: "Cosmetics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Cosmetics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Cosmetics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedImageUrl",
                table: "Cosmetics");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Cosmetics");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Cosmetics");
        }
    }
}
