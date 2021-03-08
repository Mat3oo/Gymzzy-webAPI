using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymzzyWebAPI.Migrations.Workout
{
    public partial class NewModelStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRecord_Series_SeriesId",
                table: "PersonalRecord");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropIndex(
                name: "IX_PersonalRecord_SeriesId",
                table: "PersonalRecord");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_Name",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "PersonalRecord");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Exercise");

            migrationBuilder.AddColumn<Guid>(
                name: "SetId",
                table: "PersonalRecord",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseDetailsId",
                table: "Exercise",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
                table: "Exercise",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ExerciseDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Reps = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    ExerciseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Set_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRecord_SetId",
                table: "PersonalRecord",
                column: "SetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_ExerciseDetailsId",
                table: "Exercise",
                column: "ExerciseDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TrainingId",
                table: "Exercise",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDetails_Name",
                table: "ExerciseDetails",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Set_ExerciseId",
                table: "Set",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ExerciseDetails_ExerciseDetailsId",
                table: "Exercise",
                column: "ExerciseDetailsId",
                principalTable: "ExerciseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Training_TrainingId",
                table: "Exercise",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalRecord_Set_SetId",
                table: "PersonalRecord",
                column: "SetId",
                principalTable: "Set",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ExerciseDetails_ExerciseDetailsId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Training_TrainingId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRecord_Set_SetId",
                table: "PersonalRecord");

            migrationBuilder.DropTable(
                name: "ExerciseDetails");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropIndex(
                name: "IX_PersonalRecord_SetId",
                table: "PersonalRecord");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_ExerciseDetailsId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_TrainingId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "SetId",
                table: "PersonalRecord");

            migrationBuilder.DropColumn(
                name: "ExerciseDetailsId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Exercise");

            migrationBuilder.AddColumn<Guid>(
                name: "SeriesId",
                table: "PersonalRecord",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Exercise",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Exercise",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Series_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRecord_SeriesId",
                table: "PersonalRecord",
                column: "SeriesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Name",
                table: "Exercise",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Series_ExerciseId",
                table: "Series",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_TrainingId",
                table: "Series",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalRecord_Series_SeriesId",
                table: "PersonalRecord",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
