using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enplace.Service.Migrations.SSDB
{
    /// <inheritdoc />
    public partial class MenuScaleAndUserRecipeSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_UserRecipes_N1_Recipes",
                table: "UserRecipes");

            migrationBuilder.DropForeignKey(
                name: "fk_UserRecipes_N1_Users",
                table: "UserRecipes");

            migrationBuilder.AddColumn<int>(
                name: "Scale",
                table: "UserMenuRecipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecipes_Recipes_RecipeID",
                table: "UserRecipes",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecipes_Users_UserID",
                table: "UserRecipes",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecipes_Recipes_RecipeID",
                table: "UserRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRecipes_Users_UserID",
                table: "UserRecipes");

            migrationBuilder.DropColumn(
                name: "Scale",
                table: "UserMenuRecipes");

            migrationBuilder.AddForeignKey(
                name: "fk_UserRecipes_N1_Recipes",
                table: "UserRecipes",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_UserRecipes_N1_Users",
                table: "UserRecipes",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}
