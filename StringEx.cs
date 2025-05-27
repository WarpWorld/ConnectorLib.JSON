using System.Runtime.CompilerServices;

namespace ConnectorLib.JSON;

/// <summary>Extensions for the <see cref="string"/> class.</summary>
internal static class StringEx
{
    /// <summary>Checks if a string is null or consists only of whitespace characters.</summary>
    /// <param name="input">The string to check.</param>
    /// <returns><c>true</c> if the string is null or consists only of whitespace characters; otherwise, <c>false</c>.</returns>
#if NET35
    public static bool IsNullOrWhiteSpace(this string? input)
    {
        if (input is null) return true;
        for (int i = 0; i < input.Length; i++)
            if (!char.IsWhiteSpace(input[i]))
                return false;
        return true;
    }
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrWhiteSpace(this string? input) => string.IsNullOrWhiteSpace(input);
#endif
    
    /// <summary>Converts a string to camel case.</summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The camel case version of the string.</returns>
    internal static string ToCamelCase(this string input)
    {
        bool encounteredLowercase = false;
        string result = "";

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                if (encounteredLowercase)
                {
                    // Stop converting uppercase letters if a lowercase letter is found
                    result += input.Substring(i);
                    break;
                }
                else
                {
                    // Convert leading uppercase letters to lowercase
                    result += char.ToLower(input[i]);
                }
            }
            else
            {
                encounteredLowercase = true;
                if (i > 1 && char.IsUpper(input[i - 1]) && char.IsUpper(input[i - 2]))
                {
                    // Revert the previous character to its original capitalization
                    result = result.Substring(0, result.Length - 1) + char.ToUpper(input[i - 1]) + input[i];
                }
                else
                {
                    result += input[i];
                }
            }
        }
        return result;
    }
    
    /// <summary>Chops a string into segments of a specified length.</summary>
    /// <param name="value">The string to chop.</param>
    /// <param name="chopLength">The length of each segment.</param>
    /// <returns>An array of strings, each containing a segment of the original string.</returns>
    internal static unsafe string[] Chop(this string value, int chopLength)
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