using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathEqualOperatorTest : JmesPathExpressionsTestBase
    {
        /*
         * http://jmespath.org/specification.html#equality-operators
         *  
         * search([?bar==`10`], [{"bar": 1}, {"bar": 10}]) -> [{"bar": 10}]
         * search(foo[?bar==`10`], {"foo": [{"bar": 1}, {"bar": 10}]}) -> [{"bar": 10}]
         * search(foo[?a==b], {"foo": [{"a": 1, "b": 2}, {"a": 2, "b": 2}]}) -> [{"a": 2, "b": 2}]

         */

        [Fact]
        public void JmesPathEqualOperator_Evaluate()
        {
            JmesPathExpression expression = new JmesPathFilterProjection(
                new JmesPathEqualOperator(
                    new JmesPathIdentifier("bar"),
                    new JmesPathLiteral(10)
                    )
                );

            Assert(expression, "[{\"bar\": 1}, {\"bar\": 10}]", "[{\"bar\":10}]");

            expression = new JmesPathIndexExpression(
                new JmesPathIdentifier("foo"),
                expression
                );

            Assert(expression, "{\"foo\": [{\"bar\": 1}, {\"bar\": 10}]}", "[{\"bar\":10}]");

            expression = new JmesPathIndexExpression(
                new JmesPathIdentifier("foo"),
                new JmesPathFilterProjection(
                    new JmesPathEqualOperator(
                        new JmesPathIdentifier("a"),
                        new JmesPathIdentifier("b")
                        )
                    )
                );

            Assert(expression, "{\"foo\": [{\"a\": 1, \"b\": 2}, {\"a\": 2, \"b\": 2}]}", "[{\"a\":2,\"b\":2}]");
        }
    }
}