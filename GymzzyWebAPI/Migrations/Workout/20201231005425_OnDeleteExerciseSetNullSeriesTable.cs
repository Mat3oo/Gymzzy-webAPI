using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymzzyWebAPI.Migrations.Workout
{
    public partial class OnDeleteExerciseSetNullSeriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Exercise_ExerciseId",
                table: "Series");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExerciseId",
                table: "Series",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Exercise_ExerciseId",
                table: "Series",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Exercise_ExerciseId",
                table: "Series");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExerciseId",
                table: "Series",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Exercise_ExerciseId",
                table: "Series",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
