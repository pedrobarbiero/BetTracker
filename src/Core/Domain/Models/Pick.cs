using Domain.Common;
using Domain.Enums;

namespace Domain.Models;

public class Pick : BaseEntity
{
    public required Guid BetId { get; set; }
    public virtual required Bet Bet { get; set; }
    //public required Sport Sport { get; set; }
    public decimal Odd { get; set; }
}