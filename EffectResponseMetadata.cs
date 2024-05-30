using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

[method: JsonConstructor]
public class EffectResponseMetadata(string key, EffectStatus status, object? value = null, long timeRemaining = 0, string? message = null)
{
    /// <summary>
    /// The key corresponding to the requested value.
    /// </summary>
    public string key = key;

    /// <summary>
    /// The value returned by the client.
    /// </summary>
    public JToken? value = value.IfNotNull(JToken.FromObject);

    /// <summary>
    /// The status of the request.
    /// </summary>
    public EffectStatus status = status;

    /// <summary>
    /// The due time of the request, if applicable.
    /// </summary>
    /// <remarks>
    /// This is milliseconds, not seconds.
    /// </remarks>
    public long? timeRemaining = timeRemaining;

    /// <summary>
    /// The message from the client (if any).
    /// </summary>
    public string? message = message;

    public static EffectResponseMetadata Success(string key, object? value, string? message = null) => new(key, EffectStatus.Success, value: value, message: message);

    public static EffectResponseMetadata Failure(string key) => new(key, EffectStatus.Failure);
    public static EffectResponseMetadata Failure(string key, string? message) => new(key, EffectStatus.Failure, message: message);
    public static EffectResponseMetadata Failure(string key, object? value, string? message = null) => new(key, EffectStatus.Failure, message: message);

    public static EffectResponseMetadata Retry(string key, long delay = 0, string? message = null) => new(key, EffectStatus.Retry,
        timeRemaining: delay,
        message: message);
}