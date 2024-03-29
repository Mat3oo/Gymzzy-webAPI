﻿// <auto-generated />
using System;
using GymzzyWebAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GymzzyWebAPI.Migrations.Workout
{
    [DbContext(typeof(WorkoutContext))]
    partial class WorkoutContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GymzzyWebAPI.Models.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ExerciseDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TrainingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseDetailsId");

                    b.HasIndex("TrainingId");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("GymzzyWebAPI.Models.ExerciseDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ExerciseDetails");
                });

            modelBuilder.Entity("GymzzyWebAPI.Models.PersonalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SetId")
                        .IsUnique();

                    b.ToTable("PersonalRecord");
                });

            modelBuilder.Entity("GymzzyWebAPI.Models.Set", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("GymzzyWebAPI.Models.Training", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Training");
                });

            modelBuilder.Entity("GymzzyWebAPI.Models.Exercise", b =>
                {
                    b.HasOne("GymzzyWebAPI.Models.ExerciseDetails", "ExerciseDetails")
                        .WithMany("Exercises")
                        .HasForeignKey("ExerciseDetailsId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("GymzzyWebAPI.Models.Training", "Training")
                        .WithMany("Exercises")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GymzzyWebAPI.Models.PersonalRecord", b =>
                {
                    b.HasOne("GymzzyWebAPI.Models.Set", "Set")
                        .WithOne("PersonalRecord")
                        .HasForeignKey("GymzzyWebAPI.Models.PersonalRecord", "SetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GymzzyWebAPI.Models.Set", b =>
                {
                    b.HasOne("GymzzyWebAPI.Models.Exercise", "Exercise")
                        .WithMany("Sets")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
