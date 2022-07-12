using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Expressions;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathLiteralTest : JmesPathExpressionsTestBase
    {
        [Theory]
        [InlineData("\"foo\"", "{\"foo\": \"value\"}", "\"foo\"")]
        public void JmesPathLiteralJson_String(string expression, string input, string expected)
            => Assert(new JmesPathLiteral(JToken.Parse(expression)), input, expected);
    }
}