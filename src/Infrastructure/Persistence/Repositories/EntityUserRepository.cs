using Application.Common;
using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class EntityUserRepository<T> : IEntityUserRepository<T>
    where T : BaseEntityUser
{
    protected readonly BetTrackerDbContext _dbContext;
    protected readonly IUserProvider _userProvider;

    public EntityUserRepository(BetTrackerDbContext dbContext, IUserProvider userProvider)
    {
        _dbContext = dbContext;
        _userProvider = userProvider;
    }

    private Guid GetCurrentUserId() => _userProvider.GetCurrentUserId() ?? throw new UnauthorizedAccessException("Current User not found");

    public async Task<T> AddAsync(T entity)
    {
        entity.ApplicationUserId = GetCurrentUserId();
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var userId = GetCurrentUserId();
        var count = await _dbContext.Set<T>()
            .Where(t => t.ApplicationUserId == GetCurrentUserId())
            .ExecuteDeleteAsync();
        return count > 0;
    }

    public Task<T?> GetByIdAsync(Guid id)
    {
        return _dbContext.Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id && (t.ApplicationUserId == GetCurrentUserId() || t.ApplicationUserId == Domain.Constants.Users.JokerId));
    }

    public async Task<PagedResult<T>> GetPagedAsync(uint page, uint pageSize)
    {
        var skip = (int)((page - 1) * pageSize);
        var take = (int)pageSize + 1;
        var data = await _dbContext.Set<T>()
            .AsNoTracking()
            .Where(t => t.ApplicationUserId == GetCurrentUserId() || t.ApplicationUserId == Domain.Constants.Users.JokerId)
            .OrderByDescending(t => t.CreatedDate)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return new PagedResult<T>()
        {
            Items = data.Take((int)pageSize),
            HasNextPage = data.Count > pageSize,
            Page = page,
            PageSize = pageSize
        };
    }

    public void Update(T entity)
    {
        if (entity.ApplicationUserId == Domain.Constants.Users.JokerId)
            throw new UnauthorizedAccessException("User not authorized to update default entities");

        entity.ApplicationUserId = GetCurrentUserId();
        _dbContext.Set<T>().Update(entity);
    }

    public Task<bool> Exists(Guid id)
    {
        return _dbContext.Set<T>().AnyAsync(t => t.Id == id && (t.ApplicationUserId == GetCurrentUserId() || t.ApplicationUserId == Domain.Constants.Users.JokerId));
    }
}
