namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class LiteralTest : ParserTestBase
    {
        [Fact]
        public void ParseLiteralString()
        {
            /*
             * raw : `\"foo\"`
             * json : "\"foo\""
             */
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "`\"foo\"`";
            const string expected = "\"foo\"";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ParseLiteralString_WithIdentifier()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "{\"a\":`\"foo\"`}";
            const string expected = "{\"a\":\"foo\"}";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ParseLiteralString_Composition()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "`{\"foo\": [\"first\",\"second\"]}`.foo";
            const string expected = "[\"first\",\"second\"]";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ParseLiteralString_CompositionBracketSpecifier()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "`[\"first\",\"second\"]`[0]";
            const string expected = "\"first\"";

            Assert(expression, json, expected);
        }

        [Fact]
        public void ParseLiteralString_CompositionObjectValue()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "{\"bar\":`{\"foo\": [\"first\",\"second\"]}`.foo}";
            const string expected = "{\"bar\":[\"first\",\"second\"]}";

            Assert(expression, json, expected);
        }
    }
}