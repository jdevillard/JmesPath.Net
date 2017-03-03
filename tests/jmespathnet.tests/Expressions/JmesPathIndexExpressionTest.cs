using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathIndexExpressionTest
    {
        /*
         * http://jmespath.org/specification.html#index-expressions
         * 
         * index-expression  = expression bracket-specifier / bracket-specifier
         * bracket-specifier = "[" (number / "*") "]" / "[]"
         * 
         */

        [Fact]
        public void JmesPathIndexExpression_Transform()
        {
            JmesPathIndexExpression_Transform("foo", 0, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"first\"");
            JmesPathIndexExpression_Transform("foo", 1, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"second\"");
            JmesPathIndexExpression_Transform("foo", 2, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"third\"");
            JmesPathIndexExpression_Transform("foo", 3, "{\"foo\": [\"first\", \"second\", \"third\"]}", null);
            JmesPathIndexExpression_Transform("foo", -1, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"third\"");
            JmesPathIndexExpression_Transform("foo", -2, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"second\"");
            JmesPathIndexExpression_Transform("foo", -3, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"first\"");
            JmesPathIndexExpression_Transform("foo", -4, "{\"foo\": [\"first\", \"second\", \"third\"]}", null);
        }

        [Fact]
        public void JmesPathIndexExpression_Compliance()
        {
            var expression =
                new JmesPathIndexExpression(
                    new JmesPathIndexExpression(
                        new JmesPathIndexExpression(
                            new JmesPathIndexExpression(
                                new JmesPathSubExpression(
                                    new JmesPathIdentifier("foo"),
                                    new JmesPathIdentifier("bar")),
                                new JmesPathIndex(0)),
                            new JmesPathIndex(0)),
                        new JmesPathIndex(0)),
                    new JmesPathIndex(0))
                ;

            JmesPathIndexExpression_Transform(expression, "{\"foo\": { \"bar\": [[\"one\", \"two\"], [\"three\", \"four\"]] }}", null);
        }

        public void JmesPathIndexExpression_Transform(string identifier, int specifier, string input, string expected)
        {
            JmesPathExpression index = new JmesPathIndexExpression(
                new JmesPathIdentifier(identifier),
                new JmesPathIndex(specifier)
                );

            JmesPathIndexExpression_Transform(index, input, expected);
        }

        private static void JmesPathIndexExpression_Transform(JmesPathExpression index, string input, string expected)
        {
            var json = JToken.Parse(input);

            var result = index.Transform(json);
            var actual = result.Token?.AsString();

            Assert.Equal(expected, actual);
        }
    }
}