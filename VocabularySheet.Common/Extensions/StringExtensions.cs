using System.Text;
using System.Text.RegularExpressions;

namespace VocabularySheet.Common.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Converts CamelCase to Camel Case
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string AddSpacesBeforeCapitalLetters(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]))
                newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }
    
    public static int GetWordsNumber(this string input)
    {
        // Use more efficient algorithm than .Split
        var wordCount = 0;
        var index = 0;
        while (index < input.Length)
        {
            // check if current char is part of a word
            while (index < input.Length && !char.IsWhiteSpace(input[index]))
                index++;

            wordCount++;

            // skip whitespace until next word
            while (index < input.Length && char.IsWhiteSpace(input[index]))
                index++;
        }
        
        return wordCount;
    }

    public static string CropToWords(this string input, int maxLength)
    {
        if (input.Length <= maxLength)
        {
            return input;
        }

        int lastValidEndOfWordIndex = -1;
        for (int i = 0; i < maxLength; i++)
        {
            if (input[i] == ' ')
            {
                lastValidEndOfWordIndex = i;
            }
        }

        // If no space is found, simply return the first 'maxLength' characters.
        string result = lastValidEndOfWordIndex != -1 ? input.Substring(0, lastValidEndOfWordIndex) : input.Substring(0, maxLength);
        
        // Cut off any trailing spaces of special characters (like "text -")
        return result.TrimEnd(' ', '-', '.', ',', ';', ':', '!', '?');
    }
    
    public static string CropToFullSentence(this string input, int maxLength)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));
        }

        if (input.Length <= maxLength)
        {
            return input;
        }

        int lastValidEndOfSentenceIndex = -1;
        for (int i = 0; i < maxLength; i++)
        {
            if (input[i] == '.' || input[i] == '?' || input[i] == '!')
            {
                lastValidEndOfSentenceIndex = i;
            }
        }

        // If no end of sentence character is found, simply return the first 'maxLength' characters.
        return lastValidEndOfSentenceIndex != -1 ? input.Substring(0, lastValidEndOfSentenceIndex + 1) : input.Substring(0, maxLength);
    }    
    
    public static string CropToFullSentence(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));
        }
        
        int lastValidEndOfSentenceIndex = input.LastIndexOfAny(new[] { '.', '?', '!' });

        if (lastValidEndOfSentenceIndex == -1)
        {
            return input;
        }

        return input.Substring(0, lastValidEndOfSentenceIndex + 1);
    }
    
    public static string? ToBase64Nullable(this string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        
        var bytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }
    
    public static string ToBase64(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var bytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }

    public static string RemovePunctuations(this string value, string newValue)
    {
        return RegexUtilities.Punctuations.Replace(value, newValue);
    }

    public static string RemoveNumbers(this string value, string newValue)
    {
        return RegexUtilities.Numbers.Replace(value, newValue);
    }

    public static string RemoveSpaces(this string value)
    {
        return value.Replace(" ", string.Empty);
    }

    public static string ReplaceDoubleSpaces(this string value)
    {
        return RegexUtilities.DoubleSpaces.Replace(value, " ");
    }

    public static string ReplaceDouble(this string value, char symbol)
    {
        if (symbol == ' ')
        {
            return value.ReplaceDoubleSpaces();
        }
        
        var regex = new Regex($"{symbol}+");
        
        return regex.Replace(value, $"{symbol}");
    }

    public static string ReplaceEscapes(this string value, string newValue)
    {
        return RegexUtilities.Escapes.Replace(value, newValue);
    }    
    
    public static string RemoveZeroSymbol(this string value, string newValue)
    {
        return value.Replace("\0", newValue);
    }
    
    public static string NormalizeQuery(this string value)
    {
        return value.Trim().ToLowerInvariant();
    }
    
    public static bool ContainsAny(this string value, List<string> items)
    {
        return items.Any(value.Contains);
    }
    
    public static string KeepOnlyLettersAndSpaces(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return Regex.Replace(input, "[^a-zA-Z ]", " ");
    }

    public static bool ContainsAny(this string value, params string[] items)
    {
        return items.Any(value.Contains);
    }

    /// <summary>
    /// Masks the string with asterisks keeping the first and last visibleCharacters
    /// </summary>
    /// <param name="value"></param>
    /// <param name="visibleCharacters"></param>
    /// <returns></returns>
    public static string MaskString(this string value, int visibleCharacters)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        
        if (visibleCharacters > value.Length)
        {
            visibleCharacters = value.Length;
        }

        return value.Substring(0, visibleCharacters) + new string('*', value.Length - visibleCharacters);
    }
    
    public static bool IsGuid(this string id)
    {
        return Guid.TryParse(id, out _);
    }
}