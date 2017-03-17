using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathExpressionType : JmesPathExpression
    {
        private readonly JmesPathExpression expression_;

        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathExpressionType"/> class
        /// with one <see cref="JmesPathExpression"/> expression.
        /// </summary>
        /// <param name="expression"></param>
        public JmesPathExpressionType(JmesPathExpression expression )
        {
            expression_ = expression;
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            return expression_.Transform(json);
        }
    }
}