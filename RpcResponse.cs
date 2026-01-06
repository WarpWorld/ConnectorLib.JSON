#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

/// <summary>RPC response message. </summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class RpcResponse : SimpleJSONRequest
{
    /// <summary>The return value of the method call.</summary>
    /// <remarks>
    /// This will be null if the method has a void return type.
    /// If an exception occurred during the method call, this will contain the exception details or null.
    /// </remarks>
    public object? value;
    
    /// <summary>Indicates whether an exception occurred during the method call.</summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
    public bool exception;

    /// <summary>Initializes a new instance of the <see cref="RpcResponse"/> class.</summary>
    public RpcResponse() => type = RequestType.RpcResponse;
}