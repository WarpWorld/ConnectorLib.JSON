using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

[SuppressMessage("ReSharper", "UnusedMember.Local")]
public enum RequestType : byte
{
    Test = 0x00,
    Start = 0x01,
    Stop = 0x02,

    GenericEvent = 0x10,

    RpcResponse = 0xD0,

    PlayerInfo = 0xE0,
    Login = 0xF0,
    KeepAlive = 0xFF
}
