namespace Domain.Common;

public interface IBaseEntity
{
    DateTimeOffset CreatedDate { get; set; }
    DateTimeOffset? UpdatedDate { get; set; }
}

public abstract class BaseEntity<T> : IBaseEntity where T : class
{
    public T Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}

