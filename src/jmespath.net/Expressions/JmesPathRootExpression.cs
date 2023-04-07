using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    internal sealed class JmesPathRootExpression : JmesPathSimpleExpression, IScopeHolder
    {
        public JmesPathRootExpression(JmesPathExpression expression)
            : base(expression)
        {
        }

        public IScopeParticipant Scopes { get; set; }

        protected override JmesPathArgument Transform(JToken json)
        {
            Scopes.SetRoot(json);
            return Expression.Transform(json);
        }
    }
}
