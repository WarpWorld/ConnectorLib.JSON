using Newtonsoft.Json;

namespace ConnectorLib.JSON;

public abstract class SimpleJSONMessage
{
    /// <summary>
    /// The JSON serializer settings required by the Crowd Control SimpleJSON protocol.
    /// </summary>
    public static readonly JsonSerializer JSON_SERIALIZER = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore,
        Formatting = Formatting.None
    };

    private static int _next_id = 0;

    /// <summary>
    /// Gets the next block ID.
    /// </summary>
    /// <remarks>
    /// This field is intended for use by the Crowd Control client.
    /// It is not intended for use by game plugin developers.
    /// </remarks>
    public static uint NextID
    {
        get
        {
            uint id;
            // ReSharper disable once EmptyEmbeddedStatement
            while ((id = unchecked((uint)Interlocked.Increment(ref _next_id))) == 0) ;
            return id;
        }
    }

    /// <summary>
    /// The message ID of the message to which this is a response.
    /// </summary>
    public abstract uint ID { get; }

    /// <summary>
    /// True if this message is a keepalive/heartbeat, otherwise false.
    /// </summary>
    public abstract bool IsKeepAlive { get; }
}
