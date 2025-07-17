using Blog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string Username { get; set; }    
        public string Role { get; set; }
    }
}
