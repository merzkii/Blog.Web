using Blog.Application.DTO.BlogPosts;
using MediatR;

namespace Blog.Application.Features.BlogPosts.Commands
{
    public class UpdatePost:IRequest<BlogPostDTO>
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Content{get; set; }
    }
}
