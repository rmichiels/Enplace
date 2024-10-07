using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enplace.Service.Migrations.SSDB
{
    /// <inheritdoc />
    public partial class IntegrateMenuRecipeLinkingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_UserMenuRecipes_N1_Recipes",
                table: "UserMenuRecipes");

            migrationBuilder.DropForeignKey(
                name: "fk_UserMenuRecipes_N1_UserMenus",
                table: "UserMenuRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuRecipes",
                table: "UserMenuRecipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMenuRecipes",
                table: "UserMenuRecipes",
                columns: new[] { "MenuID", "RecipeID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserMenuRecipes_Recipes_RecipeID",
                table: "UserMenuRecipes",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMenuRecipes_UserMenus_MenuID",
                table: "UserMenuRecipes",
                column: "MenuID",
                principalTable: "UserMenus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMenuRecipes_Recipes_RecipeID",
                table: "UserMenuRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMenuRecipes_UserMenus_MenuID",
                table: "UserMenuRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMenuRecipes",
                table: "UserMenuRecipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuRecipes",
                table: "UserMenuRecipes",
                columns: new[] { "MenuID", "RecipeID" });

            migrationBuilder.AddForeignKey(
                name: "fk_UserMenuRecipes_N1_Recipes",
                table: "UserMenuRecipes",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_UserMenuRecipes_N1_UserMenus",
                table: "UserMenuRecipes",
                column: "MenuID",
                principalTable: "UserMenus",
                principalColumn: "ID");
        }
    }
}
