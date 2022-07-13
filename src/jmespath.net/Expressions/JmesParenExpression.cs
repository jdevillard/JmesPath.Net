using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesParenExpression : JmesPathExpression
    {
        private readonly JmesPathExpression expression_;

        public JmesParenExpression(JmesPathExpression expression)
        {
            expression_ = expression;
        }

        protected override JmesPathArgument Transform(JToken json)
            => expression_.Transform(json);
    }
}