using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
#if NET8_0_OR_GREATER
using System.Runtime.Loader;
#endif

namespace ConnectorLib.JSON;

// UPDATES TO THIS CLASS SHOULD BE COPIED TO ConnectorLib.JSON/VersionNumber.cs, AND VICE VERSA, AS THEY ARE INTENDED TO BE IDENTICAL.

/// <summary>Represents a version number.</summary>
#if !NETSTANDARD1_0
[Serializable]
#endif
[JsonConverter(typeof(Converter))]
public class VersionNumber : IEquatable<VersionNumber>, IComparable<VersionNumber>
{
    /// <summary>Predefined version number zero.</summary>
    public static readonly VersionNumber Zero = new(0);

    /// <summary>Predefined version number one.</summary>
    public static readonly VersionNumber One = new(1);

    private readonly uint[] _version;

    /// <summary>Creates a new instance of the <see cref="VersionNumber"/> class.</summary>
    /// <param name="value">The version number as a string.</param>
    public VersionNumber(string value)
        => _version = value.Split('.').Select(v => uint.TryParse(Sanitize(v), out uint i) ? i : 0).ToArray();

    /// <summary>Creates a new instance of the <see cref="VersionNumber"/> class.</summary>
    /// <param name="values">The version number as a collection of unsigned integers.</param>
    public VersionNumber(IEnumerable<uint> values) => _version = values.ToArray();

    /// <summary>Creates a new instance of the <see cref="VersionNumber"/> class.</summary>
    /// <param name="version">The version number as a <see cref="VersionNumber"/> instance.</param>
    public VersionNumber(VersionNumber version) => _version = version._version.ToArray();

#if NET6_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
    /// <summary>Creates a new instance of the <see cref="VersionNumber"/> class.</summary>
    /// <param name="values">The version number as a <see cref="Span{T}"/> of unsigned integers.</param>
    public VersionNumber(params Span<uint> values) => _version = values.ToArray();
#else
    /// <summary>Creates a new instance of the <see cref="VersionNumber"/> class.</summary>
    /// <param name="values">The version number as an array of unsigned integers.</param>
    public VersionNumber(params uint[] values) => _version = values.ToArray();
#endif

    /// <summary>Creates a <see cref="VersionNumber"/> from a <see cref="string"/> value.</summary>
    /// <param name="value">The version number as a string.</param>
    /// <returns>A <see cref="VersionNumber"/> with <paramref name="value"/> value.</returns>
    public static implicit operator VersionNumber(string value) => new(value);

    /// <summary>Creates a <see cref="VersionNumber"/> from a <see cref="uint"/> value.</summary>
    /// <param name="value">The version number as an unsigned integer.</param>
    /// <returns>A <see cref="VersionNumber"/> with <paramref name="value"/> value.</returns>
    public static implicit operator VersionNumber(uint value) => new(value);

    /// <summary>Creates a <see cref="VersionNumber"/> from a <see cref="uint"/> array.</summary>
    /// <param name="values">The version number as an array of unsigned integers.</param>
    /// <returns>A <see cref="VersionNumber"/> with <paramref name="values"/> value.</returns>
    public static implicit operator VersionNumber(uint[] values) => new(values);

#if NET6_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
    /// <summary>Creates a <see cref="VersionNumber"/> from a <see cref="uint"/> span.</summary>
    /// <param name="values">The version number as a <see cref="Span{T}"/> of unsigned integers.</param>
    /// <returns>A <see cref="VersionNumber"/> with <paramref name="values"/> value.</returns>
    public static implicit operator VersionNumber(Span<uint> values) => new(values);
#endif

    /// <summary>Creates a <see cref="VersionNumber"/> from a <see cref="uint"/> list.</summary>
    /// <param name="values">The version number as a <see cref="List{T}"/> of unsigned integers.</param>
    /// <returns>A <see cref="VersionNumber"/> with <paramref name="values"/> value.</returns>
    public static implicit operator VersionNumber(List<uint> values) => new((IEnumerable<uint>)values);

    /// <summary>Creates a <see cref="string"/> value from a <see cref="VersionNumber"/>.</summary>
    /// <param name="value">The version number as a <see cref="string"/>.</param>
    /// <returns>A <see cref="VersionNumber"/> with <paramref name="value"/> value.</returns>
    public static implicit operator string(VersionNumber value) => value.ToString();

    /// <summary>Creates a <see cref="uint"/> array from a <see cref="VersionNumber"/>.</summary>
    /// <param name="values">The version number as an array of unsigned integers.</param>
    /// <returns>A <see cref="VersionNumber"/> with <paramref name="values"/> value.</returns>
    public static implicit operator uint[](VersionNumber value) => value._version.ToArray();

