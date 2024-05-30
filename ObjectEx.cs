using System;

namespace ConnectorLib.JSON;

internal static class ObjectEx
{
    public static T2? IfNotNull<T1, T2>(this T1? value, Func<T1, T2?> selector)
        where T2 : class =>
        value != null ? selector(value) : null;
}