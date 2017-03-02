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
            JmesPathSliceExpression_Transform(new int?[3] { (int?)0, (int?)4, (int?)1 }, "[0, 1, 2, 3]", "[0,1,2,3]");
            JmesPathSliceExpression_Transform(new int?[3] {(int?)0, (int?)4, null}, "[0, 1, 2, 3]", "[0,1,2,3]");
            JmesPathSliceExpression_Transform(new int?[3] { (int?)0, (int?)3, (int?)null }, "[0, 1, 2, 3]", "[0,1,2]");
            JmesPathSliceExpression_Transform(new int?[3] { (int?)null, (int?)2, (int?)null }, "[0, 1, 2, 3]", "[0,1]");
            JmesPathSliceExpression_Transform(new int?[3] { (int?)null, (int?)null, (int?)2 }, "[0, 1, 2, 3]", "[0,2]");
            JmesPathSliceExpression_Transform(new int?[3] { (int?)null, (int?)null, (int?)-1 }, "[0, 1, 2, 3]", "[3,2,1,0]");
            JmesPathSliceExpression_Transform(new int?[3] { (int?)-2, (int?)null, (int?)null }, "[0, 1, 2, 3]", "[2,3]");

            JmesPathSliceExpression_Transform(new int?[3] { (int?)-8, (int?)3, (int?)2 }, "[0, 1, 2, 3]", "[0,2]");
            JmesPathSliceExpression_Transform(new int?[3] { (int?)-8, (int?)-3, (int?)-2 }, "[0, 1, 2, 3]", "[]");
            JmesPathSliceExpression_Transform(new int?[3] { (int?)8, (int?)2, (int?)7 }, "[0, 1, 2, 3]", "[]");
            JmesPathSliceExpression_Transform(new int?[3] { (int?)null, (int?)null, (int?)null }, "[0, 1, 2, 3]", "[0,1,2,3]");
        }

        public void JmesPathSliceExpression_Transform(int?[] numbers, string input, string expected)
        {
            System.Diagnostics.Debug.Assert(numbers.Length == 3);

            JmesPathExpression expression = new JmesPathSliceExpression(
                numbers[0] == null ? null : new JmesPathNumber((int) numbers[0]),
                numbers[1] == null ? null : new JmesPathNumber((int) numbers[1]),
                numbers[2] == null ? null : new JmesPathNumber((int) numbers[2])
                );

            var json = JToken.Parse(input);
            var result = expression.Transform(json);
            var actual = result.Token.AsString();

            Assert.Equal(expected, actual);
        }
    }
}