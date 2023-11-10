using Domain.Common;

namespace Application.Persistence.Contracts;

public interface IGenericRepository<T, Id> where T : BaseEntity<Id>
{
    Task<T?> GetByIdAsync(Id id);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    Task<bool> DeleteAsync(Id id);
}
