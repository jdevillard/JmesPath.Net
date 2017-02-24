using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathMultiSelectList : JmesPathExpression
    {
        private readonly IList<JmesPathExpression> expressions_
            = new List<JmesPathExpression>()
            ;

        public JmesPathMultiSelectList(IList<JmesPathExpression> expressions)
        {
            foreach (var expression in expressions)
                expressions_.Add(expression);
        }

        public override JToken Transform(JToken json)
        {
            var items = new List<JToken>();
            foreach (var expression in expressions_)
            {
                var result = expression.Transform(json);
                // TODO: what is result == null ?

                items.Add(result);
            }

            return new JArray(items);
        }
    }
}