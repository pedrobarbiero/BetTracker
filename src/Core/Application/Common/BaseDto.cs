namespace Application.Common;

public record BaseDto<IdType> where IdType : class
{
    public required IdType Id { get; set; }
}
