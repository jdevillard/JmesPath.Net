using DevLab.JmesPath.Utils;
using Xunit;

namespace jmespath.net.tests.Utils
{
    public sealed class StringUtilTest
    {
        [Theory]
        [InlineData("\u2713", "\"\\u2713\"")]
        [InlineData("\ud834\udd1e", "\"\\ud834\\udd1e\"")]
        [InlineData("\udadd\udfc7\\ueFAc", "\"\\udadd\\udfc7\\\\ueFAc\"")]
        public void StringUtil_Unwrap_Unicode(string unwrapped, string text)
            => Assert.Equal(unwrapped, StringUtil.Unwrap(text));

        [Theory]
        [InlineData("\"\\u2713\"", "\u2713")] // http://www.fileformat.info/info/unicode/char/2713/index.htm
        [InlineData("\"\\ud834\\udd1e\"", "\U0001d11e")] // http://www.fileformat.info/info/unicode/char/1d11e/index.htm
        [InlineData("\"\\\"\"", "\"")]
        public void StringUtil_Wrap_Unicode(string wrapped, string text)
            => Assert.Equal(wrapped, StringUtil.Wrap(text));

        [Theory]
        [InlineData("'a", "'\\'a'")]
        [InlineData("\\a", "'\\a'")]
        [InlineData("\\p\\r\\e\\s\\e\\r\\v\\e\\d", "'\\p\\r\\e\\s\\e\\r\\v\\e\\d'")]
        [InlineData("\\b\\f\\n\\r\\t", "'\\b\\f\\n\\r\\t'")]
        [InlineData("\\u2713", "'\\u2713'")]
        public void StringUtil_Unwrap_Raw(string unescaped, string text)
            => Assert.Equal(unescaped, StringUtil.UnwrapRawString(text));

        [Theory]
        [InlineData("'\\'a'", "'a")]
        [InlineData("'\\a'", "\\a")]
        [InlineData("'\\p\\r\\e\\s\\e\\r\\v\\e\\d'", "\\p\\r\\e\\s\\e\\r\\v\\e\\d")]
        [InlineData("'\\b\\f\\n\\r\\t'", "\\b\\f\\n\\r\\t")]
        [InlineData("'\\u2713'", "\\u2713")]
        public void StringUtil_Wrap_Raw(string wrapped, string text)
            => Assert.Equal(wrapped, StringUtil.WrapRawString(text));

        [Theory]
        [InlineData("Hello, world!", "Hello, world!")]
        [InlineData("✓", "\\u2713")]
        [InlineData("𝄞", "\\ud834\\udd1e")]
        [InlineData("\"", "\\\"")]
        [InlineData("\\", "\\\\")]
        [InlineData("\b", "\\b")]
        [InlineData("\f", "\\f")]
        [InlineData("\n", "\\n")]
        [InlineData("\r", "\\r")]
        [InlineData("\t", "\\t")]
        [InlineData("/", "\\/")]
        public void StringUtil_Escape(string text, string escaped)
            => Assert.Equal(escaped, StringUtil.Escape(text));

        [Theory]
        [InlineData("Hello, world!", "Hello, world!")]
        [InlineData("\\u2713", "✓")]
        [InlineData("\\ud834\\udd1e", "𝄞")]
        [InlineData("\\\"", "\"")]
        [InlineData("\\\\", "\\")]
        [InlineData("\\b", "\b")]
        [InlineData("\\f", "\f")]
        [InlineData("\\n", "\n")]
        [InlineData("\\r", "\r")]
        [InlineData("\\t", "\t")]
        [InlineData("\\/", "/")]
        public void StringUtil_Unescape(string escaped, string text)
            => Assert.Equal(text, StringUtil.Unescape(escaped));
    }
}