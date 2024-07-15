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
    NotFocused = -3,
    Loading = -4,
    Paused = -5,
    WrongMode = -6,
    SafeArea = -7,
    Cutscene = -8,
    BadPlayerState = -9,
    Menu = -10,
    Map = -11,
    PipelineBusy = -12
}