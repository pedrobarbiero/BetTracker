using Application.Contracts.Persistence;
using Domain.Models;

namespace Persistence.Repositories;

public class BankrollRepository : GenericRepository<Bankroll>, IBankrollRepository
{
    public BankrollRepository(BetTrackerDbContext dbContext) : base(dbContext)
    {
    }
}
