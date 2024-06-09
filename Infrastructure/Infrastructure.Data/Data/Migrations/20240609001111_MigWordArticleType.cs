using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigWordArticleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cambridge",
                table: "Cambridge");

            migrationBuilder.AddColumn<int>(
                name: "ArticleType",
                table: "Words",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RowNumber",
                table: "Words",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TranslationLanguage",
                table: "Cambridge",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cambridge",
                table: "Cambridge",
                columns: new[] { "Word", "Language", "TranslationLanguage" });

            migrationBuilder.CreateTable(
                name: "ReversoContext",
                columns: table => new
                {
                    Word = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<int>(type: "INTEGER", nullable: false),
                    TranslationLanguage = table.Column<int>(type: "INTEGER", nullable: false),
                    Html = table.Column<string>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    JsonContent = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReversoContext", x => new { x.Word, x.Language, x.TranslationLanguage });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Words_ArticleType",
                table: "Words",
                column: "ArticleType");

            migrationBuilder.CreateIndex(
                name: "IX_Words_Category",
                table: "Words",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Words_Original",
                table: "Words",
                column: "Original",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_RowNumber",
                table: "Words",
                column: "RowNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_Translation",
                table: "Words",
                column: "Translation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReversoContext");

            migrationBuilder.DropIndex(
                name: "IX_Words_ArticleType",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_Category",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_Original",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_RowNumber",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_Translation",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cambridge",
                table: "Cambridge");

            migrationBuilder.DropColumn(
                name: "ArticleType",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "RowNumber",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "TranslationLanguage",
                table: "Cambridge");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cambridge",
                table: "Cambridge",
                columns: new[] { "Word", "Language" });
        }
    }
}
