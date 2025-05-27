using System.Collections.Generic;

namespace ConnectorLib.JSON;

/// <summary>Extensions for <see cref="IEnumerable{T}"/>.</summary>
// ReSharper disable once InconsistentNaming
internal static class IEnumerableEx
{
    /// <summary>Converts an <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{K, V}"/> to a <see cref="Dictionary{K, V}"/>.</summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="values">The <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{K, V}"/> to convert.</param>
    /// <returns>A <see cref="Dictionary{K, V}"/> containing the key-value pairs from the input.</returns>
    internal static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> values)
        where TKey : notnull
    {
        Dictionary<TKey, TValue> result = new();
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var val in values) result.Add(val.Key, val.Value);
        return result;
    }
}