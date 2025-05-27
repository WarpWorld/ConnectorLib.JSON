#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

/// <summary>Request to start an effect.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class EffectRequest : SimpleJSONRequest
{
    /// <summary>The ID of the effect to start.</summary>
    public string? code;
    
    /// <summary>A message to be displayed to the user, if applicable.</summary>
    public string? message;
    
    /// <summary>The parameters for the effect, if any.</summary>
    public JToken? parameters;
    
    /// <summary>The quantity of the effect, if applicable.</summary>
    public uint? quantity;
    
    /// <summary>
    /// The targets of the effect. This is an array of <see cref="Target"/> objects.
    /// </summary>
    //public Target?[]? targets;
    public JArray? targets;
    
    /// <summary>The duration of the effect in milliseconds. This is the time the effect should last.</summary>
    public long? duration; //milliseconds
    
    /// <summary>The display name of the viewer or viewers who triggered the effect.</summary>
    public string? viewer;
    
    /// <summary>The viewers who triggered the effect. This is an array of <see cref="Target"/> objects.</summary>
    public JArray? viewers;
    
    /// <summary>The cost of the effect in coins.</summary>
    public long? cost;
    
    /// <summary>The ID of the effect request.</summary>
    public Guid? requestID;

    /// <summary>The source details of the effect request. This provides additional information about the source of the effect.</summary>
    [JsonConverter(typeof(IEffectSourceDetails.Converter))]
    public IEffectSourceDetails? sourceDetails;

    /// <summary>Creates a new instance of the <see cref="EffectRequest"/> class.</summary>
    public EffectRequest() => type = RequestType.EffectStart;

    /// <summary>An object representing a viewer or effect target.</summary>
#if NETSTANDARD1_3_OR_GREATER
    [Serializable]
#endif
    public class Target
    {
        /// <summary>The streaming service that the target is associated with.</summary>
        public string? service;
        
        /// <summary>The ID of the target.</summary>
        public string? id;
        
        /// <summary>The display name of the target.</summary>
        public string? name;
        
        /// <summary>The avatar URL for the target.</summary>
        public string? avatar;
    }
}