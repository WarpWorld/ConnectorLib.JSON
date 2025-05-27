using System;
using System.Diagnostics.CodeAnalysis;
//using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>The type of the message.</summary>
/// <remarks>
/// These are messages from the Crowd Control client to the game mod.<br/>
/// The name <see cref="RequestType"/> is misleading and should not be taken as an
/// indication that all of these message types are necessarily effect requests.
/// </remarks>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
//[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum RequestType : byte
{
    /// <inheritdoc cref="EffectTest"/>
    [Obsolete($"Use {nameof(EffectTest)} instead.")]
    Test = EffectTest,
    
    /// <summary>Request to test-start an effect. Games that support this should respond as if the effect has started or failed, but not actually start it.</summary> 
    EffectTest = 0x00,

    /// <inheritdoc cref="EffectStart"/>
    [Obsolete($"Use {nameof(EffectStart)} instead.")]
    Start = EffectStart,
    
    /// <summary>Request to start an effect.</summary>
    EffectStart = 0x01,

    /// <inheritdoc cref="EffectStop"/>
    [Obsolete($"Use {nameof(EffectStop)} instead.")]
    Stop = EffectStop,
    
    /// <summary>Request to stop an effect.</summary>
    EffectStop = 0x02,

    /// <summary>Generic event callback.</summary>
    GenericEvent = 0x10,

    /// <summary>Request for game metadata.</summary>
    DataRequest = 0x20,

    /// <summary>Response to an RPC request from the game.</summary>
    RpcResponse = 0xD0,

    /// <summary>Player information.</summary>
    PlayerInfo = 0xE0,

    /// <summary>Login information.</summary>
    Login = 0xF0,
    
    /// <summary>Request for updated game state information.</summary>
    GameUpdate = 0xFD,
    
    /// <summary>Keep-alive message.</summary>
    KeepAlive = 0xFF
}
