using Xunit;
using DevLab.JmesPath;
namespace jmespath.net.tests.Parser
{
    public class SubExpressionTest
    {
        [Fact]
        public void ParseSubExpression()
        {
            const string input = "{\"foo\": {\"bar\": \"baz\"}}";
            const string expression = "foo.bar";

            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal("\"baz\"", result);
        }
    }
}