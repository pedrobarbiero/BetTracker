using Domain.Common;
using Domain.Enums;

namespace Domain;

public class BettingMarket : BaseEntity
{
    public Sport Sport { get; set; }

    //Todo: add owner/user
}
