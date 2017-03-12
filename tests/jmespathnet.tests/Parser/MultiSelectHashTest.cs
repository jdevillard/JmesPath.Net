namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class MultiSelectHashTest: ParserTestBase
    {
        [Fact]
        public void ParseMultiSelectHash()
        {
            const string input = "{\"foo\": \"value\"}";
            const string expression = "{\"bar\": foo}";
            const string expected = "{\"bar\":\"value\"}";

            Assert(expression, input, expected);
        }

        [Fact]
        public void ParseMultiSelectHash_WithTwoProps()
        {
            const string input = "{\"foo\": \"value\"}";
            const string expression = "{\"bar\": foo,\"baz\":'bazvalue'}";
            const string expected = "{\"bar\":\"value\",\"baz\":\"bazvalue\"}";

            Assert(expression, input, expected);
        }

        [Fact]
        public void ParseMultiSelectHash_Compliance()
        {
            Assert("missing.{foo: bar}", "[]", "null");
        }
    }
}