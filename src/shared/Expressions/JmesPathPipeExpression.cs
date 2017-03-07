using System;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents a pipe expression that stops projections.
    /// </summary>
    public class JmesPathPipeExpression : JmesPathExpression
    {
        private readonly JmesPathExpression left_;
        private readonly JmesPathExpression right_;

        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathPipeExpression"/> class
        /// with two <see cref="JmesPathExpression"/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public JmesPathPipeExpression(JmesPathExpression left, JmesPathExpression right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            left_ = left;
            right_ = right;
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            var token = left_.Transform(json);
            return right_.Transform(token.AsJToken());
        }
    }
}