using Domain.Common;
using Domain.Enums;
using Domain.Models.Identity;

namespace Domain.Models;

public class Bankroll : BaseEntity
{
    public required string Name { get; set; }
    public required Guid UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}