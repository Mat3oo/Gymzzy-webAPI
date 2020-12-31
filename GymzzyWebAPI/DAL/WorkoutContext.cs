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
        public DbSet<Series> Series { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<PersonalRecord> PersonalRecord { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Exercise>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
            });

            builder.Entity<Series>()
                .HasOne(p => p.Exercise)
                .WithMany(p => p.Series)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
