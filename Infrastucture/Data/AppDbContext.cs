using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastucture.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> Posts => Set<BlogPost>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Fluent configuration if needed
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "guest", Password = "guest123", Role = UserRoles.Guest },
                new User { Id = 2, UserName = "user", Password = "user123", Role = UserRoles.User },
                new User { Id = 3, UserName = "admin", Password = "admin123", Role = UserRoles.Admin }
            );

            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost { Id = 1, Title = "Welcome", Content = "Seeded post", Author = "admin", PublishedDate = DateTime.UtcNow }
            );
        }
    }
}
