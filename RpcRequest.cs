#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

/// <summary>RPC request message.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class RpcRequest : SimpleJSONResponse
{
    /// <summary>The name of the method to call.</summary>
    public string? method;

    /// <summary>The parameters to be passed to the named method.</summary>
    public object?[]? args;

    /// <summary>Initializes a new instance of the <see cref="RpcRequest"/> class.</summary>
    public RpcRequest() => type = ResponseType.RpcRequest;
}
