using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class FlattenTest
    {
        [Fact]
        public void ParseProjection()
        {
            const string json = "{\"foo\": [[0, 1], 2, [3], 4, [5, [6, 7]]] }";
            const string expression = "foo[]";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("[0,1,2,3,4,5,[6,7]]", result);
        }
    }
}