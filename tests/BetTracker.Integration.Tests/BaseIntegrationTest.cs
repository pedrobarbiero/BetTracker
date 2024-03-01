using Domain.Models.Identity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace BetTracker.Integration.Tests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly HttpClient unauthorizedClient;
    protected readonly HttpClient authorizedClient;
    protected readonly ApplicationUser authorizedUser;
    protected readonly ISender sender;
    protected readonly BetTrackerDbContext betTrackerDbContext;
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();

        sender = scope.ServiceProvider.GetRequiredService<ISender>();
        betTrackerDbContext = scope.ServiceProvider.GetRequiredService<BetTrackerDbContext>();

        unauthorizedClient = factory.UnathorizedClient;
        authorizedClient = factory.AuthorizedClient;
        authorizedUser = factory.AuthorizedUser;
    }

}
