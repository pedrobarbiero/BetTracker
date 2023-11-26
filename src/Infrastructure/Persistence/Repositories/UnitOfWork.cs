
using Application.Contracts.Persistence;

namespace Persistence.Repositories;

public sealed class UnitOfWork(BetTrackerDbContext dbContext) : IUnitOfWork
{
    private readonly BetTrackerDbContext _dbContext = dbContext;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
