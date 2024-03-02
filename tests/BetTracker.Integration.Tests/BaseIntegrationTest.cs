using BetTracker.Integration.Tests.Factories;
using Domain.Models.Identity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace BetTracker.Integration.Tests;

public abstract class BaseIntegrationTest<T> : IClassFixture<T> where T: BaseIntegrationTestWebAppFactory
{
    protected readonly HttpClient unauthorizedClient;
    protected readonly HttpClient authorizedClient;
    protected readonly ApplicationUser authorizedUser;
    protected readonly ISender sender;
    protected readonly BetTrackerDbContext dbContext;
    protected readonly ApplicationUser otherUser;
    protected BaseIntegrationTest(BaseIntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();

        sender = scope.ServiceProvider.GetRequiredService<ISender>();
        dbContext = scope.ServiceProvider.GetRequiredService<BetTrackerDbContext>();

        unauthorizedClient = factory.UnathorizedClient;
        authorizedClient = factory.AuthorizedClient;
        authorizedUser = factory.AuthorizedUser;
        otherUser = factory.OtherUser;
    }

}
