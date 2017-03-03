using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathLiteralTest
    {
        [Fact]
        public void JmesPathLiteralJson_String()
        {
            var rawString = new JmesPathLiteral(JToken.Parse("\"foo\""));
            const string input = "{\"foo\": \"value\"}";

            var json = JToken.Parse(input);

            Assert.Equal("\"foo\"", rawString.Transform(json).Token.AsString());
        }
    }
}