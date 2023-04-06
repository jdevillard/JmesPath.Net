using System;
using System.Linq;
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
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int? FindLast(this string text, string search, int? start, int? end)
        {
            int s = Math.Max(start ?? 0, 0);
            int e = Math.Min(end ?? (text.Length == 0 ? 0 : text.Length), text.Length);

            var substring = text.Substring(s, e - s);
            var pos = substring.LastIndexOf(search, StringComparison.OrdinalIgnoreCase);

            return (pos != -1) ? s + pos : (int?)null;
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

        /// <summary>
        /// Supports the JMESPath `split` function.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string[] Split(this string text, string separator, int? count)
        { 
            if (separator.Length == 0)
            {
                var position = count ?? text.Length;
                var tail = text.Substring(position);
                var head = text.Substring(0, position);

                var heads = head.ToCharArray().Select(c => new string(new[] { c, }));
                var remainders = tail.Length > 0 ? new[] { tail, } : Enumerable.Empty<string>();

                return heads.Concat(remainders).ToArray();
            }

            else
            {
                var split = count != null
                    ? text.Split(new[] { separator }, count.GetValueOrDefault() + 1, StringSplitOptions.None)
                    : text.Split(new[] { separator }, StringSplitOptions.None)
                    ;

                return split;
            }
        }
    }
}
