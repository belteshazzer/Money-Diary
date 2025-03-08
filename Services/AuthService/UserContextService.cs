using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MoneyDiary.Models.Entities;
using System.Security.Claims;

namespace MoneyDiary.Services.UserContextService
{
    public interface IUserContextService
    {
        string GetUserId();
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}