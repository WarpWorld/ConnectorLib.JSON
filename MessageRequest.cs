#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
namespace ConnectorLib.JSON;

/// <summary>A text message.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
public class MessageRequest : SimpleJSONRequest
{
    /// <summary>The message to be sent.</summary>
    public string? message;
}