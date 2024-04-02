using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

[Serializable]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class EffectUpdate : SimpleJSONResponse
{
    /// <remarks>
    /// If the message is about a class of effect (0x80 series), this should contain the effect safename.
    /// Otherwise, this field should be null or missing.
    /// </remarks>
    [Obsolete]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? code;

    /// <summary>
    /// The ID of the effect to which this report pertains.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string[]? ids;

    /// <summary>
    /// The type of the ID specified.
    /// </summary>
    public IdentifierType idType;

    public enum IdentifierType
    {
        Effect = 0,
        Group = 1,
        Category = 2
    }

    public EffectStatus status;

    public string? message;
}
