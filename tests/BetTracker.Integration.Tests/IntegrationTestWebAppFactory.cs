using Domain.Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Net.Http.Json;
using Testcontainers.MsSql;


namespace BetTracker.Integration.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();
    public required HttpClient AuthorizedClient { get; set; }
    public required HttpClient UnathorizedClient { get; set; }
    public ApplicationUser AuthorizedUser { get; set; } = null!;


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



        UnathorizedClient = CreateClient();
        AuthorizedClient = CreateClient();
        var response = await AuthorizedClient.PostAsJsonAsync("login", new
        {
            email = "normal-user@bettracker.com",
            password = "!@#123ASDfgh"
        });
        var responseData = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        AuthorizedClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {responseData.AccessToken}");
    }


    Task IAsyncLifetime.DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    private async Task CreateDefaultUsers(BetTrackerIdentityDbContext identityDbContext)
    {
        AuthorizedUser = new ApplicationUser
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

        await identityDbContext.AddAsync(AuthorizedUser);
        await identityDbContext.SaveChangesAsync();
    }
}



public record LoginResponseDto(string TokenType, string AccessToken, int ExpiresIn, string RefreshToken)
{

}