using System;

namespace ConnectorLib.JSON;

internal static class EnumEx
{
    public static string ToCamelCase(this Enum value) => value.ToString("G").ToCamelCase();
}