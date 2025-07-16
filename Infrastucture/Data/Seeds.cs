using Blog.Domain.Entities;
using Blog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastucture.Data
{
    public class Seeds
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Id = 1, UserName = "guest", Password = "guest123", Role = UserRoles.Guest },
                    new User { Id = 2, UserName = "user", Password = "user123", Role = UserRoles.User },
                    new User { Id = 3, UserName = "admin", Password = "admin123", Role = UserRoles.Admin }
                );
            }

            if (!context.Posts.Any())
            {
                context.Posts.Add(new BlogPost
                {
                    Id = 1,
                    Title = "Welcome",
                    Content = "first seed",
                    Author = "admin",
                    PublishedDate = DateTime.UtcNow
                });
            }

            context.SaveChanges();
        }
    }
}
