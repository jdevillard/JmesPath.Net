using System;
using System.Threading.Tasks;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents the base class for a JmesPath expression that
    /// operates on a sequence of two expressions.
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
            return token.IsNull() ? token : right_.Transform(token);
        }

        protected override async Task<JmesPathArgument> TransformAsync(JToken json)
        {
            var token = await left_.TransformAsync(json);
            return token.IsNull() ? token : await right_.TransformAsync(token);
        }

        public override void Accept(IVisitor visitor)
        {
            base.Accept(visitor);
            Left.Accept(visitor);
            Right.Accept(visitor);
        }

        protected override string Format()
            => $"{Left}{Right}";
    }
}