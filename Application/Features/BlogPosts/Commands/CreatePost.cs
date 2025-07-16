using Blog.Application.DTO.BlogPosts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.BlogPosts.Commands
{
    public class CreatePost:IRequest<BlogPostDTO>
    {
        public CreateBlogPostDTO Post { get; set; }
        public CreatePost(CreateBlogPostDTO post)
        {
            Post = post;
        }
    }
}
