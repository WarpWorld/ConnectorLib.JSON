#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

/// <summary>Represents a request for game data.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DataRequest : SimpleJSONRequest
{
    /// <summary>The key of the data to request.</summary>
    public string key;

    /// <summary>Creates a new instance of the <see cref="DataRequest"/> class.</summary>
    /// <param name="key">The key of the data to request. This should be a string that identifies the data you want to retrieve.</param>
    public DataRequest(string key)
    {
        this.key = key;
        type = RequestType.DataRequest;
    }
}