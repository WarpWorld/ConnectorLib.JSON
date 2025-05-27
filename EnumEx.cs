using System;

namespace ConnectorLib.JSON;

/// <summary>Extensions for enums.</summary>
internal static class EnumEx
{
    /// <summary>Converts an enum value to a string in camel case.</summary>
    /// <param name="value">The enum value to convert.</param>
    /// <returns>The camel case string representation of the enum value.</returns>
    internal static string ToCamelCase(this Enum value) => value.ToString("G").ToCamelCase();
}