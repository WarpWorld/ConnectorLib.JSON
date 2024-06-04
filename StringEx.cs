namespace ConnectorLib.JSON;

internal static class StringEx
{
    public static string ToCamelCase(this string input)
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
}