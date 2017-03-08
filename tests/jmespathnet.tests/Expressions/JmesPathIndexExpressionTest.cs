using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathIndexExpressionTest : JmesPathExpressionsTestBase
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
            Assert("foo", 0, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"first\"");
            Assert("foo", 1, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"second\"");
            Assert("foo", 2, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"third\"");
            Assert("foo", 3, "{\"foo\": [\"first\", \"second\", \"third\"]}", "null");
            Assert("foo", -1, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"third\"");
            Assert("foo", -2, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"second\"");
            Assert("foo", -3, "{\"foo\": [\"first\", \"second\", \"third\"]}", "\"first\"");
            Assert("foo", -4, "{\"foo\": [\"first\", \"second\", \"third\"]}", "null");
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

            Assert(expression, "{\"foo\": { \"bar\": [[\"one\", \"two\"], [\"three\", \"four\"]] }}", "null");
        }

        private void Assert(string identifier, int specifier, string input, string expected)
        {
            JmesPathExpression index = new JmesPathIndexExpression(
                new JmesPathIdentifier(identifier),
                new JmesPathIndex(specifier)
                );

            Assert(index, input, expected);
        }
    }
}