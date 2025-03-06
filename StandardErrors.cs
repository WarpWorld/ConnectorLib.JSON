using Newtonsoft.Json;

namespace ConnectorLib.JSON;

[JsonConverter(typeof(CamelCaseStringEnumConverter))]
public enum StandardErrors
{
    Unknown,
    ExceptionThrown,

    //Bad Request
    BadRequest,
    UnknownEffect,
    AlreadyFailed,
    CannotParseNumber,
    UnknownSelection,

    //Connector Errors
    ConnectorError,
    ConnectorReadFailure,
    ConnectorWriteFailure,
    ConnectorNotConnected,
    ConnectorNotSupported,

    //Settings Errors
    SettingsError,
    CooldownPerEffect,
    CooldownGlobal,
    RetryMaxTime,
    RetryMaxAttempts,

    //Session Errors
    NoSession,
    SessionEnding,

    //Game State Errors
    BadGameState,

    //Game State Errors - Missing Objects
    GameObjectNotFound,
    PlayerNotFound,
    CharacterNotFound,
    EnemyNotFound,
    ObjectNotFound,
    PrerequisiteNotFound,

    //Game State Errors - Object State
    ObjectStateError,
    AlreadyInState,
    AlreadyAcquired,
    AlreadyFinished,
    ConflictingEffectRunning,
    NoEmptyContainers,
    PartyFull,
    InvalidArea,
    InvalidTarget,
    NoValidTargets,
    UnqueueablePending,
    SpawnNotAllowedHere,

    //Game State Errors - Range Limits
    RangeError,
    AlreadyMaximum,
    AlreadyMinimum,

    //Game Pack Errors
    EffectNotImplemented,
    PackResourceMissing,

    //Emulator Errors
    EmulatorNotSupported,
    EmulatorInvalidSetting,
}