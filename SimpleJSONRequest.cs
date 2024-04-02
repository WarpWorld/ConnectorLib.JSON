using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

[Serializable]
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
    /// <returns>A <see cref="SimpleJSONRequest"/> object corresponding to the supplied JSON.</returns>
    public static SimpleJSONRequest Parse(string json) => Parse(JObject.Parse(json));
    public static SimpleJSONRequest Parse(JObject j)
    {
        switch (((((RequestType)(j.Value<byte>("type"))))))
        {
            case RequestType.Test:
            case RequestType.Start:
                return j.ToObject<EffectRequest>(JSON_SERIALIZER)!;
            case RequestType.Stop:
                return j.ToObject<EffectRequest>(JSON_SERIALIZER)!;
            case RequestType.RpcResponse:
                return j.ToObject<RpcResponse>(JSON_SERIALIZER)!;
            case RequestType.PlayerInfo:
                return j.ToObject<PlayerInfo>(JSON_SERIALIZER)!;
            case RequestType.Login:
                return j.ToObject<MessageRequest>(JSON_SERIALIZER)!;
            case RequestType.KeepAlive:
                return j.ToObject<EmptyRequest>(JSON_SERIALIZER)!;
            default:
                throw new SerializationException("Message type was missing or unknown.");
        }
    }
}
