namespace Domain.Common;

public abstract class BaseEntity
{
    public required Guid Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}
