using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>The type of the message.</summary>
/// <remarks>
/// These are messages from the game mod to the Crowd Control client.<br/>
/// The name <see cref="ResponseType"/> is misleading and should not be taken as an
/// indication that all of these message types are necessarily responses to messages.
/// </remarks>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum ResponseType : byte
{
    /// <summary>Response to an effect request.</summary>
    EffectRequest = 0x00,
    
    /// <summary>Effect status update.</summary>
    EffectStatus = 0x01,

    /// <summary>Generic event callback.</summary>
    GenericEvent = 0x10,
    
    /// <summary>Event callback for the player loading a game save.</summary>
    LoadEvent = 0x18,
    
    /// <summary>Event callback for the player saving a game save.</summary>
    SaveEvent = 0x19,

    /// <summary>Response to a metadata request.</summary>
    DataResponse = 0x20,

    /// <summary>RPC request to the Crowd Control client.</summary>
    RpcRequest = 0xD0,

    /// <summary>Login demand from the game mod to the Crowd Control client.</summary>
    Login = 0xF0,
    
    /// <summary>Login success response.</summary>
    LoginSuccess = 0xF1,
    
    /// <summary>Game state information update.</summary>
    GameUpdate = 0xFD,
    
    /// <summary>Disconnect with an optional message.</summary>
    Disconnect = 0xFE,

    /// <summary>Keep-alive message.</summary>
    KeepAlive = 0xFF
}
