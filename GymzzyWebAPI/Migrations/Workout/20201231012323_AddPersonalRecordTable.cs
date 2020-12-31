using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymzzyWebAPI.Migrations.Workout
{
    public partial class AddPersonalRecordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeriesId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalRecord_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRecord_SeriesId",
                table: "PersonalRecord",
                column: "SeriesId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalRecord");
        }
    }
}
