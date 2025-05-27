#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

/// <summary>A response to a request for game data.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DataResponse : SimpleJSONResponse
{
    /// <summary>The key corresponding to the requested value.</summary>
    public string key;

    /// <summary>The value returned by the client.</summary>
    public JToken? value;

    /// <summary>The status of the request.</summary>
    public EffectStatus status;

    /// <summary>The due time of the request, if applicable.</summary>
    /// <remarks>This is milliseconds, not seconds.</remarks>
    public long timeRemaining; //milliseconds

    /// <summary>The message from the client (if any).</summary>
    public string? message;

    /// <summary>Creates a new instance of the <see cref="DataResponse"/> class.</summary>
    /// <param name="key">The key corresponding to the requested value. This should be a string that identifies the data you want to retrieve.</param>
    /// <param name="status">The status of the request.</param>
    /// <param name="value">The value returned by the client. This can be any object, including null.</param>
    /// <param name="timeRemaining">The due time of the request, if applicable. This is in milliseconds.</param>
    /// <param name="message">The message from the client (if any). This can be null.</param>
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

    /// <summary>Creates a success response if the value is defined (not null or empty).</summary>
    /// <param name="key">The key corresponding to the requested value.</param>
    /// <param name="value">The value to return. If this is null or empty, a failure response will be generated.</param>
    /// <param name="failMessage">The message to return if the value is null or empty. This will be used as the message in the failure response.</param>
    /// <returns>A success response if the value is defined; otherwise, a failure response with the specified message.</returns>
    public static DataResponse SuccessIfDefined(string key, object? value, string failMessage = "")
    {
        if (value is null) return Retry(key, 0, failMessage);
        if ((value is string s) && s.IsNullOrWhiteSpace()) return Retry(key, 0, failMessage);
        return Success(key, value);
    }

    /// <summary>Creates a success response with the specified key and value.</summary>
    /// <param name="key">The key corresponding to the requested value.</param>
    /// <param name="value">The value to return.</param>
    /// <returns>A success response with the specified key and value.</returns>
    public static DataResponse Success(string key, object? value) => new(key, EffectStatus.Success, value);

    /// <summary>Creates a success response with the specified key and value.</summary>
    /// <param name="key">The key corresponding to the requested value.</param>
    /// <param name="value">The value to return.</param>
    /// <param name="message">The message to return.</param>
    /// <returns>A success response with the specified key and value.</returns>
    public static DataResponse Success(string key, object? value, string? message) => new(key, EffectStatus.Success, value, 0, message);

    /// <summary>Creates a failure response with the specified key.</summary>
    /// <param name="key">The key corresponding to the requested value.</param>
    public static DataResponse Failure(string key) => new(key, EffectStatus.Failure);

    /// <summary>Creates a failure response with the specified key and message.</summary>
    /// <param name="key">The key corresponding to the requested value.</param>
    /// <param name="message">The message to return.</param>
    public static DataResponse Failure(string key, string? message) => new(key, EffectStatus.Failure, message: message);

    /// <summary>Creates a failure response with the specified key and message.</summary>
    /// <param name="key">The key corresponding to the requested value.</param>
    /// <param name="value">The value to return.</param>
    /// <param name="message">The message to return.</param>
    public static DataResponse Failure(string key, object? value, string? message = null) => new(key, EffectStatus.Failure, value, 0, message);
    
    /// <summary>Creates a retry response with the specified key and message.</summary>
    /// <param name="key">The key corresponding to the requested value.</param>
    /// <param name="delay">The delay time in milliseconds. This is the time the client should wait before retrying the request.</param>
    /// <param name="message">The message to return.</param>
    public static DataResponse Retry(string key, long delay = 0, string? message = null) => new(key, EffectStatus.Retry,
        timeRemaining: delay,
        message: message);
}