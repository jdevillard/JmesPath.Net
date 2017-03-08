using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    /*
     * http://jmespath.org/specification.html#not-expressions
     * 
     * search(!True, {"True": true}) -> false
     * search(!False, {"False": false}) -> true
     * search(!Number, {"Number": 5}) -> false
     * search(!EmptyList, {"EmptyList": []}) -> true
     * 
     */

    public class JmesPathNotExpressionTest : JmesPathExpressionsTestBase
    {
        [Fact]
        public void JmesPathOrExpression_NotTrue()
        {
            var expression = new JmesPathNotExpression(
                new JmesPathIdentifier("True")
                );

            Assert(expression, "{\"True\": true}", "false");
        }

        [Fact]
        public void JmesPathOrExpression_NotFalse()
        {
            var expression = new JmesPathNotExpression(
                new JmesPathIdentifier("False")
                );

            Assert(expression, "{\"False\": false}", "true");
        }

        [Fact]
        public void JmesPathOrExpression_NotNumber()
        {
            var expression = new JmesPathNotExpression(
                new JmesPathIdentifier("Number")
                );

            Assert(expression, "{\"Number\": 5}", "false");
        }

        [Fact]
        public void JmesPathOrExpression_NotEmptyList()
        {
            var expression = new JmesPathNotExpression(
                new JmesPathIdentifier("Number")
                );

            Assert(expression, "{\"EmptyList\": []}", "true");
        }
    }
}