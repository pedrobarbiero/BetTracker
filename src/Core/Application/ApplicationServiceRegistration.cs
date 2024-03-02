using Application.Behaviors;
using Application.Mappers.Contracts;
using Application.Mappers.Implementation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IBetMapper, BetMapper>();
        services.AddScoped<IBankrollMapper, BankrollMapper>();
        services.AddScoped<ISportMapper, SportMapper>();

        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblyContaining(typeof(ApplicationServicesRegistration));
            c.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });
        services.AddValidatorsFromAssemblyContaining(typeof(ApplicationServicesRegistration));

        return services;
    }
}