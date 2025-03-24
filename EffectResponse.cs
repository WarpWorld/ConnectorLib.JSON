#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class EffectResponse : SimpleJSONResponse
{
    public EffectStatus status;
    public string? message;
    public StandardErrors messageID;

    /// <remarks>
    /// If applicable (messages 0x00 (sometimes) and 0x07 (always)), this should contain the time remaining on the running effect, in milliseconds.
    /// Otherwise, this field should be 0 or missing.
    /// </remarks>
    /// <remarks>This is in milliseconds, not seconds.</remarks>
    public long timeRemaining; //milliseconds

    [JsonConverter(typeof(MetadataConverter))]
    public Dictionary<string, DataResponse>? metadata;

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

    public EffectResponse() { }
    
    public EffectResponse(uint id, EffectStatus status, StandardErrors messageID)
        : this(id, status, 0, messageID) { }
    
    public EffectResponse(uint id, EffectStatus status, string? message = null)
        : this(id, status, 0, message) { }

    public EffectResponse(uint id, EffectStatus status, TimeSpan timeRemaining, StandardErrors messageID)
        : this(id, status, (long)timeRemaining.TotalMilliseconds, messageID) { }

    public EffectResponse(uint id, EffectStatus status, TimeSpan timeRemaining, string? message = null)
        : this(id, status, (long)timeRemaining.TotalMilliseconds, message) { }

    public EffectResponse(uint id, EffectStatus status, long timeRemaining, StandardErrors messageID)
        : this(id, status, timeRemaining) => this.messageID = messageID;

    [JsonConstructor]
    public EffectResponse(uint id, EffectStatus status, long timeRemaining, string? message = null)
    {
        this.id = id;
        this.status = status;
        this.timeRemaining = timeRemaining;
        this.message = message;
        type = ResponseType.EffectRequest;
    }

    public static EffectResponse Success(uint id, string? message = null) => new(id, EffectStatus.Success, message);
    public static EffectResponse Success(uint id, long delay, string? message = null) => new(id, EffectStatus.Success, delay, message);
    public static EffectResponse Success(uint id, TimeSpan delay, string? message = null) => new(id, EffectStatus.Success, delay, message);

    public static EffectResponse Failure(uint id, string? message = null) => new(id, EffectStatus.Failure, message);
    public static EffectResponse Failure(uint id, StandardErrors error) => new(id, EffectStatus.Failure, error);

    public static EffectResponse Unavailable(uint id, string? message = null) => new(id, EffectStatus.Unavailable, message);
    public static EffectResponse Unavailable(uint id, StandardErrors error) => new(id, EffectStatus.Unavailable, error);

    public static EffectResponse Retry(uint id, string? message = null) => new(id, EffectStatus.Retry, 0, message);
    public static EffectResponse Retry(uint id, long delay, string? message = null) => new(id, EffectStatus.Retry, delay, message);
    public static EffectResponse Retry(uint id, TimeSpan delay, string? message = null) => new(id, EffectStatus.Retry, delay, message);
    public static EffectResponse Retry(uint id, StandardErrors error) => new(id, EffectStatus.Retry, 0, error);
    public static EffectResponse Retry(uint id, long delay, StandardErrors error) => new(id, EffectStatus.Retry, delay, error);
    public static EffectResponse Retry(uint id, TimeSpan delay, StandardErrors error) => new(id, EffectStatus.Retry, delay, error);

    public static EffectResponse Paused(uint id, string? message = null) => new(id, EffectStatus.Paused, 0, message);
    public static EffectResponse Paused(uint id, long timeRemaining, string? message = null) => new(id, EffectStatus.Paused, timeRemaining, message);
    public static EffectResponse Paused(uint id, TimeSpan timeRemaining, string? message = null) => new(id, EffectStatus.Paused, timeRemaining, message);
    public static EffectResponse Paused(uint id, StandardErrors error) => new(id, EffectStatus.Paused, 0, error);
    public static EffectResponse Paused(uint id, long timeRemaining, StandardErrors error) => new(id, EffectStatus.Paused, timeRemaining, error);
    public static EffectResponse Paused(uint id, TimeSpan timeRemaining, StandardErrors error) => new(id, EffectStatus.Paused, timeRemaining, error);

    public static EffectResponse Resumed(uint id, string? message = null) => new(id, EffectStatus.Resumed, 0, message);
    public static EffectResponse Resumed(uint id, long timeRemaining, string? message = null) => new(id, EffectStatus.Resumed, timeRemaining, message);
    public static EffectResponse Resumed(uint id, TimeSpan timeRemaining, string? message = null) => new(id, EffectStatus.Resumed, timeRemaining, message);
    public static EffectResponse Resumed(uint id, StandardErrors error) => new(id, EffectStatus.Resumed, 0, error);
    public static EffectResponse Resumed(uint id, long timeRemaining, StandardErrors error) => new(id, EffectStatus.Resumed, timeRemaining, error);
    public static EffectResponse Resumed(uint id, TimeSpan timeRemaining, StandardErrors error) => new(id, EffectStatus.Resumed, timeRemaining, error);

    public static EffectResponse Finished(uint id, string? message = null) => new(id, EffectStatus.Finished, 0, message);
    public static EffectResponse Finished(uint id, StandardErrors error) => new(id, EffectStatus.Finished, 0, error);
}