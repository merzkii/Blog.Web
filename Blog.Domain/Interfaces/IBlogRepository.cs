using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(int id);
        Task<IEnumerable<BlogPost>> SearchByTitleAsync(string title);
        Task<BlogPost> AddAsync(BlogPost post);
        Task<BlogPost> UpdateAsync(BlogPost post);
        Task DeleteAsync(int id);
    }
}
