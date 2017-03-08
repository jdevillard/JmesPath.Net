namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class HashWildcardTest : ParserTestBase
    {
        [Fact]
        public void ParseProjection()
        {
            Assert("*.foo", "{\"a\": {\"foo\": 1}, \"b\": {\"foo\": 2}, \"c\": {\"bar\": 1}}", "[1,2]");
        }

        [Fact]
        public void ParseProjection_Nested_SubExpressions()
        {
            Assert("obj.*.foo[0]", "{\"obj\": {\"a\": {\"foo\": 1}, \"b\": {\"foo\": 2}, \"c\": {\"bar\": 1}}}", "[]");
            Assert("foo.*.*.*", "{\"foo\": {\"first-1\": {\"second-1\": \"val\"},\"first-2\": {\"second-1\": \"val\"},\"first-3\": {\"second-1\": \"val\"}}}", "[[],[],[]]");
        }

        [Fact]
        public void ParseProjection_Null()
        {
            Assert("nullvalue.*", "{\"nullvalue\": null}", "null");
        }
    }
}