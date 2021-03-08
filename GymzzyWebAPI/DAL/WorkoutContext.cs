using GymzzyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymzzyWebAPI.DAL
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext(DbContextOptions<WorkoutContext> options)
            : base(options)
        { }

        public DbSet<Training> Training { get; set; }
        public DbSet<Set> Set { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<PersonalRecord> PersonalRecord { get; set; }
        public DbSet<ExerciseDetails> ExerciseDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ExerciseDetails>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
            });

            builder.Entity<Exercise>()
                .HasOne(p => p.ExerciseDetails)
                .WithMany(p => p.Exercises)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
