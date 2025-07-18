using AutoMapper;
using Blog.Application.Common.Interfaces;
using Blog.Application.DTO.BlogPosts;
using Blog.Application.Features.BlogPosts.Commands;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using MediatR;

namespace Blog.Application.Features.BlogPosts.Handlers
{
    public class CreatePostHandler : IRequestHandler<CreatePost, BlogPostDTO>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateService _dateService;
        public CreatePostHandler(IBlogRepository blogRepository, IMapper mapper,ICurrentUserService currentUser, IDateService dateService)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _dateService = dateService;
        }

        public async Task<BlogPostDTO> Handle(CreatePost request, CancellationToken cancellationToken)
        {
            var post = new BlogPost
            {
                Title = request.Title,
                Content = request.Content,
                Author = _currentUser.Username,
                PublishedDate = _dateService.UtcNow
            };
            var createdPost = await _blogRepository.AddAsync(post);
            return _mapper.Map<BlogPostDTO>(createdPost);
        }
    }
}
