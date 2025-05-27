using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

/// <summary>A collection of request parameters.</summary>
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
    /// <summary>A dictionary that holds the parameters by their ID.</summary>
    private readonly Dictionary<string, IParameterValue> _parameters;
    
    /// <summary>A list of parameter identifiers.</summary>
    private readonly List<string> _parameter_list;

    /// <summary>Creates a new instance of the <see cref="RequestParameters"/> class.</summary>
    private RequestParameters()
    {
        _parameters = new();
        _parameter_list = new();
    }

    /// <inheritdoc cref="RequestParameters()"/>
    /// <param name="parameters">The parameters to initialize the collection with.</param>
    public RequestParameters(IEnumerable<IParameterValue> parameters)
    {
        parameters = parameters.ToArray();
        _parameters = parameters.ToDictionary(d => d.ID);
        _parameter_list = parameters.Select(v => v.Value.ToString()).ToList();
    }

    /// <inheritdoc cref="RequestParameters(IEnumerable{IParameterValue})"/>
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

    /// <inheritdoc cref="IReadOnlyCollection{T}.Count"/>
    public int Count => _parameters.Count;

    /// <inheritdoc cref="System.Linq.Enumerable.Where{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
    public IEnumerable<string> Where(Func<string, bool> predicate)
        => ((IEnumerable<string>)this).Where(predicate);

    /// <inheritdoc cref="System.Linq.Enumerable.Select{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})"/>
    public IEnumerable<TResult> Select<TResult>(Func<string, TResult> selector)
        => ((IEnumerable<string>)this).Select(selector);

    /// <inheritdoc cref="System.Linq.Enumerable.SelectMany{TSource, TResult}(IEnumerable{TSource}, Func{TSource, IEnumerable{TResult}})"/>
    public IEnumerable<TResult> SelectMany<TResult>(Func<string, IEnumerable<TResult>> selector)
        => ((IEnumerable<string>)this).SelectMany(selector);

    /// <inheritdoc cref="System.Linq.Enumerable.SelectMany{TSource, TResult}(IEnumerable{TSource}, Func{TSource, int, IEnumerable{TResult}})"/>
    public IEnumerable<TResult> SelectMany<TResult>(Func<string, int, IEnumerable<TResult>> selector)
        => ((IEnumerable<string>)this).SelectMany(selector);

    /// <inheritdoc cref="System.Linq.Enumerable.First{TSource}(IEnumerable{TSource})"/>
    public string First()
        => ((IEnumerable<string>)this).First();

    /// <inheritdoc cref="System.Linq.Enumerable.First{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
    public string First(Func<string, bool> predicate)
        => ((IEnumerable<string>)this).First(predicate);

    /// <inheritdoc cref="System.Linq.Enumerable.FirstOrDefault{TSource}(IEnumerable{TSource})"/>
    public string? FirstOrDefault()
        => ((IEnumerable<string>)this).FirstOrDefault();

    /// <inheritdoc cref="System.Linq.Enumerable.FirstOrDefault{TSource}(IEnumerable{TSource}, TSource)"/>
    public string FirstOrDefault(string defaultValue)
        => ((IEnumerable<string>)this).FirstOrDefault() ?? defaultValue;

    /// <inheritdoc cref="System.Linq.Enumerable.FirstOrDefault{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
    public string? FirstOrDefault(Func<string, bool> predicate)
        => ((IEnumerable<string>)this).FirstOrDefault(predicate);

    /// <inheritdoc cref="System.Linq.Enumerable.FirstOrDefault{TSource}(IEnumerable{TSource}, Func{TSource, bool}, TSource)"/>
    public string FirstOrDefault(Func<string, bool> predicate, string defaultValue)
        => ((IEnumerable<string>)this).FirstOrDefault(predicate) ?? defaultValue;

    /// <inheritdoc cref="System.Linq.Enumerable.Any{TSource}(IEnumerable{TSource})"/>
    public bool Any()
        => ((IEnumerable<string>)this).Any();

    /// <inheritdoc cref="System.Linq.Enumerable.Any{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
    public bool Any(Func<string, bool> predicate)
        => ((IEnumerable<string>)this).Any(predicate);

    /// <summary>Converts the request parameters to/from a JSON string representation.</summary>
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