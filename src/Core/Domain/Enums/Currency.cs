using System.ComponentModel;

namespace Domain.Enums;

public enum Currency
{
    [Description("Great British Pound")]
    GBP = 1,
    [Description("US Dollar")]
    USD = 2,
    [Description("Euro")]
    EUR = 3,
    [Description("Brazilian Real")]
    BRL = 4
}
