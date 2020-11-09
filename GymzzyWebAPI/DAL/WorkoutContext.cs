using GymzzyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymzzyWebAPI.DAL
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext(DbContextOptions<WorkoutContext> options)
            : base(options)
        { }

        public DbSet<User> User { get; set; }
    }
}
