using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class BracketSpecifierTest
    {
        [Fact]
        public void ParseBracketSpecifier()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "foo[0]";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("\"first\"", result);
        }
    }
}