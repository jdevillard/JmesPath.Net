using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathLiteralTest : JmesPathExpressionsTestBase
    {
        [Fact]
        public void JmesPathLiteralJson_String()
        {
            var expression = new JmesPathLiteral(JToken.Parse("\"foo\""));

            const string input = "{\"foo\": \"value\"}";
            const string expected = "\"foo\"";

            Assert(expression, input, expected);
        }
    }
}