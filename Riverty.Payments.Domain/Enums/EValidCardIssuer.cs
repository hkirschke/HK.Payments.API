using System.ComponentModel;

namespace HK.Payments.Core.Enums;

public enum EValidCardIssuer
{
    [Description("American Express")]
    AmericanExpress = 0,
    [Description("MasterCard")]
    MasterCard = 9,
    [Description("Visa")]
    Visa = 13,
    [Description("Unknown")]
    None = 14
}