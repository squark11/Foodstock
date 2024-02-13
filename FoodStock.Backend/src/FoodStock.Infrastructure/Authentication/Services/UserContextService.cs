using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FoodStock.Infrastructure.Authentication.Services;


public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public Guid GetUserId => Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    public Guid GetUserRole => Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.Role).Value);
}
