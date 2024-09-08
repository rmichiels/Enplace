using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enplace.Service.Migrations
{
    /// <inheritdoc />
    public partial class AppliedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeImage",
                table: "RecipeImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory");

            migrationBuilder.RenameTable(
                name: "RecipeImage",
                newName: "RecipeImages");

            migrationBuilder.RenameTable(
                name: "RecipeCategory",
                newName: "RecipeCategories");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeImage_RecipeId",
                table: "RecipeImages",
                newName: "IX_RecipeImages_RecipeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RecipeCategories",
                newName: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeImages",
                table: "RecipeImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeCategories",
                table: "RecipeCategories",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeImages",
                table: "RecipeImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeCategories",
                table: "RecipeCategories");

            migrationBuilder.RenameTable(
                name: "RecipeImages",
                newName: "RecipeImage");

            migrationBuilder.RenameTable(
                name: "RecipeCategories",
                newName: "RecipeCategory");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeImages_RecipeId",
                table: "RecipeImage",
                newName: "IX_RecipeImage_RecipeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "RecipeCategory",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeImage",
                table: "RecipeImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory",
                column: "Id");
        }
    }
}
