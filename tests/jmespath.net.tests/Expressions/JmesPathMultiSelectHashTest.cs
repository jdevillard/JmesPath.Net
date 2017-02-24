using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathMultiSelectHashTest
    {
        [Fact]
        public void JmesPathMultiSelectHash()
        {
            const string json = "{\"foo\": \"value\"}";
            var token = JToken.Parse(json);

            var dictionary = new Dictionary<string, JmesPathExpression>
            {
                { "bar", new JmesPathIdentifier("foo") },
            };

            var select = new JmesPathMultiSelectHash(dictionary);
            var result = select.Transform(token);

            Assert.Equal(JTokenType.Object, result.Type);
            Assert.Equal("{\"bar\":\"value\"}", result.AsString());
        }
    }
}