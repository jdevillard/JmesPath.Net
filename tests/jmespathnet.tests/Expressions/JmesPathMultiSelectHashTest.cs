using System.Collections.Generic;
using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

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

    public class JmesPathMultiSelectHashTest : JmesPathExpressionsTestBase
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

            Assert(expression, "{\"foo\": \"a\", \"bar\": \"b\", \"baz\": \"c\"}", "{\"foo\":\"a\",\"bar\":\"b\"}");
            Assert(expression, "{\"foo\": \"a\", \"baz\": \"b\"}", "{\"foo\":\"a\",\"bar\":null}");

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

            Assert(expression, "{\"foo\": \"a\", \"bar\": {\"baz\": \"b\"}}", "{\"foo\":\"a\",\"bar.baz\":\"b\"}");

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

            Assert(expression, "{\"foo\": \"a\", \"bar\": [\"b\"]}", "{\"foo\":\"a\",\"firstbar\":\"b\"}");
        }
    }
}