using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum RequestType : byte
{
    Test = 0x00,
    Start = 0x01,
    Stop = 0x02,

    GenericEvent = 0x10,

    RpcResponse = 0xD0,

    PlayerInfo = 0xE0,
    Login = 0xF0,
    GameUpdate = 0xFD,
    KeepAlive = 0xFF
}
