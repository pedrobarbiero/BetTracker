using Domain.Common;

namespace Domain.Models;
public class Tipster : BaseEntity
{
    public required string Name { get; set; }
    public virtual ICollection<Bet> Bets { get; }
}
