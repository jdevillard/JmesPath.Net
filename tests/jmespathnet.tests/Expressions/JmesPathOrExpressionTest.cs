using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    /*
     * http://jmespath.org/specification.html#or-expressions
     * 
     * search(foo || bar, {"foo": "foo-value"}) -> "foo-value"
     * search(foo || bar, {"bar": "bar-value"}) -> "bar-value"
     * search(foo || bar, {"foo": "foo-value", "bar": "bar-value"}) -> "foo-value"
     * search(foo || bar, {"baz": "baz-value"}) -> null
     * search(foo || bar || baz, {"baz": "baz-value"}) -> "baz-value"
     * search(override || mylist[-1], {"mylist": ["one", "two"]}) -> "two"
     * search(override || mylist[-1], {"mylist": ["one", "two"], "override": "yes"}) -> "yes"
     * 
     */

    public class JmesPathOrExpressionTest : JmesPathExpressionsTestBase
    {
        [Fact]
        public void JmesPathOrExpression_Or()
        {
            var expression = new JmesPathOrExpression(
                new JmesPathIdentifier("foo"),
                new JmesPathIdentifier("bar")
                );

            Assert(expression, "{\"foo\": \"foo-value\"}", "\"foo-value\"");
            Assert(expression, "{\"bar\": \"bar-value\"}", "\"bar-value\"");
            Assert(expression, "{\"foo\": \"foo-value\", \"bar\": \"bar-value\"}", "\"foo-value\"");
            Assert(expression, "{\"baz\": \"baz-value\"}", "null");

            expression = new JmesPathOrExpression(
                new JmesPathIdentifier("foo"),
                new JmesPathOrExpression(
                    new JmesPathIdentifier("bar"),
                    new JmesPathIdentifier("baz")
                    ));

            Assert(expression, "{\"baz\": \"baz-value\"}", "\"baz-value\"");
        }

        [Fact]
        public void JmesPathOrExpression_Override()
        {
            var expression = new JmesPathOrExpression(
                new JmesPathIdentifier("override"),
                new JmesPathIndexExpression(
                    new JmesPathIdentifier("mylist"),
                    new JmesPathIndex(-1)
                    )
                );

            Assert(expression, "{\"mylist\": [\"one\", \"two\"]}", "\"two\"");
            Assert(expression, "{\"mylist\": [\"one\", \"two\"], \"override\": \"yes\"}", "\"yes\"");
        }
    }
}