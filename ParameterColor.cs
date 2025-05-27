using System;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>A parameter that holds a color value.</summary>
public class ParameterColor : ParameterBase, IParameterValue
{
    [JsonIgnore]
    string IParameterValue.ID => ID;

    [JsonIgnore]
    string IParameterValue.Name => Name;

    [JsonIgnore]
    ParameterType IParameterValue.Type => Type;

    [JsonIgnore]
    object? IParameterValue.Value => Value;

    /// <summary>The color value.</summary>
    [JsonProperty(PropertyName = "value"), JsonConverter(typeof(HexColorConverter))]
    public ParameterColorValue Value;

    /// <summary>Creates a new instance of the <see cref="ParameterColor"/> class.</summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="id">The identifier of the parameter.</param>
    /// <param name="value">The color value.</param>
    [JsonConstructor]
    public ParameterColor(string name, string id, ParameterColorValue value) : base(name, id, ParameterType.HexColor)
        => Value = value;

    /// <inheritdoc cref="ParameterColor(string, string, ParameterColorValue)"/>
    [JsonConstructor]
    public ParameterColor(string name, string id, string value) : base(name, id, ParameterType.HexColor)
    {
        if (!HexColorConverter.TryParse(value, out Value)) throw new ArgumentException("Unknown color code.", nameof(value));
    }
}
