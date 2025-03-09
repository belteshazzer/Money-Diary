// filepath: d:\MY_FILE\Projects\MoneyDiary\Services\AuthService\UserContextService.cs
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
        private readonly ILogger<UserContextService> _logger;

        public UserContextService(IHttpContextAccessor httpContextAccessor, ILogger<UserContextService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public string GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogWarning("HttpContext is null.");
                return null;
            }

            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                _logger.LogWarning("ClaimTypes.NameIdentifier is not found.");
            }
            else
            {
                _logger.LogInformation($"User ID found: {userId}");
            }

            return userId;
        }
    }
}