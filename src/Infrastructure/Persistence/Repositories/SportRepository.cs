using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Domain.Models;

namespace Persistence.Repositories;

public class SportRepository : EntityUserRepository<Sport>, ISportRepository
{
    public SportRepository(BetTrackerDbContext dbContext, IUserProvider userProvider) : base(dbContext, userProvider)
    {
    }
}
