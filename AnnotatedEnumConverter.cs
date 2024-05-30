using System;
using System.Collections.Generic;
using System.Linq;
#if !NET35
using System.Reflection;
#endif
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

internal class AnnotatedEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    [AttributeUsage(AttributeTargets.Field)]
    public class JsonValueAttribute(string value) : Attribute
    {
        public readonly string Value = value;
    }

    private static readonly Dictionary<T, JsonValueAttribute?> _attributes;

    static AnnotatedEnumConverter()
    {
        T[] members = (T[])Enum.GetValues(typeof(T));
        _attributes = members.Select(m => new KeyValuePair<T, JsonValueAttribute?>(m, GetAttributeOfType<JsonValueAttribute>(m))).ToDictionary();
    }

    private static T? GetAttributeOfType<T>(Enum enumVal) where T : Attribute
    {
        var type = enumVal.GetType();
#if NET35
        var memInfo = type.GetMembers().First(m => string.Equals(m.Name, enumVal.ToString(), StringComparison.Ordinal));
#else
        var memInfo = type.GetTypeInfo().DeclaredMembers.First(m => string.Equals(m.Name, enumVal.ToString(), StringComparison.Ordinal));
#endif
        var attributes = memInfo.GetCustomAttributes(typeof(T), false).ToArray();
        return (attributes.Length > 0) ? (T)attributes[0] : null;
    }

    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    {
        JsonValueAttribute? attribute = _attributes[value];
        if (attribute != null)
        {
            serializer.Serialize(writer, attribute.Value);
            return;
        }
        serializer.Serialize(writer, Enum.GetName(typeof(T), value));
    }

    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string? value = serializer.Deserialize<string>(reader);
        if (value == null) return default;
        var kvp = _attributes.Cast<KeyValuePair<T, JsonValueAttribute?>?>().FirstOrDefault(a => a?.Value?.Value?.Equals(value, StringComparison.OrdinalIgnoreCase) ?? false);
        if (kvp != null) return kvp.Value.Key;
#if NET35
        try
        {
            object v = Enum.Parse(typeof(T), value, true);
            return (T)v;
        }
        catch { return default; }
#else
        if (Enum.TryParse(value, true, out T v)) return v;
        return default;
#endif
    }
}