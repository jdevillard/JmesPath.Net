using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class SliceExpressionTest
    {
        [Fact]
        public void ParseSliceExpression_IndexExpression()
        {
            const string json = "{\"foo\": [0, 1, 2, 3]}";
            const string expression = "foo[0:4:1]";
            const string expected = "[0,1,2,3]";

            ParseSliceExpression_Transform(expression, json, expected);
        }

        [Fact]
        public void ParseSliceExpression_BracketSpecifier()
        {
            const string json = "[0, 1, 2, 3]";
            const string expression = "[0:4:1]";
            const string expected= "[0,1,2,3]";

            ParseSliceExpression_Transform(expression, json, expected);
        }

        [Fact]
        public void ParseSliceExpression_BracketSpecifier_Empty()
        {
            const string json = "[0, 1, 2, 3]";
            const string expression = "[]";
            const string expected = "[0,1,2,3]";

            ParseSliceExpression_Transform(expression, json, expected);
        }

        [Fact]
        public void ParseSliceExpression_Compliance()
        {
            const string json = "{\"foo\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],\"bar\": {\"baz\": 1}}";
            const string expression = "foo[:10:]";
            const string expected = "[0,1,2,3,4,5,6,7,8,9]";

            ParseSliceExpression_Transform(expression, json, expected);
        }

        private void ParseSliceExpression_Transform(string expression, string input, string expected)
        {
            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal(expected, result);
        }
    }
}