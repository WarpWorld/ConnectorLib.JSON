using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConnectorLib.JSON;

internal class HexColorConverter : JsonConverter<ParameterColorValue>
{
    private static readonly Dictionary<char, byte> CHAR_LOOKUP = new()
    {
        {'0',0x0},
        {'1',0x1},
        {'2',0x2},
        {'3',0x3},
        {'4',0x4},
        {'5',0x5},
        {'6',0x6},
        {'7',0x7},
        {'8',0x8},
        {'9',0x9},
        {'A',0xA},
        {'B',0xB},
        {'C',0xC},
        {'D',0xD},
        {'E',0xE},
        {'F',0xF}
    };

    public override void WriteJson(JsonWriter writer, ParameterColorValue value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, $"#{(value.A != 0xFF ? value.A.ToString("X2") : string.Empty)}{value.R:X2}{value.G:X2}{value.B:X2}");
    }

    public override ParameterColorValue ReadJson(JsonReader reader, Type objectType, ParameterColorValue existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string? value = serializer.Deserialize<string>(reader);
        if (TryParse(value, out ParameterColorValue color)) return color;
        throw new SerializationException("Unrecognized color code.");
    }

    public static bool TryParse(string value, out ParameterColorValue color)
    {
        if (value == null)
        {
            color = default;
            return false;
        }

        value = value.TrimStart('#');
        switch (value.Length)
        {
            case 6:
                {
                    string[] hexStrings = Chop(value, 2);
                    byte r = byte.TryParse(hexStrings[0], NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out byte v) ? v : (byte)0;
                    byte g = byte.TryParse(hexStrings[1], NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out v) ? v : (byte)0;
                    byte b = byte.TryParse(hexStrings[2], NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out v) ? v : (byte)0;
                    color = ParameterColorValue.FromArgb(r, g, b);
                    return true;
                }
            case 8:
                {
                    string[] hexStrings = Chop(value, 2);
                    byte a = byte.TryParse(hexStrings[0], NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out byte v) ? v : (byte)0;
                    byte r = byte.TryParse(hexStrings[1], NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out v) ? v : (byte)0;
                    byte g = byte.TryParse(hexStrings[2], NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out v) ? v : (byte)0;
                    byte b = byte.TryParse(hexStrings[3], NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out v) ? v : (byte)0;
                    color = ParameterColorValue.FromArgb(a, r, g, b);
                    return true;
                }
            case 3:
                {
                    byte r = CHAR_LOOKUP.TryGetValue(value[0], out byte v) ? (byte)(v * 0x10) : (byte)0;
                    byte g = CHAR_LOOKUP.TryGetValue(value[1], out v) ? (byte)(v * 0x10) : (byte)0;
                    byte b = CHAR_LOOKUP.TryGetValue(value[2], out v) ? (byte)(v * 0x10) : (byte)0;
                    color = ParameterColorValue.FromArgb(r, g, b);
                    return true;
                }
            case 4:
                {
                    byte a = CHAR_LOOKUP.TryGetValue(value[0], out byte v) ? (byte)(v * 0x10) : (byte)0;
                    byte r = CHAR_LOOKUP.TryGetValue(value[1], out v) ? (byte)(v * 0x10) : (byte)0;
                    byte g = CHAR_LOOKUP.TryGetValue(value[2], out v) ? (byte)(v * 0x10) : (byte)0;
                    byte b = CHAR_LOOKUP.TryGetValue(value[3], out v) ? (byte)(v * 0x10) : (byte)0;
                    color = ParameterColorValue.FromArgb(a, r, g, b);
                    return true;
                }
            default:
                color = default;
                return false;
                //throw new SerializationException("Unrecognized digit count, was expecting 3, 4, 6 or 8.");
        }
    }

    private static unsafe string[] Chop(string value, int chopLength)
    {
        int len = value.Length;
        char* segment = stackalloc char[chopLength];
        string[] result = new string[len];
        for (int i = 0; i < len; i += chopLength)
        {
            int j = 0;
            for (; j < chopLength; j++)
            {
                int next = i + j;
                if (next >= len) break;
                segment[j] = value[next];
            }
            result[i / chopLength] = new string(segment, 0, j);
        }
        return result;
    }
}
