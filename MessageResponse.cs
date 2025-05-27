#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
namespace ConnectorLib.JSON;

/// <summary>A text message.</summary>
/// <remarks>
/// This is called a "response" because messages sent to the client are called responses but this message is not sent in response <b>to</b> anything.
/// It simply represents a generic event that is not defined in the API.
/// </remarks>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
public class MessageResponse : SimpleJSONResponse
{
    /// <summary>The message to be sent.</summary>
    public string? message;
}
