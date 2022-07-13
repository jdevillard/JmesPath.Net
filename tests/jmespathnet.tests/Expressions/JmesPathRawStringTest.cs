using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Tokens;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathRawStringTest : JmesPathExpressionsTestBase
    {
        [Theory]
        [InlineData("'\\'a'")]
        [InlineData("'\\a'")]
        [InlineData("'\\p\\r\\e\\s\\e\\r\\v\\e\\d'")]
        [InlineData("'\\b\\f\\n\\r\\t'")]
        [InlineData("'\\u2713'")]
        public void JmesPathRawString_ToString(string expression, string expected = null)
        {
            var token = new RawStringToken(expression);
            var raw = new JmesPathRawString((string)token.Value);

            var actual = raw.ToString();
            Xunit.Assert.Equal(expected ?? expression, actual);
        }

        [Theory]
        [InlineData("foo", "{\"foo\": \"value\"}", "\"foo\"")]
        public void JmesPathRawString_NoSpecialChar(string expression, string input, string expected)
            => Assert(new JmesPathRawString(expression), input, expected);
    }
}