    /// <summary>Compares two <see cref="VersionNumber"/> instances for equality.</summary>
    /// <param name="a">The first <see cref="VersionNumber"/> to compare.</param>
    /// <param name="b">The second <see cref="VersionNumber"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="VersionNumber"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(VersionNumber? a, VersionNumber? b) => Equals(a, b);

    /// <summary>Compares two <see cref="VersionNumber"/> instances for inequality.</summary>
    /// <param name="a">The first <see cref="VersionNumber"/> to compare.</param>
    /// <param name="b">The second <see cref="VersionNumber"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="VersionNumber"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(VersionNumber? a, VersionNumber? b) => !Equals(a, b);

    /// <summary>Compares two <see cref="VersionNumber"/> instances.</summary>
    /// <param name="a">The first <see cref="VersionNumber"/> to compare.</param>
    /// <param name="b">The second <see cref="VersionNumber"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="VersionNumber"/> is less than the second; otherwise, <c>false</c>.</returns>
    public static bool operator <(VersionNumber? a, VersionNumber? b) => CompareTo(a, b) < 0;

    /// <summary>Compares two <see cref="VersionNumber"/> instances.</summary>
    /// <param name="a">The first <see cref="VersionNumber"/> to compare.</param>
    /// <param name="b">The second <see cref="VersionNumber"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="VersionNumber"/> is greater than the second; otherwise, <c>false</c>.</returns>
    public static bool operator >(VersionNumber? a, VersionNumber? b) => CompareTo(a, b) > 0;

    /// <summary>Compares two <see cref="VersionNumber"/> instances.</summary>
    /// <param name="a">The first <see cref="VersionNumber"/> to compare.</param>
    /// <param name="b">The second <see cref="VersionNumber"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="VersionNumber"/> is less than or equal to the second; otherwise, <c>false</c>.</returns>
    public static bool operator <=(VersionNumber? a, VersionNumber? b) => CompareTo(a, b) <= 0;

    /// <summary>Compares two <see cref="VersionNumber"/> instances.</summary>
    /// <param name="a">The first <see cref="VersionNumber"/> to compare.</param>
    /// <param name="b">The second <see cref="VersionNumber"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="VersionNumber"/> is greater than or equal to the second; otherwise, <c>false</c>.</returns>
    public static bool operator >=(VersionNumber? a, VersionNumber? b) => CompareTo(a, b) >= 0;

    /// <summary>Returns a string representation of the version number.</summary>
    public override string ToString()
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        => string.Join('.', _version.Select(v => v.ToString("D")));
#elif NET35
        => string.Join(".", _version.Select(v => v.ToString("D")).ToArray());
#else
        => string.Join(".", _version.Select(v => v.ToString("D")));
#endif

    /// <summary>Compares two <see cref="VersionNumber"/> instances for equality.</summary>
    /// <param name="a">The first <see cref="VersionNumber"/> to compare.</param>
    /// <param name="b">The second <see cref="VersionNumber"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="VersionNumber"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool Equals(VersionNumber? a, VersionNumber? b)
    {
        if (ReferenceEquals(a, null)) return ReferenceEquals(b, null);
        return a.Equals(b);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType().IsAssignableTo(typeof(VersionNumber))) return false;
        return Equals((VersionNumber)obj);
    }

    public override int GetHashCode() => _version.GetHashCode();

    public bool Equals(VersionNumber? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        int length = _version.Length;
        if (length != other._version.Length) return false;
        for (int i = 0; i < length; i++)
        {
            if (_version[i] != other._version[i]) return false;
        }
        return true;
    }

    /// <summary>Compares two <see cref="VersionNumber"/> instances.</summary>
    /// <param name="a">The first <see cref="VersionNumber"/> to compare.</param>
    /// <param name="b">The second <see cref="VersionNumber"/> to compare.</param>
    /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
    /// 
    /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> The value of <paramref name="a"/> precedes <paramref name="b"/> in the sort order.</description></item><item><term> Zero</term><description> The value of <paramref name="a"/> occurs in the same position in the sort order as <paramref name="b"/>.</description></item><item><term> Greater than zero</term><description> The value of <paramref name="a"/> follows <paramref name="b"/> in the sort order.</description></item></list></returns>
    public static int CompareTo(VersionNumber? a, VersionNumber? b)
    {
        if (ReferenceEquals(a, null)) return ReferenceEquals(b, null) ? 0 : -1;
        return a.CompareTo(b);
    }

    public int CompareTo(VersionNumber? other)
    {
        if (ReferenceEquals(null, other)) return 1;

        int length = _version.Length;
        int otherLength = other._version.Length;

        int minLength = Math.Min(length, otherLength);
        int i;
        for (i = 0; i < minLength; i++)
        {
            uint val = _version[i];
            uint otherVal = other._version[i];
            if (val == otherVal) continue;
            if (val > otherVal) return 1;
            return -1;
        }

        if (length == otherLength) return 0;
        if (length > otherLength)
        {
            for (; i < length; i++)
            {
                if (_version[i] == 0) continue;
                return 1;
            }
            return 0;
        }
        for (; i < otherLength; i++)
        {
            if (other._version[i] == 0) continue;
            return -1;
        }
        return 0;
    }

