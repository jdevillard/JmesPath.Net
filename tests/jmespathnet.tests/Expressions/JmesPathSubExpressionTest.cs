using DevLab.JmesPath.Expressions;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathSubExpressionTest : JmesPathExpressionsTestBase
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

        [Theory]
        [InlineData(new[] { "foo", "bar" }, "{\"foo\": {\"bar\": \"value\" }}", "\"value\"")]
        [InlineData(new[] { "foo", "bar" }, "{\"foo\": {\"baz\": \"value\" }}", "null")]
        [InlineData(new[] { "foo", "bar", "baz" }, "{\"foo\": {\"bar\": { \"baz\": \"value\" }}}", "\"value\"")]
        [InlineData(new[] { "foo", "bar", "baz", "bad" }, "{\"foo\": {\"bar\": {\"baz\": \"correct\"}}}", "null")]
        public void JmesPathSubExpression_Transform(string[] expressions, string input, string expected)
            => Assert(expressions, input, expected);

        [Theory]
        [InlineData(new[] { "foo", "bar" }, "{\"foo\": -1}", "null")]
        public void JmesPathSubExpression_Compliance(string[] expressions, string input, string expected)
            => Assert(expressions, input, expected);

        private void Assert(string[] expressions, string input, string expected)
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

            Assert(expression, input, expected);
        }
    }
}