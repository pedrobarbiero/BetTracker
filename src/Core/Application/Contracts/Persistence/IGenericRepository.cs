using Domain.Common;

namespace Application.Contracts.Persistence;

public interface IGenericRepository<T, IdType> where T : BaseEntity<IdType> where IdType : class
{
    Task<T?> GetByIdAsync(IdType id);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    Task<bool> DeleteAsync(IdType id);
    Task<IEnumerable<T>> GetPagedAsync(uint page, uint pageSize);
}
