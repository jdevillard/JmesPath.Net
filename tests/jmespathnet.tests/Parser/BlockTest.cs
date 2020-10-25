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

        [Fact]
        public void ParseSimpleVar()
        {
            Assert("{% let var1 = name %} variable('var1')", "{\"name\": \"a\"}", "\"a\"");
        }

        [Fact]
        public void ParseObjectVar()
        {
            Assert("{% let var1 = foo %} variable('var1')", "{\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "[{\"name\":\"a\"},{\"name\":\"b\"}]");
        }

        [Fact]
        public void ParseObjectVarWithIdentifier()
        {
            Assert("{% let var1 = foo %} variable('var1')[0]", "{\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "{\"name\":\"a\"}");
        }
    }
}