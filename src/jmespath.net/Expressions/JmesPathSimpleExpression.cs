using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathSimpleExpression : JmesPathExpression
    {
        private readonly JmesPathExpression expression_;

        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathSimpleExpression"/> class.
        /// </summary>
        /// <param name="expression"></param>
        protected JmesPathSimpleExpression(JmesPathExpression expression)
        {
            expression_ = expression;
        }

        protected JmesPathExpression Expression
            => expression_;

        protected override JmesPathArgument Transform(JToken json)
        {
            return expression_.Transform(json);
        }

        public override void Accept(IVisitor visitor)
        {
            base.Accept(visitor);
            expression_.Accept(visitor);
        }
    }
}