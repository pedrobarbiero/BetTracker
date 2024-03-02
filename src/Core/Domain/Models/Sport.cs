using Domain.Common;

namespace Domain.Models;

public class Sport : BaseEntityUser
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
}
