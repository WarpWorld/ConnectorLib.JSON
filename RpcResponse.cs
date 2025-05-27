#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

/// <summary>RPC response message. </summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class RpcResponse : SimpleJSONRequest
{
    /// <summary>The return value of the method call.</summary>
    public object? value;

    /// <summary>Initializes a new instance of the <see cref="RpcResponse"/> class.</summary>
    public RpcResponse() => type = RequestType.RpcResponse;
}
