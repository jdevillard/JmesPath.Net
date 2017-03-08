using System;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents the base class for a JmesPath sub or index expression.
    /// </summary>
    public class JmesPathCompoundExpression : JmesPathExpression
    {
        private readonly JmesPathExpression left_;
        private readonly JmesPathExpression right_;

        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathCompoundExpression"/> class
        /// with two <see cref="JmesPathExpression"/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        protected JmesPathCompoundExpression(JmesPathExpression left, JmesPathExpression right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            left_ = left;
            right_ = right;
        }

        protected JmesPathExpression Left
            => left_;

        protected JmesPathExpression Right
            => right_;

        protected override JmesPathArgument Transform(JToken json)
        {
            var token = left_.Transform(json);
            return right_.Transform(token);
        }
    }
}