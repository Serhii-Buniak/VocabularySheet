using AngleSharp.Dom;
using Tools.Common.Extensions;

namespace WebSources.Common;

public static class Extensions
{
    public static string InnerHtmlStriped(this IElement element)
    {
        return element.InnerHtml.StripHtml().Trim();
    }
}