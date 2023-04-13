using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathLetExpression : JmesPathExpression, IScopeHolder
    {
        private readonly IList<JmesPathBinding> bindings_
            = new List<JmesPathBinding>();

        private readonly JmesPathExpression expression_;

        public JmesPathLetExpression(
            IList<JmesPathBinding> bindings,
            JmesPathExpression expression
        )
        {
            bindings_ = bindings;
            expression_ = expression;
        }

        public IScopeParticipant Scopes { get; set; }

        protected override JmesPathArgument Transform(JToken json)
        {
            Scopes?.PushScope(BindScope(json));

            try
            {
                return expression_.Transform(json);
            }
            finally
            {
                Scopes?.PopScope();
            }
        }

        public override void Accept(IVisitor visitor)
        {
            base.Accept(visitor);
            foreach (var binding in bindings_)
                binding.Expression.Accept(visitor);
            expression_.Accept(visitor);
        }

        private JToken BindScope(JToken json)
        {
            var properties = new List<JProperty>();

            foreach (var binding in bindings_)
            {
                var name = binding.Name;
                var value = binding.Expression.Transform(json).AsJToken();
                properties.Add(new JProperty(name, value));
            }
            return new JObject(properties);
        }

        protected override string Format()
            => $"let {String.Join(", ", bindings_.Select(b => b.ToString()))} in {expression_}";
    }
}