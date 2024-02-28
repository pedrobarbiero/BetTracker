using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Testcontainers.MsSql;


namespace BetTracker.Integration.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorDbContext = services
                 .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BetTrackerDbContext>));
            var descriptorIdentityDbContext = services
                 .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BetTrackerIdentityDbContext>));

            if (descriptorDbContext is not null)
            {
                services.Remove(descriptorDbContext);
            }
            if (descriptorIdentityDbContext is not null)
            {
                services.Remove(descriptorIdentityDbContext);
            }

            var connectionString = _dbContainer.GetConnectionString();
            services.AddDbContext<BetTrackerDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddDbContext<BetTrackerIdentityDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddAuthentication(defaultScheme: "TestScheme")
                   .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                       "TestScheme", options => { });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        using var scope = Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<BetTrackerDbContext>();
        var identityDbContext = scopedServices.GetRequiredService<BetTrackerIdentityDbContext>();

        await dbContext.Database.MigrateAsync();
        await identityDbContext.Database.MigrateAsync();
        await CreateDefaultUsers(identityDbContext);
    }


    Task IAsyncLifetime.DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    private async Task CreateDefaultUsers(BetTrackerIdentityDbContext identityDbContext)
    {
        var user = new User
        {
            Id = Guid.Parse("3ef1ab0e-19c2-4640-40de-08dc381a53e4"),
            UserName = "normal-user@bettracker.com",
            NormalizedUserName = "NORMAL-USER@BETTRACKER.COM",
            Email = "normal-user@bettracker.com",
            NormalizedEmail = "NORMAL-USER@BETTRACKER.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEFVY8MnAtT3leUN1A4SGJju7PJ5Gj9Dl4K5vR75I5w+y+gxTjZ3iMyfVTRTGRY3KOg==",
            SecurityStamp = "WIPG27DGPLSQ5BCBCJ362ND5B6JTBL3M",
            ConcurrencyStamp = "c3b833f0-f7e9-481b-b904-fd0c86c8844d",
        };
        
        await identityDbContext.AddAsync(user);
        await identityDbContext.SaveChangesAsync();
    }
}
