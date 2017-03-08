using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathPipeExpressionTest : JmesPathExpressionsTestBase
    {
        /*
         * http://jmespath.org/specification.html#pipe-expressions
         * 
         * search(foo | bar, {"foo": {"bar": "baz"}}) -> "baz"
         * search(foo[*].bar | [0], {
         *     "foo": [{"bar": ["first1", "second1"]},
         *             {"bar": ["first2", "second2"]}]}) -> ["first1", "second1"]
         * search(foo[*].bar | [0], {
         *     "foo": [{"bar": ["first1", "second1"]},
         *             {"bar": ["first2", "second2"]}]}) -> ["first1", "second1"]
         * search(foo | [0], {"foo": [0, 1, 2]}) -> [0]
         * search(foo | [0], {"foo": [0, 1, 2]}) -> 0
         * 
         */

        [Fact]
        private void JmesPathPipeExpression()
        {
            var expression = new JmesPathPipeExpression(
                new JmesPathIdentifier("foo"),
                new JmesPathIdentifier("bar")
                );

            Assert(expression, "{\"foo\": {\"bar\": \"baz\"}}", "\"baz\"");

            expression = new JmesPathPipeExpression(
                new JmesPathSubExpression(
                    new JmesPathIndexExpression(
                        new JmesPathIdentifier("foo"),
                        new JmesPathListWildcardProjection()),
                    new JmesPathIdentifier("bar")
                    ),
               new JmesPathIndex(0)
               );

            Assert(expression, "{\"foo\": [{\"bar\": [\"first1\", \"second1\"]},{\"bar\": [\"first2\", \"second2\"]}]}", "[\"first1\",\"second1\"]");
            Assert(expression, "{\"foo\": [{\"bar\": [\"first1\", \"second1\"]},{\"bar\": [\"first2\", \"second2\"]}]}", "[\"first1\",\"second1\"]");

            expression = new JmesPathPipeExpression(
                new JmesPathIdentifier("foo"),
                new JmesPathIndex(0)
                );

            Assert(expression, "{\"foo\": [0, 1, 2]}", "0");
        }
    }
}