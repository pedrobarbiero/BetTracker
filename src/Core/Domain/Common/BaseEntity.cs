namespace Domain.Common;

public abstract class BaseEntity<T> where T : class
{
    public required T Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}
