using Blog.Application.Features.BlogPosts.Commands;
using FluentValidation;

namespace Blog.Application.Features.BlogPosts.Validators.Blog
{
    public class UpdatePostValidator:AbstractValidator<UpdatePost>
    {
        public UpdatePostValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Post ID is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.")
                                       .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.")
                                         .MinimumLength(10).WithMessage("Content must be at least 10 characters long.");
        }
    }
   
}
