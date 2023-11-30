using Domain.Common;
using Domain.Enums;

namespace Domain;

public class Bankroll : BaseEntity
{
    public required string Name { get; set; }
    public decimal InitialBalance { get; set; }
    public Currency CurrentBalance { get; set; }
    public DateOnly StartedAt { get; set; }
    public decimal StandardUnit { get; set; }
    public virtual ICollection<Bet> Bets { get; } = [];
}