using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class IndexTest
    {
        [Fact]
        public void BracketSpecifier_IndexExpression()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "foo[0]";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("\"first\"", result);
        }

        [Fact]
        public void BracketSpecifier_BracketSpecifier()
        {
            const string json = "[\"first\",\"second\"]";
            const string expression = "[0]";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("\"first\"", result);
        }
    }
}