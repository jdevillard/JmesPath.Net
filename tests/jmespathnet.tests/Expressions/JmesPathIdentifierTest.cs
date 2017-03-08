using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

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

        [Fact]
        public void JmesPathIdentifier_Transform()
        {
            Assert("foo", "{\"foo\": \"value\"}", "\"value\"");
            Assert("bar", "{\"foo\": \"value\"}", "null");
            Assert("foo", "{\"foo\": [0, 1, 2]}", "[0,1,2]");
            Assert("with space", "{\"with space\": \"value\"}", "\"value\"");
            Assert("special chars: !@#", "{\"special chars: !@#\": \"value\"}", "\"value\"");
            Assert("quote\"char", "{\"quote\\\"char\": \"value\"}", "\"value\"");
            Assert("\u2713", "{\"\u2713\": \"value\"}", "\"value\"");
        }

        private void Assert(string identifier, string input, string expected)
        {
            var expression = new JmesPathIdentifier(identifier);
            Assert(expression, input, expected);
        }
    }
}