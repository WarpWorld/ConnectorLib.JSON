using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

[Serializable]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GenericEvent : SimpleJSONResponse
{
    [JsonProperty(PropertyName = "internal")] public bool @internal;
    public string eventType;
    public Dictionary<string, string> data;

    public GenericEvent() => type = ResponseType.GenericEvent;
}