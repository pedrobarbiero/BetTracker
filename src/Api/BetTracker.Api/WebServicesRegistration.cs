using Microsoft.AspNetCore.Identity;
using Persistence;
using Domain.Models.Identity;


public static class WebServicesRegistration
{
    public static IServiceCollection ConfigureIdentityEndpoints(this IServiceCollection services)
    {
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<BetTrackerIdentityDbContext>()
            .AddApiEndpoints();
        return services;
    }
}
