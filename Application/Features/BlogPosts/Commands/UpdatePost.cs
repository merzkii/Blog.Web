using Blog.Application.DTO.BlogPosts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.BlogPosts.Commands
{
    public class UpdatePost:IRequest<BlogPostDTO>
    {
        public int PostId { get; set; } 
        public CreateBlogPostDTO Post { get; set; }
        public UpdatePost(CreateBlogPostDTO post)
        {
            Post = post;
        }
    }
}
