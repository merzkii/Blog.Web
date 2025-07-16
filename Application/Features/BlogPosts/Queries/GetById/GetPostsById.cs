using Blog.Application.DTO.BlogPosts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.BlogPosts.Queries.GetById
{
    public class GetPostsById: IRequest<BlogPostDTO>
    {
        public int PostId { get; set; }
        public GetPostsById(int postId)
        {
            PostId = postId;
        }
    }
}
