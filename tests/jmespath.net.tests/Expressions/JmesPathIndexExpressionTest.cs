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
         * search([0:4:1], [0, 1, 2, 3]) -> [0, 1, 2, 3]
         * search([0:4], [0, 1, 2, 3]) -> [0, 1, 2, 3]
         * search([0:3], [0, 1, 2, 3]) -> [0, 1, 2]
         * search([:2], [0, 1, 2, 3]) -> [0, 1]
         * search([::2], [0, 1, 2, 3]) -> [0, 2]
         * search([::-1], [0, 1, 2, 3]) -> [3, 2, 1, 0]
         * search([-2:], [0, 1, 2, 3]) -> [2, 3]
         * 
         */

        [Fact]
        public void JmesPathIndexExpression_index()
        {
            var number = new JmesPathNumber(0);
            var specifier = new JmesPathSliceExpression(number);

            const string input = "[\"first\",\"second\"]";
            var json = JToken.Parse(input);

            Assert.Equal("\"first\"", specifier.Transform(json).AsString());
        }
    }
}