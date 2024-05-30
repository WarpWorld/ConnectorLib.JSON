using System.Collections.Generic;

namespace ConnectorLib.JSON;

internal static class IEnumerableEx
{
    public static Dictionary<K, V> ToDictionary<K, V>(this IEnumerable<KeyValuePair<K, V>> values) where K : notnull
    {
        Dictionary<K, V> result = new();
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var val in values) result.Add(val.Key, val.Value);
        return result;
    }
}