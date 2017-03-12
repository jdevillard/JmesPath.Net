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

        [Fact]
        public void ParseIdentifier_Compliance()
        {
            // search("\udadd\udfc7\\ueFAc", {"\udadd\udfc7\\ueFAc": true}) -> true
            // search("\"\\\\\\u4FDc\"", {"\\\u4FDc": true}) -> true

            Assert("\"\\udadd\\udfc7\\\\ueFAc\"", "{\"\\udadd\\udfc7\\\\ueFAc\": true}	", "true");
            Assert("\"\\\\\\u4FDc\"", "{\"\\\\\\u4FDc\": true}	", "true");
        }
    }
}