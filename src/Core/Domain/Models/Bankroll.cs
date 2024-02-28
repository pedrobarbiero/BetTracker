using Domain.Common;
using Domain.Enums;

namespace Domain.Models;

public class Bankroll : BaseEntity
{
    public required string Name { get; set; }
    public decimal InitialBalance { get; set; } = 0.0m;
    public decimal CurrentBalance { get; set; } = 0.0m;
    public Currency Currency { get; set; } = Currency.GBP;
    public DateOnly StartedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public decimal StandardUnit { get; set; } = 1.0m;
    public virtual ICollection<Bet> Bets { get; } = [];
}