using Domain.Enums;
using Domain;

namespace Application.Dtos.Bet;

public record CreateBetDto
{
    public required BetId Id { get; set; } // Todo: create an Id generator
    public required BankrollId BrankroolId { get; set; }
    public TipsterId? TipsterId { get; set; }
    public required BettingMarketId BettingMarketId { get; set; }
    public bool PreMatch { get; set; }
    public bool Settled { get; set; }
    public string? Description { get; set; }
    public decimal Odd { get; set; }
    public decimal TotalStake { get; set; }
    public decimal TotalReturn { get; set; }
    public decimal PotentialReturn { get; set; }
    public decimal ExpectedYield { get; set; }
    public bool IsMultiple { get; set; }
    public BetResult Result { get; set; }
}
