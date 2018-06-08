using System.Collections.Generic;

namespace TextAnalytics.Extensions
{
    public static class ListExtensions
    {
        public static string ToCommaSeparatedString(this IList<string> list)
        {
            return string.Join(",", list);
        }
    }
}
