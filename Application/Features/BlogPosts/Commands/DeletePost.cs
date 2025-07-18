using MediatR;

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
