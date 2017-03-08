using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathMultiSelectListTest : JmesPathExpressionsTestBase
    {
        /*
         * http://jmespath.org/specification.html#multiselect-list
         * 
         * search([foo,bar], {"foo": "a", "bar": "b", "baz": "c"}) -> ["a", "b"]
         * search([foo,bar], {"foo": "a", "baz": "b"}) -> ["a", null]
         * search([foo,bar.baz], {"foo": "a", "bar": {"baz": "b"}}) -> ["a", "b"]
         * search([foo,bar[0]], {"foo": "a", "bar": ["b"], "baz": "c"}) -> ["a", "b"]
         * 
         */
        [Fact]
        public void JmesPathMultiSelectList()
        {
            JmesPathMultiSelectList expression = new JmesPathMultiSelectList(
                new JmesPathIdentifier("foo"),
                new JmesPathIdentifier("bar"));

            Assert(expression, "{\"foo\": \"a\", \"bar\": \"b\", \"baz\": \"c\"}", "[\"a\",\"b\"]");
            Assert(expression, "{\"foo\": \"a\", \"baz\": \"b\"}", "[\"a\",null]");

            expression = new JmesPathMultiSelectList(
                new JmesPathIdentifier("foo"),
                new JmesPathSubExpression(
                    new JmesPathIdentifier("bar"),
                    new JmesPathIdentifier("baz")
                    )
                );

            Assert(expression, "{\"foo\": \"a\", \"bar\": {\"baz\": \"b\"}}", "[\"a\",\"b\"]");

            expression = new JmesPathMultiSelectList(
                new JmesPathIdentifier("foo"),
                new JmesPathIndexExpression(
                    new JmesPathIdentifier("bar"),
                    new JmesPathIndex(0)
                    )
                );

            Assert(expression, "{\"foo\": \"a\", \"bar\": [\"b\"], \"baz\": \"c\"}", "[\"a\",\"b\"]");
        }
    }
}