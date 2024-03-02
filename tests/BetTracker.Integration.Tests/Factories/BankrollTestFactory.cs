using Domain.Models;

namespace BetTracker.Integration.Tests.Factories;

public class BankrollTestFactory : BaseIntegrationTestWebAppFactory
{
    public BankrollTestFactory()
    {
    }

    public override async Task SeedDefaultData()
    {
        var bankrolls = new List<Bankroll>
        {
            new () { Id = Guid.NewGuid(), Name = "Test Bankroll 1", ApplicationUserId = AuthorizedUser.Id },
            new () { Id = Guid.NewGuid(), Name = "Test Bankroll 2", ApplicationUserId = AuthorizedUser.Id },
            new () { Id = Guid.NewGuid(), Name = "Test Bankroll 3", ApplicationUserId = AuthorizedUser.Id },
            new () { Id = Guid.NewGuid(), Name = "Test Bankroll 2", ApplicationUserId = OtherUser.Id },
        };

        await dbContext.Bankrolls.AddRangeAsync(bankrolls);
        await dbContext.SaveChangesAsync();
    }
}
