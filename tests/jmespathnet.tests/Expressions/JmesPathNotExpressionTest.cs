using DevLab.JmesPath.Expressions;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathNotExpressionTest : JmesPathExpressionsTestBase
    {
        /*
         * http://jmespath.org/specification.html#not-expressions
         * 
         * search(!True, {"True": true}) -> false
         * search(!False, {"False": false}) -> true
         * search(!Number, {"Number": 5}) -> false
         * search(!EmptyList, {"EmptyList": []}) -> true
         * 
         */

        [Theory]
        [InlineData("True", "{\"True\": true}", "false")]
        [InlineData("False", "{\"False\": false}", "true")]
        [InlineData("Number", "{\"Number\": 5}", "false")]
        [InlineData("Number", "{\"Number\": []}", "true")]
        public void JmesPathOrExpression_Tests(string identifier, string input, string expected)
            => Assert(new JmesPathNotExpression(new JmesPathIdentifier(identifier)), input, expected);
    }
}