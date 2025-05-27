#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

/// <summary>A response to a request for the game state.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GameUpdate : SimpleJSONResponse
{
    /// <summary>The current state of the game.</summary>
    public GameState state;

    /// <summary>The message from the client (if any).</summary>
    public string? message;
    
    /// <summary>Creates a new instance of the <see cref="GameUpdate"/> class.</summary>
    /// <param name="state">The current state of the game.</param>
    /// <param name="message">A message to be displayed to the user, if applicable. This can be null.</param>
    public GameUpdate(GameState state, string? message = null)
    {
        this.state = state;
        this.message = message;
        type = ResponseType.GameUpdate;
    }
}