using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

[Serializable]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class RpcResponse : SimpleJSONRequest
{
    public object? value;

    public RpcResponse() => type = RequestType.RpcResponse;
}
