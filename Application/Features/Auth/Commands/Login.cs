using Blog.Application.DTO.Auth;
using MediatR;

namespace Blog.Application.Features.Auth.Commands
{
    public class Login : IRequest<LoginResponseDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

}
