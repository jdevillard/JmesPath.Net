using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents a pipe expression that stops projections.
    /// </summary>
    public class JmesPathPipeExpression : JmesPathCompoundExpression
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathPipeExpression"/> class
        /// with two <see cref="JmesPathExpression"/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public JmesPathPipeExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        {
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            // stop projections after
            // evaluating the left expression

            var token = Left
                .Transform(json)
                .AsJToken()
                ;

            return Right.Transform(token);
        }
    }
}