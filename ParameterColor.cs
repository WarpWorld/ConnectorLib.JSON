using System;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

public class ParameterColor : ParameterBase, IParameterValue
{
    [JsonIgnore]
    string IParameterValue.ID => ID;

    [JsonIgnore]
    string IParameterValue.Name => Name;

    [JsonIgnore]
    ParameterType IParameterValue.Type => Type;

    /// <summary>
    /// The color value.
    /// </summary>
    [JsonProperty(PropertyName = "value"), JsonConverter(typeof(HexColorConverter))]
    public ParameterColorValue Value;

    [JsonConstructor]
    public ParameterColor(string name, string id, ParameterColorValue value) : base(name, id, ParameterType.HexColor)
        => Value = value;

    [JsonConstructor]
    public ParameterColor(string name, string id, string value) : base(name, id, ParameterType.HexColor)
    {
        if (!HexColorConverter.TryParse(value, out Value)) throw new ArgumentException("Unknown color code.", nameof(value));
    }

    [JsonIgnore]
    object? IParameterValue.Value => Value;
}
