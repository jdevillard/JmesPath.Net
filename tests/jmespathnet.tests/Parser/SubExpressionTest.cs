
namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class SubExpressionTest : ParserTestBase
    {
        [Fact]
        public void ParseSubExpression_Identifier()
        {
            const string input = "{\"foo\": {\"bar\": \"baz\"}}";
            const string expression = "foo.bar";
            const string expected = "\"baz\"";

            Assert(expression, input, expected);
        }

        [Fact]
        public void ParseSubExpression_Wildcard()
        {
            const string input = "{\"foo\": {\"a\": 1, \"b\": 2}}";
            const string expression = "foo.*";
            const string expected = "[1,2]";

            Assert(expression, input, expected);
        }
    }
}