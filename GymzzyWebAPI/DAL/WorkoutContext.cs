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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Exercise>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
            });
        }
    }
}
