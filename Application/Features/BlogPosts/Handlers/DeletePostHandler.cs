using Blog.Application.Common.Interfaces;
using Blog.Application.Exceptions;
using Blog.Application.Features.BlogPosts.Commands;
using Blog.Domain.Enums;
using Blog.Domain.Interfaces;
using MediatR;

namespace Blog.Application.Features.BlogPosts.Handlers
{
    public class DeletePostHandler: IRequestHandler<DeletePost, int>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICurrentUserService _currentUser;
        public DeletePostHandler(IBlogRepository blogRepository, ICurrentUserService currentUser)
        {
            _blogRepository = blogRepository;
            _currentUser = currentUser;
        }
        public async Task<int> Handle(DeletePost request, CancellationToken cancellationToken)
        {
            var post = await _blogRepository.GetByIdAsync(request.Id);
            if (post == null)
                throw new NotFoundException("BlogPost", request.Id);

            if (post.Author != _currentUser.Username && !_currentUser.IsInRole(UserRoles.Admin))
                throw new ForbiddenException("Only the author or admin can delete this post.");

            await _blogRepository.DeleteAsync(request.Id);
            return request.Id;
        }
    }
}
