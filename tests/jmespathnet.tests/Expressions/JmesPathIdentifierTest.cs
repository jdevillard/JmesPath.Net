using DevLab.JmesPath.Expressions;
using Xunit;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathIdentifierTest : JmesPathExpressionsTestBase
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

        [Theory]
        [InlineData("foo", "{\"foo\": \"value\"}", "\"value\"")]
        [InlineData("bar", "{\"foo\": \"value\"}", "null")]
        [InlineData("foo", "{\"foo\": [0, 1, 2]}", "[0,1,2]")]
        [InlineData("with space", "{\"with space\": \"value\"}", "\"value\"")]
        [InlineData("special chars: !@#", "{\"special chars: !@#\": \"value\"}", "\"value\"")]
        [InlineData("quote\"char", "{\"quote\\\"char\": \"value\"}", "\"value\"")]
        [InlineData("\u2713", "{\"\u2713\": \"value\"}", "\"value\"")]
        public void JmesPathIdentifier_Transform(string identifier, string input, string expected)
            => Assert(identifier, input, expected);

        private void Assert(string identifier, string input, string expected)
        {
            var expression = new JmesPathIdentifier(identifier);
            Assert(expression, input, expected);
        }
    }
}