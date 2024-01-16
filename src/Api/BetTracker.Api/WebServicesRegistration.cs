using Microsoft.AspNetCore.Identity;
using Persistence;
using Domain.Models;


public static class WebServicesRegistration
{
    public static IServiceCollection ConfigureIdentityEndpoints(this IServiceCollection services)
    {
        services.AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<BetTrackerDbContext>()
            .AddApiEndpoints();
        return services;
    }
}
