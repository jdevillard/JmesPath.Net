using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathRawStringTest : JmesPathExpressionsTestBase
    {
        [Fact]
        public void JmesPathRawString_NoSpecialChar()
        {
            var expression = new JmesPathRawString("foo");

            const string input = "{\"foo\": \"value\"}";
            const string expected = "\"foo\"";

            Assert(expression, input, expected);
        }
    }
}
