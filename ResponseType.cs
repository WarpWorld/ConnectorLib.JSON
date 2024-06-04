using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>
/// The type of the message.
/// </summary>
/// <remarks>
/// These are messages from the game mod to the Crowd Control client.<br/>
/// The name <see cref="ResponseType"/> is misleading and should not be taken as an
/// indication that all of these message types are necessarily responses to messages.
/// </remarks>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum ResponseType : byte
{
    /// <summary>
    /// This message is a response to a <see cref="ConnectorLib.JSON.EffectRequest"/>>.
    /// </summary>
    EffectRequest = 0x00,
    EffectStatus = 0x01,

    GenericEvent = 0x10,
    LoadEvent = 0x18,
    SaveEvent = 0x19,

    RpcRequest = 0xD0,

    Login = 0xF0,
    LoginSuccess = 0xF1,
    GameUpdate = 0xFD,
    Disconnect = 0xFE,
    KeepAlive = 0xFF
}
