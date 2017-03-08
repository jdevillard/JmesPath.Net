using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    /*
     * http://jmespath.org/specification.html#or-expressions
     * 
     * search(True && False, {"True": true, "False": false}) -> false
     * search(Number && EmptyList, {"Number": 5, "EmptyList": []}) -> []
     * search(foo[?a == `1` && b == `2`],
     *        {"foo": [{"a": 1, "b": 2}, {"a": 1, "b": 3}]}) -> [{"a": 1, "b": 2}]
     * 
     */

    public class JmespathAndExpressionTest : JmesPathExpressionsTestBase
    {
        [Fact]
        public void JmespathAndExpression_TrueAndFalse()
        {
            var expression = new JmesPathAndExpression(
                new JmesPathIdentifier("True"),
                new JmesPathIdentifier("False")
                );

            Assert(expression, "{\"True\": true, \"False\": false}", "false");
        }

        //[Fact]
        //public void JmespathAndExpression_FilterExpression()
        //{
        //    var expression = new JmesPathIndexExpression(
        //        new JmesPathIdentifier("foo"),
        //        new JmesFilterExpression(
        //            new JmesPathAndExpression(
        //                new JmesPathCompareEqual(
        //                    new JmesPathIdentifier("a"),
        //                    new JmesPathLiteral(1)
        //                    ),
        //                new JmesPathCompareEqual(
        //                    new JmesPathIdentifier("b"),
        //                    new JmesPathLiteral(2)
        //                    )
        //                )
        //            )
        //        );

        //    Assert(expression, "{\"foo\": [{\"a\": 1, \"b\": 2}, {\"a\": 1, \"b\": 3}]}", "[{\"a\": 1, \"b\": 2}]");
        //}


        [Fact]
        public void JmespathAndExpression_NumberAndEmptyList()
        {
            var expression = new JmesPathAndExpression(
                new JmesPathIdentifier("Number"),
                new JmesPathIdentifier("EmptyList")
                );

            Assert(expression, "{\"Number\": 5, \"EmptyList\": []}", "[]");
        }
    }
}