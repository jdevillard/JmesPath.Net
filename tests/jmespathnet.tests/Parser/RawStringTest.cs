
namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class RawStringTest : ParserTestBase
    {
        [Fact]
        public void ParseRawString()
        {
            /*
             * raw : 'foo\'\a\\'
             * C# : foo'\a\\
             * json : "foo'\\a\\\\"
             */
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "'foo\\'\\a\\\\'";
            const string expected = "\"foo'\\\\a\\\\\\\\\"";

            Assert(expression, json, expected);
        }
    }
}
