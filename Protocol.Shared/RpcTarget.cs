using System;
#if !PROTOCOL_JSON
using CrowdControl.Common.Converters;
#endif
using Newtonsoft.Json;

#if PROTOCOL_JSON
namespace ConnectorLib.JSON
#else
namespace CrowdControl.Common
#endif
{

[Flags]
#if PROTOCOL_JSON
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
#else
[JsonConverter(typeof(CamelCaseStringEnumConverter<RpcTarget>))]
#endif
/// <summary>Identifies the target of an RPC request.</summary>
public enum RpcTarget
{
    Game = 0x01,
    Pack = 0x02,
    Native = 0x04,
    Client = 0x08,
    Server = 0x10
}
}