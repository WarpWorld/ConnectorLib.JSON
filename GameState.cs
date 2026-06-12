using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

/// <summary>The state of a game.</summary>
//if you update this, also update the one in CrowdControl.Games
//the numeric values of enum members in this file exist for backward-compatibility
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum GameState
{
    //Game Errors

    /// <summary>The game state is unknown.</summary>
    /// <remarks>This is used when the game is not running or the state cannot be determined due to effect pack or connector problems.</remarks>
    Unknown = 0,

    /// <summary>The game is in an errored or crashed state.</summary>
    Error = -1,

    //Game Incompatibility

    /// <summary>The game does not have the required mod(s) installed.</summary>
    Unmodded = -2,

    /// <summary>The current game settings are not compatible with Crowd Control.</summary>
    BadGameSettings = -3,

    /// <summary>The game is the wrong version.</summary>
    WrongVersion = -4,

    //Game Meta-States

    /// <summary>The game is not focused.</summary>
    NotFocused = -5,

    /// <summary>The game is in a loading state.</summary>
    Loading = -6,

    //Normal Game States

    /// <summary>The game is running in a standard gameplay mode.</summary>
    /// <remarks>
    /// A game should only report this state when the game is playable no other states are applicable.
    /// This should be the most commonly reported state when the game is running and should be used for the main gameplay state of the game.
    /// </remarks>
    InLevel = 1,

    [Obsolete("Use InLevel instead. This state is redundant and does not provide any additional information about the game state.")]
    Ready = InLevel,

    /// <summary>The game is on the title screen or other title menu (e.g. Load Game Menu).</summary>
    TitleScreen = -17,

    /// <summary>The game is playing the credits or is otherwise finished with the main game content.</summary>
    Credits = -18,

    /// <summary>The game is in a menu other than the pause or title menus.</summary>
    Menu = -13,

    /// <summary>The game is paused.</summary>
    Paused = -7,

    /// <summary>The game is in an alternate game mode.</summary>
    /// <remarks>This is used when the game has additional modes that are not supported by Crowd Control (e.g. Training Mode).</remarks>
    WrongMode = -8,

    /// <summary>The game is in a safe area.</summary>
    /// <remarks>This is used when the game is in a safe area such as a town or hub.</remarks>
    SafeArea = -9,

    /// <summary>The game is in the starting area.</summary>
    /// <remarks>This is used when the game is in a starting area or tutorial area.</remarks>
    StartingArea = -19,

    /// <summary>The game is in a dialogue scene.</summary>
    /// <remarks>This is used when the game is in a non-cutscene dialogue state. For cutscenes with dialog, use <see cref="Cutscene"/> instead.</remarks>
    Dialogue = -20,

    /// <summary>The game is in a cutscene.</summary>
    Cutscene = -11,

    /// <summary>The player is not in a state to receive effects (e.g. The player is dead, regenerating, missing, etc).</summary>
    BadPlayerState = -12,

    /// <summary>The game is not currently accepting input.</summary>
    InputLocked = -21,

    /// <summary>The game is in a map state such as a world map or overworld.</summary>
    Map = -14,

    /// <summary>The game is in an untimed area.</summary>
    /// <remarks>
    /// This is used when the game is in an area where time does not pass and the lack of timer is not a common game state.
    /// This state should not be used for games where time does not pass during normal gameplay.
    /// </remarks>
    UntimedArea = -10,

    /// <summary>The game is in a combat state such as a boss fight or enemy encounter.</summary>
    /// <remarks>This state should not be used for games where the player is always in combat (e.g. fighting games).</remarks>
    InCombat = -15,

    /// <summary>The game is in a non-combat scene such as a cutscene or tutorial.</summary>
    /// <remarks>This state should not be used for games that do not have combat (e.g. puzzle games).</remarks>
    NotInCombat = -16,

    //Generic State Errors

    /// <summary>The game message queue is busy or full and cannot process any more messages.</summary>
    PipelineBusy = -128,

    /// <summary>The game is in a state that is not ready to receive effects but is not well-described by other states.</summary>
    NotReady = int.MinValue
}