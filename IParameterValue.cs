namespace ConnectorLib.JSON;

/// <summary>Interface for request parameter values.</summary>
public interface IParameterValue
{
    /// <summary>The identifier of the parameter.</summary>
    string ID { get; }
    
    /// <summary>The name of the parameter.</summary>
    string Name { get; }
    
    /// <summary>The type of parameter that this is.</summary>
    ParameterBase.ParameterType Type { get; }
    
    /// <summary>The value of the parameter.</summary>
    object? Value { get; }
}