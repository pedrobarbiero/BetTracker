using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Domain.Models;

namespace Persistence.Repositories;

public class BetRespository : EntityUserRepository<Bet>, IBetRepository
{
    public BetRespository(BetTrackerDbContext dbContext, IUserProvider userProvider) : base(dbContext, userProvider)
    {
    }
}
