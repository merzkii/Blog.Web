using Blog.Application.DTO.BlogPosts;
using MediatR;

namespace Blog.Application.Features.BlogPosts.Queries.Search
{
    public class SearchPosts: IRequest<IEnumerable<BlogPostDTO>>
    {
        public string SearchTerm { get; set; }
        public SearchPosts(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}
