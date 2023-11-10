using Domain.Common;

namespace Application.Persistence.Contracts;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    Task<bool> DeleteAsync(Guid id);
}
