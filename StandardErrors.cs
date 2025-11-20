using System;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>The standard set of errors that can occur in the Crowd Control system.</summary>
[JsonConverter(typeof(CamelCaseStringEnumConverter))]
//remember to copy updates into CrowdControl.Games.StandardErrors
public enum StandardErrors
{
    //General Errors
    /// <summary>An unknown error occurred.</summary>
    Unknown = 0x0000,
    /// <summary>An exception was thrown while attempting to run the effect.</summary>
    ExceptionThrown = 0x0001,

    //Bad Request
    /// <summary>The request was invalid.</summary>
    BadRequest = 0x1000,
    /// <summary>The effect was not available.</summary>
    [Obsolete($"Use {nameof(EffectUnknown)} instead.")]
    UnknownEffect = 0x1001,
    /// <summary>The effect was not available.</summary>
    EffectUnknown = 0x1001,
    /// <summary>The effect was disabled.</summary>
    EffectDisabled = 0x1002,
    /// <summary>The effect has already failed.</summary>
    AlreadyFailed = 0x1003,
    /// <summary>A supplied parameter could not be parsed.</summary>
    CannotParseNumber = 0x1010,
    /// <summary>A supplied parameter was unknown.</summary>
    UnknownSelection = 0x1011,

    //Connector Errors
    /// <summary>An unknown connector error occurred.</summary>
    ConnectorError = 0x2000,
    /// <summary>The connector could not read from the game.</summary>
    ConnectorReadFailure = 0x2001,
    /// <summary>The connector could not write to the game.</summary>
    ConnectorWriteFailure = 0x2002,
    /// <summary>The connector could not connect to the game.</summary>
    ConnectorNotConnected = 0x2003,
    /// <summary>The connector does not support the requested effect.</summary>
    ConnectorNotSupported = 0x2004,

    //TCP Connector Errors
    /// <summary>The connector received no response from the game.</summary>
    NoResponse = 0x2100,

    //Settings Errors
    /// <summary>An error occurred while processing the pack settings.</summary>
    SettingsError = 0x3000,
    /// <summary>The effect is on cooldown.</summary>
    CooldownPerEffect = 0x3101,
    /// <summary>Effects are on cooldown.</summary>
    CooldownGlobal = 0x3102,
    /// <summary>The maximum retry time was exceeded.</summary>
    RetryMaxTime = 0x3003,
    /// <summary>The maximum number of retry attempts was exceeded.</summary>
    RetryMaxAttempts = 0x3004,

    //Session Errors
    /// <summary>No active game session.</summary>
    NoSession = 0x5000,
    /// <summary>The game session ended before the effect could be run.</summary>
    SessionEnding = 0x5001,

    //Game State Errors
    /// <summary>The game state was not ready.</summary>
    BadGameState = 0x4000,

    //Game State Errors - Missing Objects
    /// <summary>A required game entity was not found.</summary>
    GameObjectNotFound = 0x4100,
    /// <summary>The player was not found.</summary>
    PlayerNotFound = 0x4101,
    /// <summary>The character or party member was not found.</summary>
    CharacterNotFound = 0x4102,
    /// <summary>The enemy was not found.</summary>
    EnemyNotFound = 0x4103,
    /// <summary>The object was not found.</summary>
    ObjectNotFound = 0x4104,
    /// <summary>A required object was not found.</summary>
    PrerequisiteNotFound = 0x4105,

    //Game State Errors - Object State
    /// <summary>A required game entity was not in the correct state.</summary>
    ObjectStateError = 0x4200,
    /// <summary>The character or object was already in the target state.</summary>
    AlreadyInState = 0x4201,
    /// <summary>The item or object was already acquired.</summary>
    AlreadyAcquired = 0x4202,
    /// <summary>The selected objective was already finished.</summary>
    AlreadyFinished = 0x4203,
    /// <summary>There are no empty containers or slots available.</summary>
    NoEmptyContainers = 0x4210,
    /// <summary>The player's group or party is full.</summary>
    PartyFull = 0x4211,
    /// <summary>The selected effect is not applicable or not allowed in the current area.</summary>
    InvalidArea = 0x4220,
    /// <summary>The selected target is not valid for the effect.</summary>
    InvalidTarget = 0x4221,
    /// <summary>No valid targets were found.</summary>
    NoValidTargets = 0x4222,
    /// <summary>The selected object or enemy cannot be spawned here.</summary>
    SpawnNotAllowedHere = 0x4224,

    //Game State Errors - Range Limits
    /// <summary>A value range error occurred.</summary>
    RangeError = 0x4300,
    /// <summary>The value was already at the minimum.</summary>
    AlreadyMinimum = 0x4301,
    /// <summary>The value was already at the maximum.</summary>
    AlreadyMaximum = 0x4302,

    //Game Pack Errors
    /// <summary>The effect is not implemented.</summary>
    EffectNotImplemented = 0x6000,
    /// <summary>A required pack resource was missing.</summary>
    PackResourceMissing = 0x6001,
    
    //Emulator Errors
    /// <summary>The emulator does not support this effect.</summary>
    EmulatorNotSupported = 0x7000,
    /// <summary>The emulator was not properly configured.</summary>
    EmulatorInvalidSetting = 0x7001,

    //Game Pack State Errors
    /// <summary>A conflicting effect is already running.</summary>
    ConflictingEffectRunning = 0x8000,
    /// <summary>This effect cannot be queued and another instance is already running.</summary>
    UnqueueablePending = 0x8001
}