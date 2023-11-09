using System.ComponentModel;

namespace Domain.Enums;

public enum Currency
{
    [Description("US Dollar")]
    USD = 1,
    [Description("Euro")]
    EUR = 2,
    [Description("British Pound")]
    GBP = 3,
    [Description("Brazilian Real")]
    BRL = 4
}
