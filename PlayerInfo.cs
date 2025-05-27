#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

/// <summary>Information about a player in the game.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PlayerInfo : SimpleJSONRequest
{
    /// <summary>The player information in JSON format.</summary>
    /// <remarks>This field is currently unstructured due to technical limitations. It will be changed in the future.</remarks>
    public JObject? player; //todo fix this

    /// <summary>Creates a new instance of the <see cref="PlayerInfo"/> class.</summary>
    public PlayerInfo() => type = RequestType.PlayerInfo;
}