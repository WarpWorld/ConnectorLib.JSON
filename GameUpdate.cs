#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GameUpdate : SimpleJSONResponse
{
    /// <summary>
    /// The current state of the game.
    /// </summary>
    public GameState state;

    public string? message;

    public GameUpdate(GameState state, string? message = null)
    {
        this.state = state;
        this.message = message;
        type = ResponseType.GameUpdate;
    }
}