using System.Security.Claims;

namespace Application.Contracts.Infrastructure;

public interface IUserProvider
{
    ClaimsPrincipal? GetCurrentUser();
    Guid? GetCurrentUserId();
}
