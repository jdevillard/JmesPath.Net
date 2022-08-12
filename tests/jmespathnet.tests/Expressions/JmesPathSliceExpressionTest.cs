using DevLab.JmesPath.Expressions;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathSliceExpressionTest : JmesPathExpressionsTestBase
    {
        [Theory]
        [InlineData("[0:4:1]")]
        [InlineData("[0:4]")]
        [InlineData("[0:3]")]
        [InlineData("[:2]")]
        [InlineData("[::2]")]
        [InlineData("[::-1]")]
        [InlineData("[-2:]")]
        [InlineData("[-8:3:2]")]
        [InlineData("[-8:-3:-2]")]
        [InlineData("[8:2:7]")]
        [InlineData("[::]", "[:]")]
        public void JmesPathSliceExpression_ToString(string slices, string expected = null)
        {
            var parameters = GetSliceParameters(slices);
            var slice = new JmesPathSliceProjection(parameters[0], parameters[1], parameters[2]);

            var actual = slice.ToString();
            Xunit.Assert.Equal(expected ?? slices, actual);
        }

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
         * search([::], [0, 1, 2, 3]) -> [0, 1, 2, 3]
         * 
         */

        [Theory]
        [InlineData("[0:4:1]", "[0, 1, 2, 3]", "[0,1,2,3]")]
        [InlineData("[0:4]", "[0, 1, 2, 3]", "[0,1,2,3]")]
        [InlineData("[0:3]", "[0, 1, 2, 3]", "[0,1,2]")]
        [InlineData("[:2]", "[0, 1, 2, 3]", "[0,1]")]
        [InlineData("[::2]", "[0, 1, 2, 3]", "[0,2]")]
        [InlineData("[::-1]", "[0, 1, 2, 3]", "[3,2,1,0]")]
        [InlineData("[-2:]", "[0, 1, 2, 3]", "[2,3]")]

        [InlineData("[-8:3:2]", "[0, 1, 2, 3]", "[0,2]")]
        [InlineData("[-8:-3:-2]", "[0, 1, 2, 3]", "[]")]
        [InlineData("[8:2:7]", "[0, 1, 2, 3]", "[]")]
        [InlineData("[::]", "[0, 1, 2, 3]", "[0,1,2,3]")]
        public void JmesPathSliceExpression_Transform(string slices, string input, string expected)
            => Assert(slices, input, expected);

        [Fact]
        public void JmesPathSliceExpression_Compliance()
        {
            var expression = new JmesPathIndexExpression(
                new JmesPathIdentifier("foo"),
                new JmesPathSliceProjection(null, 10, null)
                );

            Assert(expression, "{\"foo\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],\"bar\": {\"baz\": 1}}", "[0,1,2,3,4,5,6,7,8,9]");
        }

        private void Assert(string slices, string input, string expected)
            => Assert(GetSliceParameters(slices), input, expected);

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

        private static int?[] GetSliceParameters(string slices)
        {
            var parameters = new int?[3];
            var numbers = slices.Substring(1, slices.Length - 2).Split(':');

            for (var index = 0; index < numbers.Length; index++)
                parameters[index] = string.IsNullOrWhiteSpace(numbers[index])
                    ? (int?)null
                    : int.Parse(numbers[index])
                    ;
            return parameters;
        }
    }
}