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
        public void StringUtil_Wrap_Unicode(string wrapped, string text)
            => Assert.Equal(wrapped, StringUtil.Wrap(text));

        [Theory]
        [InlineData("'a", "'\\'a'")]
        [InlineData("\\a", "'\\a'")]
        public void StringUtil_Unwrap_Raw(string unescaped, string text)
            => Assert.Equal(unescaped, StringUtil.UnescapeRaw(text));
    }
}