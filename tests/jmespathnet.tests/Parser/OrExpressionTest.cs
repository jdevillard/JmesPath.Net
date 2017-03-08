namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class OrExpressionTest : ParserTestBase
    {
        [Fact]
        public void ParseOrExpression()

        {
            const string json = "{\"outer\": {\"foo\": \"foo\",\"bar\": \"bar\",\"baz\": \"baz\"}}";
            const string expression = "outer.foo || outer.bar";
            const string expected = "\"foo\"";

            Assert(expression, json, expected);
        }
    }
}