using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathMultiSelectListTest
    {
        [Fact]
        public void JmesPathMultiSelectList()
        {
            const string json = "{\"foo\": \"foo_value\", \"bar\": \"bar_value\"}";
            var token = JToken.Parse(json);

            var items = new List<JmesPathExpression>
            {
                new JmesPathIdentifier("foo"),
                new JmesPathIdentifier("bar"),
            };

            var select = new JmesPathMultiSelectList(items);
            var result = select.Transform(token);

            Assert.Equal(JTokenType.Array, result.Type);
            Assert.Equal("[\"foo_value\",\"bar_value\"]", result.Token.AsString());
        }
    }
}