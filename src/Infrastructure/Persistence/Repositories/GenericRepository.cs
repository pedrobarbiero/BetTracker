using Application.Common;
using Application.Contracts.Persistence;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : BaseEntity
{
    private readonly BetTrackerDbContext _dbContext;
    public GenericRepository(BetTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var count = await _dbContext.Set<T>().Where(t => t.Id == id).ExecuteDeleteAsync();
        return count > 0;
    }

    public Task<T?> GetByIdAsync(Guid id)
    {
        return _dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task<PagedResult<T>> GetPagedAsync(uint page, uint pageSize)
    {
        var data = await _dbContext.Set<T>()
            .Skip((int)((page - 1) * pageSize))
            .Take((int)pageSize + 1) // take one more than the page size to determine if there is a next page avoing a count query
            .OrderByDescending(t => t.CreatedDate)
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
        _dbContext.Set<T>().Update(entity);
    }
}
