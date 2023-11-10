using Domain.Common;
using Domain.Enums;

namespace Domain;

public class Pick : BaseEntity<PickId>
{
    public required BetId BetId { get; set; }
    public virtual required Bet Bet { get; set; }
    public required Sport Sport { get; set; }
    public decimal Odd { get; set; }
}

public record PickId(Guid Value);
