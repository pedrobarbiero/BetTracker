using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories;

public class BetRespository : GenericRepository<Bet, BetId>, IBetRepository
{
    public BetRespository(BetTrackerDbContext dbContext) : base(dbContext)
    {
    }
}
