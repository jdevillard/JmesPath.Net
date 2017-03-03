using Xunit;
using DevLab.JmesPath;
namespace jmespath.net.tests.Parser
{
    public class SubExpressionTest
    {
        [Fact]
        public void ParseSubExpression_Identifier()
        {
            const string input = "{\"foo\": {\"bar\": \"baz\"}}";
            const string expression = "foo.bar";

            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal("\"baz\"", result);
        }

        [Fact]
        public void ParseSubExpression_Wildcard()
        {
            const string input = "{\"foo\": {\"a\": 1, \"b\": 2}}";
            const string expression = "foo.*";

            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal("[1,2]", result);
        }
    }
}