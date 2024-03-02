using Domain.Models;

namespace BetTracker.Integration.Tests.Factories;

public class SportTestWebAppFactory : BaseIntegrationTestWebAppFactory
{
    public SportTestWebAppFactory()
    {
    }

    public override async Task SeedDefaultData()
    {
        var sports = new List<Sport>
        {
            new() { Id = Guid.NewGuid(), Name = "Test Sport 1", Slug = "test-sport-1", ApplicationUserId=AuthorizedUser.Id },
            new() { Id = Guid.NewGuid(), Name = "Test Sport 2", Slug = "test-sport-2", ApplicationUserId=AuthorizedUser.Id },
            new() { Id = Guid.NewGuid(), Name = "Test Sport 3", Slug = "test-sport-3", ApplicationUserId=AuthorizedUser.Id },
            new() { Id = Guid.NewGuid(), Name = "Test Sport 5", Slug = "test-sport-5", ApplicationUserId=OtherUser.Id },
        };

        await dbContext.Sports.AddRangeAsync(sports);
        await dbContext.SaveChangesAsync();
    }
}
