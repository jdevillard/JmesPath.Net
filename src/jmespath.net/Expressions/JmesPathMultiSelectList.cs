using System.Collections.Generic;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Utils;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathMultiSelectList : JmesPathExpression
    {
        private readonly IList<JmesPathExpression> expressions_
            = new List<JmesPathExpression>()
            ;

        public JmesPathMultiSelectList(params JmesPathExpression[] expressions)
            : this((IEnumerable<JmesPathExpression>)expressions)
        {
        }

        public JmesPathMultiSelectList(IEnumerable<JmesPathExpression> expressions)
        {
            foreach (var expression in expressions)
                expressions_.Add(expression);
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            var items = new List<JToken>();
            foreach (var expression in expressions_)
            {
                var result = expression.Transform(json).AsJToken();
                items.Add(result);
            }

            return new JArray().AddRange(items);
        }

        public override void Accept(IVisitor visitor)
        {
            base.Accept(visitor);            
            foreach (var expression in expressions_)
                expression.Accept(visitor);
        }
    }
}