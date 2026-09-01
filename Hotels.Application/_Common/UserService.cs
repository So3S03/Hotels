using Hotels.Application.Abstraction._Common.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Hotels.Application._Common
{
    internal class UserService(IHttpContextAccessor _httpContextAccessor) : IUserService
    {
        public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue("UserId")! ?? "SystemId";

        public string UserName => _httpContextAccessor.HttpContext?.User.FindFirstValue("UserName")! ?? "System";
    }
}
