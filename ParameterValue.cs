using Newtonsoft.Json;

namespace ConnectorLib.JSON;

public class ParameterValue<T> : ParameterBase, IParameterValue
{
    [JsonIgnore]
    string IParameterValue.ID => ID;

    [JsonIgnore]
    string IParameterValue.Name => Name;

    [JsonIgnore]
    ParameterType IParameterValue.Type => Type;

    /// <summary>
    /// The parameter value.
    /// </summary>
    [JsonProperty(PropertyName = "value")]
    public T? Value;

    [JsonConstructor]
    public ParameterValue(string name, string id, T? value) : base(name, id, ParameterType.Options)
        => Value = value;

    [JsonIgnore]
    object? IParameterValue.Value => Value;

    public override string ToString() => Name;
}
