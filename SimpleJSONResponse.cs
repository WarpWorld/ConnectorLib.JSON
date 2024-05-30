#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
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
    /// <returns>A <see cref="SimpleJSONResponse"/> object corresponding to the supplied JSON.</returns>
    public static SimpleJSONResponse Parse(string json) => Parse(JObject.Parse(json));

    /// <summary>
    /// Parses a response object from a serialized string.
    /// </summary>
    /// <param name="json">The JSON string containing the response message.</param>
    public static SimpleJSONResponse Parse(JObject j)
    {
        switch (((((ResponseType)(j.Value<byte>("type"))))))
        {
            case ResponseType.EffectRequest:
                return j.ToObject<EffectResponse>(JSON_SERIALIZER)!;
            case ResponseType.EffectStatus:
                return j.ToObject<EffectUpdate>(JSON_SERIALIZER)!;
            case ResponseType.RpcRequest:
                return j.ToObject<RpcRequest>(JSON_SERIALIZER)!;
            case ResponseType.GenericEvent:
                return j.ToObject<GenericEvent>(JSON_SERIALIZER)!;
            case ResponseType.Login:
                return j.ToObject<EmptyResponse>(JSON_SERIALIZER)!;
            case ResponseType.LoginSuccess:
                return j.ToObject<EmptyResponse>(JSON_SERIALIZER)!;
            case ResponseType.Disconnect:
                return j.ToObject<MessageResponse>(JSON_SERIALIZER)!;
            case ResponseType.KeepAlive:
                return j.ToObject<EmptyResponse>(JSON_SERIALIZER)!;
            default:
                throw new SerializationException("Message type was missing or unknown.");
        }
    }
}
