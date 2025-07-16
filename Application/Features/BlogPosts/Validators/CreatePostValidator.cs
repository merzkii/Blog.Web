using Blog.Application.Features.BlogPosts.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.BlogPosts.Validators
{
    public class CreatePostValidator:AbstractValidator<CreatePost>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Post.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Post.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.Post.Author).NotEmpty().WithMessage("Author is required.");
        }
    }
    {
    }
}
