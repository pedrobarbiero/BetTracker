namespace Application.Dtos;

public class BaseDto<IdType> where IdType : class
{
    public required IdType Id { get; set; }
}
