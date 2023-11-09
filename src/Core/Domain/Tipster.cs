using Domain.Common;

namespace Domain;
public class Tipster : BaseEntity
{
    public required string Name { get; set; }
    public virtual ICollection<Bet> Bets { get; }
}

