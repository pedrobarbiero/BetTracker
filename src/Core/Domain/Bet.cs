using Domain.Common;
using Domain.Enums;

namespace Domain;

public class Bet : BaseEntity<BetId>
{
    public required BankrollId BrankroolId { get; set; }
    public virtual required Bankroll Bankroll { get; set; }
    public TipsterId? TipsterId { get; set; }
    public virtual Tipster? Tipster { get; set; } // null if the bet is not from a tipster
    public required BettingMarketId BettingMarketId { get; set; }
    public virtual required BettingMarket BettingMarket { get; set; }
    public bool PreMatch { get; set; }
    public bool Settled { get; set; }
    public string? Description { get; set; }
    public ICollection<Pick> Picks { get; }
    public decimal Odd => Picks.Aggregate(1m, (acc, pick) => acc * pick.Odd);
    public decimal TotalStake { get; set; } // total amount wagered
    public decimal TotalReturn { get; set; } // total amount returned
    public decimal PotentialReturn => TotalStake * Odd;
    public decimal ExpectedYield => (Odd - 1) * 100;
    public bool IsMultiple => Picks.Count > 1;
    public BetResult Result => CalculateResult();

    private BetResult CalculateResult()
    {
        if (!Settled)
        {
            return BetResult.Pending;
        }

        return TotalReturn switch
        {
            var totalReturn when totalReturn >= PotentialReturn => BetResult.Won,
            var totalReturn when totalReturn <= 0 => BetResult.Lost,
            var totalReturn when totalReturn > TotalStake && totalReturn < PotentialReturn => BetResult.PartiallyWon,
            var totalReturn when totalReturn > 0 && totalReturn < TotalStake => BetResult.PartiallyLost,
            var totalReturn when totalReturn == TotalStake => BetResult.Void,
            _ => throw new NotSupportedException("We can't define this bet result")
        };
    }

}

public record BetId(Guid Value);
//Todo: configure HasConversion in entityframework