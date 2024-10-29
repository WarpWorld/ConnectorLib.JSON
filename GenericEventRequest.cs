#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GenericEventRequest : SimpleJSONRequest
{
    [JsonProperty(PropertyName = "internal")] public bool @internal;
    public string eventType;
    public Dictionary<string, object> data;

    public GenericEventRequest() => type = RequestType.GenericEvent;
}