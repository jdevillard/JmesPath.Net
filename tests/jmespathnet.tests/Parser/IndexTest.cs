
namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class IndexTest : ParserTestBase
    {
        [Fact]
        public void BracketSpecifier_IndexExpression()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "foo[0]";
            const string expected = "\"first\"";

            Assert(expression, json, expected);
        }

        [Fact]
        public void BracketSpecifier_BracketSpecifier()
        {
            const string json = "[\"first\",\"second\"]";
            const string expression = "[0]";
            const string expected = "\"first\"";

            Assert(expression, json, expected);
        }
    }
}