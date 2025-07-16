using Blog.Application.Features.BlogPosts.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.BlogPosts.Validators.Blog
{
    public class CreatePostValidator : AbstractValidator<CreatePost>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Post.Title).NotEmpty().WithMessage("Title is required.").MaximumLength(100);
            RuleFor(x => x.Post.Content).NotEmpty().WithMessage("Content is required.").MinimumLength(10).WithMessage("Content must be at least 10 characters long");
            RuleFor(x => x.Post.Author).NotEmpty().WithMessage("Author is required.");
        }
    }
}
