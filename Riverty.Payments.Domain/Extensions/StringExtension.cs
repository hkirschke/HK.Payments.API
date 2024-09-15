using System.Text.RegularExpressions;

namespace Riverty.Payments.Core.Extensions;

public static partial class StringExtension
{
    [GeneratedRegex(@"^[a-zA-Z]+$")]
    private static partial Regex HasJustLetters();

    public static bool HasJustLetter(this string value)
    {
        bool hasJustLetters = HasJustLetters().IsMatch(value.Replace(" ", ""));

        return hasJustLetters;
    }

    public static bool HasJustNumbers(this string value)
    {
        var hasJustNumbers = Int64.TryParse(value, out _);

        return hasJustNumbers;
    }
}