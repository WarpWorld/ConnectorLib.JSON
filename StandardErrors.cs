using System;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum StandardErrors
{
    Unknown = 0x0000,
    ExceptionThrown = 0x0001,

    //Bad Request
    BadRequest = 0x1000,

    [Obsolete($"Use {nameof(EffectUnknown)} instead.")]
    UnknownEffect = 0x1001,
    EffectUnknown = 0x1001,
    EffectDisabled = 0x1002,
    AlreadyFailed = 0x1003,
    CannotParseNumber = 0x1010,
    UnknownSelection = 0x1011,

    //Connector Errors
    ConnectorError = 0x2000,
    ConnectorReadFailure = 0x2001,
    ConnectorWriteFailure = 0x2002,
    ConnectorNotConnected = 0x2003,
    ConnectorNotSupported = 0x2004,

    //Settings Errors
    SettingsError = 0x3000,
    CooldownPerEffect = 0x3101,
    CooldownGlobal = 0x3102,
    RetryMaxTime = 0x3003,
    RetryMaxAttempts = 0x3004,

    //Session Errors
    NoSession = 0x5000,
    SessionEnding = 0x5001,

    //Game State Errors
    BadGameState = 0x4000,

    //Game State Errors - Missing Objects
    GameObjectNotFound = 0x4100,
    PlayerNotFound = 0x4101,
    CharacterNotFound = 0x4102,
    EnemyNotFound = 0x4103,
    ObjectNotFound = 0x4104,
    PrerequisiteNotFound = 0x4105,

    //Game State Errors - Object State
    ObjectStateError = 0x4200,
    AlreadyInState = 0x4201,
    AlreadyAcquired = 0x4202,
    AlreadyFinished = 0x4203,
    NoEmptyContainers = 0x4210,
    PartyFull = 0x4211,
    InvalidArea = 0x4220,
    InvalidTarget = 0x4221,
    NoValidTargets = 0x4222,
    SpawnNotAllowedHere = 0x4224,

    //Game State Errors - Range Limits
    RangeError = 0x4300,
    AlreadyMaximum = 0x4301,
    AlreadyMinimum = 0x4302,

    //Game Pack Errors
    EffectNotImplemented = 0x6000,
    PackResourceMissing = 0x6001,

    //Game Pack State Errors
    ConflictingEffectRunning = 0x8000,
    UnqueueablePending = 0x8001,

    //Emulator Errors
    EmulatorNotSupported = 0x7000,
    EmulatorInvalidSetting = 0x7001
}