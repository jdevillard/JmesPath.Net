using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathIdentifierTest
    {
        /*
         * http://jmespath.org/specification.html#identifiers
         * 
         * search(foo, {"foo": "value"}) -> "value"
         * search(bar, {"foo": "value"}) -> null
         * search(foo, {"foo": [0, 1, 2]}) -> [0, 1, 2]
         * search("with space", {"with space": "value"}) -> "value"
         * search("special chars: !@#", {"special chars: !@#": "value"}) -> "value"
         * search("quote\"char", {"quote\"char": "value"}) -> "value"
         * search("\u2713", {"\u2713": "value"}) -> "value"
         */

        [Fact]
        public void JmesPathIdentifier_value()
        {
            var identifier = new JmesPathIdentifier("foo");
            const string input = "{\"foo\": \"value\"}";

            var json = JToken.Parse(input);
            
            Assert.Equal("\"value\"", identifier.Transform(json).AsString());
        }

        [Fact]
        public void JmesPathIdentifier_null()
        {
            var identifier = new JmesPathIdentifier("bar");
            const string input = "{\"foo\": \"value\"}";

            var json = JToken.Parse(input);

            Assert.Equal(null, identifier.Transform(json));
        }

        [Fact]
        public void JmesPathIdentifier_array()
        {
            var identifier = new JmesPathIdentifier("foo");
            const string input = "{\"foo\": [0, 1, 2]}";

            var json = JToken.Parse(input);

            var actual = identifier.Transform(json);

            const string output = "[0, 1, 2]";
            var expected = JToken.Parse(output) as JArray;

            Assert.NotNull(expected);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Count, ((JArray) actual).Count);
            Assert.Equal(expected[0], ((JArray)actual)[0]);
            Assert.Equal(expected[1], ((JArray)actual)[1]);
            Assert.Equal(expected[2], ((JArray)actual)[2]);
        }

        [Fact]
        public void JmesPathIdentifier_withspace_value()
        {
            var identifier = new JmesPathIdentifier("with space");
            const string input = "{\"with space\": \"value\"}";

            var json = JToken.Parse(input);

            Assert.Equal("\"value\"", identifier.Transform(json).AsString());
        }

        [Fact]
        public void JmesPathIdentifier_withspecialchars_value()
        {
            var identifier = new JmesPathIdentifier("special chars: !@#");
            const string input = "{\"special chars: !@#\": \"value\"}";

            var json = JToken.Parse(input);

            Assert.Equal("\"value\"", identifier.Transform(json).AsString());
        }

        [Fact]
        public void JmesPathIdentifier_withquote_value()
        {
            var identifier = new JmesPathIdentifier("quote\"char");
            const string input = "{\"quote\\\"char\": \"value\"}";

            var json = JToken.Parse(input);

            Assert.Equal("\"value\"", identifier.Transform(json).AsString());
        }

        [Fact]
        public void JmesPathIdentifier_withunicode_value()
        {
            var identifier = new JmesPathIdentifier("\u2713");
            const string input = "{\"\u2713\": \"value\"}";

            var json = JToken.Parse(input);

            Assert.Equal("\"value\"", identifier.Transform(json).AsString());
        }
    }
}