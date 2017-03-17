namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class FunctionTest : ParserTestBase
    {
        [Fact]
        public void ParseJsonFunction()
        {
            const string json = "{\"foo\": [\"1\",\"second\"]}";
            const string expression = "{\"baz\":to_number(`{\"foo\":[\"42\",\"12\"]}`.foo[0])}";
            const string expected = "{\"baz\":42}";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ParseJsonFunction_Compliance()
        {
            Assert("people[?age > `20`].to_string(age).to_number(@)", "{\"people\": [{\"age\": 20,\"other\": \"foo\",\"name\": \"Bob\"},{\"age\": 25,\"other\": \"bar\",\"name\": \"Fred\"},{\"age\": 30,\"other\": \"baz\",\"name\": \"George\"}]}", "[25,30]");
        }
    }
}