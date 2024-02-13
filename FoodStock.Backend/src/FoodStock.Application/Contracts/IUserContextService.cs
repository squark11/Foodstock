using System.Security.Claims;

namespace FoodStock.Infrastructure.Authentication.Services;

public interface IUserContextService
{
    ClaimsPrincipal User { get; }
    Guid GetUserId { get; }
    Guid GetUserRole { get; }
}
