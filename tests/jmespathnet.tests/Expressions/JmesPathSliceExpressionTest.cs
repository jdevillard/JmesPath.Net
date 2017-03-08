using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathSliceExpressionTest : JmesPathExpressionsTestBase
    {
        /*
         * http://jmespath.org/specification.html#index-expressions
         * 
         * search([0:4:1], [0, 1, 2, 3]) -> [0, 1, 2, 3]
         * search([0:4], [0, 1, 2, 3]) -> [0, 1, 2, 3]
         * search([0:3], [0, 1, 2, 3]) -> [0, 1, 2]
         * search([:2], [0, 1, 2, 3]) -> [0, 1]
         * search([::2], [0, 1, 2, 3]) -> [0, 2]
         * search([::-1], [0, 1, 2, 3]) -> [3, 2, 1, 0]
         * search([-2:], [0, 1, 2, 3]) -> [2, 3]
         * 
         * search([-8:3:2], [0, 1, 2, 3]) -> [0,2]
         * search([-8:-3:-2], [0, 1, 2, 3]) -> [0,2]
         * search([8:2:7], [0, 1, 2, 3]) -> []
         * search([], [0, 1, 2, 3]) -> [0, 1, 2, 3]
         * 
         */

        [Fact]
        public void JmesPathSliceExpression_Transform()
        {
            Assert(new int?[] { 0, 4, 1 }, "[0, 1, 2, 3]", "[0,1,2,3]");
            Assert(new int?[] { 0, 4, null }, "[0, 1, 2, 3]", "[0,1,2,3]");
            Assert(new int?[] { 0, 3, null }, "[0, 1, 2, 3]", "[0,1,2]");
            Assert(new int?[] { null, 2, null }, "[0, 1, 2, 3]", "[0,1]");
            Assert(new int?[] { null, null, 2 }, "[0, 1, 2, 3]", "[0,2]");
            Assert(new int?[] { null, null, -1 }, "[0, 1, 2, 3]", "[3,2,1,0]");
            Assert(new int?[] { -2, null, null }, "[0, 1, 2, 3]", "[2,3]");

            Assert(new int?[] { -8, 3, 2 }, "[0, 1, 2, 3]", "[0,2]");
            Assert(new int?[] { -8, -3, -2 }, "[0, 1, 2, 3]", "[]");
            Assert(new int?[] { 8, 2, 7 }, "[0, 1, 2, 3]", "[]");
            Assert(new int?[] { null, null, null }, "[0, 1, 2, 3]", "[0,1,2,3]");
        }

        [Fact]
        public void JmesPathSliceExpression_Compliance()
        {
            var expression = new JmesPathIndexExpression(
                new JmesPathIdentifier("foo"),
                new JmesPathSliceProjection(null, 10, null)
                );

            Assert(expression, "{\"foo\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],\"bar\": {\"baz\": 1}}", "[0,1,2,3,4,5,6,7,8,9]");
        }

        private void Assert(int?[] numbers, string input, string expected)
        {
            System.Diagnostics.Debug.Assert(numbers.Length == 3);

            JmesPathExpression expression = new JmesPathSliceProjection(
                numbers[0],
                numbers[1],
                numbers[2]
                );

            Assert(expression, input, expected);
        }
    }
}