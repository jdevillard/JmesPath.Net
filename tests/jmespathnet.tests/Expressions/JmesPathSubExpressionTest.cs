using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

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

        [Fact]
        public void JmesPathSubExpression_Transform()
        {
            Assert(new[] {"foo", "bar"}, "{\"foo\": {\"bar\": \"value\" }}", "\"value\"");
            Assert(new[] { "foo", "bar" }, "{\"foo\": {\"bar\": \"value\" }}", "\"value\"");
            Assert(new[] { "foo", "bar" }, "{\"foo\": {\"baz\": \"value\" }}", "null");
            Assert(new[] { "foo", "bar", "baz" }, "{\"foo\": {\"bar\": { \"baz\": \"value\" }}}", "\"value\"");
            Assert(new[] { "foo", "bar", "baz", "bad" }, "{\"foo\": {\"bar\": {\"baz\": \"correct\"}}}", "null");
        }

        [Fact]
        public void JmesPathSubExpression_Compliance()
        {
            Assert(new[] { "foo", "bar" }, "{\"foo\": -1}", "null");
        }


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