#if NET8_0_OR_GREATER
    private class CollectableLoadContext() : AssemblyLoadContext(isCollectible: true)
    {
        public static Assembly LoadFrom(string path)
        {
            return (new CollectableLoadContext()).LoadFromAssemblyPath(path);
        }

        public static Assembly Load(string name)
        {
            return (new CollectableLoadContext()).LoadFromAssemblyName(new AssemblyName(name));
        }
    }

    private static Assembly? GetAssemblyByName(string name, string? path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            //append .dll to name only if needed
            if (!name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                name += ".dll";
            string assemblyPath = Path.Combine(path, name);
            if (File.Exists(assemblyPath))
            {
                try { return CollectableLoadContext.LoadFrom(assemblyPath); }
                catch { /**/ }
            }
            return null;
        }

        Assembly? result = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == name);
        if (result == null)
        {
            try { result = CollectableLoadContext.Load(name); }
            catch { /**/ }
        }
        return result;
    }

    private static IEnumerable<Assembly> GetPackAssemblies(string? path = null) =>
        (new[]
        {
            GetAssemblyByName("CrowdControl.Common", path),
            GetAssemblyByName("ConnectorLib", path),
            GetAssemblyByName("CrowdControl.Games", path)
        }).Where(a => (a != null))!;

    /// <summary>Gets the version of the CrowdControl assemblies.</summary>
    public static VersionNumber GetPackVersion(string? path = null) => GetAssemblyVersion(GetPackAssemblies(path));
#endif

#if NETSTANDARD2_0_OR_GREATER || NET6_0_OR_GREATER
    /// <summary>Gets the version of the provided assemblies.</summary>
    /// <param name="assemblies">The assemblies to get the version from.</param>
    /// <returns>
    /// The version of the provided assemblies.
    /// If <paramref name="assemblies"/> is null or empty, the method will return <see cref="Zero"/>.
    /// </returns>
    public static VersionNumber GetAssemblyVersion(IEnumerable<Assembly> assemblies)
    {
        VersionNumber latest = Zero;
        foreach (Assembly assembly in assemblies)
        {
            VersionNumber next;
            try { next = GetAssemblyVersion(assembly); }
            catch { next = Zero; }
            if (next > latest) latest = next;
        }
        return latest;
    }

    /// <summary>Gets the version of the provided assembly.</summary>
    /// <param name="assembly">The assembly to get the version from.</param>
    /// <returns>
    /// The version of the provided assembly.
    /// If <paramref name="assembly"/> is null, the method will return <see cref="Zero"/>.
    /// </returns>
    public static VersionNumber GetAssemblyVersion(Assembly assembly)
    {
        string? current = null;
        // custom attribute
        object[] attrs = assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
        if (attrs is { Length: > 0 }) current = ((AssemblyInformationalVersionAttribute)attrs[0]).InformationalVersion;

        // win32 version info
        if ((current is null) || (current.Length == 0))
        {
            current = GetFileVersionInfo(assembly).ProductVersion;
            current = current?.Trim();
        }

        // fake it
        if (current is null || current.Length == 0) current = Zero;
        return current;
    }
#endif

#if !NET35
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    /// <summary>Sanitizes the provided version string by removing any non-digit characters.</summary>
    /// <param name="value">The version string to sanitize.</param>
    /// <returns>The sanitized version string containing only digits and dots.</returns>
    private static string Sanitize(string value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (char.IsDigit(value[i])) continue;
            return value.Substring(0, i);
        }
        return value;
    }

#if NETSTANDARD2_0_OR_GREATER || NET6_0_OR_GREATER
    /// <summary>Gets the file version info of the provided assembly.</summary>
    /// <param name="assembly">The assembly to get the file version info from.</param>
    /// <returns>
    /// The file version info of the provided assembly.
    /// If <paramref name="assembly"/> is null, the method will return <see langword="null"/>.
    /// </returns>
    private static FileVersionInfo GetFileVersionInfo(Assembly assembly)
        => FileVersionInfo.GetVersionInfo(assembly.Location);
#endif

    private class Converter : JsonConverter<VersionNumber>
    {
        public override void WriteJson(JsonWriter writer, VersionNumber? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            writer.WriteValue((string)value);
        }

        public override VersionNumber? ReadJson(JsonReader reader, Type objectType, VersionNumber? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            object? value = reader.Value;
            if (value == null) return null;
            string? stringValue = value as string;
            if (stringValue == null) return null;
            return new VersionNumber(stringValue);
        }
    }
}