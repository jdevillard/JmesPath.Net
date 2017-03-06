using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathSliceExpressionTest
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
            JmesPathSliceExpression_Transform(new int?[] { 0, 4, 1 }, "[0, 1, 2, 3]", "[0,1,2,3]");
            JmesPathSliceExpression_Transform(new int?[] {0, 4, null}, "[0, 1, 2, 3]", "[0,1,2,3]");
            JmesPathSliceExpression_Transform(new int?[] { 0, 3, null }, "[0, 1, 2, 3]", "[0,1,2]");
            JmesPathSliceExpression_Transform(new int?[] { null, 2, null }, "[0, 1, 2, 3]", "[0,1]");
            JmesPathSliceExpression_Transform(new int?[] { null, null, 2 }, "[0, 1, 2, 3]", "[0,2]");
            JmesPathSliceExpression_Transform(new int?[] { null, null, -1 }, "[0, 1, 2, 3]", "[3,2,1,0]");
            JmesPathSliceExpression_Transform(new int?[] { -2, null, null }, "[0, 1, 2, 3]", "[2,3]");

            JmesPathSliceExpression_Transform(new int?[] { -8, 3, 2 }, "[0, 1, 2, 3]", "[0,2]");
            JmesPathSliceExpression_Transform(new int?[] { -8, -3, -2 }, "[0, 1, 2, 3]", "[]");
            JmesPathSliceExpression_Transform(new int?[] { 8, 2, 7 }, "[0, 1, 2, 3]", "[]");
            JmesPathSliceExpression_Transform(new int?[] { null, null, null }, "[0, 1, 2, 3]", "[0,1,2,3]");
        }

        [Fact]
        public void JmesPathSliceExpression_Compliance()
        {
            var expression = new JmesPathIndexExpression(
                new JmesPathIdentifier("foo"),
                new JmesPathSliceProjection(null, 10, null)
                );

            JmesPathSliceExpression_Transform(expression, "{\"foo\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],\"bar\": {\"baz\": 1}}", "[0,1,2,3,4,5,6,7,8,9]");
        }

        public void JmesPathSliceExpression_Transform(int?[] numbers, string input, string expected)
        {
            System.Diagnostics.Debug.Assert(numbers.Length == 3);

            JmesPathExpression expression = new JmesPathSliceProjection(
                numbers[0],
                numbers[1],
                numbers[2]
                );

            JmesPathSliceExpression_Transform(expression, input, expected);
        }

        private void JmesPathSliceExpression_Transform(JmesPathExpression expression, string input, string expected)
        {
            var json = JToken.Parse(input);
            var result = expression.Transform(json);
            var actual = result.AsJToken().AsString();

            Assert.Equal(expected, actual);
        }
    }
    }