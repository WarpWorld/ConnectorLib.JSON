#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

/// <summary>Response to a request for an effect.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class EffectResponse : SimpleJSONResponse
{
    /// <summary>Status of the effect.</summary>
    public EffectStatus status;
    
    /// <summary>Message to be displayed to the user, if applicable.</summary>
    public string? message;
    
    /// <summary>Standard ID of the error message, if applicable.</summary>
    public StandardErrors messageID;

    /// <remarks>
    /// If applicable (messages 0x00 (sometimes) and 0x07 (always)), this should contain the time remaining on the running effect, in milliseconds.
    /// Otherwise, this field should be 0 or missing.
    /// </remarks>
    /// <remarks>This is in milliseconds, not seconds.</remarks>
    public long timeRemaining; //milliseconds

    /// <summary>Any additional metadata associated with the effect.</summary>
    [JsonConverter(typeof(MetadataConverter))]
    public Dictionary<string, DataResponse>? metadata;

    /// <summary>Converts the metadata dictionary to and from JSON.</summary>
    private class MetadataConverter : JsonConverter<Dictionary<string, DataResponse>?>
    {
        public override void WriteJson(JsonWriter writer, Dictionary<string, DataResponse>? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            JObject result = new();
            foreach (var metaEntry in value)
            {
                JObject meta = JObject.FromObject(metaEntry.Value);
                meta.Remove("key");
                result[metaEntry.Key] = meta;
            }
            serializer.Serialize(writer, result);
        }

        public override Dictionary<string, DataResponse>? ReadJson(JsonReader reader, Type objectType, Dictionary<string, DataResponse>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject? jObject = (JObject?)serializer.Deserialize(reader);
            if (jObject == null) return null;
            Dictionary<string, DataResponse> result = new();
            foreach (JProperty property in jObject.Properties())
            {
                JObject pValue = ((JObject)property.Value);
                pValue["key"] = property.Name;
                result.Add(property.Name, pValue.ToObject<DataResponse>());
            }
            return result;
        }
    }

    /// <summary>Creates a new instance of the <see cref="EffectResponse"/> class.</summary>
    public EffectResponse() { }

    /// <inheritdoc cref="EffectResponse()"/>
    /// <param name="id">The ID of the effect.</param>
    /// <param name="status">The status of the effect.</param>
    [SuppressMessage("ReSharper", "IntroduceOptionalParameters.Global")]
    public EffectResponse(uint id, EffectStatus status)
        : this(id, status, 0, null) { }
    
    /// <inheritdoc cref="EffectResponse(uint, EffectStatus)"/>
    /// <param name="messageID">The standard ID of the error.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public EffectResponse(uint id, EffectStatus status, StandardErrors messageID)
        : this(id, status, 0, messageID) { }
    
    /// <inheritdoc cref="EffectResponse(uint, EffectStatus)"/>
    /// <param name="message">The message to be displayed to the user.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public EffectResponse(uint id, EffectStatus status, string? message)
        : this(id, status, 0, message) { }

    /// <inheritdoc cref="EffectResponse(uint, EffectStatus)"/>
    /// <param name="timeRemaining">The time remaining on the running effect.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public EffectResponse(uint id, EffectStatus status, TimeSpan timeRemaining)
        : this(id, status, (long)timeRemaining.TotalMilliseconds, null) { }

    /// <inheritdoc cref="EffectResponse(uint, EffectStatus, TimeSpan)"/>
    /// <param name="messageID">The standard ID of the error.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public EffectResponse(uint id, EffectStatus status, TimeSpan timeRemaining, StandardErrors messageID)
        : this(id, status, (long)timeRemaining.TotalMilliseconds, messageID) { }
    
    /// <inheritdoc cref="EffectResponse(uint, EffectStatus, TimeSpan)"/>
    /// <param name="message">The message to be displayed to the user.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public EffectResponse(uint id, EffectStatus status, TimeSpan timeRemaining, string? message)
        : this(id, status, (long)timeRemaining.TotalMilliseconds, message) { }
    
    /// <inheritdoc cref="EffectResponse(uint, EffectStatus)"/>
    /// <param name="timeRemaining">The time remaining on the running effect, in milliseconds.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    [SuppressMessage("ReSharper", "IntroduceOptionalParameters.Global")]
    public EffectResponse(uint id, EffectStatus status, long timeRemaining)
        : this(id, status, timeRemaining, null) { }

    /// <inheritdoc cref="EffectResponse(uint, EffectStatus, long)"/>
    /// <param name="messageID">The standard ID of the error.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public EffectResponse(uint id, EffectStatus status, long timeRemaining, StandardErrors messageID)
        : this(id, status, timeRemaining) => this.messageID = messageID;
    
    /// <inheritdoc cref="EffectResponse(uint, EffectStatus, long)"/>
    /// <param name="message">The message to be displayed to the user.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    [JsonConstructor]
    public EffectResponse(uint id, EffectStatus status, long timeRemaining, string? message)
    {
        this.id = id;
        this.status = status;
        this.timeRemaining = timeRemaining;
        this.message = message;
        type = ResponseType.EffectRequest;
    }

    /// <summary>Creates a success response.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="message">The message to be displayed to the user, if applicable.</param>
    /// <returns>A success response with the specified parameters.</returns>
    public static EffectResponse Success(uint id, string? message = null) => new(id, EffectStatus.Success, message);
    
    /// <inheritdoc cref="Success(uint, string?)"/>
    /// <param name="delay">The delay time in milliseconds.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Success(uint id, long delay, string? message = null) => new(id, EffectStatus.Success, delay, message);
    
    /// <inheritdoc cref="Success(uint, string?)"/>
    /// <param name="delay">The delay time.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Success(uint id, TimeSpan delay, string? message = null) => new(id, EffectStatus.Success, delay, message);
    
    /// <summary>Creates a success response.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="message">The message to be displayed to the user, if applicable.</param>
    /// <returns>A failure response with the specified parameters.</returns>
    public static EffectResponse Failure(uint id, string? message = null) => new(id, EffectStatus.Failure, message);

    /// <summary>Creates a success response.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="error">The standard ID of the error.</param>
    /// <returns>A failure response with the specified parameters.</returns>
    public static EffectResponse Failure(uint id, StandardErrors error) => new(id, EffectStatus.Failure, error);
    
    /// <summary>Creates a response indicating that the effect is unavailable.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="message">The message to be displayed to the user, if applicable.</param>
    /// <returns>An unavailable response with the specified parameters.</returns>
    public static EffectResponse Unavailable(uint id, string? message = null) => new(id, EffectStatus.Unavailable, message);

    /// <summary>Creates a response indicating that the effect is unavailable.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="error">The standard ID of the error.</param>
    /// <returns>A permanent failure response with the specified parameters.</returns>
    public static EffectResponse Unavailable(uint id, StandardErrors error) => new(id, EffectStatus.Unavailable, error);

    /// <summary>Creates a response indicating that the effect is temporarily unavailable.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="message">The message to be displayed to the user, if applicable.</param>
    /// <returns>A temporary failure response with the specified parameters.</returns>
    public static EffectResponse Retry(uint id, string? message = null) => new(id, EffectStatus.Retry, 0, message);

    /// <inheritdoc cref="Retry(uint, string?)"/>
    /// <param name="delay">The delay time in milliseconds.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Retry(uint id, long delay, string? message = null) => new(id, EffectStatus.Retry, delay, message);

    /// <inheritdoc cref="Retry(uint, string?)"/>
    /// <param name="delay">The delay time.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Retry(uint id, TimeSpan delay, string? message = null) => new(id, EffectStatus.Retry, delay, message);

    /// <summary>Creates a response indicating that the effect is temporarily unavailable.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="error">The standard ID of the error.</param>
    /// <returns>A temporary failure response with the specified parameters.</returns>
    public static EffectResponse Retry(uint id, StandardErrors error) => new(id, EffectStatus.Retry, 0, error);

    /// <inheritdoc cref="Retry(uint, StandardErrors?)"/>
    /// <param name="delay">The delay time in milliseconds.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Retry(uint id, long delay, StandardErrors error) => new(id, EffectStatus.Retry, delay, error);

    /// <inheritdoc cref="Retry(uint, StandardErrors?)"/>
    /// <param name="delay">The delay time.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Retry(uint id, TimeSpan delay, StandardErrors error) => new(id, EffectStatus.Retry, delay, error);

    /// <summary>Creates a response indicating that the effect is paused.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="message">The message to be displayed to the user, if applicable.</param>
    public static EffectResponse Paused(uint id, string? message = null) => new(id, EffectStatus.Paused, 0, message);
    
    /// <inheritdoc cref="Paused(uint, string?)"/>
    /// <param name="delay">The delay time in milliseconds.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Paused(uint id, long timeRemaining, string? message = null) => new(id, EffectStatus.Paused, timeRemaining, message);
    
    /// <inheritdoc cref="Paused(uint, string?)"/>
    /// <param name="delay">The delay time.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Paused(uint id, TimeSpan timeRemaining, string? message = null) => new(id, EffectStatus.Paused, timeRemaining, message);

    /// <summary>Creates a response indicating that the effect has paused.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="error">The standard ID of the error.</param>
    public static EffectResponse Paused(uint id, StandardErrors error) => new(id, EffectStatus.Paused, 0, error);

    /// <inheritdoc cref="Paused(uint, StandardErrors?)"/>
    /// <param name="delay">The delay time in milliseconds.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Paused(uint id, long timeRemaining, StandardErrors error) => new(id, EffectStatus.Paused, timeRemaining, error);

    /// <inheritdoc cref="Paused(uint, StandardErrors?)"/>
    /// <param name="delay">The delay time.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Paused(uint id, TimeSpan timeRemaining, StandardErrors error) => new(id, EffectStatus.Paused, timeRemaining, error);
    
    /// <summary>Creates a response indicating that the effect has resumed.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="message">The message to be displayed to the user, if applicable.</param>
    public static EffectResponse Resumed(uint id, string? message = null) => new(id, EffectStatus.Resumed, 0, message);

    /// <inheritdoc cref="Paused(uint, string?)"/>
    /// <param name="delay">The delay time in milliseconds.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Resumed(uint id, long timeRemaining, string? message = null) => new(id, EffectStatus.Resumed, timeRemaining, message);

    /// <inheritdoc cref="Paused(uint, string?)"/>
    /// <param name="delay">The delay time.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Resumed(uint id, TimeSpan timeRemaining, string? message = null) => new(id, EffectStatus.Resumed, timeRemaining, message);

    /// <summary>Creates a response indicating that the effect has resumed.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="error">The standard ID of the error.</param>
    public static EffectResponse Resumed(uint id, StandardErrors error) => new(id, EffectStatus.Resumed, 0, error);

    /// <inheritdoc cref="Paused(uint, StandardErrors?)"/>
    /// <param name="delay">The delay time in milliseconds.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Resumed(uint id, long timeRemaining, StandardErrors error) => new(id, EffectStatus.Resumed, timeRemaining, error);

    /// <inheritdoc cref="Paused(uint, StandardErrors?)"/>
    /// <param name="delay">The delay time.</param>
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public static EffectResponse Resumed(uint id, TimeSpan timeRemaining, StandardErrors error) => new(id, EffectStatus.Resumed, timeRemaining, error);

    /// <summary>Creates a response indicating that the effect has finished.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="message">The message to be displayed to the user, if applicable.</param>
    public static EffectResponse Finished(uint id, string? message = null) => new(id, EffectStatus.Finished, 0, message);
    
    /// <summary>Creates a response indicating that the effect has finished.</summary>
    /// <param name="id">The ID of the request that this response is for.</param>
    /// <param name="error">The standard ID of the error.</param>
    public static EffectResponse Finished(uint id, StandardErrors error) => new(id, EffectStatus.Finished, 0, error);
}