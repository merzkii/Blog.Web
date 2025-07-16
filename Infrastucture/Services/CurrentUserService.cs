using Blog.Application.Common.Interfaces;
using Blog.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastucture.Services
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string Username
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "guest";
            }
        }

        public bool IsInRole(UserRoles role)
        {
            var roleString = role.ToString();
            return _httpContextAccessor.HttpContext?.User?.IsInRole(roleString) ?? false;
        }
    }
}
