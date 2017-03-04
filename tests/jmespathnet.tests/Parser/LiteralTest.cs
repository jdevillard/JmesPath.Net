using Xunit;
using DevLab.JmesPath;

namespace jmespath.net.tests.Parser
{
    public class LiteralTest
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

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("\"foo\"", result);
        }

        [Fact]
        public void ParseLiteralString_WithIdentifier()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "{\"a\":`\"foo\"`}";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("{\"a\":\"foo\"}", result);
        }

        [Fact]
        public void ParseLiteralString_Composition()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "`{\"foo\": [\"first\",\"second\"]}`.foo";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("[\"first\",\"second\"]", result);
        }

        [Fact]
        public void ParseLiteralString_CompositionBracketSpecifier()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "`[\"first\",\"second\"]`[0]";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("\"first\"", result);
        }

        [Fact]
        public void ParseLiteralString_CompositionObjectValue()
        {
            const string json = "{\"foo\": [\"first\",\"second\"]}";
            const string expression = "{\"bar\":`{\"foo\": [\"first\",\"second\"]}`.foo}";

            var path = new JmesPath();

            var result = path.Transform(json, expression);
            Assert.Equal("{\"bar\":[\"first\",\"second\"]}", result);
        }
    }
}