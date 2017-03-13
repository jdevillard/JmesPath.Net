using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public abstract class JmesPathComparison : JmesPathExpression
    {
        private readonly JmesPathExpression left_;
        private readonly JmesPathExpression right_;

        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathComparison" /> class
        /// that performs a comparison between two specified expressions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        protected JmesPathComparison(JmesPathExpression left, JmesPathExpression right)
        {
            left_ = left;
            right_ = right;
        }

        /// <summary>
        /// Gets the left hand expression for the comparison.
        /// </summary>
        protected JmesPathExpression Left
            => left_;

        /// <summary>
        /// Gets the right hand expression for the comparison.
        /// </summary>
        protected JmesPathExpression Right
            => right_;

        protected abstract bool? Compare(JToken left, JToken right);

        protected override JmesPathArgument Transform(JToken json)
        {
            var left = Left.Transform(json).AsJToken();
            var right = Right.Transform(json).AsJToken();

            var result = Compare(left, right);

            return result == null
                ? JmesPathArgument.Null
                : result.Value ?
                    JmesPathArgument.True
                    : JmesPathArgument.False
                ;
        }

        public override void Validate()
        {
            Left.Validate();
            Right.Validate();
        }
    }
}