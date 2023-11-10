using Domain.Common;
using Domain.Enums;

namespace Domain;

public class BettingMarket : BaseEntity<BettingMarketId>
{
    public Sport Sport { get; set; }

    //Todo: add owner/user
}

public record BettingMarketId(Guid Value);
