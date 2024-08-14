using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
[JsonConverter(typeof(Converter))]
public class RequestParameters :
#if NET35
    IEnumerable<IParameterValue>
#else
    IReadOnlyList<string>, IReadOnlyDictionary<string, IParameterValue> //this unfortunately means you have to cast this when using linq
#endif
{
    private readonly Dictionary<string, IParameterValue> _parameters;
    private readonly List<string> _parameter_list;

    private RequestParameters()
    {
        _parameters = new();
        _parameter_list = new();
    }

    public RequestParameters(IEnumerable<IParameterValue> list)
    {
        list = list.ToArray();
        _parameters = list.ToDictionary(d => d.ID);
        _parameter_list = list.Select(v => v.Value.ToString()).ToList();
    }

    public RequestParameters(IEnumerable<KeyValuePair<string, IParameterValue>> parameters)
    {
        _parameters = parameters.ToDictionary();
        _parameter_list = _parameters.Values.Select(p => p.Value.ToString()).ToList();
    }

#if NET35
    public IEnumerator<IParameterValue> GetEnumerator() => _parameters.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
#else
    IEnumerator<KeyValuePair<string, IParameterValue>> IEnumerable<KeyValuePair<string, IParameterValue>>.GetEnumerator()
        => _parameters.GetEnumerator();

    IEnumerator<string> IEnumerable<string>.GetEnumerator()
        => _parameters.Values.Select(v => v.Value?.ToString()).GetEnumerator();

    int IReadOnlyCollection<string>.Count => _parameters.Count;

    int IReadOnlyCollection<KeyValuePair<string, IParameterValue>>.Count => _parameters.Count;

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<string>)this).GetEnumerator();
#endif

    public string this[int index] => _parameter_list[index];

    public bool ContainsKey(string key) => _parameters.ContainsKey(key);

    public bool TryGetValue(string key, out IParameterValue value) => _parameters.TryGetValue(key, out value);

    public IParameterValue this[string key] => _parameters[key];

    public IEnumerable<string> Keys => _parameters.Keys;
    public IEnumerable<IParameterValue> Values => _parameters.Values;

    public int Count => _parameters.Count;

    public IEnumerable<string> Where(Func<string, bool> predicate)
        => ((IEnumerable<string>)this).Where(predicate);

    public IEnumerable<TResult> Select<TResult>(Func<string, TResult> selector)
        => ((IEnumerable<string>)this).Select(selector);

    public IEnumerable<TResult> SelectMany<TResult>(Func<string, IEnumerable<TResult>> selector)
        => ((IEnumerable<string>)this).SelectMany(selector);

    public IEnumerable<TResult> SelectMany<TResult>(Func<string, int, IEnumerable<TResult>> selector)
        => ((IEnumerable<string>)this).SelectMany(selector);

    public string First()
        => ((IEnumerable<string>)this).First();

    public string First(Func<string, bool> predicate)
        => ((IEnumerable<string>)this).First(predicate);

    public string? FirstOrDefault()
        => ((IEnumerable<string>)this).FirstOrDefault();

    public string FirstOrDefault(string defaultValue)
        => ((IEnumerable<string>)this).FirstOrDefault() ?? defaultValue;

    public string? FirstOrDefault(Func<string, bool> predicate)
        => ((IEnumerable<string>)this).FirstOrDefault(predicate);

    public string FirstOrDefault(Func<string, bool> predicate, string defaultValue)
        => ((IEnumerable<string>)this).FirstOrDefault(predicate) ?? defaultValue;

    public bool Any()
        => ((IEnumerable<string>)this).Any();

    public bool Any(Func<string, bool> predicate)
        => ((IEnumerable<string>)this).Any(predicate);

    private class Converter : JsonConverter<RequestParameters>
    {
        public override void WriteJson(JsonWriter writer, RequestParameters? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value?._parameters);
        }

        public override RequestParameters? ReadJson(JsonReader reader, Type objectType, RequestParameters? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            /*
             * {
                  'age': {
                    type: 'options',
                    title: 'Adult',
                    value: 'a',
                  },
                  'color': {
                    type: 'hex-color',
                    title: 'Color',
                    value: '#ff0000',
                  }
                }
             */
            JObject j = JObject.Load(reader);
            List<IParameterValue> values = new();
            foreach (var kv in j)
            {
                string id = kv.Key;
                string name = kv.Value["name"].Value<string>();
                ParameterBase.ParameterType type = kv.Value["type"].Value<ParameterBase.ParameterType>();
                switch (type)
                {
                    case ParameterBase.ParameterType.Options:
                        {
                            string value = kv.Value["value"].Value<string>();
                            values.Add(new ParameterValue<string>(name, id, value));
                            break;
                        }
                    case ParameterBase.ParameterType.HexColor:
                        {
                            string value = kv.Value["value"].Value<string>();
                            if (HexColorConverter.TryParse(value, out ParameterColorValue color))
                                values.Add(new ParameterColor(name, id, color));
                            break;
                        }
                    default:
                        throw new SerializationException();
                }
            }
            return new RequestParameters(values);
        }
    }
}

//[JsonConverter(typeof(ParameterBase.Converter))]
public interface IParameterValue
{
    string ID { get; }
    string Name { get; }
    ParameterBase.ParameterType Type { get; }
    object? Value { get; }
}

//[JsonConverter(typeof(Converter))]
public abstract class ParameterBase
{
    /*
     *     parameters?: Record<string, {
      type: 'options' | 'hex-color'
      title: string
      value: string
    }>
     */
    /// <summary>
    /// The index name of the group.
    /// </summary>
    [JsonIgnore]
    public readonly string ID;

    /// <summary>
    /// The display name of the group.
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public readonly string Name;

    /// <summary>
    /// The display name of the group.
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public readonly ParameterType Type;

    [JsonConverter(typeof(AnnotatedEnumConverter<ParameterType>))]
    public enum ParameterType
    {
        [AnnotatedEnumConverter<ParameterType>.JsonValueAttribute("options")]
        Options = 0, //this is the default if the value is missing
        [AnnotatedEnumConverter<ParameterType>.JsonValueAttribute("hex-color")]
        HexColor
    }

    protected ParameterBase(string name, string id, ParameterType type)
    {
        ID = id;
        Name = name;
        Type = type;
    }

    /*public class Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(ParameterBase); //do not work for subtypes or this will get stuck in a recursion loop

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            serializer.Serialize(writer, value, value.GetType());
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject j = JObject.Load(reader);
            ParameterType type = j["type"]?.Value<ParameterType>() ?? ParameterType.Options;
            switch (type)
            {
                case ParameterType.Options:
                    return j.ToObject<ParameterValue<string>>();
                case ParameterType.HexColor:
                    return j.ToObject<ParameterColor>();
                default:
                    throw new SerializationException("Unknown parameter type.");
            }
        }
    }*/
}