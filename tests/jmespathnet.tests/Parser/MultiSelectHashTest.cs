using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class MultiSelectHashTest
    {
        [Fact]
        public void ParseMultiSelectHash()
        {
            const string input = "{\"foo\": \"value\"}";
            const string expression = "{\"bar\": foo}";

            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal("{\"bar\":\"value\"}", result);
        }

        [Fact]
        public void ParseMultiSelectHash_WithTwoProps()
        {
            const string input = "{\"foo\": \"value\"}";
            const string expression = "{\"bar\": foo,\"baz\":'bazvalue'}";

            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal("{\"bar\":\"value\",\"baz\":\"bazvalue\"}", result);
        }
    }
}