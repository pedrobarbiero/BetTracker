using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BetTrackerDbContext>(options =>        
            options.UseSqlServer(configuration.GetConnectionString("BetTrackerDbConnection")));
        
        services.AddDbContext<BetTrackerIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityDbConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IIdProvider, IdProvider>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IBetRepository, BetRespository>();

        services.AddScoped<IBankrollRepository, BankrollRepository>();

        return services;
    }
}
