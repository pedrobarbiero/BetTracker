using Application.Mappers.Contracts;
using Application.Mappers.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBetMapper, BetMapper>();

        return services;
    }
}