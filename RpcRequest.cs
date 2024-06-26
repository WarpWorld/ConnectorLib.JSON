﻿#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class RpcRequest : SimpleJSONResponse
{
    /// <summary>
    /// The name of the method to call.
    /// </summary>
    public string? method;

    /// <summary>
    /// The parameters to be passed to the named method.
    /// </summary>
    public object?[]? args;
}
