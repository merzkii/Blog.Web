using Blog.Application.Features.BlogPosts.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.BlogPosts.Validators.Blog
{
    public class UpdatePostValidator:AbstractValidator<UpdatePost>
    {
        public UpdatePostValidator()
        {
            RuleFor(x => x.PostId).NotEmpty().WithMessage("Post ID is required.");
            RuleFor(x => x.Post.Title).NotEmpty().WithMessage("Title is required.")
                                       .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
            RuleFor(x => x.Post.Content).NotEmpty().WithMessage("Content is required.")
                                         .MinimumLength(10).WithMessage("Content must be at least 10 characters long.");
        }
    }
   
}
