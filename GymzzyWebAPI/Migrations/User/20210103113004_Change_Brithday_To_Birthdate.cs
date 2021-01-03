using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymzzyWebAPI.Migrations.User
{
    public partial class Change_Brithday_To_Birthdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
