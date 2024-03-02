using Domain.Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Net.Http.Json;
using Testcontainers.MsSql;


namespace BetTracker.Integration.Tests.Factories;

public class BaseIntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();
    public required HttpClient AuthorizedClient { get; set; }
    public required HttpClient UnathorizedClient { get; set; }
    public ApplicationUser AuthorizedUser { get; set; } = null!;
    public ApplicationUser OtherUser { get; set; } = null!;
    protected BetTrackerDbContext dbContext { get; set; } = null!;


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                 .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BetTrackerDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            var connectionString = _dbContainer.GetConnectionString();
            services.AddDbContext<BetTrackerDbContext>(options =>
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
        dbContext = scopedServices.GetRequiredService<BetTrackerDbContext>();

        await dbContext.Database.MigrateAsync();
        await CreateDefaultUsers();

        UnathorizedClient = CreateClient();
        AuthorizedClient = CreateClient();
        var response = await AuthorizedClient.PostAsJsonAsync("login", new
        {
            email = "normal-user@bettracker.com",
            password = "!@#123ASDfgh"
        });
        var responseData = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        AuthorizedClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {responseData.AccessToken}");

        await SeedDefaultData();
    }


    Task IAsyncLifetime.DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    private async Task CreateDefaultUsers()
    {
        AuthorizedUser = CreateUser("normal-user@bettracker.com");
        OtherUser = CreateUser("oter-user@bettracker.com");

        await dbContext.AddAsync(OtherUser);
        await dbContext.AddAsync(AuthorizedUser);
        await dbContext.SaveChangesAsync();
    }

    public virtual Task SeedDefaultData()
    {
        //should implement this method in the derived class
        return Task.CompletedTask;
    }

    private ApplicationUser CreateUser(string email)
    {
        return new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = email,
            NormalizedUserName = email.ToUpper(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEFVY8MnAtT3leUN1A4SGJju7PJ5Gj9Dl4K5vR75I5w+y+gxTjZ3iMyfVTRTGRY3KOg==",
            SecurityStamp = "WIPG27DGPLSQ5BCBCJ362ND5B6JTBL3M",
            ConcurrencyStamp = "c3b833f0-f7e9-481b-b904-fd0c86c8844d",
        };
    }
}



public record LoginResponseDto(string TokenType, string AccessToken, int ExpiresIn, string RefreshToken)
{

}