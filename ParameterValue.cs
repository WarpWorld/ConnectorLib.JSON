using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary> A parameter value that holds a value of type <typeparamref name="TValue"/>.</summary>
/// <typeparam name="TValue">The type of the value that this parameter holds. This can be any serializable type.</typeparam>
public class ParameterValue<TValue> : ParameterBase, IParameterValue
{
    [JsonIgnore]
    string IParameterValue.ID => ID;

    [JsonIgnore]
    string IParameterValue.Name => Name;

    [JsonIgnore]
    ParameterType IParameterValue.Type => Type;

    [JsonIgnore]
    object? IParameterValue.Value => Value;

    /// <summary>The parameter value.</summary>
    [JsonProperty(PropertyName = "value")]
    public TValue? Value;

    /// <summary>Creates a new instance of the <see cref="ParameterValue{TValue}"/> class.</summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="id">The identifier of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    [JsonConstructor]
    public ParameterValue(string name, string id, TValue? value) : base(name, id, ParameterType.Options)
        => Value = value;

    public override string ToString() => Name;
}
