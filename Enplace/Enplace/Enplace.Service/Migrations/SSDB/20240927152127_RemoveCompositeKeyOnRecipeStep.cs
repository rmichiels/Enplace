using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enplace.Service.Migrations.SSDB
{
    /// <inheritdoc />
    public partial class RemoveCompositeKeyOnRecipeStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeSteps",
                table: "RecipeSteps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeSteps",
                table: "RecipeSteps",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeSteps",
                table: "RecipeSteps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeSteps",
                table: "RecipeSteps",
                columns: new[] { "ID", "RecipeID" });
        }
    }
}
