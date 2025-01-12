using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace klepetalko.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Users_UserskiId",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_UserskiId",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "UserskiId",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Settings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UserId",
                table: "Settings",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Users_UserId",
                table: "Settings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Users_UserId",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_UserId",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "UserskiId",
                table: "Settings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UserskiId",
                table: "Settings",
                column: "UserskiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Users_UserskiId",
                table: "Settings",
                column: "UserskiId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
