using System;
using System.Diagnostics.CodeAnalysis;
//using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>
/// The type of the message.
/// </summary>
/// <remarks>
/// These are messages from the Crowd Control client to the game mod.<br/>
/// The name <see cref="RequestType"/> is misleading and should not be taken as an
/// indication that all of these message types are necessarily effect requests.
/// </remarks>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
//[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum RequestType : byte
{
    [Obsolete($"Use {nameof(EffectTest)} instead.")]
    Test = EffectTest,
    EffectTest = 0x00,
    [Obsolete($"Use {nameof(EffectStart)} instead.")]
    Start = EffectStart,
    EffectStart = 0x01,
    [Obsolete($"Use {nameof(EffectStop)} instead.")]
    Stop = EffectStop,
    EffectStop = 0x02,

    GenericEvent = 0x10,

    DataRequest = 0x20,

    RpcResponse = 0xD0,

    PlayerInfo = 0xE0,
    Login = 0xF0,
    GameUpdate = 0xFD,
    KeepAlive = 0xFF
}
