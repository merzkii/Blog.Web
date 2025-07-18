using Blog.Application.DTO.Auth;
using Blog.Application.Exceptions;
using Blog.Application.Features.Auth.Commands;
using Blog.Domain.Interfaces;
using MediatR;

namespace Blog.Application.Features.Auth.Handlers
{
    public class LoginHandler : IRequestHandler<Login, LoginResponseDTO>
    {
        private readonly IUserRepository _userRepository;
        public LoginHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<LoginResponseDTO> Handle(Login request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.UserName, request.Password);
            if (user == null || user.Password != request.Password)
                throw new BadRequestException("Invalid username or password");
            return new LoginResponseDTO
            {
                Username = user.UserName,
                Role = user.Role.ToString(),
            };

        }
    }
}
