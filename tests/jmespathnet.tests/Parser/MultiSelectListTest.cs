namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class MultiSelectListTest : ParserTestBase
    {
        [Fact]
        public void ParseMultiSelectList()
        {
            const string input = "{\"foo\": \"foo_value\", \"bar\": \"bar_value\"}";
            const string expression = "[foo, bar]";
            const string expected = "[\"foo_value\",\"bar_value\"]";

            Assert(expression, input, expected);
        }

        [Fact]
        public void ParseMultiSelectList_Compliance()
        {
            Assert("[[*]]", "[]", "[[]]");
        }
    }
}