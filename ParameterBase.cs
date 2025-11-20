using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

public abstract class ParameterBase
{
    /*
     *     parameters?: Record<string, {
      type: 'options' | 'hex-color'
      title: string
      value: string
    }>
     */
    /// <inheritdoc cref="IParameterValue.ID"/>
    [JsonIgnore]
    public readonly string ID;


    /// <inheritdoc cref="IParameterValue.Name"/>
    [JsonProperty(PropertyName = "title")]
    public readonly string Name;


    /// <inheritdoc cref="IParameterValue.Type"/>
    [JsonProperty(PropertyName = "type")]
    public readonly ParameterType Type;
    
    /// <summary>The type of parameter that this is.</summary>
    [JsonConverter(typeof(ParameterTypeConverter))]
    public enum ParameterType
    {
        //[AnnotatedEnumConverter<ParameterType>.JsonValueAttribute("options")]
        Options = 0, //this is the default if the value is missing
        //[AnnotatedEnumConverter<ParameterType>.JsonValueAttribute("hex-color")]
        HexColor
    }

    /// <summary>Converts <see cref="ParameterType"/> values to and from JSON.</summary>
    /// <remarks>This converter was introduced in order to remove AnnotatedEnumConverter from the assembly.</remarks>
    private class ParameterTypeConverter : JsonConverter<ParameterType>
    {
        public override void WriteJson(JsonWriter writer, ParameterType value, JsonSerializer serializer)
        {
            string str = value switch
            {
                ParameterType.Options => "options",
                ParameterType.HexColor => "hex-color",
                _ => throw new SerializationException("Unknown parameter type.")
            };
            writer.WriteValue(str);
        }
        public override ParameterType ReadJson(JsonReader reader, Type objectType, ParameterType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string? str = reader.Value?.ToString().ToLowerInvariant();
            switch (str)
            {
                case "options":
                    return ParameterType.Options;
                case "hexcolor":
                case "hex-color":
                    return ParameterType.HexColor;
                default:
                    throw new SerializationException("Unknown parameter type.");
            }
        }
    }

    /// <summary>Creates a new instance of the <see cref="ParameterBase"/> class.</summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="id">The identifier of the parameter.</param>
    /// <param name="type">The type of parameter that this is.</param>
    protected ParameterBase(string name, string id, ParameterType type)
    {
        ID = id;
        Name = name;
        Type = type;
    }

    /*public class Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(ParameterBase); //do not work for subtypes or this will get stuck in a recursion loop

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            serializer.Serialize(writer, value, value.GetType());
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject j = JObject.Load(reader);
            ParameterType type = j["type"]?.Value<ParameterType>() ?? ParameterType.Options;
            switch (type)
            {
                case ParameterType.Options:
                    return j.ToObject<ParameterValue<string>>();
                case ParameterType.HexColor:
                    return j.ToObject<ParameterColor>();
                default:
                    throw new SerializationException("Unknown parameter type.");
            }
        }
    }*/
}