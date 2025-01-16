using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enplace.Service.Migrations
{
    /// <inheritdoc />
    public partial class IntegrateUnqiueSkidsAndNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SKID",
                table: "Users",
                column: "SKID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SKID",
                table: "Users");
        }
    }
}
