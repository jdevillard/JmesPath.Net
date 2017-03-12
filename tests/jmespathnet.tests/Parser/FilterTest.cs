namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class FilterTest : ParserTestBase
    {
        [Fact]
        public void ParseFilter_Compliance()
        {
            Assert("foo[?name == 'a']", "{\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "[{\"name\":\"a\"}]");
            Assert("foo[?first == last].first", "{\"foo\": [{\"first\": \"foo\", \"last\": \"bar\"},{\"first\": \"foo\", \"last\": \"foo\"},{\"first\": \"foo\", \"last\": \"baz\"}]}", "[\"foo\"]");
            Assert("foo[?age < `25`]", "{\"foo\": [{\"age\": 20},{\"age\": 25},{\"age\": 30}]}", "[{\"age\":20}]");
        }
    }
}