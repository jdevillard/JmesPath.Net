
namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class RawStringTest : ParserTestBase
    {
        [Fact]
        public void EscapeQuote()
        {
            /*
             * raw : 'foo\'bar\''  -> foo'bar'
             * C# : 'foo\\'bar\\'' -> foo'bar'
             * json :              -> "foo'bar'"
             */
            const string json = "null";
            const string expression = "'foo\\'bar\\''";
            const string expected = "\"foo'bar'\"";

            Assert(expression, json, expected);
        }

        [Fact]
        public void EscapeBackslash()
        {
            /*
             * raw : 'c:\\temp\\'    -> c:\temp\
             * C# : 'c:\\\\temp\\\\' -> c:\\temp\\
             * json :                -> "c:\\temp\\"
             */
            const string json = "null";
            const string expression = "'c:\\\\temp\\\\'";
            const string expected = "\"c:\\temp\\\"";

            Assert(expression, json, expected);
        }

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
            const string expected = "\"foo'\\\\a\\\\\"";

            Assert(expression, json, expected);
        }
    }
}
