using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum GameState
{
    Unknown = 0,
    Ready = 1,
    Exception = -1,
    Unmodded = -2,
    NotFocused = -3,
    WrongMode = -4,
    Paused = -5,
}