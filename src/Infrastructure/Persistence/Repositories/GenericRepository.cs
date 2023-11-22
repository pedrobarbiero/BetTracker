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

    public Task<IEnumerable<T>> GetPagedAsync(uint page, uint pageSize)
    {
        //Todo: implement
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }
}
