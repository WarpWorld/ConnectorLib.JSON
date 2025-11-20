#if NETSTANDARD2_0_OR_GREATER && !NO_REFLECTION
using System;
using System.IO;
using System.Reflection;
#endif
using System.Threading;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

/// <summary>Base class for all Crowd Control SimpleJSON messages.</summary>
public abstract class SimpleJSONMessage
{
#if NETSTANDARD2_0_OR_GREATER && !NO_REFLECTION
    static SimpleJSONMessage()
    {
        AppDomain.CurrentDomain.AssemblyResolve += (_, args) =>
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), new AssemblyName(args.Name).Name + ".dll");
            return File.Exists(path) ? Assembly.LoadFrom(path) : null;
        };
    }
#endif
    
    /// <summary>The JSON serializer settings required by the Crowd Control SimpleJSON protocol.</summary>
    public static readonly JsonSerializerSettings JSON_SERIALIZER_SETTINGS = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore,
        Formatting = Formatting.None
    };

    /// <summary>The JSON serializer settings required by the Crowd Control SimpleJSON protocol.</summary>
    public static readonly JsonSerializer JSON_SERIALIZER = new()
    {
        NullValueHandling = JSON_SERIALIZER_SETTINGS.NullValueHandling,
        MissingMemberHandling = JSON_SERIALIZER_SETTINGS.MissingMemberHandling,
        Formatting = JSON_SERIALIZER_SETTINGS.Formatting
    };

    private static int _next_id = 0;

    /// <summary>Gets the next block ID.</summary>
    /// <remarks>
    /// This field is intended for use by the Crowd Control client.
    /// It is not intended for use by game/plugin developers.
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

    /// <summary>The message ID of the message to which this is a response.</summary>
    public abstract uint ID { get; }

    /// <summary><c>true</c> if this message is a keepalive/heartbeat; otherwise, <c>false</c>.</summary>
    public abstract bool IsKeepAlive { get; }

    /// <summary>Serializes this message to a JSON string.</summary>
    public string Serialize() => JsonConvert.SerializeObject(this, JSON_SERIALIZER_SETTINGS);
}
