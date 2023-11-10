using Domain.Common;

namespace Domain;
public class Tipster : BaseEntity<TipsterId>
{
    public required string Name { get; set; }
    public virtual ICollection<Bet> Bets { get; }
}

public record TipsterId(Guid Value);
