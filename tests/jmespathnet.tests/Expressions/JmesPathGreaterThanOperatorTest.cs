using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathGreaterThanOperatorTest : JmesPathExpressionsTestBase
    {
        /*
         * http://jmespath.org/specification.html#comparison-operators
         * 
         */

        [Fact]
        public void JmesPathGreaterThanOperator_Evaluate()
        {
            const string json = "[{\"a\": 1}, {\"a\": 2}, {\"a\": 3}]";
            const string expected = "[{\"a\":2},{\"a\":3}]";

            var expression = new JmesPathFilterProjection(
                new JmesPathGreaterThanOperator(
                    new JmesPathIdentifier("a"),
                    new JmesPathLiteral(1)
                    )
                );

            Assert(expression, json, expected);
        }
    }
}