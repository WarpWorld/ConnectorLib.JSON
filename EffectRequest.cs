using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

[Serializable]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class EffectRequest : SimpleJSONRequest
{
    public string? code;
    public string? message;
    public object? parameters;
    public uint? quantity;
    //public Target?[]? targets;
    public JArray? targets;
    public long? duration; //milliseconds
    public string? viewer;
    public JArray? viewers;
    public int? cost;

    public EffectRequest() => type = RequestType.Start;

    [Serializable]
    public class Target
    {
        public string? service;
        public string? id;
        public string? name;
        public string? avatar;
    }
}