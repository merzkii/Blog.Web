using Blog.Application.DTO.BlogPosts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.BlogPosts.Queries.GetAll
{
    public class GetAllPosts:IRequest<IEnumerable<BlogPostDTO>>
    {

    }
}
