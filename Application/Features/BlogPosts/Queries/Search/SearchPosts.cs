using Blog.Application.DTO.BlogPosts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
