#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class SimpleJSONResponse : SimpleJSONMessage
{
    /// <inheritdoc cref="SimpleJSONMessage.ID"/>
    /// <remarks>
    /// If the message is a response to an effect request or a followup on same, this should match the ID of the originating request.
    /// Otherwise, this field should be 0 or missing.
    /// </remarks>
    public uint id; //this may be 0 or omitted if the message is about a class of effect (0x80 series)

    /// <summary>
    /// The type of message being sent.
    /// </summary>
    /// <remarks>
    /// Not all of these are necessarily "responses" to anything.
    /// </remarks>
    public ResponseType type;

    /// <inheritdoc/>
    /// <remarks>
    /// This simply returns the value of <see cref="id">id</see>, as required by <see cref="SimpleJSONMessage"/>.
    /// </remarks>
    [JsonIgnore] public override uint ID => id;

    [JsonIgnore] public override bool IsKeepAlive => type == ResponseType.KeepAlive;

    [JsonIgnore] public static SimpleJSONResponse KeepAlive { get; } = new EmptyResponse { type = ResponseType.KeepAlive };

    /// <summary>
    /// Parses a response object from a serialized string.
    /// </summary>
    /// <param name="json">The JSON string containing the response message.</param>
    /// <param name="response">A <see cref="SimpleJSONResponse"/> object corresponding to the supplied JSON.</param>
    /// <returns><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
#if NETSTANDARD2_1_OR_GREATER
    public static bool TryParse(string json, [MaybeNullWhen(false)] out SimpleJSONResponse response)
#else
    public static bool TryParse(string json, out SimpleJSONResponse? response)
#endif
        => TryParse(JObject.Parse(json), out response);

    /// <summary>
    /// Parses a response object from a serialized string.
    /// </summary>
    /// <param name="j">The JSON object containing the response message.</param>
    /// <param name="response">A <see cref="SimpleJSONResponse"/> object corresponding to the supplied JSON.</param>
    /// <returns><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
#if NETSTANDARD2_1_OR_GREATER
    public static bool TryParse(JObject j, [MaybeNullWhen(false)] out SimpleJSONResponse response)
#else
    public static bool TryParse(JObject j, out SimpleJSONResponse? response)
#endif
    {
        try
        {
            JToken? typeToken = j.GetValue("type");
            ResponseType type;
            if (typeToken != null) type = (ResponseType)CamelCaseStringEnumConverter.ReadJToken(typeToken, typeof(ResponseType));
            else type = default;
            switch (type)
            {
                case ResponseType.EffectRequest:
                    response = j.ToObject<EffectResponse>(JSON_SERIALIZER)!;
                    return true;
                case ResponseType.EffectStatus:
                    response = j.ToObject<EffectUpdate>(JSON_SERIALIZER)!;
                    return true;
                case ResponseType.RpcRequest:
                    response = j.ToObject<RpcRequest>(JSON_SERIALIZER)!;
                    return true;
                case ResponseType.GenericEvent:
                    response = j.ToObject<GenericEventResponse>(JSON_SERIALIZER)!;
                    return true;
                case ResponseType.DataResponse:
                    response = j.ToObject<DataResponse>(JSON_SERIALIZER)!;
                    return true;
                case ResponseType.Login:
                    response = j.ToObject<EmptyResponse>(JSON_SERIALIZER)!;
                    return true;
                case ResponseType.LoginSuccess:
                    response = j.ToObject<EmptyResponse>(JSON_SERIALIZER)!;
                    return true;
                case ResponseType.GameUpdate:
                    response = j.ToObject<GameUpdate>(JSON_SERIALIZER)!;
                    return true;
                case ResponseType.Disconnect:
                    response = j.ToObject<MessageResponse>(JSON_SERIALIZER)!;
                    return true;
                case ResponseType.KeepAlive:
                    response = j.ToObject<EmptyResponse>(JSON_SERIALIZER)!;
                    return true;
                default:
                    goto fail;
            }
        }
        catch { /**/ }
        fail:
        response = null;
        return false;
    }
}
