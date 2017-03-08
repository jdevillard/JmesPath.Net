using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathNotExpression : JmesPathExpression
    {
        private readonly JmesPathExpression expression_;

        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathNotExpression"/>
        /// that negates the result of evaluating its associated expression.
        /// </summary>
        /// <param name="expression"></param>
        public JmesPathNotExpression(JmesPathExpression expression)
        {
            expression_ = expression;
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            var token = expression_.Transform(json);
            return JmesPathArgument.IsFalse(token) 
                ? JmesPathArgument.True
                : JmesPathArgument.False
                ;
        }
    }
}