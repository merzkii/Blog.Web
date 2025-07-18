using Blog.Application.DTO.BlogPosts;
using MediatR;

namespace Blog.Application.Features.BlogPosts.Queries.GetAll
{
    public class GetAllPosts:IRequest<IEnumerable<BlogPostDTO>>
    {

    }
}
