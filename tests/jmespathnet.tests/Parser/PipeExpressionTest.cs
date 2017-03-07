using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class PipeExpressionTest
    {
        [Fact]
        public void ParsePipeExpression()
        {
            const string json = "{\"foo\": {\"bar\": \"baz\"}}";
            const string expression = "foo | bar";
            const string expected = "\"baz\"";

            ParsePipeExpression_Transform(expression, json, expected);
        }

        [Fact]
        public void ParsePipeExpression_Compliance()
        {
            const string json = "{\"foo\": {\"bar\": {\"baz\": \"subkey\"},\"other\": {\"baz\": \"subkey\"},\"other2\": {\"baz\": \"subkey\"},\"other3\": {\"notbaz\": [\"a\", \"b\", \"c\"]},\"other4\": {\"notbaz\": [\"a\", \"b\", \"c\"]}}}";
            const string expression = "{\"a\": foo.bar, \"b\": foo.other} | *.baz";
            const string expected = "[\"subkey\",\"subkey\"]";

            ParsePipeExpression_Transform(expression, json, expected);
        }

        private void ParsePipeExpression_Transform(string expression, string input, string expected)
        {
            var path = new JmesPath();

            var result = path.Transform(input, expression);
            Assert.Equal(expected, result);
        }
    }
}