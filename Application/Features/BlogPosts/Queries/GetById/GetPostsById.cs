using Blog.Application.DTO.BlogPosts;
using MediatR;

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
