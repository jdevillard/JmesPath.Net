namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class IdentifierTest : ParserTestBase
    {
        [Fact]
        public void ParseIdentifier()
        {
            const string input = "{\"foo\": \"value\"}";
            const string expression = "foo";
            const string expected = "\"value\"";

            Assert(expression, input, expected);
        }
    }
}