using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.BlogPosts.Commands
{
    public class DeletePost:IRequest<int>
    {
        public int Id { get; set; }
        public DeletePost(int id)
        {
            Id = id;
        }
    }
}
