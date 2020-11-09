using GymzzyWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL
{
    public class UserContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.HasIndex(p => p.Nick).IsUnique();
            });
        }
    }
}
