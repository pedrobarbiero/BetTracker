using Application.Common;
using Domain.Common;

namespace Application.Contracts.Persistence;

public interface IEntityUserRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    Task<bool> DeleteAsync(Guid id);
    Task<PagedResult<T>> GetPagedAsync(uint page, uint pageSize);
    Task<bool> Exists(Guid id);
}
