using Application.Common;
using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Domain.Common;
using Domain.Models;
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
            .Where(t => t.Id == id && t.ApplicationUserId == userId)
            .ExecuteDeleteAsync();
        return count > 0;
    }

    public Task<T?> GetByIdAsync(Guid id)
    {
        return _dbContext.Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id && t.ApplicationUserId == GetCurrentUserId());
    }

    public async Task<PagedResult<T>> GetPagedAsync(uint page, uint pageSize)
    {
        var data = await _dbContext.Set<T>()
            .Where(t => t.ApplicationUserId == GetCurrentUserId())
            .OrderByDescending(t => t.CreatedDate)
            .Skip((int)((page - 1) * pageSize))
            .Take((int)pageSize + 1) // take one more than the page size to determine if there is a next page avoing a count query
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
        entity.ApplicationUserId = GetCurrentUserId();
        _dbContext.Set<T>().Update(entity);
    }

    public Task<bool> Exists(Guid id)
    {
        return _dbContext.Set<T>().AnyAsync(b => b.Id == id && b.ApplicationUserId == GetCurrentUserId());
    }
}
