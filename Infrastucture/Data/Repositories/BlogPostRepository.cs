using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastucture.Data.Repositories
{
    public class BlogPostRepository : IBlogRepository
    {
        private readonly AppDbContext _context;
        public BlogPostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> AddAsync(BlogPost post)
        {
             _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post; 
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.Posts.OrderByDescending(p => p.PublishedDate).ToListAsync();

        }

        public async Task<BlogPost?> GetByIdAsync(int id)
        {
           return await _context.Posts.FindAsync(id);
        }

        public async Task<IEnumerable<BlogPost>> SearchByTitleAsync(string title)
        {
            return await _context.Posts
                .Where(p => p.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<BlogPost> UpdateAsync(BlogPost post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
