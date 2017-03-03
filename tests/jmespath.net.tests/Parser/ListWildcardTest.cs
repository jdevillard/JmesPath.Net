using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class ListWildcardTest
    {
        [Fact]
        public void ParseProjection()
        {
            const string json = "[{\"foo\": 1}, {\"foo\": 2}, {\"foo\": 3}]";
            const string expression = "[*].foo";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("[1,2,3]", result);
        }
    }
}