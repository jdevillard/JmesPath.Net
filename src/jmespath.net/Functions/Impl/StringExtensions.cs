using System;

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

    }
}
