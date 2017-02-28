using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class IdentifierTest
    {
        [Fact]
        public void ParseIdentifier()
        {
            const string input = "{\"foo\": \"value\"}";
            const string expression = "foo";

            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal("\"value\"", result);
        }
    }
}