using System;
using System.Text.RegularExpressions;

namespace jmespath.net.Functions.Impl
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Supports JMESPath `find_first` function.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="search"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int? Find(this string text, string search, int? start, int? end)
        {
            int s = Math.Max(start ?? 0, 0);
            int e = Math.Min(end ?? (text.Length == 0 ? 0 : text.Length), text.Length);

            var substring = text.Substring(s, e - s);
            var pos = substring.IndexOf(search, 0, StringComparison.OrdinalIgnoreCase);

            return (pos != -1) ? s + pos : (int?)null;
        }

        /// <summary>
        /// Supports JMESPath `find_last` function.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="search"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int? FindLast(this string text, string search, int? position)
        {
            int p = Math.Min(position ?? (text.Length == 0 ? 0 : text.Length), text.Length);
            int e = Math.Min(p + search.Length, text.Length);

            var substring = text.Substring(0, e);
            var pos = substring.LastIndexOf(search, StringComparison.OrdinalIgnoreCase);

            return (pos != -1) ? pos : (int?)null;
        }

        /// <summary>
        /// Supports JMESPath `replace` function.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replace"></param>
        /// <param name="with"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Replace(this string text, string replace, string with, int? count)
        {
            var pattern = Regex.Escape(replace);

            var replaced = count != null
                ?  new Regex(pattern).Replace(text, with, count.GetValueOrDefault())
                :  new Regex(pattern).Replace(text, with);
            ;

            return replaced;
        }
    }
}
