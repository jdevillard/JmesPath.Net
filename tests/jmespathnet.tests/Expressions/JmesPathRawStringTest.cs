using DevLab.JmesPath.Expressions;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathRawStringTest : JmesPathExpressionsTestBase
    {
        [Theory]
        [InlineData("foo", "{\"foo\": \"value\"}", "\"foo\"")]
        public void JmesPathRawString_NoSpecialChar(string expression, string input, string expected)
            => Assert(new JmesPathRawString(expression), input, expected);
    }
}
