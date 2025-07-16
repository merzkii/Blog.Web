using AutoMapper;
using Blog.Application.DTO.BlogPosts;
using Blog.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.BlogPosts.Queries.Search
{
    public class SearchPostsHandler: IRequestHandler<SearchPosts, IEnumerable<BlogPostDTO>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public SearchPostsHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BlogPostDTO>> Handle(SearchPosts request, CancellationToken cancellationToken)
        {
            var posts = await _blogRepository.SearchByTitleAsync(request.SearchTerm);
            return _mapper.Map<IEnumerable<BlogPostDTO>>(posts);
        }
    }
    
}
