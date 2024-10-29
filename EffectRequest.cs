#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class EffectRequest : SimpleJSONRequest
{
    public string? code;
    public string? message;
    public JToken? parameters;
    public uint? quantity;
    //public Target?[]? targets;
    public JArray? targets;
    public long? duration; //milliseconds
    public string? viewer;
    public JArray? viewers;
    public long? cost;
    public Guid? requestID;
    public JObject? sourceDetails;

    public EffectRequest() => type = RequestType.EffectStart;

#if NETSTANDARD1_3_OR_GREATER
    [Serializable]
#endif
    public class Target
    {
        public string? service;
        public string? id;
        public string? name;
        public string? avatar;
    }
}