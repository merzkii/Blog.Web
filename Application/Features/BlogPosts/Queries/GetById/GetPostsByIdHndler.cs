using AutoMapper;
using Blog.Application.DTO.BlogPosts;
using Blog.Application.Exceptions;
using Blog.Domain.Interfaces;
using MediatR;

namespace Blog.Application.Features.BlogPosts.Queries.GetById
{
    public class GetPostsByIdHndler: IRequestHandler<GetPostsById, BlogPostDTO>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public GetPostsByIdHndler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<BlogPostDTO> Handle(GetPostsById request, CancellationToken cancellationToken)
        {
            var post = await _blogRepository.GetByIdAsync(request.PostId);
            if (post == null)
                throw new NotFoundException("BlogPost", request.PostId);
            return _mapper.Map<BlogPostDTO>(post);
        }
    }
}
