namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class FunctionTest : ParserTestBase
    {
       [Fact]
        public void ParseJsonToFunction()
        {
            const string json = "{\"foo\": [\"1\",\"second\"]}";
            const string expression = "{\"baz\":to_number(`{\"foo\":[\"42\",\"12\"]}`.foo[0])}";
           const string expected = "{\"baz\":42}";

            Assert(expression, json, expected);
        }
    }
}