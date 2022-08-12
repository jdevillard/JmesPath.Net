using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Expressions;
using Xunit;
using DevLab.JmesPath.Tokens;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathLiteralTest : JmesPathExpressionsTestBase
    {
        [Theory]
        [InlineData("`{}`")]
        [InlineData("`[]`")]
        [InlineData("`[{}]`")]
        [InlineData("`[{\"foo\":\"bar\"}]`")]
        [InlineData("`\"foo\"`")]
        [InlineData("`true`")]
        [InlineData("`false`")]
        [InlineData("`null`")]
        [InlineData("`1.0e+2`", "`100.0`")]
        [InlineData("`\"f\\`oo\"`")]
        [InlineData("`\"foo \\\" \\\\ \\/ \\b \\n \\r \\t \\u2713 \\` bar\"`")]
        public void JmesPathLiteralJson_ToString(string expression, string expected = null)
        { 
            var token = new LiteralStringToken(expression);
            var json = JToken.Parse((string)token.Value);
            var literal = new JmesPathLiteral(json);

            var actual = literal.ToString();
            Xunit.Assert.Equal(expected ?? expression, actual);
        }

        [Theory]
        [InlineData("\"foo\"", "{\"foo\": \"value\"}", "\"foo\"")]
        public void JmesPathLiteralJson_String(string expression, string input, string expected)
            => Assert(new JmesPathLiteral(JToken.Parse(expression)), input, expected);
    }
}