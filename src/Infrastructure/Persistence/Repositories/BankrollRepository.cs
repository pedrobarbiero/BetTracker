using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Domain.Models;

namespace Persistence.Repositories;

public class BankrollRepository : EntityUserRepository<Bankroll>, IBankrollRepository
{
    public BankrollRepository(BetTrackerDbContext dbContext, IUserProvider userProvider) : base(dbContext, userProvider)
    {
    }
}
