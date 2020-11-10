using Microsoft.EntityFrameworkCore.Migrations;

namespace GymzzyWebAPI.Migrations.User
{
    public partial class UserName_Instead_Of_Nick_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Nick",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nick",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nick",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Nick",
                table: "AspNetUsers",
                column: "Nick",
                unique: true);
        }
    }
}
