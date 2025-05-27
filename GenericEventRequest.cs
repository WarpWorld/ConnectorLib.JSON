#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>Generic event call for any event that is not defined in the API.</summary>
/// <remarks>This is called a "request" because messages sent to the game are called requests but no response or return value is expected.</remarks>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GenericEventRequest : SimpleJSONRequest
{
    /// <summary>Indicates that this event is locally-generated.</summary>
    [JsonProperty(PropertyName = "internal")] public bool @internal;
    
    /// <summary>The type of event.</summary>
    public string eventType;

    /// <summary>The data associated with the event.</summary>
    public Dictionary<string, object>? data;

    /// <summary>Creates a new instance of the <see cref="GenericEventRequest"/> class.</summary>
    /// <param name="eventType">The type of event.</param>
    public GenericEventRequest(string eventType)
    {
        type = RequestType.GenericEvent;
        this.eventType = eventType;
    }

    /// <inheritdoc cref="GenericEventRequest(string)"/>
    /// <param name="data">The data associated with the event.</param>
    [JsonConstructor]
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public GenericEventRequest(string eventType, IEnumerable<KeyValuePair<string, object>>? data) : this(eventType)
        => this.data = data?.ToDictionary();

    /// <inheritdoc cref="GenericEventRequest(string, IEnumerable{KeyValuePair{string, object}}?)"/>
    /// <param name="internal">Indicates that this event is locally-generated.</param>
    [JsonConstructor]
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public GenericEventRequest(string eventType, IEnumerable<KeyValuePair<string, object>>? data, bool @internal) : this(eventType, data)
        => this.@internal = @internal;
}