#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PlayerInfo : SimpleJSONRequest
{
    public JObject? player; //todo fix this
    public PlayerInfo() => type = RequestType.PlayerInfo;
}