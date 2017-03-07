using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathPipeExpressionTest
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

            JmesPathPipeExpression_Transform(expression, "{\"foo\": {\"bar\": \"baz\"}}", "\"baz\"");

            expression = new JmesPathPipeExpression(
                new JmesPathSubExpression(
                    new JmesPathIndexExpression(
                        new JmesPathIdentifier("foo"),
                        new JmesPathListWildcardProjection()),
                    new JmesPathIdentifier("bar")
                    ),
               new JmesPathIndex(0)
               );

            JmesPathPipeExpression_Transform(expression, "{\"foo\": [{\"bar\": [\"first1\", \"second1\"]},{\"bar\": [\"first2\", \"second2\"]}]}", "[\"first1\",\"second1\"]");
            JmesPathPipeExpression_Transform(expression, "{\"foo\": [{\"bar\": [\"first1\", \"second1\"]},{\"bar\": [\"first2\", \"second2\"]}]}", "[\"first1\",\"second1\"]");

            expression = new JmesPathPipeExpression(
                new JmesPathIdentifier("foo"),
                new JmesPathIndex(0)
                );

            JmesPathPipeExpression_Transform(expression, "{\"foo\": [0, 1, 2]}", "0");
        }

        private void JmesPathPipeExpression_Transform(JmesPathExpression expression, string input, string expected)
        {
            var json = JToken.Parse(input);
            var result = expression.Transform(json);
            var actual = result.AsJToken().AsString();

            Assert.Equal(expected, actual);
        }
    }
}