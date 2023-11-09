using System.ComponentModel;

namespace Domain.Enums;

public enum BetResult
{
    [Description("Pending")]
    Pending = 0,
    [Description("Won")]
    Won = 1,
    [Description("Partially Won")]
    PartiallyWon = 2,
    [Description("Void")]
    Void = 3,
    [Description("Partially Lost")]
    PartiallyLost = 4,
    [Description("Lost")]
    Lost = 5
}
