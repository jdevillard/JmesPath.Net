
namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class PipeExpressionTest : ParserTestBase
    {
        [Fact]
        public void ParsePipeExpression()
        {
            const string json = "{\"foo\": {\"bar\": \"baz\"}}";
            const string expression = "foo | bar";
            const string expected = "\"baz\"";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ParsePipeExpression_Compliance()
        {
            const string json = "{\"foo\": {\"bar\": {\"baz\": \"subkey\"},\"other\": {\"baz\": \"subkey\"},\"other2\": {\"baz\": \"subkey\"},\"other3\": {\"notbaz\": [\"a\", \"b\", \"c\"]},\"other4\": {\"notbaz\": [\"a\", \"b\", \"c\"]}}}";
            const string expression = "{\"a\": foo.bar, \"b\": foo.other} | *.baz";
            const string expected = "[\"subkey\",\"subkey\"]";

            Assert(expression, json, expected);
        }
    }
}