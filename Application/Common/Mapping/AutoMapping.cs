using AutoMapper;
using Blog.Application.DTO.BlogPosts;
using Blog.Application.Features.BlogPosts.Commands;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<BlogPost, BlogPostDTO>().ReverseMap();
            CreateMap<CreatePost, BlogPost>().ReverseMap();
            CreateMap<UpdatePost, BlogPost>().ReverseMap();

        }
    }
}
