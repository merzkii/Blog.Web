using Blog.Domain.Enums;

namespace Blog.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string Username { get; }
        bool IsInRole(UserRoles role);
    }
}
