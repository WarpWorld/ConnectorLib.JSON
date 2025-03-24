using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class EffectUpdate : SimpleJSONResponse
{
    /// <remarks>
    /// If the message is about a class of effect (0x80 series), this should contain the effect safename.
    /// Otherwise, this field should be null or missing.
    /// </remarks>
    [Obsolete($"This field is deprecated. Please use the {nameof(ids)} field instead.")]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? code;

    /// <summary>
    /// The IDs of the effects, groups, or categories to which this report pertains.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string[]? ids;

    /// <summary>
    /// The type of the ID specified.
    /// </summary>
    public IdentifierType idType;

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [JsonConverter(typeof(CamelCaseStringEnumConverter))]
    public enum IdentifierType
    {
        Effect = 0,
        Group = 1,
        Category = 2
    }

    public EffectStatus status;

    public string? message;

    public EffectUpdate() { }

    public EffectUpdate(string code, EffectStatus status, string? message = null)
    {
        ids = [code];
        idType = IdentifierType.Effect;
        this.status = status;
        this.message = message;
        type = ResponseType.EffectStatus;
    }

    public EffectUpdate(string[] ids, EffectStatus status, string? message = null)
    {
        this.ids = ids;
        idType = IdentifierType.Effect;
        this.status = status;
        this.message = message;
        type = ResponseType.EffectStatus;
    }

    public EffectUpdate(IEnumerable<string> ids, EffectStatus status, string? message = null)
    {
        this.ids = ids.ToArray();
        idType = IdentifierType.Effect;
        this.status = status;
        this.message = message;
        type = ResponseType.EffectStatus;
    }
}