using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymzzyWebAPI.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    LastName = table.Column<string>(maxLength: 256, nullable: true),
                    Nick = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Height = table.Column<float>(nullable: true),
                    Weight = table.Column<float>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    PasswordSalt = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Reps = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    TrainingId = table.Column<Guid>(nullable: false),
                    ExerciseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Series_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Series_ExerciseId",
                table: "Series",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_TrainingId",
                table: "Series",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_UserId",
                table: "Training",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
