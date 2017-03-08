using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DevLab.JmesPath.Utils
{
    /// <summary>
    /// Provides some helper methods to deal with strings
    /// and escape sequences.
    /// </summary>
    public sealed class StringUtil
    {
        /// <summary>
        /// Accepts a valid representation of a JSON string
        /// with surrounding quotes and escape sequences and
        /// returns the resulting plain string.
        /// 
        /// E.g "Hello, world!" -> Hello, world!
        /// E.g "\u2713"        -> ✓
        /// 
        /// </summary>
        /// <param name="rawText"></param>
        /// <returns></returns>
        public static string Unwrap(string rawText)
        {
            // first, remove the surrounding double-quotes

            var text = rawText.Substring(1, rawText.Length - 2);

            // then, process the unicode escape sequences

            text = UnwrapUnicode(text);

            // finally, process the common escape sequences

            return text
                .Replace("\\\"", "\"")
                .Replace("\\\\", "\\")
                .Replace("\\/", "/")
                .Replace("\\b", "\b")
                .Replace("\\f", "\f")
                .Replace("\\n", "\n")
                .Replace("\\r", "\r")
                .Replace("\\v", "\v")
                .Replace("\\t", "\t")
                ;
        }
        public static string UnescapeRaw(string rawText)
        {
            // first, remove the surrounding double-quotes

            var text = rawText.Substring(1, rawText.Length - 2);

            // finally, process the common escape sequences

            return text
                .Replace("\\'", "'")
                
                ;
        }
        public static string UnescapeLiteral(string rawText)
        {
            // first, remove the surrounding double-quotes

            var text = rawText.Substring(1, rawText.Length - 2);

            // finally, process the common escape sequences

            return text
                .Replace("\\`", "`")

                ;
        }

        /// <summary>
        /// Converts the specified string to its valid JSON representation
        /// with surrounding quotes and proper escape sequences.
        /// 
        /// E.g. Hello, world! -> "Hello, world!"
        /// E.g. ✓            -> "\u2713"
        /// E.g.               -> "\ud834\udd1e"
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Wrap(string text)
        {
            // first, process common escaped characters

            var escaped = text
                .Replace("\"", "\\\"")
                .Replace("\\", "\\\\")
                .Replace("/", "\\/")
                .Replace("\b", "\\b")
                .Replace("\f", "\\f")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\v", "\\v")
                .Replace("\t", "\\t")
                ;

            // then, process extended unicode characters

            escaped = WrapUnicode(escaped);

            // finally, surround the resulting string with double-quotes

            return $"\"{escaped}\"";
        }

        // characters in a .NET string are represented by a 21-bit code value
        // of a character in the Basic Multilingual Plane (0x0000 - 0x10FFFF)
        // it can consists of either:
        // a single 16-bit code value (U+0000 to U+FFFF excluding the surrogate range U+D800 to U+DFFF) or
        // a pair of 16-bit surrogate code values (high U+D800 to U+DBFF and low U+DC00 to U+DFFF)

        private static readonly Regex RegexUnicodeEscapes
            = new Regex(
                @"
                    (?:
                    \\u(?<high>[Dd][8-9A-Ba-b][0-9A-Fa-f]{2})   # \ud800 to \udbff
                    \\u(?<low> [Dd][C-Fc-f][0-9A-Fa-f]{2})      # \udc00 to \udfff
                    )
                    |
                    (?:
                    \\u(?<code1>[0-9A-Ca-c][0-9A-Fa-f]{3})      # \u0000 to \ucfff
                    )
                    |
                    (?:
                    \\u(?<code2>[Dd][0-7][0-9A-Fa-f]{2})        # \ud000 to \ud7ff
                    )
                    |
                    (?:
                    \\u(?<code3>[EFef][0-9A-Fa-f]{3})           # \ue000 to \uffff
                    )
                    ",
                RegexOptions.Compiled |
                RegexOptions.Singleline |
                RegexOptions.IgnorePatternWhitespace
                );

        private static string UnwrapUnicode(string text)
        {
            return RegexUnicodeEscapes.Replace(text, UnwrapUnicode);
        }

        private static string UnwrapUnicode(Match match)
        {
            System.Diagnostics.Debug.Assert(match.Success);

            if (
                match.Groups["code1"].Value != "" ||
                match.Groups["code2"].Value != "" ||
                match.Groups["code3"].Value != "")
            {
                var escape = match.Groups["code1"].Value;
                if (escape == "")
                    escape = match.Groups["code2"].Value;
                if (escape == "")
                    escape = match.Groups["code3"].Value;

                var codePoint = ParseHex(escape);

                return FromUnicodeCodePoint(codePoint);
            }
            else if (
                match.Groups["high"].Value != "" &&
                match.Groups["low"].Value != "")
            {
                var highEscape = match.Groups["high"].Value;
                var lowEscape = match.Groups["low"].Value;

                var high = ParseHex(highEscape);
                var low = ParseHex(lowEscape);

                return FromUnicodeSurrogatePair(high, low);
            }

            System.Diagnostics.Debug.Assert(false);
            throw new NotSupportedException();
        }

        private static int ParseHex(string escape)
        {
            var codePoint = 0;

            const NumberStyles hex = NumberStyles.HexNumber;
            var culture = CultureInfo.InvariantCulture;
            var succeeded = Int32.TryParse(escape, hex, culture, out codePoint);
            System.Diagnostics.Debug.Assert(succeeded);

            return codePoint;
        }

        private static string FromUnicodeCodePoint(int codePoint)
        {
            return Char.ConvertFromUtf32(codePoint);
        }

        private static string FromUnicodeSurrogatePair(int high, int low)
        {
            // I don't know of a better way to create a unicode char
            // from a pair of surrogage code values.

            var h = BitConverter.GetBytes(high);
            var l = BitConverter.GetBytes(low);

            var buffer = new[] { h[0], h[1], l[0], l[1], };

            return Encoding.Unicode.GetString(buffer);
        }

        private static string WrapUnicode(string text)
        {
            var builder = new StringBuilder();

            var codePoints = GetUnicodeCodePoints(text);
            foreach (var codePoint in codePoints)
            {
                // attempt to preserve common characters
                // from being unnecessarily escaped

                if (codePoint < 32 || codePoint > 126)
                    builder.AppendFormat("\\u{0:x4}", codePoint);
                else
                    builder.Append(Char.ConvertFromUtf32(codePoint));
            }

            return builder.ToString();
        }

        private static IEnumerable<int> GetUnicodeCodePoints(string text)
        {
            var codePoints = new List<int>(text.Length);
            for (var index = 0; index < text.Length; index++)
            {
                var codePoint = Char.ConvertToUtf32(text, index);
                if (!Char.IsHighSurrogate(text[index]))
                    codePoints.Add(codePoint);
                else
                {
                    // handle surrogate pairs

                    var raw = new string(new[] { text[index] , text[index + 1] });
                    var buffer = Encoding.Unicode.GetBytes(raw);
                    var high = BitConverter.ToInt32(new byte[] { buffer[0], buffer[1], 0, 0, }, 0);
                    var low = BitConverter.ToInt32(new byte[] { buffer[2], buffer[3], 0, 0, }, 0);

                    codePoints.Add(high);
                    codePoints.Add(low);

                    index += 1;
                }
            }

            return codePoints;
        }
    }
}