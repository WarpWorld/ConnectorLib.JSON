using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

//if you update this, also update the one in CrowdControl.Games
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum GameState
{
    Unknown = 0,

    Ready = 1,

    Error = -1,
    Unmodded = -2,
    BadGameSettings = -3,
    WrongVersion = -4,
    NotFocused = -5,
    Loading = -6,
    Paused = -7,
    WrongMode = -8,
    SafeArea = -9,
    UntimedArea = -10,
    Cutscene = -11,
    BadPlayerState = -12,
    Menu = -13,

    /// <remarks>
    /// This should only be used when the game is in a map scene and shouldn't be.
    /// If the map state is correct, return <see cref="Ready"/> instead.
    /// </remarks>
    Map = -14,

    /// <remarks>
    /// This should only be used when the game is in a combat scene and shouldn't be.
    /// If the combat state is correct, return <see cref="Ready"/> instead.
    /// </remarks>
    InCombat = -15,

    /// <remarks>
    /// This should only be used when the game is not in a combat scene and should be.
    /// If the non-combat state is correct, return <see cref="Ready"/> instead.
    /// </remarks>
    NotInCombat = -16,
    PipelineBusy = -128
}