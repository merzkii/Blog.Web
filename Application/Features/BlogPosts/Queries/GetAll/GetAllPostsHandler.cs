using AutoMapper;
using Blog.Application.DTO.BlogPosts;
using Blog.Domain.Interfaces;
using MediatR;

namespace Blog.Application.Features.BlogPosts.Queries.GetAll
{
    public class GetAllPostsHandler: IRequestHandler<GetAllPosts, IEnumerable<BlogPostDTO>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public GetAllPostsHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BlogPostDTO>> Handle(GetAllPosts request, CancellationToken cancellationToken)
        {
            var posts = await _blogRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BlogPostDTO>>(posts);
        }
    }
}
