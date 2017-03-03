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

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("[0,1,2,3]", result);
        }

        [Fact]
        public void ParseSliceExpression_BracketSpecifier()
        {
            const string json = "[0, 1, 2, 3]";
            const string expression = "[0:4:1]";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("[0,1,2,3]", result);
        }

        [Fact]
        public void ParseSliceExpression_BracketSpecifier_Empty()
        {
            const string json = "[0, 1, 2, 3]";
            const string expression = "[]";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("[0,1,2,3]", result);
        }
    }
}