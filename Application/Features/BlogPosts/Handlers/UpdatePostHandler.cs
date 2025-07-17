using AutoMapper;
using Blog.Application.Common.Interfaces;
using Blog.Application.DTO.BlogPosts;
using Blog.Application.Exceptions;
using Blog.Application.Features.BlogPosts.Commands;
using Blog.Domain.Enums;
using Blog.Domain.Interfaces;
using MediatR;

namespace Blog.Application.Features.BlogPosts.Handlers
{
    public class UpdatePostHandler: IRequestHandler<UpdatePost, BlogPostDTO>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateService _dateService;
        public UpdatePostHandler(IBlogRepository blogRepository, IMapper mapper, ICurrentUserService currentUser, IDateService dateService)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _dateService = dateService;
        }

        public async Task<BlogPostDTO> Handle(UpdatePost request, CancellationToken cancellationToken)
        {
            var existingPost = await _blogRepository.GetByIdAsync(request.Id);
            if (existingPost == null)
                throw new NotFoundException("BlogPost", request.Id);
            if (existingPost.Author != _currentUser.Username && !_currentUser.IsInRole(UserRoles.Admin))
                throw new ForbiddenException("Only the author or admin can update this post.");
            existingPost.Title = request.Title;
            existingPost.Content = request.Content;
            existingPost.LastModifiedDate = _dateService.UtcNow;
            var updatedPost = await _blogRepository.UpdateAsync(existingPost);
            return _mapper.Map<BlogPostDTO>(updatedPost);
        }
    }
}
