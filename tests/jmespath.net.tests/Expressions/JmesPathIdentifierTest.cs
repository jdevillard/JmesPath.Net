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
        public void JmesPathIdentifier_Transform()
        {
            JmesPathIdentifier_Transform("foo", "{\"foo\": \"value\"}", "\"value\"");
            JmesPathIdentifier_Transform("bar", "{\"foo\": \"value\"}", null);
            JmesPathIdentifier_Transform("foo", "{\"foo\": [0, 1, 2]}", "[0,1,2]");
            JmesPathIdentifier_Transform("with space", "{\"with space\": \"value\"}", "\"value\"");
            JmesPathIdentifier_Transform("special chars: !@#", "{\"special chars: !@#\": \"value\"}", "\"value\"");
            JmesPathIdentifier_Transform("quote\"char", "{\"quote\\\"char\": \"value\"}", "\"value\"");
            JmesPathIdentifier_Transform("\u2713", "{\"\u2713\": \"value\"}", "\"value\"");
        }

        private void JmesPathIdentifier_Transform(string expression, string input, string expected)
        {
            var identifier = new JmesPathIdentifier(expression);
            var token = JToken.Parse(input);

            var result = identifier.Transform(token);
            var actual = result.Token?.AsString();

            Assert.Equal(expected, actual);
        }
    }
}