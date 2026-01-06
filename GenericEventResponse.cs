#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

/// <summary>Generic event call for any event that is not defined in the API.</summary>
/// <remarks>
/// This is called a "response" because messages sent to the client are called responses but this message is not sent in response <b>to</b> anything.
/// It simply represents a generic event that is not defined in the API.
/// </remarks>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GenericEventResponse : SimpleJSONResponse
{
    /// <summary>Indicates that this event is locally-generated.</summary>
    [JsonProperty(PropertyName = "internal")] public bool @internal;
    
    /// <summary>The type of event.</summary>
    public string eventType;
    
    /// <summary>The data associated with the event.</summary>
    public Dictionary<string, object?>? data;

    /// <summary>Creates a new instance of the <see cref="GenericEventResponse"/> class.</summary>
    /// <param name="eventType">The type of event.</param>
    public GenericEventResponse(string eventType)
    {
        type = ResponseType.GenericEvent;
        this.eventType = eventType;
    }

    /// <inheritdoc cref="GenericEventResponse(string)"/>
    /// <param name="data">The data associated with the event.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public GenericEventResponse(string eventType, IEnumerable<KeyValuePair<string, object>>? data, bool @internal = false) : this(eventType)
    {
        this.data = data?.ToDictionary();
        this.@internal = @internal;
    }

    /// <inheritdoc cref="GenericEventResponse(string, IEnumerable{KeyValuePair{string, object}}?)"/>
    /// <param name="internal">Indicates that this event is locally-generated.</param>
    [JsonConstructor]
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public GenericEventResponse(string eventType, Dictionary<string, object>? data, [JsonProperty(PropertyName = "internal")] bool @internal) : this(eventType, data)
        => this.@internal = @internal;
}