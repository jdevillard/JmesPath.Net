using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathSubExpressionTest
    {
        /*
         * http://jmespath.org/specification.html#subexpressions
         * 
         * search(foo.bar, {"foo": {"bar": "value"}}) -> "value"
         * search(foo."bar", {"foo": {"bar": "value"}}) -> "value"
         * search(foo.bar, {"foo": {"baz": "value"}}) -> null
         * search(foo.bar.baz, {"foo": {"bar": {"baz": "value"}}}) -> "value"
         * 
         */

        [Fact]
        public void JmesPathSubExpression_Transform()
        {
            JmesPathSubExpression_Transform(new[] {"foo", "bar"}, "{\"foo\": {\"bar\": \"value\" }}", "\"value\"");
            JmesPathSubExpression_Transform(new[] { "foo", "bar" }, "{\"foo\": {\"bar\": \"value\" }}", "\"value\"");
            JmesPathSubExpression_Transform(new[] { "foo", "bar" }, "{\"foo\": {\"baz\": \"value\" }}", null);
            JmesPathSubExpression_Transform(new[] { "foo", "bar", "baz" }, "{\"foo\": {\"bar\": { \"baz\": \"value\" }}}", "\"value\"");
        }

        public void JmesPathSubExpression_Transform(string[] expressions, string input, string expected)
        {
            JmesPathExpression expression = null;

            foreach (var identifier in expressions)
            {
                JmesPathExpression ident = new JmesPathIdentifier(identifier);
                expression = expression != null
                    ? new JmesPathSubExpression(expression, ident)
                    : ident
                    ;
            }

            var token = JToken.Parse(input);
            var result = expression.Transform(token);
            var actual = result.Token?.AsString();

            Assert.Equal(expected, actual);
        }
    }
}