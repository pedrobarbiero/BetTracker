using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories;

public class BetRespository : GenericRepository<Bet>, IBetRepository
{
    public BetRespository(BetTrackerDbContext dbContext) : base(dbContext)
    {
    }
}
