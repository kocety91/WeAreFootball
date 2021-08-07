using Microsoft.EntityFrameworkCore.Migrations;

namespace WeAreFootball.Data.Migrations
{
    public partial class fixThisShitTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_ImageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LogoId",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "LogoId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ImageId",
                table: "Teams",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LogoId",
                table: "Teams",
                column: "LogoId",
                unique: true,
                filter: "[LogoId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_ImageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LogoId",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "LogoId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ImageId",
                table: "Teams",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LogoId",
                table: "Teams",
                column: "LogoId",
                unique: true);
        }
    }
}
