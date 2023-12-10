using System.Text.RegularExpressions;
using AngleSharp.Dom;

namespace VocabularySheet.Parsing.Common;

public static class Extensions
{
    public static string InnerHtmlStriped(this IElement element)
    {
        string html = element.InnerHtml;
        
        // This regular expression removes HTML tags from the input string
        return Regex.Replace(html, "<.*?>", string.Empty).Trim();
    }
    
    
}