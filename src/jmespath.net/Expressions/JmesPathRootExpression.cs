using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    internal sealed class JmesPathRootExpression : JmesPathSimpleExpression
    {
        internal ScopeParticipant scopes_;

        public JmesPathRootExpression(JmesPathExpression expression)
            : base(expression)
        {
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            scopes_.SetRoot(json);
            return Expression.Transform(json);
        }
    }
}
