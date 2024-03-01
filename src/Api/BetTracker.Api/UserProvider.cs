using Application.Contracts.Infrastructure;
using System.Security.Claims;

namespace BetTracker.Api;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public ClaimsPrincipal? GetCurrentUser()
    {
        return _httpContextAccessor.HttpContext?.User;
    }

    public Guid? GetCurrentUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return null;

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid.TryParse(userId, out var result);
        return result;
    }
}
