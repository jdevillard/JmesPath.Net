using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathSubExpressionTest
    {
        [Fact]
        public void JmesPathSubExpression_identifier()
        {
            const string json = "{\"foo\": {\"bar\": \"baz\"}}";
            var token = JToken.Parse(json);

            var expr = new JmesPathIdentifier("foo");
            var sub = new JmesPathIdentifier("bar");

            var combined = new JmesPathSubExpression(expr, sub);

            var result = combined.Transform(token);

            Assert.Equal("\"baz\"", result.AsString());
        }
    }
}