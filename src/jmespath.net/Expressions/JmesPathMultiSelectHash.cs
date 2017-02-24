using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathMultiSelectHash : JmesPathExpression
    {
        private readonly IDictionary<string, JmesPathExpression> dictionary_
            = new Dictionary<string, JmesPathExpression>()
            ;

        public JmesPathMultiSelectHash(IDictionary<string, JmesPathExpression> dictionary)
        {
            foreach (var key in dictionary.Keys)
                dictionary_.Add(key, dictionary[key]);
        }

        public override JToken Transform(JToken json)
        {
            var properties = new List<JProperty>();

            foreach (var key in dictionary_.Keys)
            {
                var expression = dictionary_[key];
                var result = expression.Transform(json);

                properties.Add(new JProperty(key, result));
            }

            return new JObject(properties);
        }
    }
}