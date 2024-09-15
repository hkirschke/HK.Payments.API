namespace HK.Payments.Core.Helpers;

public static class EnumHelper
{
    public static int GetEnumIntValue<T>(this T value) where T : Enum
    {
        if (!typeof(T).IsEnum)
            return 0;

        return Convert.ToInt32(value);
    }

    public static bool IsDefined<T>(this int value) where T : Enum
    {
        var isNotDefined = Enum.IsDefined(typeof(T), value);

        return isNotDefined;
    }
}
