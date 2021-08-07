using Microsoft.EntityFrameworkCore.Migrations;

namespace WeAreFootball.Data.Migrations
{
    public partial class fixThisShit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Leagues_ImageId",
                table: "Leagues");

            migrationBuilder.AlterColumn<string>(
                name: "LogoId",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Leagues",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_ImageId",
                table: "Leagues",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Leagues_ImageId",
                table: "Leagues");

            migrationBuilder.AlterColumn<string>(
                name: "LogoId",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Leagues",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_ImageId",
                table: "Leagues",
                column: "ImageId",
                unique: true);
        }
    }
}
