using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enplace.Service.Migrations.SSDB
{
    /// <inheritdoc />
    public partial class AddRecipeIngredientNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                table: "RecipeIngredients",
                name: "Comment", nullable: true);
        }
    }
}
