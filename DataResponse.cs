#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DataResponse : SimpleJSONResponse
{
    /// <summary>
    /// The key corresponding to the requested value.
    /// </summary>
    public string key;

    /// <summary>
    /// The value returned by the client.
    /// </summary>
    public JToken? value;

    /// <summary>
    /// The status of the request.
    /// </summary>
    public EffectStatus status;

    /// <summary>
    /// The due time of the request, if applicable.
    /// </summary>
    /// <remarks>
    /// This is milliseconds, not seconds.
    /// </remarks>
    public long? timeRemaining;

    /// <summary>
    /// The message from the client (if any).
    /// </summary>
    public string? message;

    [JsonConstructor]
    public DataResponse(string key, EffectStatus status, object? value = null, long timeRemaining = 0, string? message = null)
    {
        this.key = key;
        this.value = value.IfNotNull(JToken.FromObject);
        this.status = status;
        this.timeRemaining = timeRemaining;
        this.message = message;
        type = ResponseType.DataResponse;
    }

    public static DataResponse Success(string key, object? value, string? message = null) => new(key, EffectStatus.Success, value: value, message: message);

    public static DataResponse Failure(string key) => new(key, EffectStatus.Failure);
    public static DataResponse Failure(string key, string? message) => new(key, EffectStatus.Failure, message: message);
    public static DataResponse Failure(string key, object? value, string? message = null) => new(key, EffectStatus.Failure, message: message);

    public static DataResponse Retry(string key, long delay = 0, string? message = null) => new(key, EffectStatus.Retry,
        timeRemaining: delay,
        message: message);
}