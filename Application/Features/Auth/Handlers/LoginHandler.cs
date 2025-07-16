using Blog.Application.DTO.Auth;
using Blog.Application.Features.Auth.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.Auth.Handlers
{
    public class LoginHandler: IRequestHandler<Login, LoginResponseDTO>
    {

    }
}
