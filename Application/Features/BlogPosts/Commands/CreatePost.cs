using Blog.Application.DTO.BlogPosts;
using MediatR;

namespace Blog.Application.Features.BlogPosts.Commands
{
    public class CreatePost:IRequest<BlogPostDTO>
    {
        public string Title { get; set; }   
        public string Content { get; set; }
    }
}
