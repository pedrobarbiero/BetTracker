using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace BetTracker.Integration.Tests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;

    protected readonly HttpClient client;
    protected readonly ISender sender;
    protected readonly BetTrackerDbContext betTrackerDbContext;
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        betTrackerDbContext = _scope.ServiceProvider.GetRequiredService<BetTrackerDbContext>();

        client = factory.CreateClient();
    }

}
