using DevLab.JmesPath.Utils;
using Xunit;

namespace jmespath.net.tests.Utils
{
    public sealed class StringUtilTest
    {
        [Fact]
        public void StringUtil_Unwrap_Unicode()
        {
            Assert.Equal("\u2713", StringUtil.Unwrap("\"\\u2713\""));
            Assert.Equal("\ud834\udd1e", StringUtil.Unwrap("\"\\ud834\\udd1e\""));
            Assert.Equal("\udadd\udfc7\\ueFAc", StringUtil.Unwrap("\"\\udadd\\udfc7\\\\ueFAc\""));
        }

        [Fact]
        public void StringUtil_Wrap_Unicode()
        {
            // http://www.fileformat.info/info/unicode/char/2713/index.htm
            Assert.Equal("\"\\u2713\"", StringUtil.Wrap("\u2713"));
            // http://www.fileformat.info/info/unicode/char/1d11e/index.htm
            Assert.Equal("\"\\ud834\\udd1e\"", StringUtil.Wrap("\U0001d11e"));
        }

        [Fact]
        public void StringUtil_Unwrap_Raw()
        {
            Assert.Equal("'a", StringUtil.UnescapeRaw("'\\'a'"));
            Assert.Equal("\\a", StringUtil.UnescapeRaw("'\\a'"));
        }
    }
}