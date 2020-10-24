namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class BlockTest: ParserTestBase
    {
        [Fact]
        public void ParseBlock()
        {
            Assert("{% let var1 = name %} foo[?name == 'a']", "{\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "[{\"name\":\"a\"}]");
        }

        [Fact]
        public void ParseVarOnly()
        {
            Assert("{% let var1 = name %}", "{\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "{\"foo\":[{\"name\":\"a\"},{\"name\":\"b\"}]}");
        }
    }
}