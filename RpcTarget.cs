using System;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

[Flags]
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum RpcTarget
{
    Game = 0x01,
    Pack = 0x02,
    Native = 0x04,
    Client = 0x08,
    Server = 0x10
}