using Blog.Application.Features.BlogPosts.Commands;
using FluentValidation;

namespace Blog.Application.Features.BlogPosts.Validators.Blog
{
    public class CreatePostValidator : AbstractValidator<CreatePost>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.").MaximumLength(100);
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.").MinimumLength(10).WithMessage("Content must be at least 10 characters long");
           
        }
    }
}
