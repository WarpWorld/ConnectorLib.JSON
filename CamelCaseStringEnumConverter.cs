using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

public class CamelCaseStringEnumConverter : JsonConverter<Enum>
{
    public override void WriteJson(JsonWriter writer, Enum? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        serializer.Serialize(writer, value.ToCamelCase());
    }

    public override Enum ReadJson(JsonReader reader, Type objectType, Enum? existingValue, bool hasExistingValue, JsonSerializer serializer)
        => ReadJToken(JToken.ReadFrom(reader), objectType);

    public static Enum ReadJToken(JToken reader, Type objectType)
    {
        switch (reader.Type)
        {
            case JTokenType.String:
            {
                string? value = reader.Value<string>();
                if (value == null) throw new SerializationException("The value was null.");
#if NETSTANDARD2_1_OR_GREATER
                if (Enum.TryParse(objectType, value, true, out var result)) return (Enum)result;
                throw new SerializationException("The value was not recognized.");
#else
                try
                {
                    var result = Enum.Parse(objectType, value, true);
                    return (Enum)result;
                }
                catch (Exception) { throw new SerializationException("The value was not recognized."); }
#endif
            }
            case JTokenType.Integer:
            {
                int? value = reader.Value<int>();
                if (value == null) throw new SerializationException("The value was null.");
                return (Enum)Enum.ToObject(objectType, value.Value);
            }
            default:
                throw new SerializationException("The value was not a string or number.");
        }
    }
}