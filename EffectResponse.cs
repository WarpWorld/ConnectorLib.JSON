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

    /// <remarks>
    /// If applicable (messages 0x00 (sometimes) and 0x07 (always)), this should contain the time remaining on the running effect, in milliseconds.
    /// Otherwise, this field should be 0 or missing.
    /// </remarks>
    /// <remarks>
    /// This is milliseconds, not seconds.
    /// </remarks>
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

    public EffectResponse(uint id, EffectStatus status, string? message = null)
        : this(id, status, 0, message) { }

    public EffectResponse(uint id, EffectStatus status, TimeSpan timeRemaining, string? message = null)
        : this(id, status, (long)timeRemaining.TotalMilliseconds, message) { }

    [JsonConstructor]
    public EffectResponse(uint id, EffectStatus status, long timeRemaining, string? message = null)
    {
        this.id = id;
        this.status = status;
        this.timeRemaining = timeRemaining;
        this.message = message;
        type = ResponseType.EffectRequest;
    }
}