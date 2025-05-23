#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class SimpleJSONRequest : SimpleJSONMessage
{
    public uint id = NextID;
    public RequestType type;

    /// <inheritdoc/>
    /// <remarks>
    /// This simply returns the value of <see cref="id">id</see>, as required by <see cref="SimpleJSONMessage"/>.
    /// </remarks>
    [JsonIgnore] public override uint ID => id;
    [JsonIgnore] public override bool IsKeepAlive => type == RequestType.KeepAlive;

    /// <summary>
    /// Parses a response object from a serialized string.
    /// </summary>
    /// <param name="json">The JSON string containing the response message.</param>
    /// <param name="request">A <see cref="SimpleJSONRequest"/> object corresponding to the supplied JSON.</param>
    /// <returns><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
#if NETSTANDARD2_1_OR_GREATER
    public static bool TryParse(string json, [MaybeNullWhen(false)] out SimpleJSONRequest request)
#else
    public static bool TryParse(string json, out SimpleJSONRequest? request)
#endif
        => TryParse(JObject.Parse(json), out request);

    /// <summary>
    /// Parses a response object from a serialized string.
    /// </summary>
    /// <param name="j">The JSON object containing the response message.</param>
    /// <param name="request">A <see cref="SimpleJSONRequest"/> object corresponding to the supplied JSON.</param>
    /// <returns><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
#if NETSTANDARD2_1_OR_GREATER
    public static bool TryParse(JObject j, [MaybeNullWhen(false)] out SimpleJSONRequest request)
#else
    public static bool TryParse(JObject j, out SimpleJSONRequest? request)
#endif
    {
        try
        {
            JToken? typeToken = j.GetValue("type");
            RequestType type;
            if (typeToken != null) type = (RequestType)CamelCaseStringEnumConverter.ReadJToken(typeToken, typeof(RequestType));
            else type = default;
            switch (type)
            {
                case RequestType.EffectTest:
                case RequestType.EffectStart:
                    request = j.ToObject<EffectRequest>(JSON_SERIALIZER)!;
                    return true;
                case RequestType.EffectStop:
                    request = j.ToObject<EffectRequest>(JSON_SERIALIZER)!;
                    return true;
                case RequestType.DataRequest:
                    request = j.ToObject<DataRequest>(JSON_SERIALIZER)!;
                    return true;
                case RequestType.RpcResponse:
                    request = j.ToObject<RpcResponse>(JSON_SERIALIZER)!;
                    return true;
                case RequestType.PlayerInfo:
                    request = j.ToObject<PlayerInfo>(JSON_SERIALIZER)!;
                    return true;
                case RequestType.Login:
                    request = j.ToObject<MessageRequest>(JSON_SERIALIZER)!;
                    return true;
                case RequestType.GameUpdate:
                    request = j.ToObject<EmptyRequest>(JSON_SERIALIZER)!;
                    return true;
                case RequestType.KeepAlive:
                    request = j.ToObject<EmptyRequest>(JSON_SERIALIZER)!;
                    return true;
                default:
                    goto fail;
            }
        }
        catch { /**/ }
        fail:
        request = null;
        return false;
    }
}