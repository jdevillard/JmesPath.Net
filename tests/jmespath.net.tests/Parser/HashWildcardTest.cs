using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class HashWildcardTest
    {
        [Fact]
        public void ParseProjection()
        {
            const string json = "{\"a\": {\"foo\": 1}, \"b\": {\"foo\": 2}, \"c\": {\"bar\": 1}}";
            const string expression = "*.foo";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("[1,2]", result);
        }
    }
}