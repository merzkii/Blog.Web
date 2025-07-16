using Blog.Application.DTO.Auth;
using Blog.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login( Login command)
        {
            var user = await _mediator.Send(command);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };

            var identity = new ClaimsIdentity(claims, "DefaultScheme");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("DefaultScheme", principal);

            return Ok(new LoginResponseDTO
            {
                Username = user.Username,
                Role = user.Role
            });
        }
    }
}
