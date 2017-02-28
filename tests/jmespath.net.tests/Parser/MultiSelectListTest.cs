using Xunit;
using DevLab.JmesPath;
namespace jmespath.net.tests.Parser
{
    public class MultiSelectListTest
    {
        [Fact]
        public void ParseMultiSelectList()
        {
            const string input = "{\"foo\": \"foo_value\", \"bar\": \"bar_value\"}";
            const string expression = "[foo, bar]";

            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal("[\"foo_value\",\"bar_value\"]", result);
        }
    }
}