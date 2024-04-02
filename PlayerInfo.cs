using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

[Serializable]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PlayerInfo : SimpleJSONRequest
{
    public JObject? player; //todo fix this
    public PlayerInfo() => type = RequestType.Start;
}