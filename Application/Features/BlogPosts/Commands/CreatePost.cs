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
        public string Title { get; set; }   
        public string Content { get; set; }
    }
}
