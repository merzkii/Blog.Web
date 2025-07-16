using Blog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string Username { get; }
        bool IsInRole(UserRoles role);
    }
}
