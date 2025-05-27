using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

/// <summary>The state of a game.</summary>
//if you update this, also update the one in CrowdControl.Games
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum GameState
{
    /// <summary>The game state is unknown.</summary>
    /// <remarks>This is used when the game is not running or the state cannot be determined due to effect pack or connector problems.</remarks>
    Unknown = 0,

    /// <summary>The game is running and is ready to receive effects.</summary>
    /// <remarks>This is the only game state where effects can be processed.</remarks>
    Ready = 1,

    /// <summary>The game is in an errored or crashed state.</summary>
    Error = -1,
    
    /// <summary>The game does not have the required mod(s) installed.</summary>
    Unmodded = -2,
    
    /// <summary>The current game settings are not compatible with Crowd Control.</summary>
    BadGameSettings = -3,
    
    /// <summary>The game is the wrong version.</summary>
    WrongVersion = -4,
    
    /// <summary>The game is not focused.</summary>
    NotFocused = -5,
    
    /// <summary>The game is in a loading state.</summary>
    Loading = -6,
    
    /// <summary>The game is paused.</summary>
    Paused = -7,
    
    /// <summary>The game is in a state that is not compatible with Crowd Control.</summary>
    /// <remarks>This is used when the game is in a state such as a title menu or alternate game mode.</remarks>
    WrongMode = -8,
    
    /// <summary>The game is in a safe area.</summary>
    /// <remarks>This is used when the game is in a safe area such as a town or hub.</remarks>
    SafeArea = -9,
    
    /// <summary>The game is in an untimed area.</summary>
    /// <remarks>This is used when the game is in an untimed area such as a cutscene or tutorial.</remarks>
    UntimedArea = -10,
    
    /// <summary>The game is in a cutscene.</summary>
    Cutscene = -11,
    
    /// <summary>The player is not in a state to receive effects such as being dead or stunned.</summary>
    BadPlayerState = -12,
    
    /// <summary>The game is in a menu other than the pause menu.</summary>
    Menu = -13,
    
    /// <summary>The game is in a map state such as a world map or overworld.</summary>
    /// <remarks>
    /// This should only be used when the game is in a map scene and shouldn't be.
    /// If the map state is correct, return <see cref="Ready"/> instead.
    /// </remarks>
    Map = -14,
    
    /// <summary>The game is in a combat state such as a boss fight or enemy encounter.</summary>
    /// <remarks>
    /// This should only be used when the game is in a combat scene and shouldn't be.
    /// If the combat state is correct, return <see cref="Ready"/> instead.
    /// </remarks>
    InCombat = -15,
    
    /// <summary>The game is in a non-combat scene such as a cutscene or tutorial.</summary>
    /// <remarks>
    /// This should only be used when the game is not in a combat scene and should be.
    /// If the non-combat state is correct, return <see cref="Ready"/> instead.
    /// </remarks>
    NotInCombat = -16,
    
    /// <summary>The game message queue is busy or full and cannot process any more messages.</summary>
    PipelineBusy = -128
}