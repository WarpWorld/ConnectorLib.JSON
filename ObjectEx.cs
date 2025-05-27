using System;

namespace ConnectorLib.JSON;

/// <summary>Extensions for the <see cref="object"/> class.</summary>
internal static class ObjectEx
{
    /// <summary>Converts a nullable object to a nullable type using the provided selector function.</summary>
    /// <typeparam name="T1">The type of the input object.</typeparam>
    /// <typeparam name="T2">The type of the output object.</typeparam>
    /// <param name="value">The input object.</param>
    /// <param name="selector">The function to convert the input object to the output type.</param>
    /// <returns>The converted object, or null if the input object is null.</returns>
    internal static T2? IfNotNull<T1, T2>(this T1? value, Func<T1, T2?> selector)
        where T2 : class =>
        value != null ? selector(value) : null;
}