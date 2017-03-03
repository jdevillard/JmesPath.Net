using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    /*
     * http://jmespath.org/specification.html#multiselect-hash
     * 
     * search({foo: foo, bar: bar}, {"foo": "a", "bar": "b", "baz": "c"})
     *               -> {"foo": "a", "bar": "b"}
     * search({foo: foo, bar: bar}, {"foo": "a", "baz": "b"})
     *               -> {"foo": "a", "bar": null}
     * search({foo: foo, "bar.baz": bar.baz}, {"foo": "a", "bar": {"baz": "b"}})
     *               -> {"foo": "a", "bar.baz": "b"}
     * search({foo: foo, firstbar: bar[0]}, {"foo": "a", "bar": ["b"]})
     *               -> {"foo": "a", "firstbar": "b"}
     * 
     */

    public class JmesPathMultiSelectHashTest
    {
        [Fact]
        public void JmesPathMultiSelectHash()
        {
            JmesPathMultiSelectHash expression = new JmesPathMultiSelectHash(
                new Dictionary<string, JmesPathExpression>
                {
                    {"foo", new JmesPathIdentifier("foo")},
                    {"bar", new JmesPathIdentifier("bar")},
                }
                );

            JmesPathMultiSelectList_Transform(expression, "{\"foo\": \"a\", \"bar\": \"b\", \"baz\": \"c\"}", "{\"foo\":\"a\",\"bar\":\"b\"}");
            JmesPathMultiSelectList_Transform(expression, "{\"foo\": \"a\", \"baz\": \"b\"}", "{\"foo\":\"a\",\"bar\":null}");

            expression = new JmesPathMultiSelectHash(
                new Dictionary<string, JmesPathExpression>
                {
                    {"foo", new JmesPathIdentifier("foo")},
                    {
                        "bar.baz", new JmesPathSubExpression(
                            new JmesPathIdentifier("bar"),
                            new JmesPathIdentifier("baz"))
                    },
                }
                );

            JmesPathMultiSelectList_Transform(expression, "{\"foo\": \"a\", \"bar\": {\"baz\": \"b\"}}", "{\"foo\":\"a\",\"bar.baz\":\"b\"}");

            expression = new JmesPathMultiSelectHash(
                new Dictionary<string, JmesPathExpression>
                {
                    {"foo", new JmesPathIdentifier("foo")},
                    {
                        "firstbar", new JmesPathIndexExpression(
                            new JmesPathIdentifier("bar"),
                            new JmesPathIndex(0))
                    },
                }
                );

            JmesPathMultiSelectList_Transform(expression, "{\"foo\": \"a\", \"bar\": [\"b\"]}", "{\"foo\":\"a\",\"firstbar\":\"b\"}");
        }

        public void JmesPathMultiSelectList_Transform(JmesPathMultiSelectHash expression, string input, string expected)
        {
            var token = JToken.Parse(input);
            var result = expression.Transform(token);

            Assert.Equal(JTokenType.Object, result.Type);
            Assert.Equal(expected, result.Token.AsString());
        }
    }
}