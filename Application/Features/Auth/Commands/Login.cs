using Blog.Application.DTO.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.Auth.Commands
{
    public class Login:IRequest<LoginResponseDTO>
    {
       public LoginDTO LoginDetails { get; set; }
        public Login(LoginDTO login)
        {
            LoginDetails = login;
        }
    }
    
}
