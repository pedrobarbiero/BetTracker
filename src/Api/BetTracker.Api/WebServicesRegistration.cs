using Persistence;
using Domain.Models.Identity;


public static class WebServicesRegistration
{
    public static IServiceCollection ConfigureIdentityEndpoints(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<BetTrackerIdentityDbContext>();

        return services;
    }
}
