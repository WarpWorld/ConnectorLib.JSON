#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class RpcResponse : SimpleJSONRequest
{
    public object? value;

    public RpcResponse() => type = RequestType.RpcResponse;
}
