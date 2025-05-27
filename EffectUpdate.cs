using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>Represents an update to the status of an effect type.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class EffectUpdate : SimpleJSONResponse
{
    /// <summary>ID of the effect, if applicable.</summary>
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

    /// <summary>The type of object this report pertains to.</summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [JsonConverter(typeof(CamelCaseStringEnumConverter))]
    public enum IdentifierType
    {
        Effect = 0,
        Group = 1,
        Category = 2
    }
    
    /// <summary>The status of the effect.</summary>
    public EffectStatus status;

    /// <summary>A message, if applicable.</summary>
    public string? message;

    /// <summary>Creates a new instance of the <see cref="EffectUpdate"/> class.</summary>
    public EffectUpdate() { }

    /// <inheritdoc cref="EffectUpdate()"/>
    /// <param name="code">The ID of the effect.</param>
    /// <param name="status">The status of the effect.</param>
    /// <param name="message">A message, if applicable.</param>
    public EffectUpdate(string code, EffectStatus status, string? message = null)
    {
        ids = [code];
        idType = IdentifierType.Effect;
        this.status = status;
        this.message = message;
        type = ResponseType.EffectStatus;
    }

    /// <inheritdoc cref="EffectUpdate()"/>
    /// <param name="ids">The IDs of the effects.</param>
    /// <param name="status">The status of the effect.</param>
    /// <param name="message">A message, if applicable.</param>
    public EffectUpdate(string[] ids, EffectStatus status, string? message = null)
    {
        this.ids = ids;
        idType = IdentifierType.Effect;
        this.status = status;
        this.message = message;
        type = ResponseType.EffectStatus;
    }

    /// <inheritdoc cref="EffectUpdate()"/>
    /// <param name="ids">The IDs of the effects.</param>
    /// <param name="status">The status of the effect.</param>
    /// <param name="message">A message, if applicable.</param>
    public EffectUpdate(IEnumerable<string> ids, EffectStatus status, string? message = null)
    {
        this.ids = ids.ToArray();
        idType = IdentifierType.Effect;
        this.status = status;
        this.message = message;
        type = ResponseType.EffectStatus;
    }
}