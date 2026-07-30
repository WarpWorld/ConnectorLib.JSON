#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>A response to a request for the current version of the game mod.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class VersionResponse : SimpleJSONResponse
{
    /// <summary>The message from the client (if any).</summary>
    public VersionNumber version;

    /// <summary>Creates a new instance of the <see cref="VersionResponse"/> class.</summary>
    /// <param name="version">The version number of the game mod.</param>
    [JsonConstructor]
    public VersionResponse(VersionNumber version)
    {
        this.version = version;
        type = ResponseType.Version;
    }

    /// <summary>Creates a new instance of the <see cref="VersionResponse"/> class.</summary>
    /// <param name="id">The ID of the response.</param>
    /// <param name="version">The version number of the game mod.</param>
    [JsonConstructor]
    public VersionResponse(uint id, VersionNumber version)
    {
        this.id = id;
        this.version = version;
        type = ResponseType.Version;
    }
}