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

        [Fact]
        public void JmesPathGreaterThanOperator_Evaluate_date_as_strings()
        {
            const string json = "[{\"a\": \"2018-01-01T00:00:00.000Z\"}, {\"a\": \"2018-02-01T00:00:00.000Z\"}, {\"a\": \"2018-03-01T00:00:00.000Z\"}]";
            const string expected = "[{\"a\":\"2018-02-01T00:00:00.000Z\"},{\"a\":\"2018-03-01T00:00:00.000Z\"}]";

            var expression = new JmesPathFilterProjection(
                new JmesPathGreaterThanOperator(
                    new JmesPathIdentifier("a"),
                    new JmesPathLiteral("2018-01-01T14:27:34.999Z")
                    )
                );

            Assert(expression, json, expected);
        }
    }
}