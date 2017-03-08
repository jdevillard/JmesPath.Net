namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class ListWildcardTest : ParserTestBase
    {
        [Fact]
        public void ParseProjection()
        {
            Assert("[*].foo", "[{\"foo\": 1}, {\"foo\": 2}, {\"foo\": 3}]", "[1,2,3]");
            Assert("foo[*].bar[*].kind", "{\"foo\": [{\"bar\": [{\"kind\": \"basic\"}, {\"kind\": \"intermediate\"}]},{\"bar\": [{\"kind\": \"advanced\"}, {\"kind\": \"expert\"}]},{\"bar\": \"string\"}]}", "[[\"basic\",\"intermediate\"],[\"advanced\",\"expert\"]]");
        }
    }
}