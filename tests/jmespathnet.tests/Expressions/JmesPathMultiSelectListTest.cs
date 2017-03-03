using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathMultiSelectListTest
    {
        /*
         * http://jmespath.org/specification.html#multiselect-list
         * 
         * search([foo,bar], {"foo": "a", "bar": "b", "baz": "c"}) -> ["a", "b"]
         * search([foo,bar], {"foo": "a", "baz": "b"}) -> ["a", null]
         * search([foo,bar.baz], {"foo": "a", "bar": {"baz": "b"}}) -> ["a", "b"]
         * search([foo,bar[0]], {"foo": "a", "bar": ["b"], "baz": "c"}) -> ["a", "b"]
         * 
         */
        [Fact]
        public void JmesPathMultiSelectList()
        {
            JmesPathMultiSelectList expression = new JmesPathMultiSelectList(
                new JmesPathIdentifier("foo"),
                new JmesPathIdentifier("bar"));

            JmesPathMultiSelectList_Transform(expression, "{\"foo\": \"a\", \"bar\": \"b\", \"baz\": \"c\"}", "[\"a\",\"b\"]");
            JmesPathMultiSelectList_Transform(expression, "{\"foo\": \"a\", \"baz\": \"b\"}", "[\"a\",null]");

            expression = new JmesPathMultiSelectList(
                new JmesPathIdentifier("foo"),
                new JmesPathSubExpression(
                    new JmesPathIdentifier("bar"),
                    new JmesPathIdentifier("baz")
                    )
                );

            JmesPathMultiSelectList_Transform(expression, "{\"foo\": \"a\", \"bar\": {\"baz\": \"b\"}}", "[\"a\",\"b\"]");

            expression = new JmesPathMultiSelectList(
                new JmesPathIdentifier("foo"),
                new JmesPathIndexExpression(
                    new JmesPathIdentifier("bar"),
                    new JmesPathIndex(0)
                    )
                );

            JmesPathMultiSelectList_Transform(expression, "{\"foo\": \"a\", \"bar\": [\"b\"], \"baz\": \"c\"}", "[\"a\",\"b\"]");
        }

        public void JmesPathMultiSelectList_Transform(JmesPathMultiSelectList expression, string input, string expected)
        {
            var token = JToken.Parse(input);
            var result = expression.Transform(token);

            Assert.Equal(JTokenType.Array, result.Type);
            Assert.Equal(expected, result.Token.AsString());
        }
    }
}